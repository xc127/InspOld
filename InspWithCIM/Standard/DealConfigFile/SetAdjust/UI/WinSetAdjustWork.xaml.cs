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
using System.IO;

namespace DealConfigFile
{
    /// <summary>
    /// SetCom.xaml 的交互逻辑
    /// </summary>
    public partial class WinSetAdjustWork : BaseWindow
    {
        #region 窗体单实例
        private static WinSetAdjustWork g_WinSetAdjustWork = null;
        public static WinSetAdjustWork GetWinInst()
        {
            if (g_WinSetAdjustWork == null)
            {
                g_WinSetAdjustWork = new WinSetAdjustWork();
            }
            return g_WinSetAdjustWork;
        }
        #endregion 窗体单实例

        #region 定义

        #endregion 定义

        #region 初始化
        public WinSetAdjustWork()
        {
            InitializeComponent();
        }
        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                new Task(new Action(ShowPar_Task));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinSetAdjustWork", ex);
            }
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
                //adjust1.ShowPar(ParAdjust.PathAdjust);
                //adjust2.ShowPar(ParAdjust.PathAdjust);
                //adjust3.ShowPar(ParAdjust.PathAdjust);
                //adjust4.ShowPar(ParAdjust.PathAdjust);
                //adjust5.ShowPar(ParAdjust.PathAdjust);
                //adjust6.ShowPar(ParAdjust.PathAdjust);
                //adjust7.ShowPar(ParAdjust.PathAdjust);
                //adjust8.ShowPar(ParAdjust.PathAdjust);
                //adjust9.ShowPar(ParAdjust.PathAdjust);
                //adjust10.ShowPar(ParAdjust.PathAdjust);
                //adjust11.ShowPar(ParAdjust.PathAdjust);
                //adjust12.ShowPar(ParAdjust.PathAdjust);
                //adjust13.ShowPar(ParAdjust.PathAdjust);

                if (!File.Exists(ParAdjust.PathAdjust))
                {
                    MessageBox.Show("调整值文件不存在:" + ParAdjust.PathAdjust);
                    //return;
                }
                foreach (BaseUCAdjust item in gdLayout.Children)
                {
                    ((BaseUCAdjust)item).ShowPar(ParAdjust.PathAdjust);
                }                  
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinSetAdjustWork", ex);
            }
        }
        #endregion 显示

        #region 退出窗体
        private void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                g_WinSetAdjustWork = null;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinSetAdjustWork", ex);
            }
        }
        #endregion 退出窗体
    }
}
