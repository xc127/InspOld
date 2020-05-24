using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;
using DealComInterface;

namespace DealRobot
{
    public partial class ParSetRobot : BaseClass
    {
        #region 静态类实例
        public static ParSetRobot P_I = new ParSetRobot();
        #endregion 静态类实例

        #region 定义
        #region Path
        static string c_PathTypeRobot = ParPathRoot.PathRoot + "Store\\Robot\\TypeRobot.ini";
        static string c_PathRobotData = ParPathRoot.PathRoot + "Store\\Robot\\RobotData.ini";
        static string c_PathRobotWork = ParPathRoot.PathRoot + "Store\\Robot\\RobotWork.ini";
        #endregion Path

        //string 
      
        //bool
        public bool blAlarmShake = false;//机器人握手失败报警
        public bool blDataOverflow = false;//机器人数据超限
        public bool BlAutoConnect = false;//自动连接机器人
        
        //int 
     
        //enum
        public TypeRobot_enum TypeRobot_e = TypeRobot_enum.Null;

        //List
        public List<DataLimitRobot> dataLimitRobot_L = new List<DataLimitRobot>();

        //Class
        public BaseParComInterface g_BaseParInterface = null;//通信接口参数
        #endregion 定义

        #region 设定通信参数
        /// <summary>
        /// 设定通信参数
        /// </summary>
        /// <param name="type"></param>
        public void SetParInterface(string type)
        {
            try
            {
                if (type.Contains("Ethernet"))//以太网通信
                {
                    g_BaseParInterface = new ParInterfaceEthernetRobot();
                }
                else
                {
                    g_BaseParInterface = new BaseParComInterface();
                }
                g_BaseParInterface.Init();
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 设定通信参数
    }

    /// <summary>
    /// 机器人类型
    /// </summary>
    public enum TypeRobot_enum
    {        
        YAMAH_Ethernet,//雅马哈
        YAMAH_Serial,//雅马哈
        Epsion_Ethernet,//爱普生
        Epsion_Serial,//爱普生
        NaChi_Ethernet,//那智
        Null,
    }
}
