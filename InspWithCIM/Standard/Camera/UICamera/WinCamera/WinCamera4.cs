using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class WinCamera4 : BaseWinCamera
    {
        #region 窗体单实例
        private static WinCamera4 g_WinCamera4 = null;
        public static WinCamera4 GetWinInst()
        {
            try
            {
                if (g_WinCamera4 == null)
                {
                    g_WinCamera4 = new WinCamera4();
                }
                return g_WinCamera4;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera4", ex);
                return null;
            }
        }
        #endregion 窗体单实例

        #region 初始化
        public WinCamera4()
        {
            try
            {
                NameClass = "WinCamera4";
                g_BaseUCDisplayCamera = new UCDisplayCamera4();
                Init();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera4", ex);
            }
        }
        #endregion 初始化

        #region 关闭
        public override void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            g_BaseUCDisplayCamera.RecoverPhoto_Invoke();
            g_WinCamera4 = null;
        }
        #endregion 关闭
    }
}
