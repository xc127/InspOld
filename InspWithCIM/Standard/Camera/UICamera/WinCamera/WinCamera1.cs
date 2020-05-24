using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Camera
{
    public class WinCamera1 : BaseWinCamera
    {
        #region 窗体单实例
        private static WinCamera1 g_WinCamera1 = null;
        public static WinCamera1 GetWinInst()
        {
            try
            {
                if (g_WinCamera1 == null)
                {
                    g_WinCamera1 = new WinCamera1();
                }
                return g_WinCamera1;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera1", ex);
                return null;
            }
        }        
        #endregion 窗体单实例

        #region 初始化
        public WinCamera1()
        {
            try
            {
                NameClass = "WinCamera1";
                g_BaseUCDisplayCamera = new UCDisplayCamera1();
                Init();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinCamera1", ex);
            }
        }
        #endregion 初始化
     
    }
}
