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
using System.Windows.Shapes;
using BasicClass;
using System.Threading;
using System.Threading.Tasks;
using BasicDisplay;

namespace Camera
{
    /// <summary>
    /// WinCameraROI.xaml 的交互逻辑
    /// </summary>
    public partial class WinCameraROI : BaseMetroWindow
    {
        #region 窗体单实例
        public static WinCameraROI g_WinCameraROI = null;
        public static WinCameraROI GetWinInst()
        {
            g_MtCameraROI.WaitOne();
            try
            {
                if (g_WinCameraROI == null)
                {
                    g_WinCameraROI = new WinCameraROI();
                }
                return g_WinCameraROI;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCameraROI", ex);
                return null;
            }
            finally
            {
                g_MtCameraROI.ReleaseMutex();
            }
        }
        #endregion 窗体单实例

        #region 定义
        public BaseUCDisplayCamera g_BaseUCDisplayCamera = null;

        //线程互斥
        static Mutex g_MtCameraROI = new Mutex();

        //event 
        public event Action ChangeSize_event;
        #endregion 定义

        #region 初始化
        public WinCameraROI()
        {
            try
            {
                InitializeComponent();
                LocationLeft();//靠左停靠

                NameClass = "WinCameraROI";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (g_BaseUCDisplayCamera.BlLocalImage)
                {
                    g_BaseUCDisplayCamera.SetLocalState();
                }
                new Task(new Action(GrabImage)).Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        public void Init(BaseUCDisplayCamera baseUCDisplayCamera)
        {
            try
            {
                if (g_BaseUCDisplayCamera != null)
                {
                    return;
                }
                BaseParSetDisplay baseParSetDisplay = null;
                switch (baseUCDisplayCamera.NoCamera)
                {
                    case 1:
                        g_BaseUCDisplayCamera = new UCDisplayCamera1(false);
                        baseParSetDisplay = ParSetDisplay.P_I["Camera1"];
                        break;

                    case 2:
                        g_BaseUCDisplayCamera = new UCDisplayCamera2(false);
                        baseParSetDisplay = ParSetDisplay.P_I["Camera2"];
                        break;

                    case 3:
                        g_BaseUCDisplayCamera = new UCDisplayCamera3(false);
                        baseParSetDisplay = ParSetDisplay.P_I["Camera3"];
                        break;

                    case 4:
                        g_BaseUCDisplayCamera = new UCDisplayCamera4(false);
                        baseParSetDisplay = ParSetDisplay.P_I["Camera4"];
                        break;

                    case 5:
                        g_BaseUCDisplayCamera = new UCDisplayCamera5(false);
                        baseParSetDisplay = ParSetDisplay.P_I["Camera5"];
                        break;

                    case 6:
                        g_BaseUCDisplayCamera = new UCDisplayCamera6(false);
                        baseParSetDisplay = ParSetDisplay.P_I["Camera6"];
                        break;
                }
                //窗口的显示参数 
                g_BaseUCDisplayCamera.Init(baseParSetDisplay);

                g_BaseUCDisplayCamera.HorizontalAlignment = HorizontalAlignment.Stretch;
                g_BaseUCDisplayCamera.VerticalAlignment = VerticalAlignment.Stretch;
                g_BaseUCDisplayCamera.Margin = new Thickness(2, 2, 2, 2);
                gdRoot.Children.Add(g_BaseUCDisplayCamera);

                //同步本地图像
                g_BaseUCDisplayCamera.BlLocalImage = baseUCDisplayCamera.BlLocalImage;
                g_BaseUCDisplayCamera.g_PathLocalImage_L = baseUCDisplayCamera.g_PathLocalImage_L;
                g_BaseUCDisplayCamera.NoLocalImage = baseUCDisplayCamera.NoLocalImage;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        #region 抓取图像
        void GrabImage()
        {
            try
            {
                Thread.Sleep(800);
                g_BaseUCDisplayCamera.g_BlVisibleHWin = false;
                g_BaseUCDisplayCamera.GrabImageAndShow();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 抓取图像

        #region 关闭
        public void CLose()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        private void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (g_BaseUCDisplayCamera != null)
                {
                    g_BaseUCDisplayCamera.RecoverPhotoOnly_Invoke();
                    g_BaseUCDisplayCamera = null;
                }
                g_WinCameraROI = null;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 关闭
    }
}
