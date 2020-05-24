using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCIM
{
    public partial class CIM
    {
        #region define
        const string SubKey1 = "SOFTWARE";
        const string SubKey2 = "CIM";
        #endregion

        #region variables
        public static int ChipIDCount
        {
            get
            {
                try
                {
                    return Convert.ToInt32(ReadRegedit("ChipIDCount"));
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                WriteRegedit("ChipIDCount", value.ToString());
            }
        }

        public static int LotNum
        {
            get
            {
                try
                {
                    return Convert.ToInt32(ReadRegedit("LotNum"));
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                WriteRegedit("LotNum", value.ToString());
            }
        }

        public static int CodeNGCount
        {
            get
            {
                try
                {
                    return Convert.ToInt32(ReadRegedit("CodeNGCount"));
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                WriteRegedit("CodeNGCount", value.ToString());
            }
        }

        public static int ChipIDNGCount
        {
            get
            {
                try
                {
                    return Convert.ToInt32(ReadRegedit("ChipIDNGCount"));
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                WriteRegedit("ChipIDNGCount", value.ToString());
            }
        }
        #endregion

        #region mode
        #endregion

        #region 接口
        public static string ReadRegedit(string name)
        {
            try
            {
                RegistryKey hkml = Registry.CurrentUser;
                RegistryKey software = hkml.OpenSubKey(SubKey1, true);
                RegistryKey aimdir = software.OpenSubKey(SubKey2, true);
                return aimdir.GetValue(name).ToString();
            }
            catch
            {
                return string.Empty;
            }
                     
        }

        public static void WriteRegedit(string name, string value)
        {
            try
            {
                RegistryKey hklm = Registry.CurrentUser;
                RegistryKey software = hklm.OpenSubKey(SubKey1, true);
                RegistryKey aimdir = software.CreateSubKey(SubKey2);
                aimdir.SetValue(name, value);
            }
            catch { }
        }
        #endregion
    }
}
