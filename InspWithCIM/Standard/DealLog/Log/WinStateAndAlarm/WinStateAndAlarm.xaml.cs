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
using BasicClass;

namespace DealLog
{
    /// <summary>
    /// WinStateAndAlarm.xaml 的交互逻辑
    /// </summary>
    public partial class WinStateAndAlarm : Window
    {
        #region 静态类实例
        private static WinStateAndAlarm g_WinStateAndAlarm = null;
        public static WinStateAndAlarm GetWinInst(out bool blNew)
        {
            blNew = false;
            try
            {
                if (g_WinStateAndAlarm == null)
                {
                    blNew = true;
                    g_WinStateAndAlarm = new WinStateAndAlarm();
                }
                return g_WinStateAndAlarm;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinWinStateAndAlarm", ex);
                return null;
            }
        }

        public static WinStateAndAlarm GetWinInst()
        {
            try
            {
                return g_WinStateAndAlarm;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinWinStateAndAlarm", ex);
                return null;
            }
        }
        #endregion 静态类实例

        #region 定义
        string NameClass = "WinStateAndAlarm";

        public UCStateWork g_UCStateWork
        {
            get
            {
                return uCStateWork;
            }
        }

        public UCAlarm g_UCAlarm
        {
            get
            {
                return uCAlarm;
            }
        }
        #endregion 定义

        #region 初始化
        public WinStateAndAlarm()
        {
            InitializeComponent();

            LocationRight();            
        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowPar();
        }

        public virtual void LocationRight()
        {
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            base.WindowStartupLocation = WindowStartupLocation.Manual;
            base.HorizontalAlignment = HorizontalAlignment.Right;
            base.VerticalAlignment = VerticalAlignment.Center;
            base.Left = (screenWidth - this.Width - 10);
            base.Top = (screenHeight - this.Height) / 2;
        }
        #endregion 初始化

        #region 参数调整
        /// <summary>
        /// 主界面显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbMainShow_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ParStateAndAlarm.P_I.BlShowMain = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void tsbMainShow_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                ParStateAndAlarm.P_I.BlShowMain = false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 窗体自动显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbAutoShow_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ParStateAndAlarm.P_I.BlAutoShow = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void tsbAutoShow_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                ParStateAndAlarm.P_I.BlAutoShow = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 窗体再最前
        /// </summary>
        private void chkTopMost_Checked(object sender, RoutedEventArgs e)
        {
            this.Topmost = true;
        }

        private void chkTopMost_Unchecked(object sender, RoutedEventArgs e)
        {
            this.Topmost = false;
        }
        #endregion 参数调整

        #region 显示
        public void ShowPar()
        {
            try
            {
                tsbMainShow.IsChecked = ParStateAndAlarm.P_I.BlShowMain;
                tsbAutoShow.IsChecked = ParStateAndAlarm.P_I.BlAutoShow;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }


        public void ShowStateAndAlarm()
        {
            try
            {
                uCStateWork.ShowPar_Invoke();//显示运行状态
                uCAlarm.ShowPar_Invoke();//显示报警信息
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 添加状态显示信息
        /// </summary>
        public void AddStateInfo(string info)
        {
            try
            {
                uCStateWork.AddInfo(info);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        public void AddAlarmInfo(string info)
        {
            try
            {
                uCAlarm.AddInfo(info);
                uCStateWork.AddInfo(info);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

       
    }
}
