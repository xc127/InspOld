using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class WinCamera7 : BaseWinCamera
    {
        #region 窗体单实例
        private static WinCamera7 g_WinCamera7 = null;
        public static WinCamera7 GetWinInst()
        {
            try
            {
                if (g_WinCamera7 == null)
                {
                    g_WinCamera7 = new WinCamera7();
                }
                return g_WinCamera7;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera7", ex);
                return null;
            }
        }
        #endregion 窗体单实例

        #region 初始化
        public WinCamera7()
        {
            try
            {
                NameClass = "WinCamera7";
                g_BaseUCDisplayCamera = new UCDisplayCamera7();
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
            g_WinCamera7 = null;
        }
        #endregion 关闭
    }
}
