using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealConfigFile;
using DealFile;
using BasicClass;
using System.IO;
using DealLog;

namespace Main
{
    /// <summary>
    /// 此类为设备参数类，主要规划不同设备硬件关系差别
    /// </summary>
    public partial class ParSTDArrange
    {
        #region 定义
        readonly string SectionIni = "ParSTDArrangeConfig";

        public string PathParSTDArrange
        {
            get
            {
                string path = ParPathRoot.PathRoot + "Store\\ParSTDArrange\\ParSTDArrangeConfig.ini";
                if (!Directory.Exists(ParPathRoot.PathRoot + "Store\\ParSTDArrange\\"))
                {
                    Directory.CreateDirectory(ParPathRoot.PathRoot + "Store\\ParSTDArrange\\");
                }
                return path;
            }
        }

        #endregion

        #region 读写ini
        public bool SaveConfig()
        {
            try
            {
                IniFile.I_I.WriteIni(SectionIni, "IsArrange", IsArrange, PathParSTDArrange);
                if (!IsArrange)
                {
                    return true;
                }
                IniFile.I_I.WriteIni(SectionIni, "TypePreci_E", TypePreci_E.ToString(), PathParSTDArrange);
                IniFile.I_I.WriteIni(SectionIni, "TypeRobotCoor_E", TypeRobotCoor_E.ToString(), PathParSTDArrange);
                IniFile.I_I.WriteIni(SectionIni, "TypePlatWork_E", TypePlatWork_E.ToString(), PathParSTDArrange);
                IniFile.I_I.WriteIni(SectionIni, "ITOPlatSTDCorner_E", ITOPlatSTDCorner_E.ToString(), PathParSTDArrange);
                IniFile.I_I.WriteIni(SectionIni, "FunCam1_E", FunCam1_E.ToString(), PathParSTDArrange);
                IniFile.I_I.WriteIni(SectionIni, "FunCam2_E", FunCam2_E.ToString(), PathParSTDArrange);
                IniFile.I_I.WriteIni(SectionIni, "FunCam3_E", FunCam3_E.ToString(), PathParSTDArrange);
                IniFile.I_I.WriteIni(SectionIni, "FunCam4_E", FunCam4_E.ToString(), PathParSTDArrange);
                IniFile.I_I.WriteIni(SectionIni, "FunCam5_E", FunCam5_E.ToString(), PathParSTDArrange);
                IniFile.I_I.WriteIni(SectionIni, "FunCam6_E", FunCam6_E.ToString(), PathParSTDArrange);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParSTDArrange.SaveConfig", ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 读取标准收片机配置
        /// </summary>
        public void ReadConfig()
        {
            IsArrange = IniFile.I_I.ReadIniBl(SectionIni, "IsArrange", PathParSTDArrange);
            if (!IsArrange)
            {
                return;
            }

            string content = "";
            string error = "";
            try
            {
                content = IniFile.I_I.ReadIniStr(SectionIni, "TypePreci_E", PathParSTDArrange);
                TypePreci_E = (TypePreciLight_Enum)Enum.Parse(typeof(TypePreciLight_Enum), content);
            }
            catch (Exception ex)
            {
                error += "TypePreci_E ";
                Log.L_I.WriteError("ParSTDArrange.ReadConfig", ex);
            }

            try
            {
                content = IniFile.I_I.ReadIniStr(SectionIni, "TypeRobotCoor_E", PathParSTDArrange);
                TypeRobotCoor_E = (TypeRobotCoor_Enum)Enum.Parse(typeof(TypeRobotCoor_Enum), content);
            }
            catch (Exception ex)
            {
                error += "TypeRobotCoor_E ";
                Log.L_I.WriteError("ParSTDArrange.ReadConfig", ex);
            }

            try
            {
                content = IniFile.I_I.ReadIniStr(SectionIni, "TypePlatWork_E", PathParSTDArrange);
                TypePlatWork_E = (TypePlatWork_Enum)Enum.Parse(typeof(TypePlatWork_Enum), content);
            }
            catch (Exception ex)
            {
                error += "TypePlatWork_E ";
                Log.L_I.WriteError("ParSTDArrange.ReadConfig", ex);
            }

            try
            {
                content = IniFile.I_I.ReadIniStr(SectionIni, "ITOPlatSTDCorner_E", PathParSTDArrange);
                ITOPlatSTDCorner_E = (ITOPlatCorner_Enum)Enum.Parse(typeof(ITOPlatCorner_Enum), content);
            }
            catch (Exception ex)
            {
                error += "ITOPlatSTDCorner_E ";
                Log.L_I.WriteError("ParSTDArrange.ReadConfig", ex);
            }

            try
            {
                content = IniFile.I_I.ReadIniStr(SectionIni, "FunCam1_E", PathParSTDArrange);
                FunCam1_E = (FunCam_Enum)Enum.Parse(typeof(FunCam_Enum), content);
            }
            catch (Exception ex)
            {
                error += "FunCam1_E ";
                Log.L_I.WriteError("ParSTDArrange.ReadConfig", ex);
            }

            try
            {
                content = IniFile.I_I.ReadIniStr(SectionIni, "FunCam2_E", PathParSTDArrange);
                FunCam2_E = (FunCam_Enum)Enum.Parse(typeof(FunCam_Enum), content);
            }
            catch (Exception ex)
            {
                error += "FunCam2_E ";
                Log.L_I.WriteError("ParSTDArrange.ReadConfig", ex);
            }

            try
            {
                content = IniFile.I_I.ReadIniStr(SectionIni, "FunCam3_E", PathParSTDArrange);
                FunCam3_E = (FunCam_Enum)Enum.Parse(typeof(FunCam_Enum), content);
            }
            catch (Exception ex)
            {
                error += "FunCam3_E ";
                Log.L_I.WriteError("ParSTDArrange.ReadConfig", ex);
            }

            try
            {
                content = IniFile.I_I.ReadIniStr(SectionIni, "FunCam4_E", PathParSTDArrange);
                FunCam4_E = (FunCam_Enum)Enum.Parse(typeof(FunCam_Enum), content);
            }
            catch (Exception ex)
            {
                error += "FunCam4_E ";
                Log.L_I.WriteError("ParSTDArrange.ReadConfig", ex);
            }

            try
            {
                content = IniFile.I_I.ReadIniStr(SectionIni, "FunCam5_E", PathParSTDArrange);
                FunCam5_E = (FunCam_Enum)Enum.Parse(typeof(FunCam_Enum), content);
            }
            catch (Exception ex)
            {
                error += "FunCam5_E ";
                Log.L_I.WriteError("ParSTDArrange.ReadConfig", ex);
            }

            try
            {
                content = IniFile.I_I.ReadIniStr(SectionIni, "FunCam6_E", PathParSTDArrange);
                FunCam6_E = (FunCam_Enum)Enum.Parse(typeof(FunCam_Enum), content);
            }
            catch (Exception ex)
            {
                error += "FunCam6_E ";
                Log.L_I.WriteError("ParSTDArrange.ReadConfig", ex);
            }

            //error = "";
            if (error != "")
            {
                WinError.GetWinInst().ShowError("收片机配置及参数初始化失败：\n" + error);
            }
        }
        #endregion

    }

}