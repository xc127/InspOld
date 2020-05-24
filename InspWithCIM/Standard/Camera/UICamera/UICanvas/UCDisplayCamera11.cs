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
    public class UCDisplayCamera11 : BaseUCDisplayCamera
    {
        public UCDisplayCamera11()
        {
            try
            {
                NoCamera = 11;

                //重新定义相机类实例
                base.g_CameraBase = Camera11.C_I;
                //相机显示类型
                base.g_BaseParCamera = ParCamera11.P_I;
                NameClass = "UCDisplayCamera7";

                g_HWDispImage.Visibility = Visibility.Visible;

                LoginCallBack();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCDisplayCamera11", ex);
            }
        }

        public UCDisplayCamera11(bool blShow)
        {
            try
            {
                NoCamera = 11;

                //重新定义相机类实例
                base.g_CameraBase = Camera11.C_I;
                //相机显示类型
                base.g_BaseParCamera = ParCamera11.P_I;
                NameClass = "UCDisplayCamera11";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCDisplayCamere11", ex);
            }
        }
    }
}
