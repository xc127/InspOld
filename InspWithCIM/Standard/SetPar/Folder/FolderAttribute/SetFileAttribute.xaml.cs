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
using System.IO;
using DealFile;
using System.Threading;
using System.Threading.Tasks;
using BasicClass;
using DealLog;

namespace SetPar
{
    /// <summary>
    /// SetFolderAttribute.xaml 的交互逻辑
    /// </summary>
    public partial class SetFolderAttribute : BaseControl
    {
        #region 初始化
        public SetFolderAttribute()
        {
            InitializeComponent();

            NameClass = "SetFolderAttribute";
        }

        public void Init()
        {
            try
            {
                ParFolderAttribute.V_I.ReadFolderAttributeIni();//文件属性    
                //数据显示
                ShowPar_Invoke();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        //保存修改
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //备份数据
                FunBackup.F_I.BackupSetPar();

                for (int i = 0; i < ParFolderAttribute.V_I.FolderAttribute_L.Count; i++)
                {
                    var fileAttribute = dgAttribute.Items[i] as FolderAttribute;
                    ParFolderAttribute.V_I.FolderAttribute_L[i] = fileAttribute;

                    DirectoryInfo info = new DirectoryInfo(ParFolderAttribute.V_I.FolderAttribute_L[i].Path);
                    info.Attributes = FileAttributes.Normal;
                    //修改文件属性
                    //隐藏
                    if (fileAttribute.BlHidden)
                    {
                        info.Attributes = FileAttributes.Hidden;
                    }
                  
                }
                //保存
                if (ParFolderAttribute.V_I.WriteFolderAttributeIni())
                {
                    this.btnSave.RefreshDefaultColor("保存成功", true);
                }
                else
                {
                    this.btnSave.RefreshDefaultColor("保存失败", false);
                }               
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                this.btnSave.RefreshDefaultColor("保存失败", false);
            }
            finally
            {
                //按钮日志
                FunLogButton.P_I.AddInfo("btnSave设置&保存", "设置文件属性");
            }
        }

        #region 显示
        public override void ShowPar()
        {
            try
            {
                RefreshDatagrid();

                #region 权限设置
                base.SetBtnEnable(this.gdLayout, Authority.Authority_e);
                #endregion 权限设置
            }
            catch (Exception)
            {
                
            }          
        }
        /// <summary>
        /// 刷新文件列表
        /// </summary>
        void RefreshDatagrid()
        {
            try
            {
                dgAttribute.ItemsSource = ParFolderAttribute.V_I.FolderAttribute_L;
                dgAttribute.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示
    }
}
