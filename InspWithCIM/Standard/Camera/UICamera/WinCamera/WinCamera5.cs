using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class WinCamera5 : BaseWinCamera
    {
        #region 窗体单实例
        private static WinCamera5 g_WinCamera5 = null;
        public static WinCamera5 GetWinInst()
        {
            try
            {
                if (g_WinCamera5 == null)
                {
                    g_WinCamera5 = new WinCamera5();
                }
                return g_WinCamera5;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera5", ex);
                return null;
            }
        }
        #endregion 窗体单实例

        #region 初始化
        public WinCamera5()
        {
            try
            {
                NameClass = "WinCamera5";
                g_BaseUCDisplayCamera = new UCDisplayCamera5();
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
            g_WinCamera5 = null;
        }
        #endregion 关闭
    }
}
