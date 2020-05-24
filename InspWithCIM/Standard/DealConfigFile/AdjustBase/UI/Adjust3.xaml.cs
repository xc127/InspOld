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
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using BasicClass;

namespace DealConfigFile
{
    /// <summary>
    /// SetAdjustBase.xaml 的交互逻辑
    /// </summary>
    public partial class Adjust3 : BaseUCAdjust
    {
        #region 定义
        //string 

        //bool

        //double


        #endregion 定义

        #region 初始化
        public Adjust3()
        {
            InitializeComponent();
        }

        //显示参数
        public override void ShowPar(string path)
        {

            this.g_Path = path;
            if (BlChange)
            {
                InitDoubleUpDown(dudAdjust1, dudAdjust2, dudAdjust3, null); ;
                InitLable(lbName1, lbName2, lbName3, null);               
            }
            InitInfo(gpbAdjust, txtAnnotation);
        }
        #endregion 初始化
      
        #region 保存
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double dblValue1 = (double)dudAdjust1.Value;
                double dblValue2 = (double)dudAdjust2.Value;
                double dblValue3 = (double)dudAdjust3.Value;
                string strAnnotion = txtAnnotation.Text;

                if (WriteIni3(dblValue1, dblValue2, dblValue3, strAnnotion))
                {
                    this.btnSave.RefreshDefaultColor("保存成功", true);
                }
                else
                {
                    this.btnSave.RefreshDefaultColor("保存失败", false);
                }
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("Adjust4", ex);
                this.btnSave.RefreshDefaultColor("保存失败", false);
            }
        }
        #endregion 保存

       

    }
}
