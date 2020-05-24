using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using BasicClass;

namespace DealPLC
{
    partial class ParSetPLC
    {
        #region 循环读取寄存器
        //写入Ini
        public void WriteIniRegCycle()
        {
            //循环读取寄存器
            I_I.WriteIni("CycReg", "CycReg", RegCyc, ParSetPLC.c_PathCyc);
        }
        #endregion 循环读取寄存器

        #region PLC类型
        public bool WrtiteIniTypePLC()
        {
            try
            {
                I_I.WriteIni("PLCModel", "Model", TypePLC_e.ToString(), ParSetPLC.c_PathPLC);
                I_I.WriteIni("PLCModel", "BlReadCycReg", BlReadCycReg.ToString(), ParSetPLC.c_PathPLC);//是否循环读取PLC
                I_I.WriteIni("PLCModel", "Protocol", TypePLCProtocol_e.ToString(), ParSetPLC.c_PathPLC);//协议版本
                I_I.WriteIni("PLCModel", "BlRSingleTaskCamera", BlRSingleTaskCamera.ToString(), ParSetPLC.c_PathPLC);//单线程触发
                I_I.WriteIni("PLCModel", "Delay", Delay.ToString(), ParSetPLC.c_PathPLC);//循环读取延迟

                I_I.WriteIni("PLCModel", "BlAnnotherPLC", BlAnnotherPLC.ToString(), ParSetPLC.c_PathPLC);//PLC独立通信
                I_I.WriteIni("PLCModel", "BlAnnotherPLCLog", BlAnnotherPLCLog.ToString(), ParSetPLC.c_PathPLC);//PLC独立通信

                I_I.WriteIni("PLCModel", "IP", IP.ToString(), ParSetPLC.c_PathPLC);//IP
                I_I.WriteIni("PLCModel", "Port", Port.ToString(), ParSetPLC.c_PathPLC);//PLC独立通信

                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion PLC类型
    }
    
}
