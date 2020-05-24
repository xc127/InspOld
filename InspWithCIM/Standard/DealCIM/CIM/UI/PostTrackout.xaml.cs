using BasicClass;
using Common;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace DealCIM
{
    /// <summary>
    /// PostTrackout.xaml 的交互逻辑
    /// 这主要是一个测试页面，后面可能作为手动过账使用
    /// </summary>
    public partial class PostTrackout : Window
    {
        #region 定义
        int testcnt = 0;
        static object locker = new object();

        static PostTrackout instance = null;

        public static Action<object> UpLoadChipID;
        public static Action UpLoadLot;
        public static Action UpLoadTrackOut;
        public static StrAction AddAccount;

        QRCodeBase Code = null;
        #endregion

        public static PostTrackout GetInstance()
        {
            if (instance == null)
                instance = new PostTrackout();
            return instance;
        }

        private PostTrackout()
        {
            InitializeComponent();
        }

        private void BtnPostChipID_Click(object sender, RoutedEventArgs e)
        {
            UpLoadChipID?.Invoke(tbChipID.Text);
        }

        private void BtnPostLot_Click(object sender, RoutedEventArgs e)
        {
            PostParams.P_I.StrTempLot = tbLot.Text;
            //tbLot.Text = string.Empty;
            PostParams.P_I.WriteCimConfig();
            UpLoadLot?.Invoke();
            this.Hide();
            tbLot.SelectAll();
        }

        private void BtnPostTrackOut_Click(object sender, RoutedEventArgs e)
        {
            UpLoadTrackOut?.Invoke();
        }       

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Code = CodeFactory.Instance.GetCodeType(PostParams.P_I.ETypeCode);
        }
        
        private void BtnGetChipID_Click(object sender, RoutedEventArgs e)
        {
            if (Code == null)
            {
                MessageBox.Show("读码未打开");
                return;
            }
                
            Code?.Write();
            Code?.StartMonitor(PostParams.P_I.iCodeDelay);
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BtnPostLot_Click(sender, e);
            }
        }

        private void BtnAddChipID_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("是否将输入的ChipID：" + tbTrackOut.Text + "\r进行加账操作", "提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                AddAccount?.Invoke(tbTrackOut.Text);
            else
                AddAccount?.Invoke(string.Empty);
        }
    }
}
 