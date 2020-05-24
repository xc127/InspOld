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
using DealConfigFile;

namespace Main
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class UCCamFunSet : BaseControl
    {
        public UCCamFunSet()
        {
            InitializeComponent();
        }

        private void BaseControl_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cbxCam1Set.IsEnabled = (ParCameraWork.NumCamera > 0);
                cbxCam2Set.IsEnabled = (ParCameraWork.NumCamera > 1);
                cbxCam3Set.IsEnabled = (ParCameraWork.NumCamera > 2);
                cbxCam4Set.IsEnabled = (ParCameraWork.NumCamera > 3);
                cbxCam5Set.IsEnabled = (ParCameraWork.NumCamera > 4);
                cbxCam6Set.IsEnabled = (ParCameraWork.NumCamera > 5);

                cbxCam1Set.SelectedItem = ParSTDArrange.P_I.FunCam1_E;
                cbxCam2Set.SelectedItem = ParSTDArrange.P_I.FunCam2_E;
                cbxCam3Set.SelectedItem = ParSTDArrange.P_I.FunCam3_E;
                cbxCam4Set.SelectedItem = ParSTDArrange.P_I.FunCam4_E;
                cbxCam5Set.SelectedItem = ParSTDArrange.P_I.FunCam5_E;
                cbxCam6Set.SelectedItem = ParSTDArrange.P_I.FunCam6_E;
            }
            catch (Exception ex)
            {

            }

        }

        #region 参数变更
        private void cbxCam1Set_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParSTDArrange.P_I.FunCam1_E = (FunCam_Enum)Enum.Parse(typeof(FunCam_Enum), cbxCam1Set.SelectedItem.ToString(), true);
        }

        private void cbxCam2Set_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParSTDArrange.P_I.FunCam2_E = (FunCam_Enum)Enum.Parse(typeof(FunCam_Enum), cbxCam2Set.SelectedItem.ToString(), true);
        }

        private void cbxCam3Set_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParSTDArrange.P_I.FunCam3_E = (FunCam_Enum)Enum.Parse(typeof(FunCam_Enum), cbxCam3Set.SelectedItem.ToString(), true);
        }

        private void cbxCam4Set_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParSTDArrange.P_I.FunCam4_E = (FunCam_Enum)Enum.Parse(typeof(FunCam_Enum), cbxCam4Set.SelectedItem.ToString(), true);
        }

        private void cbxCam5Set_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParSTDArrange.P_I.FunCam5_E = (FunCam_Enum)Enum.Parse(typeof(FunCam_Enum), cbxCam5Set.SelectedItem.ToString(), true);

        }

        private void cbxCam6Set_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ParSTDArrange.P_I.FunCam6_E = (FunCam_Enum)Enum.Parse(typeof(FunCam_Enum), cbxCam6Set.SelectedItem.ToString(), true);

        }
        #endregion

    }


    public enum FunCam_Enum
    {
        不使用,
        粗定位,
        粗定位_定制,
        精确定位,
        精确定位_定制,
        残边检测,
        卡塞检测相机,
    }
}
