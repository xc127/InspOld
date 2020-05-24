#define MIT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using System.Threading;
using BasicClass;
using System.Diagnostics;

#if MIT
using ACTETHERLib;
using ACTMULTILib;
using ActMsgWrapperLib;
using ActUtlWrapperLib;
#endif

namespace DealPLC
{
    public class PortPLC_MIT : BasePortPLC
    {
        #region 定义
        string NameClass = "";

#if MIT
        //组件控制
        //ActEasyIFClass ActEasyIFClass_Inst = null;
        ActUtlWrap ActEasyIFClass_Inst = null;
#endif

        Mutex g_MtWrite = new Mutex();
        #endregion 定义

        #region 初始化
        public PortPLC_MIT()
        {
            NameClass = "PLC_MIT";
        }
        #endregion 初始化

        #region 连接PLC
        //打开通信接口
        public override bool OpenPLC(out string error)
        {
            error = "";
            try
            {
#if MIT
                StartWrapper();//打开程序
                Thread.Sleep(500);

                //如果为Null，实例化对象
                if (ActEasyIFClass_Inst == null)
                {
                    ActEasyIFClass_Inst = new ActUtlWrap();
                }

                ActEasyIFClass_Inst.ActLogicalStationNumber = ParSetPLC.P_I.NoStation; //逻辑站号
                int intState = ActEasyIFClass_Inst.Open();
                if (intState == 0)
                {
                    return true;
                }
                else
                {
                    Log.L_I.WriteError(NameClass, intState.ToString());
                }
                //如果打开失败，重新打开一次            
                intState = ActEasyIFClass_Inst.Open();
                if (intState == 0)
                {
                    return true;
                }
#endif
                return false;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        #region 启动外部通信程序
        void StartWrapper()
        {
            try
            {
                try
                {
                    //Process pro = Process.GetProcessById(38148);
                    //string name = pro.ProcessName;
                    //Process[] pPLC = Process.GetProcessesByName(name);//GetProcessesByName("ActUtlWrapper.exe");//实例化进程对象
                    Process[] pPLC = Process.GetProcessesByName("ActUtlWrapper");
                    foreach (Process p in pPLC)
                    {
                        p.Kill();//关闭进程
                    }
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(NameClass, ex);
                }

                Process m_Process = null;
                m_Process = new Process();
                m_Process.StartInfo.FileName = @"C:\Mod\x86\ActUtlWrapper.exe";
                m_Process.Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 启动外部通信程序

        //关闭通信接口
        public override bool ClosePLC()
        {
            try
            {
                //首先关闭实时读取
                ParLogicPLC.P_I.BlRead = false;
                Thread.Sleep(100);
#if MIT
                ActEasyIFClass_Inst.Close();
#endif
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 连接PLC

        #region 读取寄存器
        public override bool ReadReg(string reg, out int intValue)
        {
            intValue = 0;
            try
            {
#if MIT
                int state = ActEasyIFClass_Inst.GetDevice(reg, out intValue);
                if (state != 0)
                {
                    Log.L_I.WriteError(NameClass + "Error", state.ToString());
                    intValue = 0;
                    return false;
                }
#endif
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {

            }
        }

        /// <summary>
        /// 读取批量寄存器
        /// </summary>
        /// <param name="reg">批量寄存器</param>
        /// <param name="intNum">读取个数</param>
        /// <param name="intValue">输出数组</param>
        /// <returns></returns>
        public override bool ReadBlockReg(string reg, int intNum, out int[] intValue)
        {
            intValue = new int[intNum];
            try
            {
#if MIT
                int intState = ActEasyIFClass_Inst.ReadDeviceRandom(reg, intNum, out intValue[0]);
                if (intState != 0)
                {
                    Log.L_I.WriteError(NameClass + "Error", intState.ToString());
                    return false;
                }
#endif
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 连续的寄存器
        /// </summary>
        public override bool ReadBlockReg_Continue(string reg, int intNum, out int[] intValue)
        {
            intValue = new int[intNum];
            try
            {
#if MIT
                string regStart = reg.Split('\n')[0];
                int intState = ActEasyIFClass_Inst.ReadDeviceBlock(regStart, intNum, out intValue[0]);
                if (intState != 0)
                {
                    Log.L_I.WriteError(NameClass + "Error", intState.ToString());
                    return false;
                }
#endif
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 读取寄存器

        #region 写入寄存器
        public override bool WriteReg(string reg, int intValue, string strFunName)
        {
            g_MtWrite.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    LogPLC.L_I.WritePLC("PLCWrite", "PLC离线");
                    return true;
                }
                int intState = 0;
                Stopwatch sw_WritePLC = new Stopwatch();
                sw_WritePLC.Start();
#if MIT
                intState = ActEasyIFClass_Inst.SetDevice(reg, intValue);

#endif
                if (ParSetPLC.P_I.regHeartBeat != reg)//不记录心跳的写入
                {
                    sw_WritePLC.Stop();
                    LogPLC.L_I.WritePLC(NameClass, strFunName, reg.Replace("\n", ",").PadLeft(7, ' '), intValue.ToString(), sw_WritePLC.ElapsedMilliseconds.ToString());
                }

                if (intState != 0)
                {
                    Log.L_I.WriteError(NameClass + "Error", "WriteReg", intState.ToString());
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                g_MtWrite.ReleaseMutex();
            }
        }

        //写入批量寄存器
        public override bool WriteBlockReg(string reg, int intNum, int[] intValue, string strFunName)
        {
            g_MtWrite.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    LogPLC.L_I.WritePLC(NameClass, "PLC离线");
                    return true;
                }
                Stopwatch sw_WritePLC = new Stopwatch();
                sw_WritePLC.Start();
#if MIT               
                int intState = ActEasyIFClass_Inst.WriteDeviceRandom(reg, intNum, ref intValue[0]);
                //记录数据
                try
                {
                    string strValue = "";
                    for (int i = 0; i < intNum / 2; i++)
                    {
                        strValue += (ConvertData(intValue[2 * i], intValue[2 * i + 1])).ToString() + ",";
                    }
                    if (intNum % 2 == 0)
                    {

                    }
                    else
                    {
                        strValue += intValue[intNum - 1].ToString();
                    }
                    sw_WritePLC.Stop();
                    LogPLC.L_I.WritePLC(NameClass, strFunName, reg.Replace("\n", ","), strValue, sw_WritePLC.ElapsedMilliseconds.ToString());

                }
                catch
                {

                }
                if (intState != 0)
                {
                    Log.L_I.WriteError(NameClass + "Error", intState.ToString());
                    return false;
                }
#endif
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                g_MtWrite.ReleaseMutex();
            }
        }

        public override bool WriteBlockReg(string reg, int[] intValue)
        {
            return false;
        }
        #endregion 写入寄存器

        int ConvertData(int[] value)
        {
            try
            {
                Byte[] byarrTemp;
                Byte[] byarrBufferByte = { 0, 0, 0, 0 };
                for (int i = 0; i < 2; i++)
                {
                    byarrTemp = BitConverter.GetBytes(value[i]);
                    byarrBufferByte[i * 2] = byarrTemp[0];
                    byarrBufferByte[i * 2 + 1] = byarrTemp[1];
                }
                return BitConverter.ToInt32(byarrBufferByte, 0);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return 0;
            }
        }
        double ConvertData(int value1, int value2)
        {
            try
            {
                int[] value = new int[2];
                value[0] = value1;
                value[1] = value2;
                return (double)ConvertData(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return 0;
            }
        }
    }
}
