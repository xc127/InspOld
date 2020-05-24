using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class Camera5 : CameraBase
    {
        #region 静态类实例
        public static Camera5 C_I = new Camera5();
        #endregion 静态类实例

        #region 初始化
        public Camera5()
        {
            try
            {
                NameClass = "Camera5";               
            }
            catch (Exception ex)
            {
                 Log.L_I.WriteError("Camera5", ex);
            }           
        }
        #endregion 初始化
    }
}
