using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;
using DealFile;
using System.IO;
using System.Xml;

namespace DealRobot
{
    partial class RobotStdPoint
    {
        #region 读取
        public void ReadXml()
        {
            try
            {
                XmlDocument xDoc = LoadXml(PathRobotStd);
                XmlElement xeRoot = null;
                if (xDoc != null)
                {
                    xeRoot = ReadNode(xDoc, "StdPoints");                    
                }

                RobotStdPoint robotStdPointHome = new RobotStdPoint();
                robotStdPointHome.NamePoint = "home";
                RobotStdPoint_L.Add(robotStdPointHome);
                if (xeRoot == null)
                {                   
                    return;
                }

                XmlElement xeHome = ReadNode(xeRoot, "home");
                ReadInput(xeHome, robotStdPointHome);
                ReadMove(xeHome, robotStdPointHome);
                ReadOut(xeHome, robotStdPointHome);

               
                NumPoint = ReadAttributeInt(xeRoot, "Num", 0);
                for (int i = 0; i < NumPoint; i++)
                {
                    RobotStdPoint robotStdPoint = new RobotStdPoint();

                    robotStdPoint.No = i + 1;
                    robotStdPoint.NamePoint = "pos" + i.ToString();

                    XmlElement xePos = ReadNode(xeRoot, "pos" + i.ToString());

                    ReadInput(xePos, robotStdPoint);//读取输入
                    ReadMove(xePos, robotStdPoint);//读取运动
                    ReadOut(xePos, robotStdPoint);//读取输出
                    ReadNextPos(xePos, robotStdPoint);//下一个带点
                    ReadAnnotation(xePos, robotStdPoint);
                    RobotStdPoint_L.Add(robotStdPoint);
                }
                //打印输出
                ReadPrint(xeRoot);
            }
            catch (Exception ex)
            {

            }
        }

        #region 输入
        void ReadInput(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {                
                //真空
                robotStdPoint.P_Vaccum = ReadVaccum(xeRoot);

                //输入IO
                Point4D[] points_Input1 = ReadInputIO(xeRoot, "InputIO1");
                robotStdPoint.P_InputIO1 = points_Input1[0];
                robotStdPoint.P_JumpIOP1 = points_Input1[1];

                Point4D[] points_Input2 = ReadInputIO(xeRoot, "InputIO2");
                robotStdPoint.P_InputIO2 = points_Input2[0];
                robotStdPoint.P_JumpIOP2 = points_Input2[1];

                Point4D[] points_Input3 = ReadInputIO(xeRoot, "InputIO3");
                robotStdPoint.P_InputIO3 = points_Input3[0];
                robotStdPoint.P_JumpIOP3 = points_Input3[1];

                Point4D[] points_Input4 = ReadInputIO(xeRoot, "InputIO4");
                robotStdPoint.P_InputIO4 = points_Input4[0];
                robotStdPoint.P_JumpIOP4 = points_Input4[1];

                //PC
                ReadPC(xeRoot, robotStdPoint);
            }
            catch (Exception ex)
            {

            }
        }

        Point4D ReadVaccum(XmlElement xeRoot)
        {
            try
            {
                Point4D point = new Point4D();

                bool enable_Vaccum = ReadChildAttributeBl(xeRoot, "Vaccum", "Enable");
                int no_Vaccum = ReadChildAttributeInt(xeRoot, "Vaccum", "NoIO");
                no_Vaccum = 8;
                int waitTime_Vaccum = ReadChildAttributeInt(xeRoot, "Vaccum", "WaitTime");
                int nGIO_Vaccum = ReadChildAttributeInt(xeRoot, "Vaccum", "NGIO");
                int nGPos_Vaccum = ReadChildAttributeInt(xeRoot, "Vaccum", "NGPos");

                point.NameClass = "Vaccum";
                if (enable_Vaccum)
                {
                    point.DblValue1 = no_Vaccum;
                }
                else
                {
                    point.DblValue1 = -1;
                }
                point.DblValue2 = waitTime_Vaccum;//等待时间
                point.DblValue3 = nGIO_Vaccum;//NGIO
                point.DblValue4 = nGPos_Vaccum;//NG跳转点位
                return point;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        Point4D[] ReadInputIO(XmlElement xeRoot, string name)
        {
            try
            {

                Point4D p_InputIO = new Point4D();
                Point4D p_JumpP = new Point4D();

                bool enable = ReadChildAttributeBl(xeRoot, name, "Enable");
                p_InputIO.DblValue1 = enable ? 1 : -1;

                p_InputIO.DblValue2 = ReadChildAttributeInt(xeRoot, name, "NoIO");
                p_InputIO.DblValue3 = ReadChildAttributeInt(xeRoot, name, "WaitTime");
                p_InputIO.DblValue4 = ReadChildAttributeInt(xeRoot, name, "NGIO");
                p_InputIO.Annotation = ReadChildAttributeStr(xeRoot, name, "Annotation");
                p_InputIO.NameClass = name;


                p_JumpP.DblValue1 = ReadChildAttributeInt(xeRoot, name, "OKPos");
                p_JumpP.DblValue2 = ReadChildAttributeInt(xeRoot, name, "NGPos");

                Point4D[] points = new Point4D[2] { p_InputIO, p_JumpP };

                return points;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 读取PC
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="robotStdPoint"></param>
        void ReadPC(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                //PC
                XmlElement xePC = ReadNode(xeRoot, "PC");

                bool enable_PC = ReadAttributeBl(xePC, "Enable");
                int waitTime_PC = ReadAttributeInt(xePC, "WaitTime");
                int nGIO_PC = ReadAttributeInt(xePC, "NGIO");

                int nGPos_PC = ReadAttributeInt(xePC, "NGPos");
                string annotation_PC = ReadAttributeStr(xePC, "Annotation");

                if (enable_PC)
                {
                    robotStdPoint.P_PC.DblValue1 = 1;
                }
                else
                {
                    robotStdPoint.P_PC.DblValue1 = -1;
                }
                robotStdPoint.P_PC.DblValue2 = waitTime_PC;
                robotStdPoint.P_PC.DblValue3 = nGIO_PC;
                robotStdPoint.P_PC.DblValue4 = nGPos_PC;

                robotStdPoint.P_PC.Annotation = annotation_PC;

                //PCCMD
                XmlElement xePCCMD = ReadNode(xeRoot, "PCCMD");

                robotStdPoint.P_PCCMD.DblValue1  = ReadAttributeInt(xePCCMD, "XChange");
                robotStdPoint.P_PCCMD.DblValue2 = ReadAttributeInt(xePCCMD, "YChange");
                robotStdPoint.P_PCCMD.DblValue3 = ReadAttributeInt(xePCCMD, "ZChange");
                robotStdPoint.P_PCCMD.DblValue4 = ReadAttributeInt(xePCCMD, "RChange");
                robotStdPoint.P_PCCMD.DblValue5 = ReadAttributeInt(xePCCMD, "ArmChange");
                string annotation_PCCMD = ReadAttributeStr(xePCCMD, "Annotation");


                robotStdPoint.P_PCCMD.Annotation = annotation_PCCMD;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 输入

        #region 运动
        void ReadMove(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                ReadVel(xeRoot, robotStdPoint);

                ReadMovePos(xeRoot, robotStdPoint);
            }
            catch (Exception ex)
            {

            }
        }

        void ReadVel(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {               
                bool enable = ReadChildAttributeBl(xeRoot, "Vel", "Enable");
                robotStdPoint.P_Vel.DblValue1 = enable ? 1 : -1;

                string power = ReadChildAttributeStr(xeRoot, "Vel", "Power", "Low");

                int vel = ReadChildAttributeInt(xeRoot, "Vel", "Vel");
                int acc = ReadChildAttributeInt(xeRoot, "Vel", "Acc");
                string annotation = ReadChildAttributeStr(xeRoot, "Vel", "Annotation");

                robotStdPoint.P_Vel.NameClass = "Vel";               
                
                if (power == PowerRobot_enum.High.ToString())
                {
                    robotStdPoint.P_Vel.DblValue2 = 2;//高功率
                }
                else
                {
                    robotStdPoint.P_Vel.DblValue2 = 1;//低功率
                }

                robotStdPoint.P_Vel.DblValue3 = vel;//速度
                robotStdPoint.P_Vel.DblValue4 = acc;//加速度
            }
            catch (Exception ex)
            {

            }
        }

        void ReadMovePos(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                string typeMove = ReadChildAttributeStr(xeRoot, "Move", "Type", "Stay");
                MoveRobot_enum moveRobot_e = (MoveRobot_enum)Enum.Parse(typeof(MoveRobot_enum), typeMove);
                robotStdPoint.P_Move.DblValue1 = (int)moveRobot_e+1;

                robotStdPoint.P_PassCMD.DblValue1 = ReadChildAttributeInt(xeRoot, "PassCMD", "Num", 0);
                string typeZ = ReadChildAttributeStr(xeRoot, "PassCMD", "Z", "Relative");
                if (typeZ == TypePosRobot_enum.Relative.ToString())
                {
                    robotStdPoint.P_PassCMD.DblValue2 = 1;
                }
                else
                {
                    robotStdPoint.P_PassCMD.DblValue2 = 2;
                }

                string typeR = ReadChildAttributeStr(xeRoot, "PassCMD", "R", "Relative");
                if (typeR == TypePosRobot_enum.Relative.ToString())
                {
                    robotStdPoint.P_PassCMD.DblValue3 = 1;
                }
                else
                {
                    robotStdPoint.P_PassCMD.DblValue3 = 2;
                }

                robotStdPoint.P_Des = ReadMovePos(xeRoot, "Des");
                robotStdPoint.P_Here = ReadMovePos(xeRoot, "Here");

                robotStdPoint.P_Pass1 = ReadMovePos(xeRoot, "Pass1");
                robotStdPoint.P_Pass2 = ReadMovePos(xeRoot, "Pass2");
                robotStdPoint.P_Pass3 = ReadMovePos(xeRoot, "Pass3");
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 读取Move点位
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="robotStdPoint"></param>
        Point5D ReadMovePos(XmlElement xeRoot, string name)
        {
            try
            {
                Point5D p = new Point5D();
                p.DblValue1 = ReadChildAttributeDbl(xeRoot, name, "X");
                p.DblValue2 = ReadChildAttributeDbl(xeRoot, name, "Y");
                p.DblValue3 = ReadChildAttributeDbl(xeRoot, name, "Z");
                p.DblValue4 = ReadChildAttributeDbl(xeRoot, name, "R");
                string annotation = ReadChildAttributeStr(xeRoot, name, "Annotation");
                p.Annotation = annotation;

                string arm = ReadChildAttributeStr(xeRoot, name, "Arm", "Left");
                if (arm == ArmRobot_enum.Left.ToString())
                {
                    p.DblValue5 = 1;
                }
                else
                {
                    p.DblValue5 = 2;
                }
                return p;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion 运动

        #region 输出
        void ReadOut(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                XmlElement xeOut = ReadNode(xeRoot, "Out");

                robotStdPoint.P_OutIO = ReadOutIO(xeRoot);
                robotStdPoint.P_OutVaccum = ReadOutVaccum(xeRoot);
                robotStdPoint.P_OutPC = ReadOutPC(xeRoot);
            }
            catch (Exception ex)
            {

            }
        }

        Point4D ReadOutIO(XmlElement xeRoot)
        {
            try
            {
                Point4D point = new Point4D();

                bool enable = ReadChildAttributeBl(xeRoot, "OutIO", "Enable");
                point.DblValue1 = enable ? 1 : -1;

                point.DblValue2 = ReadChildAttributeInt(xeRoot, "OutIO", "IO");
                string annotation = ReadChildAttributeStr(xeRoot, "OutIO", "Annotation");

                point.NameClass = "OutIO";
                return point;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        Point4D ReadOutVaccum(XmlElement xeRoot)
        {
            try
            {
                Point4D point = new Point4D();

                string vaccum = ReadChildAttributeStr(xeRoot, "OutVaccum", "Vaccum", "Stay");
                TypeVaccumRobot_enum typeVaccumRobot_e = (TypeVaccumRobot_enum)Enum.Parse(typeof(TypeVaccumRobot_enum), vaccum);
                point.DblValue1 = (int)typeVaccumRobot_e + 1;


                string blow = ReadChildAttributeStr(xeRoot, "OutVaccum", "Blow", "Stay");
                TypeBlowRobot_enum typeBlowRobot_e = (TypeBlowRobot_enum)Enum.Parse(typeof(TypeBlowRobot_enum), blow);
                point.DblValue2 = (int)typeBlowRobot_e + 1;

                point.DblValue3 = ReadChildAttributeInt(xeRoot, "OutVaccum", "WaitTime");

                string annotation = ReadChildAttributeStr(xeRoot, "OutVaccum", "Annotation");
                point.NameClass = "OutVaccum";

                return point;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        Point4D ReadOutPC(XmlElement xeRoot)
        {
            try
            {
                Point4D point = new Point4D();

                bool enable = ReadChildAttributeBl(xeRoot, "OutPC", "Enable");
                if (enable)
                {
                    point.DblValue1 = 1;
                }
                else
                {
                    point.DblValue1 = -1;
                }

                string type = ReadChildAttributeStr(xeRoot, "OutPC", "Type","Camera1");
                TypeOutPCRobot_enum TypeOutPCRobot_e = (TypeOutPCRobot_enum)Enum.Parse(typeof(TypeOutPCRobot_enum), type);
                point.DblValue2 = (int)TypeOutPCRobot_e + 1;

                point.DblValue3 = ReadChildAttributeInt(xeRoot, "OutPC", "CMD",1);
                point.DblValue4 = ReadChildAttributeInt(xeRoot, "OutPC", "NGIO");

                string annotation = ReadChildAttributeStr(xeRoot, "OutPC", "Annotation");
                point.NameClass = "OutPC";

                return point;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        bool ReadNextPos(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                robotStdPoint.P_NextPoint.DblValue1 = ReadChildAttributeInt(xeRoot, "NextPos", "No");
                robotStdPoint.P_NextPoint.NameClass = "NextPos";

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion 输出        

        #region 点位综合设置
        //点位注释
        bool ReadAnnotation(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                robotStdPoint.Annotation = ReadAttributeStr(xeRoot, "Annotation");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region 打印信息
        bool ReadPrint(XmlElement xeRoot)
        {
            try
            {
                bool blPrintRobot = ReadAttributeBl(xeRoot, "PrintRobot");
                P_Print.DblValue1 = blPrintRobot ? 1 : -1;

                bool blPrintPC = ReadAttributeBl(xeRoot, "PrintPC");
                P_Print.DblValue2 = blPrintPC ? 1 : -1;
                P_Print.NameClass = "PrintInfo";
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion 打印信息
        #endregion 点位综合设置
        #endregion 读取

        #region 写入
        /// <summary>
        /// 写入参数
        /// </summary>
        public bool WriteXml()
        {
            try
            {
                int numError = 0;
                XmlDocument xDoc = new XmlDocument();
                XmlElement xeRoot = xDoc.CreateElement("StdPoints");
                xDoc.AppendChild(xeRoot);
                XmlElement xeHome = CreateNewXe(xeRoot, "home");
                if (!WriteInput(xeHome, RobotStdPoint_L[0]))
                {
                    numError++;
                }
                if (!WriteMove(xeHome, RobotStdPoint_L[0]))
                {
                    numError++;
                }
                if (!WriteOut(xeHome, RobotStdPoint_L[0]))
                {
                    numError++;
                }
                if (!WriteAnnotation(xeHome, RobotStdPoint_L[0]))
                {
                    numError++;
                }
                xeRoot.AppendChild(xeHome);
                for (int i = 1; i < RobotStdPoint_L.Count; i++)
                {
                    XmlElement xePos = CreateNewXe(xeRoot, "pos" + (i - 1).ToString());
                    if (!WriteInput(xePos, RobotStdPoint_L[i]))
                    {
                        numError++;
                    }
                    
                    if (!WriteMove(xePos, RobotStdPoint_L[i]))
                    {
                        numError++;
                    }
                    if (!WriteOut(xePos, RobotStdPoint_L[i]))
                    {
                        numError++;
                    }

                    if (!WriteAnnotation(xePos, RobotStdPoint_L[i]))
                    {
                        numError++;
                    }

                    xeRoot.AppendChild(xePos);
                }

                if (!WritePrint(xeRoot))
                {
                    numError++;
                }
                if (!WriteNumPoint(xeRoot))
                {
                    numError++;
                }
                if (numError>0)
                {
                    return false;
                }
                
                xDoc.Save(PathRobotStd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region 写入输入
        bool WriteInput(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                int numError = 0;
                if (!WriteVaccum(xeRoot, robotStdPoint))
                {
                    numError++;
                }

                if (!WriteInputIO(xeRoot, robotStdPoint))
                {
                    numError++;
                }
                if (!WritePC(xeRoot, robotStdPoint))
                {
                    numError++;
                }
                if (numError > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 真空
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="point"></param>
        bool WriteVaccum(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                XmlElement xeVaccum = CreateNewXe(xeRoot, "Vaccum");
                xeRoot.AppendChild(xeVaccum);

                Point4D point = robotStdPoint.P_Vaccum;
                bool enable = point.DblValue1 == -1 ? false : true;

                WriteAttribute(xeVaccum, "Enable", enable.ToString());
                WriteAttribute(xeVaccum, "NoIO", point.DblValue1.ToString());
                WriteAttribute(xeVaccum, "WaitTime", point.DblValue2.ToString());
                WriteAttribute(xeVaccum, "NGIO", point.DblValue3.ToString());
                WriteAttribute(xeVaccum, "NGPos", point.DblValue4.ToString());
                WriteAttribute(xeVaccum, "Annotation", point.Annotation.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        bool WriteInputIO(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                int numError = 0;
                if (!WriteInputIO(xeRoot, "InputIO1", robotStdPoint.P_InputIO1, robotStdPoint.P_JumpIOP1))
                {
                    numError++;
                }
                if (!WriteInputIO(xeRoot, "InputIO2", robotStdPoint.P_InputIO2, robotStdPoint.P_JumpIOP2))
                {
                    numError++;
                }
                if (!WriteInputIO(xeRoot, "InputIO3", robotStdPoint.P_InputIO2, robotStdPoint.P_JumpIOP3))
                {
                    numError++;
                }
                if (!WriteInputIO(xeRoot, "InputIO4", robotStdPoint.P_InputIO2, robotStdPoint.P_JumpIOP4))
                {
                    numError++;
                }
               
                if (numError > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 写入输入IO
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="name"></param>
        /// <param name="p_Input"></param>
        /// <param name="p_JumpInput"></param>
        bool WriteInputIO(XmlElement xeRoot, string name, Point4D p_Input, Point4D p_JumpInput)
        {
            try
            {
                XmlElement xeInput = CreateNewXe(xeRoot, name);
                xeRoot.AppendChild(xeInput);

                bool enable = (p_Input.DblValue1 ==1) ? true : false;

                WriteAttribute(xeInput, "Enable", enable.ToString());
                WriteAttribute(xeInput, "NoIO", p_Input.DblValue2.ToString());
                WriteAttribute(xeInput, "WaitTime", p_Input.DblValue3.ToString());
                WriteAttribute(xeInput, "NGIO", p_Input.DblValue4.ToString());
                WriteAttribute(xeInput, "Annotation", p_Input.Annotation.ToString());


                WriteAttribute(xeInput, "OKPos", p_JumpInput.DblValue1.ToString());
                WriteAttribute(xeInput, "NGPos", p_JumpInput.DblValue2.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 写入PC
        /// </summary>
        bool WritePC(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                XmlElement xePC = CreateNewXe(xeRoot, "PC");
                xeRoot.AppendChild(xePC);

                bool enable = robotStdPoint.P_PC.DblValue1 == -1 ? false : true;

                WriteAttribute(xePC, "Enable", enable.ToString());
                WriteAttribute(xePC, "WaitTime", robotStdPoint.P_PC.DblValue2.ToString());
                WriteAttribute(xePC, "NGIO", robotStdPoint.P_PC.DblValue3.ToString());
                WriteAttribute(xePC, "NGPos", robotStdPoint.P_PC.DblValue4.ToString());
                WriteAttribute(xePC, "Annotation", robotStdPoint.P_PC.Annotation.ToString());

                XmlElement xePCCMD = CreateNewXe(xeRoot, "PCCMD");
                xeRoot.AppendChild(xePCCMD);
                WriteAttribute(xePCCMD, "XChange", robotStdPoint.P_PCCMD.DblValue1.ToString());
                WriteAttribute(xePCCMD, "YChange", robotStdPoint.P_PCCMD.DblValue2.ToString());
                WriteAttribute(xePCCMD, "ZChange", robotStdPoint.P_PCCMD.DblValue3.ToString());
                WriteAttribute(xePCCMD, "RChange", robotStdPoint.P_PCCMD.DblValue4.ToString());
                WriteAttribute(xePCCMD, "ArmChange", robotStdPoint.P_PCCMD.DblValue5.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion 写入输入

        #region 写入运动
        bool WriteMove(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                int numError = 0;
                if (!WriteVel(xeRoot, robotStdPoint))
                {
                    numError++;
                }

                if (!WriteMovePoint(xeRoot, robotStdPoint))
                {
                    numError++;
                }
                if (numError>0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 写入速度
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="robotStdPoint"></param>
        bool WriteVel(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                Point4D point = robotStdPoint.P_Vel;

                XmlElement xeVel = CreateNewXe(xeRoot, "Vel");
                xeRoot.AppendChild(xeVel);               

                bool enable = point.DblValue1 == -1 ? false : true;
                PowerRobot_enum powerRobot_e = (PowerRobot_enum)(point.DblValue2-1);

                WriteAttribute(xeVel, "Enable", enable.ToString());
                WriteAttribute(xeVel, "Power", powerRobot_e.ToString());

                WriteAttribute(xeVel, "Vel", point.DblValue3.ToString());
                WriteAttribute(xeVel, "Acc", point.DblValue4.ToString());
                WriteAttribute(xeVel, "Annotation", point.Annotation.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 写入点位
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="robotStdPoint"></param>
        bool WriteMovePoint(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {                
                Point4D p_Move = robotStdPoint.P_Move;

                //vel
                XmlElement xeMove = CreateNewXe(xeRoot, "Move");
                xeRoot.AppendChild(xeMove);      

                MoveRobot_enum moveRobot_e = MoveRobot_enum.Stay;
                moveRobot_e = (MoveRobot_enum)p_Move.DblValue1-1;
                WriteAttribute(xeMove, "Type", moveRobot_e.ToString());

                //passCMD
                Point4D p_passCMD = robotStdPoint.P_PassCMD;

                XmlElement xePassCMD = CreateNewXe(xeRoot, "PassCMD");
                xeRoot.AppendChild(xePassCMD);

                WriteAttribute(xePassCMD, "Num", p_passCMD.DblValue1.ToString());

                TypePosRobot_enum typeZ_e = (TypePosRobot_enum)p_passCMD.DblValue2-1;
                WriteAttribute(xePassCMD, "Z", typeZ_e.ToString());
                TypePosRobot_enum typeR_e = (TypePosRobot_enum)p_passCMD.DblValue3-1;
                WriteAttribute(xePassCMD,"R", typeR_e.ToString());

                WriteMovePoint(xeRoot, "Here", robotStdPoint.P_Here);
                WriteMovePoint(xeRoot, "Pass1", robotStdPoint.P_Pass1);
                WriteMovePoint(xeRoot, "Pass2", robotStdPoint.P_Pass2);
                WriteMovePoint(xeRoot, "Pass3", robotStdPoint.P_Pass3);

                //des               
                WriteMovePoint(xeRoot, "Des", robotStdPoint.P_Des);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        bool WriteMovePoint(XmlElement xeRoot, string name, Point5D point)
        {
            try
            {
                XmlElement xeChild = CreateNewXe(xeRoot, name);
                xeRoot.AppendChild(xeChild);

                ArmRobot_enum armRobot_e = (ArmRobot_enum)point.DblValue5-1;

                WriteAttribute(xeChild, "X", point.DblValue1.ToString());
                WriteAttribute(xeChild, "Y", point.DblValue2.ToString());
                WriteAttribute(xeChild, "Z", point.DblValue3.ToString());
                WriteAttribute(xeChild, "R", point.DblValue4.ToString());
                WriteAttribute(xeChild, "Arm", armRobot_e.ToString());
                WriteAttribute(xeChild, "Annotation", point.Annotation.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion 写入运动

        #region 写入输出
        public bool WriteOut(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                int numError = 0;
                if (!WriteOutIO(xeRoot, robotStdPoint))
                {
                    numError++;
                }
                if (!WriteOutPC(xeRoot, robotStdPoint))
                {
                    numError++;
                }
                if (!WriteNextPos(xeRoot, robotStdPoint))
                {
                    numError++;
                }
                
                if (numError > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 输出IO
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="robotStdPoint"></param>
        bool WriteOutIO(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                Point4D p_OutIO = robotStdPoint.P_OutIO;

                XmlElement xeOutIO = CreateNewXe(xeRoot, "OutIO");
                xeRoot.AppendChild(xeOutIO);

                bool enable = p_OutIO.DblValue1 == -1 ? false : true;
                WriteAttribute(xeOutIO, "Enable", enable.ToString());
                WriteAttribute(xeOutIO, "IO", p_OutIO.DblValue2.ToString());
                WriteAttribute(xeOutIO, "WaitTime", p_OutIO.DblValue3.ToString());
                WriteAttribute(xeOutIO, "Annotation", p_OutIO.Annotation.ToString());

                Point4D p_OutVaccum = robotStdPoint.P_OutVaccum;

                XmlElement xOutVaccum = CreateNewXe(xeRoot, "OutVaccum");
                xeRoot.AppendChild(xOutVaccum);

                TypeVaccumRobot_enum typeVaccumRobot_e = (TypeVaccumRobot_enum)(p_OutVaccum.DblValue1-1);
                WriteAttribute(xOutVaccum, "Vaccum", typeVaccumRobot_e.ToString());

                TypeBlowRobot_enum typeBlowRobot_e = (TypeBlowRobot_enum)(p_OutVaccum.DblValue2-1);
                WriteAttribute(xOutVaccum, "Blow", typeBlowRobot_e.ToString());
                WriteAttribute(xOutVaccum, "WaitTime", p_OutVaccum.DblValue3.ToString());
                WriteAttribute(xOutVaccum, "Annotation", p_OutVaccum.Annotation.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 输出PC
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="robotStdPoint"></param>
        bool WriteOutPC(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                Point4D p_OutPC = robotStdPoint.P_OutPC;

                XmlElement xeOutPC = CreateNewXe(xeRoot, "OutPC");
                xeRoot.AppendChild(xeOutPC);

                bool enable = p_OutPC.DblValue1 ==1 ? true : false;
                WriteAttribute(xeOutPC, "Enable", enable.ToString());

                TypeOutPCRobot_enum typeOutPCRobot_e = (TypeOutPCRobot_enum)(p_OutPC.DblValue2 - 1);

                WriteAttribute(xeOutPC, "Type", typeOutPCRobot_e.ToString());
                WriteAttribute(xeOutPC, "CMD", p_OutPC.DblValue3.ToString());
                WriteAttribute(xeOutPC, "NGIO", p_OutPC.DblValue4.ToString());
                WriteAttribute(xeOutPC, "Annotation", p_OutPC.Annotation.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// NextPos
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="robotStdPoint"></param>
        bool WriteNextPos(XmlElement xeRoot, RobotStdPoint robotStdPoint)
        {
            try
            {
                XmlElement xeNextPos = CreateNewXe(xeRoot, "NextPos");
                xeRoot.AppendChild(xeNextPos);

                WriteAttribute(xeNextPos, "No", robotStdPoint.P_NextPoint.DblValue1.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        #endregion 写入运动

        #region 综合点位设置
        bool WriteNumPoint(XmlElement xeRoot)
        {
            try
            {
                WriteAttribute(xeRoot, "Num", NumPoint.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 写入打印
        /// </summary>
        /// <param name="xeRoot"></param>
        /// <param name="name"></param>
        /// <param name="point"></param>
        bool WritePrint(XmlElement xeRoot)
        {
            try
            {
                bool blEnable_Robot = P_Print.DblValue1 == -1 ? false : true;
                bool blEnable_PC = P_Print.DblValue1 == -1 ? false : true;

                WriteAttribute(xeRoot, "PrintRobot", blEnable_Robot.ToString());
                WriteAttribute(xeRoot, "PrintPC", blEnable_PC.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //点位注释
        bool WriteAnnotation(XmlElement xeRoot,RobotStdPoint robotStdPoint)
        {
            try
            {
                WriteAttribute(xeRoot, "Annotation", robotStdPoint.Annotation);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion 综合点位设置
        #endregion 写入
    }
}
