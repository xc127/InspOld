
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
using Camera;
using HalconDotNet;
using System.Drawing;
using DealRobot;
using DealPLC;
using MahApps.Metro.Controls.Dialogs;
using System.IO;
using DealComprehensive;
using SetPar;
using SetComprehensive;
using DealFile;
using BasicClass;
using DealConfigFile;

namespace Main
{
    partial class MainWindow
    {
        #region  定义
        //
        int time_Logout = 0;//注销登录开始计数计时

        //Timer
        System.Windows.Forms.Timer timer_LongMonitor = new System.Windows.Forms.Timer();//长时间监控
        System.Windows.Forms.Timer timer_ShortMonitor = new System.Windows.Forms.Timer();//短时间监控
 
        //bool 
        #endregion  定义

        #region 初始化
        //事件注册
        public void LoginEvent_Monitor()
        {
            try
            {
                this.timer_LongMonitor.Tick += new System.EventHandler(this.timer_LongMonitor_Tick);
                this.timer_ShortMonitor.Tick += new EventHandler(timer_ShortMonitor_Tick);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        void Init_Monitor()
        {
            try
            {
                EnableTimer_Invoke();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        //控件定时器
        void EnableTimer_Invoke()
        {
            try
            {
                Action inst = new Action(EnableTimer);
                this.Dispatcher.Invoke(inst);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        void EnableTimer()
        {
            try
            {
                //长时间监控
                this.timer_LongMonitor.Interval = 30000;//30S
                this.timer_LongMonitor.Enabled = true;
                //短时间监控
                this.timer_ShortMonitor.Interval = 200;
                this.timer_ShortMonitor.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }            
        }
        #endregion 初始化

        #region 系统监控
        /// <summary>
        /// 长时间监控,30s监控一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void timer_LongMonitor_Tick(object sender, EventArgs e)
        {
            try
            {
                FileMonitor();//监控文件夹删除
                MonitorLogout();//监控自动注销
                HardDisk();//存储空间监控
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 短时间监控,200ms循环一次
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_ShortMonitor_Tick(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 系统监控

        #region 监控文件夹
        /// <summary>
        /// 监控文件夹，判断是否删除多余的文件
        /// </summary>
        void FileMonitor()
        {
            try
            {
                FunDelFolder.F_I.DelteFolder(uCStateWork);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }       
        #endregion 监控文件夹

        #region 监控注销
        /// <summary>
        /// 自动退出
        /// </summary>
        void MonitorLogout()
        {
            try
            {
                if (Authority.Authority_e != Authority_enum.Null)
                {
                    time_Logout++;
                    double time = time_Logout * 0.05;//0.5分钟为一个计时单位
                    if (ParSetLogin.P_I.TimeLogout < time
                        && !RegeditLogin.R_I.BlManufacturer)
                    {
                        Logout_Invoke();//退出登录
                        uCStateWork.AddInfo("自动退出登录");
                    }
                }
                else
                {
                    time_Logout = 0;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 监控注销

        #region 监控硬盘容量
        /// <summary>
        /// 当硬盘容量不足时报警
        /// </summary>
        public void HardDisk()
        {
            try
            {
                double[] space = FunHarDisk.F_I.GetSpace(ParHardDisk.P_I.NameDrive);
                if (space[1] < ParHardDisk.P_I.SpaceMin)
                {
                    ShowWinError_Invoke(string.Format("硬盘存储空间小于设置值{0}G，\n请删除多余文件（主要是图片）!", ParHardDisk.P_I.SpaceMin.ToString()));
                }                
            }
            catch (Exception ex)
            {
                 Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 监控硬盘容量
    }
}
