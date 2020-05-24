using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class WinCamera8 : BaseWinCamera
    {
        #region 窗体单实例
        private static WinCamera8 g_WinCamera8 = null;
        public static WinCamera8 GetWinInst()
        {
            try
            {
                if (g_WinCamera8 == null)
                {
                    g_WinCamera8 = new WinCamera8();
                }
                return g_WinCamera8;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera8", ex);
                return null;
            }
        }
        #endregion 窗体单实例

        #region 初始化
        public WinCamera8()
        {
            try
            {
                NameClass = "WinCamera8";
                g_BaseUCDisplayCamera = new UCDisplayCamera8();
                Init();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        #region 关闭
        public override void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            g_BaseUCDisplayCamera.RecoverPhoto_Invoke();
            g_WinCamera8 = null;
        }
        #endregion 关闭
    }
}
