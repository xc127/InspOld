using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService
{
    public abstract class Serializer
    {
        public abstract string Extension { get; }

        public void Serialize<T>(object data, string path)
        {
            string dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            try
            {
                IFormatter _formatter = GetFormatter<T>();//new XmlSerializer(typeof(T)) as IFormatter;
                //创建流
                using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    //序列化写入
                    _formatter.Serialize(fs, data);
                }
            }
            catch { }
        }

        public T DeSerialize<T>(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    IFormatter _formatter = GetFormatter<T>();//new XmlSerializer(typeof(T)) as IFormatter;
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        return (T)_formatter.Deserialize(fs);
                    }
                }
                catch { }
            }
            return default;
        }

        protected abstract IFormatter GetFormatter<T>();
    }
}
