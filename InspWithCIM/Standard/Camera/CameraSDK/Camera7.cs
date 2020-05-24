using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class Camera7 : CameraBase
    {
        #region 静态类实例
        public static Camera7 C_I = new Camera7();
        #endregion 静态类实例

        #region 初始化
        public Camera7()
        {
            try
            {
                NameClass = "Camera7";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Camera7", ex);
            }
        }
        #endregion 初始化
    }
}
