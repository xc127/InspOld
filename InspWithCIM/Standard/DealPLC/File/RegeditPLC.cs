using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;
using DealFile;

namespace DealPLC
{
    public class RegeditPLC : RegeditFile
    {
        #region 静态类实例
        public static new RegeditPLC R_I = new RegeditPLC();
        #endregion 静态类实例

        public bool BlOffLinePLC
        {
            get
            {
                base.StrBool = ReadRegedit("BlOffLinePLC");
                return bool.Parse(base.StrBool);
            }
            set
            {
                WriteRegedit("BlOffLinePLC", value.ToString());
            }
        }
    }
}
