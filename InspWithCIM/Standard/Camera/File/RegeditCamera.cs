using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;
using DealFile;

namespace Camera
{
    public class RegeditCamera : RegeditFile
    {
        #region 静态类实例
        public static new RegeditCamera R_I = new RegeditCamera();
        #endregion 静态类实例

        public bool BlOffLineCamera
        {
            get
            {
                base.StrBool = ReadRegedit("BlOffLineCamera");
                return bool.Parse(base.StrBool);
            }
            set
            {
                WriteRegedit("BlOffLineCamera", value.ToString());
            }
        }
    }
}
