using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class Camera1 : CameraBase
    {
        #region 静态类实例
        public static Camera1 C_I = new Camera1();
        #endregion 静态类实例

        #region 初始化
        public Camera1()
        {
            try
            {
                NameClass = "Camera1";               
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Camera1", ex);
            }          
        }
        #endregion 初始化
    }
}
