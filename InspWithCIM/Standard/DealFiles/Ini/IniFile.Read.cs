using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealFile
{
    partial class IniFile
    {
        #region String
        /// <summary>
        /// 读取String型
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadIniStr(string section, string key, string path)
        {
            try
            {
                StringBuilder temp = new StringBuilder(2000);
                int i = GetPrivateProfileString(section, key, "", temp, 2000, path);
                return temp.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public string ReadIniStr(string section, string key, string strDefault, string path)
        {
            try
            {
                StringBuilder temp = new StringBuilder(2000);
                int i = GetPrivateProfileString(section, key, "", temp, 2000, path);
                string a = temp.ToString();
                if (temp.ToString() == "")
                {
                    return strDefault;
                }
                return temp.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        #endregion String

        #region bool
        /// <summary>
        /// 读取bool型
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool ReadIniBl(string section, string key, bool blDefault, string path)
        {
            try
            {
                StringBuilder temp = new StringBuilder(1000);
                int i = GetPrivateProfileString(section, key, "", temp, 1000, path);
                //转换成bool型变量输出
                return Boolean.Parse(temp.ToString());
            }
            catch (Exception ex)
            {
                return blDefault;
            }
        }
        //默认输出false
        public bool ReadIniBl(string section, string key, string path)
        {
            try
            {
                StringBuilder temp = new StringBuilder(1000);
                int i = GetPrivateProfileString(section, key, "", temp, 1000, path);
                //转换成bool型变量输出
                return Boolean.Parse(temp.ToString());
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion bool

        #region Int
        /// <summary>
        /// 读取int型
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int ReadIniInt(string section, string key, int intDefault, string path)
        {
            try
            {
                StringBuilder temp = new StringBuilder(1000);
                int i = GetPrivateProfileString(section, key, "", temp, 1000, path);
                //转换成int型变量输出
                return int.Parse(temp.ToString());
            }
            catch (Exception ex)
            {
                return intDefault;
            }
        }

        public int ReadIniInt(string section, string key, string path)
        {
            try
            {
                StringBuilder temp = new StringBuilder(1000);
                int i = GetPrivateProfileString(section, key, "", temp, 1000, path);
                //转换成int型变量输出
                return int.Parse(temp.ToString());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion Int

        #region double
        /// <summary>
        /// 读取double型
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public double ReadIniDbl(string section, string key, double dblDefault, string path)
        {
            try
            {
                StringBuilder temp = new StringBuilder(1000);
                int i = GetPrivateProfileString(section, key, "", temp, 1000, path);
                //转换成double型变量输出
                return Double.Parse(temp.ToString());
            }
            catch (Exception ex)
            {
                return dblDefault;
            }
        }

        public double ReadIniDbl(string section, string key, string path)
        {
            try
            {
                StringBuilder temp = new StringBuilder(1000);
                int i = GetPrivateProfileString(section, key, "", temp, 1000, path);
                //转换成double型变量输出
                return Double.Parse(temp.ToString());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        #endregion double
    }
}
