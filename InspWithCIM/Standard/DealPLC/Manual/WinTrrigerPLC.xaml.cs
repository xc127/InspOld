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
using ControlLib;
using Common;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;
using System.IO;
using DealFile;
using MahApps.Metro.Controls;
using BasicClass;
using DealPLC;
using DealConfigFile;
using DealLog;

namespace DealPLC
{
    /// <summary>
    /// TrrigerPLC.xaml 的交互逻辑
    /// </summary>
    public partial class WinTrrigerPLC : BaseMetroWindow
    {
        #region 窗体单实例
        private static WinTrrigerPLC g_WinTrrigerPLC = null;
        public static WinTrrigerPLC GetWinInst(out bool blNew)
        {
            blNew = false;
            if (g_WinTrrigerPLC == null)
            {
                blNew = true;
                g_WinTrrigerPLC = new WinTrrigerPLC();
            }
            return g_WinTrrigerPLC;
        }
        #endregion 窗体单实例

        #region 定义
        //string 
        public string[] g_strAnnotation = 
        { 
            "相机1拍照","相机2拍照","相机3拍照",
            "相机4拍照","相机5拍照","相机6拍照"
        };

        //List
        public List<TrrigerPLC> TrrigerPLC_L = new List<TrrigerPLC>();//触发寄存器的集合,用来模拟触发     

        bool blClearReserve = false;
        #endregion 定义

        #region 初始化
        public WinTrrigerPLC()
        {
            InitializeComponent();
            LocationRight();//初始化窗体位置
            ComValue.C_I.blTrrigerPLC = true;

            NameClass = "WinTrrigerPLC";
        }
     
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitRegList();
        }

        /// <summary>
        /// 初始化列表
        /// </summary>
        void InitRegList()
        {
            try
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//精简版本协议
                {
                    InitRegListFullNew();
                }
                else
                {
                    InitRegListFull();
                }
                
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void InitRegListFull()
        {
            try
            {
                int num = 34;                
                string[] strReg = ParSetPLC.P_I.RegCyc.Split('\n');
                TrrigerPLC_L.Clear();

                //第一个寄存器为PC－PLC,最后一个寄存器不用
                int numReg = num + ParCameraWork.NumCamera;//相机和监控寄存器个数
                for (int i = 0; i < numReg; i++)
                {
                    TrrigerPLC_L.Add(new TrrigerPLC()
                    {
                        No = i,
                        NameReg = strReg[i] + "\\n",
                        DblValue = 0
                    });
                }
                for (int i = 0; i < (strReg.Length - numReg - 1) / 2; i++)//读值保留寄存器
                {
                    TrrigerPLC_L.Add(new TrrigerPLC()
                    {
                        No = i + numReg,
                        NameReg = strReg[2 * i + numReg] + "\\n" + strReg[2 * i + numReg + 1] + "\\n",
                        DblValue = 0
                    });
                }

                for (int i = 0; i < num; i++)
                {
                    TrrigerPLC_L[i].Annotation = RegMonitor.R_I.Reg_L[i].Annotation;
                    TrrigerPLC_L[i].Explain = RegMonitor.R_I.Reg_L[i].Explain;
                }
                for (int i = num; i < num + ParCameraWork.NumCamera; i++)
                {
                    TrrigerPLC_L[i].Annotation = g_strAnnotation[i - num];
                }
                for (int i = num + ParCameraWork.NumCamera; i < RegMonitor.R_I.NumReg + ParCameraWork.NumCamera; i++)
                {
                    TrrigerPLC_L[i].Annotation = RegMonitor.R_I.Reg_L[i - ParCameraWork.NumCamera].Annotation;
                    TrrigerPLC_L[i].Explain = RegMonitor.R_I.Reg_L[i - ParCameraWork.NumCamera].Annotation;
                    TrrigerPLC_L[i].Co = RegMonitor.R_I.Reg_L[i - ParCameraWork.NumCamera].Co;
                }
                ShowPar_Invoke();//刷新显示
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void InitRegListFullNew()
        {
            try
            {              
                TrrigerPLC_L.Clear();

                //触发寄存器
                for (int i = 0; i < RegMonitor.R_I.NumReg; i++)
                {
                    TrrigerPLC_L.Add(new TrrigerPLC()
                    {
                        No = i,
                        NameReg = RegMonitor.R_I[i].NameReg,
                        DblValue = 0,
                        Annotation = RegMonitor.R_I[i].Annotation,
                        Co = RegMonitor.R_I[i].Co,
                    });
                }
                ////读值保留寄存器
                //for (int i = RegMonitor.R_I.NumTrigger; i < RegMonitor.R_I.NumReg; i++)
                //{
                //    TrrigerPLC_L.Add(new TrrigerPLC()
                //    {
                //        No = i,
                //        NameReg = RegMonitor.R_I[i].NameReg,
                //        DblValue = 0
                //    });
                //}             
                ShowPar_Invoke();//刷新显示
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        #region 清空读值保留
        private void chkClearReserve_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                blClearReserve = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        private void chkClearReserve_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                blClearReserve = false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 清空读值保留

        #region 触发
        /// <summary>
        /// 选择寄存器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgTrrigerReg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                e.Handled = true;
                base.IndexP = this.dgTrrigerReg.SelectedIndex;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }           
        }

        /// <summary>
        /// 模拟触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTrriger_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                TrrigerPLC inst = dgTrrigerReg.Items[base.IndexP] as TrrigerPLC;
                if (inst.DblValue != 0)
                {
                    ParLogicPLC.P_I.intTrrigerValue[base.IndexP] = (int)inst.DblValue;
                }
                else
                {
                    ParLogicPLC.P_I.intTrrigerValue[base.IndexP] = 1;//数组按照单个寄存器
                    TrrigerPLC_L[base.IndexP].DblValue = 1;//两个寄存器一组
                }

                int num = 34 + ParCameraWork.NumCamera;
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//精简版本协议
                {
                    num = RegMonitor.R_I.NumTrigger;
                }
                for (int i = 0; i < num; i++)
                {
                    TrrigerPLC trrigerPLC = dgTrrigerReg.Items[i] as TrrigerPLC;
                    ParLogicPLC.P_I.intTrrigerValue[i] = (int)trrigerPLC.DblValue;//单个寄存器
                }

                ////赋值读值保留
                for (int i = num; i < dgTrrigerReg.Items.Count; i++)
                {
                    TrrigerPLC trrigerPLC = dgTrrigerReg.Items[i] as TrrigerPLC;
                    TrrigerPLC_L[i].DblValue = trrigerPLC.DblValue;//两个寄存器一组
                    double value = TrrigerPLC_L[i].DblValue / TrrigerPLC_L[i].Co;
                    ParLogicPLC.P_I.intTrrigerValue[(i - num) * 2 + num] = LogicPLC.L_I.ConvertWriteData(value)[0];//数组按照单个寄存器
                    ParLogicPLC.P_I.intTrrigerValue[(i - num) * 2 + 1 + num] = LogicPLC.L_I.ConvertWriteData(value)[1];//数组按照单个寄存器
                }

                Task task1 = new Task(LogicPLC.L_I.ManualTrriger);
                task1.Start();
                Task task2 = new Task(Clear);
                task2.Start();

                ShowPar_Invoke();//显示参数
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                //按钮日志
                FunLogButton.P_I.AddInfo("btnTrriger触发","模拟PLC触发");
            }
        }
        /// <summary>
        /// 对触发数组和显示清空
        /// </summary>
        public void Clear()
        {
            try
            {              
                Thread.Sleep(600);
                LogicPLC.L_I.ClearOld();//清除Old

                int num = 34;
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//精简版本协议
                {
                    for (int i = 0; i < RegMonitor.R_I.NumTrigger; i++)
                    {
                        ParLogicPLC.P_I.intTrrigerValue[i] = 0;
                        TrrigerPLC_L[i].DblValue = 0;
                    }

                    if (blClearReserve)
                    {
                        for (int i = 0; i < ParLogicPLC.P_I.intTrrigerValue.Length; i++)
                        {
                            ParLogicPLC.P_I.intTrrigerValue[i] = 0;
                            TrrigerPLC_L[i].DblValue = 0;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < num + ParCameraWork.NumCamera; i++)
                    {
                        ParLogicPLC.P_I.intTrrigerValue[i] = 0;
                        TrrigerPLC_L[i].DblValue = 0;
                    }

                    if (blClearReserve)
                    {
                        for (int i = ParLogicPLC.P_I.intTrrigerValue.Length - 2 * (num + ParCameraWork.NumCamera); i < ParLogicPLC.P_I.intTrrigerValue.Length; i++)
                        {
                            ParLogicPLC.P_I.intTrrigerValue[i] = 0;
                            TrrigerPLC_L[i / 2].DblValue = 0;
                        }
                    }
                }                
                ShowPar_Invoke();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发

        #region 显示
        /// <summary>
        /// 显示参数
        /// </summary>
        public override void ShowPar()
        {
            RefreshDg();
        }

        void RefreshDg()
        {          
            dgTrrigerReg.ItemsSource = TrrigerPLC_L;
            dgTrrigerReg.Items.Refresh();            
            dgTrrigerReg.SelectedIndex = base.IndexP;            
        }
        #endregion 显示

        #region 关闭
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                ComValue.C_I.blTrrigerPLC = false;
                g_WinTrrigerPLC = null;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }            
        }
        #endregion 关闭       

        
    }
}
