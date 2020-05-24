using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BasicClass;

namespace DealCIM
{
    partial class SVHCode
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
                serialPort.WriteLine("Read\r");
                serialPort.Write(CRLF, 0, CRLF.Length);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }
        #endregion
    }
}
