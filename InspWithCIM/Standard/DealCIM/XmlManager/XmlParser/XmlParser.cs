using BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DealCIM
{
    interface IXmlParser
    {
        bool ParseXml(XmlDocument xmlDoc, out PostType type);
    }

    public class XmlParserBase : IXmlParser
    {
        #region define
        const string ClassName = "XmlParserBase";
        public string error_msg;

        public static IntAction UpdataLotNum_event;
        public static StrAction JudgeOX_event;
        public static StrAction PatternList_event;
        #endregion

        #region 接口
        /// <summary>
        /// 解析字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ParseStr(string data, string id, out PostType type)
        {
            type = PostType.Null;
            try
            {
                XmlDocument xmlDoc = XMLHelpler.StringToXML(data);
                bool result = ParseXml(xmlDoc, out type);
                SaveXml(xmlDoc, "Recv-" + id, type);
                return result;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
                return false;
            }
        }

        /// <summary>
        /// 解析xml
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public virtual bool ParseXml(XmlDocument xmlDoc, out PostType type)
        {
            type = PostType.Null;
            return true;
        }

        public bool SaveXml(XmlDocument xmldoc, string id, string pathDir)
        {
            try
            {
                string path = Log.CreateAllTimeFile(pathDir) + DateTime.Now.ToLongTimeString().Replace(":", "-") + "-" + id + ".xml";
                xmldoc.Save(path);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
                return false;
            }
            return true;
        }

        public bool SaveXml(XmlDocument xmldoc, string id, PostType type)
        {
            try
            {
                string path = Log.CreateAllTimeFile(CIM.GetXmlSavePath(type)) + DateTime.Now.ToLongTimeString().Replace(":", "-") + "-" + id + ".xml";
                xmldoc.Save(path);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
                return false;
            }
            return true;
        }

        public string GetErrorMsg()
        {
            return error_msg;
        }
        #endregion
    }
}
