using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCIM
{
    public partial class VYCode : QRCodeBase
    {
        #region 定义
        new string ClassName = "VYCode";

        static VYCode instance = new VYCode();
        #endregion

        public static VYCode GetInstance()
        {
            if (instance == null)
                instance = new VYCode();
            return instance;
        }

        private VYCode() : base()
        {
            serialPort.DtrEnable = true;
            serialPort.Parity = Parity.None;
            serialPort.RtsEnable = true; 
        }

        #region 接口

        #endregion
    }
}
