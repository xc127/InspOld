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
using System.Windows.Shapes;
using BasicClass;
using System.IO;
using System.Xml;
using DealFile;

namespace DealLog
{
    /// <summary>
    /// WinRecover.xaml 的交互逻辑
    /// </summary>
    public partial class WinRecover : BaseWindow
    {
        #region 窗体单实例
        private static WinRecover g_WinRecover = null;
        public static WinRecover GetWinInst(out bool blNew)
        {
            blNew = false;
            try
            {
                if (g_WinRecover == null)
                {
                    blNew = true;
                    g_WinRecover = new WinRecover();
                }
                return g_WinRecover;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecover", ex);
                return null;
            }
        }

        public static WinRecover GetWinInst()
        {
            try
            {
                return g_WinRecover;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecover", ex);
                return null;
            }
        }

        public string TypePar = "";//参数类型
        #endregion 窗体单实例

        #region 初始化
        public WinRecover()
        {
            InitializeComponent();
        }

        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ReadDir("相机综合设置1");//初始化加载
            if (ParRecover.P_I.CountDir > 0)
            {
                ReadFile(ParRecover.P_I.g_BaseParRecoverDir_L[ParRecover.P_I.CountDir - 1]);
            }
        }
        #endregion 初始化

        #region 显示目录
        private void tvBackup_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                if (tvBackup.IsMouseOver)
                {
                    XmlElement xmlElement = (XmlElement)this.tvBackup.SelectedItem;
                    XmlElement xmlParent = (XmlElement)xmlElement.ParentNode;
                    string strParent = xmlParent.Attributes["Name"].Value;
                    string strChild = xmlElement.Attributes["Name"].Value;
                    ReadDir(strChild);

                    if (ParRecover.P_I.CountDir > 0)
                    {
                        dgDir.SelectedIndex = ParRecover.P_I.CountDir - 1;
                        ReadFile(ParRecover.P_I.g_BaseParRecoverDir_L[ParRecover.P_I.CountDir - 1]);
                    }
                    else
                    {
                        ReadFile(null);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecover", ex);
            }
        }

        void ReadDir(string type)
        {
            try
            {
                ParRecover.P_I.g_BaseParRecoverDir_L.Clear();
                DirectoryInfo rootInfo = new DirectoryInfo(FunBackup.F_I.RootBackup);

                foreach (DirectoryInfo dirRoot in rootInfo.GetDirectories())
                {
                    string path = dirRoot.FullName + "\\";

                    switch (type)
                    {
                        case "产品参数":
                            path += "Product\\";
                            break;

                        case "调整值参数":
                            path += "Adjust\\";
                            break;

                        case "基准值参数":
                            path += "Std\\";
                            break;

                        case "相机参数":
                            path += "Camera\\";
                            break;

                        case "PLC参数":
                            path += "PLC\\";
                            break;

                        case "Robot参数":
                            path += "Robot\\";
                            break;

                        case "系统设置参数":
                            path += "SetPar\\";
                            break;

                        default:
                            path += type + "\\";
                            break;
                    }
                    if (!Directory.Exists(path))
                    {
                        continue;
                    }
                    DirectoryInfo pathInfo = new DirectoryInfo(path);

                    foreach (DirectoryInfo item in pathInfo.GetDirectories())
                    {
                        BaseParRecoverDir inst = new BaseParRecoverDir();
                        inst.Date = dirRoot.Name;
                        inst.Time = item.Name;
                        inst.Path = item.FullName;
                        inst.Reason = ReadExplain(item.FullName)[2] + ":" + ReadExplain(item.FullName)[0];
                        inst.PathSource = ReadExplain(item.FullName)[1];
                        ParRecover.P_I.g_BaseParRecoverDir_L.Add(inst);
                    }
                }               

                for (int i = 0; i < ParRecover.P_I.g_BaseParRecoverDir_L.Count; i++)
                {
                    ParRecover.P_I.g_BaseParRecoverDir_L[i].No = i + 1;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecover", ex);
            }
            finally
            {
                ShowDir();
            }
        }

        string[] ReadExplain(string path)
        {
            try
            {
                string pathSource = "";
                string reason = "";
                string model = "";
                if (File.Exists(path + "\\备份说明.ini"))
                {
                    reason = IniFile.I_I.ReadIniStr("Explain", "Reason", path + "\\备份说明.ini");
                    pathSource = IniFile.I_I.ReadIniStr("Explain", "Source", path + "\\备份说明.ini");
                    model = IniFile.I_I.ReadIniStr("Explain", "Model", path + "\\备份说明.ini");
                }
                return new string[3] { reason, pathSource, model };
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecover", ex);
                return null;
            }
        }
        #endregion 显示目录

        private void dgDir_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                e.Handled = true;
                if(dgDir.IsMouseOver)
                {
                    ReadFile(ParRecover.P_I.g_BaseParRecoverDir_L[dgDir.SelectedIndex]);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecover", ex);
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        void ReadFile(BaseParRecoverDir baseParRecoverDir)
        {
            try
            {                
                ParRecover.P_I.g_BaseParRecoverFile_L.Clear();
                if (baseParRecoverDir == null)
                {
                    return;
                }

                DirectoryInfo theFolder = new DirectoryInfo(baseParRecoverDir.Path);
                foreach (FileInfo item1 in theFolder.GetFiles())
                {
                    BaseParRecoverFile inst = new BaseParRecoverFile();
                    inst.PathDir = baseParRecoverDir.Path;
                    inst.Name = item1.Name;
                    inst.Type = item1.Extension;
                    inst.Path = item1.FullName;
                    ParRecover.P_I.g_BaseParRecoverFile_L.Add(inst);
                }

                for (int i = 0; i < ParRecover.P_I.CountFile; i++)
                {
                    ParRecover.P_I.g_BaseParRecoverFile_L[i].No = i + 1;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecover", ex);
            }
            finally
            {
                ShowFile();
            }
        }
        #region 文件操作
        /// <summary>
        /// 恢复文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRecover_Click(object sender, RoutedEventArgs e)
        {
            string pathOld = "";
            string pathSource = "";
            try
            {
                base.IndexP=dgDir.SelectedIndex;
                pathOld = ParRecover.P_I.g_BaseParRecoverDir_L[base.IndexP].Path;
                pathSource = ParRecover.P_I.g_BaseParRecoverDir_L[base.IndexP].PathSourceShow;

                //先删除旧文件
                try
                {
                    DirectoryInfo firrSource = new DirectoryInfo(pathSource);
                    foreach (FileInfo item in firrSource.GetFiles())
                    {
                        if (File.Exists(item.FullName))
                        {
                            File.Delete(item.FullName);
                        }                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("删除目标文件夹的文件失败!");
                    throw;
                }
               
                //恢复文件               
                DirectoryInfo theFolder = new DirectoryInfo(pathOld);              
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    if (!Directory.Exists(pathSource))
                    {
                        Directory.CreateDirectory(pathSource);
                    }
                    File.Copy(item.FullName, pathSource + fileOld);
                
                }

                //按钮日志
                FunLogButton.P_I.AddInfo("btnRecover恢复参数成功", string.Format("将参数{0}恢复到路径{1}", pathOld, pathSource));
                MessageBox.Show("恢复成功,请重启软件!");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecover", ex);
                //按钮日志
                FunLogButton.P_I.AddInfo("btnRecover恢复参数失败", string.Format("将参数{0}恢复到路径{1}", pathOld, pathSource));
                MessageBox.Show("恢复失败!");
            }
        }

        /// <summary>
        /// 查看文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int index = dgDir.SelectedIndex;
                if (ParRecover.P_I.CountDir > 0)
                {
                    if (dgDir.SelectedIndex < 0)
                    {
                        index = 0;
                    }
                    else if (dgDir.SelectedIndex > ParRecover.P_I.CountDir-1)
                    {
                        index = ParRecover.P_I.CountDir - 1;
                    }
                }
                string path = ParRecover.P_I.g_BaseParRecoverDir_L[index].Path;
                System.Diagnostics.Process.Start("explorer.exe", path);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecover", ex);
            }
        }
        #endregion 文件操作

        #region 显示
        void ShowDir()
        {
            try
            {
                dgDir.ItemsSource = ParRecover.P_I.g_BaseParRecoverDir_L;
                dgDir.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecover", ex);
            }
        }

        void ShowFile()
        {
            try
            {
                dgFile.ItemsSource = ParRecover.P_I.g_BaseParRecoverFile_L;
                dgFile.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecover", ex);
            }
        }
        #endregion 显示

        #region 关闭
        private void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                g_WinRecover = null;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinRecover", ex);
            }
        }
        #endregion 关闭       

    }
}
