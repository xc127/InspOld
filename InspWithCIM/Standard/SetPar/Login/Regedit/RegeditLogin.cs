using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;

namespace SetPar
{
    public class RegeditLogin : RegeditFile
    {
        #region 静态类实例
        public static new RegeditLogin R_I = new RegeditLogin();
        #endregion 静态类实例

        /// <summary>
        /// 是否m默认使用厂商权限
        /// </summary>
        public bool BlManufacturer
        {
            get
            {
                this.StrBool = ReadRegedit("BlManufacturer");
                return Convert.ToBoolean(this.StrBool);
            }
            set
            {
                WriteRegedit("BlManufacturer", value.ToString());
            }
        }     

    }
}
