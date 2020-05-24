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
    public class FunLogState : BaseClass
    {
        #region 静态类实例
        public static FunLogState F_I = new FunLogState();
        #endregion 静态类实例

        #region 定义
        #region Path
        string PathState
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
        public string PathStateHour
        {
            get
            {
                return Log.CreateAllTimeFile(PathState);
            }
        }

        const string PathTemplate = "Log.State.XmlTemplate.XmlState.xml";

        //int 
        int g_Num = 0;

        public List<StateInfo> g_StateInfo_L = new List<StateInfo>();
        #endregion Path
        #endregion 定义

        #region 初始化
        public FunLogState()
        {
            NameClass = "FunLogState";
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
                StateInfo stateInfo = new StateInfo();
                stateInfo.No = g_Num;
                stateInfo.State = info;
                stateInfo.Time = time;
                g_StateInfo_L.Add(stateInfo);
                if (g_StateInfo_L.Count > 50)
                {
                    g_StateInfo_L.RemoveRange(0, g_StateInfo_L.Count - 50);
                }
                //记录日志
                //new Task(new Action(() =>
                //{
                //    WriteTxt(stateInfo);

                //})).Start();
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
                g_StateInfo_L.Clear();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }


        /// <summary>
        /// 写入本地日志
        /// </summary>
        /// <param name="alarmInfo"></param>
        public void WriteLog(StateInfo alarmInfo)
        {
            try
            {
                //文件
                string strFileLog = PathStateHour + "State.xml";

                XmlDocument xDoc = DealXml.D_I.LoadXmlStream(strFileLog, PathTemplate);
                XmlElement xeRoot = DealXml.D_I.ReadNode(xDoc, "State");
                XmlElement xeState = xDoc.CreateElement("State");
                DealXml.D_I.WriteAttribute(xeState, "Value", alarmInfo.State);
                DealXml.D_I.WriteAttribute(xeState, "Time", alarmInfo.Time);
                xeRoot.AppendChild(xeState);
                xDoc.Save(strFileLog);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //写入文本
        public void WriteTxt(StateInfo stateInfo)
        {
            try
            {
                TxtFile t_I = new TxtFile();
                //文件
                string strFileLog = PathStateHour + "State.txt";

                string value = "State:" + stateInfo.State
                    + "       " + "Time:" + stateInfo.Time;
                t_I.WriteText(strFileLog, value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
    }

    //状态信息
    public class StateInfo : BaseClass
    {
        string state = "";
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }

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
