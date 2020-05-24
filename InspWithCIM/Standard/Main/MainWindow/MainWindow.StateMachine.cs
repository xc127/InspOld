using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Main
{
    public partial class MainWindow
    {
        void ShowStateMachine()
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                    {
                        switch (MainCom.M_I.StateMachine_e)
                        {
                            case StateMachine_enum.Null:
                                lbStateMachine.Content = "待机中";
                                lbStateMachine.Foreground = Brushes.Brown;
                                ShowState("待机中");
                                break;

                            case StateMachine_enum.Alarm:
                                lbStateMachine.Content = "设备报警";
                                lbStateMachine.Foreground = Brushes.Red;
                                ShowState("设备报警");
                                break;

                            case StateMachine_enum.Auto:
                                lbStateMachine.Content = "自动运行";
                                lbStateMachine.Foreground = Brushes.Green;
                                ShowState("自动运行");
                                break;

                            case StateMachine_enum.Manual:
                                lbStateMachine.Content = "手动运行";
                                lbStateMachine.Foreground = Brushes.Blue;
                                ShowState("手动运行");
                                break;

                            case StateMachine_enum.NullRun:
                                lbStateMachine.Content = "空运转";
                                lbStateMachine.Foreground = Brushes.SkyBlue;
                                ShowState("空运转");
                                break;

                            case StateMachine_enum.Pause:
                                lbStateMachine.Content = "暂停";
                                lbStateMachine.Foreground = Brushes.Orange;
                                ShowState("暂停");
                                break;

                            case StateMachine_enum.Stop:
                                lbStateMachine.Content = "停止";
                                lbStateMachine.Foreground = Brushes.Orange;
                                ShowState("停止");
                                break;

                            case StateMachine_enum.Reset:
                                lbStateMachine.Content = "复位";
                                lbStateMachine.Foreground = Brushes.LimeGreen;
                                ShowState("复位");
                                break;
                        }
                    }
               ));
                //设置机器人是否处于空跑模式
                SetRobotNullRun();
            }
            catch (Exception ex)
            {

            }

        }
    }

    public enum StateMachine_enum
    {
        Null = 1,
        Manual = 2,
        Auto = 3,
        NullRun = 4,//空跑
        Alarm = 5,
        Pause = 6,
        Stop = 7,
        Reset = 8,
    }
}
