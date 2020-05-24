using BasicClass;
using DealFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCIM
{
    public partial class CIM
    {
        #region chipid list
        /// <summary>
        /// chipidlist的保存路径
        /// </summary>
        public static string Path_ChipIDList { get => ParPathRoot.PathRoot + logFolder + "ChipID_List.txt"; }
        /// <summary>
        /// 用于保存插栏成功的chipid
        /// </summary>
        static List<string> chipid_list = new List<string>();

        /// <summary>
        /// 获取list的内容
        /// </summary>
        /// <returns></returns>
        public static List<string> GetList()
        {
            return chipid_list;
        }
        /// <summary>
        /// 获取list当前的item数量
        /// </summary>
        /// <returns></returns>
        public static int GetChipIDCnt()
        {
            return chipid_list.Count;
        }
        /// <summary>
        /// 检查list中是否已存在重复的chipid
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool CheckDup(string item)
        {
            return chipid_list.Contains(item);
        }
        /// <summary>
        /// 清空
        /// </summary>
        public static void ClearChipID()
        {
            chipid_list.Clear();
            ChipIDCount = 0;
        }
        /// <summary>
        /// 软件开启时，从本地读取之前的记录
        /// </summary>
        /// <param name="cnt">可以设定读取前多少个，感觉没什么用，反正加了以防万一，不加个参数感觉太死板</param>
        public static void LoadList(int cnt)
        {
            try
            {
                ClearChipID();
                FileStream fileStream = new FileStream(Path_ChipIDList, FileMode.Open, FileAccess.Read);
                TextReader textReader = new StreamReader(fileStream, Encoding.Default);
                for (int i = 0; i < cnt; ++i)
                {
                    //一次一行，一行一个chipid
                    chipid_list.Add(textReader.ReadLine());
                    ChipIDCount++;
                }
                textReader.Close();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }
        /// <summary>
        /// 添加新数据，并记录到本地，按理说要trycatch，但没必要
        /// </summary>
        /// <param name="value"></param>
        public static bool AppendChipIDList(string value)
        {
            try
            {
                if (CheckDup(value))
                    return false;
                chipid_list.Add(value);
                TxtFile.DealTxt_Inst.WriteText(Path_ChipIDList, value);
                ChipIDCount++;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }

            return true;
        }
        #endregion
    }
}
