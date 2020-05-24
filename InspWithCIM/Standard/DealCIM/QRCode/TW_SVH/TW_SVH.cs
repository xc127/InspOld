using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace DealCIM
{
    public partial class TW_SVH:QRCodeBase
    {
        #region 定义
        new string ClassName = "TW_SVHCode";

        static TW_SVH instance = null;

        #endregion

        public static TW_SVH GetInstance()
        {
            if (instance == null)
                instance = new TW_SVH();
            return instance;
        }

        private TW_SVH() : base()
        {
            serialPort.DtrEnable = true;
            serialPort.Parity = Parity.None;
            serialPort.RtsEnable = true;
        }
    }
}
