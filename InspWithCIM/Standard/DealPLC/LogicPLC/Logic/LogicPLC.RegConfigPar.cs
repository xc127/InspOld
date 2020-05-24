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
        Mutex g_MtRegConfigPar= new Mutex();
        #endregion 定义

        #region 读配置参数寄存器
        /// <summary>
        /// 读入指定位置的数据寄存器
        /// </summary>
        public double ReadRegConfigPar(int index)
        {
            g_MtRegConfigPar.WaitOne();
            try
            {
                //数据判断
                int[] intValue = new int[2];
                bool blResult = ReadBlockReg(RegConfigPar.R_I[index].NameReg.Replace("\\n", "\n"), 2, "ReadRegConfigPar", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegConfigPar寄存器失败:" + RegConfigPar.R_I[index].NameReg, "red");
                    return 0;
                }
                double dblValue = (double)(ConvertReadData(intValue[0], intValue[1])) * RegConfigPar.R_I[index].Co;
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return 0;
            }
            finally
            {
                g_MtRegConfigPar.ReleaseMutex();
            }
        }

        /// <summary>
        /// 批量读入寄存器
        /// </summary>
        /// <param name="index"></param>
        public double[] ReadRegConfigPar(int index, int num)
        {
            g_MtRegConfigPar.WaitOne();
            try
            {
                //批量读取寄存器名称     
                string reg = GetRegName(RegConfigPar.R_I.Reg_L, index, num);   
                //数据判断
                int[] intValue = new int[num * 2];
                bool blResult = ReadBlockReg(reg.Replace("\\n", "\n"), 2 * num, "ReadRegConfigPar", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegConfigPar批量寄存器失败:" + RegConfigPar.R_I[index].NameReg, "red");
                    return null;
                }
                double[] dblValue = new double[num];
                for (int i = 0; i < num; i++)
                {
                    dblValue[i] = ConvertReadData(intValue[2 * i], intValue[2 * i + 1], RegConfigPar.R_I[i + index]);
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
                g_MtRegConfigPar.ReleaseMutex();
            }
        }    
        #endregion 读配置参数寄存器

        #region 写配置参数寄存器
        /// <summary>
        /// 写入指定位置的数据寄存器
        /// </summary>
        public void WriteRegConfigPar(int index, double data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC//不写入PLC
                {
                    return;
                }
                //数据判断
                double realData = 0;
                if (!ConvertJudgeData_Write(RegConfigPar.R_I[index], data, out realData))
                {
                    return;
                }

                int[] intValue = new int[2];
                intValue[0] = ConvertWriteData(realData)[0];//X
                intValue[1] = ConvertWriteData(realData)[1];

                bool blResult = WriteBlockReg(RegConfigPar.R_I[index].NameReg.Replace("\\n", "\n"), 2, intValue, "WriteCustomData");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写RegConfigPar寄存器失败:" + RegConfigPar.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
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
        public void WriteRegConfigPar(int index, int num, double[] data)
        {
            g_MtData.WaitOne();
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC)//不写入PLC//不写入PLC
                {
                    return;
                }
                //获取寄存器
                List<RegPLC> regData_L = RegConfigPar.R_I.Reg_L.GetRange(index, num);
                string reg = "";
                for (int i = index; i < index + num; i++)
                {
                    reg += RegConfigPar.R_I[i].NameReg;
                }
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

                bool blResult = WriteBlockReg(reg.Replace("\\n", "\n"), num * 2, intValue, "WriteData");
                if (blResult)
                {
                    return;
                }
                CommunicationState_event("PLC写入RegConfigPar批量寄存器失败:" + RegConfigPar.R_I[index].NameReg, "red");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 写配置参数寄存器
    }
}