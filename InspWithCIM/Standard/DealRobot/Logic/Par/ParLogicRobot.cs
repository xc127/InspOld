using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DealRobot
{
    public partial class ParLogicRobot
    {
        #region 静态类实例
        public static ParLogicRobot P_I = new ParLogicRobot();
        #endregion 静态类实例

        #region 定义
        
        //bool       
        public bool isReadOK = false;//表示机器人读取数据完成     
        public bool blStartRead = false;//开始读取数据

        //string       
        public string strTrrigerRobot="";        
    
        //enum
        public StatePortRobot_enum StatePortRobot_e = StatePortRobot_enum.Wait;//服务器默认为等待状态
        
        #endregion 定义
    }

    //模拟机器人触发命令
    public class RobotTrriger
    {
        public int No { get; set; }
        public string CMD { get; set; }//命令
        public string  Annotation { get; set; } //注释
    }
    //机器人通信结果枚举
    public enum StatePortRobot_enum
    {
        AllError = 0,
        AllTrue = 1,
        Wait = 2,
    }       
}
