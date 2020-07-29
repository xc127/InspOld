using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService
{
    public interface IFormatter
    {
        object Deserialize(Stream stream);
        void Serialize(Stream stream, object data);
    }
}
