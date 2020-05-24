using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using System.IO;
using BasicClass;

namespace DealFile
{
    public partial class XmlFile
    {
        #region Xml加载
        /// <summary>
        /// 从stream中加载xml
        /// </summary>
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
                return null;
            }           
        }

        /// <summary>
        /// 读取指定路径的xml
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
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
                return null;
            }           
        }

     
        /// <summary>
        /// 读取指定路径的Stream，为系统资源
        /// </summary>
        public virtual XmlDocument LoadXmlStream(string Path)
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

       /// <summary>
       /// 如果指定路径的本地稳定存在则读取，否则读取指定路径的stream
       /// </summary>
        public virtual XmlDocument LoadXmlStream(string Path, string PathStream)
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

        #endregion Xml加载

        #region 读取节点
        /// <summary>
        /// 搜索指定名称的子节点
        /// </summary>
        public XmlElement ReadNode(XmlElement xeRoot, string strNode)
        {
            try
            {
                if (xeRoot == null)
                {
                    return null;
                }
                return xeRoot[strNode];
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }
        /// <summary>
        /// 从文档中读取指定名称子节点
        /// </summary>
        public XmlElement ReadNode(XmlDocument xDoc, string strChild)
        {
            try
            {
                XmlNode xnRoot = xDoc.SelectSingleNode(strChild);
                return (XmlElement)xnRoot;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }
        /// <summary>
        /// 返回根节点中所有的子节点
        /// </summary>
        public XmlNodeList ReadNodes(XmlElement xeRoot)
        {
            try
            {
                if (xeRoot == null)
                {
                    return null;
                }
                return xeRoot.ChildNodes;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }
        #endregion 读取节点

        #region 读取节点内容
        /// <summary>
        /// 读取节点中子节点变量的Int文本值
        /// </summary>
        public int ReadNodeInt(XmlElement xeRoot, string strChild)
        {
            try
            {
                this.StrDouble = ReadNodeStr(xeRoot, strChild);
                int intValue = int.Parse(this.StrDouble);
                return intValue;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int ReadNodeInt(XmlElement xeRoot, string strChild,int intDefault)
        {
            try
            {
                string value = ReadNodeStr(xeRoot, strChild);
                if (value == "")
                {
                    return intDefault;
                }
                int intValue = int.Parse(value);
                return intValue;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 读取节点的Int型文本值
        /// </summary>
        public int ReadNodeInt(XmlElement xeRoot)
        {
            try
            {
                this.StrInt = xeRoot.InnerText;
                int intValue = int.Parse(this.StrDouble);
                return intValue;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 读取节点中子节点的double型文本值
        /// </summary>
        public double ReadNodeDbl(XmlElement xeRoot, string strChild)
        {
            try
            {
                this.StrDouble = ReadNodeStr(xeRoot, strChild);
                double dblValue = Convert.ToDouble(this.StrDouble);
                return dblValue;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public double ReadNodeDbl(XmlElement xeRoot, string strChild, double dblDefault)
        {
            try
            {
                string str = ReadNodeStr(xeRoot, strChild);
                if (str == "")
                {
                    return dblDefault;
                }
                double value = Convert.ToDouble(str);
                return value;
            }
            catch (Exception)
            {
                return dblDefault;
            }
        }
        /// <summary>
        /// 读取节点中double型文本值
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <returns></returns>
        public double ReadNodeDbl(XmlElement xeRoot)
        {
            try
            {
                this.StrDouble = xeRoot.InnerText;
                double dblValue = Convert.ToDouble(this.StrDouble);
                return dblValue;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 读取节点中指定子节点的文本值
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="strChild"></param>
        /// <returns></returns>
        public string ReadNodeStr(XmlElement xeRoot, string strChild)
        {
            try
            {
                return xeRoot[strChild].InnerText; 
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //包含默认值
        public string ReadNodeStr(XmlElement xeRoot, string strChild, string strDefault)
        {
            try
            {
                string str = xeRoot[strChild].InnerText;
                if (str == "")
                {
                    str = strDefault;
                }
                return str;
            }
            catch (Exception ex)
            {
                return strDefault;
            }
        }
        /// <summary>
        /// 读取节点的文本值
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <returns></returns>
        public string ReadNodeStr(XmlElement xeRoot)
        {
            try
            {
                string str = xeRoot.InnerText;
                return str;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #region Enum
        public string ReadNodeEnum(XmlElement xeRoot)
        {
            try
            {
                string str = xeRoot.InnerText;
                if (str == "")
                {
                    str = "Default";
                }
                return str;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string ReadNodeEnum(XmlElement xeRoot, string strChild)
        {
            try
            {
                string str = ReadNodeStr(xeRoot, strChild);
                if (str == "")
                {
                    str = "Default";
                }
                return str;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public string ReadNodeEnum(XmlElement xeRoot, string strChild,string strDefault)
        {
            try
            {
                string str = ReadNodeStr(xeRoot, strChild);
                if (str == "")
                {
                    return strDefault;
                }
                return str;
            }
            catch (Exception ex)
            {
                return strDefault;
            }
        }
        #endregion Enum

        #region Bool
        public bool ReadNodeBl(XmlElement xeRoot, string strChild)
        {
            try
            {
                this.StrBool = ReadNodeStr(xeRoot, strChild);
                return Convert.ToBoolean(this.StrBool);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ReadNodeBl(XmlElement xeRoot, string strChild, bool blDefault)
        {
            try
            {
                string str = ReadNodeStr(xeRoot, strChild);
                if (str == "")
                {
                    return blDefault;
                }
                return Boolean.Parse(str);
            }
            catch (Exception ex)
            {
                return blDefault;
            }
        }

        public bool ReadNodeBl(XmlElement xeRoot, bool blDefault)
        {
            try
            {
                string str = xeRoot.InnerText;
                if (str == "")
                {
                    return blDefault;
                }
                return Boolean.Parse(str);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ReadNodeBl(XmlElement xeRoot)
        {
            try
            {
                string str = xeRoot.InnerText;
                return Boolean.Parse(str);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion Bool
        #endregion  读取节点内容

        #region 读写属性
        /// <summary>
        /// 读取String型属性
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public string ReadAttributeStr(XmlElement xeRoot, string attribute)
        {
            try
            {
                return xeRoot.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string ReadAttributeStr(XmlElement xeRoot, string attribute, string strDefault)
        {
            try
            {
                string str = xeRoot.GetAttribute(attribute);
                if (str=="")
                {
                    str = strDefault;
                }
                return str;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string ReadChildAttributeStr(XmlElement xeRoot, string child, string attribute)
        {
            try
            {
                XmlElement xe = ReadNode(xeRoot, child);
                return xe.GetAttribute(attribute);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        //参数包含默认值
        public string ReadChildAttributeStr(XmlElement xeRoot, string child, string attribute, string strDefault)
        {
            try
            {               
                XmlElement xe = ReadNode(xeRoot, child);
                string str= xe.GetAttribute(attribute);
                if (str == "")
                {
                    str = strDefault;
                }
                return str;                
            }
            catch (Exception ex)
            {
                return strDefault;
            }
        }

        /// <summary>
        /// 读取Int型属性
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public int ReadAttributeInt(XmlElement xeRoot, string attribute)
        {
            try
            {
                return int.Parse(xeRoot.GetAttribute(attribute));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //参数包含默认值
        public int ReadAttributeInt(XmlElement xeRoot, string attribute, int intDefault)
        {
            try
            {
                return int.Parse(xeRoot.GetAttribute(attribute));
            }
            catch (Exception ex)
            {
                return intDefault;
            }
        }
        public int ReadChildAttributeInt(XmlElement xeRoot, string child, string attribute)
        {
            try
            {
                XmlElement xe = ReadNode(xeRoot, child);
                return int.Parse(xe.GetAttribute(attribute));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //参数包含默认值
        public int ReadChildAttributeInt(XmlElement xeRoot, string child, string attribute, int intDefault)
        {
            try
            {
                XmlElement xe = ReadNode(xeRoot, child);
                string str = xe.GetAttribute(attribute);
                if (str == "")
                {
                    return intDefault;
                }
                return int.Parse(str);
            }
            catch (Exception ex)
            {
                return intDefault;
            }
        }

        /// <summary>
        /// 读取double型属性
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public double ReadAttributeDbl(XmlElement xeRoot, string attribute)
        {
            try
            {
                return Convert.ToDouble(xeRoot.GetAttribute(attribute));
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //参数包含默认值
        public double ReadAttributeDbl(XmlElement xeRoot, string attribute, double dblDefault)
        {
            try
            {
                return double.Parse(xeRoot.GetAttribute(attribute));
            }
            catch (Exception ex)
            {
                return dblDefault;
            }
        }

        public double ReadChildAttributeDbl(XmlElement xeRoot, string child, string attribute)
        {
            try
            {
                XmlElement xe = ReadNode(xeRoot, child);
                string str = xe.GetAttribute(attribute);
                return double.Parse(str);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        //参数包含默认值
        public double ReadChildAttributeDbl(XmlElement xeRoot, string child, string attribute, double dblDefault)
        {
            try
            {
                XmlElement xe = ReadNode(xeRoot, child);
                string str = xe.GetAttribute(attribute);
                if (str == "")
                {
                    return dblDefault;
                }
                return double.Parse(str);
            }
            catch (Exception ex)
            {
                return dblDefault;
            }
        }
        /// <summary>
        /// 读取bool型属性
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public bool ReadAttributeBl(XmlElement xeRoot, string attribute)
        {
            try
            {
                return bool.Parse(xeRoot.GetAttribute(attribute));
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //参数包含默认值
        public bool ReadAttributeBl(XmlElement xeRoot, string attribute, bool blDefault)
        {
            try
            {
                return bool.Parse(xeRoot.GetAttribute(attribute));
            }
            catch (Exception ex)
            {
                return blDefault;
            }
        }
        public bool ReadChildAttributeBl(XmlElement xeRoot, string child, string attribute)
        {
            try
            {
                XmlElement xe = ReadNode(xeRoot, child);
                return bool.Parse(xe.GetAttribute(attribute));
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //参数包含默认值
        public bool ReadChildAttributeBl(XmlElement xeRoot, string child, string attribute, bool blDefault)
        {
            try
            {
                XmlElement xe = ReadNode(xeRoot, child);
                if (xe==null)
                {
                    return blDefault;
                }
                string str = xe.GetAttribute(attribute);
                if(str=="")
                {
                    return blDefault;
                }
                return bool.Parse(str);
            }
            catch (Exception ex)
            {
                return blDefault;
            }
        }
        /// <summary>
        /// 读取枚举型属性
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public string ReadAttributeEnum(XmlElement xeRoot, string attribute)
        {
            try
            {
                string type = xeRoot.GetAttribute(attribute);
                if (type == "")
                {
                    type = "Default";
                }
                return type;
            }
            catch (Exception ex)
            {
                return "Default";
            }
        }

        public string ReadAttributeEnum(XmlElement xeRoot, string attribute, string strDefault)
        {
            try
            {
                string type = xeRoot.GetAttribute(attribute);
                if (type == "")
                {
                    type = strDefault;
                }
                return type;
            }
            catch (Exception ex)
            {
                return strDefault;
            }
        }

        public string ReadChildAttributeEnum(XmlElement xeRoot, string child, string attribute)
        {
            try
            {
                XmlElement xe = ReadNode(xeRoot, child);
                string type = xe.GetAttribute(attribute);               
                return type;
            }
            catch (Exception ex)
            {
                return "Default";
            }
        }

        public string ReadChildAttributeEnum(XmlElement xeRoot, string child, string attribute, string strDefault)
        {
            try
            {
                XmlElement xe = ReadNode(xeRoot, child);
                string type = xe.GetAttribute(attribute);
                if (type=="")
                {
                    type = strDefault;
                }
                return type;
            }
            catch (Exception ex)
            {
                return strDefault;
            }
        }
        #endregion 读写属性
    }
}
