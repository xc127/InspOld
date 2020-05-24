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

namespace Main
{
    /// <summary>
    /// WinSetSTDArrayConfig.xaml 的交互逻辑
    /// </summary>
    public partial class WinSetSTDArrayConfig : BaseMetroWindow
    {
        #region 窗口单实例
        public static WinSetSTDArrayConfig g_WinInst = null;

        public static WinSetSTDArrayConfig GetWinInst(out bool blNew)
        {
            blNew = false;
            try
            {
                if (g_WinInst == null)
                {
                    blNew = true;
                    g_WinInst = new WinSetSTDArrayConfig();
                }
            }
            catch (Exception)
            {

            }
            return g_WinInst;
        }
        #endregion

        #region 初始化
        public WinSetSTDArrayConfig()
        {
            InitializeComponent();
            this.DataContext = ParSTDArrange.P_I;
        }
        #endregion

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            g_WinInst = null;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ParSTDArrange.P_I.SaveConfig())
            {
                MessageBox.Show("保存成功！", "提示");
            }
        }

        private void cbxIsArrange_CheckStatueChanged(object sender, RoutedEventArgs e)
        {
            ParSTDArrange.P_I.IsArrange = (bool)cbxIsArrange.IsChecked;
        }
    }
}
