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

namespace DealRobot
{
    /// <summary>
    /// WinAllPointValue.xaml 的交互逻辑
    /// </summary>
    public partial class WinAllPointValue : BaseMetroWindow
    {
        #region 初始化
        public WinAllPointValue()
        {
            InitializeComponent();
        }
        private void BaseMetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Init_Pos();
            ShowPar_Invoke();
        }
        #endregion 初始化

        void Init_Pos()
        {
            try
            {
                AllRobotPointValue.A_I.AllRobotPointValue_L.Clear();
                for (int i = 0; i < RobotStdPoint.R_I.Count; i++)
                {
                    RobotStdPoint robotStdPoint = RobotStdPoint.R_I[i];

                    #region 输入
                    //vaccum
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_Vaccum.NameClass,
                        X = robotStdPoint.P_Vaccum.DblValue1,
                        Y = robotStdPoint.P_Vaccum.DblValue2,
                        Z = robotStdPoint.P_Vaccum.DblValue3,
                        R = robotStdPoint.P_Vaccum.DblValue4,

                        XInfo = "使能|真空IO序号",
                        YInfo = "等待时间",
                        ZInfo = "NG报警IO",
                        RInfo = "NG跳转点位",
                    });

                    //P_InputIO1
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_InputIO1.NameClass,
                        X = robotStdPoint.P_InputIO1.DblValue1,
                        Y = robotStdPoint.P_InputIO1.DblValue2,
                        Z = robotStdPoint.P_InputIO1.DblValue3,
                        R = robotStdPoint.P_InputIO1.DblValue4,

                        XInfo = "使能",
                        YInfo = "IO序号",
                        ZInfo = "等待时间",
                        RInfo = "NG报警IO",
                    });

                    //P_JumpInputIO1
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_JumpIOP1.NameClass,
                        X = robotStdPoint.P_JumpIOP1.DblValue1,
                        Y = robotStdPoint.P_JumpIOP1.DblValue2,
                        Z = robotStdPoint.P_JumpIOP1.DblValue3,
                        R = robotStdPoint.P_JumpIOP1.DblValue4,

                        XInfo = "OK跳转点位",
                        YInfo = "NG跳转点位",
                    });

                    //P_InputIO2
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_InputIO2.NameClass,
                        X = robotStdPoint.P_InputIO2.DblValue1,
                        Y = robotStdPoint.P_InputIO2.DblValue2,
                        Z = robotStdPoint.P_InputIO2.DblValue3,
                        R = robotStdPoint.P_InputIO2.DblValue4,

                        XInfo = "使能",
                        YInfo = "IO序号",
                        ZInfo = "等待时间",
                        RInfo = "NG报警IO",
                    });

                    //P_JumpInputIO2
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_JumpIOP2.NameClass,
                        X = robotStdPoint.P_JumpIOP2.DblValue1,
                        Y = robotStdPoint.P_JumpIOP2.DblValue2,
                        Z = robotStdPoint.P_JumpIOP2.DblValue3,
                        R = robotStdPoint.P_JumpIOP2.DblValue4,

                        XInfo = "OK跳转点位",
                        YInfo = "NG跳转点位",
                    });


                    //P_InputIO3
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_InputIO3.NameClass,
                        X = robotStdPoint.P_InputIO3.DblValue1,
                        Y = robotStdPoint.P_InputIO3.DblValue2,
                        Z = robotStdPoint.P_InputIO3.DblValue3,
                        R = robotStdPoint.P_InputIO3.DblValue4,

                        XInfo = "使能",
                        YInfo = "IO序号",
                        ZInfo = "等待时间",
                        RInfo = "NG报警IO",
                     });
                     //P_JumpInputIO3
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_JumpIOP3.NameClass,
                        X = robotStdPoint.P_JumpIOP3.DblValue1,
                        Y = robotStdPoint.P_JumpIOP3.DblValue2,
                        Z = robotStdPoint.P_JumpIOP3.DblValue3,
                        R = robotStdPoint.P_JumpIOP3.DblValue4,

                        XInfo = "OK跳转点位",
                        YInfo = "NG跳转点位",
                    });
                     //P_InputIO4
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_InputIO4.NameClass,
                        X = robotStdPoint.P_InputIO4.DblValue1,
                        Y = robotStdPoint.P_InputIO4.DblValue2,
                        Z = robotStdPoint.P_InputIO4.DblValue3,
                        R = robotStdPoint.P_InputIO4.DblValue4,

                        XInfo = "使能",
                        YInfo = "IO序号",
                        ZInfo = "等待时间",
                        RInfo = "NG报警IO",
                    });
                     //P_JumpInputIO4
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_JumpIOP4.NameClass,
                        X = robotStdPoint.P_JumpIOP4.DblValue1,
                        Y = robotStdPoint.P_JumpIOP4.DblValue2,
                        Z = robotStdPoint.P_JumpIOP4.DblValue3,
                        R = robotStdPoint.P_JumpIOP4.DblValue4,

                        XInfo = "OK跳转点位",
                        YInfo = "NG跳转点位",
                    });

                    //P_PC
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_PC.NameClass,
                        X = robotStdPoint.P_PC.DblValue1,
                        Y = robotStdPoint.P_PC.DblValue2,
                        Z = robotStdPoint.P_PC.DblValue3,
                        R = robotStdPoint.P_PC.DblValue4,

                        XInfo = "使能",
                        YInfo = "等待时间",
                        ZInfo = "NG报警IO",
                        RInfo = "NG跳转点位",
                    });

                    //P_PCCMD
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_PCCMD.NameClass,
                        X = robotStdPoint.P_PCCMD.DblValue1,
                        Y = robotStdPoint.P_PCCMD.DblValue2,
                        Z = robotStdPoint.P_PCCMD.DblValue3,
                        R = robotStdPoint.P_PCCMD.DblValue4,

                        XInfo = "更改X坐标",
                        YInfo = "更改Y坐标",
                        ZInfo = "更改Z坐标",
                        RInfo = "更改R坐标",
                    });
                    #endregion 输入

                    #region 运动
                    //P_Vel
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_Vel.NameClass,
                        X = robotStdPoint.P_Vel.DblValue1,
                        Y = robotStdPoint.P_Vel.DblValue2,
                        Z = robotStdPoint.P_Vel.DblValue3,
                        R = robotStdPoint.P_Vel.DblValue4,

                        XInfo = "使能",
                        YInfo = "功率",
                        ZInfo = "速度",
                        RInfo = "加减速",
                    });

                    //P_Move
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_Move.NameClass,
                        X = robotStdPoint.P_Move.DblValue1,
                        Y = robotStdPoint.P_Move.DblValue2,
                        Z = robotStdPoint.P_Move.DblValue3,
                        R = robotStdPoint.P_Move.DblValue4,

                        XInfo = "运动指令",

                    });

                    //P_Des
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_Des.NameClass,
                        X = robotStdPoint.P_Des.DblValue1,
                        Y = robotStdPoint.P_Des.DblValue2,
                        Z = robotStdPoint.P_Des.DblValue3,
                        R = robotStdPoint.P_Des.DblValue4,

                        XInfo = "X坐标",
                        YInfo = "Y坐标",
                        ZInfo = "Z坐标",
                        RInfo = "R坐标",
                        ArmInfo="手系(1Left,2Right)",
                    });

                    //P_Here
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_Here.NameClass,
                        X = robotStdPoint.P_Here.DblValue1,
                        Y = robotStdPoint.P_Here.DblValue2,
                        Z = robotStdPoint.P_Here.DblValue3,
                        R = robotStdPoint.P_Here.DblValue4,

                        XInfo = "X坐标(相对)",
                        YInfo = "Y坐标(相对)",
                        ZInfo = "Z坐标",
                        RInfo = "R坐标",

                    });

                    //P_PassCMD
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_PassCMD.NameClass,
                        X = robotStdPoint.P_PassCMD.DblValue1,
                        Y = robotStdPoint.P_PassCMD.DblValue2,
                        Z = robotStdPoint.P_PassCMD.DblValue3,
                        R = robotStdPoint.P_PassCMD.DblValue4,

                        XInfo = "Pass点个数",
                        YInfo = "HereZ坐标相对或者绝对",
                        ZInfo = "HereR坐标相对或者绝对",
                    });


                    //P_Pass1
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_Pass1.NameClass,
                        X = robotStdPoint.P_Pass1.DblValue1,
                        Y = robotStdPoint.P_Pass1.DblValue2,
                        Z = robotStdPoint.P_Pass1.DblValue3,
                        R = robotStdPoint.P_Pass1.DblValue4,

                        XInfo = "X坐标",
                        YInfo = "Y坐标",
                        ZInfo = "Z坐标",
                        RInfo = "R坐标",
                        ArmInfo = "手系(1Left,2Right)",
                    });
                    //P_Pass2
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_Pass2.NameClass,
                        X = robotStdPoint.P_Pass2.DblValue1,
                        Y = robotStdPoint.P_Pass2.DblValue2,
                        Z = robotStdPoint.P_Pass2.DblValue3,
                        R = robotStdPoint.P_Pass2.DblValue4,

                        XInfo = "X坐标",
                        YInfo = "Y坐标",
                        ZInfo = "Z坐标",
                        RInfo = "R坐标",
                        ArmInfo = "手系(1Left,2Right)",
                    });
                    //P_Pass3
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_Pass3.NameClass,
                        X = robotStdPoint.P_Pass3.DblValue1,
                        Y = robotStdPoint.P_Pass3.DblValue2,
                        Z = robotStdPoint.P_Pass3.DblValue3,
                        R = robotStdPoint.P_Pass3.DblValue4,

                        XInfo = "X坐标",
                        YInfo = "Y坐标",
                        ZInfo = "Z坐标",
                        RInfo = "R坐标",
                        ArmInfo = "手系(1Left,2Right)",
                    });
                 
                    #endregion 运动

                    #region 输出
                    //P_OutIO
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_OutIO.NameClass,
                        X = robotStdPoint.P_OutIO.DblValue1,
                        Y = robotStdPoint.P_OutIO.DblValue2,
                        Z = robotStdPoint.P_OutIO.DblValue3,
                        R = robotStdPoint.P_OutIO.DblValue4,

                        XInfo = "使能|序号",                    
                    });

                    //P_OutVaccum
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_OutVaccum.NameClass,
                        X = robotStdPoint.P_OutVaccum.DblValue1,
                        Y = robotStdPoint.P_OutVaccum.DblValue2,
                        Z = robotStdPoint.P_OutVaccum.DblValue3,
                        R = robotStdPoint.P_OutVaccum.DblValue4,

                        XInfo = "真空IO(不操作,关闭,打开)",
                        YInfo = "吹气IO(不操作,关闭,吹气并关闭，吹气)",
                        ZInfo = "吹气延迟",
                        
                    });

                    //P_OutPC
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_OutPC.NameClass,
                        X = robotStdPoint.P_OutPC.DblValue1,
                        Y = robotStdPoint.P_OutPC.DblValue2,
                        Z = robotStdPoint.P_OutPC.DblValue3,
                        R = robotStdPoint.P_OutPC.DblValue4,

                        XInfo = "使能",
                        YInfo = "指令类型",
                        ZInfo = "指令值",
                    });

                    //P_NextPoint
                    AllRobotPointValue.A_I.AllRobotPointValue_L.Add(new AllRobotPointValue()
                    {
                        No = i,
                        NamePoint = robotStdPoint.NamePoint,
                        NameValue = robotStdPoint.P_NextPoint.NameClass,
                        X = robotStdPoint.P_NextPoint.DblValue1,
                        Y = robotStdPoint.P_NextPoint.DblValue2,
                        Z = robotStdPoint.P_NextPoint.DblValue3,
                        R = robotStdPoint.P_NextPoint.DblValue4,

                        XInfo = "跳转点位",
                    });
                   
                    #endregion 输出
                }

            }
            catch (Exception ex)
            {

            }
        }

        #region 显示
        public override void ShowPar()
        {
            RefreshDgPoint();
        }

        void RefreshDgPoint()
        {
            try
            {
                dgPoint.ItemsSource = AllRobotPointValue.A_I.AllRobotPointValue_L;
                dgPoint.Items.Refresh();
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion 显示

       
    }

    public class AllRobotPointValue:BaseClass
    {
        #region 静态类实例
        public static AllRobotPointValue A_I=new AllRobotPointValue ();
        #endregion 静态类实例

        #region 定义
        public string NamePoint { set; get; }
        public string NameValue { set; get; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double R { get; set; }
        public double Arm { get; set; }

        public string XInfo { get; set; }
        public string YInfo { get; set; }
        public string ZInfo { get; set; }
        public string RInfo { get; set; }
        public string ArmInfo { get; set; }

        //List
        public List<AllRobotPointValue> AllRobotPointValue_L=new List<AllRobotPointValue> ();

        #endregion 定义
    }
}
