using BasicClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Main
{
    /// <summary>
    /// CompareSample.xaml 的交互逻辑
    /// </summary>
    public partial class UCCompareSample : UserControl
    {
        #region define
        //类名
        const string ClassName = "CompareSample";
        //文件数组，保存所有sample图片路径
        string[] FileNames;
        //巡边参数索引
        int SideIndex = 0;
        //对是否运行过本地图片进行标识，主要用于next和双击选择图片
        bool LocalLoaded = false;

        public UCCompareSample()
        {
            InitializeComponent();
        }
        #endregion

        #region control_event
        private void LblLocalDefect_Loaded(object sender, RoutedEventArgs e)
        {
            //binding
            dgResult.DataContext = SampleManager.instance.LocalInfo_list;
        }
        #endregion

        #region click_event
        /// <summary>
        /// 加载本地store中的sample
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoadSample_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SideIndex < 1)
                {
                    MessageBox.Show("请选择创建模板使用的参数！");
                    cbSide.Focus();
                    return;
                }

                string path = SampleManager.SampleImagePath + SideIndex + @"\";
                if (Directory.Exists(path))
                {
                    //加载sample的路径
                    FileNames = Directory.GetFiles(path)
                        .Where(x => x.EndsWith(".jpg")).OrderBy(x => new FileInfo(x).CreationTime).ToArray();
                }
                else
                {
                    MessageBox.Show("无法搜索到Sample图片");
                    return;
                }
                //加载sample的缺陷数据
                SampleManager.instance.LoadSampleDefect_XML(SideIndex);
                //刷新界面
                RefreshSampleInfo(SampleManager.FileIndex);
                MessageBox.Show("Sample加载完成");
                //置位，新的sample文件加载之后必须run一次本地才会刷新本地文件
                LocalLoaded = false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }
        /// <summary>
        /// 运行local参数，并与sample进行对比
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnComPareSample_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SampleManager.instance.LocalInfo_list.Count == 0)
                {
                    MessageBox.Show("请先加载Sample");
                    return;
                }

                if (SideIndex < 1)
                {
                    MessageBox.Show("请选择创建模板使用的参数！");
                    cbSide.Focus();
                    return;
                }
                //获取缺陷数据
                DealComprehensiveResult1.D_I.GetDefectData(FileNames, SideIndex, ucLocalDisplay,
                    ref SampleManager.instance.LocalResult_list, false);

                int i = 0;
                //将缺陷赋给界面列表，用于显示
                foreach (ResultInspection item in SampleManager.instance.LocalResult_list)
                {
                    foreach (FaultInfo fault in item.SingleFalutInfo_L)
                    {
                        if (fault.FaultType_E == FaultType_Enum.贝壳)
                            SampleManager.instance.LocalInfo_list[i].Shell = true;
                        if (fault.FaultType_E == FaultType_Enum.破角)
                            SampleManager.instance.LocalInfo_list[i].Cornor = true;
                        if (fault.FaultType_E == FaultType_Enum.凸边)
                            SampleManager.instance.LocalInfo_list[i].Convex = true;
                    }
                    ++i;
                }
                //刷新本地图片
                RefreshLocalInfo(SampleManager.FileIndex);
                MessageBox.Show("本地测试完成");
                //置位，此后local窗口会响应next
                LocalLoaded = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }
        /// <summary>
        /// 下一张，可以单窗口或者两窗口同时刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SampleManager.instance.SampleInfo_list.Count == 0)
                {
                    MessageBox.Show("请先导入Sample图片");
                    return;
                }
                if (++SampleManager.FileIndex == SampleManager.instance.SampleInfo_list.Count)
                    SampleManager.FileIndex = 0;
                //默认必须先加载sample，不然什么也干不了
                RefreshSampleInfo((dgResult.SelectedItem as VModel).Index);
                if (LocalLoaded)
                    RefreshLocalInfo((dgResult.SelectedItem as VModel).Index);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        private void DgResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //获取选择的图片索引，用于刷新界面
                SampleManager.FileIndex = dgResult.SelectedIndex;
                if (SampleManager.FileIndex < 0)
                {
                    SampleManager.FileIndex = 0;
                    MessageBox.Show("请选择正确的Sample图片");
                    return;
                }
                RefreshSampleInfo((dgResult.SelectedItem as VModel).Index);
                if (LocalLoaded)
                    RefreshLocalInfo((dgResult.SelectedItem as VModel).Index);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }
        /// <summary>
        /// 弹出图片列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnList_Click(object sender, RoutedEventArgs e)
        {
            //pop.IsOpen = true;
        }
        /// <summary>
        /// 对应图片较多而窗口装不下，需要scrollviewer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
            eventArg.RoutedEvent = UIElement.MouseWheelEvent;
            eventArg.Source = sender;
            scrollViewer.RaiseEvent(eventArg);
        }
        #endregion

        private void RefreshSampleInfo(int index)
        {
            try
            {

                SampleManager.ShowResult.Invoke(SampleManager.instance.SampleResult_list[index], ucSampleDisplay);
                //SampleManager.ShowResult.Invoke(
                //    SampleManager.instance.SampleResult_list[(dgResult.SelectedItem as VModel).Index], 
                //    ucSampleDisplay);
                SetLabel(lblSampleDefect, SampleManager.instance.SampleResult_list[index].SingleFalutInfo_L);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        private void RefreshLocalInfo(int index)
        {
            try
            {
                SampleManager.ShowResult?.Invoke(SampleManager.instance.LocalResult_list[index], ucLocalDisplay);
                SetLabel(lblLocalDefect, SampleManager.instance.LocalResult_list[index]?.SingleFalutInfo_L);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        private void SetLabel(Label label, List<FaultInfo> falutInfos)
        {
            try
            {
                label.Content = "缺陷：";
                foreach (FaultInfo item in falutInfos)
                {
                    label.Content +=
                        '\n' + item.FaultType_E.ToString() +
                        ":宽度-" + item.WidthFault +
                        ",深度-" + item.DepthFault +
                        '\n' + "X:" + item.PosFalut.DblValue1 +
                        ",Y:" + item.PosFalut.DblValue2;
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

    /// <summary>
    /// 比较类，用于文件排序
    /// </summary>
    public class SortFile : IComparer
    {
        public SortFile()
        {

        }
        int IComparer.Compare(object a, object b)
        {
            try
            {
                FileInfo fa = (FileInfo)a;
                FileInfo fb = (FileInfo)b;

                return DateTime.Compare(fa.CreationTime, fb.CreationTime);
            }
            catch { }
            return 0;
        }
    }
}
