using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;
using BasicComprehensive;
using System.Xml;
using DealFile;
using System.Reflection;
using System.Runtime.Serialization;
using System.IO;

namespace DealRobot
{
    [Serializable]
    public class BaseParRobot:BaseComprehensive
    {
        #region 读取Xml
        //加载文件
        public XmlDocument LoadXml(string Path)
        {
            try
            {
                XmlDocument xDoc = XmlFile.X_I.LoadXml(Path);
                
                return xDoc;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseParCell", ex);
                return null;
            }
        }
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
                XmlDocument xDoc = XmlFile.X_I.LoadXml(Stream);
                return xDoc;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseParImageProcess", ex);
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
                    xDoc = XmlFile.X_I.LoadXml(Path);
                }
                else
                {
                    xDoc = LoadXmlStream(PathStream);
                }
                return xDoc;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseParImageProcess", ex);
                return null;
            }
        }
        #endregion 读取Xml
    }
}
