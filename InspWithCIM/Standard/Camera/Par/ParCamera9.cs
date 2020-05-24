using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Camera
{
    [Serializable]
    public class ParCamera9 : BaseParCamera
    {
        #region 静态类实例
        public static ParCamera9 P_I = new ParCamera9();
        #endregion 静态类实例

        #region 定义
        public bool BlOpenCamera = false;//相机9打开
        #endregion 定义

        public ParCamera9()
        {
            this.NoCamera = 9;
            NameClass = "ParCamera9";
        }
    }
}
