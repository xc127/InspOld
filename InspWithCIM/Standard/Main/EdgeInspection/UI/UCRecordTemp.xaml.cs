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
using DealResult;

namespace Main
{
    /// <summary>
    /// UCRecord.xaml 的交互逻辑
    /// </summary>
    public partial class UCRecordTemp : UserControl
    {
        #region 定义
        public event IntAction DGSelChanged_event;
        public static bool blRefresh = false;
        #endregion

        public UCRecordTemp()
        {
            InitializeComponent();
        }
        public void Init(List<ResultInspection> list)
        {
            try
            {
                if (dgRecordSingle.ItemsSource == null)
                {
                    dgRecordSingle.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCRecordTemp", ex);
            }
        }

        private void dgRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (ParSetWork.P_I[9].BlSelect)
                {
                    return;
                }
                DGSelChanged_event(dgRecordSingle.SelectedIndex);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCRecordTemp", ex);
            }
        }

        public void DGRefresh_Invoke()
        {
            try
            {
                if (ParSetWork.P_I[9].BlSelect)
                {
                    return;
                }

                if (!blRefresh)
                {
                    return;
                }
                blRefresh = false;

                Action inst = new Action(DGRefresh);
                this.Dispatcher.Invoke(inst);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCRecordTemp", ex);
            }
        }
        void DGRefresh()
        {
            try
            {
                dgRecordSingle.ItemsSource = null;
                dgRecordSingle.ItemsSource = new List<ResultInspection>(BaseDealComprehensiveResult.ResultInspSingeCell_L);
                dgRecordSingle.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCRecordTemp", ex);
            }
        }
    }
}
