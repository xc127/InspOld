using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class WinCamera3 : BaseWinCamera
    {
        #region 窗体单实例
        private static WinCamera3 g_WinCamera3 = null;
        public static WinCamera3 GetWinInst()
        {
            try
            {
                if (g_WinCamera3 == null)
                {
                    g_WinCamera3 = new WinCamera3();
                }
                return g_WinCamera3;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera3", ex);
                return null;
            }
        }
        #endregion 窗体单实例

        #region 初始化
        public WinCamera3()
        {
            try
            {
                NameClass = "WinCamera3";
                g_BaseUCDisplayCamera = new UCDisplayCamera3();
                Init();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera3", ex);
            }
        }
        #endregion 初始化

        #region 关闭
        public override void BaseMetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            g_BaseUCDisplayCamera.RecoverPhoto_Invoke();
            g_WinCamera3 = null;
        }
        #endregion 关闭
    }
}
