using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using BasicClass;
using BasicDisplay;
using Camera;
using DealCalibrate;
using DealComInterface;
using DealConfigFile;
using DealHelp;
using DealLog;
using DealMontionCtrler;
using DealPLC;
using DealRobot;
using ParComprehensive;
using SetPar;
using DealFile;

namespace Main
{
    /// <summary>
    /// StartUpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartUpWindow : Window
    {
        #region 定义
        //bool
        bool g_BlFinishOthers = false;

        bool g_BlFinishCamera = false;

        bool g_BlFinishComprehensive1 = false;
        bool g_BlFinishComprehensive2 = false;
        bool g_BlFinishComprehensive3 = false;
        bool g_BlFinishComprehensive4 = false;
        bool g_BlFinishComprehensive5 = false;
        bool g_BlFinishComprehensive6 = false;
        bool g_BlFinishComprehensive7 = false;
        bool g_BlFinishComprehensive8 = false;

        public string pathHalconFill = "";
        #endregion 定义

        #region 初始化
        public StartUpWindow()
        {
            InitializeComponent();
           
            ParPathRoot.P_I.ReadRootPath();

            if (Debugger.IsAttached)
            {
                string path = @"./根目录.ini";
                string section = "DebugInfo";
                //获取当前进程的完整路径，包含文件名(进程名)。
                string str = this.GetType().Assembly.Location;
                IniFile.I_I.WriteIni(section, "AssemblyLocation", str, path);

                IniFile.I_I.WriteIni(section, "UpdateTime", DateTime.Now.ToString(""), path);
                IniFile.I_I.WriteIni(section, "UserName", Environment.UserName, path);

                MainCom.M_I.IsDebugMode = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetPriority();//设置程序优先级位高
            Init();
        }
        /// <summary>
        /// 设置程序优先级
        /// </summary>
        void SetPriority()
        {
            try
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("StartUpWindow", ex);
            }
        }

        /// <summary>
        /// 初始化加载
        /// </summary>
        void Init()
        {
            try
            {
                #region 初始化配置参数
                Init_ConfigPar();//读取配置参数
                Init_SetPar();//读取系统参数         
                #endregion 初始化配置参数

                //Others
                //new Task(Init_Others).Start();
                Init_Others();
                //Camera
                new Task(OpenCamera).Start();

                //相机综合设置
                Init_Comprehensive();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("StartUpWindow", ex);
            }
        }
        #endregion 初始化

        #region 读取配置文件
        //配置参数
        void Init_ConfigPar()
        {
            ParCameraWork.P_I.ReadIniNumCamera();//读取相机个数,要先读，后面的文件都会用到 
            ParCameraWork.P_I.ReadIniPar();//相机工作文件  
            ParManageConfigPar.P_I.ReadIniPathConfigPar();//读取当前配置文件路径
            ParConfigPar.P_I.ReadIniConfigPar();//读取产品参数        
            ParSetWork.P_I.ReadIniPar(); //工作运行设置文件

            ParSetDisplay.P_I.ReadIniPar();//显示设置

            //ParVoice.P_I.ReadParIni();//读取声音报警

            ReadAssistantSharpe();//辅助灰度
            //读取校准文件
            ReadCalib();
            ParMotionCtrler.P_I.ReadAll();

            #region 巡边检
            ParAnalysis.P_I.InitInfoNow();
            ParWorkInsp.P_I.ReadIni();
            ParCam1.P_I.ReadIni();
            if (ParCameraWork.NumCamera > 1)
            {
                ParCam2.P_I.ReadIni();
            }
            if (ParCameraWork.NumCamera > 2)
            {
                ParCam3.P_I.ReadIni();
            }
            if (ParCameraWork.NumCamera > 3)
            {
                ParCam4.P_I.ReadIni();
            }
            #endregion

            ParSTDArrange.P_I.ReadConfig();//读取标准受骗文件
        }

        /// <summary>
        /// 系统参数
        /// </summary>
        void Init_SetPar()
        {
            ParSetLogin.P_I.ReadIniPar();//登陆权限设置
            ParDelFolder.P_I.ReadIniPar();//删除文件设定  
            ParHardDisk.P_I.ReadIniPar();//存储空间监控
            ParMemory.P_I.ReadIniPar();//读取内存设置
        }

        /// <summary>
        /// 读取校准文件
        /// </summary>
        void ReadCalib()
        {
            try
            {
                //Calib
                ParCalibWorld.V_I.ReadIniAmp();  //读取放大系数

                #region 机器人校准参数
                ParCalibRobot1.P_I.ReadIniCalib();
                ParCalibRobot2.P_I.ReadIniCalib();
                ParCalibRobot3.P_I.ReadIniCalib();
                ParCalibRobot4.P_I.ReadIniCalib();
                #endregion 机器人校准参数
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("StartUpWindow", ex);
            }
        }
        /// <summary>
        /// 读取辅助绘图
        /// </summary>
        void ReadAssistantSharpe()
        {
            try
            {
                ParAssistantSharpe1.P_I.ReadIniPar();
                ParAssistantSharpe2.P_I.ReadIniPar();
                ParAssistantSharpe3.P_I.ReadIniPar();
                ParAssistantSharpe4.P_I.ReadIniPar();
                ParAssistantSharpe5.P_I.ReadIniPar();
                ParAssistantSharpe6.P_I.ReadIniPar();
                ParAssistantSharpe7.P_I.ReadIniPar();
                ParAssistantSharpe8.P_I.ReadIniPar();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("StartUpWindow", ex);
            }
        }

        #region 相机综合设置参数
        /// <summary>
        /// 相机综合设置参数
        /// </summary>
        void Init_Comprehensive()
        {
            try
            {
                new Task(Init_Comprehensive1).Start();

                new Task(Init_Comprehensive2).Start();

                new Task(Init_Comprehensive3).Start();

                new Task(Init_Comprehensive4).Start();

                new Task(Init_Comprehensive5).Start();

                new Task(Init_Comprehensive6).Start();

                new Task(Init_Comprehensive7).Start();

                new Task(Init_Comprehensive8).Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("StartUpWindow", ex);
            }
        }
        /// <summary>
        /// 相机1综合设置
        /// </summary>
        void Init_Comprehensive1()
        {
            try
            {
                ParComprehensive1.P_I.ReadXmlXDoc();//综合处理参数               
            }
            catch (Exception ex)
            {

            }
            finally
            {
                g_BlFinishComprehensive1 = true;
                FinishInit();
            }
        }
        /// <summary>
        /// 相机2综合设置
        /// </summary>
        void Init_Comprehensive2()
        {
            try
            {
                if (ParCameraWork.NumCamera < 2)
                {
                    return;
                }
                ParComprehensive2.P_I.ReadXmlXDoc();//综合处理参数               
            }
            catch (Exception)
            {

            }
            finally
            {
                g_BlFinishComprehensive2 = true;
                FinishInit();
            }
        }
        /// <summary>
        /// 相机3综合设置
        /// </summary>
        void Init_Comprehensive3()
        {
            try
            {
                if (ParCameraWork.NumCamera < 3)
                {
                    return;
                }
                ParComprehensive3.P_I.ReadXmlXDoc();//综合处理参数           
            }
            catch (Exception)
            {

            }
            finally
            {
                g_BlFinishComprehensive3 = true;
                FinishInit();
            }
        }
        /// <summary>
        /// 相机4综合设置
        /// </summary>
        void Init_Comprehensive4()
        {
            try
            {
                if (ParCameraWork.NumCamera < 4)
                {
                    return;
                }
                ParComprehensive4.P_I.ReadXmlXDoc();//综合处理参数              
            }
            catch (Exception)
            {

            }
            finally
            {
                g_BlFinishComprehensive4 = true;
                FinishInit();
            }
        }
        /// <summary>
        /// 相机5综合设置
        /// </summary>
        void Init_Comprehensive5()
        {
            try
            {
                if (ParCameraWork.NumCamera < 5)
                {
                    return;
                }
                ParComprehensive5.P_I.ReadXmlXDoc();//综合处理参数              
            }
            catch (Exception)
            {

            }
            finally
            {
                g_BlFinishComprehensive5 = true;
                FinishInit();
            }
        }
        /// <summary>
        /// 相机6综合设置
        /// </summary>
        void Init_Comprehensive6()
        {
            try
            {
                if (ParCameraWork.NumCamera < 6)
                {
                    return;
                }
                ParComprehensive6.P_I.ReadXmlXDoc();//综合处理参数               
            }
            catch (Exception)
            {

            }
            finally
            {
                g_BlFinishComprehensive6 = true;
                FinishInit();
            }
        }

        /// <summary>
        /// 相机7综合设置
        /// </summary>
        void Init_Comprehensive7()
        {
            try
            {
                if (ParCameraWork.NumCamera < 7)
                {
                    return;
                }
                ParComprehensive7.P_I.ReadXmlXDoc();//综合处理参数               
            }
            catch (Exception)
            {

            }
            finally
            {
                g_BlFinishComprehensive7 = true;
                FinishInit();
            }
        }

        /// <summary>
        /// 相机8综合设置
        /// </summary>
        void Init_Comprehensive8()
        {
            try
            {
                if (ParCameraWork.NumCamera < 8)
                {
                    return;
                }
                ParComprehensive8.P_I.ReadXmlXDoc();//综合处理参数               
            }
            catch (Exception)
            {

            }
            finally
            {
                g_BlFinishComprehensive8 = true;
                FinishInit();
            }
        }
        #endregion 相机综合设置参数
        #endregion 读取配置文件

        #region 打开相机
        /// <summary>
        /// 打开相机
        /// </summary>
        void OpenCamera()
        {
            try
            {
                ParCamera1.P_I.ReadIni();//相机参数
                Camera1.C_I.Init(ParCamera1.P_I);
                if (Camera1.C_I.OpenCamera())
                {
                    ParCamera1.P_I.BlOpenCamera = true;
                }
                Thread.Sleep(50);

                if (ParCameraWork.NumCamera < 2)
                {
                    return;
                }
                ParCamera2.P_I.ReadIni();//相机参数
                Camera2.C_I.Init(ParCamera2.P_I);
                if (Camera2.C_I.OpenCamera())
                {
                    ParCamera2.P_I.BlOpenCamera = true;
                }
                Thread.Sleep(50);

                if (ParCameraWork.NumCamera < 3)
                {
                    return;
                }
                ParCamera3.P_I.ReadIni();//相机参数
                Camera3.C_I.Init(ParCamera3.P_I);
                if (Camera3.C_I.OpenCamera())
                {
                    ParCamera3.P_I.BlOpenCamera = true;
                }
                Thread.Sleep(50);

                if (ParCameraWork.NumCamera < 4)
                {
                    return;
                }
                ParCamera4.P_I.ReadIni();//相机参数
                Camera4.C_I.Init(ParCamera4.P_I);
                if (Camera4.C_I.OpenCamera())
                {
                    ParCamera4.P_I.BlOpenCamera = true;
                }
                Thread.Sleep(50);

                if (ParCameraWork.NumCamera < 5)
                {
                    return;
                }
                ParCamera5.P_I.ReadIni();//相机参数
                Camera5.C_I.Init(ParCamera5.P_I);
                if (Camera5.C_I.OpenCamera())
                {
                    ParCamera5.P_I.BlOpenCamera = true;
                }
                Thread.Sleep(50);

                if (ParCameraWork.NumCamera < 6)
                {
                    return;
                }
                ParCamera6.P_I.ReadIni();//相机参数
                Camera6.C_I.Init(ParCamera6.P_I);
                if (Camera6.C_I.OpenCamera())
                {
                    ParCamera6.P_I.BlOpenCamera = true;
                }
                Thread.Sleep(50);

                if (ParCameraWork.NumCamera < 7)
                {
                    return;
                }
                ParCamera7.P_I.ReadIni();//相机参数
                Camera7.C_I.Init(ParCamera7.P_I);
                if (Camera7.C_I.OpenCamera())
                {
                    ParCamera7.P_I.BlOpenCamera = true;
                }
                Thread.Sleep(50);

                if (ParCameraWork.NumCamera < 8)
                {
                    return;
                }
                ParCamera8.P_I.ReadIni();//相机参数
                Camera8.C_I.Init(ParCamera8.P_I);
                if (Camera8.C_I.OpenCamera())
                {
                    ParCamera8.P_I.BlOpenCamera = true;
                }
                Thread.Sleep(50);
            }
            catch (Exception ex)
            {
                this.Dispatcher.Invoke(new Action(() =>
                    {
                        WinError.GetWinInst().ShowError("相机加载报错:" + ex.Message);
                    }));
            }
            finally
            {
                if (RegeditCamera.R_I.BlOffLineCamera)
                {
                    ParCamera1.P_I.BlOpenCamera = true;
                    ParCamera2.P_I.BlOpenCamera = true;
                    ParCamera3.P_I.BlOpenCamera = true;
                    ParCamera4.P_I.BlOpenCamera = true;
                    ParCamera5.P_I.BlOpenCamera = true;
                    ParCamera6.P_I.BlOpenCamera = true;
                    ParCamera7.P_I.BlOpenCamera = true;
                    ParCamera8.P_I.BlOpenCamera = true;
                }
                g_BlFinishCamera = true;
                FinishInit();
            }
        }
        #endregion 打开相机

        #region 初始化其他
        /// <summary>
        /// 初始化其他设置
        /// </summary>
        void Init_Others()
        {
            //初始化通信
            Init_Communicate();
            //关于
            FunAbout.F_I.CopySoftLog_DebugRelease();
            g_BlFinishOthers = true;
            FinishInit();
        }

        #region 通信
        void Init_Communicate()
        {
            //仅仅只是读取机器人参数，不打开机器人
            Init_Robot();

            //PLC
            Init_PLC();

            //ParDIO.P_I.ReadIniPar();

            //ComInterface
            Task taskComInterface = new Task(Init_ComInterface);
            taskComInterface.Start();
        }

        //初始化DealPLC
        void Init_PLC()
        {
            try
            {
                //读取PLC相关参数,PLC在主界面打开
                ParSetPLC.P_I.ReadIni();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("StartUpWindow", ex);
            }
        }
        //DealRobot 
        void Init_Robot()
        {
            try
            {
                ParSetRobot.P_I.ReadIniPar(); //读取机器人配置文件  
                LogicRobot.L_I.Init();
                //if (ParSetRobot.P_I.TypeRobot_e != TypeRobot_enum.Null)
                //{
                //    LogicRobot.L_I.OpenInterface();//打开机器人
                //}
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("StartUpWindow", ex);
            }
        }

        //ComInterface 
        void Init_ComInterface()
        {
            try
            {
                ParComInterface.P_I.ReadIniStr(); //读取机器人配置文件    
                if (ParComInterface.P_I.TypeComInterface_e != TypeComInterface_enum.Null)
                {
                    LogicComInterface.L_I.OpenPort();//打开机器人
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("StartUpWindow", ex);
            }
        }
        #endregion 通信

        #region  注册dLL
        void RegeditDLL()
        {
            try
            {

                Process p = new Process();
                p.StartInfo.FileName = "Regsvr32.exe";
                p.StartInfo.Arguments = "/s C:\\DllTest.dll";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("StartUpWindow", ex);
            }
        }
        #endregion  注册dLL
        #endregion 初始化其他

        /// <summary>
        /// 加载完成
        /// </summary>
        void FinishInit()
        {
            if (g_BlFinishCamera
                && g_BlFinishComprehensive1
                && g_BlFinishComprehensive2
                && g_BlFinishComprehensive3
                && g_BlFinishComprehensive4
                && g_BlFinishComprehensive5
                && g_BlFinishComprehensive6
                && g_BlFinishComprehensive7
                && g_BlFinishComprehensive8
                && g_BlFinishOthers)
            {
                //通知主线程自己已经启动完毕
                Program.s_mre.Set();
            }
        }
    }
}
