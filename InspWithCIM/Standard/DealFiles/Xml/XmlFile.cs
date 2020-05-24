using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using BasicClass;

namespace DealFile
{
    [Serializable]
    public partial class XmlFile : BaseClass
    {
        #region 静态类实例
        public static XmlFile X_I = new XmlFile();
        #endregion 静态类实例

        #region 定义

        #endregion 定义

        #region 初始化
        public XmlFile()
        {
            NameClass = "XmlFile";
        }
        #endregion 初始化
    }
}
