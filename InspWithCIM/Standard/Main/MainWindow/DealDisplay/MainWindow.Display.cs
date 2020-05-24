using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealDisplay;
using BasicClass;
using Common;
using BasicDisplay;
using System.Threading;
using System.Threading.Tasks;

namespace Main
{
    partial class MainWindow
    {
        #region 定义
        BaseUCDisplaySum g_BaseUCDisplaySum = null;
        #endregion 定义

        #region 添加显示窗口
        /// <summary>
        /// 选择添加窗口控件的数量
        /// </summary>
        void CreateUIDisplay()
        {
            try
            {
                double co = 1;
                if (RegeditMain.R_I.Width_Win * RegeditMain.R_I.Height_Win != 0)
                {
                    double co1 = RegeditMain.R_I.Width_Win / 1300;
                    double co2 = RegeditMain.R_I.Height_Win / 785;

                    if (co1 > 1)
                    {
                        co = (co1 > co2) ? co1 : co2;
                    }
                    else
                    {
                        co = (co1 < co2) ? co1 : co2;
                    }
                }
                

                double width = 0;
                double height = 0;
                switch (ParSetDisplay.P_I.NumWinDisplayImage)
                {
                    case 1:
                        g_BaseUCDisplaySum = new UCDisplaySum1();
                        width = RegeditMain.R_I.Height_gdCamera - 4;
                        height = RegeditMain.R_I.Height_gdCamera - 4;
                        break;

                    case 2:
                        g_BaseUCDisplaySum = new UCDisplaySum2();
                        double delta = gdCamera.ActualWidth / (850 * co);
                        width = gdCamera.ActualWidth;
                        height = 450 * delta * co;
                        break;

                    case 3:
                        g_BaseUCDisplaySum = new UCDisplaySum3();
                        width = RegeditMain.R_I.Width_gdCamera - 4;
                        height = RegeditMain.R_I.Height_gdCamera - 4;
                        break;

                    case 4:
                        g_BaseUCDisplaySum = new UCDisplaySum4();
                        width = RegeditMain.R_I.Width_gdCamera - 4;
                        height = RegeditMain.R_I.Height_gdCamera - 4;
                        break;

                    case 5:
                        g_BaseUCDisplaySum = new UCDisplaySum5();
                        width = RegeditMain.R_I.Width_gdCamera - 4;
                        height = RegeditMain.R_I.Height_gdCamera - 4;
                        break;

                    case 6:
                        g_BaseUCDisplaySum = new UCDisplaySum6();
                        width = RegeditMain.R_I.Width_gdCamera - 4;
                        height = RegeditMain.R_I.Height_gdCamera - 4;
                        break;

                    case 7:
                        g_BaseUCDisplaySum = new UCDisplaySum7();
                        width = RegeditMain.R_I.Width_gdCamera - 4;
                        height = RegeditMain.R_I.Height_gdCamera - 4;
                        break;

                    case 8:
                        g_BaseUCDisplaySum = new UCDisplaySum8();
                        width = RegeditMain.R_I.Width_gdCamera - 4;
                        height = RegeditMain.R_I.Height_gdCamera - 4;
                        break;
                }
               

                //设定布局方式
                g_BaseUCDisplaySum.HorizontalAlignment = HorizontalAlignment.Center;
                g_BaseUCDisplaySum.VerticalAlignment = VerticalAlignment.Center;
                g_BaseUCDisplaySum.Width = width;
                g_BaseUCDisplaySum.Height = height;
                g_BaseUCDisplaySum.Margin = new Thickness(0, 0, 0, 0);               
               
                //添加控件
                this.gdCamera.Children.Add(g_BaseUCDisplaySum);

                g_BaseUCDisplaySum.Init(uCStateWork, uCAlarm);
                g_BaseUCDisplaySum.CameraError_event += new StrAction(g_UCDisplayCameraSum_CameraError_event);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 添加显示窗口

        #region 初始化
        /// <summary>
        /// 初始化显示
        /// </summary>
        void Init_Display()
        {
            try
            {
                new Task<bool>(ParSetDisplay.P_I.CheckPar).Start();//确认参数
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化
    }
}
