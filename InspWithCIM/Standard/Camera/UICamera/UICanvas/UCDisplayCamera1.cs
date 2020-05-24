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
using HalconDotNet;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DealFile;
using System.ComponentModel;
using ControlLib;
using BasicClass;

namespace Camera
{
    public class UCDisplayCamera1 : BaseUCDisplayCamera
    {
        public UCDisplayCamera1()
        {
            try
            {
                NameClass = "UCDisplayCamera1";

                NoCamera = 1;

                //重新定义相机类实例
                base.g_CameraBase = Camera1.C_I;

                //相机显示类型
                base.g_BaseParCamera = ParCamera1.P_I;

                //显示进程互斥
                base.Mt_RefreshImage = Camera1.C_I.Mt_Display;

                g_HWDispImage.Visibility = Visibility.Visible;

                LoginCallBack();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }         
        }

        public UCDisplayCamera1(bool blShow)
        {
            try
            {
                NameClass = "UCDisplayCamera1";

                NoCamera = 1;

                //重新定义相机类实例
                base.g_CameraBase = Camera1.C_I;
                //相机显示类型
                base.g_BaseParCamera = ParCamera1.P_I;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
    }
}
