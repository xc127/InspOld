using System;

using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace DealFile
{
    [Serializable]
    public class TxtFile
    {
        #region 静态类实例
        public static TxtFile DealTxt_Inst=new TxtFile ();
        #endregion 静态类实例

        #region 定义
        //线程互斥
        Mutex g_mutex = new Mutex();

        FileStream FS = null;//new FileStream();
        StreamWriter SW = null;//new StreamWriter();
        #endregion 定义

        #region 清空文本
        //清空
        public void ClearText(string log, string name)
        {
            g_mutex.WaitOne();
            try
            {                
                //创建文件夹
                if (!Directory.Exists(log))
                {
                    Directory.CreateDirectory(log);
                }

                log += "\\" + name + ".txt";
                FileStream fs = new FileStream(@log, FileMode.Create);
                fs.Close();
                g_mutex.ReleaseMutex();
            }
            catch
            {                
                
            }
            g_mutex.ReleaseMutex();
        }

        public void ClearText(string log)
        {
            g_mutex.WaitOne();
            try
            {               
                //创建文件夹
                if (!File.Exists(log))
                {
                    return;
                }

                FileStream fs = new FileStream(@log, FileMode.Create);
                fs.Close();                
            }
            catch 
            {
              
            }
            g_mutex.ReleaseMutex();
        }
        #endregion 清空文本

        #region 写入文本
        //写入txt
        public void WriteText(string log, string name, string value)
        {
            g_mutex.WaitOne();
            try
            {                
                //创建文件夹
                if (!Directory.Exists(log))
                {
                    Directory.CreateDirectory(log);
                }
                log += name + ".txt";

                //创建文件
                if (File.Exists(log) == false)
                {
                    FileStream fs1 = new FileStream(@log, FileMode.Create);
                    fs1.Close();
                }
                FileStream fs = new FileStream(@log, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(value);
                sw.Flush();
                sw.Close();
                fs.Close();
                g_mutex.ReleaseMutex();
            }
            catch 
            {
               
            }
            g_mutex.ReleaseMutex();
        }

        //写入txt
        public void WriteText(string log, string value)
        {
            g_mutex.WaitOne();
            try
            {                
                //创建文件
                if (File.Exists(log) == false)
                {
                    FileStream fs1 = new FileStream(@log, FileMode.Create);
                    fs1.Close();
                }
                FileStream fs = new FileStream(@log, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(value);
                sw.Flush();
                sw.Close();
                fs.Close();
                
            }
            catch
            {
                
            }
            g_mutex.ReleaseMutex();
        }

        /// <summary>
        /// 获取写入的流
        /// </summary>
        /// <param name="log"></param>
        public void GetText(string log)
        {
            g_mutex.WaitOne();
            try
            {
                //创建文件
                if (File.Exists(log) == false)
                {
                    FileStream fs1 = new FileStream(@log, FileMode.Create);
                    fs1.Close();
                }
                FS = new FileStream(@log, FileMode.Append, FileAccess.Write);
                SW = new StreamWriter(FS);
            }
            catch
            {

            }
            g_mutex.ReleaseMutex();
        }
        #endregion 写入文本

        #region 读取文本
        //读取txt
        public string ReadText(string log, string name)
        {
            g_mutex.WaitOne();
            try
            {
                log += "\\" + name + ".txt";
                //创建文件
                if (File.Exists(log) == false)
                {
                    File.CreateText(log);

                    return "";
                }
                FileStream fs = new FileStream(@log, FileMode.Open, FileAccess.ReadWrite);
                StreamReader sw = new StreamReader(fs);
                string str1 = sw.ReadToEnd();

                sw.Close();
                fs.Close();
                return str1;
            }
            catch 
            {
                return "";
            }
            finally
            {
                g_mutex.ReleaseMutex();
            }
        }
        #endregion 读取文本
       
    }
}
