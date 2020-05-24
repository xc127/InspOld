#define MIT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Common;
using BasicClass;
using DealPLC;
using DealConfigFile;
using System.Diagnostics;

#if MIT
using ACTETHERLib;
using ACTMULTILib;
#endif

namespace DealPLC
{
    public partial class LogicPLC
    {
        #region 定义
        Mutex g_MtCyc = new Mutex();
        #endregion 定义


        /// <summary>
        /// 循环读取线程
        /// </summary>
        public void ReadCycReg_Task()
        {
            try
            {
                ParLogicPLC.P_I.BlRead = true;

                Task task = new Task(ReadCycReg);
                task.Start();

                #region 相机触发监控单线程
                if (ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    new Task(TrrigerCamera1).Start();
                    new Task(TrrigerCamera2).Start();
                    new Task(TrrigerCamera3).Start();
                    new Task(TrrigerCamera4).Start();
                    new Task(TrrigerCamera5).Start();
                    new Task(TrrigerCamera6).Start();
                    new Task(TrrigerCamera7).Start();
                    new Task(TrrigerCamera8).Start();
                }
                #endregion 相机触发监控
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        #region 循环读取触发信号及处理_完整版协议
        void ReadCycReg()
        {
            Thread.Sleep(3000);//延迟3s开始读数
            int[] intValue = new int[ParSetPLC.P_I.IntNumCycReg];
            //监控整个周期的时间
            //Stopwatch swPLCTrrigger = new Stopwatch();
            //swPLCTrrigger.Start();

            while (ParLogicPLC.P_I.BlRead)
            {
                Stopwatch swPLC = new Stopwatch();//监控仅仅读取寄存器的时间
                swPLC.Restart();

                Thread.Sleep(2 + ParSetPLC.P_I.Delay);//循环延迟
                g_MtCyc.WaitOne();

                try
                {
                    bool blResult = false;
                    //区分协议
                    if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                    {
                        blResult = ReadBlockReg_Continue(ParSetPLC.P_I.RegCyc, ParSetPLC.P_I.IntNumCycReg, "ReadCycReg", out intValue);//批量读取寄存器
                    }
                    else
                    {
                        blResult = ReadBlockReg(ParSetPLC.P_I.RegCyc, ParSetPLC.P_I.IntNumCycReg, "ReadCycReg", out intValue);//批量读取寄存器
                    }
                    swPLC.Stop();

                    DateTime dt = DateTime.Now;
                    new Task(new Action(() =>
                        {
                            LogPLC.L_I.WritePLC("PLCReadCycRegTime", swPLC.ElapsedMilliseconds.ToString() + "--Delay:" + ParSetPLC.P_I.Delay.ToString(), dt);//将时间记录到本地
                        })).Start();

                    if (blResult || ComValue.C_I.blTrrigerPLC)
                    {
                        try
                        {
                            #region 记录数据
                            string str = "";
                            for (int i = 0; i < ParSetPLC.P_I.IntNumCycReg; i++)
                            {
                                str += intValue[i].ToString().PadRight(12, ' ');
                            }
                            if (ParLogicPLC.P_I.strOldCycRead != str)
                            {
                                string[] strReg = ParSetPLC.P_I.RegCyc.Split('\n');
                                string regLog = "";
                                for (int i = 0; i < strReg.Length; i++)
                                {
                                    regLog += strReg[i].PadRight(12, ' ');
                                }
                                ParLogicPLC.P_I.strOldCycRead = str;
                                new Task(new Action(() =>
                                    {
                                        LogPLC.L_I.WritePLC("PLCCycRead", regLog, str);
                                    })).Start();

                            }
                            #endregion 记录数据
                            Stopwatch swTrigger = new Stopwatch();
                            swTrigger.Restart();
                            if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//精简版本协议
                            {
                                DealTrrigerSingle_New(intValue);
                            }
                            else
                            {
                                //传入寄存器值，处理触发
                                DealTrrigerSingle(intValue);
                            }
                            swTrigger.Stop();
                            if (swTrigger.ElapsedMilliseconds > 1000)
                            {
                                CommunicationState_event("触发时间大于1S", "red");
                                LogPLC.L_I.WritePLC("TimeOutTrigger", swTrigger.ElapsedMilliseconds.ToString());
                            }
                        }
                        catch (Exception ex)
                        {
                            Thread.Sleep(10);
                            Log.L_I.WriteError("LogicPLC", ex);
                        }
                    }
                    else
                    {
                        Thread.Sleep(10);
                        CommunicationState_event("循环读取PLC监控寄存器失败！", "red");
                    }
                }
                catch (Exception ex)
                {
                    Thread.Sleep(10);
                    Log.L_I.WriteError("LogicPLC", ex);
                }
                finally
                {
                    g_MtCyc.ReleaseMutex();
                    //swPLCTrrigger.Stop();
                    //LogPLC.L_I.WritePLC(NameClass, swPLCTrrigger.ElapsedMilliseconds.ToString());//将时间记录到本地
                }
            }
        }
        #endregion 循环读取触发信号及处理_完整版协议
    }
}
