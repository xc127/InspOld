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
        /// 从本地读取当前的生产信息
        /// </summary>
        public void InitInfoNow()
        {
            try
            {
                string sec = DateNow + "-" + ShiftNow;

                g_ProductNumInfoNow.NumAll = IniFile.I_I.ReadIniInt(sec, "NumAll", r_StrPathFile);
                g_ProductNumInfoNow.NumOK = IniFile.I_I.ReadIniInt(sec, "NumOK", r_StrPathFile);
                g_ProductNumInfoNow.NumNG = IniFile.I_I.ReadIniInt(sec, "NumNG", r_StrPathFile);
                g_ProductNumInfoNow.NumNGShell = IniFile.I_I.ReadIniInt(sec, "NumNGShell", r_StrPathFile);
                g_ProductNumInfoNow.NumNGCorner = IniFile.I_I.ReadIniInt(sec, "NumNGCorner", r_StrPathFile);
                g_ProductNumInfoNow.NumNGOther = IniFile.I_I.ReadIniInt(sec, "NumNGOther", r_StrPathFile);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParAnalysis", ex);
            }

        }
    }
}
