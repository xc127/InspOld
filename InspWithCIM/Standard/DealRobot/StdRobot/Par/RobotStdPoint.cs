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
    /// <summary>
    /// 机器人点位
    /// </summary>
    [Serializable]
    public partial class RobotStdPoint : BaseParRobot
    {
        #region 静态类实例
        public static RobotStdPoint R_I = new RobotStdPoint();
        #endregion 静态类实例

        #region 定义
        #region Path
        public string PathRobotStd
        {
            get
            {
                string root = ParPathRoot.PathRoot + "Store\\Robot\\RobotStd\\";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                return root + "RobotStdPoint.xml";
            }
        }
        #endregion Path

        public List<RobotStdPoint> RobotStdPoint_L = new List<RobotStdPoint>();

        public int Count
        {
            get
            {
                return RobotStdPoint_L.Count;
            }
        }
        //索引器
        public RobotStdPoint this[int index]
        {
            get
            {
                try
                {
                    return this.RobotStdPoint_L[index];
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        int numPoint = 0;
        public int NumPoint//总共的点位个数
        {
            get
            {
                return numPoint;
            }
            set
            {
                numPoint = value;
            }
        }
        public string NamePoint { set; get; }//点位名称

        #region 输入        
        public string Info_Input
        {
            get
            {
                return (Info_Vaccum + "-" + Info_InputIO1 + "-" + Info_InputIO2 + "-" + Info_InputIO3 + "-" + Info_InputIO4 + "-" + Info_PC).Replace("----", "-").Replace("---", "-").Replace("--", "-");
            }
        }
        //真空
        public Point4D P_Vaccum = new Point4D("vaccum", -1, 4000, 0, -1);//设置真空
        public string Info_Vaccum
        {
            get
            {
                string str="";
                if (P_Vaccum.DblValue1!=-1)
                {
                    str = string.Format("IO端口{0}等待真空建立,", P_Vaccum.DblValue1.ToString());
                }
                return str;
            }
        }
        //IO
        public Point4D P_InputIO1 = new Point4D("inputIO1", -1, 0, 4000, -1);//输入IO1
        public Point4D P_JumpIOP1 = new Point4D("jumpIOP1", -1, -1);//输入IO1跳转点位
        public string Info_InputIO1
        {
            get
            {
                string str = "";
                if (P_InputIO1.DblValue1 != -1)
                {
                    str = string.Format("等待IO端口1为{0},", P_InputIO1.DblValue2.ToString());
                }
                return str;
            }
        }

        public Point4D P_InputIO2 = new Point4D("inputIO2", -1, 0, 4000, -1);//输入IO2
        public Point4D P_JumpIOP2 = new Point4D("jumpIOP2", -1, -1);//输入IO2跳转点位
        public string Info_InputIO2
        {
            get
            {
                string str = "";
                if (P_InputIO2.DblValue1 != -1)
                {
                    str = string.Format("等待IO端口2为{0},", P_InputIO2.DblValue2.ToString());
                }
                return str;
            }
        }

        public Point4D P_InputIO3 = new Point4D("inputIO3", -1, 0, 4000, -1);//输入IO3
        public Point4D P_JumpIOP3 = new Point4D("jumpIOP3", -1, -1);//输入IO3跳转点位
        public string Info_InputIO3
        {
            get
            {
                string str = "";
                if (P_InputIO3.DblValue1 != -1)
                {
                    str = string.Format("等待IO端口3为{0},", P_InputIO3.DblValue2.ToString());
                }
                return str;
            }
        }

        public Point4D P_InputIO4 = new Point4D("inputIO4", -1, 0, 4000, -1);//输入IO4
        public Point4D P_JumpIOP4 = new Point4D("jumpIOP4", -1, -1);//输入IO4跳转点位
        public string Info_InputIO4
        {
            get
            {
                string str = "";
                if (P_InputIO4.DblValue1 != -1)
                {
                    str = string.Format("等待IO端口4为{0},", P_InputIO4.DblValue2.ToString());
                }
                return str;
            }
        }

        //PC
        public Point5D P_PC = new Point5D("pc", -1, -1, -1, -1, 1);//PC数据设置

        public Point5D P_PCCMD = new Point5D("pcCMD", -1, -1, -1, -1, -1);//PC数据更改
        public string Info_PC
        {
            get
            {
                string str = "";
                if (P_PC.DblValue1 != -1)
                {
                    str = string.Format("等待PC发送坐标,");
                }
                if (P_PCCMD.DblValue1!=-1)
                {
                    str += "替换X,";
                }
                if (P_PCCMD.DblValue2 != -1)
                {
                    str += "替换Y,";
                }
                if (P_PCCMD.DblValue3 != -1)
                {
                    str += "替换Z,";
                }
                if (P_PCCMD.DblValue4 != -1)
                {
                    str += "替换R,";
                }
                return str;
            }
        }
        public Point4D P_PCDelta = new Point4D();//PC补偿值
        #endregion 输入

        #region 运动
        public string Info_Move
        {
            get
            {
                return Info_Vel + "-" + Info_MoveP;
            }
        }
        //Move
        public Point4D P_Vel = new Point4D("vel", -1, 1, 100, 100);//速度
        public string Info_Vel
        {
            get
            {
                string str = "";
                string power = ((PowerRobot_enum)((int)P_Vel.DblValue2-1)).ToString();
                if (P_Vel.DblValue1 != -1)
                {
                    str = string.Format("设置功率{0},速度{1}，加减速{2}", power, P_Vel.DblValue3.ToString(), P_Vel.DblValue4.ToString());
                }
              
                return str;
            }
        }
        public Point4D P_Move = new Point4D("move", 1);//运动指令
        public string Info_MoveP
        {
            get
            {
                string str = "";
                string move = ((MoveRobot_enum)((int)P_Move.DblValue1-1)).ToString();
                if (move == "Stay")
                {
                    str = "不移动";
                }
                else
                {
                    str = string.Format("运动指令为{0}", move);
                }                
                return str;
            }
        }
        public Point5D P_Des = new Point5D("des",0,0,0,0,1);//目标点坐标

        public Point5D P_Here = new Point5D("here");//机器人当前点位
        public Point4D P_PassCMD = new Point4D("passCMD",-1,0,1,1);//机器人当前点位移动设置

        public Point5D P_Pass1 = new Point5D("pass1",0,0,0,0,1);//中间点1
        public Point5D P_Pass2 = new Point5D("pass2", 0, 0, 0, 0, 1);//中间点2
        public Point5D P_Pass3 = new Point5D("pass3", 0, 0, 0, 0, 1);//中间点3
        #endregion 运动

        #region 输出
        public string Info_Out
        {
            get
            {
                return (Info_OutIO + "-" + Info_OutVaccum + "-" + Info_OutPC +  "-" + Info_NextPoint).Replace("----", "-").Replace("---", "-").Replace("--", "-"); ;
            }
        }
        public Point4D P_OutIO = new Point4D("outIO",-1);//输出IO
        public string Info_OutIO
        {
            get
            {
                string str = "";
                if (P_OutIO.DblValue1 != -1)
                {
                    str = string.Format("输出IO{0}", P_OutIO.DblValue1.ToString());
                }               
                return str;
            }
        }
        public Point4D P_OutVaccum = new Point4D("outVaccum",1,1);//输出真空指令
        public string Info_OutVaccum
        {
            get
            {
                string str = "";
                switch ((int)P_OutVaccum.DblValue1)
                {
                    case 2:
                        str = "打开真空";
                        break;

                    case 3:
                        str = "关闭真空";
                        break;
                }

                switch ((int)P_OutVaccum.DblValue2)
                {
                    case 2:
                        str += "关闭吹气";
                        break;

                    case 3:
                        str += string.Format("吹气并延迟{0}ms关闭", P_OutVaccum.DblValue3.ToString());
                        break;

                    case 4:
                        str += string.Format("吹气并保持");
                        break;
                }
                return str;
            }
        }
        public Point4D P_OutPC = new Point4D("outPC", -1, 1, 1, 0);//输出PC指令
        public string Info_OutPC
        {
            get
            {
                string str = "";
                if (P_OutPC.DblValue1!=-1)
                {
                    str = string.Format("输出PC指令{0}，值为{1}", "Camera" + P_OutPC.DblValue1.ToString(), P_OutPC.DblValue2.ToString());
                }
                return str;
            }
        }
        
        public Point4D P_NextPoint = new Point4D("nextPos", 1000);//下一个点位
        public string Info_NextPoint
        {
            get
            {
                string str = "";
                if (P_NextPoint.DblValue1 != 1000
                    && P_NextPoint.DblValue1 != -1)
                {
                    str = "跳转到点位:" + P_NextPoint.DblValue1.ToString();
                }
                else if (P_NextPoint.DblValue1 == 1000)
                {
                    str = "跳转到点位Home:";
                }
                return str;
            }
        }
        #endregion 输出

        #region 点位综合设置
        public Point4D P_Print = new Point4D("print", 1, -1);//打印
        public string Info_Print
        {
            get
            {
                string str = "";
                if (P_Print.DblValue1 != -1)
                {
                    str = string.Format("在机器人运行界面打印流程");
                }
                if (P_Print.DblValue2 != -1)
                {
                    str = string.Format("输出流程给PC");
                }
                return str;
            }
        }
        #endregion 点位综合设置

        #endregion 定义

        #region 点位修改
        #region Add
        /// <summary>
        /// 在点位最后增加一个点位
        /// </summary>
        public void AddPoint()
        {
            try
            {
                RobotStdPoint robotStdPoint = (RobotStdPoint)RobotStdPoint_L[Count - 1].Clone();
                robotStdPoint.NamePoint = "pos" + (Count - 1).ToString();
                robotStdPoint.No = Count;
                RobotStdPoint_L.Add(robotStdPoint);
                NumPoint = Count - 1;

                ReSortPoint();
            }
            catch (Exception ex)
            {
                
            }
        }
        /// <summary>
        /// 插入点位
        /// </summary>
        public void AddPoint(int index)
        {
            try
            {
                RobotStdPoint robotStdPoint = (RobotStdPoint)RobotStdPoint_L[index].Clone();
                robotStdPoint.NamePoint = "pos" + index.ToString();
                RobotStdPoint_L.Add(robotStdPoint);
                NumPoint = Count - 1;
                ReSortPoint();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion Add

        #region Del
        /// <summary>
        /// 删除点位
        /// </summary>
        public void DelPoint()
        {
            try
            {
                RobotStdPoint_L.RemoveAt(Count - 1);
                NumPoint = Count - 1;

                ReSortPoint();
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// 删除点位
        /// </summary>
        public void DelPoint(int index)
        {
            try
            {
                RobotStdPoint_L.RemoveAt(index);
                NumPoint = Count - 1;
                ReSortPoint();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion Del

        /// <summary>
        /// 重新排列点位
        /// </summary>
        void ReSortPoint()
        {
            try
            {
                for (int i = 1; i < Count; i++)
                {
                    RobotStdPoint_L[i].No = i;
                    RobotStdPoint_L[i].NamePoint = "pos" + (i-1).ToString();

                }
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion 点位修改
    }

    public enum PowerRobot_enum
    {
        Low,
        High,
        
    }

    public enum MoveRobot_enum
    {
        Stay,
        Go,
        Move,
        Jump,
        Pass,
        GoHere,
    }

    public enum ArmRobot_enum
    {
        Left,
        Right,
    }

    public enum TypePosRobot_enum
    {
        Relative,
        Abs
    }

    public enum TypeVaccumRobot_enum
    {
        Stay,
        Open,
        Close,
    }

    public enum TypeBlowRobot_enum
    {
        Stay,
        Close,
        OpenAndClose,
        Open,
    }

    public enum TypeOutPCRobot_enum
    {
        Camera1,
        Camera2,
        Camera3,
        Camera4,
        Camera5,
        Camera6,
        Others
    }
}
