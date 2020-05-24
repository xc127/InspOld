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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DealFile;
using Common;
using BasicClass;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using DealLog;

namespace SetPar
{
    /// <summary>
    /// WinSetLogin.xaml 的交互逻辑
    /// </summary>
    public partial class WinSetLogin : BaseWindow
    {
        #region 初始化
        public WinSetLogin()
        {
            InitializeComponent();
            NameClass = "WinSetLogin";
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }
        public override void Init()
        {
            ShowPar_Invoke();
        }                
        #endregion 初始化

        #region 保存密码
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //备份数据
                FunBackup.F_I.BackupSetPar();

                if (passwordBox.Password.Trim() != passwordBoxSure.Password.Trim())
                {
                    MessageBox.Show("两次输入的密码不一致!");
                    return;
                }
                else
                {
                    SavePassword();
                    ParSetLogin.P_I.WriteIniLogin();
                    this.btnSave.RefreshDefaultColor("保存成功", true);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                //按钮日志
                FunLogButton.P_I.AddInfo("btnSave保存&退出", "设置登录权限");
            }
        }

        void SavePassword()
        {
            try
            {
                switch ((Authority_enum)cboName.SelectedIndex + 1)
                {
                    case Authority_enum.Worker:
                        ParSetLogin.P_I.strPassWord_Worker = passwordBox.Password.Trim();
                        break;
                    case Authority_enum.Engineer:
                        ParSetLogin.P_I.strPassWord_Engineer = passwordBox.Password.Trim();

                        if (Authority.Authority_e == Authority_enum.Engineer
                            || Authority.Authority_e == Authority_enum.Manufacturer)
                        {

                        }
                        else
                        {
                            btnSave.IsEnabled = false;
                        }
                        break;
                    case Authority_enum.Manufacturer:
                        ParSetLogin.P_I.strPassWord_Manufacturer = passwordBox.Password.Trim();
                        if (Authority.Authority_e == Authority_enum.Manufacturer)
                        {

                        }
                        else
                        {
                            btnSave.IsEnabled = false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }             
        #endregion 保存密码

        #region 使能保存按钮
        private void cboName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                e.Handled = true;
                switch ((Authority_enum)cboName.SelectedIndex + 1)
                {
                    case Authority_enum.Worker:
                        //btnSave.IsEnabled = true;
                        break;
                    case Authority_enum.Engineer:

                        if (Authority.Authority_e == Authority_enum.Engineer
                            || Authority.Authority_e == Authority_enum.Manufacturer)
                        {
                            btnSave.IsEnabled = true;
                        }
                        else
                        {
                            btnSave.IsEnabled = false;
                        }
                        break;
                    case Authority_enum.Manufacturer:
                        if (Authority.Authority_e == Authority_enum.Manufacturer)
                        {
                            btnSave.IsEnabled = true;
                        }
                        else
                        {
                            btnSave.IsEnabled = false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 使能保存按钮

        #region 保存注销时间
        private void btnSaveSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveTime();
                SaveDefault();
                ParSetLogin.P_I.WriteIniSetting();
                btnSaveSetting.RefreshDefaultColor("保存成功", true);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void SaveTime()
        {
            try
            {
                ParSetLogin.P_I.TimeLogout = (int)cboTime.SelectedValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        //保存默认厂商登录
        void SaveDefault()
        {
            try
            {
                RegeditLogin.R_I.BlManufacturer = (bool)chkManufacturer.IsChecked;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 保存注销时间

        #region 显示    
        public override void ShowPar()
        {
            try
            {              
                cboTime.SelectedValue = (Time_enum)ParSetLogin.P_I.TimeLogout;
                chkManufacturer.IsChecked = RegeditLogin.R_I.BlManufacturer;

                #region 登录权限判断
                base.SetBtnEnable(this.gdLayout, Authority.Authority_e);
                base.SetBtnEnable(this.gdLayout1, Authority.Authority_e);
                #endregion 登录权限判断
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示        
    }
}
