using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService
{
    public class Serializer_Xml : Serializer
    {
        /// <summary>
        /// 获取序列化生成的文件的后缀
        /// </summary>
        /// <returns></returns>
        public override string Extension => @".xml";

        protected override IFormatter GetFormatter<T>()
        {
            return new XmlFormatterAdaptor(typeof(T));
        }

        ///// <summary>
        ///// 反序列化模板函数
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="path"></param>
        ///// <returns></returns>
        //public T DeSerialize<T>(string path)
        //{
        //    if (File.Exists(path))
        //    {
        //        try
        //        {
        //            XmlSerializer reader = new XmlSerializer(typeof(T));
        //            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        //            {
        //                return (T)reader.Deserialize(fs);
        //            }
        //        }
        //        catch (Exception ex)
        //        { }
        //    }
        //    return default;
        //}

        ///// <summary>
        ///// 序列化模板函数
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="data"></param>
        ///// <param name="path"></param>
        //public void Serialize<T>(object data, string path)
        //{
        //    string dir = Path.GetDirectoryName(path);
        //    if (!Directory.Exists(dir))
        //        Directory.CreateDirectory(dir);

        //    try
        //    {
        //        XmlSerializer writer = new XmlSerializer(typeof(T));
        //        //创建流
        //        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        //        {
        //            //序列化写入
        //            writer.Serialize(fs, data);
        //        }
        //    }
        //    catch (Exception ex)
        //    { }
        //}
    }
}
