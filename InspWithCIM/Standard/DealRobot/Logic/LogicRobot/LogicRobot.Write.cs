using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Threading.Tasks;
using System.Threading;
using DealFile;
using BasicClass;
using DealConfigFile;

namespace DealRobot
{
    partial class LogicRobot
    {
        #region 定义
        public string Head
        {
            get
            {
                string head = "";
                if (ParSetRobot.P_I.TypeRobot_e == TypeRobot_enum.YAMAH_Ethernet
                    || ParSetRobot.P_I.TypeRobot_e == TypeRobot_enum.YAMAH_Serial)
                {
                    head = "P10= ";
                }
                return head;
            }
        }

        //List
        public List<string> ParRobotCom_L = new List<string>();//公共参数
        public List<string> ParRobot1_L = new List<string>();//工位1参数
        public List<string> ParRobot2_L = new List<string>();//工位2参数
        public List<string> ParRobot3_L = new List<string>();//工位3参数
        public List<string> ParRobot4_L = new List<string>();//工位4参数

        public List<Point4D> ParRobotCom_P4L = new List<Point4D>();//公共参数
        public List<Point4D> ParRobot1_P4L = new List<Point4D>();//工位1参数
        public List<Point4D> ParRobot2_P4L = new List<Point4D>();//工位2参数
        public List<Point4D> ParRobot3_P4L = new List<Point4D>();//工位3参数
        public List<Point4D> ParRobot4_P4L = new List<Point4D>();//工位4参数

        //事件
        public event StrBlAction ConfigRobot_event;

        Mutex g_MtWrite = new Mutex();
        #endregion 定义

        #region 机器人握手
        /// <summary>
        /// 机器人握手
        /// </summary>
        public void RobotShake()
        {
            try
            {
                string value = Head
                      + "0.0".PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + "10000".PadLeft(7, ' ');
                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }
        #endregion 机器人握手

        #region 机器人配置参数
        /// <summary>
        /// 向机器人发送配置参数
        /// </summary>
        /// <param name="p4_L"></param>
        /// <returns></returns>
        public void WriteConfigRobot()
        {
            try
            {
                #region 公共参数
                for (int i = 0; i < ParRobotCom_L.Count; i++)
                {
                    WriteParCom(ParRobotCom_L[i], i);
                    if (!WaitForOK())
                    {
                        ConfigRobot_event(string.Format("PC向机器人发送公共参数{0}失败!", ParLogicRobot.c_CmdParCom + i.ToString()), false);
                        return;
                    }
                    ConfigRobot_event(string.Format("PC向机器人发送公共参数{0}成功!", ParLogicRobot.c_CmdParCom + i.ToString()), true);
                }

                for (int i = 0; i < ParRobotCom_P4L.Count; i++)
                {
                    int j = i + ParRobotCom_L.Count;
                    WriteParCom(ParRobotCom_P4L[i], j);
                    if (!WaitForOK())
                    {
                        ConfigRobot_event(string.Format("PC向机器人发送公共参数{0}失败!", ParLogicRobot.c_CmdParCom + j.ToString()), false);
                        return;
                    }
                    ConfigRobot_event(string.Format("PC向机器人发送公共参数{0}成功!", ParLogicRobot.c_CmdParCom + j.ToString()), true);
                }
                #endregion 公共参数

                #region 工位1参数
                for (int i = 0; i < ParRobot1_L.Count; i++)
                {
                    WritePar1(ParRobot1_L[i], i);
                    if (!WaitForOK())
                    {
                        ConfigRobot_event(string.Format("PC向机器人发送工位1参数{0}失败!", ParLogicRobot.c_CmdPar1 + i.ToString()), false);
                        return;
                    }
                    ConfigRobot_event(string.Format("PC向机器人发送工位1参数{0}成功!", ParLogicRobot.c_CmdPar1 + i.ToString()), true);
                }
                for (int i = 0; i < ParRobot1_P4L.Count; i++)
                {
                    int j = i + ParRobot1_L.Count;
                    WritePar1(ParRobot1_P4L[i], j);
                    if (!WaitForOK())
                    {
                        ConfigRobot_event(string.Format("PC向机器人发送工位1参数{0}失败!", ParLogicRobot.c_CmdPar1 + j.ToString()), false);
                        return;
                    }
                    ConfigRobot_event(string.Format("PC向机器人发送工位1参数{0}成功!", ParLogicRobot.c_CmdPar1 + j.ToString()), true);
                }
                #endregion 工位1参数

                #region 工位2参数
                for (int i = 0; i < ParRobot2_L.Count; i++)
                {
                    WritePar2(ParRobot2_L[i], i);
                    if (!WaitForOK())
                    {
                        ConfigRobot_event(string.Format("PC向机器人发送工位2参数{0}失败!", ParLogicRobot.c_CmdPar2 + i.ToString()), false);
                        return;
                    }
                    ConfigRobot_event(string.Format("PC向机器人发送工位2参数{0}成功!", ParLogicRobot.c_CmdPar2 + i.ToString()), true);
                }
                for (int i = 0; i < ParRobot2_P4L.Count; i++)
                {
                    int j = i + ParRobot2_L.Count;
                    WritePar2(ParRobot2_P4L[i], j);
                    if (!WaitForOK())
                    {
                        ConfigRobot_event(string.Format("PC向机器人发送工位2参数{0}失败!", ParLogicRobot.c_CmdPar2 + j.ToString()), false);
                        return;
                    }
                    ConfigRobot_event(string.Format("PC向机器人发送工位2参数{0}成功!", ParLogicRobot.c_CmdPar2 + j.ToString()), true);
                }
                #endregion 工位2参数

                #region 工位3参数
                for (int i = 0; i < ParRobot3_L.Count; i++)
                {
                    WritePar3(ParRobot3_L[i], i);
                    if (!WaitForOK())
                    {
                        ConfigRobot_event(string.Format("PC向机器人发送工位3参数{0}失败!", ParLogicRobot.c_CmdPar3 + i.ToString()), false);
                        return;
                    }
                    ConfigRobot_event(string.Format("PC向机器人发送工位3参数{0}成功!", ParLogicRobot.c_CmdPar3 + i.ToString()), true);
                }
                for (int i = 0; i < ParRobot3_P4L.Count; i++)
                {
                    int j = i + ParRobot3_L.Count;
                    WritePar3(ParRobot3_P4L[i], j);
                    if (!WaitForOK())
                    {
                        ConfigRobot_event(string.Format("PC向机器人发送工位3参数{0}失败!", ParLogicRobot.c_CmdPar3 + j.ToString()), false);
                        return;
                    }
                    ConfigRobot_event(string.Format("PC向机器人发送工位3参数{0}成功!", ParLogicRobot.c_CmdPar3 + j.ToString()), true);
                }
                #endregion 工位3参数

                #region 工位4参数
                for (int i = 0; i < ParRobot4_L.Count; i++)
                {
                    WritePar4(ParRobot4_L[i], i);
                    if (!WaitForOK())
                    {
                        ConfigRobot_event(string.Format("PC向机器人发送工位4参数{0}失败!", ParLogicRobot.c_CmdPar4 + i.ToString()), false);
                        return;
                    }
                    ConfigRobot_event(string.Format("PC向机器人发送工位4参数{0}成功!", ParLogicRobot.c_CmdPar4 + i.ToString()), true);
                }

                for (int i = 0; i < ParRobot4_P4L.Count; i++)
                {
                    int j = i + ParRobot4_L.Count;
                    WritePar4(ParRobot4_P4L[i], j);
                    if (!WaitForOK())
                    {
                        ConfigRobot_event(string.Format("PC向机器人发送工位4参数{0}失败!", ParLogicRobot.c_CmdPar4 + j.ToString()), false);
                        return;
                    }
                    ConfigRobot_event(string.Format("PC向机器人发送工位4参数{0}成功!", ParLogicRobot.c_CmdPar4 + j.ToString()), true);
                }
                #endregion 工位4参数
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        /// <summary>
        /// 配置参数
        /// </summary>
        /// <param name="par"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        bool WriteParCom(string par, int index)
        {
            try
            {
                string value = Head
                      + par.PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + ParLogicRobot.c_CmdParCom + index.ToString().PadLeft(7, ' ');

                WriteRobot(value);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
                return false;
            }
        }
        bool WriteParCom(Point4D point, int index)
        {
            try
            {
                string value = Head
                      + point.DblValue1.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue2.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue3.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue4.ToString().PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + ParLogicRobot.c_CmdParCom + index.ToString().PadLeft(7, ' ');

                WriteRobot(value);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
                return false;
            }
        }

        /// <summary>
        /// 工位1参数
        /// </summary>
        /// <param name="par"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        bool WritePar1(string par, int index)
        {
            try
            {
                string value = Head
                      + par.PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + ParLogicRobot.c_CmdPar1 + index.ToString().PadLeft(7, ' ');

                WriteRobot(value);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
                return false;
            }
        }
        bool WritePar1(Point4D point, int index)
        {
            try
            {
                string value = Head
                      + point.DblValue1.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue2.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue3.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue4.ToString().PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + ParLogicRobot.c_CmdPar1 + index.ToString().PadLeft(7, ' ');

                WriteRobot(value);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
                return false;
            }
        }
        /// <summary>
        /// 工位2参数
        /// </summary>
        /// <param name="par"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        bool WritePar2(string par, int index)
        {
            try
            {
                string value = Head
                      + par.PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + ParLogicRobot.c_CmdPar2 + index.ToString().PadLeft(7, ' ');

                WriteRobot(value);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
                return false;
            }
        }
        bool WritePar2(Point4D point, int index)
        {
            try
            {
                string value = Head
                      + point.DblValue1.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue2.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue3.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue4.ToString().PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + ParLogicRobot.c_CmdPar2 + index.ToString().PadLeft(7, ' ');

                WriteRobot(value);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
                return false;
            }
        }

        bool WritePar3(string par, int index)
        {
            try
            {
                string value = Head
                      + par.PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + ParLogicRobot.c_CmdPar3 + index.ToString().PadLeft(7, ' ');

                WriteRobot(value);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
                return false;
            }
        }
        bool WritePar3(Point4D point, int index)
        {
            try
            {
                string value = Head
                      + point.DblValue1.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue2.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue3.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue4.ToString().PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + ParLogicRobot.c_CmdPar3 + index.ToString().PadLeft(7, ' ');

                WriteRobot(value);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
                return false;
            }
        }

        /// <summary>
        /// 工位4参数
        /// </summary>
        /// <param name="par"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        bool WritePar4(string par, int index)
        {
            try
            {
                string value = Head
                      + par.PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + ParLogicRobot.c_CmdPar4 + index.ToString().PadLeft(7, ' ');

                WriteRobot(value);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
                return false;
            }
        }

        bool WritePar4(Point4D point, int index)
        {
            try
            {
                string value = Head
                      + point.DblValue1.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue2.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue3.ToString().PadLeft(7, ' ') + " "
                      + point.DblValue4.ToString().PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + ParLogicRobot.c_CmdPar4 + index.ToString().PadLeft(7, ' ');

                WriteRobot(value);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
                return false;
            }
        }
        #endregion 机器人配置参数

        #region 写入机器人数据
        void WriteRobot(string value)
        {
            ParLogicRobot.P_I.isReadOK = false;
            if (RegeditRobot.R_I.BlOffLineRobot)
            {
                return;
            }
            g_MtWrite.WaitOne();
            try
            {
                g_PortRobotBase.WriteData(value);
            }
            catch (Exception ex)
            {

            }
            g_MtWrite.ReleaseMutex();
        }

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="cmd">命令</param>
        public void WriteRobotCMD(string cmd)
        {
            try
            {
                string value = Head
                             + "0.0".PadLeft(7, ' ') + " "
                             + "0.0".PadLeft(7, ' ') + " "
                             + "0.0".PadLeft(7, ' ') + " "
                             + "0.0".PadLeft(7, ' ') + " "
                             + "0.0".PadLeft(7, ' ') + " "
                             + cmd.PadLeft(7, ' ');
                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }
        /// <summary>
        /// 发送参数+命令
        /// </summary>
        /// <param name="par">参数</param>
        /// <param name="cmd">命令</param>
        public void WriteRobotCMD(string par, string cmd)
        {
            try
            {
                string value = Head
                             + par.PadLeft(7, ' ') + " "
                             + "0.0".PadLeft(7, ' ') + " "
                             + "0.0".PadLeft(7, ' ') + " "
                             + "0.0".PadLeft(7, ' ') + " "
                             + "0.0".PadLeft(7, ' ') + " "
                             + cmd.PadLeft(7, ' ');
                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }
        /// <summary>
        /// 发送参数1+参数2+命令
        /// </summary>
        /// <param name="par1">参数1</param>
        /// <param name="par2">参数2</param>
        /// <param name="cmd">命令</param>
        public void WriteRobotCMD(string par1, string par2, string cmd)
        {
            try
            {
                string value = Head
                             + par1.PadLeft(7, ' ') + " "
                             + par2.PadLeft(7, ' ') + " "
                             + "0.0".PadLeft(7, ' ') + " "
                             + "0.0".PadLeft(7, ' ') + " "
                             + "0.0".PadLeft(7, ' ') + " "
                             + cmd.PadLeft(7, ' ');
                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }

        }

        /// <summary>
        /// 发送一个点位加命令
        /// </summary>
        /// <param name="point">点数据</param>
        /// <param name="cmd">命令</param>
        public void WriteRobotCMD(Point4D point, string cmd)
        {
            try
            {
                string value = Head
                    + point.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                    + "0".PadLeft(7, ' ') + " "
                    + cmd.PadLeft(7, ' ');
                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }

        }
        #endregion 写入机器人数据

        #region 机器人示教
        #region RobotCtr
        public void ResetRobot()
        {
            try
            {
                string value = Head
                      + "0.0".PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + "1000".PadLeft(7, ' ');

                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }

        /// <summary>
        /// 空跑,false为退出空跑
        /// </summary>
        public void RobotNullRun(bool blType)
        {
            try
            {
                string cmd = "10002";
                if (blType)
                {
                    cmd = "10001";
                }
                string value = Head
                      + "0.0".PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + cmd.PadLeft(7, ' ');

                WriteRobot(value);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion RobotCtr

        #region IO
        #region Input
        public bool ReadIn()
        {
            try
            {
                string value = Head
                      + "0.0".PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".ToString().PadLeft(7, ' ') + " "
                      + "0.0".PadLeft(7, ' ') + " "
                      + "3000".PadLeft(7, ' ');

                WriteRobot(value);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
                return false;
            }
        }
        #endregion Input

        #region Output

        #endregion Output
        #endregion IO
        #endregion 机器人示教
    }
}
