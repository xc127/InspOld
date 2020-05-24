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
using System.Windows.Shapes;
using DealFile;
using BasicClass;

namespace DealCIM
{
    /// <summary>
    /// CIMWnd.xaml 的交互逻辑
    /// </summary>
    public partial class CIMWnd : Window
    {
        #region 定义
        string ClassName = "CIMWnd";
        #endregion

        public CIMWnd()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 保存输入的配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (short.Parse(tbCycTimes.Text) > 100 || short.Parse(tbCycTimes.Text) < 1)
                {
                    MessageBox.Show("循环次数必须在1-100之间");
                    return;
                }

                if (MessageBox.Show("是否确认修改为当前参数并保存", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    PostParams.P_I.StrSendQueue = tbSendQueue.Text;
                    PostParams.P_I.StrReadQueue = tbReadQueue.Text;
                    PostParams.P_I.StrIP = tbIP.Text;
                    PostParams.P_I.StrPort = tbPort.Text;
                    PostParams.P_I.iCycTimes = short.Parse(tbCycTimes.Text);
                    PostParams.P_I.StrUserID = tbUserID.Text;
                    PostParams.P_I.StrFab = tbFab.Text;
                    PostParams.P_I.StrArea = tbArea.Text;
                    PostParams.P_I.StrLine = tbLine.Text;
                    PostParams.P_I.StrOperation = tbOperation.Text;
                    PostParams.P_I.StrModelNo = tbModelNo.Text;
                    PostParams.P_I.StrCom = tbCodeCom.Text;
                    PostParams.P_I.iBaudrate = short.Parse(tbBaudrate.Text);
                    PostParams.P_I.ETypeCode = (TypeCode_enum)Enum.Parse(typeof(TypeCode_enum), cbTypeCode.SelectedItem.ToString());
                    PostParams.P_I.ETypeMode = (TypeMode)Enum.Parse(typeof(TypeMode), cbTypeMode.SelectedItem.ToString());

                    PostParams.P_I.WriteCimConfig();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 重连cim
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnReconnect_Click(object sender, RoutedEventArgs e)
        {
            CIM.C_I.ReConnect();
        }

        /// <summary>
        /// 初始化窗口之后从本地读取参数数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataContext = PostParams.P_I;//CIM.C_I;
                //CIM.InitParams();
                PostParams.P_I.InitParams();
                //this.cbTypeCode.SelectedItem = CIM.ETypeCode;
                //this.cbTypeMode.SelectedItem = CIM.ETypeMode;
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
            //binding
            
        }
        /// <summary>
        /// 切换过账模式，实时更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbTypeMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //避免因为窗口load的时候触发而有问题
            if (!this.IsLoaded)
                return;
            PostParams.P_I.ReadCimConfig(cbTypeMode.SelectedItem.ToString()); 
            //tbSendQueue.Text = CIM.StrSendQueue;
            //tbReadQueue.Text = CIM.StrReadQueue;
            //tbIP.Text = CIM.StrIP;
            //tbPort.Text = CIM.StrPort;
        }
    }
}
