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
using MahApps.Metro.Controls.Dialogs;
using System.Threading;
using System.Threading.Tasks;
using BasicClass;

namespace DealPLC
{
    /// <summary>
    /// SetPLC.xaml 的交互逻辑
    /// </summary>
    public partial class WinSetPLC : BaseWindow
    {
        #region 窗体单实例
        private static WinSetPLC g_WinSetPLC = null;
        public static WinSetPLC GetWinInst(out bool blNew)
        {
            blNew = false;
            try
            {
                if (g_WinSetPLC == null)
                {
                    blNew = true;
                    g_WinSetPLC = new WinSetPLC();
                }
                return g_WinSetPLC;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinSetPLC", ex);
                return null;
            }
        }

        public static WinSetPLC GetWinInst()
        {
            try
            {
                return g_WinSetPLC;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinSetPLC", ex);
                return null;
            }
        }
        #endregion 窗体单实例

        #region 定义
        BaseUCPLC g_BaseUCPLC = null;
        #endregion 定义

        #region 初始化
        public WinSetPLC()
        {
            InitializeComponent();

            NameClass = "WinSetPLC";
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        //初始化加载
        public void Init()
        {
            ShowPar_Invoke();
        }
        #endregion 初始化

        #region 选择显示
        private void tvSetPLC_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                //移除前一个UI
                if (this.gdLayout.Children.Count > 1)
                {
                    this.gdLayout.Children.RemoveAt(1);
                }
                XmlElement xmlElement = (XmlElement)this.tvSetPLC.SelectedItem;
                string name = xmlElement.Attributes["Name"].Value;

                //选择功能
                CreateFunShow(name);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //选择功能
        void CreateFunShow(string name)
        {
            try
            {
                int intHeight = 450;
                int intWidth = 850;
                switch (name)
                {
                    case "PLC基本参数":
                        g_BaseUCPLC = new UCSetTypePLC();
                        intHeight = 480;
                        intWidth = 600;
                        break;

                    case "PLC寄存器":
                        g_BaseUCPLC = new UCSetRegConfigPar();
                        break;

                    case "配置参数寄存器":
                        g_BaseUCPLC = new UCSetRegConfigPar();
                        break;

                    case "监控寄存器":
                        g_BaseUCPLC = new UCSetRegMonitor();
                        ((UCSetRegMonitor)g_BaseUCPLC).SaveReg_event += new Action(WinSetPLC_SaveReg_event);
                        break;

                    case "相机寄存器":
                        g_BaseUCPLC = new UCSetRegCameraData();
                        ((UCSetRegCameraData)g_BaseUCPLC).SaveReg_event += new Action(WinSetPLC_SaveReg_event);
                        break;

                    case "数据寄存器1":
                        g_BaseUCPLC = new UCSetRegData();
                        break;

                    case "数据寄存器2":
                        g_BaseUCPLC = new UCSetRegData2();
                        break;

                    case "数据寄存器3":
                        g_BaseUCPLC = new UCSetRegData3();
                        break;

                    case "数据寄存器4":
                        g_BaseUCPLC = new UCSetRegData4();
                        break;

                    case "数据寄存器5":
                        g_BaseUCPLC = new UCSetRegData5();
                        break;

                    case "数据寄存器6":
                        g_BaseUCPLC = new UCSetRegData6();
                        break;

                    case "循环读取寄存器":
                        g_BaseUCPLC = new UCSetRegCycle();
                        intHeight = 325;
                        intWidth = 485;
                        break;
                }
                //添加控件显示
                AddChildCtr(intHeight, intWidth);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
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
                if (g_BaseUCPLC == null)
                {
                    return;
                }
                //设定布局方式
                g_BaseUCPLC.HorizontalAlignment = HorizontalAlignment.Center;
                g_BaseUCPLC.VerticalAlignment = VerticalAlignment.Center;
                g_BaseUCPLC.Height = intHeight;
                g_BaseUCPLC.Width = intWidth;
                g_BaseUCPLC.Margin = new Thickness(0, 0, 0, 0);
                Grid.SetColumn(g_BaseUCPLC, 1);
                Grid.SetRowSpan(g_BaseUCPLC, 2);

                //初始化加载              
                g_BaseUCPLC.Init();

                //添加控件
                this.gdLayout.Children.Add(g_BaseUCPLC);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 选择显示

        #region 监控寄存器和相机寄存器保存的时候，触发修改循环读取寄存器
        void WinSetPLC_SaveReg_event()
        {
            try
            {
                UCSetRegCycle inst = new UCSetRegCycle();
                if (inst.CreateCycReg())
                {
                    MessageBox.Show("保存此寄存器时，会修改和保存循环读取寄存器，已保存成功！");
                }
                else
                {
                    MessageBox.Show("保存此寄存器时，会修改和保存循环读取寄存器，但保存异常，请检查相机寄存器和监控寄存器设置！");
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 监控寄存器和相机寄存器保存的时候，触发修改循环读取寄存器

        #region 显示
        public override void ShowPar()
        {
            CreateFunShow("PLC基本参数");
        }
        #endregion 显示

        #region 关闭
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (MessageBox.Show("需要重启软件使更改的设置生效！", "确认信息", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                {
                    e.Cancel = true;
                    return;
                }

                g_WinSetPLC = null;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 关闭
    }
}
