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
    /// WinMemory.xaml 的交互逻辑
    /// </summary>
    public partial class WinMemory : BaseMetroWindow
    {
        #region 初始化
        public WinMemory()
        {
            InitializeComponent();

            NameClass = "WinMemory";
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
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

        #region 保存退出
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //备份数据
                FunBackup.F_I.BackupSetPar();

                ParMemory.P_I.BlRecord = (bool)tsbRecordMemory.IsChecked;
                ParMemory.P_I.BlGCCollect = (bool)tsbGCCollect.IsChecked;
                ParMemory.P_I.Max = (double)dudMax.Value;
                ParMemory.P_I.WriteIniPar();

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
                FunLogButton.P_I.AddInfo("btnSave保存&退出", "设置内存监控");
            }
        }
        #endregion 保存退出

        #region 显示
        public override void ShowPar()
        {
            try
            {
                dudMax.Value = ParMemory.P_I.Max;
                tsbRecordMemory.IsChecked = ParMemory.P_I.BlRecord;
                tsbGCCollect.IsChecked = ParMemory.P_I.BlGCCollect;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

    }
}
