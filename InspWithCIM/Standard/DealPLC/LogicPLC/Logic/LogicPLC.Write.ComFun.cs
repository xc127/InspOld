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
        bool WriteReg(string reg, int value)
        {
            g_mtWrite.WaitOne();
            try
            {
                //离线，不写入PLC
                if (RegeditPLC.R_I.BlOffLinePLC)
                {
                    return true;
                }

                bool blResult = false;
                if (ParSetPLC.P_I.BlAnnotherPLC)
                {
                    ComDataPLC c_I = new ComDataPLC(FunPLC_enum.WriteBlockReg, reg, 1, "", value);
                    blResult = WriteData(c_I);
                }
                else
                {
                    blResult = PortPLC_I.g_BasePortPLC.WriteReg(reg, value, "WritePLC");
                }

                if (!blResult)
                {
                    CommunicationState_event("WriteReg写入寄存器失败:" + reg, "red");
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
                g_mtWrite.ReleaseMutex();
            }
        }


        /// <summary>
        /// 写批量寄存器
        /// </summary>
        public bool WriteBlockReg(string reg, int num, int[] value, string nameFun)
        {
            g_mtWriteBlock.WaitOne();
            try
            {
                bool blResult = false;
                if (ParSetPLC.P_I.BlAnnotherPLC)
                {
                    ComDataPLC c_I = new ComDataPLC(FunPLC_enum.WriteBlockReg, reg, num, nameFun, value);
                    blResult = WriteData(c_I);
                }
                else
                {
                    blResult = PortPLC_I.g_BasePortPLC.WriteBlockReg(reg.Replace("\\n", "\n"), num, value, nameFun);
                }
                if (!blResult)
                {
                    CommunicationState_event("WriteBlockReg写入寄存器失败:" + reg, "red");
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
                g_mtWriteBlock.ReleaseMutex();
            }
        }
    }
}
