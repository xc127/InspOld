using System;
using System.Threading.Tasks;
using DealPLC;
using DealRobot;
using System.Threading;
using BasicClass;
using DealComprehensive;
using DealMontionCtrler;
using DealCIM;
using DealConfigFile;

namespace Main
{
    partial class MainWindow
    {
        #region 定义
        bool g_blRobotShake = false;//机器人握手

        bool g_blRobotNullRun = false;

        bool BlRobotToSafe = false;//通知机器人去安全位置

        bool BlPreseisePlatWaiting = false;//PLC通知精确定位平台正在等待机器人取料

        #endregion 定义

        #region 初始化
        //事件注册
        void LoginEvent_Robot()
        {
            try
            {
                //如果没有机器人通信
                if (ParSetRobot.P_I.TypeRobot_e == TypeRobot_enum.Null)
                {
                    return;
                }
                LogicRobot.L_I.StateRobotInterface_event += new StrBlAction(L_I_StateRobotInterface_event);
                //配置参数
                LogicRobot.L_I.ConfigRobot_event += new StrBlAction(LogicRobot_Inst_ConfigRobot_event);

                //数据反馈
                LogicRobot.L_I.Shakehand_event += new Action(L_I_Shakehand_event);
                LogicRobot.L_I.FeedBackOK_event += new StrAction(R_Inst_FeedBackOK_event);
                LogicRobot.L_I.FeedBackNG_event += new StrAction(R_Inst_FeedBackNG_event);
                LogicRobot.L_I.RobotReset_event += new IntAction(L_I_RobotReset_event);
                LogicRobot.L_I.RobotHome_event += new IntAction(L_I_RobotHome_event);
                LogicRobot.L_I.RobotThrow_event += new IntAction(L_I_RobotThrow_event);
                LogicRobot.L_I.Monitor_event += new StrAction(L_I_Monitor_event);
                //LogicRobot.L_I.RobotCode_event += new Action(L_I_Code_event);

                #region 相机综合处理
                LogicRobot.L_I.Camera1_event += new TrrigerSourceAction_del(DealComprehensive_Camera1_event);
                LogicRobot.L_I.Camera2_event += new TrrigerSourceAction_del(DealComprehensive_Camera2_event);
                LogicRobot.L_I.Camera3_event += new TrrigerSourceAction_del(DealComprehensive_Camera3_event);
                LogicRobot.L_I.Camera4_event += new TrrigerSourceAction_del(DealComprehensive_Camera4_event);
                LogicRobot.L_I.Camera5_event += new TrrigerSourceAction_del(DealComprehensive_Camera5_event);
                LogicRobot.L_I.Camera6_event += new TrrigerSourceAction_del(DealComprehensive_Camera6_event);

                LogicRobot.L_I.Camera1_index_event += new TrrigerSourceIntAction_del(DealComprehensive_Camera1_event);
                LogicRobot.L_I.Camera2_index_event += new TrrigerSourceIntAction_del(DealComprehensive_Camera2_event);
                LogicRobot.L_I.Camera3_index_event += new TrrigerSourceIntAction_del(DealComprehensive_Camera3_event);
                LogicRobot.L_I.Camera4_index_event += new TrrigerSourceIntAction_del(DealComprehensive_Camera4_event);
                LogicRobot.L_I.Camera5_index_event += new TrrigerSourceIntAction_del(DealComprehensive_Camera5_event);
                LogicRobot.L_I.Camera6_index_event += new TrrigerSourceIntAction_del(DealComprehensive_Camera6_event);

                LogicRobot.L_I.Camera1_IndexN_event += new TrrigerSourceIntNAction_del(DealComprehensive_Camera1_event);
                LogicRobot.L_I.Camera2_IndexN_event += new TrrigerSourceIntNAction_del(DealComprehensive_Camera2_event);
                LogicRobot.L_I.Camera3_IndexN_event += new TrrigerSourceIntNAction_del(DealComprehensive_Camera3_event);
                LogicRobot.L_I.Camera4_IndexN_event += new TrrigerSourceIntNAction_del(DealComprehensive_Camera4_event);
                LogicRobot.L_I.Camera5_IndexN_event += new TrrigerSourceIntNAction_del(DealComprehensive_Camera5_event);
                LogicRobot.L_I.Camera6_IndexN_event += new TrrigerSourceIntNAction_del(DealComprehensive_Camera6_event);


                #endregion 相机综合处理

                #region 数据超限报警
                LogicRobot.L_I.DataError_event += new StrAction(RobotLCamera1_Inst_DataError_event);
                #endregion 数据超限报警

                #region Others
                LogicRobot.L_I.Others_event += new IntAction(R_Inst_Others_event);
                #endregion Others
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 初始化机器人
        /// </summary>       
        void Init_Robot()
        {
            try
            {
                if (RegeditRobot.R_I.BlOffLineRobot)
                {
                    return;
                }
                //如果没有机器人通信
                if (ParSetRobot.P_I.TypeRobot_e == TypeRobot_enum.Null)
                {
                    this.Dispatcher.Invoke(new BoolAction(EnableRobotCtr), false);
                    return;
                }
                //读取机器人标准规划
                RobotStdPoint.R_I.ReadXml();

                switch (ParLogicRobot.P_I.StatePortRobot_e)
                {
                    case StatePortRobot_enum.AllTrue:
                        ShowState("机器人通信成功！");
                        //机器人握手
                        Task task = new Task(RobotShake);
                        task.Start();
                        break;

                    case StatePortRobot_enum.AllError:
                        //显示报警窗口
                        ShowWinError_Invoke("机器人通信失败！");
                        break;

                    case StatePortRobot_enum.Wait:
                        ShowAlarm("等待机器人连接通信");
                        break;
                }
                //自动连接机器人
                if (ParSetRobot.P_I.BlAutoConnect)
                {
                    RobotReStart();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        void EnableRobotCtr(bool enable)
        {
            try
            {
                cmiManualRobot.IsEnabled = enable;
                cmiRestartRobot.IsEnabled = enable;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 初始化

        #region 机器人复位重启
        /// <summary>
        /// 重启机器人通信
        /// </summary>
        void RobotReStart()
        {
            try
            {
                //Thread.Sleep(500);
                ShowState("开始重启PC机器人通信！");
                //关闭数据接收和数据打开按钮         
                ShowAlarm("开始关闭机器人通信");
                if (!LogicRobot.L_I.CloseRobot())
                {
                    ShowWinError_Invoke("关闭机器人通信失败！");
                    return;
                }

                ShowState("关闭机器人通信成功！");

                Thread.Sleep(200);
                ShowAlarm("等待机器人和PC连接");
                LogicRobot.L_I.OpenInterface();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 机器人复位重启

        #region 机器人通信状态
        /// <summary>
        /// 机器人接口状态
        /// </summary>
        /// <param name="str"></param>
        void L_I_StateRobotInterface_event(string str, bool blResult)
        {
            try
            {
                if (blResult)
                {
                    ParLogicRobot.P_I.StatePortRobot_e = StatePortRobot_enum.AllTrue;
                    ShowState("机器人通信成功");

                    //机器人握手
                    Task task = new Task(RobotShake);
                    task.Start();
                }
                else
                {
                    ParLogicRobot.P_I.StatePortRobot_e = StatePortRobot_enum.AllError;
                    ShowAlarm("机器人通信失败");
                    //显示报警窗口
                    ShowWinError_Invoke("机器人通信失败！");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 机器人通信状态

        #region 机器人握手
        void RobotShake()
        {
            try
            {
                ShowState("机器人开始握手！");
                Thread.Sleep(300);
                g_blRobotShake = false;
                LogicRobot.L_I.RobotShake();
                int i = 0;
                while (!g_blRobotShake)
                {
                    Thread.Sleep(400);
                    i++;
                    if (i > 10)
                    {
                        break;
                    }
                }

                if (!g_blRobotShake)
                {
                    ShowAlarm("机器人握手失败");
                    //LogicPLC.L_I.PCConnectRobotNG();
                }
                else
                {
                    ShowState("机器人握手成功！");
                    //LogicPLC.L_I.PCConnectRobotOK();

                    DealCustomHand();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 机器人启动

        #region 配置参数
        /// <summary>
        /// 给机器人发送配置参数
        /// </summary>
        public void ConfigRobot_Task()
        {
            try
            {
                if (ParSetRobot.P_I.TypeRobot_e == TypeRobot_enum.Null)
                {
                    return;
                }
                if (RegeditRobot.R_I.BlOffLineRobot)
                {
                    return;
                }
                #region 清空旧的参数
                LogicRobot.L_I.ParRobotCom_L.Clear();
                LogicRobot.L_I.ParRobot1_L.Clear();
                LogicRobot.L_I.ParRobot2_L.Clear();
                LogicRobot.L_I.ParRobot3_L.Clear();
                LogicRobot.L_I.ParRobot4_L.Clear();

                LogicRobot.L_I.ParRobotCom_P4L.Clear();
                LogicRobot.L_I.ParRobot1_P4L.Clear();
                LogicRobot.L_I.ParRobot2_P4L.Clear();
                LogicRobot.L_I.ParRobot3_P4L.Clear();
                LogicRobot.L_I.ParRobot4_P4L.Clear();
                #endregion 清空旧的参数

                ShowState("发送插栏交接X：" + MainParProduct.M_I.YTransInsert);
                LogicRobot.L_I.ParRobotCom_P4L.Add(new Point4D(MainParProduct.M_I.YTransInsert, 0, 0, 0));//500 插栏交接基准

                ShowState("发送抛料角度：" + MainParProduct.M_I.AngleBelt);
                LogicRobot.L_I.ParRobotCom_P4L.Add(new Point4D(0, 0, 0, MainParProduct.M_I.AngleBelt));//500 插栏交接基准

                //发送参数
                Task task = new Task(LogicRobot.L_I.WriteConfigRobot);
                task.Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 配置参数

        #region 设置机器人处于空跑状态
        void SetRobotNullRun()
        {
            try
            {
                //如果没有机器人通信
                if (ParSetRobot.P_I.TypeRobot_e == TypeRobot_enum.Null)
                {
                    return;
                }

                if (MainCom.M_I.StateMachine_e == StateMachine_enum.NullRun)
                {
                    LogicRobot.L_I.RobotNullRun(true);
                    g_blRobotNullRun = true;
                    ShowState("机器人进入空跑模式");
                }
                else if (g_blRobotNullRun)
                {
                    LogicRobot.L_I.RobotNullRun(false);
                    ShowState("机器人退出空跑模式");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 设置机器人处于空跑状态

        #region 接收数据结果反馈
        void L_I_Shakehand_event()
        {
            try
            {
                g_blRobotShake = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        void R_Inst_FeedBackOK_event(string cmd)
        {
            ShowState("机器人接收指令：" + cmd + "成功！");
        }
        void R_Inst_FeedBackNG_event(string cmd)
        {
            try
            {
                ShowAlarm("机器人接收指令：" + cmd + "异常！请重新启动机器人");
                //LogicPLC.L_I.PCAlarm();
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 机器人监控
        /// </summary>
        /// <param name="str"></param>
        void L_I_Monitor_event(string str)
        {
            try
            {
                ShowAlarm(str);
                ShowWinError_Invoke(str);
                LogicPLC.L_I.PCAlarm();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        void L_I_Code_event()
        {
            try
            {
                if (ParSetWork.P_I[0].BlSelect)
                {
                    Thread.Sleep(100);
                    //Code.C_I.Write();
                }
                else
                {
                    LogicRobotCam3.L_I.WriteData2(new Point4D(1, 0, 0, 0));
                    LogicRobotCam3.L_I.WriteData1(new Point4D(1, 0, 0, 0));
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        //相机数据超限
        void RobotLCamera1_Inst_DataError_event(string str)
        {
            try
            {
                ShowWinError_Invoke("数据超限:" + str);
                LogicPLC.L_I.PCAlarm();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 接收数据结果反馈

        #region 机器人HomeThrow
        void L_I_RobotReset_event(int i)
        {
            try
            {
                ShowState("机器人复位完成");
                MainCom.M_I.ResetRobot = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 机器人回到Home点
        /// </summary>
        /// <param name="i"></param>
        void L_I_RobotHome_event(int i)
        {
            try
            {
                ShowState("机器人回到Home点");
                MainCom.M_I.HomeRobot = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 抛料
        /// </summary>
        /// <param name="i"></param>
        void L_I_RobotThrow_event(int i)
        {
            try
            {
                ShowState("机器人进行抛料");
                MainCom.M_I.HomeRobot = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 机器人HomeThrow

        #region 板卡控制重启
        void ResetRobot()
        {
            PauseRobot();
            ShowState("重启机器人");
            LogicMotionCtler.L_I.OffOutBit(1, 1);
            Thread.Sleep(200);
            //shineng-->reset-->pgreset-->start
            LogicMotionCtler.L_I.OnOutBit(1, 0);
            LogicMotionCtler.L_I.OnOutBit(1, 1);
            LogicMotionCtler.L_I.OnOutBitReset(1, 3, 200);

            LogicMotionCtler.L_I.OnOutBitReset(1, 4, 200);

            LogicMotionCtler.L_I.OnOutBitReset(1, 2, 200);
            ShowState("重启机器人完成");

            LogicRobot.L_I.OpenInterface();
        }
        /// <summary>
        /// 启动机器人
        /// </summary>
        void AutoRunRobot()
        {
            LogicMotionCtler.L_I.OnOutBit(1, 6);
        }
        /// <summary>
        /// 暂停机器人
        /// </summary>
        void PauseRobot()
        {
            LogicMotionCtler.L_I.OffOutBit(1, 6);
        }

        #endregion

        #region 信息显示
        /// <summary>
        /// 机器人配置参数反馈情况
        /// </summary>
        /// <param name="str"></param>
        /// <param name="result"></param>
        void LogicRobot_Inst_ConfigRobot_event(string str, bool result)
        {
            if (result)
            {
                ShowState(str);
            }
            else
            {
                ShowWinError_Invoke(str);
            }
        }
        #endregion 信息显示

        #region 关闭机器人
        /// <summary>
        /// 关闭机器人
        /// </summary>
        void Close_Robot()
        {
            try
            {
                if (ParSetRobot.P_I.TypeRobot_e != TypeRobot_enum.Null)
                {
                    LogicRobot.L_I.CloseRobot();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        void EventLogout_Robot()
        {
            try
            {
                //如果没有机器人通信
                if (ParSetRobot.P_I.TypeRobot_e == TypeRobot_enum.Null)
                {
                    return;
                }
                //配置参数
                LogicRobot.L_I.ConfigRobot_event -= new StrBlAction(LogicRobot_Inst_ConfigRobot_event);

                //数据反馈
                LogicRobot.L_I.FeedBackOK_event -= new StrAction(R_Inst_FeedBackOK_event);
                LogicRobot.L_I.FeedBackNG_event -= new StrAction(R_Inst_FeedBackNG_event);

                #region 相机
                LogicRobot.L_I.Camera1_event -= new TrrigerSourceAction_del(DealComprehensive_Camera1_event);
                LogicRobot.L_I.Camera2_event -= new TrrigerSourceAction_del(DealComprehensive_Camera2_event);
                LogicRobot.L_I.Camera3_event -= new TrrigerSourceAction_del(DealComprehensive_Camera3_event);
                LogicRobot.L_I.Camera4_event -= new TrrigerSourceAction_del(DealComprehensive_Camera4_event);
                LogicRobot.L_I.Camera5_event -= new TrrigerSourceAction_del(DealComprehensive_Camera5_event);
                LogicRobot.L_I.Camera6_event -= new TrrigerSourceAction_del(DealComprehensive_Camera6_event);
                #endregion 相机

                #region 数据超限报警
                LogicRobot.L_I.DataError_event -= new StrAction(RobotLCamera1_Inst_DataError_event);
                #endregion 数据超限报警
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 关闭机器人
    }
}
