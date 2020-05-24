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
        #region 定义事件
        public event TrrigerSourceSNSAction_del Camera1_Std_event;
        public event TrrigerSourceSNSAction_del Camera2_Std_event;
        public event TrrigerSourceSNSAction_del Camera3_Std_event;
        public event TrrigerSourceSNSAction_del Camera4_Std_event;
        public event TrrigerSourceSNSAction_del Camera5_Std_event;
        public event TrrigerSourceSNSAction_del Camera6_Std_event;
        #endregion 定义事件

        /// <summary>
        /// 读取机器人指令
        /// </summary>
        /// <param name="str"></param>
        public void TrrigerRobotStd(string str)
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
                //如果机器人指令是触发相机
                if (Head.Contains("Cam"))
                {
                    int cam = int.Parse(cmd[1]);
                    int num = int.Parse(cmd[2]);
                    switch (cam)
                    {
                        case 1:
                            new Task(TrrigerCamera1, num).Start();
                            break;

                        case 2:
                            new Task(TrrigerCamera2, num).Start();
                            break;

                        case 3:
                            new Task(TrrigerCamera3, num).Start();
                            break;

                        case 4:
                            new Task(TrrigerCamera4, num).Start();
                            break;

                        case 5:
                            new Task(TrrigerCamera5, num).Start();
                            break;

                        case 6:
                            new Task(TrrigerCamera6, num).Start();
                            break;
                    }                   
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }


        #region 线程触发
        /// <summary>
        /// 相机1触发
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerCamera1(object[] value)
        {
            try
            {
                string[] cmd = new string[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    cmd[i] = value[i].ToString();
                }
                Camera1_Std_event(TriggerSource_enum.Robot, cmd);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        /// <summary>
        /// 相机2触发
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerCamera2(object[] value)
        {
            try
            {
                string[] cmd = new string[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    cmd[i] = value[i].ToString();
                }
                Camera2_Std_event(TriggerSource_enum.Robot, cmd);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        /// <summary>
        /// 相机3触发
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerCamera3(object[] value)
        {
            try
            {
                string[] cmd = new string[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    cmd[i] = value[i].ToString();
                }
                Camera3_Std_event(TriggerSource_enum.Robot, cmd);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        /// <summary>
        /// 相机4触发
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerCamera4(object[] value)
        {
            try
            {
                string[] cmd = new string[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    cmd[i] = value[i].ToString();
                }
                Camera4_Std_event(TriggerSource_enum.Robot, cmd);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        /// <summary>
        /// 相机5触发
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerCamera5(object[] value)
        {
            try
            {
                string[] cmd = new string[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    cmd[i] = value[i].ToString();
                }
                Camera5_Std_event(TriggerSource_enum.Robot, cmd);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        /// <summary>
        /// 相机6触发
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerCamera6(object[] value)
        {
            try
            {
                string[] cmd = new string[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    cmd[i] = value[i].ToString();
                }
                Camera6_Std_event(TriggerSource_enum.Robot, cmd);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        /// <summary>
        /// 其他
        /// </summary>
        /// <param name="intValue"></param>
        void TrrigerOthers(object[] intValue)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
        }
        
        #endregion 线程触发
    }
}
