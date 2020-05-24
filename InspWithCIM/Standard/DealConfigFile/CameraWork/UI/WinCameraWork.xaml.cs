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
using Common;
using System.Threading;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using BasicClass;
using DealLog;

namespace DealConfigFile
{
    /// <summary>
    /// WinTestRun.xaml 的交互逻辑
    /// </summary>
    public partial class WinCameraWork : BaseMetroWindow
    {
        #region 定义
        //int
        
        //List
      
        #endregion 定义

        #region 初始化
        public WinCameraWork()
        {      
            InitializeComponent();
            NameClass = "WinCameraWork";
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

        #region 保存
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string info = "保存成功";
            try
            {
                for (int i = 0; i < dgCameraWork.Items.Count; i++)
                {
                     ParCameraWork.P_I.ParCameraWork_L[i]= (ParCameraWork)dgCameraWork.Items[i];
                }
                
                //写入Ini
                if (ParCameraWork.P_I.WriteIni())
                {
                    btnSave.RefreshDefaultColor("保存成功!", true);
                }
                else
                {
                    btnSave.RefreshDefaultColor("保存失败!", false);
                    info = "保存失败";
                }

                //备份数据
                FunBackup.F_I.BackupParSysWork();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                btnSave.RefreshDefaultColor("保存失败!", false);
                info = "保存失败";
            }
            finally
            {
                //按钮日志
                FunLogButton.P_I.AddInfo("btnSave保存",
                "设置相机工作参数" + info);
            }
        }
        #endregion 保存

        #region 显示       
        public override void ShowPar()
        {
            try
            {
                dgCameraWork.ItemsSource = ParCameraWork.P_I.ParCameraWork_L;
                dgCameraWork.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

        #region Close
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion Close       
    }
}
