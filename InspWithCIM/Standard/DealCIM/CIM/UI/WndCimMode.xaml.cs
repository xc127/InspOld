using System;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace DealCIM
{
    /// <summary>
    /// WndCimMode.xaml 的交互逻辑
    /// </summary>
    public partial class WndCimMode : Window
    {
        public static WndCimMode instance = null;
        public static Action SetCimStatus_event;

        public static WndCimMode GetInstance()
        {
            if (instance == null)
                instance = new WndCimMode();
            return instance;
        }

        private WndCimMode()
        {
            InitializeComponent();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            PostParams.P_I.WriteCimModeConfig();
            this.Hide();
            SetCimStatus_event?.Invoke();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = PostParams.P_I;
        }

        private void TbtnGetCode_Checked(object sender, RoutedEventArgs e)
        {
            tbtnGetCode.Content = "读码开启";
        }

        private void TbtnGetCode_Unchecked(object sender, RoutedEventArgs e)
        {
            tbtnGetCode.Content = "读码关闭";
        }

        private void TbtnPost_Checked(object sender, RoutedEventArgs e)
        {
            ((ToggleButton)sender).Content = "CIM过账开启";
        }

        private void TbtnPost_Unchecked(object sender, RoutedEventArgs e)
        {
            ((ToggleButton)sender).Content = "CIM过账关闭";
        }

        private void TbtnVerifyChipid_Checked(object sender, RoutedEventArgs e)
        {
            ((ToggleButton)sender).Content = "ChipID认证开启";
        }

        private void TbtnVerifyChipid_Unchecked(object sender, RoutedEventArgs e)
        {
            ((ToggleButton)sender).Content = "ChipID认证关闭";
        }

        private void TbtnPassCodeNG_Checked(object sender, RoutedEventArgs e)
        {
            ((ToggleButton)sender).Content = "读码NG不抛料";
        }

        private void TbtnPassCodeNG_Unchecked(object sender, RoutedEventArgs e)
        {
            ((ToggleButton)sender).Content = "读码NG抛料";
        }
    }
}
