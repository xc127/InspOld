using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;
using DealFile;
using System.IO;

namespace DealLog
{
    public class ParStateAndAlarm:BaseClass
    {
        #region 静态类实例
        public static ParStateAndAlarm P_I=new ParStateAndAlarm();
        #endregion 静态类实例

        #region 定义
        string Path
        {
            get
            {
                string path = ParPathRoot.PathRoot + "store\\SetLog\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path+"SetLog.ini";
            }
        }

        public bool BlShowMain = true;//主界面是否显示
        public bool BlAutoShow = false;//主窗体自动显示

        #endregion 定义

        #region 读写参数
        public void ReadIni()
        {
            try
            {
                IniFile i_I=new IniFile ();
                BlShowMain = i_I.ReadIniBl("SetStateAlarm", "BlShowMain",true, Path);

                BlAutoShow = i_I.ReadIniBl("SetStateAlarm", "BlAutoShow", Path);
            }
            catch (Exception ex)
            {
                
            }
        }

        public void WriteIni()
        {
            try
            {
                IniFile i_I = new IniFile();
                i_I.WriteIni("SetStateAlarm", "BlShowMain", BlShowMain.ToString(), Path);
                i_I.WriteIni("SetStateAlarm", "BlAutoShow", BlAutoShow.ToString(), Path);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 读写参数
    }
}
