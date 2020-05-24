using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Camera
{
    [Serializable]
    public class ParCamera4 : BaseParCamera
    {
        #region 静态类实例
        public static ParCamera4 P_I = new ParCamera4();
        #endregion 静态类实例

        #region 定义
        public bool BlOpenCamera = false;//相机1打开
        #endregion 定义


        public ParCamera4()
        {
            this.NoCamera = 4;
            NameClass = "ParCamera4";
        }
    }
}
