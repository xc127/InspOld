using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;
using BasicClass;
using DealFile;

namespace DealLog
{
    public class DealXml : XmlFile
    {
        #region 静态类实例
        public static DealXml D_I = new DealXml();
        #endregion 静态类实例

        //加载Xml文档
        public override XmlDocument LoadXmlStream(string Path)
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
                return null;
            }
        }

        public override XmlDocument LoadXmlStream(string Path, string PathStream)
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
                return null;
            }
        }
    }
}
