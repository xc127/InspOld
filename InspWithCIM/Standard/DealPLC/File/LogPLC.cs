using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Common;
using BasicClass;
using System.Threading;

namespace DealPLC
{
    public partial class LogPLC : Log
    {
        #region 静态类实例
        public new static LogPLC L_I = new LogPLC();
        #endregion 静态类实例

        #region 定义
        Mutex mt_LogPLC = new Mutex();
        string PathPLCLog
        {
            get
            {
                string path = ParPathRoot.PathRoot + "软件运行记录\\PLC通信日志\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        #endregion 定义

        /// <summary>
        /// 保存信息
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="strInfo"></param>
        public void WritePLC(string strFileName, string strInfo)
        {
            mt_LogPLC.WaitOne();
            try
            {
                if (!Directory.Exists(PathPLCLog))
                {
                    Directory.CreateDirectory(PathPLCLog);
                }

                //获取当前日期
                DateTime dt = DateTime.Now;
                string strPath = CreateAllTimeFile(PathPLCLog);
                //文件
                string strFileLog = strPath + strFileName + ".Log";
                using (StreamWriter sw = new StreamWriter(strFileLog, true, Encoding.Default))
                {
                    //写入时间
                    sw.WriteLine("******" + dt.ToString("HH:mm:ss:fff") + "******");
                    sw.WriteLine(strInfo);
                    sw.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogPLC", ex);
            }
            finally
            {
                mt_LogPLC.ReleaseMutex();
            }
        }

        /// <summary>
        /// 保存信息 使用当时记录的时间
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="strInfo"></param>
        public void WritePLC(string strFileName, string strInfo, DateTime dt)
        {
            mt_LogPLC.WaitOne();
            try
            {
                if (!Directory.Exists(PathPLCLog))
                {
                    Directory.CreateDirectory(PathPLCLog);
                }

                string strPath = CreateAllTimeFile(PathPLCLog);
                //文件
                string strFileLog = strPath + strFileName + ".Log";
                using (StreamWriter sw = new StreamWriter(strFileLog, true, Encoding.Default))
                {
                    //写入时间
                    sw.WriteLine("******" + dt.ToString("HH:mm:ss:fff") + "******");
                    sw.WriteLine(strInfo);
                    sw.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogPLC", ex);
            }
            finally
            {
                mt_LogPLC.ReleaseMutex();
            }
        }

        /// <summary>
        /// 记录机PLC寄存器的值变化
        /// </summary>
        /// <param name="strFileName"></param>
        /// <param name="strReg"></param>
        /// <param name="strInfo"></param>
        public void WritePLC(string strFileName, string strReg, string strInfo)
        {
            mt_LogPLC.WaitOne();
            try
            {
                if (!Directory.Exists(PathPLCLog))
                {
                    Directory.CreateDirectory(PathPLCLog);
                }

                //获取当前日期
                DateTime dt = DateTime.Now;
                string strPath = CreateAllTimeFile(PathPLCLog);
                //文件
                string strFileLog = strPath + strFileName + ".Log";
                using (StreamWriter sw = new StreamWriter(strFileLog, true, Encoding.Default))
                {
                    //写入时间
                    sw.WriteLine("******" + dt.ToString("HH:mm:ss:fff") + "******");
                    sw.WriteLine(strReg);
                    sw.WriteLine(strInfo);
                    sw.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogPLC", ex);
            }
            finally
            {
                mt_LogPLC.ReleaseMutex();
            }
        }

        public void WritePLC(string strFileName, string strFunName, string strReg, string strInfo, string time)
        {
            mt_LogPLC.WaitOne();
            try
            {
                //获取当前日期
                DateTime dt = DateTime.Now;
                string strPath = CreateAllTimeFile(PathPLCLog);
                //文件
                string strFileLog = strPath + strFileName + ".Log";
                using (StreamWriter sw = new StreamWriter(strFileLog, true, Encoding.Default))
                {
                    //写入时间
                    sw.WriteLine("******" + dt.ToLongTimeString() + "******");
                    sw.WriteLine(strFunName);
                    sw.WriteLine(strReg);
                    sw.WriteLine(strInfo);
                    sw.WriteLine("Time:" + time);
                    sw.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                WriteError("LogPLC", ex);
            }
            finally
            {
                mt_LogPLC.ReleaseMutex();
            }
        }
    }
}
