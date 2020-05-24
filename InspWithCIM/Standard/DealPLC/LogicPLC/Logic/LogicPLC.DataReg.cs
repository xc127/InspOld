using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BasicClass;

namespace DealPLC
{
    /// <summary>
    /// 读写数据寄存器
    /// </summary>
    partial class LogicPLC
    {
        #region 定义
        public Mutex g_MtData = new Mutex();
        
        #endregion 定义

        #region 读数据寄存器
        #region 读数据寄存器1
        /// <summary>
        /// 读入指定位置的数据寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public double ReadRegData1(int index)
        {
            g_MtData.WaitOne();
            try
            {                
                //数据判断
                int[] intValue = new int[2];
                bool blResult = ReadBlockReg(RegData.R_I[index].NameReg.Replace("\\n", "\n"), 2, "ReadRegData1", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData寄存器失败:" + RegData.R_I[index].NameReg, "red");
                    return 0;
                }
                double dblValue = ConvertReadData(intValue[0], intValue[1], RegData.R_I[index]);
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
                return int.MaxValue;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量读入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public double[] ReadRegData1(int index, int num)
        {
            g_MtData.WaitOne();
            try
            {
                //批量读取寄存器名称     
                string reg = GetRegName(RegData.R_I.Reg_L, index, num);           
                //数据判断
                int[] intValue = new int[num * 2];
                bool blResult = ReadBlockReg(reg.Replace("\\n", "\n"), 2 * num, "ReadRegData1", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData1批量寄存器失败:" + RegData.R_I[index].NameReg, "red");
                    return null;
                }
                double[] dblValue = new double[num];
                for (int i = 0; i < num; i++)
                {
                    dblValue[i] = ConvertReadData(intValue[2 * i], intValue[2 * i + 1], RegData.R_I[i + index]);
                }
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
                return null;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 读数据寄存器1

        #region 读数据寄存器2
        /// <summary>
        /// 读入指定位置的数据寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public double ReadRegData2(int index)
        {
            g_MtData.WaitOne();
            try
            {
                //数据判断
                int[] intValue = new int[2];
                bool blResult = ReadBlockReg(RegData2.R_I[index].NameReg.Replace("\\n", "\n"), 2, "ReadRegData2", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData2寄存器失败:" + RegData2.R_I[index].NameReg, "red");
                    return 0;
                }
                double dblValue = ConvertReadData(intValue[0], intValue[1], RegData2.R_I[index]);
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
                return int.MaxValue;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量读入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public double[] ReadRegData2(int index, int num)
        {
            g_MtData.WaitOne();
            try
            {
                //批量读取寄存器名称     
                string reg = GetRegName(RegData2.R_I.Reg_L, index, num);   
                //数据判断
                int[] intValue = new int[num * 2];
                bool blResult = ReadBlockReg(reg.Replace("\\n", "\n"), 2 * num, "ReadRegData2", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData2批量寄存器失败:" + RegData2.R_I[index].NameReg, "red");
                    return null;
                }
                double[] dblValue = new double[num];
                for (int i = 0; i < num; i++)
                {
                    dblValue[i] = ConvertReadData(intValue[2 * i], intValue[2 * i + 1], RegData2.R_I[i + index]);
                }
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
                return null;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 读数据寄存器2

        #region 读数据寄存器3
        /// <summary>
        /// 读入指定位置的数据寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public double ReadRegData3(int index)
        {
            g_MtData.WaitOne();
            try
            {
                //数据判断
                int[] intValue = new int[2];
                bool blResult = ReadBlockReg(RegData3.R_I[index].NameReg.Replace("\\n", "\n"), 2, "ReadRegData3", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData3寄存器失败:" + RegData3.R_I[index].NameReg, "red");
                    return 0;
                }
                double dblValue = ConvertReadData(intValue[0], intValue[1], RegData3.R_I[index]);
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return int.MaxValue;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量读入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public double[] ReadRegData3(int index, int num)
        {
            g_MtData.WaitOne();
            try
            {
                //批量读取寄存器名称     
                string reg = GetRegName(RegData3.R_I.Reg_L, index, num);   
                //数据判断
                int[] intValue = new int[num * 2];
                bool blResult = ReadBlockReg(reg.Replace("\\n", "\n"), 2 * num, "ReadRegData3", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData3批量寄存器失败:" + RegData3.R_I[index].NameReg, "red");
                    return null;
                }
                double[] dblValue = new double[num];
                for (int i = 0; i < num; i++)
                {
                    dblValue[i] = ConvertReadData(intValue[2 * i], intValue[2 * i + 1], RegData3.R_I[i + index]);
                }
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 读数据寄存器3

        #region 读数据寄存器4
        /// <summary>
        /// 读入指定位置的数据寄存器
        /// </summary>
        public double ReadRegData4(int index)
        {
            g_MtData.WaitOne();
            try
            {
                //数据判断
                int[] intValue = new int[2];
                bool blResult = ReadBlockReg(RegData4.R_I[index].NameReg.Replace("\\n", "\n"), 2, "ReadRegData4", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData4寄存器失败:" + RegData4.R_I[index].NameReg, "red");
                    return 0;
                }
                double dblValue = ConvertReadData(intValue[0], intValue[1], RegData4.R_I[index]);
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return int.MaxValue;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量读入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public double[] ReadRegData4(int index, int num)
        {
            g_MtData.WaitOne();
            try
            {
                //批量读取寄存器名称     
                string reg = GetRegName(RegData4.R_I.Reg_L, index, num);   
                //数据判断
                int[] intValue = new int[num * 2];
                bool blResult = ReadBlockReg(reg.Replace("\\n", "\n"), 2 * num, "ReadRegData4", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData4批量寄存器失败:" + RegData4.R_I[index].NameReg, "red");
                    return null;
                }
                double[] dblValue = new double[num];
                for (int i = 0; i < num; i++)
                {
                    dblValue[i] = ConvertReadData(intValue[2 * i], intValue[2 * i + 1], RegData4.R_I[i + index]);
                }
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
                return null;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 读数据寄存器4

        #region 读数据寄存器5
        /// <summary>
        /// 读入指定位置的数据寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public double ReadRegData5(int index)
        {
            g_MtData.WaitOne();
            try
            {
                //数据判断
                int[] intValue = new int[2];
                bool blResult = ReadBlockReg(RegData5.R_I[index].NameReg.Replace("\\n", "\n"), 2, "ReadRegData5", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData5寄存器失败:" + RegData5.R_I[index].NameReg, "red");
                    return 0;
                }
                double dblValue = ConvertReadData(intValue[0], intValue[1], RegData5.R_I[index]);
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return int.MaxValue;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量读入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public double[] ReadRegData5(int index, int num)
        {
            g_MtData.WaitOne();
            try
            {
                //批量读取寄存器名称     
                string reg = GetRegName(RegData5.R_I.Reg_L, index, num);   
                //数据判断
                int[] intValue = new int[num * 2];
                bool blResult = ReadBlockReg(reg.Replace("\\n", "\n"), 2 * num, "ReadRegData5", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData5批量寄存器失败:" + RegData5.R_I[index].NameReg, "red");
                    return null;
                }
                double[] dblValue = new double[num];
                for (int i = 0; i < num; i++)
                {
                    dblValue[i] = ConvertReadData(intValue[2 * i], intValue[2 * i + 1], RegData5.R_I[i + index]);
                }
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
                return null;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 读数据寄存器5

        #region 读数据寄存器6
        /// <summary>
        /// 读入指定位置的数据寄存器
        /// </summary>
        public double ReadRegData6(int index)
        {
            g_MtData.WaitOne();
            try
            {
                //数据判断
                int[] intValue = new int[2];
                bool blResult = ReadBlockReg(RegData6.R_I[index].NameReg.Replace("\\n", "\n"), 2, "ReadRegData6", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData6寄存器失败:" + RegData6.R_I[index].NameReg, "red");
                    return 0;
                }
                double dblValue = ConvertReadData(intValue[0], intValue[1], RegData6.R_I[index]);
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
                return int.MaxValue;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量读入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public double[] ReadRegData6(int index, int num)
        {
            g_MtData.WaitOne();
            try
            {
                //批量读取寄存器名称     
                string reg = GetRegName(RegData6.R_I.Reg_L, index, num);   
                //数据判断
                int[] intValue = new int[num * 2];
                bool blResult = ReadBlockReg(reg.Replace("\\n", "\n"), 2 * num, "ReadRegData6", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData6批量寄存器失败:" + RegData6.R_I[index].NameReg, "red");
                    return null;
                }
                double[] dblValue = new double[num];
                for (int i = 0; i < num; i++)
                {
                    dblValue[i] = ConvertReadData(intValue[2 * i], intValue[2 * i + 1], RegData6.R_I[i + index]);
                }
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 读数据寄存器6
        #endregion 读数据寄存器

        #region 写数据寄存器
        #region 写入数据寄存器1
        /// <summary>
        /// 写入指定位置的数据寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public void WriteRegData1(int index, double data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                //数据判断
                double realData = 0;
                if (!ConvertJudgeData_Write(RegData.R_I[index], data, out realData))
                {
                    return;
                }

                int[] intValue = new int[2];
                intValue[0] = ConvertWriteData(realData)[0];//X
                intValue[1] = ConvertWriteData(realData)[1];

                bool blResult = WriteBlockReg(RegData.R_I[index].NameReg.Replace("\\n", "\n"), 2, intValue, "WriteRegData1");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("LogicPLC写入寄存器失败:" + RegData.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量写入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public void WriteRegData1(int index, int num, double[] data)
        {
            g_MtData.WaitOne();
            try
            {
                //if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC//不写入PLC
                //{
                //    return;
                //}
                //获取寄存器
                List<RegPLC> regData_L = RegData.R_I.Reg_L.GetRange(index, num);
                //批量读取寄存器名称     
                string reg = GetRegName(RegData.R_I.Reg_L, index, num);   

                //数据判断和转换（乘以系数）
                double[] realData = null;
                if (!ConvertJudgeData_Write(regData_L, data, out realData))
                {
                    return;
                }

                int[] intValue = new int[num * 2];
                for (int i = 0; i < num; i++)
                {
                    intValue[2 * i] = ConvertWriteData(realData[i])[0];//X
                    intValue[2 * i + 1] = ConvertWriteData(realData[i])[1];
                }

                bool blResult = WriteBlockReg(reg.Replace("\\n", "\n"), num * 2, intValue, "WriteRegData1");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("LogicPLC写入寄存器失败:" + RegData.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 写入数据寄存器1

        #region 写入数据寄存器2
        /// <summary>
        /// 写入指定位置的数据寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public void WriteRegData2(int index, double data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                //数据判断
                double realData = 0;
                if (!ConvertJudgeData_Write(RegData2.R_I[index], data, out realData))
                {
                    return;
                }

                int[] intValue = new int[2];
                intValue[0] = ConvertWriteData(realData)[0];//X
                intValue[1] = ConvertWriteData(realData)[1];

                bool blResult = WriteBlockReg(RegData2.R_I[index].NameReg.Replace("\\n", "\n"), 2, intValue, "WriteRegData2");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写RegData2寄存器失败:" + RegData2.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量写入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public void WriteRegData2(int index, int num, double[] data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC//不写入PLC
                {
                    return;
                }
                //获取寄存器
                List<RegPLC> regData_L = RegData2.R_I.Reg_L.GetRange(index, num);
                //批量读取寄存器名称     
                string reg = GetRegName(RegData2.R_I.Reg_L, index, num);  
 
                //数据判断和转换（乘以系数）
                double[] realData = null;
                if (!ConvertJudgeData_Write(regData_L, data, out realData))
                {
                    return;
                }

                int[] intValue = new int[num * 2];
                for (int i = 0; i < num; i++)
                {
                    intValue[2 * i] = ConvertWriteData(realData[i])[0];//X
                    intValue[2 * i + 1] = ConvertWriteData(realData[i])[1];
                }

                bool blResult = WriteBlockReg(reg.Replace("\\n", "\n"), num * 2, intValue, "WriteRegData2");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写RegData2批量寄存器失败:" + RegData2.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 写入数据寄存器2

        #region 写入数据寄存器3
        /// <summary>
        /// 写入指定位置的数据寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public void WriteRegData3(int index, double data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                //数据判断
                double realData = 0;
                if (!ConvertJudgeData_Write(RegData3.R_I[index], data, out realData))
                {
                    return;
                }

                int[] intValue = new int[2];
                intValue[0] = ConvertWriteData(realData)[0];//X
                intValue[1] = ConvertWriteData(realData)[1];

                bool blResult = WriteBlockReg(RegData3.R_I[index].NameReg.Replace("\\n", "\n"), 2, intValue, "WriteRegData3");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写RegData3寄存器失败:" + RegData3.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量写入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public void WriteRegData3(int index, int num, double[] data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                //获取寄存器
                List<RegPLC> regData_L = RegData3.R_I.Reg_L.GetRange(index, num);
                //批量读取寄存器名称     
                string reg = GetRegName(RegData3.R_I.Reg_L, index, num);   

                //数据判断和转换（乘以系数）
                double[] realData = null;
                if (!ConvertJudgeData_Write(regData_L, data, out realData))
                {
                    return;
                }

                int[] intValue = new int[num * 2];
                for (int i = 0; i < num; i++)
                {
                    intValue[2 * i] = ConvertWriteData(realData[i])[0];
                    intValue[2 * i + 1] = ConvertWriteData(realData[i])[1];
                }

                bool blResult = WriteBlockReg(reg.Replace("\\n", "\n"), num * 2, intValue, "WriteRegData3");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写RegData3批量寄存器失败:" + RegData3.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 写入数据寄存器3

        #region 写入数据寄存器4
        /// <summary>
        /// 写入指定位置的数据寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public void WriteRegData4(int index, double data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                //数据判断
                double realData = 0;
                if (!ConvertJudgeData_Write(RegData4.R_I[index], data, out realData))
                {
                    return;
                }

                int[] intValue = new int[2];
                intValue[0] = ConvertWriteData(realData)[0];
                intValue[1] = ConvertWriteData(realData)[1];

                bool blResult = WriteBlockReg(RegData4.R_I[index].NameReg.Replace("\\n", "\n"), 2, intValue, "WriteRegData4");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写RegData6寄存器失败:" + RegData4.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量写入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public void WriteRegData4(int index, int num, double[] data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                //获取寄存器
                List<RegPLC> regData_L = RegData4.R_I.Reg_L.GetRange(index, num);
                //批量读取寄存器名称     
                string reg = GetRegName(RegData4.R_I.Reg_L, index, num);   

                //数据判断和转换（乘以系数）
                double[] realData = null;
                if (!ConvertJudgeData_Write(regData_L, data, out realData))
                {
                    return;
                }

                int[] intValue = new int[num * 2];
                for (int i = 0; i < num; i++)
                {
                    intValue[2 * i] = ConvertWriteData(realData[i])[0];
                    intValue[2 * i + 1] = ConvertWriteData(realData[i])[1];
                }

                bool blResult = WriteBlockReg(reg.Replace("\\n", "\n"), num * 2, intValue, "WriteRegData4");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写RegData4批量寄存器失败:" + RegData4.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 写入数据寄存器4

        #region 写入数据寄存器5
        /// <summary>
        /// 写入指定位置的数据寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public void WriteRegData5(int index, double data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                //数据判断
                double realData = 0;
                if (!ConvertJudgeData_Write(RegData5.R_I[index], data, out realData))
                {
                    return;
                }

                int[] intValue = new int[2];
                intValue[0] = ConvertWriteData(realData)[0];
                intValue[1] = ConvertWriteData(realData)[1];

                bool blResult = WriteBlockReg(RegData5.R_I[index].NameReg.Replace("\\n", "\n"), 2, intValue, "WriteRegData5");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写RegData寄存器失败:" + RegData5.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量写入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public void WriteRegData5(int index, int num, double[] data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                //获取寄存器
                List<RegPLC> regData_L = RegData5.R_I.Reg_L.GetRange(index, num);
                //批量读取寄存器名称     
                string reg = GetRegName(RegData5.R_I.Reg_L, index, num);   

                //数据判断和转换（乘以系数）
                double[] realData = null;
                if (!ConvertJudgeData_Write(regData_L, data, out realData))
                {
                    return;
                }

                int[] intValue = new int[num * 2];
                for (int i = 0; i < num; i++)
                {
                    intValue[2 * i] = ConvertWriteData(realData[i])[0];
                    intValue[2 * i + 1] = ConvertWriteData(realData[i])[1];
                }

                bool blResult = WriteBlockReg(reg.Replace("\\n", "\n"), num * 2, intValue, "WriteRegData5");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写RegData5批量寄存器失败:" + RegData5.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 写入数据寄存器5

        #region 写入数据寄存器6
        /// <summary>
        /// 写入指定位置的数据寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        public void WriteRegData6(int index, double data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                //数据判断
                double realData = 0;
                if (!ConvertJudgeData_Write(RegData6.R_I[index], data, out realData))
                {
                    return;
                }

                int[] intValue = new int[2];
                intValue[0] = ConvertWriteData(realData)[0];
                intValue[1] = ConvertWriteData(realData)[1];

                bool blResult = WriteBlockReg(RegData6.R_I[index].NameReg.Replace("\\n", "\n"), 2, intValue, "WriteRegData6");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写RegData6寄存器失败:" + RegData6.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量写入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public void WriteRegData6(int index, int num, double[] data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                //获取寄存器
                List<RegPLC> regData_L = RegData6.R_I.Reg_L.GetRange(index, num);
                //批量读取寄存器名称     
                string reg = GetRegName(RegData6.R_I.Reg_L, index, num);   

                //数据判断和转换（乘以系数）
                double[] realData = null;
                if (!ConvertJudgeData_Write(regData_L, data, out realData))
                {
                    return;
                }

                int[] intValue = new int[num * 2];
                for (int i = 0; i < num; i++)
                {
                    intValue[2 * i] = ConvertWriteData(realData[i])[0];//X
                    intValue[2 * i + 1] = ConvertWriteData(realData[i])[1];
                }

                bool blResult = WriteBlockReg(reg.Replace("\\n", "\n"), num * 2, intValue, "WriteRegData6");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写RegData6批量寄存器失败:" + RegData6.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 写入数据寄存器6
        #endregion 写数据寄存器

    }
}
