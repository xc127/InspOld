using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CommonService
{
    class XmlFormatterAdaptor : IFormatter
    {
        public XmlFormatterAdaptor(Type t)
        {
            _formatter = new XmlSerializer(t);
        }

        readonly XmlSerializer _formatter;

        public object Deserialize(Stream stream)
        {
            return _formatter.Deserialize(stream);
        }

        public void Serialize(Stream stream, object data)
        {
            _formatter.Serialize(stream, data);
        }
    }
}
