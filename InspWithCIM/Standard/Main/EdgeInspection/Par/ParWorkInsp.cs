using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;
using System.IO;
using DealFile;

namespace Main
{
    public class ParWorkInsp
    {
        #region 定义
public static ParWorkInsp P_I = new ParWorkInsp();

readonly string section = "WorkType";

        public string BasePathIni
        {
            get
            {
                string path = ParPathRoot.PathRoot + "Store\\Inspection\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path + "ParInspWork.ini";
            }
        }

        #endregion

        public int CodeStartIndex { get; set; }
        public int NumCodeReg { get; set; }
        public int ResultForPLCIndex { get; set; }

        public bool SaveAllImage { get; set; }

        public void ReadIni()
        {
            CodeStartIndex = IniFile.I_I.ReadIniInt(section, "CodeStartIndex", BasePathIni);
            NumCodeReg = IniFile.I_I.ReadIniInt(section, "NumCodeReg", BasePathIni);
            ResultForPLCIndex = IniFile.I_I.ReadIniInt(section, "ResultForPLCIndex", BasePathIni);

            SaveAllImage = IniFile.I_I.ReadIniBl(section, "SaveAllImage", BasePathIni);
            CheckVisionWhenStart = IniFile.I_I.ReadIniBl(section, "CheckVisionWhenStart", BasePathIni);
        }

        public void WriteIni()
        {
            IniFile.I_I.WriteIni(section,"CodeStartIndex",CodeStartIndex,BasePathIni);
            IniFile.I_I.WriteIni(section,"NumCodeReg",NumCodeReg,BasePathIni);
            IniFile.I_I.WriteIni(section, "ResultForPLCIndex", ResultForPLCIndex, BasePathIni);
            IniFile.I_I.WriteIni(section, "SaveAllImage", SaveAllImage, BasePathIni);
            IniFile.I_I.WriteIni(section, "CheckVisionWhenStart", CheckVisionWhenStart, BasePathIni);

        }

        public bool CheckVisionWhenStart { get; set; }
    }
}
