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
    public class UCDisplayCamera4 : BaseUCDisplayCamera
    {
        public UCDisplayCamera4()
        {
            try
            {
                NoCamera = 4;

                //重新定义相机类实例
                base.g_CameraBase = Camera4.C_I;

                //相机显示类型
                base.g_BaseParCamera = ParCamera4.P_I;

                NameClass = "UCDisplayCamera4";

                g_HWDispImage.Visibility = Visibility.Visible;

                LoginCallBack();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCDisplayCamera4", ex);
            }           
        }

        public UCDisplayCamera4(bool blShow)
        {
            try
            {
                NoCamera = 4;

                //重新定义相机类实例
                base.g_CameraBase = Camera4.C_I;

                //相机显示类型
                base.g_BaseParCamera = ParCamera4.P_I;

                NameClass = "UCDisplayCamera4";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCDisplayCamera4", ex);
            }      
        }
    }
}
