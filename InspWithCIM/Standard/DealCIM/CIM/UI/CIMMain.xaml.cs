using System;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Media;

namespace DealCIM
{
    /// <summary>
    /// CIMMain.xaml 的交互逻辑
    /// </summary>
    public partial class CIMMain : UserControl
    {
        public CIMMain()
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
        public void SetlblRunCard()
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

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            DataContext = CIM.C_I;
        }
    }
}
