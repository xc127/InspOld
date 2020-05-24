using System;
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
using Camera;
using HalconDotNet;
using DealFile;
using SetPar;
using DealPLC;
using DealConfigFile;
using BasicClass;
using DealMontionCtrler;
using DealLog;
using DealRobot;

namespace Main
{
    partial class MainWindow
    {
        #region 定义
        bool door1Safe = true;
        bool door2Safe = true;
        bool door3Safe = true;
        bool door4Safe = true;
        bool grating1Safe = true;
        bool grating2Safe = true;




        #endregion 定义

        #region 报错数量
        int g_NumErrorAxis = 0;
        #endregion

        #region 注册
        void LoginMotionEvent()
        {
            #region 注册本地IO
            LogicMotionCtler.L_I.Can0Input0_event += new IntAction(L_I_Can0Input0_event);
            LogicMotionCtler.L_I.Can0Input1_event += new IntAction(L_I_Can0Input1_event);
            LogicMotionCtler.L_I.Can0Input2_event += new IntAction(L_I_Can0Input2_event);
            LogicMotionCtler.L_I.Can0Input3_event += new IntAction(L_I_Can0Input3_event);
            LogicMotionCtler.L_I.Can0Input4_event += new IntAction(L_I_Can0Input4_event);
            LogicMotionCtler.L_I.Can0Input5_event += new IntAction(L_I_Can0Input5_event);
            LogicMotionCtler.L_I.Can0Input6_event += new IntAction(L_I_Can0Input6_event);
            LogicMotionCtler.L_I.Can0Input7_event += new IntAction(L_I_Can0Input7_event);
            LogicMotionCtler.L_I.Can0Input8_event += new IntAction(L_I_Can0Input8_event);
            LogicMotionCtler.L_I.Can0Input9_event += new IntAction(L_I_Can0Input9_event);
            LogicMotionCtler.L_I.Can0Input10_event += new IntAction(L_I_Can0Input10_event);
            LogicMotionCtler.L_I.Can0Input11_event += new IntAction(L_I_Can0Input11_event);
            LogicMotionCtler.L_I.Can0Input12_event += new IntAction(L_I_Can0Input12_event);
            LogicMotionCtler.L_I.Can0Input13_event += new IntAction(L_I_Can0Input13_event);
            LogicMotionCtler.L_I.Can0Input14_event += new IntAction(L_I_Can0Input14_event);
            LogicMotionCtler.L_I.Can0Input15_event += new IntAction(L_I_Can0Input15_event);
            #endregion

            #region 注册Can1
            LogicMotionCtler.L_I.Can1Input0_event += new IntAction(L_I_Can1Input0_event);
            LogicMotionCtler.L_I.Can1Input1_event += new IntAction(L_I_Can1Input1_event);
            LogicMotionCtler.L_I.Can1Input2_event += new IntAction(L_I_Can1Input2_event);
            LogicMotionCtler.L_I.Can1Input3_event += new IntAction(L_I_Can1Input3_event);
            LogicMotionCtler.L_I.Can1Input4_event += new IntAction(L_I_Can1Input4_event);
            LogicMotionCtler.L_I.Can1Input5_event += new IntAction(L_I_Can1Input5_event);
            LogicMotionCtler.L_I.Can1Input6_event += new IntAction(L_I_Can1Input6_event);
            LogicMotionCtler.L_I.Can1Input7_event += new IntAction(L_I_Can1Input7_event);
            LogicMotionCtler.L_I.Can1Input8_event += new IntAction(L_I_Can1Input8_event);
            LogicMotionCtler.L_I.Can1Input9_event += new IntAction(L_I_Can1Input9_event);
            LogicMotionCtler.L_I.Can1Input10_event += new IntAction(L_I_Can1Input10_event);
            LogicMotionCtler.L_I.Can1Input11_event += new IntAction(L_I_Can1Input11_event);
            LogicMotionCtler.L_I.Can1Input12_event += new IntAction(L_I_Can1Input12_event);
            LogicMotionCtler.L_I.Can1Input13_event += new IntAction(L_I_Can1Input13_event);
            LogicMotionCtler.L_I.Can1Input14_event += new IntAction(L_I_Can1Input14_event);
            LogicMotionCtler.L_I.Can1Input15_event += new IntAction(L_I_Can1Input15_event);
            #endregion

            #region 注册Can2
            LogicMotionCtler.L_I.Can2Input0_event += new IntAction(L_I_Can2Input0_event);
            LogicMotionCtler.L_I.Can2Input1_event += new IntAction(L_I_Can2Input1_event);
            LogicMotionCtler.L_I.Can2Input2_event += new IntAction(L_I_Can2Input2_event);
            LogicMotionCtler.L_I.Can2Input3_event += new IntAction(L_I_Can2Input3_event);
            LogicMotionCtler.L_I.Can2Input4_event += new IntAction(L_I_Can2Input4_event);
            LogicMotionCtler.L_I.Can2Input5_event += new IntAction(L_I_Can2Input5_event);
            LogicMotionCtler.L_I.Can2Input6_event += new IntAction(L_I_Can2Input6_event);
            LogicMotionCtler.L_I.Can2Input7_event += new IntAction(L_I_Can2Input7_event);
            LogicMotionCtler.L_I.Can2Input8_event += new IntAction(L_I_Can2Input8_event);
            LogicMotionCtler.L_I.Can2Input9_event += new IntAction(L_I_Can2Input9_event);
            LogicMotionCtler.L_I.Can2Input10_event += new IntAction(L_I_Can2Input10_event);
            LogicMotionCtler.L_I.Can2Input11_event += new IntAction(L_I_Can2Input11_event);
            LogicMotionCtler.L_I.Can2Input12_event += new IntAction(L_I_Can2Input12_event);
            LogicMotionCtler.L_I.Can2Input13_event += new IntAction(L_I_Can2Input13_event);
            LogicMotionCtler.L_I.Can2Input14_event += new IntAction(L_I_Can2Input14_event);
            LogicMotionCtler.L_I.Can2Input15_event += new IntAction(L_I_Can2Input15_event);
            #endregion

            #region 注册轴IO
            LogicMotionCtler.L_I.AxisError_Event += new StrAction(L_I_AxisError_Event);
            LogicMotionCtler.L_I.AxisErrorReset_Event += new Action(L_I_AxisErrorReset_Event);
            #endregion
        }

        #endregion

        #region 轴报错相关
        void L_I_AxisErrorReset_Event()
        {
            g_NumErrorAxis--;
        }

        void L_I_AxisError_Event(string str)
        {
            g_NumErrorAxis++;
            AlarmMachine(str);
        }
        #endregion

        #region Can0处理事件
        void L_I_Can0Input0_event(int i)
        {
            ShowState("启动按钮按下！");
        }
        void L_I_Can0Input1_event(int i)
        {
            ShowState("停止按钮按下！");
        }
        void L_I_Can0Input2_event(int i)
        {
            if (i == 0)
            {
                AlarmMachine("紧急停止");
            }
        }
        void L_I_Can0Input3_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[0].AnnotationIn[3] + "信号触发");
                g_AirCyliderUpDown.PNStatue_E = PNStatue_Enum.Positive;
            }
            else
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[0].AnnotationIn[3] + "信号消失");
                g_AirCyliderUpDown.PNStatue_E = PNStatue_Enum.Null;
            }
        }
        void L_I_Can0Input4_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[0].AnnotationIn[4] + "信号触发");
                g_AirCyliderUpDown.PNStatue_E = PNStatue_Enum.Negative;
            }
            else
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[0].AnnotationIn[4] + "信号消失");
                g_AirCyliderUpDown.PNStatue_E = PNStatue_Enum.Null;
            }
        }
        void L_I_Can0Input5_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[0].AnnotationIn[5] + "信号触发");
                door1Safe = false;
            }
            else
            {
                door1Safe = true;
            }
        }
        void L_I_Can0Input6_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[0].AnnotationIn[6] + "信号触发");
                door2Safe = false;
            }
            else
            {
                door2Safe = true;
            }
        }
        void L_I_Can0Input7_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[0].AnnotationIn[8] + "信号触发");
                g_BlPlateVacoo1_1OK = true;
            }
            else
            {
                g_BlPlateVacoo1_1OK = false;
            }
        }
        void L_I_Can0Input8_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[0].AnnotationIn[8] + "信号触发");
                g_BlPlateVacoo1_2OK = true;
            }
            else
            {
                g_BlPlateVacoo1_2OK = false;
            }
        }
        void L_I_Can0Input9_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[0].AnnotationIn[9] + "信号触发");
                g_BlPlateVacoo2_1OK = true;
                if (g_BlPlateVacoo2_2OK)
                {
                    g_BlPlateVacoo2AllOK = true;
                }
            }
            else
            {
                g_BlPlateVacoo2AllOK = false;
                g_BlPlateVacoo2_1OK = false;
            }
        }
        void L_I_Can0Input10_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[0].AnnotationIn[10] + "信号触发");
                g_BlPlateVacoo2_2OK = true;
                if (g_BlPlateVacoo2_1OK)
                {
                    g_BlPlateVacoo2AllOK = true;
                }
            
            }
            else
            {
                g_BlPlateVacoo2AllOK = false;
                g_BlPlateVacoo2_2OK = false;
            }
        }
        void L_I_Can0Input11_event(int i)
        {

        }
        void L_I_Can0Input12_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[0].AnnotationIn[12] + "信号触发");
                g_BlTransVacoo1OK = true;
            }
            else
            {
                g_BlTransVacoo1OK = false;
            }
        }
        void L_I_Can0Input13_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[0].AnnotationIn[13] + "信号触发");
                g_BlTransVacoo2OK = true;
            }
            else
            {
                g_BlTransVacoo2OK = false;
            }
        }
        void L_I_Can0Input14_event(int i) { }
        void L_I_Can0Input15_event(int i) { }
        #endregion

        #region Can1处理事件
        void L_I_Can1Input0_event(int i) { }
        void L_I_Can1Input1_event(int i) { }
        void L_I_Can1Input2_event(int i) { }
        void L_I_Can1Input3_event(int i) { }
        void L_I_Can1Input4_event(int i) { }
        void L_I_Can1Input5_event(int i) { }
        void L_I_Can1Input6_event(int i) { }
        void L_I_Can1Input7_event(int i) { }
        void L_I_Can1Input8_event(int i) { }
        void L_I_Can1Input9_event(int i) { }
        void L_I_Can1Input10_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[1].AnnotationIn[10] + "信号触发");
                g_BlRobotArrive = true;
            }
        }
        void L_I_Can1Input11_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[1].AnnotationIn[11] + "信号触发");
                g_BlRobotLeave = true;
            }
        }
        void L_I_Can1Input12_event(int i) { }
        void L_I_Can1Input13_event(int i) { }
        void L_I_Can1Input14_event(int i) { }
        void L_I_Can1Input15_event(int i) { }
        #endregion

        #region Can2处理事件
        void L_I_Can2Input0_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[2].AnnotationIn[0] + "信号触发");

                g_BlDownStreamLeave = true;
            }
        }
        void L_I_Can2Input1_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[2].AnnotationIn[1] + "信号触发");
                g_BlDownStreamArrive1 = true;

            }
            else
            {

            }
        }
        void L_I_Can2Input2_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[2].AnnotationIn[2] + "信号触发");

                g_BlDownStreamArrive2 = true;
            }
        }
        void L_I_Can2Input3_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[2].AnnotationIn[3] + "信号触发");
                g_BlDownStreamTransError1 = true;
            }
        }
        void L_I_Can2Input4_event(int i)
        {
            if (i == 1)
            {
                ShowState(ParMotionCtrler.P_I.ParIOModule_L[2].AnnotationIn[4] + "信号触发");
                g_BlDownStreamTransError2 = true;
            }
        }
        void L_I_Can2Input5_event(int i) { }
        void L_I_Can2Input6_event(int i) { }
        void L_I_Can2Input7_event(int i) { }
        void L_I_Can2Input8_event(int i) { }
        void L_I_Can2Input9_event(int i) { }
        void L_I_Can2Input10_event(int i) { }
        void L_I_Can2Input11_event(int i) { }
        void L_I_Can2Input12_event(int i) { }
        void L_I_Can2Input13_event(int i) { }
        void L_I_Can2Input14_event(int i) { }
        void L_I_Can2Input15_event(int i) { }
        #endregion

    }

    public enum PNStatue_Enum
    {
        Null,
        Positive,
        Negative,
    }
}
