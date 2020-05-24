using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using BasicClass;

namespace DealFile
{
    [Serializable]
    public partial class RegeditFile:BaseClass
    {
        #region 静态类实例
        public static RegeditFile R_I = new RegeditFile();
        #endregion 静态类实例

        #region 方法
        /// <summary>
        /// 读取注册表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string ReadRegedit(string name)
        {
            string regeditData;
            try
            {
                RegistryKey hkml = Registry.CurrentUser;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
                RegistryKey aimdir = software.OpenSubKey("OnSight", true);
                regeditData = aimdir.GetValue(name).ToString();
            }
            catch
            {
                regeditData = "";               
            }
            return regeditData;
        }


        public double ReadRegeditDbl(string name)
        {
            string regeditData;
            try
            {
                RegistryKey hkml = Registry.CurrentUser;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
                RegistryKey aimdir = software.OpenSubKey("OnSight", true);
                regeditData = aimdir.GetValue(name).ToString();
                return double.Parse(regeditData);
            }
            catch
            {
                return 0;
            }
        }
        public double ReadRegeditInt(string name)
        {
            string regeditData;
            try
            {
                RegistryKey hkml = Registry.CurrentUser;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
                RegistryKey aimdir = software.OpenSubKey("OnSight", true);
                regeditData = aimdir.GetValue(name).ToString();
                return int.Parse(regeditData);
            }
            catch
            {
                return 0;
            }
        }
        public bool ReadRegeditBl(string name)
        {
            string regeditData;
            try
            {
                RegistryKey hkml = Registry.CurrentUser;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
                RegistryKey aimdir = software.OpenSubKey("OnSight", true);
                regeditData = aimdir.GetValue(name).ToString();
                return bool.Parse(regeditData);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 写注册表  
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
    
        public void WriteRegedit(string name, string value)
        {
            try
            {
                RegistryKey hklm = Registry.CurrentUser;
                RegistryKey software = hklm.OpenSubKey("SOFTWARE", true);
                RegistryKey aimdir = software.CreateSubKey("OnSight");
                aimdir.SetValue(name, value);
            }
            catch
            {
               
            }
        }
        #endregion 方法

    }

}
