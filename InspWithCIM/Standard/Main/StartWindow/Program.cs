using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using System.Windows;
using BasicClass;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Main
{
    class Program
    {
        #region 定义
        //event
        public static ManualResetEvent s_mre = new ManualResetEvent(false); //线程同步信号，用于通知主线程启动屏幕已显示完毕
        //Win
        static StartUpWindow g_StartUpWindow = null;//启动窗体
        //Mutex
        static Mutex g_MtSingle = null;//窗体单实例
        #endregion 定义

        [STAThread]
        static void Main()
        {
            try
            {
                //只允许一个程序运行
                bool requestInitialOwnership = true;
                bool mutexWasCreated;
                g_MtSingle = new Mutex(requestInitialOwnership, "机器视觉控制处理软件", out mutexWasCreated);
                if (!(requestInitialOwnership && mutexWasCreated))
                {
                    MessageBox.Show("软件正在运行中，如为异常情况，请先在任务管理器的进程中将软件进程关闭!");
                    return;
                }

                //在一个独立UI线程中显示启动屏幕
                Thread th = new Thread(ShowSplashScreen);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
                //启动初始化过程
                SystemInit();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Program", ex);
            }
        }

        /// 创建启动屏幕对象，显示系统初始化信息，最后显示主窗体
        static void ShowSplashScreen()
        {
            try
            {
                g_StartUpWindow = new StartUpWindow();
                g_StartUpWindow.ShowDialog();
                //显示主窗体
                Application application = new Application();
                application.Run(new MainWindow());
            }
            catch (Exception ex)
            {
                WinMsgBox.ShowMsgBox("软件发生异常\r①点击确定或取消后软件将关闭 \r②移开此窗口并手动对主界面截屏\r③观察机台内当前巡边玻璃是否已往后流\r④记录时间，发送异常信息日志和运行报警日志到开发人员邮箱");

                BinaryFormatter formatter = null;
                string path = Log.CreateAllTimeFile(Log.L_I.PathErrorLog) + "Exception" + DateTime.Now.ToString(" HH-mm-ss") + ".dat";
                using (FileStream ms = new FileStream(path, FileMode.Create))
                {
                    formatter = new BinaryFormatter();
                    formatter.Serialize(ms, ex);
                }
                Log.L_I.WriteError("Program", ex);
            }
        }
        /// 进行系统初始化……
        static void SystemInit()
        {
            try
            {
                //等待启动窗体启动完毕
                s_mre.WaitOne();
                //关闭启动窗体
                g_StartUpWindow.Dispatcher.BeginInvoke(new Action(CloseStartWindow), DispatcherPriority.Input, null);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Program", ex);
            }
        }

        //关闭启动画面
        static void CloseStartWindow()
        {
            g_StartUpWindow.Close();
        }
    }
}
