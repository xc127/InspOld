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
using DealFile;
using System.IO;
using System.Xml;
using System.Reflection;
using Common;

namespace DealLog
{
    /// <summary>
    /// UIInfoWork.xaml 的交互逻辑
    /// </summary>
    public partial class UCStateWork : BaseControl
    {
        #region 定义      
        //bool 
        bool blChange = false;//运行状态发生变化

        //Mutex
        Mutex mutex = new Mutex();
        #endregion 定义

        #region 初始化
        public UCStateWork()
        {
            InitializeComponent();
        }
        #endregion 初始化

        #region 增加信息
        public void AddInfo(string info)
        {
            mutex.WaitOne();
            try
            {
                FunLogState.F_I.AddInfo(info);
                blChange = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCStateWork", ex);
            }
            mutex.ReleaseMutex();
        }

        public void AddInfo(string info, bool blShow)
        {
            mutex.WaitOne();
            try
            {
                FunLogState.F_I.AddInfo(info, blShow);
                blChange = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCStateWork", ex);
            }
            mutex.ReleaseMutex();
        }

        /// <summary>
        /// 添加信息,英文20190424
        /// </summary>
        /// <param name="info">日志信息</param>
        /// <param name="blAlarm">是否是报警信息</param>
        public void AddInfo_EN(string info, bool blAlarm = false)
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
        public void AddInfo_FT(string info, bool blAlarm = false)
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
        #endregion 增加信息      

        #region 清空
        private void lblClear_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (Authority.Authority_e == Authority_enum.Null)
            {
                //lblClear.RefreshLB("无权限", false);
                return;
            }

            mutex.WaitOne();
            try
            {
                FunLogState.F_I.ClearInfo();
                blChange = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCStateWork", ex);
            }
            mutex.ReleaseMutex();

        }
        #endregion 清空

        #region 查看本地日志
        private void lblOpenFolder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("explorer.exe", FunLogState.F_I.PathStateHour);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCStateWork", ex);
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
                Log.L_I.WriteError("UCStateWork", ex);
            }
            mutex.ReleaseMutex();
        }
        //显示参数
        public override void ShowPar()
        {
            try
            {
                RefreshDgState();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCStateWork", ex);
            }
        }

        //刷新产品参数
        void RefreshDgState()
        {
            try
            {
                //重新建立list防止主界面闪退
                List<StateInfo> stateInfo_L = new List<StateInfo>();
                for (int i = 0; i < FunLogState.F_I.g_StateInfo_L.Count; i++)
                {
                    StateInfo stateInfo = new StateInfo();
                    if (FunLogState.F_I.g_StateInfo_L[i].State != null)
                    {
                        stateInfo.State = FunLogState.F_I.g_StateInfo_L[i].State;
                    }
                    else
                    {
                        stateInfo.State = "Null";
                    }

                    if (FunLogState.F_I.g_StateInfo_L[i].Time != null)
                    {
                        stateInfo.Time = FunLogState.F_I.g_StateInfo_L[i].Time;
                    }
                    else
                    {
                        stateInfo.Time = "Null";
                    }
                    if (stateInfo != null)
                    {
                        stateInfo_L.Add(stateInfo);
                    }
                }

                dgState.ItemsSource = stateInfo_L;
                dgState.Items.Refresh();
                if (FunLogState.F_I.g_StateInfo_L.Count > 0)
                {
                    dgState.ScrollIntoView(stateInfo_L[stateInfo_L.Count - 1], dgState.Columns[0]);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCStateWork", ex);
            }
        }
        #endregion 显示        


    }


}
