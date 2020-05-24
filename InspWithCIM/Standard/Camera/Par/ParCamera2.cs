using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Camera
{
    [Serializable]
    public class ParCamera2 : BaseParCamera
    {
        #region 静态类实例
        public static ParCamera2 P_I = new ParCamera2();
        #endregion 静态类实例

        #region 定义
        public bool BlOpenCamera = false;//相机1打开
        #endregion 定义

        public ParCamera2()
        {
            this.NoCamera = 2;
            NameClass = "ParCamera2";
        }
    }
}
