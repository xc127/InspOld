using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace Main
{
    /// <summary>
    /// UIShield.xaml 的交互逻辑
    /// </summary>
    public partial class WinGetPermission : Window
    {
        public WinGetPermission()
        {
            InitializeComponent();
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (PsdBx.Password == "ccd1234" || PsdBx.Password == "finevision")
                {
                    StackTrace stack = new StackTrace();

                    Assembly assembly = Assembly.GetExecutingAssembly();
                    string path = assembly.Location;
                    string path1 = assembly.CodeBase; ;
                    string path3 = assembly.EscapedCodeBase;
                    if (File.Exists("PathVsion.log"))
                    {
                        File.Delete("PathVsion.log");
                    }
                    Log.L_I.WriteLog("PathVsion.log", path,path1,path3);
                    WinMsgBox.ShowMsgBox("已输出！");
                    Close();
                }
                else
                {
                    LblState.Content = "密码错误";
                }
            }
        }
    }
}
