using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class Camera3 : CameraBase
    {
        #region 静态类实例
        public static Camera3 C_I = new Camera3();
        #endregion 静态类实例

        #region 初始化
        public Camera3()
        {
            try
            {
                NameClass = "Camera3";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Camera3", ex);
            }
        }
        #endregion 初始化
    }
}
