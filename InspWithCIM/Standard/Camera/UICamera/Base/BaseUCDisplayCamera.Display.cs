using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Threading;
using System.Threading.Tasks;
using BasicClass;
using System.Windows;
using HalconDotNet;
using Common;
using System.IO;
using DealConfigFile;

namespace Camera
{
    partial class BaseUCDisplayCamera
    {
        /// <summary>
        /// 显示图像以及信息
        /// </summary>
        public override void DisplayImageInfo()
        {
            try
            {
                DisplayLocalImageInfo();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 显示本地图片信息
        /// </summary>
        void DisplayLocalImageInfo()
        {
            try
            {
                if (BlLocalImage)
                {
                    string path = g_PathLocalImage_L[NoLocalImage].Replace(ComValue.c_PathImageLog, "");
                  
                    DispString(path, 10, -1, FullHeight_HWin - 15 - 2 * CurWin_BeginRow, "red");
                    //DispInfoPop(path);
                    //this.Dispatcher.Invoke(new Action(VisiblePreNext));
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        
        /// <summary>
        ///显示当前位置和灰度值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public override void ShowPosandGray(double x, double y)
        {
            try
            {
                HTuple gray = 0;
                if (y > HeightImage ||
                    x > WidthImage
                    || x < 0
                    || y < 0)
                {
                    return;
                }
                HOperatorSet.GetGrayval(Ho_Image, y, x, out gray);
                if (ParCameraWork.P_I.ParCameraWork_L[NoCamera - 1].TypeImageCoord_e != TypeImageCoord_enum.LeftTop)
                {
                    y = HeightImage - y;
                }

                this.Dispatcher.Invoke(
                  new Dbl2Action((a, b) =>
                  {
                      lblPosGray.Content = string.Format("X{0},Y{1}*({2})", a.ToString("f0"), b.ToString("f0"), gray.ToString());
                  }),
                  x, y);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }


        /// <summary>
        /// 使能显示控件
        /// </summary>
        public void EnableShowPosandGray()
        {
            try
            {
                lblPosGray.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 清空polygn中的list
        /// </summary>
        public void ClearShape()
        {
            g_DrawDisplay.ClearShape();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="indexP"></param>
        public void SelectROI(string type, int indexP)
        {
            g_DrawDisplay.SelectShape(type,indexP);
        }



        ///// <summary>
        ///// 保存截图，不能截屏halcon
        ///// </summary>
        ///// <param name="ui">控件名称</param>
        ///// <param name="filename">图片文件名</param>
        //public void SaveFrameworkElementToImage()
        //{
        //    try
        //    {
        //        this.Dispatcher.Invoke(new Action(() =>
        //            {
        //                System.IO.FileStream ms = new System.IO.FileStream("p.jpg", System.IO.FileMode.Create);
        //                System.Windows.Media.Imaging.RenderTargetBitmap bmp = new System.Windows.Media.Imaging.RenderTargetBitmap((int)this.ActualWidth, (int)this.ActualHeight, 96d, 96d, System.Windows.Media.PixelFormats.Pbgra32);
        //                bmp.Render(ccImage);
        //                System.Windows.Media.Imaging.JpegBitmapEncoder encoder = new System.Windows.Media.Imaging.JpegBitmapEncoder();
        //                encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bmp));
        //                encoder.Save(ms);
        //                ms.Close();

        //                File.Copy("p.jpg", "D:\\p.jpg", true);
        //            }));
              
        //    }
        //    catch (Exception ex)
        //    {
        //        //记录异常
        //    }
        //}
    }
}
