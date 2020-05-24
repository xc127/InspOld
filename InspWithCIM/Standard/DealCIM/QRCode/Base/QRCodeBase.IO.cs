using BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DealCIM
{
    partial class QRCodeBase
    {
        #region 定义
        public string data = string.Empty;
        public static StrAction GetData_event;
        public static Action OverTime_event;
        #endregion

        #region read
        public virtual void StartMonitor(object delay)
        {
            try
            {
                blReadEnabled = true;
                taskRead = Task.Factory.StartNew(new Action<object>(Read), delay);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        public virtual void Read(object delay)
        {
            int i = 0;
            Thread.Sleep(Int16.Parse(delay.ToString()));
            while (i++ < 40)
            {
                try
                {
                    string content = serialPort.ReadExisting();
                    //Log.L_I.WriteError("Code", "原始：" + content);
                    //if (content.Length != 0)
                    //    Log.L_I.WriteError("Code", content);
                    if (content.Contains(EOF))
                    {
                        content = content.Replace("\n", "").Replace("\r", "");
                        data += content;
                      //  Log.L_I.WriteError("Code", "传入：" + data);
                        GetData_event?.Invoke(data);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(ClassName, ex);
                    return;
                }
                finally
                {
                    data = string.Empty;
                    Thread.Sleep(200);
                }
            }
            OverTime_event?.Invoke();
        }

        public virtual void Write()
        { }
        #endregion
    }
}
