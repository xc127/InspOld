using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealRobot
{
    public class LogicRobotCam2 : LogicRobot
    {
        #region 静态类实例
        public static new LogicRobot L_I
        {
            get
            {
                LogicRobot.L_I.NoCamera = 2;
                return LogicRobot.L_I;
            }
        }
        #endregion 静态类实例
    }
}
