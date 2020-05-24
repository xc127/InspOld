using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    public class ParCam1 : ParInspAll
    {
        #region 静态类实例
        public static ParCam1 P_I = new ParCam1();
        #endregion

        public ParCam1()
        {
            NameCam = "Cam1";
        }
    }
}
