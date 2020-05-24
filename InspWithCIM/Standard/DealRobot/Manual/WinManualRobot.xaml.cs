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
    /// WinMannulRobot.xaml 的交互逻辑
    /// </summary>
    public partial class WinManualRobot : BaseMetroWindow
    {
        #region 窗体单实例
        private static WinManualRobot g_WinManualRobot = null;
        public static WinManualRobot GetWinInst(out bool blNew)
        {
            blNew = false;
            if (g_WinManualRobot == null)
            {
                blNew = true;
                g_WinManualRobot = new WinManualRobot();
            }
            return g_WinManualRobot;
        }
        #endregion 窗体单实例

        #region 初始化
        public WinManualRobot()
        {
            InitializeComponent();
        }

        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            uCRobotIO.Init();
            loginEvent();
        }

        void loginEvent()
        {
            LogicRobot.L_I.Input_event += uCRobotIO.SetInputValue;
            LogicRobot.L_I.Output_event += uCRobotIO.SetOutputValue;
            LogicRobot.L_I.JogPosCurr_event += uCTeachRobot.JogPosCurr_EventHandler;
        }
        #endregion 初始化

        #region 退出
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                
            }
        }
        private void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            g_WinManualRobot = null;
        }
        #endregion 退出

       
    }
}
