using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using Common;
using BasicClass;

namespace SetPar
{
    public partial class ParSetLogin:BaseClass
    {
        #region 读取Ini
        /// <summary>
        /// 读取权限设置相关参数
        /// </summary>
        public void ReadIniPar()
        {
            ReadIniLogin();
            ReadIniSetting();
        }

        //读取系统参数
        void ReadIniLogin()
        {
            try
            {
                strPassWord_Worker = IniFile.I_I.ReadIniStr("PassWord", "Worker", c_strPathinLog);
                strPassWord_Engineer = IniFile.I_I.ReadIniStr("PassWord", "Engineer", c_strPathinLog);
               
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("VarSetPar", ex);
            }
        }

        void ReadIniSetting()
        {
            try
            {
                strPassWord_Manufacturer = IniFile.I_I.ReadIniStr("PassWord", "Manufacturer", c_strPathinLog);

                this.StrInt = IniFile.I_I.ReadIniStr("Time", "TimeLogout", c_strPathinLog);
                TimeLogout = int.Parse(this.StrInt);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("VarSetPar", ex);
            }
        }
        #endregion 读取Ini

        #region 写入Ini
        /// <summary>
        /// 写入相关登陆密码
        /// </summary>
        public void WriteIniLogin()
        {
            try
            {
                IniFile.I_I.WriteIni("PassWord", "Worker", strPassWord_Worker.Trim(), c_strPathinLog);
                IniFile.I_I.WriteIni("PassWord", "Engineer", strPassWord_Engineer.Trim(), c_strPathinLog);
               
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("VarSetPar", ex);
            }
        }

        public void WriteIniSetting()
        {
            try
            {                
                IniFile.I_I.WriteIni("Time", "TimeLogout", TimeLogout.ToString(), c_strPathinLog);
                IniFile.I_I.WriteIni("PassWord", "Manufacturer", strPassWord_Manufacturer.Trim(), c_strPathinLog);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("VarSetPar", ex);
            }
        }
        #endregion 写入Ini
    }
}
