#define SDK

using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;
using HalconDotNet;
using System.Drawing;
using System.Windows.Media.Imaging;

using System.Threading;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Common;

#if(SDK)
using MvCamCtrl.NET;
#endif


namespace Camera
{
    public class Camera_HIKSDK : CameraAbstract
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
        bool g_BlCallBack = false;//是否注册了回调函数
        bool BlTriggerBitmap = false;//触发需要获取ImageAll

        //触发模式
        public override TriggerSourceCamera_enum TriggerSource_e
        {
            get
            {
                return g_BaseParCamera.TriggerSource_e;
            }
        }

        //string         

        //int      
        UInt32 g_BufSizeForDriver = 3072 * 2048 * 3 + 2048;
        byte[] g_PBufForDriver = new byte[3072 * 2048 * 3 + 2048];            // 用于从驱动获取图像的缓存
        byte[] g_PBufForSaveImage = new byte[3072 * 2048 * 3 * 3 + 2048];         // 用于保存图像的缓存

        PathDirectory g_PathDirectory = new PathDirectory();

        //相机
        BaseParCamera g_BaseParCamera = null;       
        Queue<ImageAll> ImageAll_Q = new Queue<ImageAll>();        

#if(SDK)
        MyCamera g_MyCamera = new MyCamera();
        MyCamera.MV_CC_DEVICE_INFO_LIST g_PDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        List<MyCamera.MV_CC_DEVICE_INFO> DeviceCam_L = new List<MyCamera.MV_CC_DEVICE_INFO>();
        MyCamera.MV_CC_DEVICE_INFO thisDevice;

        MyCamera.MV_FRAME_OUT_INFO_EX PFrameInfo_Bit;
        IntPtr PData_Bit;

        //定义软触发回调函数
        MyCamera.cbOutputExdelegate ImageCallback = null;       

        delegate void ImageCallBack(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO pFrameInfo, IntPtr pUser);
#endif

        Mutex g_MtGrabImage = new Mutex();

        /// 定义事件
        public event Action Trigger_event;//触发模式事件
        #endregion 定义

        #region 初始化
        public Camera_HIKSDK()
        {
            NameClass = "Camera_HIKSDK";
        }
        #endregion 初始化

        #region 打开相机
#if(SDK)
        //枚举相机
        public static int EnumDevices(uint nLayerType, ref MyCamera.MV_CC_DEVICE_INFO_LIST stDeviceList)
        {

            return MyCamera.MV_CC_EnumDevices_NET(nLayerType, ref stDeviceList);
        }

        //使用string枚举相机
        public static int EnumDevices(uint nLayerType, ref MyCamera.MV_CC_DEVICE_INFO_LIST stDeviceList, string pManufacturerName)
        {
            return MyCamera.MV_CC_EnumDevicesEx_NET(nLayerType, ref stDeviceList, pManufacturerName);
        }
#endif

        /// <summary>
        /// 打开相机，并传入相机参数
        /// </summary>
        public override bool OpenCamera(BaseParCamera baseParCamera)
        {
            try
            {
                g_BaseParCamera = baseParCamera;

                //连接相机
                if (!ConnectCamera( baseParCamera.Serial_Camera))
                {
                    BlOpen = false;
                    return false;
                }
                BlOpen = true;
                return true;
            }
            catch (Exception ex)
            {
                BlOpen = false;
                Log.L_I.WriteError(NameClass + NameClass, ex);
                return false;
            }
        }

        //使用序列号打开相机
        public bool ConnectCamera(string serialNumber)
        {
#if(SDK)
            int nRet = 0;
            //设置心跳
            uint nValue = 3000; //毫秒
            BlOpen = false;
            try
            {

                List<MyCamera.MV_GIGE_DEVICE_INFO> gIGEDevice_L = new List<MyCamera.MV_GIGE_DEVICE_INFO>();
                List<string> CamSerial_L = new List<string>();

                //枚举相机
                if (0 != Camera_HIKSDK.EnumDevices(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref g_PDeviceList))
                {
                    return false;
                }
                if (g_PDeviceList.nDeviceNum == 0)
                {
                    return false;
                }

                for (int i = 0; i < g_PDeviceList.nDeviceNum; i++)
                {
                    //获取相机信息,以MV_CC结构存入list
                    DeviceCam_L.Add((MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(g_PDeviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO)));

                    //将相机信息MV_CC 结构转换为MV_GIGE结构
                    IntPtr struptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MyCamera.MV_GIGE_DEVICE_INFO)));
                    Marshal.Copy(DeviceCam_L[i].SpecialInfo.stGigEInfo, 0, struptr, Marshal.SizeOf(typeof(MyCamera.MV_GIGE_DEVICE_INFO)));
                    gIGEDevice_L.Add((MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(struptr, typeof(MyCamera.MV_GIGE_DEVICE_INFO)));

                    if (!g_BaseParCamera.BlNameCamera)//使用序列号
                    {
                        //将相机序列号存入List
                        CamSerial_L.Add(gIGEDevice_L[i].chSerialNumber);
                    }
                    else
                    {
                        //将相机名称存入List
                        CamSerial_L.Add(gIGEDevice_L[i].chUserDefinedName);
                    }
                }

                int thisCamOrder = -1;
                //找到当前序列号相机
                for (int i = 0; i < CamSerial_L.Count; i++)
                {
                    if (serialNumber == CamSerial_L[i])
                    {
                        thisCamOrder = i;
                        break;
                    }
                }

                if (thisCamOrder == -1)
                {
                    return false;
                }

                //连接当前序列号相机            
                thisDevice = DeviceCam_L[thisCamOrder];
                nRet = g_MyCamera.MV_CC_CreateDevice_NET(ref thisDevice);
                if (nRet != MyCamera.MV_OK)
                {
                    return false;
                }

                nRet = g_MyCamera.MV_CC_OpenDevice_NET();
                if (nRet != MyCamera.MV_OK)
                {
                    return false;
                }

                //设置是否为触发模式
                if (g_BaseParCamera.BlUsingTrigger)
                {
                    SetTrigger();
                }

                //设置心跳
                nRet = g_MyCamera.MV_CC_SetHeartBeatTimeout_NET(nValue);

                //注册回调，不在软触发模式下
                if (!g_BlCallBack)
                {
                    nRet = g_MyCamera.MV_CC_StartGrabbing_NET();
                }

                if (nRet != MyCamera.MV_OK)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
#endif
            return true;
        }
        #endregion 打开相机

        #region 抓取图像
        /// <summary>
        /// 抓取一帧图像
        /// </summary>
#if(SDK)
        public int GetOneFrameTimeout(IntPtr pData, ref UInt32 pnDataLen, UInt32 nDataSize, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, Int32 nMsec)
        {
            int nRet = 0;

            pnDataLen = 0;
            nRet = g_MyCamera.MV_CC_GetOneFrameTimeout_NET(pData, nDataSize, ref pFrameInfo, nMsec);
            pnDataLen = pFrameInfo.nFrameLen;
            if (MyCamera.MV_OK != nRet)
            {
                return nRet;
            }
            return nRet;
        }


        private void GrabHal_CallBack(IntPtr pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, IntPtr pUser)
        {
            try
            {
                //使用触发模式
                if (g_BaseParCamera.BlUsingTrigger)
                {
                    MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfoNew = pFrameInfo;
                    new Task(new Action(() =>
                    {
                        ImageAll imTemp = new ImageAll();
                        imTemp.Ho_Image = ConvertHobject(pFrameInfoNew, pData);
                        imTemp.TimeTrigger = PathDirectory.P_I.GetTimeName();//获取时间

                        if (BlTriggerBitmap)//触发需要获取ImageAll
                        {
                            BlTriggerBitmap = false;
                            PFrameInfo_Bit = pFrameInfoNew;
                            PData_Bit = pData;
                        }
                        ImageAll_Q.Enqueue(imTemp);//图像加入队列
                        if (Trigger_event != null)
                        {
                            Trigger_event();//触发事件
                        }
                    })).Start();
                }
            }
            catch (Exception ex)
            {
                BlTriggerBitmap = false;
                Log.L_I.WriteError(NameClass, ex);
            }
        }
#endif
        //抓取图像
        public override ImageAll GrabImageAll()
        {
            g_MtGrabImage.WaitOne();
            ImageAll imageAll = new ImageAll();
            UInt32 nDataLen = 0;
            int waitTime = 1000;
            try
            {
                if (g_BaseParCamera.BlUsingTrigger
                    && TriggerSource_e != TriggerSourceCamera_enum.Software)//使用软触发
                {                   
                    if (ImageAll_Q.Count > 0)
                    {
                        ImageAll im = ImageAll_Q.Peek();
                        im.BitmapSource = ConvertImageBitmap(PFrameInfo_Bit, PData_Bit);
                        ImageAll_Q.Dequeue();//出站
                        if (ImageAll_Q.Count > 20)//认为溢出
                        {
                            ImageAll_Q.Clear();
                        }

                        im.Time = g_PathDirectory.GetTimeName();
                        im.TimeShort = g_PathDirectory.GetShortTimeName();//图像抓取短时间
                        return im;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
#if(SDK)
                    IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(g_PBufForDriver, 0);

                    MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

                    //获取一帧，超时时间为1秒,此处单位为ms
                    int nRet = GetOneFrameTimeout(pData, ref nDataLen, g_BufSizeForDriver, ref pFrameInfo, waitTime);

                    imageAll.Ho_Image = ConvertHobject(pFrameInfo, pData);
                    imageAll.BitmapSource = ConvertImageBitmap(pFrameInfo, pData);

                    imageAll.Time = g_PathDirectory.GetTimeName();
                    imageAll.TimeShort = g_PathDirectory.GetShortTimeName();//图像抓取短时间
                    return imageAll;
                }
#endif
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                g_MtGrabImage.ReleaseMutex();
            }
        }

        public override ImageAll GrabImageHal()
        {
            g_MtGrabImage.WaitOne();
            ImageAll imageAll = new ImageAll();
            int waitTime = 1000;
            try
            {
                if (BlOpen)//如果相机打开成功
                {
                    if (g_BaseParCamera.BlUsingTrigger
                        && TriggerSource_e != TriggerSourceCamera_enum.Software)//使用软触发
                    {
                        if (ImageAll_Q.Count > 0)
                        {
                            ImageAll im = ImageAll_Q.Peek();
                            ImageAll_Q.Dequeue();//出站
                            if (ImageAll_Q.Count > 20)//认为溢出
                            {
                                ImageAll_Q.Clear();
                            }

                            im.Time = g_PathDirectory.GetTimeName();
                            im.TimeShort = g_PathDirectory.GetShortTimeName();//图像抓取短时间
                            return im;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
#if(SDK)
                        UInt32 nDataLen = 0;
                        IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(g_PBufForDriver, 0);

                        MyCamera.MV_FRAME_OUT_INFO_EX image_grab = new MyCamera.MV_FRAME_OUT_INFO_EX();

                        //获取一帧，超时时间为1秒
                        int nRet = GetOneFrameTimeout(pData, ref nDataLen, g_BufSizeForDriver, ref image_grab, waitTime);

                        imageAll.Ho_Image = ConvertHobject(image_grab, pData);
                        imageAll.BitmapSource = null;

                        imageAll.Time = g_PathDirectory.GetTimeName();
                        imageAll.TimeShort = g_PathDirectory.GetShortTimeName();//图像抓取短时间
#endif
                    }
                }
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

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hobject);
        /// <summary>
        /// 图像格式转换，转换为BitmapSource
        /// </summary>
#if(SDK)
        public BitmapSource ConvertImageBitmap(MyCamera.MV_FRAME_OUT_INFO_EX image_grab, IntPtr pData)
        {
            try
            {
                if (BlOpen)
                {

                    //获取bmp图像
                    Bitmap image_bmp = new Bitmap(image_grab.nWidth, image_grab.nHeight, image_grab.nWidth * 1, PixelFormat.Format8bppIndexed, pData);
                    ColorPalette cp = image_bmp.Palette;
                    // init palette
                    for (int i = 0; i < 256; i++)
                    {
                        cp.Entries[i] = Color.FromArgb(i, i, i);
                    }
                    // set palette back
                    image_bmp.Palette = cp;
                    IntPtr p_bmpImage = image_bmp.GetHbitmap();
                    BitmapSource BbitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(p_bmpImage, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    DeleteObject(p_bmpImage);
                    image_bmp.Dispose();
                    return BbitmapSource;
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

        /// <summary>
        /// 将图像转换为Halcon 格式
        /// </summary>
        /// <returns></returns>
        HObject ConvertHobject(MyCamera.MV_FRAME_OUT_INFO_EX image_grab, IntPtr pData)
        {
            try
            {
                HObject hImage = new HObject();
                unsafe
                {
                    //将抓取到的图像转换成halcon图像
                    HOperatorSet.GenImage1(out hImage, (HTuple)"byte", (HTuple)(image_grab.nWidth), (HTuple)(image_grab.nHeight), pData);
                }

                return hImage;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }
#endif
        #endregion 抓取图像

        #region 设置相机
        /// <summary>
        /// 通用设置
        /// </summary>
        /// <param name="fValue">写入值</param>
        /// <param name="strKey" >写入项目</param>
        public bool SetFloatValue(string strKey, float fValue)
        {
            try
            {
#if(SDK)
                int nRet = g_MyCamera.MV_CC_SetFloatValue_NET(strKey, fValue);
                if (MyCamera.MV_OK != nRet)
                {
                    return false;
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
        /// 曝光时间 
        /// </summary>
        /// <param name="dblValue"></param>
        /// <returns></returns>
        public override bool SetExposure(double dblValue)
        {
            try
            {
#if(SDK)
                if (SetFloatValue("ExposureTime", (float)dblValue * 1000))
                {
                    return true;
                }
#endif
                return false;
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
                if (SetFloatValue("Gain", (float)dblValue))
                {
                    return true;
                }
#endif
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 帧率
        /// </summary>
        public bool SetFrameRate(double dblValue)
        {
            try
            {
#if(SDK)
                if (SetFloatValue("ResultingFrameRate", (float)dblValue))
                {
                    return true;
                }
#endif
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }


        /// <summary>
        /// 保存设置到通道
        /// </summary>
        public override bool SaveParChannel()
        {
            try
            {
#if(SDK)
                int nRet = g_MyCamera.MV_CC_SetEnumValue_NET("UserSetSelector", 1);
                if (MyCamera.MV_OK != nRet)
                {
                    return false;
                }
                nRet = g_MyCamera.MV_CC_SetCommandValue_NET("UserSetSave");
                if (MyCamera.MV_OK != nRet)
                {
                    return false;
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
        ///设置软触发
        /// </summary>
        public override bool SetTrriggerMode()
        {
            try
            {
                int nRet = 0;
#if(SDK)
                if (g_BaseParCamera.BlUsingTrigger)//使用软触发
                {
                    SetTrigger();
                }
                else
                {
                    SetContinue();
                }

                if (MyCamera.MV_OK != nRet)
                {
                    return false;
                }
                return true;
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
        /// 触发模式
        /// </summary>
        bool SetTrigger()
        {
            try
            {
                int nRet = 0;
#if(SDK)
                if (g_BaseParCamera.BlUsingTrigger)//使用软触发
                {
                    //触发模式，触发源
                    nRet = g_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 1);
                    nRet = g_MyCamera.MV_CC_SetEnumValue_NET("TriggerSource", (uint)TriggerSource_e);

                    if (!g_BlCallBack
                       && TriggerSource_e != TriggerSourceCamera_enum.Software)//非软触发模式
                    {
                        nRet = g_MyCamera.MV_CC_StopGrabbing_NET();
                        g_BlCallBack = true;
                        ImageCallback = new MyCamera.cbOutputExdelegate(GrabHal_CallBack);
                        nRet = g_MyCamera.MV_CC_RegisterImageCallBackEx_NET(ImageCallback, IntPtr.Zero);

                        nRet = g_MyCamera.MV_CC_StartGrabbing_NET();
                    }
                }

                if (MyCamera.MV_OK != nRet)
                {
                    return false;
                }
                return true;
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
        /// 连续触发模式
        /// </summary>
        /// <returns></returns>
        bool SetContinue()
        {
            try
            {
                int nRet = 0;
#if(SDK)
                if (g_BlCallBack == true)
                {
                    nRet = g_MyCamera.MV_CC_StopGrabbing_NET();
                    g_BlCallBack = false;
                    ImageCallback = null;
                    g_MyCamera.MV_CC_RegisterImageCallBackEx_NET(ImageCallback, IntPtr.Zero);
                    nRet = g_MyCamera.MV_CC_StartGrabbing_NET();
                }
                //连续采集模式
                nRet = g_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", 0);
                if (MyCamera.MV_OK != nRet)
                {
                    return false;
                }
                return true;
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
        /// 触发延迟
        /// </summary>
        public override bool SetTriggerDelay(double delay)
        {
            try
            {
#if(SDK)
                int nRet = g_MyCamera.MV_CC_SetTriggerDelay_NET((float)delay);
                if (MyCamera.MV_OK != nRet)
                {
                    return false;
                }
#endif
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 模拟软触发
        /// </summary>
        public override bool TriggerSoftware()
        {
            try
            {
#if(SDK)
                int nRet = g_MyCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
                if (MyCamera.MV_OK != nRet)
                {
                    return false;
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
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 设置相机

        #region 获取相机参数
        /// <summary>
        /// 通用函数
        /// </summary>
        /// /// <param name="strKey" >读取项目</param>
        /// <param name="dblValue">读取值</param>        
        /// <returns></returns>
        public bool GetFloatValue(string strKey, ref float pfValue)
        {
#if(SDK)
            MyCamera.MVCC_FLOATVALUE stParam = new MyCamera.MVCC_FLOATVALUE();
            int nRet = g_MyCamera.MV_CC_GetFloatValue_NET(strKey, ref stParam);
            if (MyCamera.MV_OK != nRet)
            {
                return false;
            }

            pfValue = stParam.fCurValue;
#endif
            return true;
        }

        /// <summary>
        /// 曝光时间 
        /// </summary>
        /// <param name="dblValue"></param>
        /// <returns></returns>
        public bool GetExposure(ref double dblValue)
        {
            try
            {
#if(SDK)
                float temp = (float)dblValue;
                if (GetFloatValue("ExposureTime", ref temp))
                {
                    dblValue = (double)temp;
                    return true;
                }
#endif
                return false;
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
        /// <param name="dblValue"></param>
        /// <returns></returns>
        public bool GetGain(ref double dblValue)
        {
            try
            {
#if(SDK)
                float temp = (float)dblValue;
                if (GetFloatValue("Gain", ref temp))
                {
                    dblValue = (double)temp;
                    return true;
                }
#endif
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 帧率
        /// </summary>
        /// <param name="cameraP"></param>
        public bool GetFrameRate(double dblValue)
        {
            try
            {
#if(SDK)
                float temp = (float)dblValue;
                if (GetFloatValue("ResultingFrameRate", ref temp))
                {
                    dblValue = (double)temp;
                    return true;
                }
#endif
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 获取相机参数

        #region 关闭相机
        public override bool CloseCamera()
        {
            try
            {
                int nRet;
#if(SDK)
                nRet = g_MyCamera.MV_CC_StopGrabbing_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    //return false;
                }

                nRet = g_MyCamera.MV_CC_CloseDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    //return false;
                }

                nRet = g_MyCamera.MV_CC_DestroyDevice_NET();
                if (MyCamera.MV_OK != nRet)
                {
                    //return false;
                }
#endif
                return true;
          
            }
            catch
            {
                return false;
            }

        }
        #endregion 关闭相机
    }
}
