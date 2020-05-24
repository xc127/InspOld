using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCIM
{
    public partial class SVHCode : QRCodeBase
    {
        #region 定义
        new string ClassName = "SVHCode";

        static SVHCode instance = null;

        #endregion

        public static SVHCode GetInstance()
        {
            if (instance == null)
                instance = new SVHCode();
            return instance;
        }

        private SVHCode() : base()
        {
            serialPort.DtrEnable = true;
            serialPort.Parity = Parity.None;
            serialPort.RtsEnable = true;
        }

        #region 接口
        //public override void Init()
        //{
        //    base.Init();
        //}

        //public override void Close()
        //{
        //    base.Close();
        //}

        //public override bool Connect()
        //{
        //    //TODO
        //    return base.Connect();
        //}

        //public override bool DisConnect()
        //{
        //    //TODO  
        //    return base.DisConnect();
        //}
        #endregion
    }
}
