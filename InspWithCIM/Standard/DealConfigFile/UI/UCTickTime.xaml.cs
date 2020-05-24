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
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace DealConfigFile
{
    /// <summary>
    /// UCTackTime.xaml 的交互逻辑
    /// </summary>
    public partial class UCTickTime : BaseControl
    {
        #region 初始化
        public UCTickTime()
        {
            InitializeComponent();
        }
        #endregion 初始化

        public void ShowTackTime()
        {
            try
            {
                ShowLabel_Invoke(lblTackTime, ComValue.C_I.TickTime.ToString("f2"));
                ComValue.C_I.TickTime = 0;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UCTickTime", ex);
            }

        }

    }
}
