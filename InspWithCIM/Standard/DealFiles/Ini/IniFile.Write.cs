using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealFile
{
    partial class IniFile
    {
        /// <summary>
        /// 写入string型
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="strKey"></param>
        /// <param name="strKeyValue"></param>
        /// <param name="strPath"></param>
        public void WriteIni(string strSection, string strKey, string strKeyValue, string strPath)
        {
            try
            {
                WritePrivateProfileString(strSection, strKey, strKeyValue, strPath);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 写入double int 型
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="strKey"></param>
        /// <param name="strKeyValue"></param>
        /// <param name="strPath"></param>
        public void WriteIni(string strSection, string strKey, double dblKeyValue, string strPath)
        {
            try
            {
                WritePrivateProfileString(strSection, strKey, dblKeyValue.ToString(), strPath);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 写入bool型
        /// </summary>
        /// <param name="strSection"></param>
        /// <param name="strKey"></param>
        /// <param name="strKeyValue"></param>
        /// <param name="strPath"></param>
        public void WriteIni(string strSection, string strKey, bool blKeyValue, string strPath)
        {
            try
            {
                WritePrivateProfileString(strSection, strKey, blKeyValue.ToString(), strPath);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
