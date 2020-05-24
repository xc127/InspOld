using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Threading.Tasks;
using System.Threading;
using DealFile;
using System.Text.RegularExpressions;
using BasicClass;

namespace DealRobot
{
    partial class LogicRobot
    {
        #region 定义
        int thWaitTime_m = 20;//数据读取等待时间

        //首先定义正则表达式，验证输入的初始化寄存器
        string g_strRegex = "\\w\\d{1,6}";//规则为一个字符，加1-6位的数字
        #endregion 定义

        #region 定义事件
        public event Action Shakehand_event;
        public event StrAction FeedBackOK_event;
        public event StrAction FeedBackNG_event;

        public event IntAction RobotReset_event;
        public event IntAction RobotHome_event;
        public event IntAction RobotThrow_event;
        public event StrAction Monitor_event;
        public event Point4DAction CurrPoint_event;//机器人当前位置

        public event TrrigerSourceAction_del Camera1_event;
        public event TrrigerSourceAction_del Camera2_event;
        public event TrrigerSourceAction_del Camera3_event;
        public event TrrigerSourceAction_del Camera4_event;
        public event TrrigerSourceAction_del Camera5_event;
        public event TrrigerSourceAction_del Camera6_event;

        public event TrrigerSourceIntAction_del Camera1_index_event;
        public event TrrigerSourceIntAction_del Camera2_index_event;
        public event TrrigerSourceIntAction_del Camera3_index_event;
        public event TrrigerSourceIntAction_del Camera4_index_event;
        public event TrrigerSourceIntAction_del Camera5_index_event;
        public event TrrigerSourceIntAction_del Camera6_index_event;

        public event TrrigerSourceIntNAction_del Camera1_IndexN_event;
        public event TrrigerSourceIntNAction_del Camera2_IndexN_event;
        public event TrrigerSourceIntNAction_del Camera3_IndexN_event;
        public event TrrigerSourceIntNAction_del Camera4_IndexN_event;
        public event TrrigerSourceIntNAction_del Camera5_IndexN_event;
        public event TrrigerSourceIntNAction_del Camera6_IndexN_event;

        public event IntAction Others_event;
        public event IntAction Delay_event;//数据延迟

        public event IntNAction Input_event;
        public event IntNAction Output_event;
        public event DblNAction JogPosCurr_event;
        #endregion 定义事件

        #region 读取数据
        public void ReadData_Task()
        {
            try
            {
                ParLogicRobot.P_I.blStartRead = true;
                Task task = new Task(ReadData);
                task.Start();
            }
            catch
            {

            }
        }
        //读取数据
        public void ReadData()
        {
            while (ParLogicRobot.P_I.blStartRead)
            {
                try
                {
                    Thread.Sleep(20);
                    string str = g_PortRobotBase.ReadData().TrimEnd(new char[] { '\n', '\r', '\\' });
                    if (str == "Error")
                    {
                        return;
                    }
                    TrrigerRobot(str);
                }
                catch (Exception ex)
                {
                    Thread.Sleep(500);
                    Log.L_I.WriteError(NameClass, ex);
                }
            }
        }
        //根据指令触发机器人
        public void TrrigerRobot(string str)
        {
            try
            {
                string[] cmd = null;
                if (str.Contains(','))
                {
                    cmd = str.Trim().Split(',');
                }
                else
                {
                    cmd = str.Trim().Split(' ');
                }
                string Head = cmd[0].Trim();
                int intValue = 0;
                int index = -1;
                int[] indexN = null;
                if (cmd.Length == 2)
                {

                    try
                    {
                        intValue = int.Parse(cmd[1].Trim());
                    }
                    catch (Exception)
                    {

                    }

                }
                else if (cmd.Length == 3)
                {
                    try
                    {
                        intValue = int.Parse(cmd[1].Trim());
                        index = int.Parse(cmd[2].Trim());
                    }
                    catch (Exception)
                    {

                    }
                }
                else if (cmd.Length == 4)
                {
                    try
                    {
                        indexN = new int[2];
                        intValue = int.Parse(cmd[1].Trim());
                        indexN[0] = int.Parse(cmd[2].Trim());
                        indexN[1] = int.Parse(cmd[3].Trim());
                    }
                    catch (Exception)
                    {

                    }
                }

                switch (Head)
                {
                    case "SHAKEHAND":
                        Shakehand_event();
                        break;

                    case "HAND":
                        Shakehand_event();
                        break;

                    case "OK":
                        ParLogicRobot.P_I.isReadOK = true;
                        FeedBackOK_event(str);
                        break;

                    case "NG":
                        FeedBackNG_event(str);
                        break;

                    case "RESET":
                        RobotReset_event(intValue);
                        break;

                    case "HOME":
                        RobotHome_event(intValue);
                        break;

                    case "THROW":
                        RobotThrow_event(intValue);
                        break;

                    case "A"://相机1
                        if (cmd.Length == 2)
                        {
                            TrrigerCamera1(intValue);
                            //new Task(TrrigerCamera1, intValue).Start();
                        }
                        else if (cmd.Length == 3)
                        {
                            Camera1_index_event(TriggerSource_enum.Robot, intValue, index);
                        }
                        else if (cmd.Length == 4)
                        {
                            Camera1_IndexN_event(TriggerSource_enum.Robot, intValue, indexN);
                        }
                        break;

                    case "B"://相机2
                        if (cmd.Length == 2)
                        {
                            TrrigerCamera2(intValue);
                            //new Task(TrrigerCamera2, intValue).Start();
                        }
                        else if (cmd.Length == 3)
                        {
                            Camera2_index_event(TriggerSource_enum.Robot, intValue, index);
                        }
                        else if (cmd.Length == 4)
                        {
                            Camera2_IndexN_event(TriggerSource_enum.Robot, intValue, indexN);
                        }
                        break;

                    case "C"://相机3
                        if (cmd.Length == 2)
                        {
                            TrrigerCamera3(intValue);
                            //new Task(TrrigerCamera3, intValue).Start();
                        }
                        else if (cmd.Length == 3)
                        {
                            Camera3_index_event(TriggerSource_enum.Robot, intValue, index);
                        }
                        else if (cmd.Length == 4)
                        {
                            Camera3_IndexN_event(TriggerSource_enum.Robot, intValue, indexN);
                        }
                        break;

                    case "D"://相机4
                        if (cmd.Length == 2)
                        {
                            TrrigerCamera4(intValue);
                            //new Task(TrrigerCamera4, intValue).Start();
                        }
                        else if (cmd.Length == 3)
                        {
                            Camera4_index_event(TriggerSource_enum.Robot, intValue, index);
                        }
                        else if (cmd.Length == 4)
                        {
                            Camera4_IndexN_event(TriggerSource_enum.Robot, intValue, indexN);
                        }
                        break;

                    case "E"://相机5
                        if (cmd.Length == 2)
                        {
                            TrrigerCamera5(intValue);
                            //new Task(TrrigerCamera5, intValue).Start();
                        }
                        else if (cmd.Length == 3)
                        {
                            Camera5_index_event(TriggerSource_enum.Robot, intValue, index);
                        }
                        else if (cmd.Length == 4)
                        {
                            Camera5_IndexN_event(TriggerSource_enum.Robot, intValue, indexN);
                        }
                        break;

                    case "F"://相机6
                        if (cmd.Length == 2)
                        {
                            TrrigerCamera6(intValue);
                            //new Task(TrrigerCamera6, intValue).Start();
                        }
                        else if (cmd.Length == 3)
                        {
                            Camera6_index_event(TriggerSource_enum.Robot, intValue, index);
                        }
                        else if (cmd.Length == 4)
                        {
                            Camera6_IndexN_event(TriggerSource_enum.Robot, intValue, indexN);
                        }
                        break;

                    case "O":
                        new Task(TrrigerOthers, intValue).Start();
                        break;

                    case "In":
                        new Task(TrrigerInput, str).Start();
                        break;

                    case "Out":
                        new Task(TrrigerOutput, str).Start();
                        break;

                    case "PosHere":
                        new Task(TrrigerJogPosCurr, str).Start();
                        break;

                    case "Delay"://超时
                        Delay_event(intValue);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }


        #region 线程触发
        /// <summary>
        /// 相机1触发
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerCamera1(object intValue)
        {
            try
            {
                Camera1_event(TriggerSource_enum.Robot, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机2触发
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerCamera2(object intValue)
        {
            try
            {
                Camera2_event(TriggerSource_enum.Robot, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机3触发
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerCamera3(object intValue)
        {
            try
            {
                Camera3_event(TriggerSource_enum.Robot, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机4触发
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerCamera4(object intValue)
        {
            try
            {
                Camera4_event(TriggerSource_enum.Robot, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机5触发
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerCamera5(object intValue)
        {
            try
            {
                Camera5_event(TriggerSource_enum.Robot, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机6触发
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerCamera6(object intValue)
        {
            try
            {
                Camera6_event(TriggerSource_enum.Robot, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 其他
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerOthers(object intValue)
        {
            try
            {
                Others_event((int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 模拟触发
        /// </summary>
        public void TrrigerRobot()
        {
            try
            {
                string str = ParLogicRobot.P_I.strTrrigerRobot;
                TrrigerRobot(str);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 线程触发

        #region IO
        void TrrigerInput(object o)
        {
            try
            {
                string[] temp = o.ToString().Split(',');
                int[] cmd = new int[temp.Length - 1];
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    cmd[i] = Convert.ToInt32(temp[i + 1]);
                }
                Input_event(cmd);
            }
            catch (Exception ex)
            {

            }

        }

        void TrrigerOutput(object o)
        {
            try
            {
                string[] temp = o.ToString().Split(',');
                int[] value = new int[temp.Length - 1];
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    value[i] = Convert.ToInt32(temp[i + 1]);
                }
                Output_event(value);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion IO

        #region Jog
        void TrrigerJogPosCurr(object o)
        {
            try
            {
                string[] temp = o.ToString().Split(',');
                double[] value = new double[temp.Length - 1];
                for (int i = 0; i < temp.Length - 1; i++)
                {
                    value[i] = Convert.ToDouble(temp[i + 1].Trim());
                }
                JogPosCurr_event(value);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        //等待机器人返回数据接收OK信号
        public bool WaitForOK()
        {
            try
            {
                int i = 0;
                while (!ParLogicRobot.P_I.isReadOK)
                {
                    //等待1s
                    if (i < thWaitTime_m)
                    {
                        i++;
                    }
                    else
                    {
                        return false;
                    }
                    Thread.Sleep(50);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 读取数据

        #region 监控通信
        void Monitor_Task()
        {
            try
            {
                new Task(new Action(Monitor)).Start();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void Monitor()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 监控通信
    }
}
