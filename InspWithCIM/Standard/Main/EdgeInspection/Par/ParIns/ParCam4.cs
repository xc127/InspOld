using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    public class ParCam4: ParInspAll
    {
        #region 静态类实例
        public static ParCam4 P_I = new ParCam4();
        #endregion

        public ParCam4()
        {
            NameCam = "Cam4";
        }
    }
}
