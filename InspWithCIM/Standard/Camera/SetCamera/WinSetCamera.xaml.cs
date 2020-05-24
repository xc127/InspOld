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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DealFile;
using Common;
using BasicClass;
using DealConfigFile;
using System.Threading;
using System.Threading.Tasks;

namespace Camera
{
    /// <summary>
    /// SetWork.xaml 的交互逻辑
    /// </summary>
    public partial class WinSetCamera : BaseWindow
    {
        #region 窗体单实例
        private static WinSetCamera g_WinSetCamera = null;
        public static WinSetCamera GetWinInst()
        {
            try
            {
                if (g_WinSetCamera == null)
                {
                    g_WinSetCamera = new WinSetCamera();
                }
                return g_WinSetCamera;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecord", ex);
                return null;
            }
        }

        #endregion 窗体单实例

        #region 初始化
        public WinSetCamera()
        {
            InitializeComponent();

            NameClass = "WinSetCamera";
        }
        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }
        public override void Init()
        {
            try
            {
                setCamera1.Init(ParCamera1.P_I);
                setCamera2.Init(ParCamera2.P_I);
                setCamera3.Init(ParCamera3.P_I);
                setCamera4.Init(ParCamera4.P_I);
                setCamera5.Init(ParCamera5.P_I);
                setCamera6.Init(ParCamera6.P_I);
                setCamera7.Init(ParCamera7.P_I);
                setCamera8.Init(ParCamera8.P_I);
                ShowPar_Invoke();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 初始化

        #region 参数调整
        private void dudNumCamera_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (dudNumCamera.IsMouseOver)
                {
                    ParCameraWork.NumCamera = (int)((double)dudNumCamera.Value);
                    ParCameraWork.P_I.WriteIniNumCamera();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //相机触发模式切换
        private void tsbSoftTrrigger_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                new Task(new Action(() =>
                {
                    SetTriggerMode(true);
                })).Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void tsbSoftTrrigger_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                new Task(new Action(() =>
                {
                    SetTriggerMode(true);
                })).Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void SetTriggerMode(bool blTrigger)
        {
            int numError = 0;
            try
            {
                if (!setCamera1.SetSoftTrrigger(blTrigger))
                {
                    numError++;
                }
                if (!setCamera2.SetSoftTrrigger(blTrigger))
                {
                    numError++;
                }
                if (!setCamera3.SetSoftTrrigger(blTrigger))
                {
                    numError++;
                }
                if (!setCamera4.SetSoftTrrigger(blTrigger))
                {
                    numError++;
                }
                if (!setCamera5.SetSoftTrrigger(blTrigger))
                {
                    numError++;
                }
                if (!setCamera6.SetSoftTrrigger(blTrigger))
                {
                    numError++;
                }
                if (!setCamera7.SetSoftTrrigger(blTrigger))
                {
                    numError++;
                }
                if (!setCamera8.SetSoftTrrigger(blTrigger))
                {
                    numError++;
                }
                if (numError == 0)
                {
                    MessageBox.Show("设置成功");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 参数调整

        #region 显示
        public override void ShowPar()
        {
            try
            {
                dudNumCamera.Value = ParCameraWork.NumCamera;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

        #region 关闭
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("重启软件后，修改的参数才会生效！");
            this.Close();
        }

        private void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            g_WinSetCamera = null;
            GC.Collect();
        }
        #endregion 关闭


    }
}
