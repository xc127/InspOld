using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class Camera10 : CameraBase
    {
        #region 静态类实例
        public static Camera10 C_I = new Camera10();
        #endregion 静态类实例

        #region 初始化
        public Camera10()
        {
            try
            {
                NameClass = "Camera10";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Camera10", ex);
            }
        }
        #endregion 初始化
    }
}
