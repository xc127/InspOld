using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace DealLog
{
    public class ParRecover : BaseClass
    {
        #region 静态类实例
        public static ParRecover P_I = new ParRecover();
        #endregion 静态类实例

        #region 定义
        //Path
        public string PathRecover = "";

        public int CountDir
        {
            get
            {
                return g_BaseParRecoverDir_L.Count;
            }
        }

        public int CountFile
        {
            get
            {
                return g_BaseParRecoverFile_L.Count;
            }
        }

        public List<BaseParRecoverDir> g_BaseParRecoverDir_L = new List<BaseParRecoverDir>();
        public List<BaseParRecoverFile> g_BaseParRecoverFile_L = new List<BaseParRecoverFile>();
        #endregion 定义

    }

    public class BaseParRecoverDir:BaseClass
    {
        public string Date { set; get; }
        public string Time { set; get; }
        public string Reason { set; get; }
        public string Path { set; get; }
        public string PathSource { set; get; }
        public string PathSourceShow
        {
            get
            {
                return ParPathRoot.PathRoot + PathSource;
            }
        }
    }

    public class BaseParRecoverFile : BaseClass
    {
        public string Name { set; get; }
        public string Type { set; get; }
        public string PathDir { set; get; }
        public string Path { set; get; }
    }
}
