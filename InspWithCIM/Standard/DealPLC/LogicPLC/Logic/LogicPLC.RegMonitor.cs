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

namespace DealPLC
{
    /// <summary>
    /// 监控寄存器
    /// </summary>
    partial class LogicPLC
    {
        #region 定义
        Mutex g_MtRegMonitor = new Mutex();
        Mutex g_MtRegMonitor_Write = new Mutex();
        #endregion 定义

        #region 读取寄存器数值
        /// <summary>
        /// 读取单个寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double ReadRegMonitor(int index)
        {
            g_MtRegMonitor.WaitOne();
            try
            {
                //数据判断
                int[] data = null;
                bool blResult = PortPLC_I.g_BasePortPLC.ReadBlockReg(RegMonitor.R_I[index].NameReg.Replace("\\n", "\n"), 2, out data);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegMonitor寄存器失败:" + RegMonitor.R_I[index].NameReg, "red");
                    return 0;
                }
                double dblValue = ConvertReadData(data[0], data[1], RegMonitor.R_I[index]);
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return 0;
            }
            finally
            {
                g_MtRegMonitor.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量读入寄存器
        /// </summary>
        public double[] ReadRegMonitor(int index, int num)
        {
            g_MtRegMonitor.WaitOne();
            try
            {
                string reg = GetRegName(RegMonitor.R_I.Reg_L, index, num);     
                //数据判断
                int[] intValue = new int[num * 2];
                bool blResult = ReadBlockReg(reg.Replace("\\n", "\n"), 2 * num, "ReadRegMonitor", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegMonitor批量寄存器失败:" + RegMonitor.R_I[index].NameReg, "red");
                    return null;
                }
                double[] dblValue = new double[num];
                for (int i = 0; i < num; i++)
                {
                    dblValue[i] = ConvertReadData(intValue[2 * i], intValue[2 * i + 1], RegMonitor.R_I[i + index]);
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
                g_MtRegMonitor.ReleaseMutex();
            }
        }    
        #endregion 读取寄存器

        #region 写入寄存器
        /// <summary>
        /// 写入指定位置的数据寄存器
        /// </summary>
        public void WriteRegMonitor(int index, double data)
        {
            g_MtRegMonitor_Write.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC
                {
                    return;
                }
                //数据判断
                double realData = 0;
                if (!ConvertJudgeData_Write(RegMonitor.R_I[index], data, out realData))
                {
                    return;
                }

                int[] intValue = new int[2];
                intValue[0] = ConvertWriteData(realData)[0];
                intValue[1] = ConvertWriteData(realData)[1];

                bool blResult = WriteBlockReg(RegMonitor.R_I[index].NameReg.Replace("\\n", "\n"), 2, intValue, "WriteRegMonitor");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写入寄存器失败:" + RegMonitor.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                g_MtRegMonitor_Write.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量写入寄存器
        /// </summary>
        /// <param name="index"></param>
        /// <param name="num"></param>
        /// <param name="data"></param>
        public void WriteRegMonitor(int index, int num, double[] data)
        {
            g_MtRegMonitor_Write.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC//不写入PLC
                {
                    return;
                }
                //获取寄存器
                List<RegPLC> regData_L = RegMonitor.R_I.Reg_L.GetRange(index, num);
                //批量读取寄存器名称     
                string reg = GetRegName(RegMonitor.R_I.Reg_L, index, num);

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

                bool blResult = WriteBlockReg(reg.Replace("\\n", "\n"), num * 2, intValue, "WriteRegMonitor");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写入寄存器失败:" + RegMonitor.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                g_MtRegMonitor_Write.ReleaseMutex();
            }
        }
        #endregion 写入寄存器

        #region 清理读值保留寄存器
        public void WriteReserveData(int index, double data)
        {
            g_MtRegMonitor_Write.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)
                {
                    return;
                }
                //数据判断
                double realData = 0;
                if (!ConvertJudgeData_Write(RegMonitor.R_I[index], data, out realData))
                {
                    return;
                }

                int[] intValue = new int[2];
                intValue[0] = ConvertWriteData(realData)[0];//X
                intValue[1] = ConvertWriteData(realData)[1];

                bool blResult = WriteBlockReg(RegMonitor.R_I[index].NameReg.Replace("\\n", "\n"), 2, intValue, "WriteReserveData");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写寄存器失败:" + RegMonitor.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                g_MtRegMonitor_Write.ReleaseMutex();
            }
        }
        #endregion 清理读值保留寄存器
    }
}
