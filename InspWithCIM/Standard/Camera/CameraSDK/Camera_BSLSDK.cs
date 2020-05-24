#define SDK

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using DealFile;
using HalconDotNet;
using BasicClass;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using System.Threading.Tasks;
using Common;

#if(SDK)
using Basler.Pylon;
#endif

namespace Camera
{
    public class Camera_BSLSDK : CameraAbstract
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

        //string 
        string g_SerialNumber = "";//序列号
        string g_PixelFormat = "";

        //触发模式
        public override TriggerSourceCamera_enum TriggerSource_e
        {
            get
            {
                return g_BaseParCamera.TriggerSource_e;
            }
        }

        //int       
        public int CountQ//队列的个数
        {
            get
            {
                return ImageAll_Q.Count;
            }
        }
        //相机    
        BaseParCamera g_BaseParCamera = null;
        Queue<ImageAll> ImageAll_Q = new Queue<ImageAll>();        

#if(SDK)
        static AutoResetEvent g_AutoResetEvent = new AutoResetEvent(false);
        IGrabResult g_GrabResult;
        Basler.Pylon.Camera g_Camera = null;
#endif

        /// 定义事件
        public event Action Trigger_event;//触发模式事件
        #endregion 定义

        #region 初始化
        public Camera_BSLSDK()
        {
            NameClass = "Camera_BSLSDK";
        }
        #endregion 初始化

        #region 打开相机
        /// <summary>
        /// 打开相机，并传入相机参数
        /// </summary>
        public override bool OpenCamera(BaseParCamera baseParCamera)
        {
            try
            {
                g_BaseParCamera = baseParCamera;
                g_SerialNumber = baseParCamera.Serial_Camera;//序列号
#if(SDK)
                g_Camera = new Basler.Pylon.Camera(g_SerialNumber);
                g_Camera.Open();

                // 设置触发模式
                if (g_BaseParCamera.BlUsingTrigger)
                {
                    g_Camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                    g_Camera.StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                    SetTrigger();
                }
                else
                {
                    g_Camera.Parameters[PLCamera.TriggerMode].TrySetValue("Off");
                    g_Camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                    SetContinue();
                }

                //通过像素格式来判断是否彩色相机
                g_PixelFormat = g_Camera.Parameters[PLCamera.PixelFormat].GetValue();
                if (g_PixelFormat.Contains("Mono"))
                {
                    BlColor = false;
                }
                else
                {
                    BlColor = true;
                }
                //触发模式
                if (g_BaseParCamera.BlUsingTrigger)
                {
                    g_Camera.StreamGrabber.ImageGrabbed += StreamGrabber_ImageGrabbed;
                }

                string path = new DirectoryInfo("../").FullName;
                if (path.Contains("bin"))//调试状态
                {
                    //设置60000ms检测不到心跳，那么相机自动断开，防止相机需要断电处理
                    g_Camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetToMaximum();
                    double heartbeatValue = g_Camera.Parameters[PLTransportLayer.HeartbeatTimeout].GetValue();
                }
                else
                {
                    //设置4000ms检测不到心跳，那么相机自动断开，防止相机需要断电处理
                    g_Camera.Parameters[PLTransportLayer.HeartbeatTimeout].TrySetValue(4000, IntegerValueCorrection.Nearest);
                }
                try
                {
                    if (g_Camera.Parameters[PLCamera.PixelFormat].TrySetValue(PLCamera.PixelFormat.BayerBG8))
                    {

                    }
                }
                catch (Exception ex)
                {

                }
#endif
                //相机打开成功的标志位
                BlOpen = true;

            }
            catch (Exception ex)
            {
                BlOpen = false;
                Log.L_I.WriteError(NameClass, ex);
            }
            return BlOpen;
        }
        #endregion 控制相机

        #region 抓取相机图像以及格式转换
#if(SDK)
        //★★★ 相机采集的回调函数
        //相机每次得到图像数据后，会自动调用该函数，给出采集结果：e.GrabResult 
        void StreamGrabber_ImageGrabbed(object sender, ImageGrabbedEventArgs e)
        {
            try
            {
                IGrabResult grabResult = e.GrabResult;

                if (grabResult.GrabSucceeded)
                {
                    g_AutoResetEvent.Set();
                }

                //使用触发模式
                if (g_BaseParCamera.BlUsingTrigger)
                {
                    //new Task(new Action(() =>
                    //{
                    ImageAll imTemp = new ImageAll();
                    imTemp.TimeTrigger = PathDirectory.P_I.GetShortTimeName();//获取外触发的时间

                    using (grabResult)
                    {
                        if (grabResult != null && grabResult.GrabSucceeded)
                        {
                            imTemp.Ho_Image = ConvertHobject(grabResult);
                            imTemp.BitmapSource = null;
                            imTemp.TimeTrigger = PathDirectory.P_I.GetTimeName();
                            grabResult.Dispose();
                        }
                    }
                    ImageAll_Q.Enqueue(imTemp);//图像加入队列

                    //})).Start();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {

            }
        }

#endif
        /// <summary>
        /// 抓取相机的图像,输出所有的格式
        /// </summary>
        public override ImageAll GrabImageAll()
        {
            ImageAll imageAll = new ImageAll();
            try
            {
                if (!BlOpen
                    || g_BaseParCamera.BlUsingTrigger)//外触发模式不进行All抓取
                {
                    return null;
                }
#if(SDK)
                if (g_Camera != null && g_Camera.IsOpen)
                {
                    if (g_BaseParCamera.BlUsingTrigger &&
                         g_Camera.StreamGrabber.IsGrabbing)
                    {
                        g_Camera.ExecuteSoftwareTrigger();
                        g_AutoResetEvent.WaitOne(500);
                    }
                    else
                    {
                        try
                        {
                            g_Camera.StreamGrabber.Start();
                            g_GrabResult = g_Camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                        }
                        catch (Exception ex)
                        {
                            Log.L_I.WriteError(NameClass, ex); //The stream grabber is already grabbing.说明取图太快，或者两个线程取图冲突
                        }
                    }
                }

                using (g_GrabResult)
                {
                    if (g_GrabResult != null && g_GrabResult.GrabSucceeded)
                    {
                        imageAll.Ho_Image = ConvertHobject(g_GrabResult);
                        imageAll.BitmapSource = ConvertImageBitmap(g_GrabResult);

                        PathDirectory p = new PathDirectory();
                        imageAll.TimeShort = p.GetShortTimeName();//图像抓取短时间
                        imageAll.Time = p.GetTimeName();
                    }
                }

                g_Camera.StreamGrabber.Stop();

#endif
                return imageAll;

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }

        /// <summary>
        /// 抓取相机的图像,输出Halcon格式
        /// </summary>
        public override ImageAll GrabImageHal()
        {
            ImageAll imageAll = new ImageAll();
            try
            {
                if (!BlOpen)
                {
                    return null;
                }
#if(SDK)
                //★★★ 确保相机当前是采集状态 && 确保相机可以接收软件触发指令
                if (g_Camera != null && g_Camera.IsOpen)
                {                  
                    if (g_BaseParCamera.BlUsingTrigger)//使用软触发
                    {
                        if (ImageAll_Q.Count > 0)
                        {
                            ImageAll im = ImageAll_Q.Peek();
                            ImageAll_Q.Dequeue();//出站
                            if (ImageAll_Q.Count > 20)//认为溢出
                            {
                                ImageAll_Q.Clear();
                                Log.L_I.WriteError(NameClass, "图像外触发溢出");
                            }
                            return im;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        try
                        {
                            g_Camera.StreamGrabber.Start();
                            g_GrabResult = g_Camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                        }
                        catch (Exception ex)
                        {
                            Log.L_I.WriteError(NameClass, ex);

                        }
                    }
                }

                using (g_GrabResult)
                {
                    if (g_GrabResult != null && g_GrabResult.GrabSucceeded)
                    {
                        imageAll.Ho_Image = ConvertHobject(g_GrabResult);
                        imageAll.BitmapSource = null;

                        PathDirectory p = new PathDirectory();
                        imageAll.Time = p.GetTimeName();
                        imageAll.TimeShort = p.GetShortTimeName();
                    }
                }
                g_Camera.StreamGrabber.Stop();
#endif
                return imageAll;

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }

#if(SDK)
        //抓取相机的图像
        public BitmapSource ConvertImageBitmap(IGrabResult grabResult)
        {
            try
            {
                if (BlOpen)
                {
                    BitmapSource bitSource = null;
                    switch (g_PixelFormat)
                    {
                        case "BayerRG8":
                            bitSource = GetBitmapSourceFromRGData(grabResult, grabResult.Width, grabResult.Height, "Rgb24");
                            break;

                        case "BayerBG8":
                            bitSource = GetBitmapSourceFromBGData(grabResult, grabResult.Width, grabResult.Height, "Rgb24");
                            break;
                        case "Mono8":
                            bitSource = GetBitmapSourceFromData(grabResult, grabResult.Width, grabResult.Height, "Bgr24");
                            break;
                    }
                    return bitSource;
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
        }


        //Bayer格式为RG
        public BitmapSource GetBitmapSourceFromRGData(IGrabResult grabResult, int width, int height, string pixelFormat = "Gray8")
        {
            try
            {
                BitmapSource bmpSrc = null;
               
                PixelDataConverter converter = new PixelDataConverter(); //把pixelFormat转化为Bitmap的PixelFormat属性
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);          
              
                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);  // Lock the bits of the bitmap.
               
                converter.OutputPixelFormat = PixelType.BGR8packed; // Place the pointer to the buffer of the bitmap.
                IntPtr ptrBmp = bmpData.Scan0;
                converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult); //Exception handling TODO
                bitmap.UnlockBits(bmpData);
                bmpSrc = BitmapToBitmapSource(bitmap);
                return bmpSrc;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }

        //Bayer格式为BG
        public BitmapSource GetBitmapSourceFromBGData(IGrabResult grabResult, int width, int height, string pixelFormat = "Gray8")
        {
            try
            {
                BitmapSource bmpSrc = null;
                
                PixelDataConverter converter = new PixelDataConverter();//把pixelFormat转化为Bitmap的PixelFormat属性
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                
                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);// Lock the bits of the bitmap.
            
                converter.OutputPixelFormat = PixelType.BGRA8packed;    // Place the pointer to the buffer of the bitmap.
                IntPtr ptrBmp = bmpData.Scan0;
                converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult); //Exception handling TODO
                bitmap.UnlockBits(bmpData);
                bmpSrc = BitmapToBitmapSource(bitmap);
                return bmpSrc;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }

        public BitmapSource GetBitmapSourceFromData(IGrabResult grabResult, int width, int height, string pixelFormat = "Gray8")
        {
            try
            {
                BitmapSource bmpSrc = null;
              
                PixelDataConverter converter = new PixelDataConverter();  //把pixelFormat转化为Bitmap的PixelFormat属性
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppRgb);
                
                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);// Lock the bits of the bitmap.
              
                converter.OutputPixelFormat = PixelType.BGRA8packed;  // Place the pointer to the buffer of the bitmap.
                IntPtr ptrBmp = bmpData.Scan0;
                converter.Convert(ptrBmp, bmpData.Stride * bitmap.Height, grabResult); //Exception handling TODO
                bitmap.UnlockBits(bmpData);
                bmpSrc = BitmapToBitmapSource(bitmap);
                return bmpSrc;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }
#endif
        /// <summary>
        /// 将bitmap转换为WPF可以用的bitmapImage
        /// </summary>
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public static BitmapSource BitmapToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            try
            {
                IntPtr ptr = bitmap.GetHbitmap();
                BitmapSource result =
                    System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                DeleteObject(ptr);
                return result;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Camera_BSLSDK", ex);
                return null;
            }
        }

#if(SDK)
        /// <summary>
        /// 将图像转换为Halcon 格式
        /// </summary>
        HObject ConvertHobject(IGrabResult grabResult)
        {
            try
            {
                HImage hImage = new HImage();
                HObject ho_Image = null;
                HOperatorSet.GenEmptyObj(out ho_Image);

                unsafe
                {
                    byte[] data = grabResult.PixelData as byte[];

                    fixed (byte* pt = data)
                    {
                        ho_Image.Dispose();
                        hImage.GenImage1("byte", grabResult.Width, grabResult.Height, new IntPtr(pt));
                    }
                }
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
                HObject ho_ImageOut = null;
                switch (g_PixelFormat)
                {
                    case "BayerRG8":
                        HOperatorSet.CfaToRgb(ho_Image, out ho_ImageOut, "bayer_rg", "bilinear");
                        break;

                    case "BayerBG8":
                        HOperatorSet.CfaToRgb(ho_Image, out ho_ImageOut, "bayer_bg", "bilinear");
                        break;
                    case "Mono8":
                        ho_ImageOut = ho_Image;
                        break;

                    default :
                        HOperatorSet.CfaToRgb(ho_Image, out ho_ImageOut, "bayer_bg", "bilinear");
                        break;
                }
                return ho_ImageOut;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }
#endif
        #endregion 抓取相机图像以及格式转换

        #region 设置相机
        //曝光时间
        public override bool SetExposure(double dblValue)
        {
            try
            {
#if(SDK)
                double valueMax = g_Camera.Parameters[PLCamera.ExposureTimeAbs].GetMaximum() / 1000;
                if (dblValue > valueMax)
                {
                    dblValue = valueMax;
                }
                g_Camera.Parameters[PLCamera.ExposureTimeAbs].TrySetValue(dblValue * 1000);  //basler曝光时间设置是us，此处和PointGrey统一
#endif
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        //增益
        public override bool SetGain(double dblValue)
        {
            try
            {
#if(SDK)
                long value = g_Camera.Parameters[PLCamera.GainRaw].GetValue();
                long valueMax = g_Camera.Parameters[PLCamera.GainRaw].GetMaximum();
                long valueMin = g_Camera.Parameters[PLCamera.GainRaw].GetMinimum();
                if (dblValue > valueMax)
                {
                    dblValue = valueMax;
                }
                if (dblValue < valueMin)
                {
                    dblValue = valueMin;
                }
                g_Camera.Parameters[PLCamera.GainRaw].SetValue((long)(dblValue));   
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
        /// 保存设置到通道
        /// </summary>
        public override bool SaveParChannel()
        {
            try
            {
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
                    SetTrigger();
                }
                else
                {
                    SetContinue();
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
        /// 触发模式
        /// </summary>
        bool SetTrigger()
        {
            try
            {
#if(SDK)
                if (g_BaseParCamera.BlUsingTrigger)//使用软触发
                {
                    g_Camera.Parameters[PLCamera.TriggerMode].TrySetValue("On");
                    g_Camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                    switch (g_BaseParCamera.TriggerSource_e)
                    {
                        case TriggerSourceCamera_enum.Software:
                            g_Camera.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Software);
                            break;
                        case TriggerSourceCamera_enum.Counter:
                            g_Camera.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Counter1Start);
                            break;
                        case TriggerSourceCamera_enum.Line1:
                            g_Camera.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Line1);
                            break;
                        case TriggerSourceCamera_enum.Line2:
                            g_Camera.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Line2);
                            break;
                        case TriggerSourceCamera_enum.Line3:
                            g_Camera.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Line3);
                            break;
                    }
                    //先进入采集模式
                    g_Camera.StreamGrabber.Stop();
                    g_Camera.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
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
        bool SetContinue()
        {
            try
            {               
#if(SDK)              
                g_Camera.Parameters[PLCamera.TriggerMode].TrySetValue("Off");
                g_Camera.Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
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


        public override bool SetTriggerDelay(double delay)
        {
            return false;
        }
  
        //进行一次软触发采图
        public override bool TriggerSoftware()
        {
            try
            {
 #if(SDK)    
                g_Camera.StreamGrabber.Stop();
                g_Camera.StreamGrabber.Start(GrabStrategy.LatestImages, GrabLoop.ProvidedByStreamGrabber);
                Thread.Sleep(50);
                //进行软触发，等待100ms，如果相机没有触发则抛出异常
                if (g_Camera.WaitForFrameTriggerReady(100, TimeoutHandling.ThrowException))
                {
                    g_Camera.ExecuteSoftwareTrigger();
                }
                else
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
            finally
            {
                
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
                    //关闭软触发
                }
                if (g_Camera != null && g_Camera.IsOpen && g_Camera.StreamGrabber.IsGrabbing)
                {
                    g_Camera.StreamGrabber.Stop();
                    g_Camera.Close();
                    g_Camera.Dispose();
                    g_Camera = null;
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
        #endregion 关闭相机
    }

}
