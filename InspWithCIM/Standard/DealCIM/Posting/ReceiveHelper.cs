using BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DealCIM
{
    public partial class ReceiveHelper
    {
        #region 定义
        static string ClassName = "ReceiveHelper";

        public static Action DataFlowAlarm_event;
        public static IntAction Monitor_event;
        public static Action FinishMonitor_event;

        public static CancellationTokenSource cts = new CancellationTokenSource();
        static object locker = new object();
        #endregion

        public static void Monitor()
        {            
            try
            {
                string data = string.Empty;
                string key = string.Empty;
                while (true)
                {
                    if (cts.Token.IsCancellationRequested)
                    {
                        break;
                    }

                    int i = 0;
                    while (i++ < PostParams.P_I.iCycTimes && CIM.GetListCnt() > 0)
                    {
                        Log.L_I.WriteError("MCMQ", string.Format("开始第{0}次扫描,当前待Recv数量:{1}", i, CIM.GetListCnt()));

                        Thread.Sleep(200);
                        if (CIM.C_I.ReadData(out data, out key) == CIM.ReturnOK)
                        {
                            CIM.AddDic(key, XMLHelpler.ParseStr(data, key, out PostType type));
                            Log.L_I.WriteError("MCMQ", "ADD" + key);
                            CIM.RemoveIDFromList(key);
                            i = 0;
                        }
                    }
                    if (CIM.GetListCnt() > 5)
                        DataFlowAlarm_event?.Invoke();
                    //Log.L_I.WriteError("MCMQ", "Finish Monitor " + key);
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        public static void Monitor(object xmlParser)
        {
            try
            {
                XmlParserBase parser = (XmlParserBase)xmlParser;
                string data = string.Empty;
                string key = string.Empty;
                while (true)
                {
                    if (cts.Token.IsCancellationRequested)
                    {
                        break;
                    }
                    int i = 0; 

                    lock (locker)
                    {
                        while (CIM.GetListCnt() > 0 && i++ < 3)
                        {
                            Log.L_I.WriteError("MCMQ",
                                   string.Format("开始第{0}次扫描,当前待Recv数量:{1}", i, CIM.GetListCnt()) + "," + CIM.GetListContent());

                            Monitor_event?.Invoke(i);
                            Thread.Sleep(200);
                            if (CIM.C_I.ReadData(out data, out key) == CIM.ReturnOK)
                            {
                                CIM.AddDic(key, parser.ParseStr(data, key, out PostType type));
                                Log.L_I.WriteError("MCMQ", "ADD" + key);
                                if (CIM.RemoveIDFromList(key))
                                    i = 0;
                            }
                            FinishMonitor_event?.Invoke();
                        }
                    }
                    //如果超出次数，就清空不再查询
                    CIM.ClearList();
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        public static bool CheckResult(string key, out bool value)
        {
            if (CIM.CheckDic(key as string, out value))
            {
                return true;
            }
            return false;
        }
    }
}
