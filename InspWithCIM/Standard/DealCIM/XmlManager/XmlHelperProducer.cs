using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealCIM
{

    public abstract class XmlHelplerFactory
    {
        public abstract XmlCreaterBase GetXmlCreater();
        public abstract XmlParserBase GetXmlParser();
    }

    public class XmlHelper_XM : XmlHelplerFactory
    {
        public override XmlCreaterBase GetXmlCreater()
        {
            return new XmlCreater_XM();
        }

        public override XmlParserBase GetXmlParser()
        {
            return new XmlParser_XM();
        }
    }

    public class XmlHelper_SZ : XmlHelplerFactory
    {
        public override XmlCreaterBase GetXmlCreater()
        {
            return new XmlCreater_SZ();
        }

        public override XmlParserBase GetXmlParser()
        {
            return new XmlParser_SZ();
        }
    }

    public enum Client_Enum
    {
        厦门友达,
        苏州友达
    }
}
