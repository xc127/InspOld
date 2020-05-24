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

namespace Main
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UCParConfig : BaseControl
    {
        public UCParConfig()
        {
            InitializeComponent();
            if (MainSTDArrangeProd.M_I.DeltaVision == null)
            {
                MainSTDArrangeProd.M_I.GetRealDelta(new Point2D(1, 1));
            }
            this.DataContext = MainSTDArrangeProd.M_I;
        }


        private void BaseControl_Loaded(object sender, RoutedEventArgs e)
        {
            //显示中间生成参数
            ShowPar();
            BtnApply.IsEnabled = false;
        }

        /// <summary>
        /// 显示中间生成参数
        /// </summary>
        public override void ShowPar()
        {
            cbxPreciLight.SelectedItem = ParSTDArrange.P_I.TypePreci_E;
            cbxRobotCoor.SelectedItem = ParSTDArrange.P_I.TypeRobotCoor_E;
            cbxITOPlatCorner.SelectedItem = ParSTDArrange.P_I.ITOPlatSTDCorner_E;
            cbxTypePlatWork.SelectedItem = ParSTDArrange.P_I.TypePlatWork_E;
        }

        #region 控件响应
        private void cbxAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                BtnApply.IsEnabled = true;
            }));
        }
        /// <summary>
        /// 应用变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ParSTDArrange.P_I.TypePreci_E = (TypePreciLight_Enum)Enum.Parse(typeof(TypePreciLight_Enum), cbxPreciLight.SelectedItem.ToString(), true);
                ParSTDArrange.P_I.TypeRobotCoor_E = (TypeRobotCoor_Enum)Enum.Parse(typeof(TypeRobotCoor_Enum), cbxRobotCoor.SelectedItem.ToString(), true);
                ParSTDArrange.P_I.ITOPlatSTDCorner_E = (ITOPlatCorner_Enum)Enum.Parse(typeof(ITOPlatCorner_Enum), cbxITOPlatCorner.SelectedItem.ToString(), true);
                ParSTDArrange.P_I.TypePlatWork_E = (TypePlatWork_Enum)Enum.Parse(typeof(TypePlatWork_Enum), cbxTypePlatWork.SelectedItem.ToString(), true);
                MainSTDArrangeProd.M_I.GetRealDelta();

                UpdatePar();

                BtnApply.IsEnabled = false;
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 显示配置参数
        /// </summary>
        public void UpdatePar()
        {
            try
            {
                MainSTDArrangeProd.M_I.CallChanging();
            }
            catch (Exception ex)
            {

            }

        }

        void ShowConfig()
        {

        }
        #endregion

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            double deltaX = 0;
            double deltaY = 0;

            if (!Double.TryParse(txtDeltaX.Text, out deltaX) ||
            !Double.TryParse(txtDeltaY.Text, out deltaY))
            {
                MessageBox.Show("输入不能为空");
                return;
            }
            else
            {
                MainSTDArrangeProd.M_I.DeltaVision = new Point2D(deltaX, deltaY);

                MainSTDArrangeProd.M_I.GetRealDelta();
            }            
        }
    }
}
