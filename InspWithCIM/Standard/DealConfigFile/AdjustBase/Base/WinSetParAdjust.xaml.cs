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

namespace DealConfigFile
{
    /// <summary>
    /// UCSetParAdjust.xaml 的交互逻辑
    /// </summary>
    public partial class WinSetParAdjust : BaseMetroWindow
    {
        #region 定义
        public string g_Name = "";

        //定义事件
        public event Action ChangeInfo_event;
        #endregion 定义

        #region 初始化
        public WinSetParAdjust()
        {
            InitializeComponent();
        }
       
        public void Init(string name)
        {
            try
            {
                g_Name = name;
                ShowPar_Invoke();
            }
            catch (Exception ex)
            {
               
            }
        }
        #endregion 初始化

        #region 保存
        /// <summary>
        /// 将修改后的参数保存到本地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int i = 0; i < ParSetAdjust.P_I.g_ParSetAdjust_L.Count; i++)
                {
                    BaseParSetAdjust baseParSetAdjust = (BaseParSetAdjust)dgSetAdjust.Items[i];
                    ParSetAdjust.P_I.g_ParSetAdjust_L[i] = baseParSetAdjust;
                }
                ParSetAdjust.P_I.g_Title = txtTitle.Text.Trim();
                ParSetAdjust.P_I.WriteIni();

                btnSave.RefreshDefaultColor("保存成功",true);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 保存

        #region 显示
        /// <summary>
        /// 显示参数
        /// </summary>
        public override void ShowPar()
        {           
            RefreshDatagrid();
            txtTitle.Text = ParSetAdjust.P_I.Title;
        }

        public void RefreshDatagrid()
        {
            try
            {
                dgSetAdjust.ItemsSource = ParSetAdjust.P_I[g_Name];
                dgSetAdjust.Items.Refresh();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 显示

        #region 退出
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ChangeInfo_event();//触发重置调整控件
        }
        #endregion 退出

       
    }

    public enum TypeIncrement_e
    {
        Num0,
        Num1,
        Num2,
        Num3,
        Num4,
        Num5,
    }
}
