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
using BasicClass;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using HalconDotNet;
using BasicDisplay;
using System.Threading;
using System.Threading.Tasks;
using DealConfigFile;
using Common;
using DealLog;
using System.Drawing.Imaging;
using DealFile;

namespace Camera
{
    partial class BaseUCDisplayCamera
    {
        #region 定义
        public bool BlRealGrab = false;

        bool BlGrabForAuto = false;
        bool BlFinishGrab = false;

        List<ImageAll> ImDisp_L = new List<ImageAll>();
        #endregion 定义


        /// <summary>
        /// 处于实时抓取状态
        /// </summary>
        public void InitRealGrab()
        {
            try
            {
                if(!g_CameraBase.BlOpen)//相机如果没有打开则退出
                {
                    return;
                }

                if (BlRealGrab)
                {
                    return;
                }

                BlRealGrab = true;
                if(BlRealImage)//如果实时显示，则要先退出
                {
                    this.Dispatcher.Invoke(new Action(RecoverPhotoOnly));
                    Thread.Sleep(500);
                }
                this.Dispatcher.Invoke(new Action(()=>
                {
                    //设定实时显示状态
                    SetTitleColor(TitleType_enum.RealImage);

                    EnableBtnPhotoCtr(false);//停止按钮

                }));
             
                new Task(RealGrab).Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 停止实时触发
        /// </summary>
        public void StopRealGrab()
        {
            try
            {
                BlRealGrab = false;
                if (ImDisp_L != null)
                {
                    ImDisp_L.Clear();
                }
                SetTitleColor(TitleType_enum.ShowImage);

                EnableBtnPhotoCtr(true);//停止按钮
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        public int TimeCycForReal = 30;
        /// <summary>
        /// 实时状态下进行处理和显示
        /// </summary>
        void RealGrab()
        {
            try
            {               
                while (BlRealGrab)
                {
                    Thread.Sleep(2);
                    Thread.Sleep(TimeCycForReal);//循环间隔
                    try
                    {
                        ImageAll im = null;
                        if (!BlGrabForAuto)//非触发状态，如果处于触发状态，则调用GrabForAuto
                        {
                            BlFinishGrab = false;
                            //im = GrabImageHalForReal();
                            BlFinishGrab = true;
                        }
                        //显示图像以及字符
                        if (im != null)
                        {
                            //显示图像
                            DispImage(im);
                            //显示ROI已经结果
                            DispHobject_L();
                        }
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(200);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 自动抓取
        /// 是否是DealGrabImage项目调用
        /// </summary>
        public ImageAll GrabForAuto()
        {
            try
            {
                if (BlRealGrab)
                {
                    BlGrabForAuto = true;
                    int num = 0;
                    while (!BlFinishGrab)//等待抓取到图像
                    {
                        Thread.Sleep(20);
                        num++;
                        if (num > 50)
                        {
                            return null;
                        }
                    }
                    if (BlFinishGrab)
                    {
                        ImageAll im = null;// GrabImageHalForReal();
                        if (ImBitInit != null)
                        {                           
                            ImBitInit.Dispose();//先进行释放
                            ImBitInit = null;
                        }
                        if (Bit != null)
                        {                            
                            Bit.Dispose();//先进行释放
                            Bit = null;
                        }
                        ImBitInit = im;

                        BlGrabForAuto = false;//自动触发结束
                        return im;
                    }
                    return null;
                }
                else
                {
                    return GrabImageHal();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return null;
            }
        }


        #region 显示_HL
        /// <summary>
        /// 
        /// </summary>
        /// <param name="im_L"></param>
        public void DispHobjectFirst_L(List<ImageAll> im_L)
        {
            try
            {
                foreach (ImageAll item in ImDisp_L)
                {
                    if (item != null)
                    {
                        item.Dispose();
                    }
                }
                ImDisp_L = im_L;

                foreach (ImageAll item in im_L)
                {
                    if (item == null)
                    {
                        Log.L_I.WriteError(NameClass, "DispHobject_L中im为null");
                        continue;
                    }
                    HOperatorSet.SetDraw(Hv_WinHandle, item.TypeFill);
                    string color = item.Color;
                    if (color.Contains("_"))
                    {
                        color = color.Replace("_", " ");
                    }
                    if (color == "")
                    {
                        color = "red";
                    }
                    HOperatorSet.SetColor(Hv_WinHandle, color);

                    HOperatorSet.SetLineWidth(Hv_WinHandle, item.Thickness);
                    item.Ho_Image.DispObj(Hv_WinHandle);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 显示上一次的ROI和结果,目前只在实时处理显示中使用
        /// </summary>
        public void DispHobject_L()
        {
             Int64 pp=0;
             try
             {
                foreach (ImageAll item in ImDisp_L)
                {
                    if (item == null)
                    {
                        Log.L_I.WriteError(NameClass, "DispHobject_L中im为null");
                        continue;
                    }
                    HOperatorSet.SetDraw(Hv_WinHandle, item.TypeFill);
                    string color = item.Color;
                    if (color.Contains("_"))
                    {
                        color = color.Replace("_", " ");
                    }
                    if (color == "")
                    {
                        color = "red";
                    }
                    HOperatorSet.SetColor(Hv_WinHandle, color);

                    HOperatorSet.SetLineWidth(Hv_WinHandle, item.Thickness);
                    IntPtr p = item.Ho_Image.Key;

                    pp = p.ToInt64();
                    if (pp != 0)
                    {
                        item.Ho_Image.DispObj(Hv_WinHandle);
                    }
                }
             }
             catch (Exception ex)
             {
                 Log.L_I.WriteError(NameClass, ex);
             }
        }
        #endregion 显示_HL
    }
}
