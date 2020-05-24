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

#if MIT
using ACTETHERLib;
using ACTMULTILib;
#endif

namespace DealPLC
{
    public partial class LogicPLC
    {
        #region 定义
        //线程互斥
        Mutex g_MtRead = new Mutex();
        Mutex g_MtReadBlock = new Mutex();
        Mutex g_MtConvertRead = new Mutex();
        #endregion 定义        

        /// <summary>
        /// 读取单个寄存器
        /// </summary>
        bool ReadReg(string reg, out int value)
        {
            value = 0;
            g_MtRead.WaitOne();
            try
            {
                int intValue = 0;
                bool blResult = false;
                if (ParSetPLC.P_I.BlAnnotherPLC)
                {
                    ComDataPLC c_I = new ComDataPLC(FunPLC_enum.ReadReg, reg, 1, "", value);
                    blResult = ReadData(c_I, out value);
                }
                else
                {
                    blResult = PortPLC_I.g_BasePortPLC.ReadReg(reg, out intValue);
                }                

                return blResult;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                g_MtRead.ReleaseMutex();
            }
        }
           
        
        /// <summary>
        /// 批量读取寄存器
        /// </summary>
        bool ReadBlockReg(string reg, int num, string namefun, out int[] value)
        {
            g_MtReadBlock.WaitOne();
            value = null;
            try
            {
                bool blResult = false;
                if (ParSetPLC.P_I.BlAnnotherPLC)
                {
                    ComDataPLC c_I = new ComDataPLC(FunPLC_enum.ReadBlockReg, reg, num, namefun, value);
                    blResult = ReadData(c_I, out value);
                }
                else
                {
                    blResult = PortPLC_I.g_BasePortPLC.ReadBlockReg(reg.Replace("\\n", "\n"), num, out value);
                }
                return blResult;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                g_MtReadBlock.ReleaseMutex();
            }
        }


        /// <summary>
        /// 批量读取寄存器，连续地址
        /// </summary>
        bool ReadBlockReg_Continue(string reg, int num, string namefun, out int[] value)
        {
            g_MtReadBlock.WaitOne();
            value = null;
            try
            {
                bool blResult = false;
                if (ParSetPLC.P_I.BlAnnotherPLC)
                {
                    ComDataPLC c_I = new ComDataPLC(FunPLC_enum.ReadBlockReg, reg, num, namefun, value);
                    blResult = ReadData(c_I, out value);
                }
                else
                {
                    blResult = PortPLC_I.g_BasePortPLC.ReadBlockReg_Continue(reg.Replace("\\n", "\n"), num, out value);
                }
                return blResult;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                g_MtReadBlock.ReleaseMutex();
            }
        }
        #region 数据转换
        /// <summary>
        /// 两个寄存器转换成整数
        /// </summary>
        int ConvertReadData(int[] value)
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
                return int.MaxValue;
            }
        }

        double ConvertReadData(int value1, int value2)
        {
            try
            {
                int[] value = new int[2];
                value[0] = value1;
                value[1] = value2;
                return (double)ConvertReadData(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return int.MaxValue;
            }            
        }

        /// <summary>
        /// 最终调用
        /// </summary>
        double ConvertReadData(int value1, int value2, RegPLC regPLC)
        {
            g_MtConvertRead.WaitOne();
            try
            {
                return Math.Round(ConvertReadData(value1, value2) * regPLC.Co,4);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return int.MaxValue;
            }
            finally
            {
                g_MtConvertRead.ReleaseMutex();
            }
        }
        #endregion 数据转换
    }
}
