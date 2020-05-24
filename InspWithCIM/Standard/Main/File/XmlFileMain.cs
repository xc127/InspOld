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

namespace Main
{
    public class XmlFileMain
    {
        #region 静态类实例
        public static XmlFileMain X_I = new XmlFileMain();
        #endregion 静态类实例

        #region 读写xml
        public XmlDocument LoadXmlStream(string Path, string PathStream)
        {
            try
            {
                XmlDocument xDoc = null;
                if (File.Exists(Path))
                {
                    xDoc = LoadXml(Path);
                }
                else
                {
                    xDoc = LoadXmlStream(PathStream);
                }
                return xDoc;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("XmlFileMain", ex);
                return null;
            }
        }

        //加载文件
        public XmlDocument LoadXml(string Path)
        {
            try
            {
                if (!File.Exists(Path))
                {
                    return null;
                }
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(Path);
                return xDoc;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("XmlFileMain", ex);
                return null;
            }
        }


        //加载Xml文档
        public XmlDocument LoadXmlStream(string Path)
        {
            try
            {
                Assembly assembly = Assembly.GetCallingAssembly();
                string name = assembly.GetName().Name + ".";
                Stream Stream = assembly.GetManifestResourceStream(name + Path);
                //创建文件,从模板中读取
                XmlDocument xDoc = LoadXml(Stream);
                return xDoc;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("XmlFileMain", ex);
                return null;
            }
        }
        public XmlDocument LoadXml(Stream stream)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(stream);
                return xDoc;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("XmlFileMain", ex);
                return null;
            }
        }
        #endregion 读写xml
    }
}
