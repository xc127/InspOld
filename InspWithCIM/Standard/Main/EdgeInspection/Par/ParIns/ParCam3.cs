using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    public class ParCam3 : ParInspAll
    {
        #region 静态类实例
        public static ParCam3 P_I = new ParCam3();
        #endregion

        public ParCam3()
        {
            NameCam = "Cam3";
        }
    }
}
