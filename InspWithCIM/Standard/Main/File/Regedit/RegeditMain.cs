using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;

namespace Main
{
    public partial class RegeditMain : RegeditFile
    {
        #region 静态类实例
        public static new RegeditMain R_I = new RegeditMain();
        #endregion 静态类实例

        #region 窗体最大化状态
        /// <summary>
        /// 窗体是否最大化
        /// </summary>
        public bool BlMaxWin
        {
            get
            {
                base.StrBool = ReadRegedit("BlMaxWin");
                return Boolean.Parse(base.StrBool);
            }
            set
            {
                WriteRegedit("BlMaxWin", value.ToString());
            }
        }
        #endregion 窗体最大化状态

        #region 离线模式

        public bool BlOffLineComPort
        {
            get
            {
                base.StrBool = ReadRegedit("BlOffLineComPort");
                return bool.Parse(base.StrBool);
            }
            set
            {
                WriteRegedit("BlOffLineComPort", value.ToString());
            }
        }
        #endregion 离线模式

        #region gdCamera
        public double Width_gdCamera
        {
            get
            {
                base.StrDouble = ReadRegedit("Width_gdCamera");
                return double.Parse(base.StrDouble);
            }
            set
            {
                WriteRegedit("Width_gdCamera", value.ToString());
            }
        }
        public double Height_gdCamera
        {
            get
            {
                base.StrDouble = ReadRegedit("Height_gdCamera");
                return double.Parse(base.StrDouble);
            }
            set
            {
                WriteRegedit("Height_gdCamera", value.ToString());
            }
        }
        public double Width_gdInfo
        {
            get
            {
                base.StrDouble = ReadRegedit("Width_gdInfo");

                return double.Parse(base.StrDouble);
            }
            set
            {
                WriteRegedit("Width_gdInfo", value.ToString());
            }
        }
        #endregion gdCamera

        #region 窗体大小
        public double Width_Win
        {
            get
            {
                base.StrDouble = ReadRegedit("Width_Win");
                return double.Parse(base.StrDouble);
            }
            set
            {
                WriteRegedit("Width_Win", value.ToString());
            }
        }
        public double Height_Win
        {
            get
            {
                base.StrDouble = ReadRegedit("Height_Win");
                return double.Parse(base.StrDouble);
            }
            set
            {
                WriteRegedit("Height_Win", value.ToString());
            }
        }
        #endregion 窗体大小

        #region 中片平台所处的位置
        public int LoadPlatePos
        {
            get
            {
                try
                {
                    return Convert.ToInt32(ReadRegedit("LoadPlatePos"));
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                WriteRegedit("LoadPlatePos", value.ToString());
            }
        }

        public int GlassCnt
        {
            get
            {
                try
                {
                    return Convert.ToInt32(ReadRegedit("GlassCnt"));
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                WriteRegedit("GlassCnt", value.ToString());
            }
        }
        #endregion

        public int AccessIndex
        {
            get
            {
                try
                {
                    return Convert.ToInt32(ReadRegedit("AccessIndex"));
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                WriteRegedit("AccessIndex", value.ToString());
            }
        }
    }
}
