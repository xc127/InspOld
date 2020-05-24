using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Camera
{
    [Serializable]
    public class ParCamera1 : BaseParCamera
    {
        #region 静态类实例
        public static ParCamera1 P_I = new ParCamera1();
        #endregion 静态类实例

        #region 定义
        public bool BlOpenCamera = false;//相机1打开
        #endregion 定义

        public ParCamera1()
        {
            this.NoCamera = 1;
            NameClass = "ParCamera1";
        }
    }
}
