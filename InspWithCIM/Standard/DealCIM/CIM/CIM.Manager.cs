using BasicClass;
using DealFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace DealCIM
{
    public partial class CIM
    {       
        #region 接口
        /// <summary>
        /// CIM的初始化，连接队列并打开monitor
        /// </summary>
        public bool Init()
        {
            return Connect() == "0000";
        }

        #endregion
    }  
}
