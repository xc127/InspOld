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
using Common;
using DealConfigFile;

namespace DealPLC
{
    /// <summary>
    /// UCSetRegCycle.xaml 的交互逻辑
    /// </summary>
    public partial class UCSetRegCycle : BaseUCPLC
    {
        #region 初始化
        public UCSetRegCycle()
        {
            InitializeComponent();

            NameClass = "UCSetRegCycle";
        }

        public override void Init()
        {
            try
            {
                #region 权限设置
                base.SetBtnEnable(this.gdLayout, Authority.Authority_e);
                #endregion 权限设置
                ShowPar_Invoke();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        #region 创建循环读取寄存器
        private void btnCreateCycReg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CreateCycReg())
                {
                    btnCreateCycReg.RefreshDefaultColor("生成成功", true);
                    ShowPar_Invoke();//显示设置
                }
                else
                {
                    btnCreateCycReg.RefreshDefaultColor("生成失败", false);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }        
        }
        /// <summary>
        /// 按照触发寄存器以及数据寄存器的配置，生产循环读取寄存器
        /// </summary>
        /// <returns></returns>
        public bool CreateCycReg()
        {
            try
            {
                ParSetPLC.P_I.RegCyc = "";
                //触发保留,采用单寄存器，减少数量
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//精简版本协议
                {
                    for (int i = 0; i < RegMonitor.R_I.NumReg; i++)
                    {
                        ParSetPLC.P_I.RegCyc += RegMonitor.R_I[i].NameReg + "\\n";
                    }
                    ParSetPLC.P_I.RegCyc = ParSetPLC.P_I.RegCyc.Replace("\\n\\n", "\\n");
                }
                else
                {
                    for (int i = 0; i < 34; i++)
                    {
                        ParSetPLC.P_I.RegCyc += RegMonitor.R_I[i].NameReg.Split('\\')[0] + "\\n";
                    }

                    switch (ParCameraWork.NumCamera)
                    {
                        case 1:
                            ParSetPLC.P_I.RegCyc += RegCameraData.R_I.Reg_L[0].NameReg.Split('\\')[0] + "\\n";
                            break;
                        case 2:
                            ParSetPLC.P_I.RegCyc += RegCameraData.R_I.Reg_L[0].NameReg.Split('\\')[0] + "\\n"
                                + RegCameraData.R_I.Reg_L[7].NameReg.Split('\\')[0] + "\\n";
                            break;
                        case 3:
                            ParSetPLC.P_I.RegCyc += RegCameraData.R_I.Reg_L[0].NameReg.Split('\\')[0] + "\\n"
                               + RegCameraData.R_I.Reg_L[7].NameReg.Split('\\')[0] + "\\n"
                               + RegCameraData.R_I.Reg_L[14].NameReg.Split('\\')[0] + "\\n";
                            break;
                        case 4:
                            ParSetPLC.P_I.RegCyc += RegCameraData.R_I.Reg_L[0].NameReg.Split('\\')[0] + "\\n"
                               + RegCameraData.R_I.Reg_L[7].NameReg.Split('\\')[0] + "\\n"
                               + RegCameraData.R_I.Reg_L[14].NameReg.Split('\\')[0] + "\\n"
                               + RegCameraData.R_I.Reg_L[21].NameReg.Split('\\')[0] + "\\n";
                            break;
                        case 5:
                            ParSetPLC.P_I.RegCyc += RegCameraData.R_I.Reg_L[0].NameReg.Split('\\')[0] + "\\n"
                               + RegCameraData.R_I.Reg_L[7].NameReg.Split('\\')[0] + "\\n"
                               + RegCameraData.R_I.Reg_L[14].NameReg.Split('\\')[0] + "\\n"
                               + RegCameraData.R_I.Reg_L[21].NameReg.Split('\\')[0] + "\\n"
                               + RegCameraData.R_I.Reg_L[28].NameReg.Split('\\')[0] + "\\n";
                            break;
                        case 6:
                            ParSetPLC.P_I.RegCyc += RegCameraData.R_I.Reg_L[0].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[7].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[14].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[21].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[28].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[35].NameReg.Split('\\')[0] + "\\n";

                            break;

                        case 7:
                            ParSetPLC.P_I.RegCyc += RegCameraData.R_I.Reg_L[0].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[7].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[14].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[21].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[28].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[35].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[42].NameReg.Split('\\')[0] + "\\n";
                            break;

                        case 8:
                            ParSetPLC.P_I.RegCyc += RegCameraData.R_I.Reg_L[0].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[7].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[14].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[21].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[28].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[35].NameReg.Split('\\')[0] + "\\n"
                              + RegCameraData.R_I.Reg_L[42].NameReg.Split('\\')[0] + "\\n"
                                + RegCameraData.R_I.Reg_L[49].NameReg.Split('\\')[0] + "\\n";
                            break;
                    }

                    //读值保留
                    for (int i = 34; i < RegMonitor.R_I.NumReg; i++)
                    {
                        ParSetPLC.P_I.RegCyc += RegMonitor.R_I[i].NameReg;
                    }
                }

                //只有重新设定初始寄存器，才写此ini
                ParSetPLC.P_I.WriteIniRegCycle();
                ParSetPLC.P_I.ReadIniRegCycle();

                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 创建循环读取寄存器

        #region 显示      
        public override void ShowPar()
        {
            txtCycReg.Text = ParSetPLC.P_I.RegCyc.Replace("\n", "\\n");
        }
        #endregion 显示
    }
}
