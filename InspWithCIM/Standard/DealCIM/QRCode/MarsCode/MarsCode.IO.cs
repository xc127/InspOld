using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DealCIM
{
    partial class MarsCode
    {
        public override char EOF => '\r';

        public override void Write()
        {
            serialPort.DiscardInBuffer();
            string temps = "670D";
            byte[] tempb = new byte[2];
            int j = 0;
            for (int i = 0; i < temps.Length; i += 2, j++)
            {
                tempb[j] = Convert.ToByte(temps.Substring(i, 2), 16);
            }
            //MessageBox.Show(string.Format("给串口写入:", tempb));
            serialPort.Write(tempb, 0, tempb.Length);
        }
    }
}
