using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using BasicClass;

namespace SetPar
{
    public class FunMemory : BaseClass
    {
        #region 初始化
        public FunMemory()
        {
            NameClass = "FunMemory";
        }
        #endregion 初始化

        /// <summary>
        /// 记录内存到本地
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="pos"></param>
        /// <param name="annotation"></param>
        /// <returns></returns>
        public void RecordMemory(string camera, int pos, double pre, double deal)
        {
            try
            {
                string info = string.Format("Pos{0}处理前:{1}M;处理后:{2}M;差:{3}M", pos.ToString(), pre.ToString("f3"), deal.ToString("f3"), (deal - pre).ToString("f3"));
                Log.L_I.WriteError("Memory" + camera, info);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 获取当前的进程值
        /// </summary>
        /// <returns></returns>
        public double GetMemory()
        {
            try
            {
                Process cur = Process.GetCurrentProcess();
                PerformanceCounter pf = new PerformanceCounter("Process", "Working Set - Private", cur.ProcessName);   //第二个参数就是得到只有工作集
                double memory = pf.NextValue() / 1024.0 / 1024.0;
                return memory;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return 0;
            }
        }
    }
}
