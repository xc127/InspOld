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
using System.Threading;

namespace DealLog
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class WinError : MetroWindow
    {
        #region 窗体单实例
        private static WinError g_WinError = null;
        public static WinError GetWinInst()
        {
            try
            {
                if (g_WinError == null)
                {
                    g_WinError = new WinError();
                }
                return g_WinError;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinError", ex);
                return null;
            }
        }
        #endregion 窗体单实例

        #region 定义
        public bool BlInit = false;
        bool BlTopmost = false;

        int NumTopmost = 0;

        //string
        string g_ErrorInfo = "";
        string NameClass = "WinError";

        System.Windows.Forms.Timer tm_WinTop = new System.Windows.Forms.Timer();//刷新定时器;//控制窗体TopMost时间

        //mutex
        Mutex g_MtShowError = new Mutex();
        #endregion 定义

        #region 初始化
        public WinError()
        {
            InitializeComponent();
            //开始定时器
            //tm_WinTop.Interval = 1000;
            //tm_WinTop.Enabled = true;
            //tm_WinTop.Tick += new EventHandler(tm_WinTop_Tick);
        }

        #endregion 初始化

        #region 显示
        public void ShowError(string info)
        {
            g_MtShowError.WaitOne();
            try
            {
                g_ErrorInfo += info + "\n\n";

                if (BlInit)
                {
                    BlTopmost = true;
                    this.Dispatcher.Invoke(new Action(ShowError_Invoke));
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            g_MtShowError.ReleaseMutex();
        }
        void ShowError_Invoke()
        {
            try
            {
                if (g_ErrorInfo.Trim() != "")
                {
                    this.Visibility = Visibility.Visible;//窗体显示出来
                    txtBError.Text = g_ErrorInfo;
                    //this.Opacity = 1;
                    this.Topmost = true;
                    base.Show();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }


        /// <summary>
        /// 控制窗体的Topmost
        /// </summary>
        void tm_WinTop_Tick(object sender, EventArgs e)
        {
            try
            {
                if(BlTopmost)
                {
                    BlTopmost = false;
                    NumTopmost = 0;
                }
                NumTopmost++;
                //当错误窗体显示超过5S，退出Topmost
                if (NumTopmost > 10)
                {
                    NumTopmost = 0;
                    this.Dispatcher.Invoke(new Action(() =>
                        {
                            this.Opacity = 0.2;
                        }));
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

        #region 关闭
        /// <summary>
        /// 关闭窗体
        /// </summary>
        public virtual void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;//只在此函数中处理

                g_ErrorInfo = "";
                txtBError.Text = "";

                this.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }               
        #endregion 关闭
    }
}
