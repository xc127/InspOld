using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Threading.Tasks;
using System.Threading;
using DealFile;
using DealRobot;
using BasicClass;
using DealComInterface;

namespace DealRobot
{
    public partial class LogicRobot : BaseClass
    {
        #region 静态类实例
        public static LogicRobot L_I = new LogicRobot();
        #endregion 静态类实例

        #region 定义

        static Mutex g_Mutex = new Mutex();
        BaseFunInterface g_PortRobotBase = null;
        public int NoCamera = 1;

        //定义事件
        public event StrBlAction StateRobotInterface_event;
        #endregion 定义

        #region 初始化
        public LogicRobot()
        {
            NameClass = "LogicRobot";
        }
        /// <summary>
        /// 事件注册
        /// </summary>
        void Login_Event()
        {
            try
            {
                StateRobotInterface_event += new StrBlAction(LogicRobot_StateRobotInterface_event);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void LogicRobot_StateRobotInterface_event(string str, bool result)
        {

        }

        public void Init()
        {

            try
            {
                switch (ParSetRobot.P_I.TypeRobot_e)
                {
                    case TypeRobot_enum.Null:
                        break;

                    case TypeRobot_enum.Epsion_Ethernet:
                        g_PortRobotBase = new FunInterFaceEthernet();
                        break;

                    case TypeRobot_enum.Epsion_Serial:
                        break;

                    case TypeRobot_enum.YAMAH_Ethernet:
                        g_PortRobotBase = new FunInterFaceEthernet();
                        break;

                    case TypeRobot_enum.YAMAH_Serial:
                        break;

                    case TypeRobot_enum.NaChi_Ethernet:
                        g_PortRobotBase = new FunInterFaceEthernet();
                        break;
                }
                if (g_PortRobotBase != null)
                {
                    g_PortRobotBase.ISInterfaceOpen_evnt += new StrBlAction(LogicRobot_ISServerOpen_evnt);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        #region 打开机器人
        /// <summary>
        /// 打开机器人接口
        /// </summary>
        public void OpenInterface()
        {
            try
            {
                g_Mutex.WaitOne();

                TypePortState_enum typePortState_e = g_PortRobotBase.OpenInterface(ParSetRobot.P_I.g_BaseParInterface);//传入参数，打开响应的接口

                //如果不是服务器，则开始读取数据
                if (((ParInterfaceEthernet)(ParSetRobot.P_I.g_BaseParInterface)).TypeInterface_e != TypeInterface_enum.ServerEthernet)
                {
                    if (typePortState_e == TypePortState_enum.OK)
                    {
                        ReadData_Task();
                        StateRobotInterface_event("机器人通信连接成功!", true);
                    }
                    else
                    {
                        StateRobotInterface_event("机器人通信连接失败!", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                g_Mutex.ReleaseMutex();
            }
        }

        /// <summary>
        /// 服务器端口连接情况
        /// </summary>
        /// <param name="str"></param>
        /// <param name="result"></param>
        void LogicRobot_ISServerOpen_evnt(string str, bool result)
        {
            try
            {
                if (result)//接口打开成功后，实时监控读数据
                {
                    ReadData_Task();                    
                }
                StateRobotInterface_event(str, result);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 打开机器人

        #region 关闭机器人
        /// <summary>
        /// 关闭机器人通信端口
        /// </summary>
        /// <returns></returns>
        public bool CloseRobot()
        {
            try
            {
                //停止机器人数据读取
                ParLogicRobot.P_I.blStartRead = false;
                if (g_PortRobotBase == null)
                {
                    return true;
                }
                return g_PortRobotBase.CloseInterface();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 关闭机器人
    }
}
