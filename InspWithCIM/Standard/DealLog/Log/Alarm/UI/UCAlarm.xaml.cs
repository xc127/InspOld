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
using System.Threading;
using System.Threading.Tasks;
using BasicClass;
using Common;
using DealFile;
using System.IO;
using System.Xml;
using System.Reflection;

namespace DealLog
{
    /// <summary>
    /// UIError.xaml 的交互逻辑
    /// </summary>
    public partial class UCAlarm : BaseControl
    {
        #region 定义
        //bool 
        bool blChange = false;//运行状态发生变化

        //Mutex
        Mutex mutex = new Mutex();
        #endregion 定义

        #region 初始化
        public UCAlarm()
        {
            InitializeComponent();

            NameClass = "UCAlarm";
        }
        #endregion 初始化
        
        #region 更新信息
        public void AddInfo(string info)
        {
            mutex.WaitOne();
            try
            {
                FunLogAlarm.F_I.AddInfo(info);
                blChange = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            mutex.ReleaseMutex();
        }
        /// <summary>
        /// 添加信息,英文20190424
        /// </summary>
        /// <param name="info">日志信息</param>
        /// <param name="blAlarm">是否是报警信息</param>
        public void AddInfo_EN(string info)
        {
            mutex.WaitOne();
            try
            {
                
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            mutex.ReleaseMutex();
        }


        /// <summary>
        /// 添加信息,繁体20191123
        /// </summary>
        /// <param name="info">日志信息</param>
        /// <param name="blAlarm">是否是报警信息</param>
        public void AddInfo_FT(string info)
        {
            mutex.WaitOne();
            try
            {
              
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            mutex.ReleaseMutex();
        }
        //清空
        private void lblClearAlarm_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Authority.Authority_e == Authority_enum.Null)
            {
                //lblClearAlarm.RefreshLB("无权限", false);
                return;
            }

            mutex.WaitOne();
            try
            {
                FunLogAlarm.F_I.ClearInfo();
                blChange = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            mutex.ReleaseMutex();
        }        
        #endregion 更新信息

        #region 查看本地日志
        private void lblOpenFolder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", FunLogAlarm.F_I.PathAlarmHour);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 查看本地日志

        #region 显示
        public override void ShowPar_Invoke()
        {
            mutex.WaitOne();
            try
            {
                if (blChange)
                {
                    blChange = false;
                    this.Dispatcher.Invoke(new Action(ShowPar));
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            mutex.ReleaseMutex();
        }
        //显示参数
        public override void ShowPar()
        {
            try
            {
                RefreshDgAlarm();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //刷新产品参数
        void RefreshDgAlarm()
        {
            try
            {              
                //重新建立list防止主界面闪退
                List<AlarmInfo> alarmInfo_L = new List<AlarmInfo>();
                for (int i = 0; i < FunLogAlarm.F_I.g_AlarmInfo_L.Count; i++)
                {
                    AlarmInfo alarmInfo = new AlarmInfo();
                    alarmInfo.No = FunLogAlarm.F_I.g_AlarmInfo_L[i].No;

                    if (FunLogAlarm.F_I.g_AlarmInfo_L[i].Alarm != null)
                    {
                        alarmInfo.Alarm = FunLogAlarm.F_I.g_AlarmInfo_L[i].Alarm;
                    }
                    else
                    {
                        alarmInfo.Alarm = "Null";
                    }
                    if (FunLogAlarm.F_I.g_AlarmInfo_L[i].Time != null)
                    {
                        alarmInfo.Time = FunLogAlarm.F_I.g_AlarmInfo_L[i].Time;
                    }
                    else
                    {
                        alarmInfo.Time = "Null";
                    }
                    if (alarmInfo != null)
                    {
                        alarmInfo_L.Add(alarmInfo);
                    }
                }

                dgAlarm.ItemsSource = alarmInfo_L;
                dgAlarm.Items.Refresh();
                if (FunLogAlarm.F_I.g_AlarmInfo_L.Count > 0)
                {
                    dgAlarm.ScrollIntoView(alarmInfo_L[alarmInfo_L.Count - 1], dgAlarm.Columns[0]);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

       
    }
    
}
