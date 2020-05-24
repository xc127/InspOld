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
    public class FunLogButton
    {
        #region 静态类实例
        public static FunLogButton P_I = new FunLogButton();
        #endregion 静态类实例

        #region 定义
        #region Path
        string PathButton
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
        //string 
        const string PathTemplate = "Log.Button.XmlTemplate.XmlButton.xml";
        #endregion Path

        //List
        List<ButtonInfo> g_ButtonInfo_L = new List<ButtonInfo>();

        //int 

        //Mutex
        Mutex mutex = new Mutex();
        #endregion 定义

        #region 更新信息
        public void AddInfo(string name, string info)
        {
             mutex.WaitOne();
            try
            {
                ButtonInfo buttonInfo = new ButtonInfo();              
                string time = DateTime.Now.ToLongTimeString();//时间
                
                buttonInfo.NameButton=name;
                buttonInfo.Annotation=info;
                buttonInfo.Time = time;
                g_ButtonInfo_L.Add(buttonInfo);
                if (g_ButtonInfo_L.Count > 100)
                {
                    g_ButtonInfo_L.RemoveRange(0, g_ButtonInfo_L.Count - 100);
                }
                //记录日志
                WriteLog(buttonInfo);
            }
            catch (Exception ex)
            {
                 Log.L_I.WriteError("FunLogButton", ex);
            }
              mutex.ReleaseMutex();
        }        
        #endregion 更新信息

        #region 日志记录
        public void WriteLog(ButtonInfo buttonInfo)
        {
            try
            {
                string strPath = Log.CreateAllTimeFile(PathButton);
                //文件
                string strFileLog = strPath + "Button.xml";

                XmlDocument xDoc = DealXml.D_I.LoadXmlStream(strFileLog, PathTemplate);
                XmlElement xeRoot = DealXml.D_I.ReadNode(xDoc, "Button");
                XmlElement xeButton = xDoc.CreateElement("Button");
                DealXml.D_I.WriteAttribute(xeButton, "NameButton", buttonInfo.NameButton);
                DealXml.D_I.WriteAttribute(xeButton, "Annotation", buttonInfo.Annotation);
                DealXml.D_I.WriteAttribute(xeButton, "Time", buttonInfo.Time);
                DealXml.D_I.WriteAttribute(xeButton, "Authority", Authority.Authority_e.ToString());
                DealXml.D_I.WriteAttribute(xeButton, "Model", ComConfigPar.C_I.NameModel);//记录型号名称
                xeRoot.AppendChild(xeButton);
                xDoc.Save(strFileLog);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("FunLogButton", ex);
            }
        }
        #endregion 日志记录
    }

    //报警信息
    public class ButtonInfo:BaseClass
    {              
        /// <summary>
        /// 按钮名称
        /// </summary>
        string nameButton = "";
        public string NameButton
        {
            get
            {
                return nameButton;
            }
            set
            {
                nameButton = value;
            }
        }       
       
        /// <summary>
        /// 操作权限
        /// </summary>
        string authority = "";
        public string Authority
        {
            get
            {
                return authority;
            }
            set
            {
                authority = value;
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
