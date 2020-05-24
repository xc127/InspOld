using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealComInterface;
using BasicClass;

namespace DealRobot
{
    public class ParInterfaceEthernetRobot : ParInterfaceEthernet
    {
        #region 定义
        #region Path
        public override string PathSave
        {
            get
            {
                return ParPathRoot.PathRoot+"Store\\Robot\\TypeRobot.ini";
            }
        }
        #endregion Path
        #endregion 定义
    }
}
