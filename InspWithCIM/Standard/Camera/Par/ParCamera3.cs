using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Camera
{
    [Serializable]
    public class ParCamera3 : BaseParCamera
    {
        #region 静态类实例
        public static ParCamera3 P_I = new ParCamera3();
        #endregion 静态类实例

        #region 定义
        public bool BlOpenCamera = false;//相机1打开
        #endregion 定义

        public ParCamera3()
        {
            this.NoCamera = 3;
            NameClass = "ParCamera3";
        }
    }
}
