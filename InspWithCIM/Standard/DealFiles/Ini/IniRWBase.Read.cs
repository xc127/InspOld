using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace DealFile
{
    public partial class IniRWBase
    {
        //读取Point2DList
        public bool ReadP2ListIni(string strSection, string strKey, string strPath, out List<Point2D> list)
        {
            list = new List<Point2D>();
            string strNum = IniFile.I_I.ReadIniStr(strSection, "Num", strPath);
            int intNum = int.Parse(strNum);
            try
            {
                for (int i = 0; i < intNum; i++)
                {
                    string strX = IniFile.I_I.ReadIniStr(strSection, strKey + "X" + i.ToString(), strPath);
                    string strY = IniFile.I_I.ReadIniStr(strSection, strKey + "Y" + i.ToString(), strPath);
                    double dblX = Convert.ToDouble(strX);
                    double dblY = Convert.ToDouble(strY);
                    list.Add(new Point2D(dblX, dblY));
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
