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
using Common;
using MahApps.Metro.Controls;
using System.Threading;
using System.Threading.Tasks;
using BasicClass;

namespace DealRobot
{
    /// <summary>
    /// TypeWork.xaml 的交互逻辑
    /// </summary>
    public partial class SetTypeWork : BaseUCRobot
    {
        #region 初始化
        public SetTypeWork()
        {
            InitializeComponent();
        }
        #endregion 初始化

        #region 保存
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("SetTypeWork", ex);
            }
        }
        #endregion 保存
    }
}
