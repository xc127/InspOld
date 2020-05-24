using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Camera
{
    [Serializable]
    public class ParCamera5 : BaseParCamera
    {
        #region 静态类实例
        public static ParCamera5 P_I = new ParCamera5();
        #endregion 静态类实例

        #region 定义
        public bool BlOpenCamera = false;//相机1打开
        #endregion 定义


        public ParCamera5()
        {
            this.NoCamera = 5;
            NameClass = "ParCamera5";
        }
    }
}
