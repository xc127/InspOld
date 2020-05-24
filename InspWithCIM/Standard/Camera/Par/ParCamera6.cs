using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Camera
{
    [Serializable]
    public class ParCamera6 : BaseParCamera
    {
        #region 静态类实例
        public static ParCamera6 P_I = new ParCamera6();
        #endregion 静态类实例

        #region 定义
        public bool BlOpenCamera = false;//相机1打开
        #endregion 定义

        public ParCamera6()
        {
            this.NoCamera = 6;
            NameClass = "ParCamera6";
        }
    }
}
