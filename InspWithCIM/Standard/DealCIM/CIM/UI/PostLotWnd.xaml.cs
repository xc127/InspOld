using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DealCIM
{
    /// <summary>
    /// PostLotWnd.xaml 的交互逻辑
    /// </summary>
    public partial class PostLotWnd : Window
    {
        #region 定义
        public static Action UpLoadLot;
        static PostLotWnd instance = null;
        #endregion

        public static PostLotWnd GetInstance()
        {
            if (instance == null)
                instance = new PostLotWnd();
            return instance;
        }

        private PostLotWnd()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 手动刷lot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPostLot_Click(object sender, RoutedEventArgs e)
        {
            PostParams.P_I.StrModelNo = tbModelNo.Text;
            PostParams.P_I.StrTempLot = tbLot.Text.Replace(" ", "").ToUpper();
            //tbLot.Text = string.Empty;
            PostParams.P_I.WriteCimConfig();
            UpLoadLot?.Invoke();
            this.Hide();
            tbLot.Text = string.Empty;
            tbLot.SelectAll();
        }

        /// <summary>
        /// 该窗口不关闭，只hide
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = PostParams.P_I;
        }
        /// <summary>
        /// 回车自动上报lot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                BtnPostLot_Click(sender, e);
            }
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            tbLot.Focus();
            this.Hide();
        }
    }
}
