using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CommonService
{
    class BinaryFormatterAdaptor : IFormatter
    {
        readonly BinaryFormatter _formatter = new BinaryFormatter();

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
