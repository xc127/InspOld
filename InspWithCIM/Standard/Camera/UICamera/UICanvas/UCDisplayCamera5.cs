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
    public class UCDisplayCamera5 : BaseUCDisplayCamera
    {
        public UCDisplayCamera5()
        {
            try
            {
                NoCamera = 5;

                //重新定义相机类实例
                base.g_CameraBase = Camera5.C_I;

                //相机显示类型
                base.g_BaseParCamera = ParCamera5.P_I;
                NameClass = "UCDisplayCamera5";

                g_HWDispImage.Visibility = Visibility.Visible;

                LoginCallBack();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCDisplayCamera5", ex);
            }
        }

        public UCDisplayCamera5(bool blShow)
        {
            try
            {
                NoCamera = 5;

                //重新定义相机类实例
                base.g_CameraBase = Camera5.C_I;

                //相机显示类型
                base.g_BaseParCamera = ParCamera5.P_I;
                NameClass = "UCDisplayCamera5";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCDisplayCamera5", ex);
            }
        }
    }
}
