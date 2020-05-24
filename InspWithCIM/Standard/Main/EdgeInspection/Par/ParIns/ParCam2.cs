using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    public class ParCam2 : ParInspAll
    {
        #region 静态类实例
        public static ParCam2 P_I = new ParCam2();
        #endregion

        public ParCam2()
        {
            NameCam = "Cam2";
        }
    }
}
