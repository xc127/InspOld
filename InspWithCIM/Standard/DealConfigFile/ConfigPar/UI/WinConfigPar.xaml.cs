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
using System.IO;
using Common;
using DealFile;
using System.Threading;
using System.Threading.Tasks;
using BasicClass;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using DealLog;

namespace DealConfigFile
{
    /// <summary>
    /// WinConfigPar.xaml 的交互逻辑
    /// </summary>
    public partial class WinConfigPar : BaseWindow
    {
        #region 窗体单实例
        private static WinConfigPar g_WinConfigPar = null;
        public static WinConfigPar GetWinInst(out bool blNew)
        {
            blNew = false;
            try
            {
                if (g_WinConfigPar == null)
                {
                    blNew = true;
                    g_WinConfigPar = new WinConfigPar();
                }
                return g_WinConfigPar;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinConfigPar", ex);
                return null;
            }
        }

        public static WinConfigPar GetWinInst()
        {
            try
            {
                return g_WinConfigPar;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinConfigPar", ex);
                return null;
            }
        }
        #endregion 窗体单实例

        #region 定义
        //string 

        //定义事件       

        #endregion 定义

        #region 初始化
        //构造函数
        public WinConfigPar()
        {
            try
            {
                InitializeComponent();

                NameClass = "WinConfigPar";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinConfigPar", ex);
            }
        }

        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Init();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //初始化
        public override void Init()
        {
            ShowPar_Invoke();
        }
        #endregion 初始化

        #region 鼠标右键
        private void btnSave_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                btnPLCAnno.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 鼠标右键

        #region 读取PLC注释
        private void btnPLCAnno_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ParConfigPar.P_I.ReadPLCAnnotation();
                RefreshDgConfigPar();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 读取PLC注释

        #region 保存
        /// <summary>
        /// 保存产品参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string info = "保存成功";
            try
            {
                int numError = 0;
                if (!SavePar())
                {
                    numError++;
                }
                if (!ParConfigPar.P_I.WriteConfigIni())
                {
                    numError++;
                }
                if (numError == 0)
                {                    
                    btnSave.RefreshDefaultColor("保存成功", true);
                    //备份
                    FunBackup.F_I.BackupProduct();
                }
                else
                {
                    btnSave.RefreshDefaultColor("保存失败", false);
                    info = "保存失败";
                }
            }
            catch (Exception ex)
            {
                btnSave.RefreshDefaultColor("保存失败", false);
                Log.L_I.WriteError(NameClass, ex);
                info = "保存失败";
            }
            finally
            {
                //按钮日志
                FunLogButton.P_I.AddInfo("btnSave保存","设置产品参数" + info);
            }
        }
        #endregion 保存

        #region 显示
        //显示参数
        public override void ShowPar()
        {
            try
            {
                #region 显示参数
                ShowModel();//型号名称
                RefreshDgConfigPar();//刷新产品参数
                RefreshDgPosPhoto();//刷新拍照位置
                #endregion 显示参数

                #region 权限设置
                //base.SetBtnEnable(this.gdLayout, Authority.Authority_e);
                //base.SetBtnEnable(this.gdButton1, Authority.Authority_e);
                #endregion 权限设置
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //显示型号，即配置文件名称
        void ShowModel()
        {
            try
            {
                txtModel.Text = ComConfigPar.C_I.NameModel;//保存型号名称
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //刷新产品参数
        void RefreshDgConfigPar()
        {
            try
            {
                dgConfigPar.ItemsSource = ParConfigPar.P_I.ParProduct_L;
                dgConfigPar.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //刷新产品参数
        void RefreshDgPosPhoto()
        {
            try
            {
                dgPosPhoto.ItemsSource = ParConfigPar.P_I.PosPhoto_L;
                dgPosPhoto.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

        #region 关闭
        private void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                g_WinConfigPar = null;
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion 关闭
    }
}
