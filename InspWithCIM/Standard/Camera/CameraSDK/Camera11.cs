using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class Camera11 : CameraBase
    {
        #region 静态类实例
        public static Camera11 C_I = new Camera11();
        #endregion 静态类实例

        #region 初始化
        public Camera11()
        {
            try
            {
                NameClass = "Camera11";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Camera11", ex);
            }
        }
        #endregion 初始化
    }
}
