using BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealCIM
{
    partial class TW_SVH
    {
        #region 定义
        readonly byte[] CRLF = { Convert.ToByte("0D", 16), Convert.ToByte("0A", 16) };
        #endregion

        #region write
        public override void Write()
        {
            try
            {
                serialPort.DiscardInBuffer();
                serialPort.WriteLine("READ\r");
                //serialPort.Write(CRLF, 0, CRLF.Length);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }
        #endregion
    }
}
