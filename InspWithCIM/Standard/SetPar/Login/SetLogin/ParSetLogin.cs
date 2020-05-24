using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using BasicClass;

namespace SetPar
{
    public partial class ParSetLogin
    {
        #region 静态类实例
        public static ParSetLogin P_I = new ParSetLogin();
        #endregion 静态类实例

        //string
        public static string c_strPathinLog = ComValue.c_PathSetPar + "Login.ini";

        //登录密码
        public string strPassWord_Worker = "";//技术员
        public string strPassWord_Engineer = "";//工程师
        public string strPassWord_Manufacturer = "";//厂商
        public int timeLogout = 5;
        public int TimeLogout
        {
            get
            {
                if(timeLogout<5)
                {
                    return 5;
                }
                else
                {

                    return timeLogout;
                }              
            }
            set
            {
                timeLogout=value;            
            }
        }

    }
}
