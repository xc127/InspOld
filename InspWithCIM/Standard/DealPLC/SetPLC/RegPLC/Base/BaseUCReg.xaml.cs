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
using System.Text.RegularExpressions;
using DealFile;
using Common;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using BasicClass;
//using Excel = Microsoft.Office.Interop.Excel;
using DealLog;

namespace DealPLC
{
    /// <summary>
    /// PLCRegBase.xaml 的交互逻辑
    /// </summary>
    public partial class BaseUCReg : BaseUCPLC
    {
        #region 定义
        //bool
        
        //string 

        //首先定义正则表达式，验证输入的初始化寄存器
        protected string g_strRegex = "[a-zA-Z]\\d{1,6}";//规则为一个字符，加1-6位的数字

        //enum
        protected BaseRegPLC g_RegPLCBase = new BaseRegPLC();

        public virtual event Action SaveReg_event;//触发保存事件
        #endregion 定义

        #region 初始化
        public BaseUCReg()
        {
            InitializeComponent();
        }

        public void Init(BaseRegPLC R_I,string strName)
        {
            try
            {
                //初始化赋值
                g_RegPLCBase = R_I;           
                this.gpbReg.Header = strName;

                ShowPar_Invoke();
                
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);  
            }
        }
        #endregion 初始化

        /// <summary>
        /// 初始创建寄存器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                //权限返回
                if (!EngineerReturn())
                {
                    return;
                }
                string strReg = txtRegStart.Text.Trim().ToUpper();//将字母全部转换为大写
                bool blMatch = Regex.IsMatch(strReg, g_strRegex);
                if (!blMatch)
                {
                    g_RegPLCBase.regStart = "";
                    MessageBox.Show("寄存器起始地址不正确,应为字母+数字，如D100！");
                    return;
                }
                
                g_RegPLCBase.regStart = strReg;  //起始寄存器          
                CreateRegTwo();//按照2个D寄存器进行生成                
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCReg", ex);
                MessageBox.Show("配置出错！");
            }
            finally
            {              
                RefreshDG();  //刷新数据显示
            }
        }
       
        /// <summary>
        /// 按照两个D寄存器进行生成
        /// </summary>
        public virtual void CreateRegTwo()
        {
            try
            {
                int intSum = g_RegPLCBase.regStart.Length;
                int intNo = int.Parse(g_RegPLCBase.regStart.Substring(1, intSum - 1));
                string strLetter = g_RegPLCBase.regStart.Substring(0, 1);
                //是否清除注释
                if ((bool)chkClearCoAnno.IsChecked)
                {
                    g_RegPLCBase.Reg_L.Clear();
                    for (int i = 0; i < g_RegPLCBase.NumReg; i++)
                    {
                        string PLCSendPC = "";
                        string PCSendPLC = "";
                        string Annotation = "";
                        double Co = 1;
                        if (g_RegPLCBase.PLCSendPC != null)
                        {
                            PLCSendPC = g_RegPLCBase.PLCSendPC[i];
                        }
                        if (g_RegPLCBase.PCSendPLC != null)
                        {
                            PCSendPLC = g_RegPLCBase.PCSendPLC[i];
                        }
                        if (g_RegPLCBase.Annotation != null)
                        {
                            Annotation = g_RegPLCBase.Annotation[i];
                        }
                        if (g_RegPLCBase.Co != null)
                        {
                            Co = g_RegPLCBase.Co[i];
                        }
                        //选择读取寄存器为单个或是两个                       
                        RegPLC inst = new RegPLC()
                        {
                            No = i,
                            NameReg = strLetter + (intNo + 2 * i).ToString() + "\\n" + strLetter + (intNo + 2 * i + 1).ToString() + "\\n",
                            NameCustomReg = "",
                            PLCSend = PLCSendPC,
                            PCSend = PCSendPLC,
                            Annotation = Annotation,
                            Explain = Annotation,
                            Co = Co,
                            DblMax = int.MaxValue,
                            DblMin = int.MinValue
                        };
                        g_RegPLCBase.Reg_L.Add(inst);
                    }
                }
                else
                {
                    if (dgReg.Items.Count < g_RegPLCBase.NumReg)
                    {
                        for (int i = 0; i < dgReg.Items.Count; i++)
                        {
                            RegPLC rItem = dgReg.Items[i] as RegPLC;
                            g_RegPLCBase.Reg_L[i].NameReg = strLetter + (intNo + 2 * i).ToString() + "\\n" + strLetter + (intNo + 2 * i + 1).ToString() + "\\n";
                            g_RegPLCBase.Reg_L[i].NameCustomReg = rItem.NameCustomReg;
                            g_RegPLCBase.Reg_L[i].Co = rItem.Co;
                            g_RegPLCBase.Reg_L[i].DblMin = rItem.DblMin;
                            g_RegPLCBase.Reg_L[i].DblMax = rItem.DblMax;
                            g_RegPLCBase.Reg_L[i].Annotation = rItem.Annotation;
                            g_RegPLCBase.Reg_L[i].Explain = rItem.Annotation;
                        }

                        for (int i = dgReg.Items.Count; i < g_RegPLCBase.NumReg; i++)
                        {
                            RegPLC inst = new RegPLC()
                            {
                                No = i,
                                NameReg = strLetter + (intNo + 2 * i).ToString() + "\\n" + strLetter + (intNo + 2 * i + 1).ToString() + "\\n",
                                NameCustomReg = g_RegPLCBase.Reg_L[i].NameCustomReg,
                                PLCSend = g_RegPLCBase.PLCSendPC[i],
                                PCSend = g_RegPLCBase.PCSendPLC[i],
                                Annotation = "",
                                Explain = "",
                                Co = g_RegPLCBase.Co[i],
                                DblMax = int.MaxValue,
                                DblMin = int.MinValue
                            };
                            g_RegPLCBase.Reg_L.Add(inst);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < g_RegPLCBase.NumReg; i++)
                        {
                            RegPLC rItem = dgReg.Items[i] as RegPLC;
                            g_RegPLCBase.Reg_L[i].NameReg = strLetter + (intNo + 2 * i).ToString() + "\\n" + strLetter + (intNo + 2 * i + 1).ToString() + "\\n";
                            g_RegPLCBase.Reg_L[i].NameCustomReg = rItem.NameCustomReg;
                            g_RegPLCBase.Reg_L[i].Co = rItem.Co;
                            g_RegPLCBase.Reg_L[i].DblMin = rItem.DblMin;
                            g_RegPLCBase.Reg_L[i].DblMax = rItem.DblMax;
                            g_RegPLCBase.Reg_L[i].Annotation = rItem.Annotation;
                            g_RegPLCBase.Reg_L[i].Explain = rItem.Annotation;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCReg", ex);
            }
        }

        #region 参数调整
        /// <summary>
        /// 设置寄存器的个数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void dudNumReg_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {                
                if (dudNumReg.IsMouseOver)
                {
                    e.Handled = true;
                    g_RegPLCBase.NumRegSet = (int)dudNumReg.Value;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCReg", ex);
            }
        }
        #endregion 参数调整

        #region 保存
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!EngineerReturn())
                {
                    return;
                }
                //备份文件到本地
                FunBackup.F_I.BackupPLC();

                for (int i = 0; i < g_RegPLCBase.NumReg; i++)
                {
                    RegPLC rItem = dgReg.Items[i] as RegPLC;            
                    g_RegPLCBase.Reg_L[i].DblMin = rItem.DblMin;
                    g_RegPLCBase.Reg_L[i].DblMax = rItem.DblMax;
                    g_RegPLCBase.Reg_L[i].Co = rItem.Co;
                    g_RegPLCBase.Reg_L[i].Annotation = rItem.Annotation;

                    if (g_RegPLCBase.Reg_L[i].NameCustomReg.Trim() != "")
                    {
                        g_RegPLCBase.Reg_L[i].NameReg = g_RegPLCBase.Reg_L[i].NameCustomReg.ToUpper().Replace("N", "n");
                        g_RegPLCBase.Reg_L[i].NameCustomReg = g_RegPLCBase.Reg_L[i].NameCustomReg.ToUpper().Replace("N", "n");
                    }
                    else
                    {
                        g_RegPLCBase.Reg_L[i].NameReg = g_RegPLCBase.Reg_L[i].NameReg.ToUpper().Replace("N", "n");
                    }
                }
                if (g_RegPLCBase.WriteIni())
                {
                    this.btnSave.RefreshDefaultColor("保存成功", true);
                    SaveEvent();
                }
                else
                {
                    this.btnSave.RefreshDefaultColor("保存失败", false);
                }
                ShowPar_Invoke();//重新刷新显示
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCReg", ex);
                MessageBox.Show("配置出错！");
            }
        }

        /// <summary>
        /// 是否触发保存事件,只有相机寄存器和监控寄存器会触发
        /// </summary>
        protected virtual void SaveEvent()
        {

        }
        #endregion 保存

        #region 显示
        public override void ShowPar()
        {
            try
            {                
                txtRegStart.Text = g_RegPLCBase.regStart;
                dudNumReg.Value = g_RegPLCBase.NumRegSet;//寄存器个数
                //刷新Datagrid数据
                RefreshDG();               
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCReg", ex);
            }           
        }
        /// <summary>
        /// 刷新列表数据
        /// </summary>
        protected void RefreshDG()
        {
            try
            {
                dgReg.ItemsSource = g_RegPLCBase.Reg_L;
                dgReg.Items.Refresh();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseUCReg", ex);
            }            
        }
        #endregion 显示        

        #region 导入Excel
        /// <summary>
        /// 从Excel表格中导入寄存器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInputExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //权限返回
                if (!EngineerReturn())
                {
                    return;
                }
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.InitialDirectory = ParPathRoot.PathRoot + "Store\\PLC\\";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog.FileName.Trim() == "")
                    {
                        MessageBox.Show("文件名不能为空！");
                        return;
                    }
                    new Task(SetRegFromExcel, openFileDialog.FileName).Start();
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// 导入excel
        /// </summary>
        /// <param name="path"></param>
        public virtual void SetRegFromExcel(object path)
        {
            try
            {
                BaseRegPLC baseRegPLC = ReadExcelReg.R_I.ReadExcel(path.ToString());
                g_RegPLCBase.regStart = baseRegPLC.regStart;
                g_RegPLCBase.NumRegSet = baseRegPLC.NumRegSet;

                g_RegPLCBase.Reg_L.Clear();
                g_RegPLCBase.Reg_L = baseRegPLC.Reg_L;

                ShowPar_Invoke();
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion 导入Excel

        #region 自定义属性
        [CategoryAttribute("自定义属性"), DescriptionAttribute("设定显示标题")]
        public string NameTitle
        {
            get { return (string)GetValue(NameTitleProperty); }
            set { SetValue(NameTitleProperty, value); }
        }
        [CategoryAttribute("自定义属性"), DescriptionAttribute("设定显示标题")]
        public static readonly DependencyProperty NameTitleProperty =
        DependencyProperty.Register("NameTitle", typeof(string), typeof(BaseUCReg), new PropertyMetadata("寄存器"));
        #endregion 自定义属性        
    }
}
