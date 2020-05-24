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
using BasicClass;
using DealFile;
using System.Threading;

namespace DealRobot
{
    /// <summary>
    /// UCRobotIO.xaml 的交互逻辑
    /// </summary>
    public partial class UCRobotIO : BaseControl
    {
        #region 定义
        #region Path
        public string PathIO
        {
            get
            {
                string path = ParPathRoot.PathRoot + "Store\\Robot\\IORobot.ini";
                return path;
            }
        }
        #endregion Path

        int NumIn = 0;
        int NumOut = 0;
        List<IORobot> Input_L = new List<IORobot>();
        List<IORobot> Output_L = new List<IORobot>();
        #endregion 定义

        #region 初始化
        public UCRobotIO()
        {
            InitializeComponent();
        }

        public void Init()
        {
            try
            {
                ReadParIO();
                ShowPar_Invoke();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 初始化

        #region 输入
        public void SetInputValue(int[] value)
        {
            try
            {
                for (int i = 0; i < value.Length; i++)
                {
                    int data = value[i];
                    string io = Convert.ToString(data, 2).PadLeft(8, '0');

                    char[] cIO_Arr = io.ToCharArray();

                    for (int j = 0; j < cIO_Arr.Length; j++)
                    {
                        Input_L[i * 8 + j].Value = Convert.ToInt32(cIO_Arr[cIO_Arr.Length - 1 - j].ToString());
                    }
                }
                RefreshIn();
            }
            catch (Exception ex)
            {

            }

        }

        private void dudNumInput_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void btnReadIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LogicRobot.L_I.WriteRobotCMD("3000");
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 输入

        #region 输出
        public void SetOutputValue(int[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                int data = value[i];
                string io = Convert.ToString(data, 2).PadLeft(8, '0');

                char[] cIO_Arr = io.ToCharArray();

                for (int j = 0; j < cIO_Arr.Length; j++)
                {
                    Output_L[i * 8 + j].Value = Convert.ToInt32(cIO_Arr[cIO_Arr.Length - 1 - j].ToString());
                }
            }
            RefreshOut();
        }

        private void dudNumOutput_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }

        private void dgOut_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int par1 = dgOut.SelectedIndex;
                IORobot ioSelected = Output_L[par1];
                string cmd = ioSelected.Value == 0 ? "2000" : "2001";
                LogicRobot.L_I.WriteRobotCMD(par1.ToString(), cmd);
            }
            catch (Exception ex)
            {

            }
        }


        private void btnWriteComboOut_Click(object sender, RoutedEventArgs e)
        {
            string cmd = "2000";
            string par1 = "99";
            string par2 = dudComboValue.Value.ToString();
            LogicRobot.L_I.WriteRobotCMD(par1,par2,cmd);
        }
        #endregion 输出

        #region 读取保存文件
        void ReadParIO()
        {
            try
            {
                Input_L.Clear();
                NumIn = IniFile.I_I.ReadIniInt("In", "Num", 24, PathIO);
                for (int i = 0; i < NumIn; i++)
                {
                    bool blUnion = IniFile.I_I.ReadIniBl("In", "BlUnion" + i.ToString(), PathIO);
                    string annotation = IniFile.I_I.ReadIniStr("In", "Annotation" + i.ToString(), PathIO);

                    Input_L.Add(new IORobot()
                    {
                        No = i,
                        BlUnion = blUnion,
                        Annotation = annotation,
                    });
                }

                Output_L.Clear();
                NumOut = IniFile.I_I.ReadIniInt("Out", "Num", 16, PathIO);
                for (int i = 0; i < NumOut; i++)
                {
                    bool blUnion = IniFile.I_I.ReadIniBl("Out", "BlUnion" + i.ToString(), PathIO);
                    string annotation = IniFile.I_I.ReadIniStr("Out", "Annotation" + i.ToString(), PathIO);

                    Output_L.Add(new IORobot()
                    {
                        No = i,
                        BlUnion = blUnion,
                        Annotation = annotation,
                    });
                }
            }
            catch (Exception ex)
            {

            }
        }

        void WriteParIO()
        {
            try
            {
                IniFile.I_I.WriteIni("In", "Num", Input_L.Count.ToString(), PathIO);
                for (int i = 0; i < NumIn; i++)
                {
                    IniFile.I_I.WriteIni("In", "BlUnion" + i.ToString(), Input_L[0].BlUnion.ToString(), PathIO);
                    IniFile.I_I.WriteIni("In", "Annotation" + i.ToString(), Input_L[0].Annotation.ToString(), PathIO);
                }

                IniFile.I_I.WriteIni("Out", "Num", Output_L.Count.ToString(), PathIO);
                for (int i = 0; i < NumOut; i++)
                {
                    IniFile.I_I.WriteIni("Out", "BlUnion" + i.ToString(), Output_L[0].BlUnion.ToString(), PathIO);
                    IniFile.I_I.WriteIni("Out", "Annotation" + i.ToString(), Output_L[0].Annotation.ToString(), PathIO);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 读取保存文件

        #region 保存
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NumIn = (int)dudNumInput.Value;

                for (int i = 0; i < NumIn; i++)
                {
                    IORobot io = (IORobot)dgIn.Items[i];
                    Input_L[i].BlUnion = io.BlUnion;
                    Input_L[i].Annotation = io.Annotation;
                }

                NumOut = (int)dudNumOutput.Value;
                for (int i = 0; i < NumOut; i++)
                {
                    IORobot io = (IORobot)dgOut.Items[i];
                    Output_L[i].BlUnion = io.BlUnion;
                    Output_L[i].Annotation = io.Annotation;
                }
                WriteParIO();

                //刷新显示
                ShowPar_Invoke();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 保存

        #region 显示
        public override void ShowPar()
        {
            try
            {
                RefreshIn();
                RefreshOut();
            }
            catch (Exception ex)
            {

            }
        }
        void RefreshIn()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                dudNumInput.Value = NumIn;
                dgIn.ItemsSource = Input_L;
                dgIn.Items.Refresh();
            }));
        }
        void RefreshOut()
        {
            this.Dispatcher.Invoke(new Action(() =>
                {
                    dudNumOutput.Value = NumOut;
                    dgOut.ItemsSource = Output_L;
                    dgOut.Items.Refresh();
                }));

        }
        #endregion 显示

    }

    public class IORobot : BaseClass
    {
        public int Value { get; set; }
        public bool BlUnion { get; set; }
    }
}
