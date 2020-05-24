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

namespace Main
{
    /// <summary>
    /// WinParInsp.xaml 的交互逻辑
    /// </summary>
    public partial class WinParWorkMode : Window
    {
        public static WinParWorkMode g_WinInst = null;

        public static WinParWorkMode GetWinInst()
        {
            if (g_WinInst == null)
            {
                g_WinInst = new WinParWorkMode();
            }
            g_WinInst.Focus();
            return g_WinInst;
        }

        public WinParWorkMode()
        {
            InitializeComponent();
            this.DataContext = ParWorkInsp.P_I;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            ParWorkInsp.P_I.WriteIni();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            g_WinInst = null;
        }
    }
}
