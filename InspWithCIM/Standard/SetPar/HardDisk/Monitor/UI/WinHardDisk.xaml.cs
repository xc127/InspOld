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
using BasicClass;
using DealLog;

namespace SetPar
{
    /// <summary>
    /// WinHardDisk.xaml 的交互逻辑
    /// </summary>
    public partial class WinHardDisk : BaseMetroWindow
    {
        #region 初始化
        public WinHardDisk()
        {
            InitializeComponent();

            NameClass = "WinHardDisk";
        }

        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ShowPar_Invoke();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        #region 设置硬盘号
        private void cboDriver_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                e.Handled = true;
                if (cboDriver.IsMouseOver)
                {
                    e.Handled = true;
                    string[] str = (cboDriver.SelectedValue.ToString()).Split(':');
                    ParHardDisk.P_I.NameDrive = str[1].Trim() + ":\\";
                    ShowSize();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 设置硬盘号

        #region 保存
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //备份数据
                FunBackup.F_I.BackupSetPar();

                ParHardDisk.P_I.SpaceMin = (double)dudMinSpace.Value;
                ParHardDisk.P_I.WriteIniPar();
                btnSave.RefreshDefaultColor("保存成功", true);

                Close700_Task();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                //按钮日志
                FunLogButton.P_I.AddInfo("btnSave保存&退出", "设置硬盘监控");
            }
        }
        #endregion 保存

        #region 显示
        public override void ShowPar()
        {
            try
            {
                cboDriver.Text = ParHardDisk.P_I.NameDrive.ToString();
                dudMinSpace.Value = ParHardDisk.P_I.SpaceMin;
                ShowSize();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void ShowSize()
        {
            try
            {
                double[] size = FunHarDisk.F_I.GetSpace(ParHardDisk.P_I.NameDrive);
                lblTotalSpace.Content = size[0];
                lblLeft.Content = size[1];
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示
    }
}
