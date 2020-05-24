using BasicClass;
using BasicDisplay;
using Common;
using DealFile;
using HalconDotNet;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Camera
{
    /// <summary>
    /// BaseUCDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class BaseUCDisplayCamera : BaseUCDisplay
    {
        #region 定义
        #region Path
        /// <summary>
        /// 保存图片路径
        /// </summary>
        string PathLocalImage
        {
            get
            {
                string root = ComValue.c_PathImageLog + ComConfigPar.C_I.NameModel + "\\";
                try
                {
                    string[] path = Directory.GetDirectories(root);
                    if (path.Length == 0)
                    {
                        return root;
                    }
                    Array.Sort(path);

                    string[] path1 = Directory.GetDirectories(path[path.Length - 1]);
                    if (path1.Length == 0)
                    {
                        return path[path.Length - 1];
                    }
                    Array.Sort(path1);
                    for (int i = 0; i < path1.Length; i++)
                    {
                        if (path1[i].Contains("Camera" + NoCamera.ToString()))
                        {
                            return path1[i];
                        }
                    }
                    return root;
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(NameClass, ex);
                    return root;
                }
            }
        }

        string Path_LocalFolder = "";//从本地文件夹获取图像时的根目录
        #endregion Path

        #region int
        
        #endregion int

        #region bool
        public bool BlLocalImage = false;// 获取本地图片
        public bool BlAutoNextLocal = false;//本地图像自动加载
        public bool BlAutoPreLocal = false;//本地图像自动加载 

        bool BlStateZoom = false;//图像窗体的缩放状态
        public bool BlStateBtnPhoto = true;//操作按钮使能

        bool PreStateWinEvent = false;//当前窗体的事件状态

        public bool BlOpenCamera//相机是否打开
        {
            get
            {
                if (g_CameraBase != null)
                {
                    return g_CameraBase.BlOpen;
                }
                return false;
            }
        }

        bool BlPI = false;//允许加载屏幕截图
        bool BlPIForce = false;//强制屏幕截图
        #endregion bool

        #region double
        
        #endregion double

        ImageAll ImGrabImageHal = null;

        //Class
        public CameraBase g_CameraBase = null;//相机控制类
        public BaseParCamera g_BaseParCamera = null;//相机参数类

        //List

        //enum

        //右键菜单    
        MenuItem MiRealImage = new MenuItem();
        MenuItem MiPhotoImage = new MenuItem();
        MenuItem MiRefershWinHal = new MenuItem();//刷新窗体

        MenuItem MiSaveImage = new MenuItem();
        MenuItem MiLocalImage = new MenuItem();
        MenuItem MiFolder = new MenuItem();
        MenuItem MiEnableInspect = new MenuItem();
        MenuItem MiFit = new MenuItem();
        MenuItem MiCycle = new MenuItem();//本地图片循环运行
        MenuItem MiPI = new MenuItem();//允许加载屏幕截图181124-zx
        MenuItem MiPIForce = new MenuItem();//强制屏幕截图，本地图像时用

        MenuItem MiRestartCamera = new MenuItem();//重启相机

        /// <summary>
        /// 定义事件
        /// </summary>
        public event BoolAction RealImage_event;
        public event Action ShowNewImage_event;
        public event ImAction ShowCVImage_event;//在Canvas里面显示
        #endregion 定义

        #region 初始化
        public BaseUCDisplayCamera()
        {
            InitializeComponent();
            NameClass = "BaseUCDisplayCamera";

            //初始化控件
            g_CcImage = ccImage;
            g_DpImage = dpImage;
            g_LblSaveImage = lblSaveImage;
            g_GpbTitle = gpbCamera;
            g_CvDraw = cvDraw;
            g_ImDisplay = imDisplay;
            g_SvImage = svImage;
            g_HWDispImage = hWDispImage;           
        }

        #region 注册事件
        /// <summary>
        /// 注册事件
        /// </summary>
        public void LoginEvent(bool blMove = true)
        {
            try
            {
                g_CcImage.MouseLeftButtonDown += ccImage_MouseLeftButtonDown;
                g_CcImage.MouseLeftButtonUp += ccImage_MouseLeftButtonUp;
                g_CcImage.MouseMove += ccImage_MouseMove;
                g_CcImage.MouseWheel += ccImage_MouseWheel;

                
                LoginEvent_HalconMouse();
                //鼠标移动事件
                LoginEvent_HalWinMove();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        protected void LoginCallBack()
        {
            //g_CameraBase.Im_event += new ImAction(g_CameraBase_Im_event);
        }
       
        /// <summary>
        /// 注销事件
        /// </summary>
        public void LogoutEvent(bool blMoveLogout = true)
        {
            try
            {
                g_CcImage.MouseLeftButtonDown -= ccImage_MouseLeftButtonDown;
                g_CcImage.MouseLeftButtonUp -= ccImage_MouseLeftButtonUp;
                g_CcImage.MouseMove -= ccImage_MouseMove;
                g_CcImage.MouseWheel -= ccImage_MouseWheel;
                //注销halcon窗体事件
                LogOutEvent_HalconMouse();
                if (blMoveLogout)
                {
                    LogOutEvent_HalconMove();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 注册事件

        public void InitLanguage()
        {

        }
        /// <summary>
        /// 初始化参数
        /// </summary>
        /// <param name="baseParSetDisplay"></param>
        public override void Init(BaseParSetDisplay baseParSetDisplay)
        {
            try
            {
                base.Init(baseParSetDisplay);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 初始化右键菜单
        /// </summary>
        public override void InitContextMenu()
        {
            try
            {
                if (CmOperation.Items.Count != 0)
                {
                    return;
                }
                MiRealImage.Header = "实时画面";
                MiRealImage.Click += new RoutedEventHandler(MiRealImage_Click);
                CmOperation.Items.Add(MiRealImage);

                MiPhotoImage.Header = "拍摄画面";
                MiPhotoImage.Click += new RoutedEventHandler(MiPhotoImage_Click);
                CmOperation.Items.Add(MiPhotoImage);

                MiRefershWinHal.Header = "刷新窗体";
                MiRefershWinHal.Click += new RoutedEventHandler(MiRefershWinHal_Click);
                CmOperation.Items.Add(MiRefershWinHal);
                CmOperation.Items.Add(new Separator());

                MiSaveImage.Header = "保存图像";
                MiSaveImage.Click += new RoutedEventHandler(MiSaveImage_Click);
                CmOperation.Items.Add(MiSaveImage);

                miSaveAs.Header = "图像另存为";
                miSaveAs.Click += new RoutedEventHandler(miSaveAs_Click);
                CmOperation.Items.Add(miSaveAs);
                CmOperation.Items.Add(new Separator());

                MiLocalImage.Header = "本地图像";
                MiLocalImage.Click += new RoutedEventHandler(MiLocalImage_Click);
                CmOperation.Items.Add(MiLocalImage);

                MiFolder.Header = "本地图像文件夹";
                MiFolder.Click += new RoutedEventHandler(MiFolder_Click);
                CmOperation.Items.Add(MiFolder);

                MiCycle.Header = "本地图片循环读取";
                MiCycle.IsChecked = false;
                MiCycle.IsCheckable = true;
                MiCycle.Checked += new RoutedEventHandler(MiCycle_Checked);
                MiCycle.Unchecked += new RoutedEventHandler(MiCycle_Unchecked);
                CmOperation.Items.Add(MiCycle);

                CmOperation.Items.Add(new Separator());
                MiEnableInspect.Header = "查看画面";
                MiEnableInspect.IsChecked = false;
                MiEnableInspect.IsCheckable = true;
                MiEnableInspect.Checked += new RoutedEventHandler(MiEnableInspect_Checked);
                MiEnableInspect.Unchecked += new RoutedEventHandler(MiEnableInspect_Unchecked);
                MiEnableInspect.ToolTip = "缩放平移图像";
                CmOperation.Items.Add(MiEnableInspect);
                
                MiFit.Header = "画面恢复原始尺寸";
                MiFit.Click += new RoutedEventHandler(MiFit_Click);
                CmOperation.Items.Add(MiFit);

                miAssistantSharpe.Header = "辅助绘图";
                miAssistantSharpe.Click += new RoutedEventHandler(miAssistantSharpe_Click);
                CmOperation.Items.Add(miAssistantSharpe);


                CmOperation.Items.Add(new Separator());
                MiPI.Header = "允许加载屏幕截图";
                MiPI.IsChecked = false;
                MiPI.IsCheckable = true;
                MiPI.Checked += new RoutedEventHandler(MiPI_Checked);
                MiPI.Unchecked += new RoutedEventHandler(MiPI_Unchecked);
                CmOperation.Items.Add(MiPI);


                MiPIForce.Header = "强制屏幕截图";
                MiPIForce.IsChecked = false;
                MiPIForce.IsCheckable = true;
                MiPIForce.ToolTip = "强制本地图片模式下进行保存";
                MiPIForce.Checked += new RoutedEventHandler(MiPIForce_Checked);
                MiPIForce.Unchecked += new RoutedEventHandler(MiPIForce_Unchecked);
                CmOperation.Items.Add(MiPIForce);
                CmOperation.Items.Add(new Separator());

                MiRestartCamera.Header = "重启相机";
                MiRestartCamera.Click += new RoutedEventHandler(MiRestartCamera_Click);
                CmOperation.Items.Add(MiRestartCamera);

                CmOperation.Background = Brushes.White;
                gpbCamera.ContextMenu = CmOperation;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }       
      
        /// <summary>
        /// 将按钮的宽度设置为0
        /// </summary>
        public void ZeroWidthLable(bool blLocal, bool blSaveImage, bool blReal)
        {
            try
            {
                if (blLocal)
                {
                    lblLocalImage.Width = 0;
                }
                if (blSaveImage)
                {
                    lblSaveImage.Width = 0;
                }
                if (blReal)
                {
                    lblRealImage.Width = 0;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        #region 右键菜单
        /// <summary>
        /// 拍摄画面
        /// </summary>
        void MiPhotoImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Authority.Authority_e == Authority_enum.Null)
                {
                    MessageBox.Show("没有权限,至少为技术员权限!");
                    return;
                }
                RecoverPhoto_Invoke();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 实时画面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MiRealImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Authority.Authority_e == Authority_enum.Null)
                {
                    MessageBox.Show("没有权限,至少为技术员权限!");
                    return;
                }
                lblRealImage_MouseLeftButtonDown(null,null);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 本地画面
        /// </summary>
        void MiLocalImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Authority.Authority_e == Authority_enum.Null)
                {
                    MessageBox.Show("没有权限,至少为技术员权限!");
                    return;
                }
                lblLocalImage_MouseLeftButtonDown(null, null);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 刷新窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MiRefershWinHal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(BlRealImage)
                {
                    RecoverPhotoOnly();
                    return;
                }
                //如果是Halcon窗体在顶层
                if (g_BlVisibleHWin)
                {
                    ClearHalWin();//清空窗体
                    RefreshHoImage();//刷新图像
                }                
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        #region 保存图像
        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MiSaveImage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                lblSaveImage_MouseLeftButtonDown(null, null);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 图像另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void miSaveAs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Image files (*.jpg)|*.jpg";
                string root = ComValue.c_PathImageLog + ComConfigPar.C_I.NameModel + "\\";
                save.InitialDirectory = root;
                if ((bool)save.ShowDialog())
                {
                    string path = root + save.SafeFileName + ".jpg";
                    SaveHoImage(Ho_Image, "jpg 100", path);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 保存图像

        #region 图像查看：缩放平移
        /// <summary>
        /// 图像控件缩放移动有效
        /// </summary>
        public void MiEnableInspect_Checked(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).IsMouseOver)
            {                
                EnableZoom(true);

                //隐藏弹框
                HidePPResult();
            }
        }
       
        /// <summary>
        /// 使能图像控件缩放移动失效
        /// </summary>
        void MiEnableInspect_Unchecked(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).IsMouseOver)
            {                
                EnableZoom(false);
            }
        }

        public bool EnableZoom(bool blEnable, bool blMoveLogout = true)
        {
            try
            {
                bool pre = BlEnbaleHWinEvent;//记录前一次的设置
                if (blMoveLogout)//注销移动事件
                {
                    EnableHalWin(blEnable, blEnable);
                }
                else//不注销
                {
                    EnableHalWin(blEnable, true);
                }

                if (blEnable)
                {
                    LoginEvent(blEnable);
                    //ShowInfoInvoke("可查看", "red");
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        MiEnableInspect.Foreground = Brushes.Green;
                    }));
                }
                else
                {
                    LogoutEvent(blMoveLogout);
                    //ShowInfoInvoke("不可查看", "red");
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        MiEnableInspect.Foreground = Brushes.Black;
                    }));
                }
                return pre;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }


        /// <summary>
        /// 图像控件恢复
        /// </summary>
        void MiFit_Click(object sender, RoutedEventArgs e)
        {
            BlRecoverImage = true; 
            DispBImageFit();
            Refresh_Rect2();//重绘矩形2

            //隐藏弹框
            HidePPResult();

            new Task(RefreshDelay100HoImage).Start();
        }

        /// <summary>
        /// 使能缩放控件
        /// </summary>
        public void EnableZoomFit(bool blValue)
        {
            if (blValue == true)
            {
                MiFit.IsEnabled = true;
            }
            else
            {
                MiFit.IsEnabled = false;
            }
        }
        #endregion 图像查看：缩放平移

        #region 本地图像
        /// <summary>
        /// 打开本地文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MiFolder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
                folderBrowserDialog.SelectedPath = this.PathRootLocalImage;
                if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Path_LocalFolder = folderBrowserDialog.SelectedPath;
                    List<string> image_L = GetFolderImage(Path_LocalFolder);//所有文件夹下面的图片文件
                    //传入图像路径，设置为本地状态
                    SetLocalState(image_L);

                    //触发事件
                    if (ShowNewImage_event != null)
                    {
                        ShowNewImage_event();
                    }
                }
                //设置状态弹框宽度
                //SetPPStateSize();
                GrabImageAndShow();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 本地图片是否循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MiCycle_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                g_BlCycleLocal = false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        public void MiCycle_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                g_BlCycleLocal = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 本地图像

        #region 屏幕截图
        /// <summary>
        /// 允许加载屏幕截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MiPI_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                BlPI = true;
                this.Dispatcher.Invoke(new Action(() =>
                {
                    MiPI.Foreground = Brushes.Green;
                }));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 不允许
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MiPI_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                BlPI = false;
                this.Dispatcher.Invoke(new Action(() =>
                {
                    MiPI.Foreground = Brushes.Black;
                }));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }


        /// <summary>
        /// 强制截屏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        void MiPIForce_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                BlPIForce = true;
                this.Dispatcher.Invoke(new Action(() =>
                {
                    MiPIForce.Foreground = Brushes.Black;
                }));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void MiPIForce_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                BlPIForce = false;
                this.Dispatcher.Invoke(new Action(() =>
                {
                    MiPIForce.Foreground = Brushes.Black;
                }));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        #endregion 屏幕截图

        #region 重启相机
        void MiRestartCamera_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //先停止实时
                BlRealImage = false;
                new Task(new Action(() =>
                    {
                        Thread.Sleep(200);
                        if (!g_CameraBase.CloseCamera())
                        {
                            WinMsgBox.ShowMsgBox("关闭相机失败!");
                        }
                        else if (!g_CameraBase.OpenCamera())
                        {
                            WinMsgBox.ShowMsgBox("打开相机失败!");
                        }
                        else
                        {
                            WinMsgBox.ShowMsgBox("打开相机成功!");

                            RecoverPhoto_Invoke();
                            GrabImageAndShow();//重新抓取图像                
                        }
                    })).Start();

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 重启相机

        #endregion 右键菜单

        #region 拍摄画面
        /// <summary>
        /// 恢复拍摄画面
        /// </summary>
        void RecoverPhoto()
        {
            try
            {
                //如果是本地图片，则读取一副新的图片进行显示
                if (BlLocalImage)
                {
                    BlLocalImage = false;

                    SetPreNext(false);
                }         

                //如果是实时画面，则退出实时
                if (BlRealImage)
                {
                    BlRealImage = false;                    
                }
                else
                {
                    GrabImageAndShow();//抓取一副图像并且显示
                }
                EnableZoomFit(true);
                lblRealImage.Content = "实时画面";
                SetTitleColor(TitleType_enum.ShowImage);//设置Title颜色，表示状态

                //隐藏弹框
                HidePPResult();

                //隐藏状态弹框
                //ClearPPState();
                try
                {
                    //实时图像
                    RealImage_event(false);
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(NameClass, ex);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        public void RecoverPhoto_Invoke(bool blGrab = true)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(RecoverPhoto));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 不再抓图，仅仅停止
        /// </summary>
        void RecoverPhotoOnly()
        {
            try
            {               
                //如果正处在拍摄画面，则退出
                if (!BlRealImage
                    &&!BlLocalImage)
                {
                    return;
                }

                BlLocalImage = false;
                //如果是实时画面，则退出实时
                if (BlRealImage)
                {
                    BlRealImage = false;
                }

                EnableZoomFit(true);

                lblRealImage.Content = "实时画面";
                SetTitleColor(TitleType_enum.ShowImage);//设置Title颜色，表示状态
                SetPreNext(false);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        public void RecoverPhotoOnly_Invoke()
        {
            try
            {
                //如果正处在拍摄画面，则退出
                if (!BlRealImage
                    && !BlLocalImage)
                {
                    return;
                }
                this.Dispatcher.Invoke(new Action(RecoverPhotoOnly));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 离开实时画面
        /// </summary>
        public bool LeaveReaImage_Invoke()
        {
            try
            {
                if (!BlRealImage)
                {
                    return true;
                }
                this.Dispatcher.Invoke(new Action(LeaveReaImage));
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        /// <summary>
        /// 不再抓图，仅仅停止
        /// </summary>
        void LeaveReaImage()
        {
            try
            {               
                //如果正处在拍摄画面，则退出
                if (!BlRealImage)
                {
                    return;
                }

                BlRealImage = false;

                EnableZoomFit(true);
                lblRealImage.Content = "实时画面";
                SetTitleColor(TitleType_enum.ShowImage);//设置Title颜色，表示状态
                SetPreNext(false);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 设置成拍摄状态
        /// </summary>
        public void SetRecoverPhotoState_Invoke()
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                    {
                        try
                        {                           
                            BlRealImage = false;
                            BlLocalImage = false;
                            EnableZoomFit(true);
                            lblRealImage.Content = "实时画面";
                            SetTitleColor(TitleType_enum.ShowImage);//设置Title颜色，表示状态
                            SetPreNext(false);
                            lblTactTime.Content = 0;
                        }
                        catch (Exception ex)
                        {
                            Log.L_I.WriteError(NameClass, ex);
                        }
                    }));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 拍摄画面

        #region 获取本地图片
        /// <summary>
        /// 打开对话框，获取本地图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lblLocalImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Authority.Authority_e == Authority_enum.Null)
                {
                    lblLocalImage.RefreshLB("无权限", false);
                    return;
                }
                //隐藏弹框
                HidePPResult();
                

                //如果是实时图像，则先退出
                if (BlRealImage)
                {
                    RecoverPhotoOnly();
                    return;
                }

                //设置状态弹框的宽度
                //SetPPStateSize();

                ////隐藏状态信息
                //HidePPState();

                //如果获取图像成功，设置为本地图像状态
                if (GetLocalImage())
                {
                    //打开本地图片停止缩放
                    BlStateZoom = EnableZoom(false);

                    NoLocalImage = -1;//本地图像序号清零
                    SetLocalState();//设置处于本地图像状态
                    //抓取并显示图像
                    new Task<ImageAll>(GrabImageAndShow).Start();

                    //还原缩放
                    new Task(new Action(() =>
                        {
                            Thread.Sleep(1000);
                            if (BlStateZoom)
                            {
                                EnableZoom(true);
                            }

                        })).Start();

                    //触发事件
                    if (ShowNewImage_event != null)
                    {
                        ShowNewImage_event();
                    }
                }

                //显示状态信息
                //VisiblePPState();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 打开对话框，获取本地文件
        /// </summary>
        /// <returns></returns>
        bool GetLocalImage()
        {
            try
            {
                OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

                openFileDialog.FileName = "";
                openFileDialog.Filter = "Images (*.jpg,*.bmp)|*.jpg;*.bmp";
                string strPath = PathLocalImage;//打开保存图片的跟目录
                string pathOld = RegeditFile.R_I.ReadRegedit("LocalPathCamera" + this.NoCamera.ToString());
                if (Directory.Exists(pathOld))
                {
                    strPath = pathOld;
                }
                // 判断是否存在目录夹
                if (Directory.Exists(strPath) == false)
                {
                    Directory.CreateDirectory(strPath);
                }
                openFileDialog.InitialDirectory = strPath;
                openFileDialog.Multiselect = true;

                if (!(bool)openFileDialog.ShowDialog())
                {
                    return false;
                }

                if (openFileDialog.FileName.Trim() == "")
                {
                    MessageBox.Show("文件名不能为空！");
                    return false;
                }

                string[] paths = openFileDialog.FileNames;

                //把历史路径写入注册表
                if (paths.Length != 0)
                {
                    int split = paths[0].LastIndexOf('\\');
                    RegeditFile.R_I.WriteRegedit("LocalPathCamera" + this.NoCamera.ToString(), paths[0].Substring(0, split));
                }

                g_PathLocalImage_L.Clear();
                for (int i = 0; i < paths.Length; i++)
                {
                    if (BlPI)//允许加载屏幕截图
                    {
                        g_PathLocalImage_L.Add(paths[i]);
                    }
                    else
                    {
                        if (!paths[i].Contains("PI"))//屏幕截图
                        {
                            g_PathLocalImage_L.Add(paths[i]);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 从指定路径加载图像
        /// </summary>
        public void LoadLocalImage(string path)
        {
            try
            {
                BlLocalImage = true;
                g_PathLocalImage_L.Clear();
                g_PathLocalImage_L.Add(path);

                this.Dispatcher.Invoke(new Action(() =>
                    {
                        SetLocalState();
                    }));

                //显示图像
                GrabImageAndShow();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 获取指定路径下的所有文件,采用递归的方式
        /// </summary>
        /// <param name="path"></param>
        List<string> GetFolderImage(string path)
        {
            try
            {
                List<string> image_L = new List<string>();
                //判断目录深度
                int num = ((path.Replace(Path_LocalFolder, "")).Split('\\')).Length;
                if (num > 6)
                {
                    return image_L;
                }
                string name = "Camera" + NoCamera.ToString();
                if (path.Contains(name))
                {
                    string[] files = Directory.GetFiles(path);
                    foreach (string item in files)
                    {
                        if (BlPI)//允许加载屏幕截图
                        {
                            if (item.Contains("jpg")
                            || item.Contains("bmp"))
                            {
                                image_L.Add(item);
                            }
                        }
                        else
                        {
                            if (item.Contains("PI"))
                            {
                                continue;
                            }
                            if (item.Contains("jpg")
                            || item.Contains("bmp"))
                            {
                                image_L.Add(item);
                            }
                        }
                    }
                }
                DirectoryInfo[] dirImage = new DirectoryInfo(path).GetDirectories();
                if (dirImage.Length != 0)//如果依然包含文件夹
                {
                    foreach (DirectoryInfo item in dirImage)
                    {
                        List<string> imageChild_L = GetFolderImage(item.FullName);

                        image_L.AddRange(imageChild_L);
                    }
                }
                return image_L;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }

        /// <summary>
        /// 设置为本地图像状态,并传入文件路径
        /// </summary>
        /// <param name="image_L"></param>
        public void SetLocalState(List<string> image_L)
        {
            try
            {
                if (image_L.Count == 0)
                {
                    MessageBox.Show("文件夹内没有图片文件");
                    return;
                }

                lblRealImage.Content = "拍摄画面";//设置为拍摄画面是为了推出本地

                BlLocalImage = true;//指示为本地图像状态

                g_PathLocalImage_L.Clear();
                g_PathLocalImage_L = image_L;
                g_PathLocalImage_L.Sort();

                SetTitleColor(TitleType_enum.LocalImage);
                VisiblePreNext();//显示prenext按钮              
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 设置为本地图片状态
        /// </summary>
        public void SetLocalState()
        {
            try
            {
                BlLocalImage = true;
                lblRealImage.Content = "拍摄画面";//设置为拍摄画面是为了推出本地
                
                SetTitleColor(TitleType_enum.LocalImage);
                VisiblePreNext();//显示prenext按钮     
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 点击显示下一幅图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lblNext_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                new Task(new Action(() =>
                    {
                        BlAutoNextLocal = false;
                        BlAutoPreLocal = false;
                        NoLocalImage++;
                        GrabImageAndShow();
                        VisiblePreNext();
                    })).Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 点击显示上一幅图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void lblPre_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                new Task(new Action(() =>
                    {
                        BlAutoNextLocal = false;
                        BlAutoPreLocal = false;
                        NoLocalImage--;
                        GrabImageAndShow();
                        VisiblePreNext();
                    })).Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 设置本地图片翻页按钮显示与隐藏
        /// </summary>
        /// <param name="blVisible"></param>
        public void SetPreNext(bool blVisible)
        {
            try
            {
                if (blVisible)
                {
                    lblPre.Visibility = Visibility.Visible;
                    lblNext.Visibility = Visibility.Visible;
                }
                else
                {
                    lblPre.Visibility = Visibility.Hidden;
                    lblNext.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 根据本地图片的序号显示响应
        /// </summary>
        void VisiblePreNext()
        {
            try
            {
                if (g_BlCycleLocal)
                {
                    VisiblePre(true);
                    VisibleNext(true);
                    return;
                }
                if (NoLocalImage == g_PathLocalImage_L.Count - 1)
                {
                    VisiblePre(true);
                    VisibleNext(false);
                }
                else if (NoLocalImage == 0)
                {
                    VisiblePre(false);
                    VisibleNext(true);
                }
                else
                {
                    VisiblePre(true);
                    VisibleNext(true);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void VisiblePre(bool blVisible)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                    {
                        if (blVisible)
                        {
                            lblPre.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            lblPre.Visibility = Visibility.Hidden;
                        }
                    }));
             
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void VisibleNext(bool blVisible)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                    {
                        if (blVisible)
                        {
                            lblNext.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            lblNext.Visibility = Visibility.Hidden;
                        }
                    }));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 获取本地图片

        #region 回调画面
        void g_CameraBase_Im_event(ImageAll im)
        {
            try
            {
                //显示图像以及字符
                DispImage(im);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 回调画面

        #region 实时画面
        /// <summary>
        /// 控制实时画面和拍摄画面
        /// </summary>
        public void lblRealImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {       
                if (Authority.Authority_e == Authority_enum.Null)
                {
                    lblRealImage.RefreshLB("无权限", false);
                    return;
                }

                //是否使用触发模式
                if (g_BaseParCamera.BlUsingTrigger
                    && g_BaseParCamera.TriggerSource_e != TriggerSourceCamera_enum.Software)
                {
                    MessageBox.Show("触发模式，不允许使用实时拍照模式");
                    return;
                }               

                //隐藏prenext按钮  
                SetPreNext(false);
                //隐藏结果弹框
                HidePPResult();
                //隐藏状态弹框
                //ClearPPState();

                if (lblRealImage.Content.ToString() == "拍摄画面")
                {
                    //停止实时显示
                    RecoverPhoto_Invoke();
                   
                    new Task(new Action(() => { Thread.Sleep(300); GrabImageAndShow(); })).Start();

                    //还原缩放
                    new Task(new Action(() =>
                    {
                        Thread.Sleep(1000);
                        if (BlStateZoom)
                        {
                            EnableZoom(true);
                        }

                    })).Start();
                }
                else
                {
                    BlStateZoom = EnableZoom(false);
                    RecoverPhoto_Invoke();//退出本地图像状态
                    SetRealImage();//设定实时画面
                }

                //触发事件
                if (ShowNewImage_event != null)
                {
                    ShowNewImage_event();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 设定实时画面
        /// </summary>
        public void SetRealImage_Invoke()
        {
            try
            {
                this.Dispatcher.Invoke(new Action(SetRealImage));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
       
        public void SetRealImage()
        {
            try
            {
                //设定实时显示状态
                SetTitleColor(TitleType_enum.RealImage);
                lblRealImage.Content = "拍摄画面";

                //禁止对画面进行缩放拖动
                EnableZoomFit(false);
                MiEnableInspect.IsChecked = false;

                try
                {
                    //实时图像
                    RealImage_event(true);
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(NameClass, ex);
                }
                new Task(RealImage).Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 开始实时图像
        /// </summary>
        void RealImage()
        {
            int num = 0;
            try
            {
                Stopwatch swTime = new Stopwatch();
                swTime.Start();//打开计时
                BlRealImage = true;                
                while (BlRealImage)
                {
                    Thread.Sleep(10);//防止出现UI假死
                    //软触发的时候，多延迟
                    if (g_BaseParCamera.TriggerSource_e == TriggerSourceCamera_enum.Software
                        && g_BaseParCamera.BlUsingTrigger)
                    {
                        Thread.Sleep(50);
                    }

                    swTime.Restart();//重新打开计时
                    ImageAll imAll = GrabImageAndShow();//抓取并显示图像
                    if (imAll.Ho_Image == null
                        && imAll.BitmapSource == null)
                    {
                        return;
                    }
                    swTime.Stop();
                    long t = swTime.ElapsedMilliseconds + 10;//不能刷新太快
                    num++;
                    if (t < 100)
                    {
                        if (num > 10)
                        {
                            num = 0;
                            ShowInfoInvoke(t.ToString() + "ms", "blue");
                        }
                    }
                    else
                    {
                        num = 0;
                        ShowInfoInvoke(t.ToString() + "ms", "blue");
                    }

                    if (Ho_Image != null)//释放资源
                    {
                        Ho_Image.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 实时画面

        #region 保存图像
        /// <summary>
        /// 按照型号和相机序号命名文件夹，对图像进行保存
        /// </summary>
        public override void lblSaveImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Authority.Authority_e == Authority_enum.Null)
                {
                    g_LblSaveImage.RefreshLB("无权限", false);
                    return;
                }
                //线退出实时
                if (BlRealImage)
                {
                    RecoverPhotoOnly();
                    lblSaveImage.RefreshLB("退出实时", true);
                    return;
                }
                ImageAll imageAll = GrabImageAndShow();//先抓取图像
                if (SaveImageDefaultPath(imageAll))
                {
                    lblSaveImage.RefreshLB("保存成功", true);
                }
                else
                {
                    lblSaveImage.RefreshLB("保存失败", false);
                    MessageBox.Show("保存失败");
                }
            }
            catch (Exception ex)
            {
                lblSaveImage.RefreshLB("保存失败", false);
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 保存图像

        #region 抓取图像
        /// <summary>
        /// 第一次加载图像 
        /// </summary>
        /// <returns></returns>
        public ImageAll GrabImageAndShowHalcon()
        {
            try
            {               
                ImageAll imageAll = null;
                if (g_BlVisibleHWin)//如果时Halcon窗体，则只抓取Halcon图像
                {
                    imageAll = GrabImageHal();
                }
                else
                {
                    imageAll = new ImageAll();
                }
                //显示图像以及字符
                DispImageHalcon(imageAll);
                return imageAll;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                ShowAlarm("相机" + NoCamera.ToString() + "抓取图像失败!");
                return null;
            }
        }

        /// <summary>
        /// 图像抓取并且显示
        /// </summary>
        /// <returns></returns>
        public ImageAll GrabImageAndShow()
        {
            try
            {                
                ImageAll imageAll = null;
                if (g_BlVisibleHWin)//如果时Halcon窗体，则只抓取Halcon图像
                {
                    imageAll = GrabImageHal();
                }
                else//抓取所有的格式
                {
                    imageAll = GrabImageAll();
                }
                //显示图像以及字符
                DispImage(imageAll);
                if (imageAll == null)
                {
                    ShowAlarm("相机" + NoCamera.ToString() + "抓取图像失败!");
                }
                return imageAll;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                ShowAlarm("相机" + NoCamera.ToString() + "抓取图像失败!");
                return null;
            }
            finally
            {
                if (!g_BlVisibleHWin)
                {
                    //触发事情响应，采集的是CV显示的图像
                    if (ShowCVImage_event != null)
                    {
                        //ShowCVImage_event(imageAll);
                    }
                }
            }
        }
      

        /// <summary>
        /// 抓取图像包括Halcon和BitmapSource
        /// </summary>
        public ImageAll GrabImageAll()
        {
            try
            {
                if (ImGrabImageHal != null)
                {
                    ImGrabImageHal.Dispose();
                }
                //如果是获取本地图片,本地图像不进行显示变换
                if (BlLocalImage)
                {
                    ImGrabImageHal = GrabImageLocalAll();
                    if (ImGrabImageHal == null)
                    {
                        ShowAlarm("相机" + NoCamera.ToString() + "抓取图像失败!");
                    }
                    ImGrabImageHal.BlLocalImage = true;//图像为本地图像
                    return ImGrabImageHal;
                }
                else
                {
                    //如果是软触发，则先给出软触发信号
                    if (g_BaseParCamera.TriggerSource_e == TriggerSourceCamera_enum.Software
                        && g_BaseParCamera.BlUsingTrigger)
                    {
                        g_CameraBase.TriggerSoftware(true);//触发需要获取bitmap
                    }

                    ImGrabImageHal = g_CameraBase.GrabImageAll();//从相机抓取图像    
                    if (ImGrabImageHal == null)
                    {
                        ShowAlarm("相机" + NoCamera.ToString() + "抓取图像失败!");
                    }
                    //对图像进行翻转平移等变换
                    ImGrabImageHal = TranslateImage(ImGrabImageHal);
                    return ImGrabImageHal;
                }   
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                ShowAlarm("相机" + NoCamera.ToString() + "抓取图像失败!");
                return null;
            }
        }

        /// <summary>
        /// 只抓取Halcon图像
        /// </summary>
        public ImageAll GrabImageHal()
        {
            //Stopwatch s = new Stopwatch();
            try
            {
                ImGrabImageHal = new ImageAll();
                if (ImGrabImageHal != null)
                    //&& !g_CameraBase.BlTrriger)
                {
                    ImGrabImageHal.Dispose();
                }
                //如果是获取本地图片
                if (BlLocalImage)
                {
                    //s.Restart();
                    ImGrabImageHal = GrabImageLocalHal();

                    if (ImGrabImageHal == null)
                    {
                        ShowAlarm("相机" + NoCamera.ToString() + "抓取图像失败!");
                    }
                    ImGrabImageHal.BlLocalImage = true;//图像为本地图像
                    base.ho_Image = ImGrabImageHal.Ho_Image;
                    return ImGrabImageHal;
                }
                else
                {
                    //如果是软触发，则先给出软触发信号
                    if (g_BaseParCamera.TriggerSource_e == TriggerSourceCamera_enum.Software
                        && g_BaseParCamera.BlUsingTrigger)
                    {
                        g_CameraBase.TriggerSoftware();
                    }

                    ImGrabImageHal = g_CameraBase.GrabImageHal();//从相机抓取图像
                    if (ImGrabImageHal == null)
                    {
                        ShowAlarm("相机" + NoCamera.ToString() + "抓取图像失败!");
                    }
                    //对图像进行翻转平移等变换
                    return TranslateImage(ImGrabImageHal);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                ShowAlarm("相机" + NoCamera.ToString() + "抓取图像失败!");
                return null;
            }
            finally
            {
                //long t = s.ElapsedMilliseconds;
            }
        }

        /// <summary>
        /// 只抓取Bitmap图像
        /// </summary>
        public ImageAll GrabImageBitmap()
        {
            ImageAll imageAll = null;
            try
            {
                //如果是获取本地图片
                if (BlLocalImage)
                {
                    imageAll = GrabImageLocalHal();
                    imageAll.BlLocalImage = true;//图像为本地图像
                    base.ho_Image = imageAll.Ho_Image;
                    return imageAll;
                }
                else
                {
                    imageAll = g_CameraBase.GrabImageHal();//从相机抓取图像

                    return TranslateImage(imageAll);//对图像进行翻转平移等变换
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }

        /// <summary>
        /// 抓取本地图像,包含所有的格式
        /// </summary>
        public ImageAll GrabImageLocalAll()
        {
            try
            {
                ImageAll imageAll = new ImageAll();
                HObject ho_Image = null;

                if (BlAutoNextLocal)//下一幅
                {
                    NoLocalImage++;
                }
                else if (BlAutoPreLocal)//上一幅
                {
                    NoLocalImage--;
                }
                else
                {
                    //NoLocalImage = 0;
                }

                if (g_PathLocalImage_L.Count < NoLocalImage)
                {
                    NoLocalImage = g_PathLocalImage_L.Count - 1;
                }
                if (NoLocalImage == -1)
                {
                    NoLocalImage = 0;
                }

                try
                {
                    HOperatorSet.ReadImage(out ho_Image, g_PathLocalImage_L[NoLocalImage]);//读取halcon类型图片
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(NameClass, ex);
                }

                Stopwatch sw = new Stopwatch();
                sw.Start();

                BitmapSource bitmapsource = new BitmapImage(new Uri(g_PathLocalImage_L[NoLocalImage]));//读取bitmapsource类型

                bitmapsource.Freeze();
                imageAll.BitmapSource = bitmapsource;
                imageAll.Ho_Image = ho_Image;
                imageAll.Time = g_PathDirectory.GetTimeName();//图像抓取时间
                imageAll.TimeShort = g_PathDirectory.GetShortTimeName();//图像抓取短时间

                sw.Stop();
                double t = sw.ElapsedMilliseconds;
                return imageAll;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }

        /// <summary>
        /// 抓取本地图像,只包含Halcon图像
        /// </summary>
        /// <returns></returns>
        public ImageAll GrabImageLocalHal()
        {
            try
            {
                if (g_PathLocalImage_L.Count == 0)
                {
                    return null;
                }

                if (BlAutoNextLocal)//下一幅
                {
                    NoLocalImage++;
                }
                else if (BlAutoPreLocal)//上一幅
                {
                    NoLocalImage--;
                }
                else
                {
                   // NoLocalImage = 0;
                }

                if (g_PathLocalImage_L.Count < NoLocalImage
                    || g_PathLocalImage_L.Count == NoLocalImage)
                {
                    NoLocalImage = g_PathLocalImage_L.Count - 1;
                }
                if (NoLocalImage == -1)
                {
                    NoLocalImage = 0;
                }
                HObject ho_Image = null;
                HOperatorSet.ReadImage(out ho_Image, g_PathLocalImage_L[NoLocalImage]);//读取halcon类型图片
                //NoLocalImage = -1;

                ImageAll imageAll = new ImageAll(ho_Image);
                imageAll.Time = g_PathDirectory.GetTimeName();//图像抓取时间
                imageAll.TimeShort = g_PathDirectory.GetShortTimeName();//图像抓取短时间
                imageAll.BlLocalImage = true;
                return imageAll;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }
        #endregion 抓取图像        

        #region 设置相机
        /// <summary>
        /// /曝光时间
        /// </summary>
        /// <param name="dblValue"></param>
        /// <returns></returns>
        public bool SetExposure(double dblValue)
        {
            return g_CameraBase.SetExposure(dblValue);
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        /// <param name="dblValue"></param>
        /// <returns></returns>
        public bool SetGain(double dblValue)
        {
            return g_CameraBase.SetGain(dblValue);
        }

        /// <summary>
        /// 保存参数
        /// </summary>
        /// <returns></returns>
        public bool SavePar()
        {
            return g_CameraBase.SaveParChannel();
        }
        #endregion 设置相机

        #region 鼠标事件
        private void cvDraw_MouseMove(object sender, MouseEventArgs e)
        {
            g_DrawDisplay.g_CanvasDraw_MouseMove(sender, e);
        }
        #endregion 鼠标事件

        #region 控件使能
        /// <summary>
        /// 使能控制按钮
        /// </summary>
        /// <param name="enable"></param>
        public new void EnableBtnPhotoCtr(bool enable)
        {
            try
            {
                BlStateBtnPhoto = enable;//操作按钮使能

                dpBtn.IsEnabled = enable;
                gpbCamera.ContextMenu.IsEnabled = enable;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion  控件使能

        #region 截屏 181124-zx
        /// <summary>
        /// 截取窗体屏幕图像,将分辨率升到1500左右
        /// </summary>
        /// <returns></returns>
        public ImageAll DumpWinImage()
        {
            ImageAll im = new ImageAll();
            HObject ho_Image = null;
            HObject ho_ImageZoom = null;
            HTuple ht_Width = 0;
            HTuple ht_Height = 0;
            try
            {
                HOperatorSet.DumpWindowImage(out ho_Image, hWDispImage.HalconWindow);
                HOperatorSet.GetImageSize(ho_Image, out ht_Width, out ht_Height);
                int zoom = 1500 / ht_Height;
                HOperatorSet.ZoomImageFactor(ho_Image, out ho_ImageZoom, zoom, zoom, "constant");
                im.Ho_Image = ho_ImageZoom;//保存完图片后释放

                PathDirectory pathDirectory = new PathDirectory();
                im.Time = pathDirectory.GetTimeName();
                im.TimeShort = pathDirectory.GetShortTimeName();
                im.Name = "PI";//PrintImage
                return im;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
            finally
            {
                if (ho_Image != null)
                {
                    ho_Image.Dispose();
                }
            }
        }
        #endregion 截屏

        #region 显示
        /// <summary>
        /// 显示标题栏颜色
        /// </summary>
        /// <param name="blResult"></param>
        public void SetTileColor(bool blResult)
        {
            if (blResult)
            {
                if (BlLocalImage)
                {
                    SetTileColor(TitleType_enum.LocalImage);
                }
                else
                {
                    SetTileColor(TitleType_enum.ImagePRight);
                }
            }
            else
            {
                SetTileColor(TitleType_enum.ImagePError);
            }
        }

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="str"></param>
        /// <param name="strColor"></param>
        public override void ShowInfo(string str, string strColor)
        {
            try
            {
                lblTactTime.Content = str;
                if (strColor == "red")
                {
                    lblTactTime.Foreground = Brushes.Red;
                }
                else
                {
                    lblTactTime.Foreground = Brushes.Blue;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 显示查看图像时用来显示灰度的Label，20181110-zx
        /// </summary>
        public void ShowGrayLabel()
        {
            try
            {
                lblPosGray.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

        #region 关闭相机
        public bool CloseCamera()
        {
            try
            {
                 return g_CameraBase.CloseCamera();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 关闭相机

       
    }
}
