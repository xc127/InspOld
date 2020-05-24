using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Camera
{
    [Serializable]
    public class ParCamera10 : BaseParCamera
    {
        #region 静态类实例
        public static ParCamera10 P_I = new ParCamera10();
        #endregion 静态类实例

        #region 定义
        public bool BlOpenCamera = false;//相机10打开
        #endregion 定义

        public ParCamera10()
        {
            this.NoCamera = 10;
            NameClass = "ParCamera10";
        }
    }
}
