using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BasicClass;

namespace DealCIM
{
    partial class VYCode
    {
        #region 定义
        readonly byte[] CRLF = { Convert.ToByte("1B", 16), Convert.ToByte("50", 16), Convert.ToByte("00", 16), Convert.ToByte("FF", 16), Convert.ToByte("0D", 16) };
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

        //public override void Read()
        //{
        //    while (blReadEnabled)
        //    {
        //        Thread.Sleep(100);
        //        try
        //        {
        //            string content = serialPort.ReadExisting();
        //            if (!content.Contains('\r'))
        //                data += content;
        //            else
        //            {
        //                content = content.Replace("\n", "").Replace("\r", "");
        //                data += content;
        //                GetData_event(data);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.L_I.WriteError(ClassName, ex);
        //        }
        //        finally
        //        {
        //            data = string.Empty;
        //        }
        //    }
        //}
        #endregion
    }
}
