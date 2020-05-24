using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using BasicClass;

namespace DealConfigFile
{
    public class AdjustIniBase
    {
        #region 静态类实例
        public static AdjustIniBase A_I = new AdjustIniBase();
        #endregion 静态类实例

        #region 定义       
        //string 
        //保存路径
        public string g_Path { get; set; }//保存路径        

        //double
        #region Value1
        public double Value1(string strSection)
        {
            try
            {
                return IniFile.I_I.ReadIniDbl(strSection.ToLower(), "Value1", g_Path);
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("AdjustIniBase", ex);
                return 0;
            }            
        }
        public double Value1(string strSection,string path)
        {
            try
            {
                return IniFile.I_I.ReadIniDbl(strSection.ToLower(), "Value1", path);
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("AdjustIniBase", ex);
                return 0;
            }
        }
        #endregion Value1

        #region Value2
        public double Value2(string strSection)
        {
            try
            {
                return IniFile.I_I.ReadIniDbl(strSection.ToLower(), "Value2", g_Path);
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("AdjustIniBase", ex);
                return 0;
            }
        }
        public double Value2(string strSection, string path)
        {
            try
            {
                return IniFile.I_I.ReadIniDbl(strSection.ToLower(), "Value2", path);
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("AdjustIniBase", ex);
                return 0;
            }
        }
        #endregion Value2

        #region Value3
        public double Value3(string strSection)
        {
            try
            {
                return IniFile.I_I.ReadIniDbl(strSection.ToLower(), "Value3", g_Path);
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("AdjustIniBase", ex);
                return 0;
            }
        }
        public double Value3(string strSection, string path)
        {
            try
            {
                return IniFile.I_I.ReadIniDbl(strSection.ToLower(), "Value3", path);
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("AdjustIniBase", ex);
                return 0;
            }
        }
        #endregion Value3

        #region Value4
        public double Value4(string strSection)
        {
            try
            {
                return IniFile.I_I.ReadIniDbl(strSection.ToLower(), "Value4", g_Path);
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("AdjustIniBase", ex);
                return 0;
            }
        }
        public double Value4(string strSection, string path)
        {
            try
            {
                return IniFile.I_I.ReadIniDbl(strSection.ToLower(), "Value4", path);
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("AdjustIniBase", ex);
                return 0;
            }
        }
        #endregion Value4

        //string 
        public string Annotation(string strSection)
        {
            try
            {
                return IniFile.I_I.ReadIniStr(strSection, "Annotation", g_Path);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("AdjustIniBase", ex);
                return "";
            }            
        }

        public string Annotation(string strSection, string path)
        {
            try
            {
                return IniFile.I_I.ReadIniStr(strSection, "Annotation", path);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("AdjustIniBase", ex);
                return "";
            }
        }
        #endregion 定义
     
    }
}
