using System;
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
using DealFile;
using SetPar;
using DealPLC;
using DealConfigFile;
using BasicClass;
using DealMontionCtrler;
using DealLog;
using System.Collections;
using DealCIM;

namespace Main
{
    partial class MainWindow
    {
        #region 定义
        UIShield uiShield = new UIShield();
        object o_Locker1 = new object();
        object o_Locker2 = new object();

        #region 灯
        //Light g_LtLoad = null;
        //Light g_LtStart = null;
        //Light g_LtPause = null;
        Light g_LtGreen = null;
        Light g_LtYellow = null;
        Light g_LtRed = null;
        #endregion

        #region 气缸
        AirCylinderDblCtl g_AirCyliderUpDown = null;

        #endregion

        #region 逻辑
        int g_OldLogicStatue1 = 1;
        int g_OldLogicStatue2 = 1;
        int g_OldLogicStatue3 = 1;
        /// <summary>
        /// 1
        /// </summary>
        int logicStep1 = 1;
        /// <summary>
        /// 模块1逻辑控制
        /// </summary>
        int LogicStep1
        {
            get { return logicStep1; }
            set
            {
                logicStep1 = value;
                ShowState("Step1:" + LogicStep1);
            }
        }

        int logicStep2 = 1;
        /// <summary>
        /// 模块2逻辑控制
        /// </summary>
        int LogicStep2
        {
            get { return logicStep2; }
            set
            {
                logicStep2 = value;
                ShowState("Step2:" + LogicStep2);
            }
        }

        int logicStep3 = 1;
        /// <summary>
        /// 模块3逻辑控制
        /// </summary>
        int LogicStep3
        {
            get { return logicStep3; }
            set
            {
                logicStep3 = value;
                ShowState("Step3:" + LogicStep3);
            }
        }

        #endregion

        #region 标志位
        bool g_BlRobotArrive = false;
        bool g_BlRobotLeave = false;

        bool g_BlPlateVacoo1_1OK = false;
        bool g_BlPlateVacoo1_2OK = false;
        bool g_BlPlate1Ready = false;

        bool g_BlTransVacoo1OK = false;
        bool g_BlTransVacoo2OK = false;
        bool g_BlTransVacooAllOK = false;
        bool g_BlCanTransLeave = false;
        bool g_BlTrans1Finished = false;
        bool g_BlTrans2Arrive = false;
        bool g_BlTrans2Finished = false;

        bool g_BlPlate2Ready = false;
        bool g_BlPlateVacoo2_1OK = false;
        bool g_BlPlateVacoo2_2OK = false;
        bool g_BlPlateVacoo2AllOK = false;
        bool g_BlDownStreamArrive1 = false;
        bool g_BlDownStreamArrive2 = false;
        bool g_BlDownStreamTransError1 = false;
        bool g_BlDownStreamTransError2 = false;

        bool unLoad1 = false;
        bool unLoad2 = false;//两个标志着完成

        bool g_BlDownStreamLeave = false;
        //安全
        public bool IsAllSafe
        {
            get { return (door1Safe && door2Safe && door3Safe && door4Safe && grating1Safe && grating2Safe) || ParSetWork.P_I[8].BlSelect; }
        }
        #endregion

        string g_StrAlarmInfo = "";
        #endregion

        #region 初始化
        void InitMotionCtler()
        {
            if (!ParMotionCtrler.P_I.IsEnable)
            {
                return;
            }
            //包括初始化IO模块
            LogicMotionCtler.L_I.InitModule();
            DmcResult result = LogicMotionCtler.L_I.Dmc_card_init();

            //板卡初始化之后初始化实deng例
            InitLight();
            InitAirCylider();

            if (result.resultType == ResultType.OK)
            {
                ParMotionCtrler.P_I.IsInitialized = true;

                if (WinMsgBox.ShowMsgBox("开始按照既定的顺序\n执行回零？"))
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        this.gdRoot.Children.Add(uiShield);
                    }));

                    ShowState("打开气缸，等待气缸到达限位");
                    g_AirCyliderUpDown.OnAirCylinder(4000);
                    ShowState("气缸已打开，开始回零");


                    LogicMotionCtler.L_I.Home();
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        this.gdRoot.Children.Remove(uiShield);
                    }));

                    ShowState("回零动作结束，已回到初始位！");
                }
                else
                {
                    ShowState("已拒绝回零！");
                }
            }
            else
            {
                WinError.GetWinInst().ShowError("控制卡初始化失败！\n" + result.resultInfo);
            };
        }

        void InitLight()
        {
            g_LtRed = new Light("红灯", 2, 6);
            g_LtYellow = new Light("黄灯", 2, 7);
            g_LtGreen = new Light("绿灯", 2, 8);
        }

        void InitAirCylider()
        {
            g_AirCyliderUpDown = new AirCylinderDblCtl("上下取料气缸", 0, 0, 0, 1);
        }
        #endregion

        #region 控制按钮

        #endregion

        #region 运行逻辑
        private void StartCycle()
        {
            if (g_NumErrorAxis != 0)
            {
                ShowWinError_Invoke("轴报错中，无法执行Cycle\r\n请检查轴状态！");
                return;
            }
            if (!IsAllSafe)
            {
                ShowWinError_Invoke("安全装置异常，请检查！");
                return;
            }

            ShowState("可以启动自动流程，于Step" + LogicStep1);

            MainCom.M_I.g_IsCycling = true;

            #region 准备工作
            StopBuzzShort();

            EnableUI();

            g_LtRed.CloseLight();
            g_LtYellow.CloseLight();
            g_LtGreen.OpenLight();

            AutoRunRobot();
            #endregion

            LogicStep1 = g_OldLogicStatue1;
            LogicStep2 = g_OldLogicStatue2;
            LogicStep3 = g_OldLogicStatue3;

            #region Module1
            new Task(delegate()
            {
                try
                {
                    while (MainCom.M_I.g_IsCycling)
                    {
                        g_OldLogicStatue1 = LogicStep1;
                        switch (LogicStep1)
                        {

                            default:
                                break;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }).Start();
            #endregion

            #region Module2
            new Task(delegate()
            {
                try
                {
                    while (MainCom.M_I.g_IsCycling)
                    {
                        g_OldLogicStatue2 = LogicStep2;
                        switch (LogicStep2)
                        {

                            default:
                                break;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }).Start();
            #endregion

            #region Module3
            new Task(delegate()
            {
                try
                {
                    while (MainCom.M_I.g_IsCycling)
                    {
                        g_OldLogicStatue3 = LogicStep3;
                        switch (LogicStep3)
                        {

                            default:
                                break;
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }).Start();
            #endregion

        }

        #region Module1逻辑

        #endregion

        #region Module2逻辑

        #endregion

        #region Module3逻辑

        #endregion

        void EnableUI()
        {
            this.Dispatcher.Invoke(new Action(delegate()
            {

            }));
        }

        public void PauseMachine()
        {
            MainCom.M_I.g_IsCycling = false;
            EnableUI();
            g_LtYellow.OpenLight();
            g_LtGreen.CloseLight();
            PauseRobot();
        }

        public void AlarmMachine(string str)
        {
            PauseMachine();
            StartBuzzShort();
            g_StrAlarmInfo = str;
            ShowWinError_Invoke(g_StrAlarmInfo);
            g_LtRed.Flash();
        }
        #endregion

        #region 蜂鸣
        public void StartBuzzLong()
        {

        }

        public void StartBuzzShort()
        {

        }

        public void StopBuzzLong()
        {

        }

        public void StopBuzzShort()
        {

        }
        #endregion

        #region 皮带线
        void RunBelt(int time)
        {
        }
        void RunBelt()
        {
        }
        void StopBelt()
        {
        }
        #endregion

        #region 关闭
        void CloseMotionCtrler()
        {
            try
            {
                if (ParMotionCtrler.P_I.IsEnable)
                {
                    LogicMotionCtler.L_I.g_BlCycRead = false;
                    PauseRobot();
                    Thread.Sleep(200);
                    LogicMotionCtler.L_I.Dmc_close_board();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion
    }
}
