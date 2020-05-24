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
using ControlLib;

namespace DealPLC
{
    public class UCSetRegMonitor : BaseUCReg
    {
        #region 定义
        public override event Action SaveReg_event;//触发保存事件
        #endregion 定义

        #region 初始化
        public override void Init()
        {
            base.Init(RegMonitor.R_I, "监控寄存器");

            NameClass = "UCSetRegMonitor";
        }
        #endregion 初始化   
             
        #region 触发保存事件
        protected override void SaveEvent()
        {
            try
            {
                SaveReg_event();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 触发保存事件

        #region 生成寄存器
        public override void CreateRegTwo()
        {
            try
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//精简版本协议                
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

                            if (i < RegMonitor.R_I.NumTrigger)
                            {
                                //选择读取寄存器为单个或是两个                       
                                RegPLC inst = new RegPLC()
                                {
                                    No = i,
                                    NameReg = strLetter + (intNo + i).ToString() + "\\n",
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
                            else
                            {
                                //选择读取寄存器为单个或是两个                       
                                RegPLC inst = new RegPLC()
                                {
                                    No = i,
                                    NameReg = strLetter + (intNo + RegMonitor.R_I.NumTrigger + 2 * (i - RegMonitor.R_I.NumTrigger)).ToString() + "\\n" + strLetter + (intNo + RegMonitor.R_I.NumTrigger + 2 * (i - +RegMonitor.R_I.NumTrigger) + 1).ToString() + "\\n",
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
                else
                {
                    base.CreateRegTwo();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 生成寄存器

        #region 显示

        #endregion 显示
    }
}

