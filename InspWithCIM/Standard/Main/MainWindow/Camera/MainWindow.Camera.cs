
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
using ControlLib;
using Common;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Camera;
using HalconDotNet;
using DealFile;
using BasicClass;
using DealConfigFile;
using SetPar;
using DealPLC;
using DealDisplay;

namespace Main
{
    partial class MainWindow
    {
        #region 定义
        
        #endregion 定义

        #region 初始化
        //事件注册
        void LoginEvent_Camera()
        {
            //相机
            Camera1.C_I.Camera_event += new TrrigerSourceAction_del(DealComprehensive_Camera1_event);
            Camera2.C_I.Camera_event += new TrrigerSourceAction_del(DealComprehensive_Camera2_event);
            Camera3.C_I.Camera_event += new TrrigerSourceAction_del(DealComprehensive_Camera3_event);
            Camera4.C_I.Camera_event += new TrrigerSourceAction_del(DealComprehensive_Camera4_event);
            Camera5.C_I.Camera_event += new TrrigerSourceAction_del(DealComprehensive_Camera5_event);
            Camera6.C_I.Camera_event += new TrrigerSourceAction_del(DealComprehensive_Camera6_event);
            Camera7.C_I.Camera_event += new TrrigerSourceAction_del(DealComprehensive_Camera7_event);
            Camera8.C_I.Camera_event += new TrrigerSourceAction_del(DealComprehensive_Camera8_event);
        }       

        /// <summary>
        /// 相机抓取图像
        /// </summary>
        void Init_Camera()
        {
            try
            {
                if (RegeditCamera.R_I.BlOffLineCamera)
                {
                    return;
                }
                g_BaseUCDisplaySum.InitGrabImage();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        #region 相机出错
        /// <summary>
        /// 显示相机出错信息
        /// </summary>
        /// <param name="str"></param>
        void g_UCDisplayCameraSum_CameraError_event(string str)
        {
            try
            {
                LogicPLC.L_I.PCAlarm();//PLC报警
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 相机出错

        #region 关闭
        //停止实时显示
        void StopReal()
        {
            g_BaseUCDisplaySum.StopRealImage();
        }

        //关闭相机
        void Close_Camera()
        {
            g_BaseUCDisplaySum.CloseCamera();
        }

        //注销事件
        void EventLogout_Camera()
        {

        }
        #endregion 关闭
    }
}
