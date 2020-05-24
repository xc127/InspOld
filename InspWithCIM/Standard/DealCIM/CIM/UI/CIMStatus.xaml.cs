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

namespace DealCIM
{
    /// <summary>
    /// CIMStatus.xaml 的交互逻辑
    /// </summary>
    public partial class CIMStatus : UserControl
    {
        public CIMStatus()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置arm持有Chipid内容
        /// </summary>
        /// <param name="content"></param>
        public void SetlblArmChipID(string content)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lblArmChipID.Content = content;
            }));
        }

        /// <summary>
        /// 设置Fork持有ChipID内容
        /// </summary>
        /// <param name="content"></param>
        public void SetlblForkChipID(string content)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lblForkChipID.Content = content;
            }));
        }

        /// <summary>
        /// 设置Chipid过账结果
        /// </summary>
        /// <param name="content"></param>
        public void SetlblChipIDResult(string content, bool isAlarm)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lblChipIDResult.Content = content;
                lblChipIDResult.Foreground = new SolidColorBrush(isAlarm ? Colors.Red : Colors.Blue);
            }));
        }

        /// <summary>
        /// 设置Lot过账结果
        /// </summary>
        /// <param name="content"></param>
        public void SetlblLotResult(string content, bool isAlarm)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lblLotResult.Content = content;
                lblLotResult.Foreground = new SolidColorBrush(isAlarm ? Colors.Red : Colors.Blue);
            }));
        }

        /// <summary>
        /// 设置Trackout过账结果
        /// </summary>
        /// <param name="content"></param>
        public void SetlblTrackOutResult(string content, bool isAlarm)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lblTrackoutResult.Content = content;
                lblTrackoutResult.Foreground = new SolidColorBrush(isAlarm ? Colors.Red : Colors.Blue);
            }));
        }

        /// <summary>
        /// 设置当前RunCard
        /// </summary>
        /// <param name="content"></param>
        public void SetlblLot()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lblLot.Content = PostParams.P_I.StrLot;
            }));
        }

        /// <summary>
        /// 设置Cim初始化结果
        /// </summary>
        /// <param name="content"></param>
        public void SetCimStatus(string content, bool isAlarm)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lblCIMStatus.Content = content;
                lblCIMStatus.Foreground = new SolidColorBrush(isAlarm ? Colors.Red : Colors.Blue);
            }));
        }

        /// <summary>
        /// 设置二维码初始化结果
        /// </summary>
        /// <param name="content"></param>
        public void SetCodeStatus(string content, bool isAlarm)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                lblCodeStatus.Content = content;
                lblCodeStatus.Foreground = new SolidColorBrush(isAlarm ? Colors.Red : Colors.Blue);
            }));
        }
    }
}
