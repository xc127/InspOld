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
using System.Xml;
using Common;
using MahApps.Metro.Controls;
using System.Threading;
using System.Threading.Tasks;
using BasicClass;

namespace DealRobot
{
    /// <summary>
    /// SetRobot.xaml 的交互逻辑
    /// </summary>
    public partial class WinSetRobot : MetroWindow
    {
        #region 定义
        BaseUCRobot g_UIBase = null;
        #endregion 定义

        #region 初始化
        public WinSetRobot()
        {
            InitializeComponent();
        }
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }
        public void Init()
        {
            ShowPar_Invoke();
        }
        #endregion 初始化

        #region 显示选择
        private void tvSetRobot_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                 //移除前一个UI
                if (this.gdLayout.Children.Count > 1)
                {
                    this.gdLayout.Children.RemoveAt(1);
                }
                XmlElement xmlElement = (XmlElement)this.tvSetRobot.SelectedItem;
                string name = xmlElement.Attributes["Name"].Value;

                //选择功能
                CreateFunShow(name);

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("SetRobot", ex);
            }
        }

        //选择功能
        void CreateFunShow(string name)
        {
            try
            {
                int intHeight = 450;
                int intWidth = 750;
                switch (name)
                {
                    case "机器人类型":
                        g_UIBase = new UCSetTypeRobot();
                        intHeight = 450;
                        intWidth = 600;
                        break;

                    case "机器人控制设定":
                        g_UIBase = new SetTypeWork();
                        intHeight = 220;
                        intWidth = 200;
                        break;

                    case "机器人数据范围":
                        g_UIBase = new UCSetDataLimit();
                        intHeight = 550;
                        intWidth = 750;
                        break;
                }
                //添加控件显示
                AddChildCtr(intHeight, intWidth);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UISetCImagePBase", ex);
            }
        }

        //添加控件
        void AddChildCtr(int intHeight, int intWidth)
        {
            try
            {
                //移除前一个UI
                if (this.gdLayout.Children.Count > 2)
                {
                    this.gdLayout.Children.RemoveAt(2);
                }
                if (g_UIBase == null)
                {
                    return;
                }

                //设定布局方式
                g_UIBase.HorizontalAlignment = HorizontalAlignment.Center;
                g_UIBase.VerticalAlignment = VerticalAlignment.Center;
                g_UIBase.Height = intHeight;
                g_UIBase.Width = intWidth;
                g_UIBase.Margin = new Thickness(0, 0, 0, 0);
                Grid.SetColumn(g_UIBase, 1);
                Grid.SetRowSpan(g_UIBase, 2);

                //初始化加载              
                g_UIBase.Init();

                //添加控件
                this.gdLayout.Children.Add(g_UIBase);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("UISetCImagePBase", ex);
            }
        }
        #endregion 显示选择

        #region 显示
        void ShowPar_Invoke()
        {
            Action inst = new Action(ShowPar);
            this.Dispatcher.Invoke(inst);
        }

        void ShowPar()
        {
            CreateFunShow("机器人类型");
        }
        #endregion 显示

        
    }
}
