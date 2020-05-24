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

namespace Camera
{
    partial class BaseUCDisplayCamera
    {
        #region  定义
        //字体
        public int SizeFont
        {
            get
            {
                return ParSetDisplay.P_I.SizeFont;
            }
        }

        public int XOffset
        {
            get
            {
                return ParSetDisplay.P_I.XOffset + 20;
            }
        }

        public int YOffset
        {
            get
            {
                return ParSetDisplay.P_I.YOffset + 20;
            }
        }
        #endregion  定义


        /// <summary>
        /// 显示或者隐藏Poppup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gpbCamera_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //显示结果
                if (ppShowResult.IsOpen)
                {
                    ppShowResult.IsOpen = false;
                }
                else
                {
                    ppShowResult.IsOpen = true;
                }

                //状态信息
                //if (ppShowState.IsOpen)
                //{
                //    HidePPState();
                //}
                //else
                //{
                //    VisiblePPState();
                //}
            }
            catch (Exception ex)
            {

            }
           
        }

        #region 显示
        /// <summary>
        /// 显示信息，默认LimeGreen
        /// </summary>
        /// <param name="info"></param>
        public void ShowResult(string info)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    //this.ppShow.IsOpen = false;
                    this.ppShowResult.IsOpen = true;
                    txtInfo.Text = info;
                    txtInfo.FontSize = SizeFont;
                    txtInfo.Foreground = Brushes.LimeGreen;

                    ppShowResult.HorizontalOffset = XOffset;
                    ppShowResult.VerticalOffset = YOffset;
                }));
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 显示信息，表示结果true或者false
        /// </summary>
        /// <param name="info"></param>
        /// <param name="blResult"></param>
        public void ShowResult(string info, bool blResult)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                    {
                        //this.ppShow.IsOpen = false;
                        this.ppShowResult.IsOpen = true;
                        txtInfo.Text = info;
                        if (blResult)
                        {
                            txtInfo.Foreground = Brushes.LimeGreen;
                        }
                        else
                        {
                            txtInfo.Foreground = Brushes.Red;
                        }
                        txtInfo.FontSize = SizeFont;
                        ppShowResult.HorizontalOffset = XOffset;
                        ppShowResult.VerticalOffset = YOffset;
                    }));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 显示信息，传入颜色
        /// </summary>
        /// <param name="info"></param>
        /// <param name="color"></param>
        public void ShowResult(string info, string color)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    //this.ppShow.IsOpen = false;
                    this.ppShowResult.IsOpen = true;
                    txtInfo.Text = info;

                    Color c = (Color)ColorConverter.ConvertFromString(color.Replace(" ", ""));
                    txtInfo.Foreground = new SolidColorBrush(c);

                    ppShowResult.HorizontalOffset = XOffset;
                    ppShowResult.VerticalOffset = YOffset;

                }));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

        #region 弹框
        /// <summary>
        /// 显示弹框
        /// </summary>
        public void ShowPPResult()
        {
            this.Dispatcher.Invoke(new Action(() =>
                {
                    this.ppShowResult.IsOpen = true;
                }));
        }

        public void HidePPResult()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.ppShowResult.IsOpen = false;
                txtInfo.Text = "";
            }));
        }

        public void MovePPResult()
        {
            try
            {
                if (this.ppShowResult.IsOpen)
                {
                    this.ppShowResult.IsOpen = false;
                    this.ppShowResult.IsOpen = true;
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// 显示控件
        /// </summary>
        public void VisibleTxtResult()
        {
            try
            {
                if (this.ppShowResult.IsOpen)
                {
                    //this.ppShowResult.Visibility = Visibility.Visible;
                    txtInfo.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 显示控件
        /// </summary>
        public void UnVisibleTxtResult()
        {
            try
            {
                if (this.ppShowResult.IsOpen)
                {
                    //this.ppShowResult.Visibility = Visibility.Hidden;
                    txtInfo.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 弹框

        //#region State信息
        ///// <summary>
        ///// 设置状态弹框宽度
        ///// </summary>
        //void SetPPStateSize()
        //{
        //    try
        //    {
        //        ppShowState.Width = this.ActualWidth - 20;//设置宽度
        //        txtState.Text = "";
        //        ppShowState.IsOpen = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.L_I.WriteError(NameClass, ex);
        //    }
        //}

        ///// <summary>
        ///// 隐藏
        ///// </summary>
        //public void HidePPState()
        //{
        //    try
        //    {
        //        if (BlLocalImage)
        //        {
        //            ppShowState.IsOpen = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.L_I.WriteError(NameClass, ex);
        //    }
        //}

        ///// <summary>
        ///// 显示
        ///// </summary>
        //public void VisiblePPState()
        //{
        //    try
        //    {
        //        if (BlLocalImage)
        //        {
        //            ppShowState.IsOpen = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.L_I.WriteError(NameClass, ex);
        //    }
        //}

        ///// <summary>
        ///// 清除
        ///// </summary>
        //void ClearPPState()
        //{
        //    try
        //    {
        //        ppShowState.IsOpen = false;
        //        txtState.Text = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.L_I.WriteError(NameClass, ex);
        //    }
        //}

        ///// <summary>
        ///// 通过弹框显示信息
        ///// </summary>
        //void DispInfoPop(string str)
        //{
        //    try
        //    {
        //        if (BlLocalImage)
        //        {
        //            this.Dispatcher.Invoke(new Action(() =>
        //            {
        //                txtState.Text = str;
        //            }));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.L_I.WriteError(NameClass, ex);
        //    }
        //}

        ///// <summary>
        ///// 移动PP
        ///// </summary>
        //public void MovePPShowState()
        //{
        //    try
        //    {
        //        if (BlLocalImage)
        //        {
        //            this.ppShowState.IsOpen = false;
        //            this.ppShowState.IsOpen = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //#endregion State信息
    }
}
