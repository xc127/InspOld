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

namespace DealPLC
{
    /// <summary>
    /// WinReadValue.xaml 的交互逻辑
    /// </summary>
    public partial class WinReadValue : Window
    {
        #region  窗体单实例
        static WinReadValue g_WinReadValue = null;
        public static WinReadValue GetWinInst
        {
            get
            {
                if (g_WinReadValue == null)
                {
                    g_WinReadValue = new WinReadValue();
                }
                return g_WinReadValue;
            }
        }
        #endregion  窗体单实例

        #region 初始化
        public WinReadValue()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgRegReadData.ItemsSource = RegReadData_L;
        }
        #endregion 初始化

    
     

        public List<RegReadData> RegReadData_L
        {
            get
            {
                List<RegReadData> regReadData_L = new List<RegReadData>();
                string[] strRegArray = ParSetPLC.P_I.RegCyc.Split('\n');
                try
                {
                    for (int i = 0; i < strRegArray.Length; i++)
                    {
                        regReadData_L.Add(new RegReadData(strRegArray[i], ParLogicPLC.P_I.intOldValue[i], ParLogicPLC.P_I.intNewValue[i]));
                    }
                }
                catch
                {
                }
                return regReadData_L;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            dgRegReadData.ItemsSource = RegReadData_L;
            dgRegReadData.Items.Refresh();
        }

        private void btnWriteTest_Click(object sender, RoutedEventArgs e)
        {
            int num = (int)dudNum.Value;
            double[] data = new double[num];
            for (int i = 0; i < num; i++)
            {
                data[i] = (double)dudData.Value;
            }
            if (cbxPort.SelectedIndex == 0)
            {
                LogicPLC.L_I.WriteRegData2(10, num, data);  ///临时使用从银胶工作路径的第十个点开始写入，地址为ZR22,
            }
            else if (cbxPort.SelectedIndex == 1)
            {
                LogicPLC.L_I.WriteRegData2_Write1(10, num, data);
            }
            else if (cbxPort.SelectedIndex == 2)
            {
                LogicPLC.L_I.WriteRegData2_Write2(10, num, data);
            }
            else if (cbxPort.SelectedIndex == 3)
            {
                LogicPLC.L_I.WriteRegData2_Write3(10, num, data);
            }
            else if (cbxPort.SelectedIndex == 4)
            {
                LogicPLC.L_I.WriteRegData2_Write4(10, num, data);
            }
            else if (cbxPort.SelectedIndex == 5)
            {
                LogicPLC.L_I.WriteRegData2_Write5(10, num, data);
            }
            else if (cbxPort.SelectedIndex == 6)
            {
                LogicPLC.L_I.WriteRegData2_Write6(10, num, data);
            }
        }

        #region 关闭
        private void Window_Closed(object sender, EventArgs e)
        {
            g_WinReadValue = null;
        }
        #endregion 关闭
    }

    public class RegReadData
    {
        public RegReadData()
        { 
        }
        public RegReadData(string reg,int value1,int value2)
        {
            strReg = reg;
            oldValue = value1;
            newValue = value2;
        }
        string strReg = "D";

        int olderValue = 0;
        int oldValue = 0;
        int newValue = 0;
        public string StrReg
        {
            get
            {
                return strReg;
            }
            set
            {
                strReg = value;
            }
        }
        public int OlderValue
        {
            get
            {
                return olderValue;
            }
            set
            {
                olderValue = value;
            }
        }
        public int OldValue
        {
            get
            {
                return oldValue;
            }
            set
            {
                oldValue = value;
            }
        }
        public int NewValue
        {
            get
            {
                return newValue;
            }
            set
            {
                newValue = value;
            }
        }
    }
}
