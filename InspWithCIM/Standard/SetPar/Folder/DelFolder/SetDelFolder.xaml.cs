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
using Common;
using BasicClass;
using DealLog;

namespace SetPar
{
    /// <summary>
    /// SetDelFolder.xaml 的交互逻辑
    /// </summary>
    public partial class SetDelFolder : BaseControl
    {
        #region 初始化
        public SetDelFolder()
        {
            InitializeComponent();

            NameClass = "SetDelFolder";
        }

        public void Init()
        {
            try
            {
                //数据显示
                ShowPar_Invoke();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        #region 保存设置
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //备份数据
                FunBackup.F_I.BackupSetPar();

                for (int i = 0; i < ParDelFolder.P_I.BaseParDelFolder_L.Count; i++)
                {
                    BaseParDelFolder baseParDelFolder = (BaseParDelFolder)dgDelFolder.Items[i];
                    ParDelFolder.P_I.BaseParDelFolder_L[i].Num = baseParDelFolder.Num;
                }

                //删除时间
                ParDelFolder.P_I.BlDel1 = (bool)tsbDel1.IsChecked;
                ParDelFolder.P_I.BlDel2 = (bool)tsbDel2.IsChecked;
                ParDelFolder.P_I.Time1 = ((DateTime)tpDel1.Value).ToShortTimeString();
                ParDelFolder.P_I.Time2 = ((DateTime)tpDel2.Value).ToShortTimeString();

                //保存文件
                if (ParDelFolder.P_I.WriteDeleteIni())
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
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                //按钮日志
                FunLogButton.P_I.AddInfo("btnSave保存所有参数", "设置文件删除");
            }
        }
        #endregion 保存设置            
   
        #region 显示
        public override void ShowPar()
        {
            try
            {
                tsbDel1.IsChecked = ParDelFolder.P_I.BlDel1;
                tsbDel2.IsChecked = ParDelFolder.P_I.BlDel2;

                DateTime dt1 = new DateTime();
                DateTime.TryParse(ParDelFolder.P_I.Time1, out dt1);
                tpDel1.Value = dt1;

                DateTime dt2 = new DateTime();
                DateTime.TryParse(ParDelFolder.P_I.Time2, out dt2);
                tpDel2.Value = dt2;

                RefreshDgDel();

                #region 权限设置
                base.SetBtnEnable(this.gdLayout, Authority.Authority_e);
                #endregion 权限设置
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void RefreshDgDel()
        {
            try
            {
                dgDelFolder.ItemsSource = ParDelFolder.P_I.BaseParDelFolder_L;
                dgDelFolder.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示
    }
}
