using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

using System.Windows.Media;
using System.Windows.Media.Imaging;

using System.Windows;
using HalconDotNet;
using System.Runtime.InteropServices;
using System.Runtime;

using System.Diagnostics;
using System.IO;
using BasicClass;

namespace Camera
{
    public class ImageTrans
    {
        public Bitmap ToGrayBitmap(byte[] rawValues, int width, int height)
        {
            try
            {
                //// 申请目标位图的变量，并将其内存区域锁定
                Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
                System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, width, height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

                //// 获取图像参数
                int stride = bmpData.Stride;　 // 扫描线的宽度
                int offset = stride - width;　 // 显示宽度与扫描线宽度的间隙
                IntPtr iptr = bmpData.Scan0;　 // 获取bmpData的内存起始位置
                int scanBytes = stride * height;　　 // 用stride宽度，表示这是内存区域的大小

                //// 下面把原始的显示大小字节数组转换为内存中实际存放的字节数组
                int posScan = 0, posReal = 0;　　 // 分别设置两个位置指针，指向源数组和目标数组
                byte[] pixelValues = new byte[scanBytes];　 //为目标数组分配内存

                for (int x = 0; x < height; x++)
                {
                    //// 下面的循环节是模拟行扫描
                    for (int y = 0; y < width; y++)
                    {
                        pixelValues[posScan++] = rawValues[posReal++];
                    }
                    posScan += offset;　 //行扫描结束，要将目标位置指针移过那段“间隙”
                }

                //// 用Marshal的Copy方法，将刚才得到的内存字节数组复制到BitmapData中
                System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, iptr, scanBytes);
                bmp.UnlockBits(bmpData);　 // 解锁内存区域

                ////// 下面的代码是为了修改生成位图的索引表，从伪彩修改为灰度
                System.Drawing.Imaging.ColorPalette tempPalette;
                using (Bitmap tempBmp = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format8bppIndexed))
                {
                    tempPalette = tempBmp.Palette;
                }
                for (int i = 0; i < 256; i++)
                {
                    tempPalette.Entries[i] = System.Drawing.Color.FromArgb(i, i, i);
                }

                bmp.Palette = tempPalette;

                return bmp;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCDisplayCameraSum", ex);
                return null;
            }
        }
        /// <summary>
        /// 将bitmap转换为WPF可以用的bitmapImage
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public static BitmapSource BitmapToBitmapSource(System.Drawing.Bitmap bitmap)
        {
            IntPtr ptr = bitmap.GetHbitmap();
            BitmapSource result =
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            //release resource
            DeleteObject(ptr);
            return result;
        }

        public BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            return null;
        }

        //转换为黑白的bitmap图像
        public void GenertateGrayBitmap(HObject image, out Bitmap res)
        {
            HTuple hpoint, type, width, height;

            const int Alpha = 255;
            int[] ptr = new int[2];
            HOperatorSet.GetImagePointer1(image, out hpoint, out type, out width, out height);

            res = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            ColorPalette pal = res.Palette;
            for (int i = 0; i <= 255; i++)
            {
                pal.Entries[i] = System.Drawing.Color.FromArgb(Alpha, i, i, i);
            }
            res.Palette = pal;
            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = res.LockBits(rect, ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            int PixelSize = Bitmap.GetPixelFormatSize(bitmapData.PixelFormat) / 8;
            ptr[0] = bitmapData.Scan0.ToInt32();
            ptr[1] = hpoint.I;
            if (width % 4 == 0)
                CopyMemory(ptr[0], ptr[1], width * height * PixelSize);
            else
            {
                for (int i = 0; i < height - 1; i++)
                {
                    ptr[1] += width;
                    CopyMemory(ptr[0], ptr[1], width * PixelSize);
                    ptr[0] += bitmapData.Stride;
                }
            }
            res.UnlockBits(bitmapData);



        }
        [DllImport("kernel32.dll")]
        public static extern void CopyMemory(int Destination, int Source, int Length);


        //转换为彩色的bitmap图像
        public void GenertateRGBBitmap(HObject image, out Bitmap res)
        {
            HTuple hred, hgreen, hblue, type, width, height;

            HOperatorSet.GetImagePointer3(image, out hred, out hgreen, out hblue, out type, out width, out height);

            res = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            //res = new Bitmap[3];
            //res[0] = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            //res[1] = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            //res[2] = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            Rectangle rect = new Rectangle(0, 0, width, height);
            BitmapData bitmapData = res.LockBits(rect, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            //BitmapData bitmapDataR = res[0].LockBits(rect, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            //BitmapData bitmapDataG = res[1].LockBits(rect, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            //BitmapData bitmapDataB = res[2].LockBits(rect, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            //此处花的时间太长，需优化
            unsafe
            {


                byte* bptr = (byte*)bitmapData.Scan0;
                byte* r = ((byte*)hred.I);
                byte* g = ((byte*)hgreen.I);
                byte* b = ((byte*)hblue.I);
                for (int i = 0; i < width * height; i++)
                {
                    bptr[i * 4] = (b)[i];
                    bptr[i * 4 + 1] = (g)[i];
                    bptr[i * 4 + 2] = (r)[i];
                    bptr[i * 4 + 3] = 255;
                }
            }

            res.UnlockBits(bitmapData);

            //********************************************//
            ////另一种方式
            //int[] ptr = new int[2];

            ////B通道
            //ptr[0] = bitmapDataB.Scan0.ToInt32();
            //ptr[1] = hblue.I;

            //if (width % 4 == 0)
            //    CopyMemory(ptr[0], ptr[1], width * height);
            //else
            //{
            //    for (int i = 0; i < height - 1; i++)
            //    {
            //        ptr[1] += width;
            //        CopyMemory(ptr[0], ptr[1], width);
            //        ptr[0] += bitmapDataB.Stride;
            //    }
            //}
            ////G通道
            //ptr[0] = bitmapDataG.Scan0.ToInt32();
            //ptr[1] = hgreen.I;

            //if (width % 4 == 0)
            //    CopyMemory(ptr[0], ptr[1], width * height);
            //else
            //{
            //    for (int i = 0; i < height - 1; i++)
            //    {
            //        ptr[1] += width;
            //        CopyMemory(ptr[0], ptr[1], width);
            //        ptr[0] += bitmapDataG.Stride;
            //    }
            //}

            ////R通道
            //ptr[0] = bitmapDataR.Scan0.ToInt32();
            //ptr[1] = hred.I;

            //if (width % 4 == 0)
            //    CopyMemory(ptr[0], ptr[1], width * height);
            //else
            //{
            //    for (int i = 0; i < height - 1; i++)
            //    {
            //        ptr[1] += width;
            //        CopyMemory(ptr[0], ptr[1], width);
            //        ptr[0] += bitmapDataR.Stride;
            //    }
            //}

            //res[0].UnlockBits(bitmapDataB);
            //res[1].UnlockBits(bitmapDataG);
            //res[2].UnlockBits(bitmapDataR);
        }
    }
}
