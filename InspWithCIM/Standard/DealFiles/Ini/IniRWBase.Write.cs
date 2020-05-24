using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace DealFile
{
    public partial class IniRWBase
    {
        //写入DoubleList
        public bool WriteDblListIni(string strSection, string strKey, string strPath, List<double> list)
        {
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    IniFile.I_I.WriteIni(strSection, strKey + i.ToString(), list[i].ToString(), strPath);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //写入IntList
        public bool WriteIntListIni(string strSection, string strKey, string strPath, List<int> list)
        {
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    IniFile.I_I.WriteIni(strSection, strKey + i.ToString(), list[i].ToString(), strPath);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //写入Point2DList
        public bool WriteP2ListIni(string strSection, string strKey, string strPath, List<Point2D> list)
        {
            try
            {
                IniFile.I_I.WriteIni(strSection, "Num", list.Count.ToString(), strPath);
                for (int i = 0; i < list.Count; i++)
                {
                    IniFile.I_I.WriteIni(strSection, strKey + "X" + i.ToString(), list[i].DblValue1.ToString(), strPath);
                    IniFile.I_I.WriteIni(strSection, strKey + "Y" + i.ToString(), list[i].DblValue2.ToString(), strPath);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
