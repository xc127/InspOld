using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BasicClass;

namespace DealCIM
{
    public partial class PostHelper
    {
        // public static PostHelper instance = new PostHelper();

        public static string PostChipID(string chipid, string modelno, out string key)
        {
            XmlDocument xmlDoc = XMLHelpler.CreateChipIDXml(chipid, modelno, PostParams.P_I.StrLot);
            string returnValue = CIM.C_I.WriteData(XMLHelpler.XMLToString(xmlDoc), out key);
            XMLHelpler.SaveXml(xmlDoc, "Post-" + chipid + "-" + key, PostType.ChipID);

            return returnValue;
        }

        public static string PostChipID(string chipid, string modelno, XmlCreaterBase xmlCreater, out string key)
        {
            XmlDocument xmlDoc = xmlCreater.CreateChipIDXml(chipid, modelno, PostParams.P_I.StrLot);
            string returnValue = CIM.C_I.WriteData(XMLHelpler.XMLToString(xmlDoc), out key);
            if(PostParams.P_I.BlLog)
                XMLHelpler.SaveXml(xmlDoc, "Post-" + chipid + "-" + key, PostType.ChipID);

            return returnValue;
        }

        public static string PostLot(string modelno, out string key)
        {
            XmlDocument xmlDoc = XMLHelpler.CreateLotXml(modelno,PostParams.P_I.StrTempLot);
            string returnValue = CIM.C_I.WriteData(XMLHelpler.XMLToString(xmlDoc), out key);
            XMLHelpler.SaveXml(xmlDoc, "Post-" + key, PostType.Lot);

            return returnValue;
        }

        public static string PostLot(string modelno, XmlCreaterBase xmlCreater, out string key)
        {
            XmlDocument xmlDoc = xmlCreater.CreateLotXml(modelno, PostParams.P_I.StrTempLot);
            string returnValue = CIM.C_I.WriteData(XMLHelpler.XMLToString(xmlDoc), out key);
            if (PostParams.P_I.BlLog)
                XMLHelpler.SaveXml(xmlDoc, "Post-" + key, PostType.Lot);

            return returnValue;
        }

        public static string PostTrackOut(List<string> list,string modelno, out string key)
        {
            XmlDocument xmlDoc = XMLHelpler.CreateTrackoutXml(list, modelno, PostParams.P_I.StrLot);
            string returnValue = CIM.C_I.WriteData(XMLHelpler.XMLToString(xmlDoc), out key);
            XMLHelpler.SaveXml(xmlDoc, "Post-" + key, PostType.TrackOut);

            return returnValue;
        }

        public static string PostTrackOut(List<string> list, string modelno, XmlCreaterBase xmlCreater, out string key)
        {
            XmlDocument xmlDoc = xmlCreater.CreateTrackoutXml(list, modelno, PostParams.P_I.StrLot);
            string returnValue = CIM.C_I.WriteData(XMLHelpler.XMLToString(xmlDoc), out key);
            if (PostParams.P_I.BlLog)
                XMLHelpler.SaveXml(xmlDoc, "Post-" + key, PostType.TrackOut);

            return returnValue;
        }
    }

    public struct PostInfo
    {
        public string correlationID;
        public PostType type;
    }
}
