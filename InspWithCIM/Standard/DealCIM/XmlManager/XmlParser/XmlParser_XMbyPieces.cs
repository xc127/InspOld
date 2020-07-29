using BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DealCIM
{
    public class XmlParser_XMbyPieces : XmlParserBase
    {
        const string ClassName = "XmlParser_XM";

        public override bool ParseXml(XmlDocument xmlDoc, out PostType type)
        {
            type = PostType.Null;
            try
            {
                XmlElement root = xmlDoc.DocumentElement;

                XmlNode xn = root.SelectSingleNode("rtn_msg");
                error_msg = xn?.InnerText;
                xn = root.SelectSingleNode("trx_name");

                if (xn.InnerText == "UPrintCode")
                    type = PostType.ChipID;
                if (xn.InnerText == "UKitting")
                    type = PostType.TrackOut;

                xn = root.SelectSingleNode("rtn_code");
                //此处不区分trackout/validate_lot，只要返回值是ok的就返回true
                bool result = xn.InnerText == "0000000";
                //if (result)
                //{
                //    xn = root.SelectSingleNode("lot_qty");
                //    //如果有该节点，获取该节点的值作为lotnum显示到主界面，至于为什么要显示，原因不明
                //    if (xn != null && int.TryParse(xn.InnerText, out int lotnum))
                //    {
                //        type = PostType.Lot;
                //        UpdataLotNum_event?.Invoke(lotnum);
                //    }
                //}

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
