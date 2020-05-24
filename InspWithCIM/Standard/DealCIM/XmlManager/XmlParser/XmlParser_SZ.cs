using BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DealCIM
{
    class XmlParser_SZ : XmlParserBase
    {
        const string ClassName = "XmlParser_SZ";        

        public override bool ParseXml(XmlDocument xmlDoc, out PostType type)
        {
            type = PostType.Null;
            try
            {
                XmlElement root = xmlDoc.DocumentElement;

                XmlNode xn = root.SelectSingleNode("error_message");
                error_msg = xn?.InnerText;
                xn = root.SelectSingleNode("result");
                if (xn.InnerText == "valid" || xn.InnerText == "invalid")
                    type = PostType.ChipID;
                if (xn.InnerText == "success" || xn.InnerText == "fail")
                    type = PostType.TrackOut;

                //此处不区分trackout/validate_lot，只要返回值是ok的就返回true
                bool result = xn.InnerText == "valid" || xn.InnerText == "success";

                xn = root.SelectSingleNode("test_pattern_list");
                if (xn != null)
                    PatternList_event?.Invoke(xn.InnerText);

                xn = root.SelectSingleNode("ox");
                if (xn != null)
                    JudgeOX_event?.Invoke(xn.InnerText);
                //如果是x，就排出，如果不是x，就认为该节点结果为ok，如果result同时为ok，则为ok片
                result &= xn?.InnerText == "X" ? false : true;

                return result;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
                return false;
            }
        }
    }
}
