using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class Camera12 : CameraBase
    {
        #region 静态类实例
        public static Camera12 C_I = new Camera12();
        #endregion 静态类实例

        #region 初始化
        public Camera12()
        {
            try
            {
                NameClass = "Camera12";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Camera12", ex);
            }
        }
        #endregion 初始化
    }
}
