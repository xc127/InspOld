using BasicClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;

namespace DealCIM
{
    interface IXmlCreater
    {
        XmlDocument CreateChipIDXml(string chipid, string modelno, string lot);
        XmlDocument CreateLotXml(string modelno, string lot);
        XmlDocument CreateTrackoutXml(List<string> chipid_list, string modelno, string lot);
    }

    public class XmlCreaterBase : IXmlCreater
    {
        const string ClassName = "XmlBase";
        public const string strRoot = @"si300_interface";
        #region path xml
        /// <summary>
        /// xml文档根目录，保存的是标准xml文件
        /// </summary>
        private const string xmlFolder = "Store\\Custom\\XML\\";
        /// <summary>
        /// trackout.xml目录
        /// </summary>
        public static string Path_XML_TrackOut { get => ParPathRoot.PathRoot + xmlFolder + "track_out.xml"; }
        /// <summary>
        /// chipid.xml目录
        /// </summary>
        public static string Path_XML_ChipID { get => ParPathRoot.PathRoot + xmlFolder + "chipid.xml"; }
        /// <summary>
        /// lot.xml目录
        /// </summary>
        public static string Path_XML_Lot { get => ParPathRoot.PathRoot + xmlFolder + "lot.xml"; }
        #endregion

        #region 接口
        /// <summary>
        /// 读取本地标准xml文件然后适当修改节点内容生成xml
        /// </summary>
        /// <param name="chipid"></param>
        /// <param name="modelno"></param>
        /// <returns></returns>
        public virtual XmlDocument CreateChipIDXml(string chipid, string modelno, string lot)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                if (!File.Exists(Path_XML_ChipID))
                {
                    MessageBox.Show(Path_XML_ChipID + @"文件不存在，请确认！");
                }
                else
                {
                    xmlDoc.Load(Path_XML_ChipID);

                    AssignXML(ref xmlDoc, modelno, lot);
                    XmlNode xn = xmlDoc.DocumentElement.SelectSingleNode("chip_id");
                    xn.InnerText = chipid;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
            return xmlDoc;
        }
        /// <summary>
        /// 读取本地标准xml文件后修改适当节点内容生成xml
        /// </summary>
        /// <param name="modelno"></param>
        /// <returns></returns>
        public virtual XmlDocument CreateLotXml(string modelno, string lot)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                if (!File.Exists(Path_XML_Lot))
                {
                    MessageBox.Show(Path_XML_Lot + @"文件不存在，请确认！");
                    return null;
                }
                else
                {
                    xmlDoc.Load(Path_XML_Lot);

                    AssignXML(ref xmlDoc, modelno, lot);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
            return xmlDoc;
        }
        /// <summary>
        /// 读取本地标准xml文件后修改适当节点内容生成xml
        /// </summary>
        /// <param name="chipid_list"></param>
        /// <param name="modelno"></param>
        /// <returns></returns>
        public virtual XmlDocument CreateTrackoutXml(List<string> chipid_list, string modelno, string lot)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                if (!File.Exists(Path_XML_TrackOut))
                {
                    MessageBox.Show(Path_XML_TrackOut + @"文件不存在，请确认！");
                    return null;
                }
                else
                {
                    xmlDoc.Load(Path_XML_TrackOut);
                    AssignXML(ref xmlDoc, modelno, lot);

                    XmlElement xe = (XmlElement)xmlDoc.SelectSingleNode(strRoot).SelectSingleNode("chip_info");
                    foreach (string item in chipid_list)
                    {
                        XmlElement xe1 = xmlDoc.CreateElement("chip_id");
                        xe1.InnerText = item;
                        xe.AppendChild(xe1);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
            return xmlDoc;
        }
        /// <summary>
        /// 配置xml中的数据，为了普适，其实准备好对应的xml，只要写一个modelno即可，其余对于一台机而言可以写死，但耗费较少，不做特殊处理，并且写法冗长，有志之士可以用dic<节点名,数据>来进行管理，一个foreach+dic.trygetvalue就可以少几十行代码
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="modelno"></param>
        public virtual void AssignXML(ref XmlDocument xmlDoc, string modelno, string lot)
        {
            try
            {
                XmlNodeList nodeList = xmlDoc.SelectSingleNode(strRoot).ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement xe = (XmlElement)xn; //将子节点类型转换为XmlElement类型 

                    if (xe.Name == "user_id")
                    {
                        xe.InnerText = PostParams.P_I.StrUserID;
                    }
                    if (xe.Name == "p_area")
                    {
                        xe.InnerText = PostParams.P_I.StrFab;
                    }
                    if (xe.Name == "area")
                    {
                        xe.InnerText = PostParams.P_I.StrArea;
                    }
                    if (xe.Name == "line")
                    {
                        xe.InnerText = PostParams.P_I.StrLine;
                    }
                    if (xe.Name == "operation")
                    {
                        xe.InnerText = PostParams.P_I.StrOperation;
                    }
                    if (xe.Name == "lot_no")
                    {
                        xe.InnerText = lot;
                    }
                    if (xe.Name == "model_no")
                    {
                        xe.InnerText = modelno;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }
        #endregion


    }

}
