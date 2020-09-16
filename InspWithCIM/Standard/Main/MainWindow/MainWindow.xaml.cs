using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Common;
using System.Threading;
using System.Threading.Tasks;
using Camera;
using DealRobot;
using DealPLC;
using System.IO;
using SetPar;
using SetComprehensive;
using DealConfigFile;
using BasicClass;
using DealComInterface;
using BasicDisplay;
using DealTool;
using DealLog;
using DealCIM;
using DealHelp;
using DealMontionCtrler;
using DealFile;
using System.Collections;
using FTPSDK;

namespace Main
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 定义
        string NameClass = "MainWindow";

        bool BlFinishInit = false;//完成初始化相机综合设置

        //完成初始化相机综合设置
        //完成初始化相机综合设置
        bool BlInitComprehensiveWin = false;
        bool BlInitComprehensiveTempWin = false;
        public static CIMStatus g_UCCimStatus = null;
        public static UCStateWork g_UCState = null;
        public static UCAlarm g_UCAlarm = null;

        BaseDealComprehensiveResult g_BaseDealComprehensiveResult = null;

        string UpdataInfo = "";
        string UserName = "";
        #endregion 定义

        #region 初始化
        /// <summary>
        /// 构造函数
        /// </summary>e
        public MainWindow()
        {
            InitializeComponent();


            //是否最大化窗体 
            MaxWinMain();
            //初始化尺寸
            InitWinSize();
            //事件注册
            Event_Init();

            if (!MainCom.M_I.IsDebugMode)
            {
                string path = @"./根目录.ini";
                if (File.Exists(path))
                {
                    string section = "DebugInfo";
                    UpdataInfo = IniFile.I_I.ReadIniStr(section, "UpdateTime", path);
                    UserName = IniFile.I_I.ReadIniStr(section, "UserName", path);
                    if (ParWorkInsp.P_I.CheckVisionWhenStart)
                    {
                        if (!WinMsgBox.ShowMsgBox(string.Format("当前版本软件更新时间：\r  {0}\r更新人员：{1}\r是否启用？", UpdataInfo, UserName)))
                        {
                            this.Close();
                        }
                    }
                }
                this.Title += string.Format("    更新时间：{0}   人员：{1}", UpdataInfo, UserName);
            }
        }

        #region 事件注册
        void Event_Init()
        {
            LoginEvent_MainWindow();
            LoginEvent_PLC();
            LoginEvent_Robot();
            LoginEvent_ComInterface();
            LoginEvent_Others();
            LoginEvent_ShowInfo();
            LoginEvent_CIM();

            LoginEvent_Camera();

            LoginMotionEvent();
        }
        //事件注册
        public void LoginEvent_MainWindow()
        {
            try
            {
                LoginEvent_Monitor();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 事件注册

        private void BaseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowState("软件启动");

            //设定默认登录权限
            SetDefaultLogin();

            //初始化主界面控件显示
            Init_UIMainShow();

            //创建相机图像显示窗口
            CreateUIDisplay();

            g_UCCimStatus = ucCIMStatus;
            g_UCState = uCStateWork;
            g_UCAlarm = uCAlarm;
            //初始化
            Task task = new Task(Init);
            task.Start();

            //图像处理初始化
            Init_ImageProcess();

            //指示是否处于离线状态
            ChangeColorOffLine();


        }

        /// <summary>
        /// 初始化主界面显示
        /// </summary>
        void Init_UIMainShow()
        {
            try
            {
                if (ParCameraWork.NumCamera < 4)
                {
                    RdbCam4.IsEnabled = false;
                }
                if (ParCameraWork.NumCamera < 3)
                {
                    RdbCam3.IsEnabled = false;
                }
                if (ParCameraWork.NumCamera < 2)
                {
                    RdbCam2.IsEnabled = false;
                }
                cmiCamera8.IsEnabled = false;
                cmiCamera7.IsEnabled = false;
                cmiCamera6.IsEnabled = false;
                cmiCamera5.IsEnabled = false;
                cmiCamera4.IsEnabled = false;
                cmiCamera3.IsEnabled = false;
                cmiCamera2.IsEnabled = false;
                cmiCamera1.IsEnabled = true;

                return;
                #region 相机综合设置
                if (ParCameraWork.NumCamera < 8)
                {
                    cmiCamera8.IsEnabled = false;
                }
                if (ParCameraWork.NumCamera < 7)
                {
                    cmiCamera7.IsEnabled = false;
                }
                if (ParCameraWork.NumCamera < 6)
                {
                    cmiCamera6.IsEnabled = false;
                }
                if (ParCameraWork.NumCamera < 5)
                {
                    cmiCamera5.IsEnabled = false;
                }
                if (ParCameraWork.NumCamera < 4)
                {
                    cmiCamera4.IsEnabled = false;
                }
                if (ParCameraWork.NumCamera < 3)
                {
                    cmiCamera3.IsEnabled = false;
                }
                if (ParCameraWork.NumCamera < 2)
                {
                    cmiCamera2.IsEnabled = false;
                }
                if (ParCameraWork.NumCamera < 1)
                {
                    cmiCamera1.IsEnabled = false;
                }
                #endregion 相机综合设置
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void Init()
        {
            //触发，处理，输出结果初始化
            Init_TrrigerDealResult();
            //初始化文件夹
            Init_DictionaryFiles();
            //初始化显示
            Init_ShowInfo();
            //Others
            Init_Others();
            //Custum
            Init_Custom();
            //监控
            Init_Monitor();
            //初始化显示
            Init_Display();

            //初始化相机
            Init_Camera();
            //IO模块
            Init_IO();

            //延迟
            Thread.Sleep(500);
            //打开相机综合设置窗体
            this.Dispatcher.Invoke(new Action(InitWinComprehensive));

            //初始化报警窗口
            this.Dispatcher.Invoke(new Action(() =>
                {
                    WinError.GetWinInst().BlInit = true;
                    WinError.GetWinInst().ShowError("");
                }));

            //通信
            Init_Communicate();

            //初始化CIM
            Init_CIM();
        }

        /// <summary>
        /// 通信
        /// </summary>
        void Init_Communicate()
        {
            //PLC 
            new Task(new Action(Init_PLC)).Start();
            //Robot
            new Task(new Action(Init_Robot)).Start();
            //ComInterface
            Init_ComInterface();
            //相机外触发监控
            //Init_ExCameraTrigger();


            //运控卡
            //new Task(InitMotionCtler).Start();
        }
        /// <summary>
        /// 初始化文件夹
        /// </summary>
        void Init_DictionaryFiles()
        {
            #region
            //存储文件夹
            if (!Directory.Exists(ComValue.c_PathStore))
            {
                Directory.CreateDirectory(ComValue.c_PathStore);
            }
            //参数文件夹
            if (!Directory.Exists(ComValue.c_PathPar))
            {
                Directory.CreateDirectory(ComValue.c_PathPar);
            }
            //软件运行记录
            if (!Directory.Exists(ComValue.c_PathRecord))
            {
                Directory.CreateDirectory(ComValue.c_PathRecord);
            }
            //相机文件夹
            if (!Directory.Exists(ComValue.c_PathCamera))
            {
                Directory.CreateDirectory(ComValue.c_PathCamera);
            }
            //Calib
            if (!Directory.Exists(ComValue.c_PathCalib))
            {
                Directory.CreateDirectory(ComValue.c_PathCalib);
            }
            //PLC
            if (!Directory.Exists(ComValue.c_PathPLC))
            {
                Directory.CreateDirectory(ComValue.c_PathPLC);
            }
            //Robot
            if (!Directory.Exists(ComValue.c_PathRobot))
            {
                Directory.CreateDirectory(ComValue.c_PathRobot);
            }

            //SetPar
            if (!Directory.Exists(ComValue.c_PathSetPar))
            {
                Directory.CreateDirectory(ComValue.c_PathSetPar);
            }
            //调整值
            if (!Directory.Exists(ComValue.c_PathAdjustStd))
            {
                Directory.CreateDirectory(ComValue.c_PathAdjustStd);
            }
            //系统路径初始化
            if (!Directory.Exists(ComValue.c_PathSysInit))
            {
                Directory.CreateDirectory(ComValue.c_PathSysInit);
            }

            //图片记录
            if (!Directory.Exists(ComValue.c_PathImageLog))
            {
                Directory.CreateDirectory(ComValue.c_PathImageLog);
            }
            //Custom
            if (!Directory.Exists(ComValue.c_PathCustom))
            {
                Directory.CreateDirectory(ComValue.c_PathCustom);
            }
            //Custom
            if (!Directory.Exists(ComValue.c_PathCustomLog))
            {
                Directory.CreateDirectory(ComValue.c_PathCustomLog);
            }
            #endregion
        }

        #region 窗体最大化
        public void maxBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Maximized;
                    RegeditMain.R_I.BlMaxWin = true;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                    RegeditMain.R_I.BlMaxWin = false;
                }
                cmiMaxWin.IsChecked = RegeditMain.R_I.BlMaxWin;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        void MaxWinMain()
        {
            try
            {
                if (RegeditMain.R_I.BlMaxWin)
                {
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }
                cmiMaxWin.IsChecked = RegeditMain.R_I.BlMaxWin;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 窗体最大化

        #region 调整窗体
        void InitWinSize()
        {
            try
            {
                if (RegeditMain.R_I.Width_gdCamera != 0)
                {
                    gdRoot.ColumnDefinitions[0].Width = new GridLength(RegeditMain.R_I.Width_Win - RegeditMain.R_I.Width_gdInfo, GridUnitType.Star);

                }
                if (RegeditMain.R_I.Width_gdInfo != 0)
                {
                    gdRoot.ColumnDefinitions[2].Width = new GridLength(RegeditMain.R_I.Width_gdInfo, GridUnitType.Star);
                }
                else
                {
                    gdRoot.ColumnDefinitions[2].Width = new GridLength(420, GridUnitType.Pixel);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }


        private void gdsMain_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            try
            {
                if (gdCamera.ActualWidth != 0)
                {
                    RegeditMain.R_I.Width_gdCamera = gdCamera.ActualWidth;
                }
                else
                {
                    RegeditMain.R_I.Width_gdCamera = 1;
                }
                if (gdInfo.ActualWidth != 0)
                {
                    RegeditMain.R_I.Width_gdInfo = gdInfo.ActualWidth;
                }
                else
                {
                    RegeditMain.R_I.Width_gdInfo = 1;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 窗体尺寸变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            try
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    RegeditMain.R_I.BlMaxWin = true;
                }
                else if (this.WindowState == WindowState.Normal)
                {
                    RegeditMain.R_I.BlMaxWin = false;
                }
                cmiMaxWin.IsChecked = RegeditMain.R_I.BlMaxWin;

                RegeditMain.R_I.Width_Win = this.ActualWidth;
                RegeditMain.R_I.Height_Win = this.ActualHeight;

                RegeditMain.R_I.Width_gdCamera = gdCamera.ActualWidth;
                RegeditMain.R_I.Height_gdCamera = gdCamera.ActualHeight;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 调整窗体

        #region 初始化相机综合设置窗体

        /// <summary>
        /// 预打开相机综合设置窗体
        /// </summary>
        public void InitWinComprehensive()
        {
            try
            {
                this.Topmost = true;

                if (ParSetDisplay.P_I.TypeScreen_e != TypeScreen_enum.S800)
                {
                    WinComprehensiveFull.GetWinInst().Show();
                    WinComprehensiveFull.GetWinInst().Hide();
                    //this.Topmost = false;
                    WinComprehensiveFull.GetWinInst().GetResultValueMult_event += MainWindow_GetResultValueMult_event;
                    WinComprehensiveFull.GetWinInst().ActiveWinComp_event += MainWindow_ActiveWinComp_event; ;
                    WinComprehensiveFull.GetWinInst().CloseWin_event += MainWindow_CloseWin_event;
                }
                else//最小窗体
                {
                    //WinComprehensiveSmall.GetWinInst().Show();
                    //WinComprehensiveSmall.GetWinInst().Hide();
                    ////this.Topmost = false;
                    //WinComprehensiveSmall.GetWinInst().GetResultValueMult_event += new ExeAndGetResultValue_del(MainWindow_GetResultValueMult_event);
                    //WinComprehensiveSmall.GetWinInst().ActiveWinComp_event += new Action(MainWindow_ActiveWinComp_event);
                    //WinComprehensiveSmall.GetWinInst().CloseWin_event += new Action(WinInitMain_CloseWin_event);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                BlInitComprehensiveWin = true;
                FinishInitWin();//结束初始化
            }

        }

        private void MainWindow_CloseWin_event()
        {
           
        }

        private void MainWindow_ActiveWinComp_event()
        {
           
        }

        private void MainWindow_GetResultValueMult_event(DealAlgorithm.ParGetResultFromCell par, DealCalibrate.BaseParCalibrate baseParCalibrate, HashAction fun)
        {
            
        }
        #endregion 初始化相机综合设置窗体

        #region 主窗体取消最前
        private void muSetting_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.Topmost)
            {
                this.Topmost = false;
            }
        }

        bool BlFinishAllInit = false;//完成所有的初始化
        /// <summary>
        /// 结束初始化
        /// </summary>
        void FinishInitWin()
        {
            try
            {
                if (BlInitComprehensiveTempWin
                    && BlInitComprehensiveWin)
                {
                    BlFinishAllInit = true;//完成所有的初始化
                    this.Dispatcher.Invoke(new Action(() =>
                        {
                            this.Topmost = false;
                        }));
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 主窗体取消最前
        #endregion 初始化

        #region 配置参数
        /// <summary>
        /// 配置参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiConfigPar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool blNew = false;
                WinConfigPar winConfigPar = WinConfigPar.GetWinInst(out blNew);
                //注册事件
                if (blNew)
                {
                    LoginEvent_ConfigPar(winConfigPar);
                }
                //显示窗体
                winConfigPar.Show();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiConfigPar" + cmiConfigPar.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        void LoginEvent_ConfigPar(WinConfigPar winConfigPar)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 打开调整值窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cimAdjust_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }
                WinSetAdjustWork winSetAdjustWork = WinSetAdjustWork.GetWinInst();
                winSetAdjustWork.Show();

                //按钮日志
                FunLogButton.P_I.AddInfo("cimAdjust" + cimAdjust.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 设置基准值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cimStd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!EngineerReturn())
                {
                    return;
                }
                WinSetStdWork winSetStdWork = WinSetStdWork.GetWinInst();
                winSetStdWork.Show();

                //按钮日志
                FunLogButton.P_I.AddInfo("cimStd" + cimStd.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 打开工作模式设置窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cimTypeWork_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!WorkerReturn())
                {
                    return;
                }
                WinSetWorkType winSetWorkType = new WinSetWorkType();
                winSetWorkType.Show();

                //按钮日志
                FunLogButton.P_I.AddInfo("cimTypeWork" + cimTypeWork.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        #region 配置文件
        /// <summary>
        /// 打开配置文件管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cimManageConfigPar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!EngineerReturn())
                {
                    return;
                }
                WinManageConfigPar winManageConfigPar = new WinManageConfigPar();
                winManageConfigPar.DeleteModel_event += new FdBlStrAction_del(DelModel);//删除文件响应事件
                winManageConfigPar.NewModel_event += new FdBlAction_del(NewModel);//新建型号
                winManageConfigPar.ChangeModel_event += new Action(ChangeModel_event);//换型
                winManageConfigPar.ShowDialog();

                //按钮日志
                FunLogButton.P_I.AddInfo("cimManageConfigPar" + cimManageConfigPar.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }


        /// <summary>
        /// 新建型号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cimNewModel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }
                WinSaveNewModel winSaveNewModel = WinSaveNewModel.GetWinInst();
                winSaveNewModel.NewModel_event += new FdBlAction_del(NewModel);
                winSaveNewModel.ChangeModel_event += new Action(ChangeModel_event);
                winSaveNewModel.Init();
                winSaveNewModel.Show();

                //按钮日志
                FunLogButton.P_I.AddInfo("cimNewModel" + cimNewModel.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }


        /// <summary>
        /// 切换型号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cimChangeModel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }
                if (OpenFileDialog())
                {
                    Task task = new Task(ChangeModel);
                    task.Start();
                }

                //按钮日志
                FunLogButton.P_I.AddInfo("cimChangeModel" + cimChangeModel.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        //切换型号
        void ChangeModel_event()
        {
            Task task = new Task(ChangeModel);
            task.Start();
        }
        #endregion 型号相关

        /// <summary>
        /// 设置声音 报警
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cimSetVoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //WinSetVoice.GetWinInst().Show();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 标准收片机配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cimSTDArrangeConfig_Click(object sender, RoutedEventArgs e)
        {
            bool bl = false;
            WinSetSTDArrayConfig.GetWinInst(out bl).Show();
        }
        #endregion 配置参数

        #region 相机综合设置

        /// <summary>
        /// 相机1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiCamera1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenWinWinComprehensive(1);
                //按钮日志
                FunLogButton.P_I.AddInfo("cmiCamera1" + cmiCamera1.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        WinComprehensiveFull WinComprehensiveFull_I
        {
            get
            {
                return WinComprehensiveFull.GetWinInst();
            }
        }
        /// <summary>
        /// 打开相机综合设置窗体
        /// </summary>
        public void OpenWinWinComprehensive(int noCameraNew)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }
                if (g_BaseUCDisplaySum.BlRealImage)
                {
                    StopReal();
                    return;
                }

                //实时抓取模式
                if (ParSetDisplay.P_I.BlSetReal)
                {
                    g_BaseUCDisplaySum.StopRealGrabImage();
                    return;
                }

                if (ParSetDisplay.P_I.TypeScreen_e != TypeScreen_enum.S800)
                {
                    int noCamera = WinComprehensiveFull_I.NoCamera;
                    if (noCamera != noCameraNew
                        && WinComprehensiveFull_I.Visibility == Visibility.Visible)
                    {
                        MessageBox.Show("请先关闭已打开的相机综合设置窗体" + noCamera.ToString());
                        return;
                    }
                    WinComprehensiveFull_I.Init(noCameraNew);
                    WinComprehensiveFull_I.ShowPar_Invoke();
                    WinComprehensiveFull_I.Visibility = Visibility.Visible;
                    WinComprehensiveFull_I.Focus();
                    WinComprehensiveFull_I.Topmost = true;

                    new Task(new Action(() =>
                    {
                        Thread.Sleep(1000);
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            WinComprehensiveFull_I.Topmost = false;
                        }));

                    })).Start();
                }
                else
                {
                    int noCamera = WinComprehensiveSmall.GetWinInst().NoCamera;
                    if (noCamera != noCameraNew
                        && WinComprehensiveSmall.GetWinInst().Visibility == Visibility.Visible)
                    {
                        MessageBox.Show("请先关闭已打开的相机综合设置窗体" + noCamera.ToString());
                        return;
                    }
                    WinComprehensiveSmall.GetWinInst().Init(noCameraNew);
                    WinComprehensiveSmall.GetWinInst().ShowPar_Invoke();
                    WinComprehensiveSmall.GetWinInst().Visibility = Visibility.Visible;
                    WinComprehensiveSmall.GetWinInst().Focus();
                    WinComprehensiveSmall.GetWinInst().Topmost = true;

                    new Task(new Action(() =>
                    {
                        Thread.Sleep(1000);
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            WinComprehensiveSmall.GetWinInst().Topmost = false;
                        }));

                    })).Start();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void cmiCamera2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenWinWinComprehensive(2);
                //按钮日志
                FunLogButton.P_I.AddInfo("cmiCamera2" + cmiCamera2.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void cmiCamera3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenWinWinComprehensive(3);
                //按钮日志
                FunLogButton.P_I.AddInfo("cmiCamera3" + cmiCamera3.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void cmiCamera4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenWinWinComprehensive(4);
                //按钮日志
                FunLogButton.P_I.AddInfo("cmiCamera4" + cmiCamera4.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void cmiCamera5_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }
                if (g_BaseUCDisplaySum.BlRealImage)
                {
                    StopReal();
                    return;
                }

                WinComprehensive5.GetWinInst().ShowPar_Invoke();
                WinComprehensive5.GetWinInst().Visibility = Visibility.Visible;
                WinComprehensive5.GetWinInst().Focus();
                WinComprehensive5.GetWinInst().Topmost = true;
                new Task(new Action(() =>
                {
                    Thread.Sleep(1000);
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        WinComprehensive5.GetWinInst().Topmost = false;
                    }));

                })).Start();
                //按钮日志
                FunLogButton.P_I.AddInfo("cmiCamera5" + cmiCamera5.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void cmiCamera6_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }
                if (g_BaseUCDisplaySum.BlRealImage)
                {
                    StopReal();
                    return;
                }

                WinComprehensive6.GetWinInst().ShowPar_Invoke();
                WinComprehensive6.GetWinInst().Visibility = Visibility.Visible;
                WinComprehensive6.GetWinInst().Focus();
                WinComprehensive6.GetWinInst().Topmost = true;
                new Task(new Action(() =>
                {
                    Thread.Sleep(1000);
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        WinComprehensive6.GetWinInst().Topmost = false;
                    }));

                })).Start();
                //按钮日志
                FunLogButton.P_I.AddInfo("cmiCamera6" + cmiCamera6.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void cmiCamera7_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }
                if (g_BaseUCDisplaySum.BlRealImage)
                {
                    StopReal();
                    return;
                }

                WinComprehensive7.GetWinInst().ShowPar_Invoke();
                WinComprehensive7.GetWinInst().Visibility = Visibility.Visible;
                WinComprehensive7.GetWinInst().Focus();
                WinComprehensive7.GetWinInst().Topmost = true;
                new Task(new Action(() =>
                {
                    Thread.Sleep(1000);
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        WinComprehensive7.GetWinInst().Topmost = false;
                    }));

                })).Start();
                //按钮日志
                FunLogButton.P_I.AddInfo("cmiCamera7" + cmiCamera6.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void cmiCamera8_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }
                if (g_BaseUCDisplaySum.BlRealImage)
                {
                    StopReal();
                    return;
                }

                WinComprehensive8.GetWinInst().ShowPar_Invoke();
                WinComprehensive8.GetWinInst().Visibility = Visibility.Visible;
                WinComprehensive8.GetWinInst().Focus();
                WinComprehensive8.GetWinInst().Topmost = true;
                new Task(new Action(() =>
                {
                    Thread.Sleep(1000);
                    this.Dispatcher.Invoke(new Action(() =>
                        {
                            WinComprehensive8.GetWinInst().Topmost = false;
                        }));

                })).Start();
                //按钮日志
                FunLogButton.P_I.AddInfo("cmiCamera8" + cmiCamera6.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }


        /// <summary>
        /// 相机参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiSetCameraPar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!WorkerReturn())
                {
                    return;
                }
                if (g_BaseUCDisplaySum.BlRealImage)
                {
                    StopReal();
                    return;
                }

                WinSetCamera.GetWinInst().Show();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiSetCameraPar" + cmiSetCameraPar.Header.ToString(), "Main窗体");

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机拍摄次数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cimCameraWork_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!EngineerReturn())
                {
                    return;
                }
                if (g_BaseUCDisplaySum.BlRealImage)
                {
                    StopReal();
                    return;
                }

                WinCameraWork winCameraWork = new WinCameraWork();
                winCameraWork.ShowDialog();

                //按钮日志
                FunLogButton.P_I.AddInfo("cimCameraWork" + cimCameraWork.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 图像显示设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cimDisplayImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!EngineerReturn())
                {
                    return;
                }
                if (g_BaseUCDisplaySum.BlRealImage)
                {
                    StopReal();
                    return;
                }

                WinSetDisplayImage winSetDisplayImage = new WinSetDisplayImage();
                winSetDisplayImage.ShowDialog();

                //按钮日志
                FunLogButton.P_I.AddInfo("cimDisplayImage" + cimDisplayImage.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 相机综合设置

        #region 通信设定
        /// <summary>
        /// 设定PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiPLC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }
                bool blNew = false;
                WinSetPLC winSetPLC = WinSetPLC.GetWinInst(out blNew);
                winSetPLC.Show();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiPLC" + cmiPLC.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 设定机器人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiRobot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }
                WinSetRobot winSetRobot = new WinSetRobot();
                winSetRobot.ShowDialog();
                //再次读取参数配置,确保重新打开的时候，已经完成加载
                ParSetRobot.P_I.ReadIniPar();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiRobot" + cmiPLC.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        private void cmiRobotStd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }
                bool blNew = false;
                WinStdRobotData.GetWinInst(out blNew).Show();
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 通用端口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiComInterface_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }
                WinSetComInterface winSetComInterface = new WinSetComInterface();
                winSetComInterface.ShowDialog();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiComInterface" + cmiComInterface.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        //IO模块
        private void cmiIO_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!NullReturn())
                {
                    return;
                }


                //按钮日志
                FunLogButton.P_I.AddInfo("cmiIO" + cmiIO.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        //运控卡
        private void cmiMotionCtrler_Click(object sender, RoutedEventArgs e)
        {
            //DMCWindow win = DMCWindow.GetWinInst();//new WinMotionCtrler();
            //win.ShowDialog();
            WinMotionCtrler win = new WinMotionCtrler();
            win.Show();
        }
        #endregion 通信设定

        #region 系统设置
        /// <summary>
        /// 登录权限设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cimSetLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!WorkerReturn())
                {
                    return;
                }
                WinSetLogin winSetLogin = new WinSetLogin();
                winSetLogin.ShowDialog();
                SetDefaultLogin();//设定默认的登录权限

                //按钮日志
                FunLogButton.P_I.AddInfo("cimSetLogin" + cimSetLogin.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 设定默认的登录权限
        /// </summary>
        void SetDefaultLogin()
        {
            try
            {
                //默认厂商权限
                if (RegeditLogin.R_I.BlManufacturer)
                {
                    Authority.Authority_e = Authority_enum.Manufacturer;
                    Login();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //没有登录
                if (Authority.Authority_e == Authority_enum.Null)
                {
                    WinLogin winLogin = new WinLogin();
                    winLogin.ShowDialog();
                    //判断是否登录
                    Login();

                    ShowState("打开登录界面");
                }
                else
                {
                    //退出登录
                    Logout();
                    ShowState("退出登录");
                }

                //按钮日志
                FunLogButton.P_I.AddInfo("imLogin", "Main窗体登陆");

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 显示登陆权限
        /// </summary>
        void Login()
        {
            try
            {
                if (Authority.Authority_e != Authority_enum.Null)
                {
                    imLogin.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Login.jpg"));
                }
                
                switch (Authority.Authority_e)
                {
                    case Authority_enum.Worker:
                        lbLogin.Content = "技术员";
                        break;
                    case Authority_enum.Engineer:
                        lbLogin.Content = "工程师";
                        break;
                    case Authority_enum.Manufacturer:
                        lbLogin.Content = "厂商";
                        WinParInsp.IsManufacturer = true;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        void Logout_Invoke()
        {
            try
            {
                this.Dispatcher.Invoke(new Action(Logout));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        void Logout()
        {
            try
            {
                Authority.Authority_e = Authority_enum.Null;
                RegeditLogin.R_I.BlManufacturer = false;//退出默认厂商权限
                imLogin.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Logout.jpg"));
                lbLogin.Content = "Logout";
                WinParInsp.IsManufacturer = false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 文件删除设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!WorkerReturn())
                {
                    return;
                }
                WinSetFolder winSetFolder = new WinSetFolder();
                winSetFolder.ShowDialog();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiFolder" + cmiFolder.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 内存管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiMemory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!WorkerReturn())
                {
                    return;
                }

                WinMemory winMemory = new WinMemory();
                winMemory.ShowDialog();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiMemory" + cmiMemory.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 系统根目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiPathRoot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!EngineerReturn())
                {
                    return;
                }
                WinPathRoot winPathRoot = new WinPathRoot();
                winPathRoot.ShowDialog();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiPathRoot" + cmiPathRoot.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 设置存储空间监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiMonitorSpace_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!EngineerReturn())
                {
                    return;
                }
                WinHardDisk winHardDisk = new WinHardDisk();
                winHardDisk.ShowDialog();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiMonitorSpace" + cmiMonitorSpace.Header.ToString(), "Main窗体");

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 清理存储空间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiClearSpace_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!WorkerReturn())
                {
                    return;
                }
                WinClearHardDisk.GetWinInst().Show();
                //按钮日志
                FunLogButton.P_I.AddInfo("cmiClearSpace" + cmiClearSpace.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 历史数据恢复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiRecover_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!EngineerReturn())
                {
                    return;
                }
                bool blNew = false;
                WinRecover winRecover = WinRecover.GetWinInst(out blNew);
                winRecover.Show();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiRecover" + cmiClearSpace.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 系统设置

        #region 手动运行
        /// <summary>
        /// 模拟PC触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiManualPC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!WorkerReturn())
                {
                    return;
                }
                TrrigerPC();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiManualPC" + cmiManualPC.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 模拟PLC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiManualPLC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!WorkerReturn())
                {
                    return;
                }

                bool blNew = false;
                WinTrrigerPLC inst = WinTrrigerPLC.GetWinInst(out blNew);
                inst.Show();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiManualPLC" + cmiManualPLC.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 模拟通用端口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiManualComInterface_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!WorkerReturn())
                {
                    return;
                }
                WinTrrigerComInterface inst = new WinTrrigerComInterface();
                inst.Show();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiManualComInterface" + cmiManualComInterface.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 模拟机器人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiManualRobot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!WorkerReturn())
                {
                    return;
                }
                bool blNew = false;
                WinTrrigerRobot winTrrigerRobot = WinTrrigerRobot.GetWinInst(out blNew);
                winTrrigerRobot.ManualRobot_event += new ManualRobot_del(winTrrigerRobot_ManualRobot_event);
                winTrrigerRobot.Show();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiManualRobot" + cmiManualRobot.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 机器人示教
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiTechRobot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool blNew = false;
                WinManualRobot winManualRobot = WinManualRobot.GetWinInst(out blNew);
                winManualRobot.Show();
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 手动操作机器人
        /// </summary>
        /// <param name="manualRobot_e"></param>
        void winTrrigerRobot_ManualRobot_event(ManualRobot_enum manualRobot_e)
        {
            try
            {
                switch (manualRobot_e)
                {
                    case ManualRobot_enum.ConfigPar://配置机器人参数
                        ConfigRobot_Task();
                        break;

                    case ManualRobot_enum.Close:
                        break;

                    case ManualRobot_enum.Restart:
                        break;

                    case ManualRobot_enum.Reset:
                        break;

                    case ManualRobot_enum.Home:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 手动发送机器人配置参数
        /// </summary>
        void winTrrigerRobot_ConfigRobot_event()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }


        /// <summary>
        /// 重启机器人通信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiRestartRobot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!WorkerReturn())
                {
                    return;
                }
                new Task(new Action(RobotReStart)).Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 软件重启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiRestartSoft_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RestartSoft();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiRestartSoft" + cmiRestartSoft.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 关闭电脑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiShutDownPC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ShutDownPC();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiShutDownPC" + cmiShutDownPC.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 重启电脑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiRestartPC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RestartPC();

                //按钮日志
                FunLogButton.P_I.AddInfo("cmiRestartPC" + cmiRestartPC.Header.ToString(), "Main窗体");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 手动运行

        #region 工具
        /// <summary>
        /// 计算器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiCal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SysTool.S_I.OpenCal();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 打开记事本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiText_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SysTool.S_I.OpenTxt();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 绘图板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiPaint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SysTool.S_I.OpenPaint();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        private void cmiKeyboard_Click(object sender, RoutedEventArgs e)
        {
            SysTool.S_I.OpenKeyboard();
        }
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiCopyFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool blNew = false;
                WinCopyFile winCopyFile = WinCopyFile.GetWinInst(out blNew);
                winCopyFile.Show();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 重启网卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiRestartNet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WinRestartNet winRestartNet = WinRestartNet.GetWinInst();
                winRestartNet.Show();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }


        /// <summary>
        /// 窗体最大化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiMaxWin_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                RegeditMain.R_I.BlMaxWin = true;
                this.WindowState = WindowState.Maximized;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        private void cmiMaxWin_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                RegeditMain.R_I.BlMaxWin = false;
                this.WindowState = WindowState.Normal;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 工具

        #region 离线模式
        private void cmiOffline_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                if (cmiOffline.IsMouseOver)
                {
                    if (Authority.Authority_e == Authority_enum.Null
                    || Authority.Authority_e == Authority_enum.Worker)
                    {
                        cmiPLCOffline.IsEnabled = false;
                        cmiRobotOffline.IsEnabled = false;
                        cmiComPortOffline.IsEnabled = false;
                        cmiCameraOffline.IsEnabled = false;
                    }
                    else
                    {
                        cmiPLCOffline.IsEnabled = true;
                        cmiRobotOffline.IsEnabled = true;
                        cmiComPortOffline.IsEnabled = true;
                        cmiCameraOffline.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 相机离线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiCameraOffline_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmiCameraOffline.IsMouseOver)
                {
                    RegeditCamera.R_I.BlOffLineCamera = true;
                    ChangeColorOffLine();
                    ShowAlarm("相机设置为离线状态");

                    //按钮日志
                    FunLogButton.P_I.AddInfo("cmiCameraOffline" + cmiCameraOffline.Header.ToString() + "Checked", "Main窗体");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        private void cmiCameraOffline_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmiCameraOffline.IsMouseOver)
                {
                    RegeditCamera.R_I.BlOffLineCamera = false;
                    ChangeColorOffLine();
                    ShowAlarm("相机设置为离线状态");
                    //按钮日志
                    FunLogButton.P_I.AddInfo("cmiCameraOffline" + cmiCameraOffline.Header.ToString() + "Unchecked", "Main窗体");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// PLC离线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiPLCOffline_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmiPLCOffline.IsMouseOver)
                {
                    RegeditPLC.R_I.BlOffLinePLC = true;
                    ChangeColorOffLine();
                    ShowAlarm("PLC设置为离线状态");
                    //按钮日志
                    FunLogButton.P_I.AddInfo("cmiPLCOffline" + cmiPLCOffline.Header.ToString() + "Checked", "Main窗体");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        private void cmiPLCOffline_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmiPLCOffline.IsMouseOver)
                {
                    RegeditPLC.R_I.BlOffLinePLC = false;
                    ChangeColorOffLine();
                    ShowAlarm("PLC恢复在线状态");
                    //按钮日志
                    FunLogButton.P_I.AddInfo("cmiPLCOffline" + cmiPLCOffline.Header.ToString() + "Unchecked", "Main窗体");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 机器人处于离线状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmiRobotOffline_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmiRobotOffline.IsMouseOver)
                {
                    RegeditRobot.R_I.BlOffLineRobot = true;
                    ChangeColorOffLine();
                    ShowAlarm("机器人设置为离线状态");

                    //按钮日志
                    FunLogButton.P_I.AddInfo("cmiRobotOffline" + cmiRobotOffline.Header.ToString() + "Checked", "Main窗体");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        private void cmiRobotOffline_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmiRobotOffline.IsMouseOver)
                {
                    RegeditRobot.R_I.BlOffLineRobot = false;
                    ChangeColorOffLine();
                    ShowAlarm("机器人恢复在线状态");

                    //按钮日志
                    FunLogButton.P_I.AddInfo("cmiRobotOffline" + cmiRobotOffline.Header.ToString() + "Unchecked", "Main窗体");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 改变菜单字体颜色
        /// </summary>
        void ChangeColorOffLine()
        {
            try
            {
                if (RegeditPLC.R_I.BlOffLinePLC
                    || RegeditRobot.R_I.BlOffLineRobot
                    || RegeditMain.R_I.BlOffLineComPort
                    || RegeditCamera.R_I.BlOffLineCamera)
                {
                    cmiOffline.Foreground = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    cmiOffline.Foreground = System.Windows.Media.Brushes.Black;
                }
                cmiRobotOffline.IsChecked = RegeditRobot.R_I.BlOffLineRobot;
                cmiPLCOffline.IsChecked = RegeditPLC.R_I.BlOffLinePLC;
                cmiComPortOffline.IsChecked = RegeditMain.R_I.BlOffLineComPort;
                cmiCameraOffline.IsChecked = RegeditCamera.R_I.BlOffLineCamera;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 离线模式

        #region 其他
        private void cmiCim_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!WorkerReturn())
                    return;
                CIMWnd wnd = new CIMWnd();
                if (wnd.IsVisible == false)
                    wnd.Show();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void CmiPost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!WorkerReturn())
                    return;

                if (ModelParams.DefaultQrCodeOK || ModelParams.DefaultChipIDOK ||
                ModelParams.DefaultRunCardOK || ModelParams.DefaultTrackOutOK)
                {
                    MessageBox.Show("存在CIM模块处于屏蔽状态，可能导致CIM未连接");
                    //return;
                }

                PostTrackout wnd = PostTrackout.GetInstance();
                if (wnd.IsVisible == false)
                    wnd.Show();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void CmiPostLot_Click(object sender, RoutedEventArgs e)
        {
            PostLotWnd wnd = PostLotWnd.GetInstance();
            if (wnd.IsVisible == false)
                wnd.Show();
        }
        #endregion 其他

        #region 帮助
        private void cmiAbout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WinAbout winAbout = new WinAbout();
                winAbout.Show();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        private void CmiVsionPath_Click(object sender, RoutedEventArgs e)
        {
            WinGetPermission win = new WinGetPermission();
            win.ShowDialog();
        }
        #endregion 帮助

        #region 权限判断
        /// <summary>
        /// 无权限返回
        /// </summary>
        /// <returns></returns>
        bool NullReturn()
        {
            if (Authority.Authority_e == Authority_enum.Null)
            {
                MessageBox.Show("需技术员及以上权限");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 技术员权限返回
        /// </summary>
        /// <returns></returns>
        bool WorkerReturn()
        {
            if (Authority.Authority_e == Authority_enum.Null
                || Authority.Authority_e == Authority_enum.Worker)
            {
                MessageBox.Show("需工程师及以上权限");
                return false;
            }
            return true;
        }
        /// <summary>
        /// 工程师权限返回
        /// </summary>
        /// <returns></returns>
        bool EngineerReturn()
        {
            if (Authority.Authority_e != Authority_enum.Manufacturer)
            {
                MessageBox.Show("需厂商权限");
                return false;
            }
            return true;
        }
        #endregion 权限判断

        #region CIM模拟

        #endregion CIM模拟

        #region 工位示教
        /// <summary>
        /// 工位示教标定 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTechPos_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                string strPar = "1";
                string btnContent = btn.Content.ToString();
                if (btnContent.Contains("一"))
                {
                    strPar = "1";
                }
                else if (btnContent.Contains("二"))
                {
                    strPar = "2";
                }
                else if (btnContent.Contains("三"))
                {
                    strPar = "3";
                }
                else if (btnContent.Contains("四"))
                {
                    strPar = "4";
                }

                LogicRobot.L_I.WriteRobotCMD(strPar, "200");
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 工位示教

        /// <summary>
        /// 插栏临时补偿
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dudXInsert_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudXInsert.IsMouseOver)
                {
                    MainCom.M_I.XInset = Math.Round((double)dudXInsert.Value, 2);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dudRInsert_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudXInsert.IsMouseOver)
                {
                    MainCom.M_I.RInset = Math.Round((double)dudRInsert.Value, 2);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void BtnRestartRobot_Click(object sender, RoutedEventArgs e)
        {
            new Task(ResetRobot).Start();
        }

        void ClearTempInsertAdj()
        {
            try
            {
                this.Dispatcher.Invoke(new Action(delegate ()
                {
                    ShowState("插栏新卡塞");
                    dudXInsert.Value = 0;
                    dudRInsert.Value = 0;

                    MainCom.M_I.XInset = 0;
                    MainCom.M_I.RInset = 0;
                }));

            }
            catch (Exception ex)
            {

            }
        }

        private void BtnWriteCode_Click(object sender, RoutedEventArgs e)
        {
            DealComprehensiveResult1.D_I.WriteCodeToPLC("ABCDEFGH12345");
        }

        private void RdbCam1_Checked(object sender, RoutedEventArgs e)
        {
            g_BaseDealComprehensiveResult = DealComprehensiveResult1.D_I;
        }

        private void RdbCam2_Checked(object sender, RoutedEventArgs e)
        {
            g_BaseDealComprehensiveResult = DealComprehensiveResult2.D_I;
        }

        private void RdbCam3_Checked(object sender, RoutedEventArgs e)
        {
            g_BaseDealComprehensiveResult = DealComprehensiveResult3.D_I;
        }

        private void RdbCam4_Checked(object sender, RoutedEventArgs e)
        {
            g_BaseDealComprehensiveResult = DealComprehensiveResult4.D_I;
        }

        private void BtnSetPar_Click(object sender, RoutedEventArgs e)
        {
            if (!WorkerReturn())
            {
                ShowAlarm("权限不足，请先登录！");
                return;
            }
            WinParInsp win = WinParInsp.GetWinInst(g_BaseDealComprehensiveResult.g_ParIns);
            win.ShowDialog();
        }

        private void BtnTrigger_Click(object sender, RoutedEventArgs e)
        {
            //ucResultInsp.ShowResult(0);

            new Task(() =>
            {
                BaseDealComprehensiveResult.ResultInspSingeCell_L.Add(new ResultInspection());
                //xc-1229
                //ucSingleRecord.blRefresh = true;
                UCRecordTemp.blRefresh = true;
            }).Start();



            //return;
            //g_BaseDealComprehensiveResult.ManualTrigger(CmbbSide.SelectedIndex + 1);
        }

        private void BtnSetWork_Click(object sender, RoutedEventArgs e)
        {
            WinParWorkMode win = WinParWorkMode.GetWinInst();
            win.Show();
        }

        private void BtnOneKStart1_Click(object sender, RoutedEventArgs e)
        {
            ShowState("开启一键检测1");
            ManualStartCycle(1);
        }

        private void BtnOneKStart2_Click(object sender, RoutedEventArgs e)
        {
            ShowState("开启一键检测2");
            ManualStartCycle(2);
        }

        private void BtnOneKStop_Click(object sender, RoutedEventArgs e)
        {
            ShowState("开启一键停止");
            ManualStop();
        }

        private void BtnOneKFinish_Click(object sender, RoutedEventArgs e)
        {
            ShowState("开启一键结束");

            BaseDealComprehensiveResult.CommunicateInspResult();
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            ShowState("OK图片清理中……");
            Log.L_I.DeleteDir(ParDelFolder.P_I.ImageInsp.PathFolder, ParDelFolder.P_I.ImageInsp.Num);
            ShowState("OK图片清理结束");
            ShowState("NG图片清理中……");
            Log.L_I.DeleteDir(ParDelFolder.P_I.ImageInspNG.PathFolder, ParDelFolder.P_I.ImageInspNG.Num);
            ShowState("NG图片清理结束");
        }

        private void BtnLocalTest_Click(object sender, RoutedEventArgs e)
        {
            DealComprehensiveResult1.D_I.LocalTest();
        }

        private void BtnLocalTest2_Click(object sender, RoutedEventArgs e)
        {
            DealComprehensiveResult2.D_I.LocalTest();
        }

        private void BtnLocalTest3_Click(object sender, RoutedEventArgs e)
        {
            DealComprehensiveResult3.D_I.LocalTest();
        }

        private void BtnLocalTest4_Click(object sender, RoutedEventArgs e)
        {
            DealComprehensiveResult4.D_I.LocalTest();
        }

        private void BtnPost_Click(object sender, RoutedEventArgs e)
        {
            PostLotWnd wnd = PostLotWnd.GetInstance();
            if (!wnd.IsVisible)
                wnd.Show();
        }

        private void CmiSample_Click(object sender, RoutedEventArgs e)
        {
            if (!WorkerReturn())
                return;
            WinSample wnd = WinSample.instance;
            if (!wnd.IsVisible)
                wnd.Show();
        }

        private void CmiReport_Click(object sender, RoutedEventArgs e)
        {
            ProductivityReport wndProductivityReport = new ProductivityReport();
            wndProductivityReport.Show();
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            //CIM.ChipIDCount=159;
        }


        private void CmiCimMode_Click(object sender, RoutedEventArgs e)
        {
            if (!WorkerReturn())
                return;
            WndCimMode wnd = WndCimMode.GetInstance();
            if (!wnd.IsVisible)
                wnd.Show();
        }

        bool quit = false;
        private void BtnBugTest_Click(object sender, RoutedEventArgs e)
        {
            quit = false;

            Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < 100; ++i)
                {
                    if (quit)
                        break;

                    Thread.Sleep(500);
                    BaseDealComprehensiveResult.Init();
                    Thread.Sleep(500);

                    DealComprehensiveResult1.D_I.DealComprehensiveResultFun1(TriggerSource_enum.PLC, out Hashtable hashtable);
                    DealComprehensiveResult2.D_I.DealComprehensiveResultFun1(TriggerSource_enum.PLC, out hashtable);
                    DealComprehensiveResult3.D_I.DealComprehensiveResultFun1(TriggerSource_enum.PLC, out hashtable);
                    DealComprehensiveResult4.D_I.DealComprehensiveResultFun1(TriggerSource_enum.PLC, out hashtable);
                    Thread.Sleep(1500);
                    

                    DealComprehensiveResult1.D_I.BlCyclePhotoStop = true;
                    DealComprehensiveResult2.D_I.BlCyclePhotoStop = true;
                    DealComprehensiveResult3.D_I.BlCyclePhotoStop = true;
                    DealComprehensiveResult4.D_I.BlCyclePhotoStop = true;
                    ShowState("PLC通知单边检测停止");

                    Thread.Sleep((int)ParStd.Value1("std18"));
                    ShowState("开始进行采图");
                    DealComprehensiveResult1.D_I.DealComprehensiveResultFun2(TriggerSource_enum.PLC, out hashtable);
                    DealComprehensiveResult2.D_I.DealComprehensiveResultFun2(TriggerSource_enum.PLC, out hashtable);
                    DealComprehensiveResult3.D_I.DealComprehensiveResultFun2(TriggerSource_enum.PLC, out hashtable);
                    DealComprehensiveResult4.D_I.DealComprehensiveResultFun2(TriggerSource_enum.PLC, out hashtable);
                    Thread.Sleep(1500);

                    DealComprehensiveResult1.D_I.BlCyclePhotoStop = true;
                    DealComprehensiveResult2.D_I.BlCyclePhotoStop = true;
                    DealComprehensiveResult3.D_I.BlCyclePhotoStop = true;
                    DealComprehensiveResult4.D_I.BlCyclePhotoStop = true;
                    ShowState("PLC通知单边检测停止");

                    Thread.Sleep((int)ParStd.Value2("std18"));
                    ShowState("PLC通知所有检测结束");
                    BaseDealComprehensiveResult.CommunicateInspResult();
                }
            });
        }

        private void BtnStopTest_Click(object sender, RoutedEventArgs e)
        {
            quit = true;
            ShowState("BUG测试结束");
        }

        void FTPTest()
        {
            FtpConnectionModel data = new FtpConnectionModel
            {
                Host = @"ftp://192.168.0.106/",
                Username = "xc",
                Password = "910127",
            };
            RequesterFtp requester = new RequesterFtp(data);
            requester.Upload(new FileInfo(@"E:\1.jpg"), "img.jpg");
        }
    }
}
