//#define SDK

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DealFile;
using HalconDotNet;
using BasicClass;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows;
using Common;

#if(SDK)
using FlyCapture2Managed;
#endif

namespace Camera
{
    public class Camera_PGSDK : CameraAbstract
    {
        #region 类名称
        string nameClass = "";
        public override string NameClass
        {
            set
            {
                nameClass = value;
            }
            get
            {
                return nameClass;
            }
        }
        #endregion 类名称

        #region 定义
        //bool
        bool BlOpen = false;//指示相机是否打开成功
        bool BlColor = false;//指示是否彩色相机
        bool BlTriggerBitmap = false;

        //触发模式
        public override TriggerSourceCamera_enum TriggerSource_e
        {
            get
            {
                return g_BaseParCamera.TriggerSource_e;
            }
        }

        //int      

        //相机参数设置
        BaseParCamera g_BaseParCamera = null;

#if(SDK)
        ManagedCameraBase g_ManagedCameraBase = null;//相机总线
        ManagedImage g_RawImage = new ManagedImage();//原始图像
        ManagedImage g_ConvertImage = new ManagedImage();//格式转换

        BayerTileFormat BayerTileFormat_e = BayerTileFormat.None;//彩色图像格式
#endif

        Mutex g_MtGrabImage = new Mutex();

        /// 定义事件
        public event Action Trigger_event;//触发模式事件
        #endregion 定义

        #region 打开相机
        /// <summary>
        /// 打开相机，并传入相机参数
        /// </summary>
        public override bool OpenCamera(BaseParCamera baseParCamera)
        {
            try
            {
                g_BaseParCamera = baseParCamera;
                //连接相机
                if (!ConnectCamera(baseParCamera.Serial_Camera))
                {
                    BlOpen = false;
                    return false;
                }
                //彩色相机格式判断
                if (!ColorCamera())
                {
                    BlOpen = false;
                    return false;
                }
                //抓取图像
                if (!StartCapture())
                {
                    BlOpen = false;
                    return false;
                }
                BlOpen = true;
            }
            catch (Exception ex)
            {
                BlOpen = false;
                Log.L_I.WriteError(NameClass, ex);
            }
            return BlOpen;
        }


        /// <summary>
        /// 连接相机
        /// </summary>
        /// <param name="SerialNumber"></param>
        /// <returns></returns>
        bool ConnectCamera(string SerialNumber)
        {
            try
            {
#if(SDK)
                ManagedBusManager busMgr = new ManagedBusManager();
                //相机索引号
                uint intSerialNumber = uint.Parse(SerialNumber);
                ManagedPGRGuid guid = busMgr.GetCameraFromSerialNumber(intSerialNumber);
                InterfaceType Type = busMgr.GetInterfaceTypeFromGuid(guid);

                if (Type == InterfaceType.GigE)
                {
                    g_ManagedCameraBase = new ManagedGigECamera();
                    g_ManagedCameraBase.Connect(guid);

                    SetGigEPacketResend();//设置丢帧重传
                }
                else
                {
                    g_ManagedCameraBase = new ManagedCamera();
                    g_ManagedCameraBase.Connect(guid);
                }

                if (g_BaseParCamera.BlUsingTrigger)
                {
                    SetSoftTrriger(true);//设置触发
                }
#endif
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 彩色相机格式
        /// </summary>
        bool ColorCamera()
        {
            try
            {
#if(SDK)
                //判断是否彩色相机
                CameraInfo caminfo = null;
                caminfo = g_ManagedCameraBase.GetCameraInfo();
                BlColor = caminfo.isColorCamera;
                //如果是彩色图像，则判断彩色格式
                if (BlColor)
                {
                    BayerTileFormat_e = caminfo.bayerTileFormat;
                }
#endif
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 设置相机在软触发模式
        /// </summary>
        void SetSoftTrriger(bool blOpen)
        {
            try
            {
#if(SDK)
                //开启触发模式
                TriggerMode triggermode;
                triggermode = g_ManagedCameraBase.GetTriggerMode();
                triggermode.onOff = blOpen;
                triggermode.mode = 0;
                triggermode.parameter = 0;
                triggermode.source = 7;
                g_ManagedCameraBase.SetTriggerMode(triggermode);
#endif
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 抓取图像
        /// </summary>
        bool StartCapture()
        {
            try
            {
#if(SDK)
                g_ManagedCameraBase.StartCapture();
#endif
                return true;

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 设置丢帧重传
        /// </summary>
        void SetGigEPacketResend()
        {
            try
            {
#if(SDK)
                ManagedGigECamera managedGigECamera = (ManagedGigECamera)g_ManagedCameraBase;
                GigEConfig gigEConfig = managedGigECamera.GetGigEConfig();
                gigEConfig.enablePacketResend = true;
                managedGigECamera.SetGigEConfig(gigEConfig);//设置丢帧重传
#endif
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 控制相机

        #region 抓取相机图像以及格式转换
        /// <summary>
        /// 抓取相机的图像,输出所有的格式
        /// </summary>
        public override ImageAll GrabImageAll()
        {
            g_MtGrabImage.WaitOne();
            ImageAll imageAll = new ImageAll();
            try
            {
                if (!BlOpen)//如果相机打开失败
                {
                    return null;
                }
#if(SDK)
                try
                {
                    SetTrriggerMode();//设置软触发              
                    g_ManagedCameraBase.RetrieveBuffer(g_RawImage);
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(NameClass, ex);
                    try
                    {
                        if (g_BaseParCamera.BlUsingTrigger)//使用软触发
                        {
                            g_ManagedCameraBase.WriteRegister(0x62C, 0x80000000);//发送软触发指令
                        }
                        g_ManagedCameraBase.RetrieveBuffer(g_RawImage);
                    }
                    catch
                    {
                        if (g_BaseParCamera.BlUsingTrigger)//使用软触发
                        {
                            g_ManagedCameraBase.WriteRegister(0x62C, 0x80000000);//发送软触发指令
                        }
                        g_ManagedCameraBase.RetrieveBuffer(g_RawImage);
                    }
                }

                //将图像转换为Halcon格式                    
                imageAll.Ho_Image = ConvertHobject();
                imageAll.BitmapSource = ConvertImageBitmap();
                PathDirectory p = new PathDirectory();
                imageAll.Time = p.GetShortTimeName();
#endif
                return imageAll;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
            finally
            {
                g_MtGrabImage.ReleaseMutex();
            }
        }
        /// <summary>
        /// 抓取相机的图像,输出Halcon格式
        /// </summary>
        public override ImageAll GrabImageHal()
        {
            g_MtGrabImage.WaitOne();
            ImageAll imageAll = new ImageAll();
            try
            {
                if (!BlOpen)//如果相机打开失败
                {
                    return null;
                }
#if(SDK)
                try
                {
                    SetTrriggerMode();//设置软触发
                    g_ManagedCameraBase.RetrieveBuffer(g_RawImage);
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(NameClass, ex);
                    try
                    {
                        if (g_BaseParCamera.BlUsingTrigger)//使用软触发
                        {
                            g_ManagedCameraBase.WriteRegister(0x62C, 0x80000000);//发送软触发指令
                        }
                        g_ManagedCameraBase.RetrieveBuffer(g_RawImage);
                    }
                    catch
                    {
                        if (g_BaseParCamera.BlUsingTrigger)//使用软触发
                        {
                            g_ManagedCameraBase.WriteRegister(0x62C, 0x80000000);//发送软触发指令
                        }
                        g_ManagedCameraBase.RetrieveBuffer(g_RawImage);
                    }
                }

                //将图像转换为Halcon格式
                imageAll.Ho_Image = ConvertHobject();
                imageAll.BitmapSource = null;

                PathDirectory p = new PathDirectory();
                imageAll.Time = p.GetShortTimeName();
#endif
                return imageAll;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
            finally
            {
                g_MtGrabImage.ReleaseMutex();
            }
        }

        /// <summary>
        /// 图像格式转换，转换为BitmapSource
        /// </summary>
        public BitmapSource ConvertImageBitmap()
        {
            try
            {
                if (BlOpen)
                {
#if(SDK)
                    g_RawImage.ConvertToBitmapSource(g_ConvertImage);
                    g_ConvertImage.bitmapsource.Freeze();
                    return g_ConvertImage.bitmapsource;
#endif
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
            finally
            {
                GC.Collect();
            }
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hobject);

        /// <summary>
        /// 将图像转换为Halcon 格式
        /// </summary>
        HObject ConvertHobject()
        {
            try
            {
                HImage hImage = new HImage();
#if(SDK)

                unsafe
                {
                    hImage.GenImage1Rect((System.IntPtr)g_RawImage.data,
                        (int)g_RawImage.cols, (int)g_RawImage.rows, (int)g_RawImage.stride, (int)g_RawImage.bitsPerPixel, (int)g_RawImage.bitsPerPixel,
                        "false", (System.IntPtr)0);
                }
#endif
                return ColorImage(hImage);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }
        /// <summary>
        /// 彩色/灰度图像,根据图像制式，转换图像
        /// </summary>
        HObject ColorImage(HObject ho_Image)
        {
            try
            {
                if (!BlColor)
                {
                    return ho_Image;
                }
                else
                {
                    HObject ho_ImageOut = null;
#if(SDK)
                    switch (BayerTileFormat_e)
                    {
                        case BayerTileFormat.RGGB:
                            HOperatorSet.CfaToRgb(ho_Image, out ho_ImageOut, "bayer_rg", "bilinear");
                            break;

                        case BayerTileFormat.GRBG:
                            HOperatorSet.CfaToRgb(ho_Image, out ho_ImageOut, "bayer_gr", "bilinear");
                            break;

                        case BayerTileFormat.GBRG:
                            HOperatorSet.CfaToRgb(ho_Image, out ho_ImageOut, "bayer_gb", "bilinear");
                            break;

                        case BayerTileFormat.BGGR:
                            HOperatorSet.CfaToRgb(ho_Image, out ho_ImageOut, "bayer_bg", "bilinear");
                            break;
                    }
#endif
                    return ho_ImageOut;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }
        #endregion 抓取相机图像以及格式转换

        #region 设置相机
        /// <summary>
        /// 快门时间
        /// </summary>
        public override bool SetExposure(double dblValue)
        {
            try
            {
#if(SDK)
                CameraProperty cameraP = new CameraProperty();
                cameraP.type = PropertyType.Shutter;
                cameraP.absControl = true;
                cameraP.autoManualMode = false;
                cameraP.absValue = (float)dblValue;
                SetProperty(cameraP);
#endif
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 增益
        /// </summary>
        public override bool SetGain(double dblValue)
        {
            try
            {
#if(SDK)
                CameraProperty cameraP = new CameraProperty();
                cameraP.type = PropertyType.Gain;
                cameraP.absControl = true;
                cameraP.autoManualMode = false;
                cameraP.absValue = (float)dblValue;
                SetProperty(cameraP);
#endif
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

#if(SDK)
        /// <summary>
        /// 设置相机属性
        /// </summary>
        void SetProperty(CameraProperty cameraP)
        {
            try
            {

                g_ManagedCameraBase.SetProperty(cameraP);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
#endif
        /// <summary>
        /// 保存设置到通道
        /// </summary>
        public override bool SaveParChannel()
        {
            try
            {
#if(SDK)
                uint channel = g_ManagedCameraBase.GetMemoryChannel();
                g_ManagedCameraBase.SaveToMemoryChannel(channel);
#endif
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        ///设置软触发
        /// </summary>
        public override bool SetTrriggerMode()
        {
            try
            {
#if(SDK)
                if (g_BaseParCamera.BlUsingTrigger)//使用软触发
                {
                    g_ManagedCameraBase.WriteRegister(0x62C, 0x80000000);//发送软触发指令
                }
#endif
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        public override bool SetTriggerDelay(double delay)
        {
            return false;
        }
        public override bool TriggerSoftware()
        {
            return false;
        }

        public override bool TriggerSoftware(bool blTrrigerBitmap)
        {
            try
            {
                BlTriggerBitmap = true;
                TriggerSoftware();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion 设置相机

        #region 关闭相机
        /// <summary>
        /// 关闭相机,停止抓取相机并断开相机连接
        /// </summary>
        public override bool CloseCamera()
        {
            try
            {
#if(SDK)
                if (g_BaseParCamera.BlUsingTrigger)
                {
                    SetSoftTrriger(false);//关闭软触发
                }
                g_ManagedCameraBase.StopCapture();
                g_ManagedCameraBase.Disconnect();
#endif
                return true;
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
