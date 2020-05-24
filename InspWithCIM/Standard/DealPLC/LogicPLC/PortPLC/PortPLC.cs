using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealPLC;

namespace DealPLC
{
    public class PortPLC
    {
        #region 定义
        public BasePortPLC g_BasePortPLC = null;
        #endregion

        public PortPLC()
        {
            try
            {
                switch (ParSetPLC.P_I.TypePLC_e)
                {
                    case TypePLC_enum.MIT:
                        g_BasePortPLC = new PortPLC_MIT();
                        break;

                    case TypePLC_enum.SEM:
                        break;

                    case TypePLC_enum.PAN:
                        break;

                    case TypePLC_enum.MIT_NEW:
                        g_BasePortPLC = new PortPLC_MITNew();
                        break;

                    case TypePLC_enum.MIT_Hls:
                        g_BasePortPLC = new PortPLC_MITHls();
                        break;

                    default:
                        g_BasePortPLC = new PortPLC_MIT();
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
