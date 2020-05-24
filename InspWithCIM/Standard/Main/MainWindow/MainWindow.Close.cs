using System;
using System.Windows;
using System.Threading;
using System.Diagnostics;
using BasicClass;

namespace Main
{
    partial class MainWindow
    {
        //关闭软件各个功能
        void CloseWork()
        {
            SendCloseToPLC();
            //事件注销
            EventLogout_Camera();
            EventLogout_ImageProcess();
            EventLogOut_Others();

            //关闭相机
            Close_Camera();
            //关闭机器人
            Close_Robot();
            //关闭DealPLC
            Close_PLC();
            //关闭Custom
            Close_Custom();
            //关闭CIM
            CloseCIM();

            CloseIO();
            Thread.Sleep(200);
            CloseMotionCtrler();
            //this.Close();
        }



        /// <summary>
        /// 软件重启
        /// </summary>
        void RestartSoft()
        {
            try
            {
                if (MessageBox.Show("是否重启软件？", "确认信息", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                {
                    return;
                }     
                //关闭软件工作
                CloseWork();
                System.Windows.Forms.Application.Restart();
                //主界面退出的时候关闭程序
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                Application.Current.Shutdown();
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
      
        /// <summary>
        /// 软件关闭
        /// </summary>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否关闭软件？", "确认信息", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                {
                    e.Cancel = true;
                    return;
                }

                //关闭软件工作
                CloseWork();

                ////主界面退出的时候关闭程序
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 电脑关机
        /// </summary>
        void ShutDownPC()
        {
            try
            {
                if (MessageBox.Show("是否关闭电脑？", "确认信息", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                {
                    return;
                }     
                //关闭软件
                CloseWork();

                Process p = new Process();
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = "cmd";
                p.StartInfo.Arguments = "/c shutdown -s -t 0";
                p.Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 电脑重启
        /// </summary>
        void RestartPC()
        {
            try
            {
                if (MessageBox.Show("是否关闭电脑？", "确认信息", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                {
                    return;
                }     
                Process p = new Process();
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = "cmd";
                p.StartInfo.Arguments = "/c shutdown -r -t 0";
                p.Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ShowState("软件关闭");
        }

    }
}
