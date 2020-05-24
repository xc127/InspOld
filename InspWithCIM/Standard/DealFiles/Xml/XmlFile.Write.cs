using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace DealFile
{
    public partial class XmlFile
    {
        #region 创建XML
        //创建一个新的文件
        public XmlDocument CreateXml()
        {            
            try
            {
                XmlDocument xDoc = new XmlDocument();               
                XmlDeclaration xDec = xDoc.CreateXmlDeclaration("1.0", "GB2312", null);
                xDoc.AppendChild(xDec);                   
                return xDoc;
            }
            catch (Exception ex)
            {
                return null;
            }            
        }
        #endregion 创建XML

        #region 增加节点 
        //生成新节点
        public  XmlElement CreateNewXe(XmlElement xeRoot, string child)
        {
            try
            {
                XmlDocument xDoc = xeRoot.OwnerDocument;
                XmlElement xe = xDoc.CreateElement(child);
                return xe;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //在根节点里面插入子节点和内容
        public bool InnerNewText(XmlElement xeRoot, string strChild, string text)
        {
            try
            {
                XmlDocument xDoc = xeRoot.OwnerDocument;
                XmlElement xe = xDoc.CreateElement(strChild);
                xe.InnerText = text;
                xeRoot.AppendChild(xe);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //在节点里面插入Text
        public bool InnerText(XmlElement xeRoot, string text)
        {
            try
            {
                xeRoot.InnerText = text;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //在文档里插入节点内容
        public bool InnerText(XmlDocument xDoc, string strChild, string text)
        {
            try
            {
                XmlNode xnRoot = xDoc.SelectSingleNode(strChild);
                xnRoot.InnerText = text;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }       
        #endregion 增加节点

        #region 清除节点
        //清除所有节点
        public bool ClearNode(XmlElement xeRoot)
        {
            try
            {
                //移除当前所有的节点
                xeRoot.RemoveAll();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion 清除节点

        #region 设置属性
        /// <summary>
        /// 写入属性
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="attribute"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool WriteAttribute(XmlElement xeRoot, string attribute, string text)
        {
            try
            {
                xeRoot.SetAttribute(attribute, text);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 写入根节点下面子节点的属性
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="attribute"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool WriteChildAttribute(XmlElement xeRoot, string child, string attribute, string value)
        {
            try
            {
                XmlElement xeChild = xeRoot[child];
                xeChild.SetAttribute(attribute, value);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="child"></param>
        /// <param name="attribute"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool WriteNewChildAttribute(XmlElement xeRoot, string child, string attribute, string value)
        {
            try
            {
                XmlDocument xDoc = xeRoot.OwnerDocument;
                XmlElement xe = xDoc.CreateElement(child);
                xe.SetAttribute(attribute, value);
                xeRoot.AppendChild(xe);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion 设置属性
    }
}
