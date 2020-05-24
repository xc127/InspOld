using BasicClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Main
{
    /// <summary>
    /// SettingSample.xaml 的交互逻辑
    /// </summary>
    public partial class UCSetSample : UserControl
    {
        #region define
        //类名
        const string ClassName = "SetSample";
        //openfile用
        System.Windows.Forms.OpenFileDialog Of = new System.Windows.Forms.OpenFileDialog();
        //巡边参数索引
        int SideIndex = 0;

        public UCSetSample()
        {
            InitializeComponent();
        }
        #endregion

        #region control_event
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //binding
            dgResult.DataContext = SampleManager.instance.SampleInfo_list;
        }
        #endregion

        #region Click_event
        private void BtnLoadImg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Of = new System.Windows.Forms.OpenFileDialog
                {
                    Multiselect = true,
                    //实际使用后，应该只剩最清晰的格式，其余的直接忽略，避免出现因为存图格式导致run出不同结果又会很麻烦
                    Filter = @"Images (*.jpg,*.bmp)|*.jpg;*.bmp"
                };
                if (Of.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //重置
                    SampleManager.FileIndex = 0;
                    //保存读取到的文件
                    SampleManager.instance.SetFileName(Of.FileNames);
                    RefreshInfo(SampleManager.FileIndex);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        private void BtnSaveParams_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Of.FileNames.Length == 0)
                {
                    MessageBox.Show("请先导入Sample图片");
                    return;
                }
                File.Delete(@"D:\Store\Sample\sample_defect.xml");
                string dir = ParPathRoot.PathRoot + @"Store\Sample\Image\" + SideIndex;
                if (Directory.Exists(dir))
                    Directory.Delete(dir, true);
                DealComprehensiveResult1.D_I.GetDefectData(Of.FileNames, SideIndex, ucSetDisplay,
                    ref SampleManager.instance.SampleResult_list);
                //保存巡边参数
                if (!DealComprehensiveResult1.D_I.SaveSampleParams(SampleManager.SampleParPath, SideIndex))
                {
                    MessageBox.Show("保存参数失败!");
                }
                //保存缺陷，目前所有
                if (!SampleManager.instance.SaveSampleDefect(SideIndex))
                {
                    MessageBox.Show("保存缺陷失败!");
                }
                else
                {
                    MessageBox.Show("Sample建立完成！");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        private void BtnNextImg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Of?.FileNames.Length == 0)
                {
                    MessageBox.Show("请先导入Sample图片");
                    return;
                }
                if (++SampleManager.FileIndex == Of.FileNames.Length)
                    SampleManager.FileIndex = 0;
                RefreshInfo((dgResult.SelectedItem as VModel).Index);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        private void BtnTestImg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SampleManager.MeanGray = 0;
                SampleManager.Sharpness = 0;
                if (Of?.FileNames.Length == 0)
                {
                    MessageBox.Show("请先导入Sample图片");
                    return;
                }

                if (SideIndex < 1)
                {
                    MessageBox.Show("请选择创建模板使用的参数！");
                    cbSide.Focus();
                    return;
                }

                DealComprehensiveResult1.D_I.GetDefectData(Of.FileNames, SideIndex, ucSetDisplay,
                    ref SampleManager.instance.SampleResult_list, false);
                int i = 0;
                foreach (ResultInspection item in SampleManager.instance.SampleResult_list)
                {
                    SampleManager.MeanGray += item.MeanGray.D;
                    SampleManager.Sharpness += item.Sharpness.D;
                    foreach (FaultInfo fault in item.SingleFalutInfo_L)
                    {
                        if (fault.FaultType_E == FaultType_Enum.贝壳)
                            SampleManager.instance.SampleInfo_list[i].Shell = true;
                        if (fault.FaultType_E == FaultType_Enum.破角)
                            SampleManager.instance.SampleInfo_list[i].Cornor = true;
                        if (fault.FaultType_E == FaultType_Enum.凸边)
                            SampleManager.instance.SampleInfo_list[i].Convex = true;
                    }
                    ++i;
                }
                SampleManager.MeanGray /= i;
                SampleManager.Sharpness /= i;
                RefreshInfo(SampleManager.FileIndex);
                MessageBox.Show("Sample图片测试完成\n平均灰度：" + SampleManager.MeanGray + "\n平均清晰度：" + SampleManager.Sharpness);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Sample", ex);
            }
        }

        private void DgResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SampleManager.FileIndex = dgResult.SelectedIndex;
                if (SampleManager.FileIndex < 0)
                {
                    SampleManager.FileIndex = 0;
                    MessageBox.Show("请选择正确的Sample图片");
                    return;
                }
                RefreshInfo((dgResult.SelectedItem as VModel).Index);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;
            scrollViewer.RaiseEvent(eventArg);
        }
        #endregion

        private void RefreshInfo(int index)
        {
            try
            {
                SampleManager.ShowResult?.Invoke(SampleManager.instance.SampleResult_list[index], ucSetDisplay);
                lblDefect.Content = "缺陷：";
                foreach (FaultInfo item in SampleManager.instance.SampleResult_list[index]?.SingleFalutInfo_L)
                {
                    lblDefect.Content +=
                        '\n' + item.FaultType_E.ToString() +
                        ":宽度-" + item.WidthFault.ToString() +
                        ",深度-" + item.DepthFault.ToString();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        private void CbSide_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SideIndex = cbSide.SelectedIndex + 1;
        }
    }
}