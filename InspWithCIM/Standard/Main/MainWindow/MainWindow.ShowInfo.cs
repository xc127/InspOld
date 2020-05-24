#define CIM

using BasicClass;
using DealLog;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Main
{
    partial class MainWindow
    {
        #region 定义
        int NumStateRun = 0;
        //Timer
        System.Windows.Forms.Timer timer_Refresh = new System.Windows.Forms.Timer();//刷新定时器
        #endregion 定义

        #region 初始化
        void LoginEvent_ShowInfo()
        {
            try
            {
                this.timer_Refresh.Tick += new EventHandler(timer_Refresh_Tick);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        void Init_ShowInfo()
        {
            //产品参数初始化
            uIParProduct.Init();

            Action inst = new Action(InitTimer_ShowInfo);
            this.Dispatcher.Invoke(inst);
        }
        void InitTimer_ShowInfo()
        {
            try
            {
                timer_Refresh.Interval = 300;
                timer_Refresh.Enabled = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UIInfoWork", ex);
            }
        }
        #endregion 初始化    

        #region 产品参数      
        /// <summary>
        /// 显示换型之后的型号名称
        /// </summary>
        void ShowParProduct()
        {
            try
            {
                uIParProduct.ShowModelName_Invoke();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion  产品参数

        #region 定时器刷新
        void timer_Refresh_Tick(object sender, EventArgs e)
        {
            try
            {
                uCStateWork.ShowPar_Invoke();//显示运行状态
                uCAlarm.ShowPar_Invoke();//显示报警信息
                uCResult.ShowPar_Invoke();//显示结果数据

                ucSingleRecord.DGRefresh_Invoke();

                #region 指示软件运行状态
                NumStateRun++;
                if (NumStateRun == 8)
                {
                    ShowStateRun(Visibility.Hidden);
                }
                else if (NumStateRun == 16)
                {
                    ShowStateRun(Visibility.Visible);
                    NumStateRun = 0;
                }
                #endregion 指示软件运行状态
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 定时器刷新

        #region 软件运行状态
        void ShowStateRun(Visibility visible)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                    {
                        lblStateRun.Visibility = visible;
                    }));

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 软件运行状态

        #region State
        void ShowState(string info)
        {
            uCStateWork.AddInfo(info);
        }
        #endregion State

        #region Alarm
        /// <summary>
        /// 将报警信息显示出来，同时显示在状态区域
        /// </summary>
        /// <param name="alarm"></param>
        void ShowAlarm(string alarm)
        {
            uCAlarm.AddInfo(alarm);
            ShowState(alarm);
        }
        #endregion Alarm

        #region CIM状态栏更新接口
        public static void SetArmChipID(string content)
        {
            g_UCCimStatus?.SetlblArmChipID(content);
        }

        public static void SetForkChipID(string content)
        {
            g_UCCimStatus?.SetlblForkChipID(content);
        }

        public static void SetChipIDResult(string content, bool isAlarm)
        {
            ShowMsg(content, isAlarm);
            g_UCCimStatus?.SetlblChipIDResult(content, isAlarm);
        }

        public static void SetLotResult(string content, bool isAlarm)
        {
            ShowMsg(content, isAlarm);
            g_UCCimStatus?.SetlblLotResult(content, isAlarm);
        }

        public static void SetTrackOutResult(string content, bool isAlarm)
        {
            ShowMsg(content, isAlarm);
            g_UCCimStatus?.SetlblTrackOutResult(content, isAlarm);
        }

        public static void SetCimStatus(string content, bool isAlarm)
        {
            ShowMsg(content, isAlarm);
            g_UCCimStatus?.SetCimStatus(content, isAlarm);
        }

        public static void SetCodeStatus(string content, bool isAlarm)
        {
            ShowMsg(content, isAlarm);
            g_UCCimStatus?.SetCodeStatus(content, isAlarm);
        }

        public static void SetLot()
        {
            g_UCCimStatus?.SetlblLot();
        }

        public static void ShowMsg(string content, bool isAlarm)
        {
            if (isAlarm)
            {
                ShowState_static(content);
                ShowAlarm_static(content);
            }
            else
                ShowState_static(content);
        }

        public static void ShowState_static(string msg)
        {
            g_UCState.AddInfo(msg);
        }

        public static void ShowAlarm_static(string msg)
        {
            g_UCAlarm.AddInfo(msg);
        }
        #endregion

        #region Custom

        #endregion Custom

        #region 显示基方法
        void ShowLabel_Invoke(Label lbl, string str, string color)
        {
            try
            {
                LabelStr2Action inst = new LabelStr2Action(ShowLabel);
                this.Dispatcher.Invoke(inst, lbl, str, color);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void ShowLabel(Label lbl, string str, string strColor)
        {
            try
            {
                if (strColor == "red")
                {
                    lbl.Foreground = Brushes.Red;
                }
                else
                {
                    lbl.Foreground = Brushes.Blue;
                }
                lbl.Content = str;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void ShowLabel_Invoke(Label lbl, string str)
        {
            try
            {
                LabelStrAction inst = new LabelStrAction(ShowLabel);
                this.Dispatcher.Invoke(inst, lbl, str);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void ShowLabel(Label lbl, string str)
        {
            try
            {
                lbl.Foreground = Brushes.Blue;
                lbl.Content = str;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示基方法

        #region WinError
        /// <summary>
        /// 显示报警窗体
        /// </summary>
        /// <param name="str"></param>
        void ShowWinError_Invoke(string str)
        {
            try
            {
                this.Dispatcher.Invoke(new StrAction(ShowWinError), str);
            }
            catch (Exception ex)
            {

            }
        }
        void ShowWinError(string str)
        {
            try
            {
                ShowAlarm(str);
                WinError.GetWinInst().ShowError(str);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion WinError
    }
}
