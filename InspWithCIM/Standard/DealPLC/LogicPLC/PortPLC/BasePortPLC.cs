using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealPLC
{
    public abstract class BasePortPLC
    {
        public bool BlOpen = true;

        #region 连接PLC
        //打开通信接口
        public abstract bool OpenPLC(out string error);
        
        //关闭通信接口
        public abstract bool ClosePLC();        
        #endregion 连接PLC

        #region 读取PLC
        //读取寄存器
        public abstract bool ReadReg(string reg, out int intValue);        

        //读取寄存器
        public abstract bool ReadBlockReg_Continue(string reg, int intNum, out int[] intValue);
      
        public abstract bool ReadBlockReg(string reg, int intNum, out int[] intValue);
       
        #endregion 读取PLC

        #region  写入PLC
        //写入寄存器
        public abstract bool WriteReg(string reg, int intValue,string strFunName);
       
        public abstract bool WriteBlockReg(string reg, int[] intValue);

        public abstract bool WriteBlockReg(string reg, int intNum, int[] intValue, string strFunName);
        
        #endregion 写入PLC
    }
}
