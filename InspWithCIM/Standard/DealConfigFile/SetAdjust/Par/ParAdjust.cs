using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DealFile;
using System.IO;

namespace DealConfigFile
{
    public class ParAdjust
    {
        #region 定义
        public const int C_Num = 18;

        //Path
        //新建型号时的补偿值文件
        public static string PathOldAdjust//参数保存的路径，按照不同的配置文件进行保存
        {
            get
            {
                string root = ComConfigPar.C_I.PathOldConfigIni.Replace("Product.ini", "调整值");
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                return root + "\\Adjust.ini";
            }
        }

        public static string PathRootAdjust
        {
            get
            {
                string root = ComConfigPar.C_I.PathConfigIni.Replace("Product.ini", "调整值");
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                return root;
            }
        }

        public static string PathAdjust
        {
            get
            {
                string root = ComConfigPar.C_I.PathConfigIni.Replace("Product.ini", "调整值");
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                return root + "\\Adjust.ini";
            }
        }
        #endregion 定义

        #region 读取参数
        public static double Value1(string strSection)
        {
            try
            {
                if (strSection.Contains("adjust"))
                {
                    strSection = strSection.Replace("adjust", "adj");
                }
                return AdjustIniBase.A_I.Value1(strSection, PathAdjust);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double Value2(string strSection)
        {
            try
            {
                if (strSection.Contains("adjust"))
                {
                    strSection = strSection.Replace("adjust", "adj");
                }
                return AdjustIniBase.A_I.Value2(strSection, PathAdjust);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double Value3(string strSection)
        {
            try
            {
                if (strSection.Contains("adjust"))
                {
                    strSection = strSection.Replace("adjust", "adj");
                }
                return AdjustIniBase.A_I.Value3(strSection, PathAdjust);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static double Value4(string strSection)
        {
            try
            {
                if (strSection.Contains("adjust"))
                {
                    strSection = strSection.Replace("adjust", "adj");
                }
                return AdjustIniBase.A_I.Value4(strSection, PathAdjust);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion 读取参数

        #region 写入参数
        public static void SetValue1(string section ,double value)
        {
            IniFile.I_I.WriteIni(section, "Value1", value.ToString(), PathAdjust);
        }

        public static void SetValue2(string section, double value)
        {
            IniFile.I_I.WriteIni(section, "Value2", value.ToString(), PathAdjust);
        }

        public static void SetValue3(string section, double value)
        {
            IniFile.I_I.WriteIni(section, "Value3", value.ToString(), PathAdjust);
        }

        public static void SetValue4(string section, double value)
        {
            IniFile.I_I.WriteIni(section, "Value4", value.ToString(), PathAdjust);
        }
        #endregion 写入参数
    }
}
