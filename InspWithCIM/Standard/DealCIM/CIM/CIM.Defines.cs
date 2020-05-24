using BasicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCIM
{
    /// <summary>
    /// 列出使用到的常量，主要是一些路径
    /// </summary>
    public partial class CIM
    {
        #region const
        public const string ReturnOK = "0000";
        #endregion

        #region config
        //配置文件section
        public const string commonSection = "CONFIG";
        //配置文件路径
        public static string Path_Config = ParPathRoot.PathRoot + "Store\\Custom\\" + "Cim.ini";
        #endregion

        #region log path
        /// <summary>
        /// 日志根目录
        /// </summary>
        public const string logFolder = "软件运行记录\\Custom\\CIM\\";
        /// <summary>
        /// trackout日志目录
        /// </summary>
        public static string Path_Log_TrackOut { get => ParPathRoot.PathRoot + logFolder + "TrackOut\\"; }
        /// <summary>
        /// chipid日志目录
        /// </summary>
        public static string Path_Log_ChipID { get => ParPathRoot.PathRoot + logFolder + "ChipID\\"; }
        /// <summary>
        /// lotno日志目录
        /// </summary>
        public static string Path_Log_Lot { get => ParPathRoot.PathRoot + logFolder + "Lot\\"; }

        public static string Path_Log_Response { get => ParPathRoot.PathRoot + logFolder + "Response\\"; }

        public static string GetXmlSavePath(PostType type)
        {
            string path = string.Empty;
            switch (type)
            {
                case PostType.ChipID:
                    path = Path_Log_ChipID;
                    break;
                case PostType.Lot:
                    path = Path_Log_Lot;
                    break;
                case PostType.TrackOut:
                    path = Path_Log_TrackOut;
                    break;
                default:
                    break;
            }
            return path;
        }
        #endregion
    }
}
