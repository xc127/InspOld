using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class WinCamera6 : BaseWinCamera
    {
        #region 窗体单实例
        private static WinCamera6 g_WinCamera6 = null;
        public static WinCamera6 GetWinInst()
        {
            try
            {
                if (g_WinCamera6 == null)
                {
                    g_WinCamera6 = new WinCamera6();
                }
                return g_WinCamera6;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera6", ex);
                return null;
            }
        }
        #endregion 窗体单实例

        #region 初始化
        public WinCamera6()
        {
            try
            {
                NameClass = "WinCamera6";
                g_BaseUCDisplayCamera = new UCDisplayCamera6();
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
            g_WinCamera6 = null;
        }
        #endregion 关闭
    }
}
