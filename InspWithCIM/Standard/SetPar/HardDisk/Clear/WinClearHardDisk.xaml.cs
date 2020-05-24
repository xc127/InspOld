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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using BasicClass;
using DealLog;
using System.IO;

namespace SetPar
{
    /// <summary>
    /// WinClearHardDisk.xaml 的交互逻辑
    /// </summary>
    public partial class WinClearHardDisk : BaseMetroWindow
    {
        #region 窗体单实例
        private static WinClearHardDisk g_WinClearHardDisk = null;
        public static WinClearHardDisk GetWinInst()
        {
            if (g_WinClearHardDisk == null)
            {
                g_WinClearHardDisk = new WinClearHardDisk();
            }
            return g_WinClearHardDisk;
        }
        #endregion 窗体单实例

        #region 定义
        List<ParClearHardDisk> ParClearHardDisk_L = new List<ParClearHardDisk>();
        #endregion 定义

        #region 初始化
        public WinClearHardDisk()
        {
            InitializeComponent();

            NameClass = "WinClearHardDisk";
        }

        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ReadFolder();
                ShowPar();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 初始化

        #region 读取文件目录
        void ReadFolder()
        {
            try
            {
                ParClearHardDisk_L.Clear();
                string root = ParPathRoot.PathRoot + "软件运行记录\\图片记录\\";
                DirectoryInfo modelInfo = new DirectoryInfo(root);
                foreach (DirectoryInfo item in modelInfo.GetDirectories())
                {
                    string model = item.Name;//型号
                    string pathModel = item.FullName;
                    DirectoryInfo dateInfo = new DirectoryInfo(pathModel);
                    foreach (DirectoryInfo itemDate in dateInfo.GetDirectories())
                    {
                        string date = itemDate.Name;
                        string pathDate = itemDate.FullName;
                        double storage = GetDirectoryLength(pathDate);
                        ParClearHardDisk_L.Add(new ParClearHardDisk()
                        {
                            Model = model,
                            Date = date,
                            Path = pathDate,
                            Storage = Math.Round(storage / (1024 * 1024), 2),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        long GetDirectoryLength(string dirPath)
        {
            try
            {
                //判断给定的路径是否存在,如果不存在则退出
                if (!Directory.Exists(dirPath))
                    return 0;
                long len = 0;

                //定义一个DirectoryInfo对象
                DirectoryInfo di = new DirectoryInfo(dirPath);

                //通过GetFiles方法,获取di目录中的所有文件的大小
                foreach (FileInfo fi in di.GetFiles())
                {
                    len += fi.Length;
                }

                //获取di中所有的文件夹,并存到一个新的对象数组中,以进行递归
                DirectoryInfo[] dis = di.GetDirectories();
                if (dis.Length > 0)
                {
                    for (int i = 0; i < dis.Length; i++)
                    {
                        len += GetDirectoryLength(dis[i].FullName);
                    }
                }
                return len;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return 0;
            }
        }
        #endregion 读取文件目录

        #region 删除文件夹
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dgDir.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择需要删除的文件夹!");
                    return;
                }
                if (MessageBox.Show("是否删除文件夹?", "确认信息", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                {
                    return;
                }
                int index = this.dgDir.SelectedIndex;
                string path = ParClearHardDisk_L[index].Path;
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                //再次读取和显示
                ReadFolder();
                ShowPar();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 删除文件夹

        #region 显示
        void ShowPar()
        {
            try
            {
                dgDir.ItemsSource = ParClearHardDisk_L;
                dgDir.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

        #region 关闭
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                g_WinClearHardDisk = null;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 关闭

    }
    public class ParClearHardDisk:BaseClass
    {
        public string Model { get; set; }
        public string Date { get; set; }
        public double Storage { get; set; }
        public string Path { get; set; }
    }
}
