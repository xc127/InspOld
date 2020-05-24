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
using DealFile;
using BasicClass;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace SetPar
{
    /// <summary>
    /// Others.xaml 的交互逻辑
    /// </summary>
    public partial class WinSetFolder : MetroWindow
    {
        #region 定义


        #endregion 定义

        #region 初始化
        public WinSetFolder()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }
        void Init()
        {
            setDel.Init();
            setFolderAttribute.Init();
        }
        #endregion 初始化
    }
}
