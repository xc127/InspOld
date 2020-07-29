using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonService
{
    /// <summary>
    /// 二进制序列化类
    /// </summary>
    public class Serializer_Binary : Serializer
    {
        /// <summary>
        /// 生成的序列化文件后缀
        /// </summary>
        /// <returns></returns>
        public override string Extension => @".btn";

        protected override IFormatter GetFormatter<T>()
        {
            return new BinaryFormatterAdaptor();
        }


        ///// <summary>
        ///// 反序列化模板函数
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="path">反序列化的文件路径</param>
        ///// <returns></returns>
        //public T DeSerialize<T>(string path)
        //{
        //    if (File.Exists(path))
        //    {
        //        try
        //        {
        //            BinaryFormatter bf = new BinaryFormatter();
        //            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
        //            {
        //                return (T)bf.Deserialize(fs);
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
        //    if (!Directory.Exists(path))
        //        Directory.CreateDirectory(path);

        //    try
        //    {
        //        BinaryFormatter bf = new BinaryFormatter();

        //        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
        //        {
        //            bf.Serialize(fs, data);
        //        }
        //        return;
        //    }
        //    catch (Exception ex)
        //    { }
        //}
    }
}
