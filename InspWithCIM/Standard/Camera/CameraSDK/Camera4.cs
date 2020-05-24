using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class Camera4 : CameraBase
    {
        #region 静态类实例
        public static Camera4 C_I = new Camera4();
        #endregion 静态类实例

        #region 初始化
        public Camera4()
        {
            try
            {
                NameClass = "Camera4";                
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("Camera4", ex);
            }          
        }
        #endregion 初始化
    }
}
