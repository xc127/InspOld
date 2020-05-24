using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Threading.Tasks;
using System.IO;
using DealFile;
using BasicClass;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Reflection;

namespace Camera
{
    public partial class CameraBase:BaseClass
    {
        #region 定义
        public CameraAbstract g_CameraAbstract = null;
        BaseParCamera g_ParCamera = null;

        public bool BlOpen = false;//相机是否打开
        public bool BlTrriger//是否处于触发模式
        {
            get
            {
                return g_ParCamera.BlUsingTrigger;
            }
        }

        public Mutex Mt_Display = new Mutex();

        // 定义事件
        public event TrrigerSourceAction_del Camera_event;
        #endregion 定义

        #region 初始化
        public CameraBase()
        {
            NameClass = "CameraBase";
        }
       
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="baseParCamera"></param>
        public void Init(BaseParCamera baseParCamera)
        {
            try
            {
                g_ParCamera = baseParCamera;
                //选择是何型号的相机，以及采用的打开方式
                //switch (baseParCamera.TypeCamera_e)
                //{
                //    case TypeCamera_enum.PGSDK:
                //        g_CameraAbstract = new Camera_PGSDK();                       
                //        break;

                //    case TypeCamera_enum.BSLSDK:
                //        g_CameraAbstract = new Camera_BSLSDK();
                //        break;

                //    case TypeCamera_enum.HIKSDK:
                //        g_CameraAbstract = new Camera_HIKSDK();
                //        break;

                //    default:
                //        g_CameraAbstract = new Camera_HIKSDK();
                //        break;
                //}
                //使用反射来进行处理
                string nameClass = "Camera_" + baseParCamera.TypeCamera_e.ToString();//类名称

                g_CameraAbstract = Activator.CreateInstance(null, "Camera." + nameClass).Unwrap() as CameraAbstract;//null表示当前程序集
                g_CameraAbstract.NameClass = NameClass;

                //事件注册
                LoginEvent();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void LoginEvent()
        {
            try
            {
                //使用反射来处理
                var eventInfo = g_CameraAbstract.GetType().GetEvent("Trigger_event");
                var handler = new Action(CameraBase_Trigger_event);
                eventInfo.AddEventHandler(g_CameraAbstract, handler);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            //switch (g_ParCamera.TypeCamera_e)
            //{
            //    case TypeCamera_enum.PGSDK:
                  
            //        break;

            //    case TypeCamera_enum.BSLSDK:
            //        ((Camera_BSLSDK)g_CameraAbstract).Trigger_event += new Action(CameraBase_Trigger_event);
            //        break;

            //    case TypeCamera_enum.HIKSDK:
            //        ((Camera_HIKSDK)g_CameraAbstract).Trigger_event+=new Action(CameraBase_Trigger_event);
            //        break;

            //    default:
                   
            //        break;
            //}
           
        }
        #endregion 初始化

        #region 打开关闭相机
        /// <summary>
        /// 通过序列号打开相机
        /// </summary>
        public bool OpenCamera()
        {
            try
            {
                ParCameraSetting parCameraSetting = new ParCameraSetting();
                parCameraSetting.BlSoftTrriger = g_ParCamera.BlUsingTrigger;//是否使用软触发
                parCameraSetting.TriggerSource_e = g_ParCamera.TriggerSource_e;

                if (RegeditCamera.R_I.BlOffLineCamera)
                {
                    return true;
                }
                BlOpen = g_CameraAbstract.OpenCamera(g_ParCamera);
                return BlOpen;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        /// <summary>
        /// 关闭相机
        /// </summary>
        /// <returns></returns>
        public bool CloseCamera()
        {
            return g_CameraAbstract.CloseCamera();
        }
        #endregion 打开关闭相机

        #region 抓取图像
        /// <summary>
        /// 抓取图像
        /// </summary>
        /// <returns></returns>
        public ImageAll GrabImageAll()
        {            
            return g_CameraAbstract.GrabImageAll();
        }

        public ImageAll GrabImageHal()
        {
            return g_CameraAbstract.GrabImageHal();
        }
        #endregion 抓取图像

        #region 回调函数响应
        void CameraBase_Trigger_event()
        {
            try
            {               
                Camera_event(TriggerSource_enum.Camera, 1);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }          
        }
        #endregion 回调函数响应

        #region 设置相机参数
        /// <summary>
        /// /曝光时间
        /// </summary>
        public bool SetExposure(double dblValue)
        {
            if (g_CameraAbstract == null)
            {
                return true;
            }
            return g_CameraAbstract.SetExposure(dblValue);
        }

        /// <summary>
        /// 设置增益
        /// </summary>
        public bool SetGain(double dblValue)
        {
            if (g_CameraAbstract == null)
            {
                return true;
            }
            return g_CameraAbstract.SetGain(dblValue);
        }

        /// <summary>
        /// 保存到相机
        /// </summary>
        /// <returns></returns>
        public bool SaveParChannel()
        {
            return g_CameraAbstract.SaveParChannel();
        }

        /// <summary>
        /// 设置软触发
        /// </summary>
        public bool SetSoftTrrigger()
        {
            return g_CameraAbstract.SetTrriggerMode();
        }

        /// <summary>
        /// 模拟软触发
        /// </summary>
        /// <returns></returns>
        public bool TriggerSoftware()
        {
            return g_CameraAbstract.TriggerSoftware();
        }

        /// <summary>
        /// 软触发
        /// </summary>
        /// <param name="blBitmap"></param>
        /// <returns></returns>
        public bool TriggerSoftware(bool blBitmap)
        {
            return g_CameraAbstract.TriggerSoftware(blBitmap);
        }

        /// <summary>
        /// 设置延迟
        /// </summary>
        /// <returns></returns>
        public bool SetTriggerDelay(double delay)
        {
            return g_CameraAbstract.SetTriggerDelay(delay);
        }
        #endregion 设置相机参数

        
    }
}
