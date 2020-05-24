using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class Camera6 : CameraBase
    {
        #region 静态类实例
        public static Camera6 C_I = new Camera6();
        #endregion 静态类实例

        #region 初始化
        public Camera6()
        {
            try
            {
                NameClass = "Camera6";              
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Camera6", ex);
            }           
        }
        #endregion 初始化
    }
}
