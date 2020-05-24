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
    class XmlCreater_SZ : XmlCreaterBase
    {
        string ClassName = "XmlCreater_SZ";
        public override XmlDocument CreateChipIDXml(string chipid, string modelno, string lot)
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
                    XmlNode xn2 = xmlDoc.DocumentElement.SelectSingleNode("lot_no");
                    xn2.InnerText = chipid;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
            return xmlDoc;
        }
    }
}
