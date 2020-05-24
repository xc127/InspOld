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
using DealFile;
using Common;
using BasicClass;

namespace DealConfigFile
{
    /// <summary>
    /// SetWork.xaml 的交互逻辑
    /// </summary>
    public partial class WinSetWorkType : BaseMetroWindow
    {
        #region 窗体单实例
        private static WinSetWorkType g_WinSetWorkType = null;
        public static WinSetWorkType GetWinInst()
        {
            if (g_WinSetWorkType == null)
            {
                g_WinSetWorkType = new WinSetWorkType();
            }
            return g_WinSetWorkType;
        }
        #endregion 窗体单实例

        #region 初始化
        public WinSetWorkType()
        {
            InitializeComponent();
        }

        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowPar_Invoke();
        }
        #endregion 初始化
      
        #region 保存
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < ParSetWork.C_NumWork; i++)
                {
                    WorkSelect inst = dgSetWork.Items[i] as WorkSelect;
                    ParSetWork.P_I.WorkSelect_L[i] = inst;                
                }

                ParSetWork.P_I.WriteIniPar();
                this.btnSave.RefreshDefaultColor("保存成功", true);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("SetWorkType", ex);
            }
        }
        #endregion 保存

        #region 关闭
        private void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            g_WinSetWorkType = null;
        }
        #endregion 关闭

        #region 显示
        public override void ShowPar()
        {
            try
            {
                dgSetWork.ItemsSource = ParSetWork.P_I.WorkSelect_L;
                dgSetWork.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("SetWorkType", ex);
            }            
        }
        #endregion 显示

    

    }    
}
