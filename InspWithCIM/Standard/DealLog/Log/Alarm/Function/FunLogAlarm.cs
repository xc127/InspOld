using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Threading.Tasks;
using BasicClass;
using Common;
using DealFile;
using System.IO;
using System.Xml;
using System.Reflection;

namespace DealLog
{
    public class FunLogAlarm:BaseClass
    {
        #region 静态类实例
        public static FunLogAlarm F_I = new FunLogAlarm();
        #endregion 静态类实例

        #region 定义
        #region Path
        string PathAlarm
        {
            get
            {
                string path = ParPathRoot.PathRoot + "软件运行记录\\运行及报警日志\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        public string PathAlarmHour
        {
            get
            {
                return Log.CreateAllTimeFile(PathAlarm);
            }
        }
        const string PathTemplate = "Log.Alarm.XmlTemplate.XmlAlarm.xml";

        //int 
        int g_Num = 0;

        public List<AlarmInfo> g_AlarmInfo_L = new List<AlarmInfo>();
        #endregion Path
        #endregion 定义

        #region 初始化
        public FunLogAlarm()
        {
            NameClass = "FunLogAlarm";
        }
        #endregion 初始化

        /// <summary>
        /// 添加报警信息
        /// </summary>
        /// <param name="info"></param>
        public void AddInfo(string info)
        {
            try
            {
                g_Num++;
                string time = DateTime.Now.ToLongTimeString();//时间
                AlarmInfo alarmInfo = new AlarmInfo();
                alarmInfo.No = g_Num;
                alarmInfo.Alarm = info;
                alarmInfo.Time = time;
                g_AlarmInfo_L.Add(alarmInfo);
                if (g_AlarmInfo_L.Count > 50)
                {
                    g_AlarmInfo_L.RemoveRange(0, g_AlarmInfo_L.Count - 50);
                }
                //记录日志
                new Task(new Action(() =>
                    {                     
                        WriteTxt(alarmInfo);
                    })).Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 清除报警信息
        /// </summary>
        public void ClearInfo()
        {
            try
            {
                g_Num = 0;
                g_AlarmInfo_L.Clear();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }


        /// <summary>
        /// 写入本地日志
        /// </summary>
        public void WriteLog(AlarmInfo alarmInfo)
        {
            try
            {               
                //文件
                string strFileLog = PathAlarmHour + "Alarm.xml";

                XmlDocument xDoc = DealXml.D_I.LoadXmlStream(strFileLog, PathTemplate);
                XmlElement xeRoot = DealXml.D_I.ReadNode(xDoc, "Alarm");
                XmlElement xeAlarm = xDoc.CreateElement("Alarm");
                DealXml.D_I.WriteAttribute(xeAlarm, "Value", alarmInfo.Alarm);
                DealXml.D_I.WriteAttribute(xeAlarm, "Time", alarmInfo.Time);
                xeRoot.AppendChild(xeAlarm);
                xDoc.Save(strFileLog);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //写入文本
        public void WriteTxt(AlarmInfo alarmInfo)
        {
            try
            {
                TxtFile t_I = new TxtFile();
                //文件
                string strFileLog = PathAlarmHour + "Alarm.txt";

                string value = "Alarm:" + alarmInfo.Alarm
                    + "       " + "Time:" + alarmInfo.Time;
                t_I.WriteText(strFileLog, value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
    }

    //报警信息
    public class AlarmInfo
    {
        int no = 0;
        public int No
        {
            get
            {
                return no;
            }
            set
            {
                no = value;
            }
        }
        string alarm = "";
        public string Alarm
        {
            get
            {
                return alarm;
            }
            set
            {
                alarm = value;
            }
        }
        /// <summary>
        /// 时间
        /// </summary>
        string time = "";
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }
    }
}
