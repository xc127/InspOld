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
using System.Drawing;
using DealRobot;
using DealPLC;
using MahApps.Metro.Controls.Dialogs;
using System.IO;
using DealComprehensive;
using SetPar;
using SetComprehensive;
using DealFile;
using DealConfigFile;
using BasicClass;
using DealComInterface;
using ParComprehensive;

namespace Main
{
    public partial class MainWindow
    {
        #region 定义

        #endregion 定义

        #region 初始化
        /// <summary>
        /// 初始化参数
        /// </summary>
        void Init_Custom()
        {
            try
            {
            }
            catch (Exception ex)
            {
                
            }
        }

       
        #endregion 初始化

        #region CustomHand
        void DealCustomHand()
        {

        }
        #endregion


        #region 显示

        #endregion 显示

        #region 关闭
        public void Close_Custom()
        {
            try
            {
                ExcelMain.E_I.CloseProcess("EXCEL");
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion 关闭
    }
}
