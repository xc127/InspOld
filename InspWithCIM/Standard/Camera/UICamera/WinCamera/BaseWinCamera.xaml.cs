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
    /// WinCamera.xaml 的交互逻辑
    /// </summary>
    public partial class BaseWinCamera : BaseMetroWindow
    {
        #region 定义
        public BaseUCDisplayCamera g_BaseUCDisplayCamera = null;

        /// <summary>
        /// 窗体是否可以改变尺寸
        /// </summary>
        public ResizeMode ResizeMode_e
        {
            get
            {
                ResizeMode resizeMode = ResizeMode.NoResize;

                if (!BlRealImage)
                {
                    resizeMode = ResizeMode.CanResize;
                }

                return resizeMode;
            }
            set
            {

            }
        }

        public bool BlRealImage
        {
            get
            {
                if (g_BaseUCDisplayCamera != null)
                {
                    return g_BaseUCDisplayCamera.BlRealImage;                    
                }
                return false;
            }
        }
        #endregion 定义

        #region 初始化
        public BaseWinCamera()
        {
            InitializeComponent();   
           
        }
        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
        public void Init()
        {
            AddChildCtr();
            InitLocation();//设置窗体位置
            InitSize();
            Init_event();
        }
        #endregion 初始化

        #region 加载控件
        void AddChildCtr()
        {
            try
            {
                //设定布局方式
                g_BaseUCDisplayCamera.HorizontalAlignment = HorizontalAlignment.Stretch;
                g_BaseUCDisplayCamera.VerticalAlignment = VerticalAlignment.Stretch;
                g_BaseUCDisplayCamera.Margin = new Thickness(10);
                g_BaseUCDisplayCamera.Background = Brushes.White;
                g_BaseUCDisplayCamera.BorderThickness = new Thickness(1);
                g_BaseUCDisplayCamera.BorderBrush = Brushes.AliceBlue;
                //添加控件
                this.gdCtr.Children.Add(g_BaseUCDisplayCamera);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera", ex);
            }
        }
        #endregion 加载控件

        #region 初始化事件
        public void Init_event()
        {
            try
            {
                g_BaseUCDisplayCamera.RealImage_event += new BoolAction(g_BaseUCDisplayCamera_RealImage_event);
            }
            catch (Exception ex)
            {
                 Log.L_I.WriteError("WinCamera", ex);
            }
        }

        void g_BaseUCDisplayCamera_RealImage_event(bool bl)
        {
            this.Dispatcher.Invoke(new BoolAction(ShowResize),bl);
        }

        void ShowResize(bool blRealImage)
        {
            try
            {
                if (blRealImage)
                {
                    this.ResizeMode = ResizeMode.NoResize;
                }
                else
                {
                    this.ResizeMode = ResizeMode.CanResize;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera", ex);
            }
        }
        #endregion 初始化事件

        #region 记录窗体位置
        /// <summary>
        /// 设置窗体位置
        /// </summary>
        public void InitLocation()
        {
            try
            {
                this.Left=RegeditCamera.R_I.ReadRegeditDbl(this.NameClass + "Left");
                this.Top=RegeditCamera.R_I.ReadRegeditDbl(this.NameClass + "Top");               
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
      
        
        /// <summary>
        /// 窗体位置变动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseMetroWindow_LocationChanged(object sender, EventArgs e)
        {
            if (this.IsMouseOver)
            {
                RegeditCamera.R_I.WriteRegedit(this.NameClass + "Left", this.Left.ToString());
                RegeditCamera.R_I.WriteRegedit(this.NameClass + "Top", this.Top.ToString());
              
            }
        }

        /// <summary>
        /// 窗体大小变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseMetroWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.IsMouseOver)
            {
                RegeditCamera.R_I.WriteRegedit(this.NameClass + "WindowState", this.WindowState.ToString());

                RegeditCamera.R_I.WriteRegedit(this.NameClass + "Width", this.Width.ToString());
                RegeditCamera.R_I.WriteRegedit(this.NameClass + "Height", this.Height.ToString());
            }
        }

        void InitSize()
        {
            try
            {
                double width = RegeditCamera.R_I.ReadRegeditDbl(this.NameClass + "Width");
                double height = RegeditCamera.R_I.ReadRegeditDbl(this.NameClass + "Height");

                if (width * height != 0)
                {
                    this.Width = width;
                    this.Height=height;
                }

                WindowState winstate = WindowState.Normal;
                try 
	            {
                    string type = RegeditCamera.R_I.ReadRegedit(this.NameClass + "WindowState");
                    winstate = (WindowState)Enum.Parse(typeof(WindowState), type);
	            }
	            catch (Exception ex)
	            {
                    Log.L_I.WriteError(NameClass, ex);
	            }

                this.WindowState = winstate;
            }
            catch           
            {
                
            }          
        }
        #endregion 记录窗体位置

        #region 显示
        public void ShowState(bool blState)
        {
            try
            {
                g_BaseUCDisplayCamera.ShowState_Invoke(blState);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }    
        #endregion 显示

        #region 关闭
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (g_BaseUCDisplayCamera.BlRealImage)
                {
                    g_BaseUCDisplayCamera.RecoverPhoto_Invoke();
                    e.Cancel = true;
                }
                else
                {
                    base.OnClosing(e);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera", ex);
            }
        }

        public void Close()
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera1", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            g_BaseUCDisplayCamera.RecoverPhoto_Invoke();
            g_BaseUCDisplayCamera = null;
        }
        #endregion 关闭

    }
}
