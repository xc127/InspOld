using BasicClass;
using System;
using System.Windows;
using System.Windows.Input;

namespace Main
{
    /// <summary>
    /// UCResult.xaml 的交互逻辑
    /// </summary>
    public partial class UCResultIns : BasicClass.BaseControl
    {
        public UCResultIns()
        {
            InitializeComponent();
        }

        private void BaseControl_Loaded(object sender, RoutedEventArgs e)
        {
            ShowPar_Invoke();

            ShowLabel_Invoke(lblTotalNum, ParAnalysis.P_I.g_ProductNumInfoNow.NumAll.ToString());
        }
        /// <summary>
        /// 检测结果显示
        /// </summary>
        /// <param name="result"></param>
        public void ShowResult(int result)
        {
            try
            {
                string color = "red";
                string str = "NG";
                if (result < 1)//0402改动 这里不再等于0
                {
                    str = "OK";
                    color = "green";
                }
                ShowLabel_Invoke(lblResult, str, color);
                CountNum(result);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCResultIns", ex);
            }
        }

        private void CountNum(int result)
        {
            //先调用班次信息来刷新产量
            ShowLabel_Invoke(lblShift, ParAnalysis.P_I.ShiftNow);

            ParAnalysis.P_I.g_ProductNumInfoNow.NumAll++;            

            if (result == 0)
            {
                ParAnalysis.P_I.g_ProductNumInfoNow.NumOK++;
            }
            else
            {
                ParAnalysis.P_I.g_ProductNumInfoNow.NumNG++;
            }

            ShowPar_Invoke();
            ParAnalysis.P_I.WriteDateNow();
        }

        public override void ShowPar()
        {

            ShowLabel_Invoke(lblTotalNum, ParAnalysis.P_I.g_ProductNumInfoNow.NumAll.ToString());


            ShowLabel_Invoke(lblInspNGNum, ParAnalysis.P_I.g_ProductNumInfoNow.NumNG.ToString());
            ShowLabel_Invoke(lblCodeNGNum, DealCIM.CIM.CodeNGCount.ToString());
            ShowLabel_Invoke(lblNGShell, ParAnalysis.P_I.g_ProductNumInfoNow.NumNGShell.ToString());
            ShowLabel_Invoke(lblNGCorner, ParAnalysis.P_I.g_ProductNumInfoNow.NumNGCorner.ToString());
            ShowLabel_Invoke(lblNGOther, ParAnalysis.P_I.g_ProductNumInfoNow.NumNGOther.ToString());
        }

        /// <summary>
        /// 显示二维码，同时初始化结果
        /// </summary>
        /// <param name="code"></param>
        public void ShowCode(string code)
        {
            try
            {
                ShowLabel_Invoke(lblCode, code);
                ShowLabel_Invoke(lblResult, "Wait...", "blue");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCResultIns", ex);
            }
        }

        private void lblClear_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ParAnalysis.P_I.g_ProductNumInfoNow.NumAll = 0;
            ParAnalysis.P_I.g_ProductNumInfoNow.NumOK = 0;
            ParAnalysis.P_I.g_ProductNumInfoNow.NumNG = 0;
            ParAnalysis.P_I.g_ProductNumInfoNow.NumNGShell = 0;
            ParAnalysis.P_I.g_ProductNumInfoNow.NumNGCorner = 0;
            ParAnalysis.P_I.g_ProductNumInfoNow.NumNGOther = 0;
            ShowPar_Invoke();
        }
    }
}
