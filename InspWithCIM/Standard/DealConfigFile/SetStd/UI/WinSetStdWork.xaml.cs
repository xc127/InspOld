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
using BasicClass;
using Common;
using System.Threading;
using System.Threading.Tasks;

namespace DealConfigFile
{
    /// <summary>
    /// WinSetStdWork.xaml 的交互逻辑
    /// </summary>
    public partial class WinSetStdWork : BaseWindow
    {
        #region 窗体单实例
        private static WinSetStdWork g_WinSetStdWork = null;
        public static WinSetStdWork GetWinInst()
        {
            if (g_WinSetStdWork == null)
            {
                g_WinSetStdWork = new WinSetStdWork();
            }
            return g_WinSetStdWork;
        }
        #endregion 窗体单实例

        #region 定义

        #endregion 定义

        #region 初始化
        public WinSetStdWork()
        {
            InitializeComponent();
            NameClass = "WinSetStdWork";
        }
        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Task task = new Task(ShowPar_Task);
            task.Start();
        }
        #endregion 初始化

        #region 显示
        public void ShowPar_Task()
        {
            Thread.Sleep(300);
            ShowPar_Invoke();
        }
        public override void ShowPar()
        {
            try
            {
                //adjust1.ShowPar(ParStd.PathStd);
                //adjust2.ShowPar(ParStd.PathStd);
                //adjust3.ShowPar(ParStd.PathStd);
                //adjust4.ShowPar(ParStd.PathStd);
                //adjust5.ShowPar(ParStd.PathStd);
                //adjust6.ShowPar(ParStd.PathStd);
                //adjust7.ShowPar(ParStd.PathStd);
                //adjust8.ShowPar(ParStd.PathStd);
                //adjust9.ShowPar(ParStd.PathStd);
                //adjust10.ShowPar(ParStd.PathStd);
                //adjust11.ShowPar(ParStd.PathStd);
                //adjust12.ShowPar(ParStd.PathStd);
                //adjust13.ShowPar(ParStd.PathStd);

                foreach (BaseUCAdjust item in gdLayout.Children)
                {
                    ((BaseUCAdjust)item).ShowPar(ParStd.PathStd);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

        #region 退出窗体
        private void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                g_WinSetStdWork = null;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 退出窗体

    }
}
