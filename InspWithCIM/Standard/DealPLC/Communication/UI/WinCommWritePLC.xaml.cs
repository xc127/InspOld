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
using BasicClass;
using DealLog;
using System.Collections;
using BasicComprehensive;
using DealResult;
using DealAlgorithm;

namespace DealPLC
{
    /// <summary>
    /// WinCommPLC.xaml 的交互逻辑
    /// </summary>
    public partial class WinCommWritePLC : BaseWinPLC
    {
        #region 窗体单实例
        private static WinCommWritePLC g_WinCommPLC = null;
        public static WinCommWritePLC GetWinInst(out bool blNew)
        {
            blNew = false;
            try
            {
                if (g_WinCommPLC == null)
                {
                    blNew = true;
                    g_WinCommPLC = new WinCommWritePLC();
                }
                return g_WinCommPLC;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCommWritePLC", ex);
                return null;
            }
        }

        public static WinCommWritePLC GetWinInst()
        {
            try
            {
                return g_WinCommPLC;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCommWritePLC", ex);
                return null;
            }
        }
        #endregion 窗体单实例

        #region 定义
        ParCommWritePLC g_ParCommWritePLC = null;
        ParCommWritePLC g_ParCommWritePLC_Old = null;

        CellReference g_CellReference = null;
        ResultReference g_ResultReference = null;

        List<RegPLC> RegWrite_L = null;

        //事件
        public event FdBlStr2Action SavePar_event;//保存参数
        #endregion 定义

        #region 初始化
        public WinCommWritePLC()
        {
            InitializeComponent();

            NameClass = "WinCommWritePLC";

            //初始化控件位置
            LocationRight();

            Login_Event();
        }

        //注册事件
        void Login_Event()
        {
            try
            {
                uCCellDataReference.CellDataReferenceSelect_event += new CellDataReference_del(uCGetResultFromCell_GetResultFromCell_event);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        public void Init(ParCommWritePLC par, List<CellReference> cellExe_L, List<CellHObjectReference> cellHObject_L, List<CellReference> cellData_L, Hashtable htResult)
        {
            try
            {
                NameClass = "WinCommWritePLC" + par.NameCell;

                //参数赋值        
                g_ParCommWritePLC = par;
                g_ParCommWritePLC_Old = (ParCommWritePLC)par.Clone();

                //权限设置
                g_AuthorityCtr_L = par.g_AuthorityCtr_L;

                g_HtResult = htResult;//结果

                g_CellData_L = cellData_L;//数据单元格名称
                g_CellExecute_L = cellExe_L;//可执行单元格名称              

                uCCellDataReference.Init(cellData_L, htResult);
                //显示参数
                ShowPar_Invoke();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        #region 引用数据结果
        //获取选择的结果
        void uCGetResultFromCell_GetResultFromCell_event(CellReference cellReference, ResultReference resultReference)
        {
            try
            {
                g_CellReference = cellReference;
                g_ResultReference = resultReference;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //引用结果
        private void btnGetResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                base.IndexP = this.dgDataAndReg.SelectedIndex;

                string nameCell = g_CellReference.NameCell;
                string nameResult = g_ResultReference.NameResult;
                if (nameResult.Contains("_L"))//如果是数组
                {
                    WinSetIndex win = new WinSetIndex();
                    bool result = (bool)win.ShowDialog();

                    if (result)
                    {

                    }
                }

                GetReusltValueFromResultcs fun = new GetReusltValueFromResultcs();
                double value = fun.GetResultValue(nameCell, nameResult, g_HtResult);

                g_ParCommWritePLC[base.IndexP].NameCell = nameCell;
                g_ParCommWritePLC[base.IndexP].NameResult = nameResult;
                g_ParCommWritePLC[base.IndexP].Result = value;
                g_ParCommWritePLC[base.IndexP].BlSelect = true;

                //刷新显示
                RefreshDgDataAndReg();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 引用数据结果

        #region 刷新PLC寄存器
        private void tvSetPLC_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                string str = ((TreeViewItem)tvSetPLC.SelectedValue).Header.ToString();
                RefreshPLCReg(str);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void RefreshPLCReg(string type)
        {
            try
            {
                switch (type)
                {
                    case "相机寄存器":
                        RegWrite_L = RegCameraData.R_I.RegWrite_L;
                        break;

                    case "数据寄存器1":
                        RegWrite_L = RegData.R_I.RegWrite_L;
                        break;

                    case "数据寄存器2":
                        RegWrite_L = RegData2.R_I.RegWrite_L;
                        break;

                    case "数据寄存器3":
                        RegWrite_L = RegData3.R_I.RegWrite_L;
                        break;

                    case "数据寄存器4":
                        RegWrite_L = RegData4.R_I.RegWrite_L;
                        break;

                    case "数据寄存器5":
                        RegWrite_L = RegData5.R_I.RegWrite_L;
                        break;

                    case "数据寄存器6":
                        RegWrite_L = RegData6.R_I.RegWrite_L;
                        break;
                }
                dgPLCReg.ItemsSource = RegWrite_L;
                dgPLCReg.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示PLC寄存器

        #region 获取PLC寄存器
        /// <summary>
        /// 可以批量设置选择的寄存器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                base.IndexP = this.dgPLCReg.SelectedIndex;
                base.IntPosP = this.dgDataAndReg.SelectedIndex;

                if (dgPLCReg.SelectedItems.Count == 0)
                {
                    g_ParCommWritePLC[base.IntPosP].Reg = RegWrite_L[base.IndexP].NameReg;
                    g_ParCommWritePLC[base.IntPosP].DblMax = RegWrite_L[base.IndexP].DblMax;
                    g_ParCommWritePLC[base.IntPosP].DblMin = RegWrite_L[base.IndexP].DblMin;
                    g_ParCommWritePLC[base.IntPosP].Co = RegWrite_L[base.IndexP].Co;
                    g_ParCommWritePLC[base.IntPosP].Annotation = RegWrite_L[base.IndexP].Explain;
                }
                else
                {
                    for (int i = 0; i < dgPLCReg.SelectedItems.Count; i++)
                    {
                        RegPLC inst = (RegPLC)dgPLCReg.SelectedItems[i];

                        g_ParCommWritePLC[base.IntPosP + i].Reg = inst.NameReg;
                        g_ParCommWritePLC[base.IntPosP + i].DblMax = inst.DblMax;
                        g_ParCommWritePLC[base.IntPosP + i].DblMin = inst.DblMin;
                        g_ParCommWritePLC[base.IntPosP + i].Co = inst.Co;
                        g_ParCommWritePLC[base.IntPosP + i].Annotation = inst.Explain;
                    }
                }

                //刷新显示
                RefreshDgDataAndReg();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 获取PLC寄存器

        #region 删除配置
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                base.IndexP = this.dgDataAndReg.SelectedIndex;

                g_ParCommWritePLC[base.IndexP] = new ResultforWritePLC(base.IndexP);
                //刷新显示
                RefreshDgDataAndReg();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 删除配置

        #region 保存
        private void btnSaveOnly_Click(object sender, RoutedEventArgs e)
        {
            string info = "保存成功";
            try
            {
                if (g_ParCommWritePLC != null)
                {
                    //触发保存此单元格参数到本地
                    if (SavePar_event(g_ParCommWritePLC.NameCell, g_ParCommWritePLC.TypeParent + ":" + g_ParCommWritePLC.TypeChild))
                    {
                        this.btnSaveOnly.RefreshDefaultColor("保存成功", true);
                    }
                    else
                    {
                        btnSaveOnly.RefreshDefaultColor("保存失败", false);
                        info = "保存失败";
                    }
                }
                else
                {
                    this.btnSaveOnly.RefreshDefaultColor("保存失败", true);
                    info = "保存失败";
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                this.btnSaveOnly.RefreshDefaultColor("保存失败", false);
                info = "保存失败";
            }
            finally
            {
                try
                {
                    //按钮日志
                    FunLogButton.P_I.AddInfo("btnSave保存",
                    "相机综合设置" + g_ParCommWritePLC.NoCamera.ToString() + g_ParCommWritePLC.NameCell + ":设置写入PLC," + info);
                }
                catch (Exception ex)
                {

                }
            }
        }
        /// <summary>
        /// 保存参数到本地并且退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string info = "保存成功";
            try
            {
                if (g_ParCommWritePLC != null)
                {
                    //触发保存此单元格参数到本地
                    if (SavePar_event(g_ParCommWritePLC.NameCell, g_ParCommWritePLC.TypeParent + ":" + g_ParCommWritePLC.TypeParent))
                    {
                        this.btnSave.RefreshDefaultColor("保存成功", true);
                        Close700_Task();//延迟退出
                    }
                    else
                    {
                        btnSave.RefreshDefaultColor("保存失败", false);
                        info = "保存失败";
                    }
                }
                else
                {
                    this.btnSave.RefreshDefaultColor("保存失败", true);
                    info = "保存失败";
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                this.btnSave.RefreshDefaultColor("保存失败", false);
                info = "保存失败";
            }
            finally
            {
                try
                {
                    //按钮日志
                    FunLogButton.P_I.AddInfo("btnSave保存&退出",
                    "相机综合设置" + g_ParCommWritePLC.NoCamera.ToString() + g_ParCommWritePLC.NameCell + ":设置写入PLC," + info);
                }
                catch (Exception)
                {

                }
            }
        }
        #endregion 保存

        #region 显示
        public override void ShowPar()
        {
            try
            {
                RefreshPLCReg("相机寄存器");
                RefreshDgDataAndReg();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// shauxin
        /// </summary>
        void RefreshDgDataAndReg()
        {
            try
            {
                dgDataAndReg.ItemsSource = g_ParCommWritePLC.ResultforWritePLC_L;
                dgDataAndReg.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 显示

        #region 退出
        /// <summary>
        /// 退出窗体,恢复备份参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {               
                CloseWin();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        public void CloseWin()
        {
            try
            {
                TrriggerRecoverPar(g_ParCommWritePLC_Old);//恢复参数
                this.Close();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        private void BaseWinPLC_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                g_WinCommPLC = null;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 退出                   
    }
}
