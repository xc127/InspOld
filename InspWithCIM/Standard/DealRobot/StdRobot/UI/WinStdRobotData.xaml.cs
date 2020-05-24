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

namespace DealRobot
{
    /// <summary>
    /// WinStdRobotData.xaml 的交互逻辑
    /// </summary>
    public partial class WinStdRobotData : BaseMetroWindow
    {
        #region 窗体单实例
        private static WinStdRobotData g_WinStdRobotData = null;
        public static WinStdRobotData GetWinInst(out bool blNew)
        {
            blNew = false;
            if (g_WinStdRobotData == null)
            {
                blNew = true;
                g_WinStdRobotData = new WinStdRobotData();
            }
            return g_WinStdRobotData;
        }
        #endregion 窗体单实例

        #region 初始化
        public WinStdRobotData()
        {
            InitializeComponent();
        }

        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            uCPropPoint.Init();
        }
        #endregion 初始化

        private void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            g_WinStdRobotData = null;
        }

       
    }
}
