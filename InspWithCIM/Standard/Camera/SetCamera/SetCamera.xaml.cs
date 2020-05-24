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
using Common;
using BasicClass;
using DealLog;

namespace Camera
{
    /// <summary>
    /// UISetCamera.xaml 的交互逻辑
    /// </summary>
    public partial class SetCamera : BaseControl
    {
        #region 定义
        //int

        //class
        public BaseParCamera g_ParCamera = null;

        CameraBase g_CameraBase
        {
            get
            {
                CameraBase cameraBase = null;
                switch (g_ParCamera.NoCamera)
                {
                    case 1:
                        cameraBase = Camera1.C_I;
                        break;

                    case 2:
                        cameraBase = Camera2.C_I;
                        break;

                    case 3:
                        cameraBase = Camera3.C_I;
                        break;

                    case 4:
                        cameraBase = Camera4.C_I;
                        break;

                    case 5:
                        cameraBase = Camera5.C_I;
                        break;

                    case 6:
                        cameraBase = Camera6.C_I;
                        break;

                    case 7:
                        cameraBase = Camera7.C_I;
                        break;

                    case 8:
                        cameraBase = Camera8.C_I;
                        break;
                }
                return cameraBase;
            }
        }
        #endregion 定义

        #region 初始化
        //构造函数
        public SetCamera()
        {
            InitializeComponent();

            Login_Event();//事件注册

            tsbSoftTrrigger.Visibility = Visibility.Visible;

            NameClass = "SetCamera";
        }

        public void Init(BaseParCamera varCamera)
        {
            try
            {
                //图像处理
                g_ParCamera = varCamera;
                ShowPar_Invoke();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //事件注册
        public void Login_Event()
        {

        }
        #endregion 初始化

        #region 相机名称
        /// <summary>
        /// 是否使用相机名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbNameCamera_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbNameCamera.IsMouseOver)
                {
                    g_ParCamera.BlNameCamera = true;

                    ShowNameCamera();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void tsbNameCamera_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tsbNameCamera.IsMouseOver)
                {
                    g_ParCamera.BlNameCamera = false;

                    ShowNameCamera();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 相机名称

        #region 模拟软触发
        private void btnSoftwore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                g_CameraBase.TriggerSoftware();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 模拟软触发

        #region 保存
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int numError = 0;
            try
            {
                btnSave.Content = "保   存";

                //备份数据到本地
                FunBackup.F_I.BackupCamera();

                //相机驱动
                g_ParCamera.TypeCamera_e = (TypeCamera_enum)cboCameraType.SelectedIndex;

                //相机序列号
                g_ParCamera.Serial_Camera = txtSerialNumber.Text.Trim();

                //设置软触发
                if (!SetSoftTrrigger())
                {
                    numError++;
                }

                //保存相机参数
                if (!g_ParCamera.WriteIni())
                {
                    numError++;
                }

                if (numError > 0)
                {
                    btnSave.RefreshDefaultColor("保存失败", false);
                }
                else
                {
                    btnSave.RefreshDefaultColor("保存成功", true);
                }
            }
            catch (Exception ex)
            {
                btnSave.RefreshDefaultColor("保存失败", false);
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 设置软触发
        /// </summary>
        public bool SetSoftTrrigger(bool blTrigger)
        {
            bool blResult = true;
            bool blChange = false;
            try
            {
                if (g_ParCamera.BlUsingTrigger != blTrigger)
                {
                    blChange = true;
                }
                g_ParCamera.BlUsingTrigger = blTrigger;

                //触发源
                g_ParCamera.TriggerSource_e = (TriggerSourceCamera_enum)Enum.Parse(typeof(TriggerSourceCamera_enum), cboTriggerSource.Text);

                if (blChange
                    &&
                    (g_ParCamera.TypeCamera_e == TypeCamera_enum.BSLSDK
                    || g_ParCamera.TypeCamera_e == TypeCamera_enum.HIKSDK))
                {
                    if (g_CameraBase == null)
                    {
                        return true;
                    }
                    blResult = g_CameraBase.SetSoftTrrigger();

                }
                if (blResult == false)
                {
                    MessageBox.Show("触发模式色设置失败:相机" + g_ParCamera.NoCamera.ToString());
                }
                this.Dispatcher.Invoke(new Action(() =>
                {
                    tsbSoftTrrigger.IsChecked = blTrigger;
                }));

                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        bool SetSoftTrrigger()
        {
            bool blResult = true;
            bool blChange = false;
            try
            {
                //是否使用触发,以及判断触发模式是否更改
                bool blCheck = (bool)tsbSoftTrrigger.IsChecked;
                if (g_ParCamera.BlUsingTrigger != blCheck)
                {
                    blChange = true;
                }
                g_ParCamera.BlUsingTrigger = blCheck;

                //触发源
                g_ParCamera.TriggerSource_e = (TriggerSourceCamera_enum)Enum.Parse(typeof(TriggerSourceCamera_enum), cboTriggerSource.Text);

                if (blChange
                    &&
                    (g_ParCamera.TypeCamera_e == TypeCamera_enum.BSLSDK
                    || g_ParCamera.TypeCamera_e == TypeCamera_enum.HIKSDK))
                {
                    if (g_CameraBase == null)
                    {
                        return true;
                    }
                    blResult = g_CameraBase.SetSoftTrrigger();

                    if (blResult)
                    {
                        MessageBox.Show("打开或者关闭触发成功");
                    }
                    else
                    {
                        MessageBox.Show("打开或者关闭触发失败");
                    }
                }
                return blResult;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                MessageBox.Show("打开或者关闭触发失败");
                return false;
            }
        }
        #endregion 保存

        #region 显示
        public override void ShowPar()
        {
            try
            {
                //相机驱动
                cboCameraType.SelectedIndex = (int)g_ParCamera.TypeCamera_e;

                //相机序列号
                txtSerialNumber.Text = g_ParCamera.Serial_Camera.ToString();

                //软触发 
                tsbSoftTrrigger.IsChecked = g_ParCamera.BlUsingTrigger;

                cboTriggerSource.Text = g_ParCamera.TriggerSource_e.ToString();//触发源

                //相机名称
                ShowNameCamera();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机名称
        /// </summary>
        void ShowNameCamera()
        {
            try
            {
                tsbNameCamera.IsChecked = g_ParCamera.BlNameCamera;
                if (g_ParCamera.BlNameCamera)
                {
                    lblNameCamera.Content = "相机名称:";
                }
                else
                {
                    lblNameCamera.Content = "相机序列号:";
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

        #region Close
        //析构函数
        ~SetCamera()
        {
            Logout_Event();
        }
        //事件注销
        public void Logout_Event()
        {

        }
        #endregion Close

        

    }
}
