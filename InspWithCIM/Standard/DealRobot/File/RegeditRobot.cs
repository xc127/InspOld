using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;

namespace DealRobot
{
    public class RegeditRobot : RegeditFile
    {
        #region 静态类实例
        public static new RegeditRobot R_I = new RegeditRobot();
        #endregion 静态类实例

        public bool BlOffLineRobot
        {
            get
            {
                base.StrBool = ReadRegedit("BlOffLineRobot");
                return bool.Parse(base.StrBool);
            }
            set
            {
                WriteRegedit("BlOffLineRobot", value.ToString());
            }
        }
    }
}
