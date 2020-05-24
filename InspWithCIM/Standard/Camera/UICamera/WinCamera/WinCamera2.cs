using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class WinCamera2 : BaseWinCamera
    {
        #region 窗体单实例
        private static WinCamera2 g_WinCamera2 = null;
        public static WinCamera2 GetWinInst()
        {
            try
            {
                if (g_WinCamera2 == null)
                {
                    g_WinCamera2 = new WinCamera2();
                }
                return g_WinCamera2;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera2", ex);
                return null;
            }
        }
        #endregion 窗体单实例

        #region 初始化
        public WinCamera2()
        {
            try
            {
                NameClass = "WinCamera2";
                g_BaseUCDisplayCamera = new UCDisplayCamera2();
                Init();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera2", ex);
            }
        }
        #endregion 初始化

        #region 关闭
        public override void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            g_BaseUCDisplayCamera.RecoverPhoto_Invoke();
            g_WinCamera2 = null;
        }
        #endregion 关闭
    }
}
