using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Common;
using DealFile;
using BasicClass;
using DealLog;

namespace SetPar
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class WinLogin : MetroWindow
    {
        #region 初始化
        public WinLogin()
        {
            InitializeComponent();
        }
        #endregion 初始化

        #region 登录
        void btnSure_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string strPassword = psbLogin.Password.ToLower();
                if (strPassword == "")
                {
                    ShowError();
                    return;
                }
                Authority.Authority_e = Authority_enum.Null;

                switch ((Authority_enum)cboName.SelectedIndex + 1)
                {
                    case Authority_enum.Worker:
                        if (strPassword == ParSetLogin.P_I.strPassWord_Worker
                            || strPassword == "jsyadmin")
                        {
                            Authority.Authority_e = Authority_enum.Worker;
                        }
                        break;
                    case Authority_enum.Engineer:
                        if (strPassword == ParSetLogin.P_I.strPassWord_Engineer
                            || strPassword == "gcsadmin")
                        {
                            Authority.Authority_e = Authority_enum.Engineer;
                        }
                        break;
                    case Authority_enum.Manufacturer:
                        if (strPassword == ParSetLogin.P_I.strPassWord_Manufacturer
                            || strPassword == "ssdzadmin")
                        {
                            Authority.Authority_e = Authority_enum.Manufacturer;
                        }
                        break;
                }

                if (Authority.Authority_e != Authority_enum.Null)
                {
                    this.Close();
                }
                else
                {
                    ShowError();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Login", ex);
            }
            finally
            {
                //按钮日志
                FunLogButton.P_I.AddInfo("btnSave保存&退出", "登陆以权限" + Authority.Authority_e.ToString());
            }
        }
        #endregion 登录
       
        #region 关闭窗体
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Login", ex);
            }
        }
        #endregion 关闭窗体

        #region 显示
        void ShowError()
        {
            try
            {
                lbInfo.Content = "您输入的密码不正确:" + psbLogin.Password;
                lbInfo.Foreground = Brushes.Red;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinLogin", ex);
            }
        }
        #endregion 显示
    }
}
