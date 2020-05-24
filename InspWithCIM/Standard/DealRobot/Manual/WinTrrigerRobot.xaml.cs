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
using ControlLib;
using Common;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using DealRobot;
using MahApps.Metro.Controls.Dialogs;
using System.IO;
using DealFile;
using MahApps.Metro.Controls;
using BasicClass;
using DealConfigFile;
using DealLog;

namespace DealRobot
{
    /// <summary>
    /// TrrigerRobot.xaml 的交互逻辑
    /// </summary>
    public partial class WinTrrigerRobot : BaseMetroWindow
    {
        #region 窗体单实例
        private static WinTrrigerRobot g_WinTrrigerRobot = null;
        public static WinTrrigerRobot GetWinInst(out bool blNew)
        {
            blNew = false;
            if (g_WinTrrigerRobot == null)
            {
                blNew = true;
                g_WinTrrigerRobot = new WinTrrigerRobot();
            }
            return g_WinTrrigerRobot;
        }
        #endregion 窗体单实例

        #region 定义
        public int g_intIndex = 0;

        //List
        public List<RobotTrriger> g_RobotTrriger_L = new List<RobotTrriger>();

        //event
        public event ManualRobot_del ManualRobot_event;

        //string 
        
        //List
        public List<RobotTrriger> g_ManualRobot_L = new List<RobotTrriger>();
        #endregion 定义

        #region 初始化
        public WinTrrigerRobot()
        {
            InitializeComponent();
            ComValue.C_I.blTrrigerRobot = true;
            InitCMD();
        }
        /// <summary>
        /// 初始化机器人指令
        /// </summary>
        void InitCMD()
        {
            try
            {
                int num = 1;
                g_RobotTrriger_L.Clear();
                for (int i = 0; i < ParCameraWork.NumCamera; i++)
                {
                    for (int j = 0; j < ParCameraWork.P_I[i + 1]; j++)
                    {
                        char ch = (char)('A' + i);
                        g_RobotTrriger_L.Add(new RobotTrriger()
                        {
                            No = num,
                            CMD = ch.ToString() +","+ (j + 1).ToString(),
                            Annotation = "相机" + (i + 1).ToString() + "位置" + (j + 1).ToString()
                        });
                        num++;
                    }
                }

                dgTrrigerRobot.ItemsSource = g_RobotTrriger_L;
                dgTrrigerRobot.Items.Refresh();


                //手动机器人操作
                g_ManualRobot_L.Add(new RobotTrriger());
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerRobot", ex);
            }

        }
        #endregion 初始化

        #region 手动操作
        /// <summary>
        /// 发送配置参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRobotConfigPar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ManualRobot_event(ManualRobot_enum.ConfigPar);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerRobot", ex);
            }
        }

        /// <summary>
        /// 重启机器人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestartRobot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ManualRobot_event(ManualRobot_enum.Restart);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerRobot", ex);
            }
        }

        /// <summary>
        /// 关闭机器人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseRobot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ManualRobot_event(ManualRobot_enum.Close);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerRobot", ex);
            }
        }

        /// <summary>
        /// Home
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHomeRobot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ManualRobot_event(ManualRobot_enum.Home);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerRobot", ex);
            }
        }

        /// <summary>
        /// reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResetRobot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ManualRobot_event(ManualRobot_enum.Reset);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerRobot", ex);
            }
        }

        #endregion 手动操作

        #region 触发
        //选择命令
        private void dgTrrigerRobot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                e.Handled = true;
                g_intIndex = this.dgTrrigerRobot.SelectedIndex;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerRobot", ex);
            }           
        }

        private void btnTrriger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ParLogicRobot.P_I.strTrrigerRobot = g_RobotTrriger_L[g_intIndex].CMD;
                Task task = new Task(LogicRobot.L_I.TrrigerRobot);
                task.Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerRobot", ex);
            }
            finally
            {
                //按钮日志
                FunLogButton.P_I.AddInfo("btnTrriger触发",
                "模拟机器人触发");
            }
        } 
        #endregion 触发

        #region 关闭
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                ComValue.C_I.blTrrigerRobot = false;
                g_WinTrrigerRobot = null;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinTrrigerRobot", ex);
            }            
        }
        #endregion 关闭

        
        
    }

    public delegate void ManualRobot_del(ManualRobot_enum manualRobot_e);

    /// <summary>
    /// 机器人手动操作
    /// </summary>
    public enum ManualRobot_enum
    {
        Null,
        Close,
        Restart,
        ConfigPar,
        Reset,
        Home,
    }   
}
