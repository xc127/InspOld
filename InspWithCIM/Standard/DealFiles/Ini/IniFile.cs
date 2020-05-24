using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;

namespace DealFile
{
    [Serializable]
    public partial class IniFile
    {
        #region 静态类实例
        public static IniFile I_I= new IniFile();
        #endregion 静态类实例        

        //dll里的函数，用来支持ini读写
        [DllImport("kernel32")]
        static extern long WritePrivateProfileString(string section,
         string key, string val, string filePath);

        [DllImport("kernel32")]
        static extern int GetPrivateProfileString(string section,
            string key, string def, StringBuilder retVal,
            int size, string filePath);
    }
}
