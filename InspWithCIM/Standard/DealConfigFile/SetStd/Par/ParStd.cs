using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DealFile;
using System.IO;
using BasicClass;

namespace DealConfigFile
{
    public class ParStd
    {
        #region 定义
        public const int C_Num = 18;

        //Path       
        public static string PathStd
        {
            get
            {
                return ParPathRoot.PathRoot + "Store\\AdjustStd\\Std.ini";
            }
        }
        #endregion 定义

        #region 读取参数
        public static double Value1(string strSection)
        {
            return AdjustIniBase.A_I.Value1(strSection, PathStd);
        }

        public static double Value2(string strSection)
        {
            return AdjustIniBase.A_I.Value2(strSection, PathStd);
        }

        public static double Value3(string strSection)
        {
            return AdjustIniBase.A_I.Value3(strSection, PathStd);
        }

        public static double Value4(string strSection)
        {
            return AdjustIniBase.A_I.Value4(strSection, PathStd);
        }
        #endregion 读取参数

        #region 写入参数
        public static void SetValue1(string section, double value)
        {
            IniFile.I_I.WriteIni(section, "Value1", value.ToString(), PathStd);
        }

        public static void SetValue2(string section, double value)
        {
            IniFile.I_I.WriteIni(section, "Value2", value.ToString(), PathStd);
        }

        public static void SetValue3(string section, double value)
        {
            IniFile.I_I.WriteIni(section, "Value3", value.ToString(), PathStd);
        }

        public static void SetValue4(string section, double value)
        {
            IniFile.I_I.WriteIni(section, "Value4", value.ToString(), PathStd);
        }
        #endregion 写入参数
    }
}
