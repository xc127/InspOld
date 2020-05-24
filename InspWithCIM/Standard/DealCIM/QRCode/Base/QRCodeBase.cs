using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicClass;

namespace DealCIM
{
    public partial class QRCodeBase
    {
        #region 定义
        protected string ClassName = "QrCode";
        protected SerialPort serialPort = new SerialPort();
        public bool blReadEnabled = false;
        Task taskRead = null;
        public virtual char EOF => '\n';
        #endregion

        #region 接口
        public QRCodeBase()
        {
        }

        public virtual bool Init(string port, int baudRate)
        {
            serialPort.PortName = port;
            serialPort.BaudRate = baudRate;
            DisConnect();
            if (!Connect())
                return false;

            //StartMonitor();
            return true;
        }

        public virtual void Close()
        {
            try
            {
                DisConnect();
                blReadEnabled = false;
                Task.WaitAll(taskRead);
            }
            catch(Exception ex)
            {
                WriteLog(ex);
            }
        }

        public virtual bool Connect()
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
            return false;
        }

        public virtual bool DisConnect()
        {
            try
            {
                if(serialPort.IsOpen)
                {
                    serialPort.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
            return false;
        }

        void WriteLog(Exception ex)
        {
            Log.L_I.WriteError(ClassName, ex);
        }
        #endregion    
    }
}
