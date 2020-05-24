using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class Camera2 : CameraBase
    {
        #region 静态类实例
        public static Camera2 C_I = new Camera2();
        #endregion 静态类实例

        #region 初始化
        public Camera2()
        {
            try
            {
                NameClass = "Camera2";
            }
            catch (Exception ex)
            {
               Log.L_I.WriteError("Camera2", ex);
            }          
        }
        #endregion 初始化
    }
}
