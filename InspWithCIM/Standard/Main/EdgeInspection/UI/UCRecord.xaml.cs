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
    public partial class UCRecord : UserControl
    {
        #region 定义
        public event IntAction DGRecordSelChanged_event;
        #endregion

        public UCRecord()
        {
            InitializeComponent();
        }
        public void Init(List<ResultInspection> list)
        {
            try
            {
                if (dgRecord.ItemsSource == null)
                {
                    dgRecord.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCRecord", ex);
            }
        }

        private void dgRecord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DGRecordSelChanged_event(dgRecord.SelectedIndex);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCRecord", ex);
            }
        }

        public void DGRefresh_Invoke()
        {
            try
            {
                if (ParSetWork.P_I[8].BlSelect)
                {
                    return;
                }

                Action inst = new Action(DGRefresh);
                this.Dispatcher.Invoke(inst);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCRecord", ex);
            }
        }

        void DGRefresh()
        {
            try
            {
                dgRecord.ItemsSource = null;
                dgRecord.ItemsSource = new List<ResultInspection>(BaseDealComprehensiveResult.ResultInspCell_L);
                dgRecord.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCRecord", ex);
            }
        }


    }
}
