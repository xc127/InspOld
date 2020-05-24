using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class Camera8 : CameraBase
    {
        #region 静态类实例
        public static Camera8 C_I = new Camera8();
        #endregion 静态类实例

        #region 初始化
        public Camera8()
        {
            try
            {
                NameClass = "Camera8";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Camera8", ex);
            }
        }
        #endregion 初始化
    }
}
