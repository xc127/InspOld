using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DealFile;
using BasicClass;

namespace Main
{
    public partial class ParAnalysis
    {
        /// <summary>
        /// 用于将实时更新的数据写入到ini
        /// </summary>
        public void WriteDateNow()
        {
            try
            {
                string sec = DateNow + "-" + ShiftNow;
                IniFile.I_I.WriteIni(sec, "ShitfInfo", ShiftNow, r_StrPathFile);
                IniFile.I_I.WriteIni(sec, "NumAll", g_ProductNumInfoNow.NumAll, r_StrPathFile);
                IniFile.I_I.WriteIni(sec, "NumOK", g_ProductNumInfoNow.NumOK, r_StrPathFile);
                IniFile.I_I.WriteIni(sec, "NumNG", g_ProductNumInfoNow.NumNG, r_StrPathFile);
                IniFile.I_I.WriteIni(sec, "NumNGShell", g_ProductNumInfoNow.NumNGShell, r_StrPathFile);
                IniFile.I_I.WriteIni(sec, "NumNGCorner", g_ProductNumInfoNow.NumNGCorner, r_StrPathFile);
                IniFile.I_I.WriteIni(sec, "NumNGOther", g_ProductNumInfoNow.NumNGOther, r_StrPathFile);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParAnalysis",ex);
            }
        }
    }
}
