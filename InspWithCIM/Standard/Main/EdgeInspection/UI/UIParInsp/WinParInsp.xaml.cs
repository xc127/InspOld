using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BasicClass;

namespace Main
{
    /// <summary>
    /// WinParInsp.xaml 的交互逻辑
    /// </summary>
    public partial class WinParInsp : Window
    {
        //ParInspection g_Par = null;
        readonly static string g_NameClass = "WinParInsp";
        ParInspAll g_ParAll = new ParInspAll();
        int g_TypeIndex = 0;
        string strRegex = "^[1-4]{1}([,]{1}[1-4]{1})*$";
        bool blSideIndexSetOK = true;
        public static WinParInsp g_WinInst = null;
        public static bool IsManufacturer
        {
            get; set;
        }

        public static WinParInsp GetWinInst(ParInspAll parAll)
        {
            try
            {
                if (g_WinInst == null)
                {
                    g_WinInst = new WinParInsp(parAll);
                }
                g_WinInst.Focus();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(g_NameClass, ex);
            }
            return g_WinInst;
        }

        public WinParInsp(ParInspAll par)
        {
            InitializeComponent();
            g_ParAll = par;
            this.DataContext = g_ParAll.BaseParInspection_L[g_TypeIndex];
            this.Title = par.NameCam;
            if (g_TypeIndex == 0)
            {
                RdbSide1.IsChecked = true;
            }
            else
            {
                RdbSide2.IsChecked = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableControl((int)Authority.Authority_e);
        }

        void EnableControl(int level)
        {
            bool isEnable = level >= 3;
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    DudAmp.IsEnabled = isEnable;
                    DudThRectangleularity.IsEnabled = isEnable;
                    DudSmooth.IsEnabled = isEnable;
                    DudSmooth_Copy.IsEnabled = isEnable;
                    DudDblOutRate.IsEnabled = isEnable;
                    DudThITO.IsEnabled = isEnable;
                    TxtSideMatch.IsEnabled = isEnable;
                    DudThShellY.IsEnabled = isEnable;
                    DudThShellX.IsEnabled = isEnable;
                    DudThCornerX.IsEnabled = isEnable;
                    DudThCornerY.IsEnabled = isEnable;
                    DudThPreiectionX.IsEnabled = isEnable;
                    DudThPreiectionY.IsEnabled = isEnable;
                    DudNumCornerStart.IsEnabled = isEnable;
                    DudNumCornerEnd.IsEnabled = isEnable;
                    //CbxIgnoreMark.IsEnabled = isEnable;
                    cbUseOr.IsEnabled = isEnable;
                    DudThresholdEdge.IsEnabled = isEnable;
                    DudThresholdGlass.IsEnabled = isEnable;
                }));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(g_NameClass, ex);
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!blSideIndexSetOK)
                {
                    WinMsgBox.ShowMsgBox("边序号设置不合法，请检查");
                    TxtSideMatch.Focus();
                    return;
                }
                var a_Var = g_ParAll[0].IntArrSidesMatch.Intersect(g_ParAll[1].IntArrSidesMatch).ToArray();
                if (g_ParAll[0].IntArrSidesMatch.Intersect(g_ParAll[1].IntArrSidesMatch).ToArray().Length != 0)
                {
                    WinMsgBox.ShowMsgBox("边序号设置重复，请检查");
                    return;
                }

                if (g_ParAll.WriteIni())
                {
                    WinMsgBox.ShowMsgBox("保存完成");
                    Close();
                }

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(g_NameClass, ex);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            g_WinInst = null;
        }

        private void RdbSide1_Checked(object sender, RoutedEventArgs e)
        {
            if (RdbSide1.IsMouseOver)
            {
                //CbxIsCFSide.IsEnabled = true;
                g_TypeIndex = 0;
                this.DataContext = g_ParAll[0];
            }
        }

        private void RdbSide2_Checked(object sender, RoutedEventArgs e)
        {
            if (RdbSide2.IsMouseOver)
            {
                g_TypeIndex = 1;
                g_ParAll[1].BlInspCF = false;
               // CbxIsCFSide.IsEnabled = false;
                this.DataContext = g_ParAll[1];
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                blSideIndexSetOK = false;
                if (Regex.IsMatch(TxtSideMatch.Text, strRegex))
                {
                    LblState.Content = "";
                    blSideIndexSetOK = true;
                }
                else
                {
                    LblState.Content = "设置不合法，请检查";
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(g_NameClass, ex);
            }
        }

    }
}
