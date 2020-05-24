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
using BasicClass;

namespace DealRobot
{
    /// <summary>
    /// UCRobotCtr.xaml 的交互逻辑
    /// </summary>
    public partial class UCRobotCtr : BaseControl
    {
        #region 初始化
        public UCRobotCtr()
        {
            InitializeComponent();
        }
        #endregion 初始化

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogicRobot.L_I.ResetRobot();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btnSuction_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnSuction.Content == "吸气")
                {
                    LogicRobot.L_I.WriteRobotCMD("1", "2200");
                    if (LogicRobot.L_I.WaitForOK())
                    {
                        btnSuction.Background = Brushes.Yellow;
                        btnSuction.Content = "停止吸气";
                    }
                }
                else if (btnSuction.Content == "停止吸气")
                {
                    LogicRobot.L_I.WriteRobotCMD("2", "2200");
                    if (LogicRobot.L_I.WaitForOK())
                    {
                        btnSuction.Background = new SolidColorBrush(btnSuction.ButtonColor_Basic);
                        btnSuction.Content = "吸气";
                    }
                }
            }
            catch (Exception ex)
            {
            
            }
        }

        private void btnBlow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnSuction.Content == "吹气")
                {
                    LogicRobot.L_I.WriteRobotCMD("1", "2300");
                    if (LogicRobot.L_I.WaitForOK())
                    {
                        btnSuction.Background = Brushes.Yellow;
                        btnSuction.Content = "停止吹气";
                    }

                }
                else if (btnSuction.Content == "停止吹气")
                {
                    LogicRobot.L_I.WriteRobotCMD("2", "2300");
                    if (LogicRobot.L_I.WaitForOK())
                    {
                        btnSuction.Background = new SolidColorBrush(btnSuction.ButtonColor_Basic);
                        btnSuction.Content = "吹气";
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
