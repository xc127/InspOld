using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DealFile;
using BasicClass;
using DealRobot;
using DealConfigFile;
using DealComInterface;

namespace DealRobot
{
    public partial class LogicRobot
    {
        public void WriteStdData(int noPos,Point4D point)
        {
            try
            {                
                string cmd = NoCamera.ToString() + "1";//命令

                string value = "PData".PadLeft(7, ' ') + " "
                    + noPos.ToString().PadLeft(7, ' ') + " "
                    + point.DblValue1.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue2.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue3.ToString("f2").PadLeft(7, ' ') + " "
                    + point.DblValue4.ToString("f2").PadLeft(7, ' ') + " "
                    + "0".PadLeft(7, ' ') + " "
                    + "0".PadLeft(7, ' ');

                WriteRobot(value);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }


        #region 配置参数

        public void WriteConfigParStdRobot()
        {
            try
            {
                foreach (RobotStdPoint item in RobotStdPoint.R_I.RobotStdPoint_L)
                {
                    string pos = "Config "+item.NamePoint;
                    //写入输入
                    if (!WriteInput(pos, item))
                    {
                        return;
                    }
                    //写入运动
                    if (!WriteMove(pos, item))
                    {
                        return;
                    }
                    //写入输出
                    if (!WriteOut(pos, item))
                    {
                        return;
                    }
                    //写入点位综合
                    if (!WritePrint(pos, item))
                    {
                        return;
                    }
                    //配置完成
                    if (!WriteFinish())
                    {
                        return ;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }


        public void WriteConfigParStdRobot(object index)
        {
            try
            {
                RobotStdPoint item = RobotStdPoint.R_I.RobotStdPoint_L[(int)index];

                string pos = "Config " + item.NamePoint;
                //写入输入
                if (!WriteInput(pos, item))
                {
                    return;
                }
                //写入运动
                if (!WriteMove(pos, item))
                {
                    return;
                }
                //写入输出
                if (!WriteOut(pos, item))
                {
                    return;
                }
                //写入点位综合
                if (!WritePrint(pos, item))
                {
                    return;
                }
                
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicRobot", ex);
            }
        }
        public bool WriteInput(string name,RobotStdPoint robotStdPoint)
        {
            try
            {
                int waitV = (int)robotStdPoint.P_Vaccum.DblValue2;
                if (waitV == -1)
                {
                    waitV = int.MaxValue;
                }

                string vaccum = name + " vaccum" + " "
                    + robotStdPoint.P_Vaccum.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + waitV.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Vaccum.DblValue3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Vaccum.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(vaccum);

                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", vaccum), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", vaccum), true);


                #region InputIO
                int waitIO1 = (int)robotStdPoint.P_InputIO1.DblValue3;
                if (waitIO1 == -1)
                {
                    waitIO1 = int.MaxValue;
                }
                string inputIO1 = name + " inputIO1" + " "
                    + robotStdPoint.P_InputIO1.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_InputIO1.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + waitIO1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_InputIO1.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(inputIO1);

                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", inputIO1), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", inputIO1), true);


                string jumpInputIO1 = name + " jumpIOP1" + " "
                   + robotStdPoint.P_JumpIOP1.DblValue1.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_JumpIOP1.DblValue2.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_JumpIOP1.DblValue3.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_JumpIOP1.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(jumpInputIO1);

                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", jumpInputIO1), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", jumpInputIO1), true);


                int waitIO2 = (int)robotStdPoint.P_InputIO2.DblValue3;
                if (waitIO2 == -1)
                {
                    waitIO2 = int.MaxValue;
                }
                string inputIO2 = name + " inputIO2" + " "
                    + robotStdPoint.P_InputIO2.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_InputIO2.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + waitIO2.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_InputIO2.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(inputIO2);

                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", inputIO2), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", inputIO2), true);

                string jumpInputIO2 = name + " jumpIOP2" + " "
                   + robotStdPoint.P_JumpIOP2.DblValue1.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_JumpIOP2.DblValue2.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_JumpIOP2.DblValue3.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_JumpIOP2.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(jumpInputIO2);

                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", jumpInputIO2), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", jumpInputIO2), true);


                int waitIO3 = (int)robotStdPoint.P_InputIO3.DblValue3;
                if (waitIO3 == -1)
                {
                    waitIO3 = int.MaxValue;
                }
                string inputIO3 = name + " inputIO3" + " "
                    + robotStdPoint.P_InputIO3.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_InputIO3.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + waitIO3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_InputIO3.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(inputIO3);

                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", inputIO3), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", inputIO3), true);

                string jumpInputIO3 = name + " jumpIOP3" + " "
                   + robotStdPoint.P_JumpIOP3.DblValue1.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_JumpIOP3.DblValue2.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_JumpIOP3.DblValue3.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_JumpIOP3.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(jumpInputIO3);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", jumpInputIO3), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", jumpInputIO3), true);


                int waitIO4 = (int)robotStdPoint.P_InputIO4.DblValue3;
                if (waitIO4 == -1)
                {
                    waitIO4 = int.MaxValue;
                }
                string inputIO4 = name + " inputIO4" + " "
                    + robotStdPoint.P_InputIO4.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_InputIO4.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + waitIO4.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_InputIO4.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(inputIO4);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", vaccum), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", vaccum), true);

                string jumpInputIO4 = name + " jumpIOP4" + " "
                   + robotStdPoint.P_JumpIOP4.DblValue1.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_JumpIOP4.DblValue2.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_JumpIOP4.DblValue3.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_JumpIOP4.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(jumpInputIO4);

                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", jumpInputIO4), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", jumpInputIO4), true);
                #endregion InputIO

                #region PC

                string pc = name + " pc" + " "
                    + robotStdPoint.P_PC.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_PC.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_PC.DblValue3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_PC.DblValue4.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_PC.DblValue5.ToString().PadLeft(7, ' ');
                WriteRobot(pc);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", pc), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", pc), true);


                int waitPC = (int)robotStdPoint.P_PCCMD.DblValue2;
                if (waitPC == -1)
                {
                    waitPC = int.MaxValue;
                }
                string pcCMD = name + " pcCMD" + " "
                    + robotStdPoint.P_PCCMD.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + waitPC.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_PCCMD.DblValue3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_PCCMD.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(pcCMD);

                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", pcCMD), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", pcCMD), true);
                #endregion PC
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool WriteMove(string name, RobotStdPoint robotStdPoint)
        {
            try
            {
                string vel = name + " vel" + " "
                    + robotStdPoint.P_Vel.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Vel.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Vel.DblValue3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Vel.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(vel);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", vel), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", vel), true);

                string move = name + " move" + " "
                    + robotStdPoint.P_Move.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Move.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Move.DblValue3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Move.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(move);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", move), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", move), true);

                string des = name + " des" + " "
                   + robotStdPoint.P_Des.DblValue1.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_Des.DblValue2.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_Des.DblValue3.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_Des.DblValue4.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_Des.DblValue5.ToString().PadLeft(7, ' ');
                WriteRobot(des);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", des), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", des), true);

                string here = name + " here" + " "
                    + robotStdPoint.P_Here.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Here.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Here.DblValue3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Here.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(here);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", here), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", here), true);

                string passCMD = name + " passCMD" + " "
                   + robotStdPoint.P_PassCMD.DblValue1.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_PassCMD.DblValue2.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_PassCMD.DblValue3.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_PassCMD.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(passCMD);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", passCMD), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", passCMD), true);

                string pass1 = name + " pass1" + " "
                    + robotStdPoint.P_Pass1.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Pass1.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Pass1.DblValue3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Pass1.DblValue4.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Pass1.DblValue5.ToString().PadLeft(7, ' ');
                WriteRobot(pass1);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", pass1), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", pass1), true);

                string pass2 = name + " pass2" + " "
                   + robotStdPoint.P_Pass2.DblValue1.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_Pass2.DblValue2.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_Pass2.DblValue3.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_Pass2.DblValue4.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_Pass2.DblValue5.ToString().PadLeft(7, ' ');
                WriteRobot(pass2);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", pass2), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", pass2), true);

                string pass3 = name + " pass3" + " "
                    + robotStdPoint.P_Pass3.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Pass3.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Pass3.DblValue3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Pass3.DblValue4.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Pass3.DblValue5.ToString().PadLeft(7, ' ');
                WriteRobot(pass3);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", pass3), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", pass3), true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool WriteOut(string name, RobotStdPoint robotStdPoint)
        {
            try
            {
                string outIO = name + " outIO" + " "
                    + robotStdPoint.P_OutIO.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_OutIO.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_OutIO.DblValue3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_OutIO.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(outIO);
 
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", outIO), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", outIO), true);

                string outVaccum = name + " outVaccum" + " "
                    + robotStdPoint.P_OutVaccum.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_OutVaccum.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_OutVaccum.DblValue3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_OutVaccum.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(outVaccum);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", outVaccum), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", outVaccum), true);

                string outPC = name + " outPC" + " "
                   + robotStdPoint.P_OutPC.DblValue1.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_OutPC.DblValue2.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_OutPC.DblValue3.ToString().PadLeft(7, ' ') + " "
                   + robotStdPoint.P_OutPC.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(outPC);
          
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", outPC), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", outPC), true);

                string nextPos = name + " nextPos" + " "
                    + robotStdPoint.P_NextPoint.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_NextPoint.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_NextPoint.DblValue3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_NextPoint.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(nextPos);

                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", nextPos), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", nextPos), true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool WritePrint(string name, RobotStdPoint robotStdPoint)
        {
            try
            {
                string print = name + " print" + " "
                    + robotStdPoint.P_Print.DblValue1.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Print.DblValue2.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Print.DblValue3.ToString().PadLeft(7, ' ') + " "
                    + robotStdPoint.P_Print.DblValue4.ToString().PadLeft(7, ' ');
                WriteRobot(print);
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数{0}失败!", print), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数{0}成功!", print), true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 配置完成
        /// </summary>
        public bool WriteFinish()
        {
            try
            {
                WriteRobot("Config finish");
                if (!WaitForOK())
                {
                    ConfigRobot_event(string.Format("PC向机器人发送参数finish失败!"), false);
                    return false;
                }
                ConfigRobot_event(string.Format("PC向机器人发送参数finish成功!"), true);
                return true;
            }
            catch (Exception ex)
            {
                return false;;
            }
          
        }
        #endregion 配置参数
    }
}
