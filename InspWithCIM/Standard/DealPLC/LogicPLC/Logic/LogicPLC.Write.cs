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

#if MIT
using ACTETHERLib;
using ACTMULTILib;
#endif


namespace DealPLC
{
    partial class LogicPLC
    {
        #region 定义
        //Event
        public event StrAction WriteDataOverFlow;
        Mutex g_mtWrite = new Mutex();
        Mutex g_mtClear = new Mutex();
        Mutex g_mtWriteBlock = new Mutex();
        Mutex g_mtConvertWrite = new Mutex();
        #endregion 定义

        #region 清除寄存器
        /// <summary>
        /// 清除PLC寄存器
        /// </summary>
        public void ClearPLC(RegPLC regPLC)
        {
            g_mtClear.WaitOne();
            try
            {
                //离线，不写入PLC
                if (RegeditPLC.R_I.BlOffLinePLC)
                {
                    return;
                }
                ClearPLC(regPLC.NameReg.Replace("\\n", "\n"));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                g_mtClear.ReleaseMutex();
            }
        }

        void ClearPLC(string strReg)
        {
            try
            {
                //离线，不写入PLC
                if (RegeditPLC.R_I.BlOffLinePLC)
                {
                    return;
                }
                string[] str = strReg.Split('\n');
                int[] intValue = new int[str.Length - 1];
                bool blResult = WriteBlockReg(strReg, str.Length - 1, intValue, "ClearPLC");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC清零寄存器失败:" + strReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        #endregion 清除寄存器

        #region 拍照完成以及结果
        public void FinishPhoto(string strReg, int intResult)
        {
            g_mtWrite.WaitOne();
            try
            {
                //离线，不写入PLC
                if (RegeditPLC.R_I.BlOffLinePLC)
                {
                    return;
                }
                string[] str = null;
                int[] intValue = null;
                if (ParSetPLC.P_I.TypePLC_e == TypePLC_enum.MIT_Hls)
                {
                    intValue = new int[2];
                    str = strReg.Split('\n');

                    intValue[0] = 0;//拍照完成清零
                    intValue[1] = intResult;//拍照结果
                }
                else
                {
                    intValue = new int[4];
                    str = strReg.Split('\n');

                    intValue[0] = 0;//拍照完成清零
                    intValue[2] = intResult;//拍照结果
                }


                bool blResult = false;
                if (ParSetPLC.P_I.TypePLC_e == TypePLC_enum.MIT_Hls)
                {
                    blResult = WriteReg(str[0], 0);
                    WriteReg(str[1], intResult);
                }
                else
                {
                    blResult = WriteBlockReg(strReg, str.Length - 1, intValue, "FinishPhoto");
                }
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("FinishPhoto写入寄存器失败:" + strReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                g_mtWrite.ReleaseMutex();
            }
        }
        #endregion 拍照完成以及结果

        #region 写入计算数据和数据传输确认信号
        /// <summary>
        /// 写入相机寄存器
        /// </summary>
        public void WriteCalData(List<RegPLC> Reg_L, double[] dblData, string strFinishDataReg, int intFinish)
        {
            g_mtWrite.WaitOne();
            try
            {
                //离线，不写入PLC
                if (RegeditPLC.R_I.BlOffLinePLC)
                {
                    return;
                }

                //获取寄存器值
                string strReg = GetRegName(Reg_L);
                strReg += strFinishDataReg;

                //数据判断和转换（乘以系数）
                double[] realData = null;
                if (!ConvertJudgeData(Reg_L, dblData, out realData))
                {
                    return;
                }

                int[] intValue = new int[10];
                intValue[0] = ConvertWriteData(realData[0])[0];//X
                intValue[1] = ConvertWriteData(realData[0])[1];
                intValue[2] = ConvertWriteData(realData[1])[0];//Y
                intValue[3] = ConvertWriteData(realData[1])[1];
                intValue[4] = ConvertWriteData(realData[2])[0];//Z
                intValue[5] = ConvertWriteData(realData[2])[1];
                intValue[6] = ConvertWriteData(realData[3])[0];//R
                intValue[7] = ConvertWriteData(realData[3])[1];
                intValue[8] = intFinish;
                intValue[9] = 0;

                bool blResult = WriteBlockReg(strReg, 10, intValue, "WriteCalData");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("WriteCalData写入寄存器失败:" + strReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                g_mtWrite.ReleaseMutex();
            }
        }

        public void WriteCalData(List<RegPLC> Reg_L, double[] dblData, string strFinishDataReg)
        {
            g_mtWrite.WaitOne();
            try
            {
                //离线，不写入PLC
                if (RegeditPLC.R_I.BlOffLinePLC)
                {
                    return;
                }

                //获取寄存器值
                string strReg = GetRegName(Reg_L);
                strReg += strFinishDataReg;

                //数据判断和转换（乘以系数）
                double[] realData = null;
                if (!ConvertJudgeData(Reg_L, dblData, out realData))
                {
                    return;
                }

                int[] intValue = new int[10];
                intValue[0] = ConvertWriteData(realData[0])[0];//X
                intValue[1] = ConvertWriteData(realData[0])[1];
                intValue[2] = ConvertWriteData(realData[1])[0];//Y
                intValue[3] = ConvertWriteData(realData[1])[1];
                intValue[4] = ConvertWriteData(realData[2])[0];//Z
                intValue[5] = ConvertWriteData(realData[2])[1];
                intValue[6] = ConvertWriteData(realData[3])[0];//R
                intValue[7] = ConvertWriteData(realData[3])[1];
                intValue[8] = 1;
                intValue[9] = 0;

                bool blResult = WriteBlockReg(strReg, 10, intValue, "WriteCalData");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("WriteCalData写入寄存器失败:" + strReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                g_mtWrite.ReleaseMutex();
            }
        }
        #endregion 写入计算数据和数据传输确认信号

        #region 写入数据
        /// <summary>
        /// 将数据写入PLC
        /// </summary>
        /// reg里面不包含\n
        public bool WriteData(string[] reg, double[] co, double[] data)
        {
            try
            {
                int numReg = reg.Length;
                int numCo = co.Length;
                int numData = data.Length;
                string strReg = "";

                if (numReg == numCo
                    || numCo == numData)
                {
                    double[] value = new double[numData];
                    for (int i = 0; i < numData; i++)
                    {
                        value[i] = Math.Round(data[i] * co[i], 3);
                    }

                    for (int i = 0; i < numReg; i++)
                    {
                        strReg += reg[i].Replace("\n", "") + "\n";
                    }

                    int[] valueReg = new int[numReg];
                    for (int i = 0; i < numData; i++)
                    {
                        valueReg[2 * i] = ConvertWriteData(value[i])[0];
                        valueReg[2 * i + 1] = ConvertWriteData(value[i])[1];
                    }

                    bool blResult = WriteBlockReg(strReg, numReg, valueReg, "WriteData");
                    if (blResult)
                    {
                        return true;
                    }
                    CommunicationState_event("WriteData写入寄存器失败:" + strReg, "red");
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex, "WriteData寄存器，系数，或数值的数组个数不一样");
                return false;
            }
        }

        public bool WriteData(List<ResultforWritePLC> resultforWritePLC_L)
        {
            g_mtWrite.WaitOne();
            try
            {
                //离线，不写入PLC
                if (RegeditPLC.R_I.BlOffLinePLC)
                {
                    return true;
                }

                //获取寄存器值
                string strReg = GetRegName(resultforWritePLC_L);

                //数据判断和转换（乘以系数）
                double[] realData = null;
                if (!ConvertJudgeData(resultforWritePLC_L, out realData))
                {
                    return false;
                }

                //数据个数
                int count = resultforWritePLC_L.Count;
                int[] intValue = new int[count * 2];
                for (int i = 0; i < resultforWritePLC_L.Count; i++)
                {
                    intValue[2 * i] = ConvertWriteData(realData[i])[0];
                    intValue[2 * i + 1] = ConvertWriteData(realData[i])[1];
                }

                bool blResult = WriteBlockReg(strReg, count * 2, intValue, "WriteCalData");
                if (blResult)
                {
                    return true;
                }
                CommunicationState_event("WriteData写入寄存器失败:" + strReg, "red");
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                g_mtWrite.ReleaseMutex();
            }
        }
        #endregion 写入数据

        #region PCPLC连接
        public void PCConnectPLC()
        {
            try
            {
                //WriteReg(ParSetPLC.P_I.regPCConnectPLC, 1);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        public void PCDisConnectPLC()
        {
            try
            {
                //WriteReg(ParSetPLC.P_I.regPCConnectPLC, 0);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion PCPLC连接

        #region PC报警
        /// <summary>
        /// 上位机出错，具体信息查看上位机报警信息
        /// </summary>
        public void PCAlarm()
        {
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                int[] intValue = new int[2];
                intValue[0] = (int)ErrorPC_enum.Error0;
                bool blResult = WriteBlockReg(ParSetPLC.P_I.regPCALarm, 2, intValue, "PCAlarm");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PCAlarm写入寄存器失败:" + ParSetPLC.P_I.regPCALarm, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        public void PCAlarm(ErrorPC_enum errorPC_e)
        {
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                int[] intValue = new int[2];
                intValue[0] = (int)errorPC_e;
                bool blResult = WriteBlockReg(ParSetPLC.P_I.regPCALarm, 2, intValue, "PCAlarm");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PCAlarm写入寄存器失败:" + ParSetPLC.P_I.regPCALarm, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// PC通知PLC蜂鸣
        /// </summary>
        public void PCBuzzer()
        {
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC//不写入PLC
                {
                    return;
                }
                int[] intValue = new int[2];
                intValue[0] = (int)ErrorPC_enum.Buzzer;
                bool blResult = PortPLC_I.g_BasePortPLC.WriteBlockReg(ParSetPLC.P_I.regPCALarm, 2, intValue, "PCAlarm");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PCBuzzer写入寄存器失败:" + ParSetPLC.P_I.regPCALarm, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion PC报警

        #region Robot通信
        public void PCConnectRobotOK()
        {
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC//不写入PLC
                {
                    return;
                }
                LogicPLC.L_I.WriteRegData1(2, 1);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        public void PCConnectRobotNG()
        {
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC//不写入PLC
                {
                    return;
                }
                LogicPLC.L_I.WriteRegData1(2, 2);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion Robot通信

        #region 清空所有循环读取的寄存器
        /// <summary>
        /// 关闭软件的时候，会清零所有的循环读取寄存器
        /// </summary>
        public void ClearCyc()
        {
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                int[] intValue = new int[ParSetPLC.P_I.IntNumCycReg];

                bool blResult = WriteBlockReg(ParSetPLC.P_I.RegCyc, ParSetPLC.P_I.IntNumCycReg, intValue, "ClearCyc");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写寄存器失败:" + ParSetPLC.P_I.RegCyc, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 清空所有循环读取的寄存器

        #region 数据转换
        /// <summary>
        /// 将数据转换为两个16位
        /// </summary>
        public int[] ConvertWriteData(double value)
        {
            g_mtConvertWrite.WaitOne();
            try
            {
                Byte[] bytes = BitConverter.GetBytes((int)value);
                int[] val = { 0, 0 };
                val[0] = BitConverter.ToInt16(bytes, 0);
                val[1] = BitConverter.ToInt16(bytes, 2);
                return val;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return new int[] { 0, 0 };
            }
            finally
            {
                g_mtConvertWrite.ReleaseMutex();
            }
        }

        /// <summary>
        /// 数据乘以系数转换，以及范围判断
        /// </summary>
        bool ConvertJudgeData(List<RegPLC> RegData_L, double[] data, out double[] realData)
        {
            g_mtConvertWrite.WaitOne();
            realData = null;
            string strError = "";
            try
            {
                int num = data.Length;
                realData = new double[num];
                //数据转换以及判断
                for (int i = 0; i < num; i++)
                {
                    if (data[i] > RegData_L[i].DblMax
                        || data[i] < RegData_L[i].DblMin)
                    {
                        strError = RegData_L[i].NameReg + ",Max:" + RegData_L[i].DblMax + ",Min:" + RegData_L[i].DblMin
                                + ",Real:" + data[i].ToString();
                        WriteDataOverFlow(strError.Replace("\n", "."));//数据超限
                        LogPLC.L_I.WritePLC("ConvertJudgeData", strError, "");
                        return false;
                    }
                    else
                    {
                        //为了兼容老版本的发送是乘以系数，新版本发送是除以系数
                        if (RegData_L[i].Co > 2)
                        {
                            realData[i] = data[i] * RegData_L[i].Co;
                        }
                        else
                        {
                            realData[i] = data[i] / RegData_L[i].Co;
                        }
                    }
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
                g_mtConvertWrite.ReleaseMutex();
            }
        }

        /// <summary>
        /// 对数据寄存器的判断和系数转换
        /// </summary>
        bool ConvertJudgeData_Write(List<RegPLC> RegData_L, double[] data, out double[] realData)
        {
            g_mtConvertWrite.WaitOne();
            realData = null;
            string strError = "";
            try
            {
                int intNum = data.Length;//殷--使用实际的数据个数
                realData = new double[intNum];
                //数据转换以及判断
                for (int i = 0; i < intNum; i++)
                {
                    if (data[i] > RegData_L[i].DblMax
                        || data[i] < RegData_L[i].DblMin)
                    {
                        strError = RegData_L[i].NameReg + ",Max:" + RegData_L[i].DblMax + ",Min:" + RegData_L[i].DblMin
                                + ",Real:" + data[i].ToString();
                        WriteDataOverFlow(strError.Replace("\n", "."));//数据超限
                        LogPLC.L_I.WritePLC("ConvertJudgeData", strError, "");
                        return false;
                    }
                    else
                    {
                        //为了兼容老版本的发送是乘以系数，新版本发送是除以系数
                        if (RegData_L[i].Co > 2)
                        {
                            realData[i] = data[i] * RegData_L[i].Co;
                        }
                        else
                        {
                            realData[i] = data[i] / RegData_L[i].Co;
                        }
                    }
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
                g_mtConvertWrite.ReleaseMutex();
            }
        }

        bool ConvertJudgeData_Write(RegPLC reg, double data, out double realData)
        {
            g_mtConvertWrite.WaitOne();
            realData = 0;
            string strError = "";
            try
            {
                if (data > reg.DblMax
                    || data < reg.DblMin)
                {
                    strError = reg.NameReg + ",Max:" + reg.DblMax + ",Min:" + reg.DblMin
                            + ",Real:" + data.ToString();
                    WriteDataOverFlow(strError);//数据超限
                    return false;
                }
                else
                {
                    //为了兼容老版本的发送是乘以系数，新版本发送是除以系数
                    if (reg.Co > 2)
                    {
                        realData = data * reg.Co;
                    }
                    else
                    {
                        realData = data / reg.Co;
                    }
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
                g_mtConvertWrite.ReleaseMutex();
            }
        }

        bool ConvertJudgeData(List<ResultforWritePLC> resultforWritePLC_L, out double[] realData)
        {
            g_mtConvertWrite.WaitOne();
            realData = null;
            string strError = "";
            try
            {
                int num = resultforWritePLC_L.Count;
                realData = new double[num];
                //数据转换以及判断
                for (int i = 0; i < num; i++)
                {
                    if (resultforWritePLC_L[i].Result > resultforWritePLC_L[i].DblMax
                        || resultforWritePLC_L[i].Result < resultforWritePLC_L[i].DblMin)
                    {
                        strError = resultforWritePLC_L[i].NameReg + ",Max:" + resultforWritePLC_L[i].DblMax + ",Min:" + resultforWritePLC_L[i].DblMin
                                + ",Real:" + resultforWritePLC_L[i].ToString();
                        WriteDataOverFlow(strError.Replace("\n", "."));//数据超限
                        LogPLC.L_I.WritePLC("ConvertJudgeData", strError, "");
                        return false;
                    }
                    else
                    {
                        //为了兼容老版本的发送是乘以系数，新版本发送是除以系数
                        if (resultforWritePLC_L[i].Co > 2)
                        {
                            realData[i] = resultforWritePLC_L[i].Result * resultforWritePLC_L[i].Co;
                        }
                        else
                        {
                            realData[i] = resultforWritePLC_L[i].Result / resultforWritePLC_L[i].Co;
                        }
                    }
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
                g_mtConvertWrite.ReleaseMutex();
            }
        }
        #endregion 数据转换

    }
}
