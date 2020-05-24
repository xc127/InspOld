using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Camera
{
    [Serializable]
    public class ParCamera12 : BaseParCamera
    {
        #region 静态类实例
        public static ParCamera12 P_I = new ParCamera12();
        #endregion 静态类实例

        #region 定义
        public bool BlOpenCamera = false;//相机12打开
        #endregion 定义

        public ParCamera12()
        {
            this.NoCamera = 12;
            NameClass = "ParCamera12";
        }
    }
}
