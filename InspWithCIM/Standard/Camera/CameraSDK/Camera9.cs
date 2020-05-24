using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class Camera9 : CameraBase
    {
        #region 静态类实例
        public static Camera9 C_I = new Camera9();
        #endregion 静态类实例

        #region 初始化
        public Camera9()
        {
            try
            {
                NameClass = "Camera9";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Camera9", ex);
            }
        }
        #endregion 初始化
    }
}
