using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using System.Threading;
using BasicClass;
using System.Diagnostics;
using HslCommunication;
using HslCommunication.Profinet;
using HslCommunication.Profinet.Melsec;

namespace DealPLC
{
    public class PortPLC_MITHls : BasePortPLC
    {
        #region 定义
        string NameClass = "";

        private MelsecMcNet Melsec_net = null;
        Mutex g_MtWrite = new Mutex();
        #endregion 定义

        #region 初始化
        public PortPLC_MITHls()
        {
            NameClass = "PLC_MITHis";

            //新建实例
            Melsec_net = new MelsecMcNet(ParSetPLC.P_I.IP, ParSetPLC.P_I.Port);
        }
        #endregion 初始化

        #region 连接PLC
        //打开通信接口
        public override bool OpenPLC(out string error)
        {
            error = "";
            try
            {
                OperateResult connect = Melsec_net.ConnectServer();
                Melsec_net.ConnectTimeOut = 600; // 网络连接的超时时间
                Melsec_net.ReceiveTimeOut = 1000; // 网络读取的超时时间

                if (connect.IsSuccess)
                {
                    return true;
                }
                error = connect.ErrorCode.ToString() + connect.Message;
                Log.L_I.WriteError(NameClass + "Error", connect.ErrorCode.ToString() + connect.Message);
                return false;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        //关闭通信接口
        public override bool ClosePLC()
        {
            try
            {
                //首先关闭实时读取
                ParLogicPLC.P_I.BlRead = false;
                Thread.Sleep(100);

                OperateResult connect = Melsec_net.ConnectClose();
                if (connect.IsSuccess)
                {
                    return true;
                }
                Log.L_I.WriteError(NameClass + "Error", connect.ErrorCode.ToString() + connect.Message);

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
                OperateResult<UInt16> result = Melsec_net.ReadUInt16(reg);
                if (result.IsSuccess)
                {
                    intValue = result.Content;
                    return true;
                }
                Log.L_I.WriteError(NameClass + "Error", result.ErrorCode.ToString() + result.Message);
                intValue = 0;
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
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
                string startReg = reg.Split('\n')[0];
                OperateResult<UInt16[]> result = Melsec_net.ReadUInt16(startReg, (ushort)intNum);
                if (result.IsSuccess)
                {
                    for (int i = 0; i < result.Content.Length; i++)
                    {
                        intValue[i] = result.Content[i];
                    }
                    return true;
                }
                Log.L_I.WriteError(NameClass + "Error", result.ErrorCode.ToString() + result.Message);

                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        public override bool ReadBlockReg_Continue(string reg, int intNum, out int[] intValue)
        {
            intValue = new int[intNum];
            try
            {
                string startReg = reg.Split('\n')[0];
                OperateResult<UInt16[]> result = Melsec_net.ReadUInt16(startReg, (ushort)intNum);
                if (result.IsSuccess)
                {
                    for (int i = 0; i < result.Content.Length; i++)
                    {
                        intValue[i] = (int)result.Content[i];
                    }
                    return true;
                }
                Log.L_I.WriteError(NameClass + "Error", result.ErrorCode.ToString() + result.Message);

                return false;
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

                OperateResult result = null;
                Stopwatch sw_WritePLC = new Stopwatch();
                if (BlOpen)
                {
                    sw_WritePLC.Start();

                    result = Melsec_net.Write(reg, intValue);

                    sw_WritePLC.Stop();
                }

                if (ParSetPLC.P_I.regHeartBeat != reg)//不记录心跳的写入
                {
                    LogPLC.L_I.WritePLC(NameClass, strFunName, reg.Replace("\n", ",").PadLeft(7, ' '), intValue.ToString(), sw_WritePLC.ElapsedMilliseconds.ToString());
                }
                if (!BlOpen)
                {
                    Log.L_I.WriteError(NameClass + "Error", "PLC　is Closed");
                    return false;
                }
                if (result.IsSuccess)
                {
                    return true;
                }
                Log.L_I.WriteError(NameClass + "Error", result.ErrorCode.ToString() + result.Message);
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
                short[] sValue = new short[intValue.Length];
                for (int i = 0; i < sValue.Length; i++)
                {
                    sValue[i] = (short)intValue[i];
                }
                string startReg = reg.Split('\n')[0];

                OperateResult result = null;
                Stopwatch sw_WritePLC = new Stopwatch();
                if (BlOpen)
                {
                    sw_WritePLC.Start();

                    result = Melsec_net.Write(startReg, sValue);

                    sw_WritePLC.Stop();
                }

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

                    if (ParSetPLC.P_I.regHeartBeat != reg)//不记录心跳的写入
                    {
                        LogPLC.L_I.WritePLC(NameClass, strFunName + "-the reg must be continuous", reg.Replace("\n", ","), strValue, sw_WritePLC.ElapsedMilliseconds.ToString());
                    }
                }
                catch
                {

                }
                if (!BlOpen)
                {
                    Log.L_I.WriteError(NameClass + "Error", "PLC　is Closed");
                    return false;
                }

                if (result.IsSuccess)
                {
                    return true;
                }
                Log.L_I.WriteError(NameClass + "Error", result.ErrorCode.ToString() + result.Message);

                return false;
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
