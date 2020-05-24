using BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCIM
{
    public partial class CIM
    {
        #region returnValue
        //保护dic的读写
        static object dic_lock = new object();
        /// <summary>
        /// 保存cim反馈结果的dic，<correlationid,result>
        /// </summary>
        static Dictionary<string, bool> dic_returnValue = new Dictionary<string, bool>();

        /// <summary>
        /// 查询Dic中是否有对应的结果，如果有则返回结果并清除记录
        /// </summary>
        /// <param name="key">correlationid</param>
        /// <param name="value">如果查询到id，返回该id对应的结果</param>
        /// <returns>查询结果-是否存在所查询的内容</returns>
        public static bool CheckDic(string key, out bool value)
        {
            value = false;
            lock (dic_lock)
            {
                if (dic_returnValue.ContainsKey(key))
                {
                    value = dic_returnValue[key];
                    dic_returnValue.Remove(key);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 在dic中添加新获取的cim反馈结果
        /// </summary>
        /// <param name="key">correlationid，大而不重复的整数</param>
        /// <param name="value">cim返回结果，是ok还是ng</param>
        /// <returns></returns>
        public static bool AddDic(string key, bool value)
        {
            lock (dic_lock)
            {
                try
                {
                    dic_returnValue.Add(key, value);
                    return true;
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(ClassName, ex);
                    return false;
                }
            }
        }
        #endregion
    }
}
