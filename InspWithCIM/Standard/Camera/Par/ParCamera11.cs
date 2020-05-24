using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Camera
{
    [Serializable]
    public class ParCamera11 : BaseParCamera
    {
        #region 静态类实例
        public static ParCamera11 P_I = new ParCamera11();
        #endregion 静态类实例

        #region 定义
        public bool BlOpenCamera = false;//相机11打开
        #endregion 定义

        public ParCamera11()
        {
            this.NoCamera = 11;
            NameClass = "ParCamera11";
        }
    }
}
