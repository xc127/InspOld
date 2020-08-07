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
    public class XmlCreater_XMbyPiece : XmlCreaterBase
    {
        string ClassName = "XmlCreater_XMbyPiece";
        public override string strRoot => "trx";

        public override string Path_XML_ChipID => ParPathRoot.PathRoot + xmlFolder + "uprintcode.xml";
        public override string Path_XML_TrackOut => ParPathRoot.PathRoot + xmlFolder + "ukitting.xml";

        public XmlCreater_XMbyPiece() { }

        public override XmlDocument CreateTrackoutXml(List<string> chipid_list,
            string modelno, string lot)
        {
            XmlDocument xmlDoc = new XmlDocument();
            //try
            //{
            //    if (!File.Exists(Path_XML_TrackOut))
            //    {
            //        MessageBox.Show(Path_XML_TrackOut + @"文件不存在，请确认！");
            //        return null;
            //    }
            //    else
            //    {
            //        xmlDoc.Load(Path_XML_TrackOut);
            //        AssignXML(ref xmlDoc, modelno, lot);

            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.L_I.WriteError(ClassName, ex);
            //}
            return xmlDoc;
        }

        public override XmlDocument CreateTrackoutXml(string chipid, string modelno)
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
                    AssignXML(ref xmlDoc, modelno, "");
                    XmlNode xn = xmlDoc.DocumentElement.SelectSingleNode("chip_id");
                    xn.InnerText = chipid;

                    xn = xmlDoc.DocumentElement.SelectSingleNode("runcard_sn");
                    xn.InnerText = chipid;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
            return xmlDoc;
        }

        public override void AssignXML(ref XmlDocument xmlDoc, string modelno, string lot)
        {
            try
            {
                XmlNodeList nodeList = xmlDoc.SelectSingleNode(strRoot).ChildNodes;
                foreach (XmlNode xn in nodeList)
                {
                    XmlElement xe = (XmlElement)xn; //将子节点类型转换为XmlElement类型 

                    if (xe.Name == "log_id")
                    {
                        xe.InnerText = "0" + DateTime.Today.ToString("yyyyMMdd") + "12345678";
                    }
                    if (xe.Name == "lm_user")
                    {
                        xe.InnerText = PostParams.P_I.StrUserID;
                    }
                    if (xe.Name == "parea")
                    {
                        xe.InnerText = PostParams.P_I.StrFab;
                    }
                    //if (xe.Name == "area")
                    //{
                    //    xe.InnerText = PostParams.P_I.StrArea;
                    //}
                    if (xe.Name == "line_id")
                    {
                        xe.InnerText = PostParams.P_I.StrLine;
                    }
                    if (xe.Name == "eqp_id")
                    {
                        xe.InnerText = PostParams.P_I.EqpId;
                    }
                    if (xe.Name == "op_id")
                    {
                        xe.InnerText = PostParams.P_I.StrOperation;
                    }
                    if (xe.Name == "wo_id")
                    {
                        xe.InnerText = PostParams.P_I.StrLot;
                    }
                    //if (xe.Name == "model_no")
                    //{
                    //    xe.InnerText = modelno;
                    //}
                    if (xe.Name == "lm_time")
                    {
                        xe.InnerText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }
    }
}
