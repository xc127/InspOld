using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Threading;
using BasicClass;
using System.Diagnostics;
using DealFile;
using BasicComprehensive;
using DealAlgorithm;
using System.Xml;
using System.Reflection;
using DealCommunication;


namespace DealPLC
{
    public partial class BaseParPLC : BaseParCommunication
    {
        #region 定义
        //Stopwatch
        public Stopwatch stopWatch = new Stopwatch();
        #endregion 定义

        #region 初始化
        public BaseParPLC()
        {
            NameClass = "BaseParPLC";
        }
        #endregion 初始化


        #region 读取Xml
        /// <summary>
        /// 加载资源中Xml文档
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public override XmlDocument LoadXmlStream(string Path)
        {
            try
            {
                Assembly assembly = Assembly.GetCallingAssembly();
                string name = assembly.GetName().Name + ".";
                Stream Stream = assembly.GetManifestResourceStream(name + Path);
                XmlDocument xDoc = g_XmlFile.LoadXml(Stream);
                return xDoc;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }
        /// <summary>
        /// 加载本地或者资源中的xml
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="PathStream"></param>
        /// <returns></returns>
        public override XmlDocument LoadXmlStream(string Path, string PathStream)
        {
            try
            {
                XmlDocument xDoc = null;
                if (File.Exists(Path))
                {
                    xDoc = g_XmlFile.LoadXml(Path);
                }
                else
                {
                    xDoc = LoadXmlStream(PathStream);
                }
                return xDoc;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }
        #endregion 读取Xml
    }
}
