using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using BasicClass;
using System.IO;

namespace DealLog
{
    public class DealRecordCell : BaseClass
    {
        #region 定义
        string NameCell = "";
        double[] NumRunInfo
        {
            get
            {
                double[] num = new double[3];
                try
                {
                    RegeditFile r_I = new RegeditFile();
                    string str = r_I.ReadRegedit(NameCell);
                    if (str.Contains(","))
                    {
                        string[] strNum = str.Split(',');
                        if (strNum.Length == 3)
                        {
                            num[0] = int.Parse(strNum[0]);
                            num[1] = int.Parse(strNum[1]);
                            num[2] = int.Parse(strNum[2]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                return num;
            }
            set
            {
                RegeditFile r_I = new RegeditFile();
                string str = value[0].ToString() + "," + value[1].ToString() + "," + value[2].ToString();
                r_I.WriteRegedit(NameCell, str.ToString());
            }
        }
        #endregion 定义

        #region 初始化
        public DealRecordCell()
        {
            NameClass = "DealRecordCell";
        }
        #endregion 初始化

        public void FunRecordCell(int noCamera, string nameCell, string name, double time, bool blResult, string type)
        {
            try
            {
                string root = ParPathRoot.PathRoot + "软件运行记录\\RecordCell\\Camera" + noCamera + "\\";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                string pathDir = Log.CreateAllTimeFile(root);

                string path = pathDir + nameCell + name + ".txt";
                string timeNow = DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + "-" + DateTime.Now.Millisecond.ToString();

                //记录个数
                NameCell = nameCell;
                double[] num = NumRunInfo;
                if (blResult)
                {
                    num[0] = num[0] + 1;//OK
                }
                else
                {
                    num[1] = num[1] + 1;//NG
                }
                num[2] = num[2] + 1;//Sum
                NumRunInfo = num;
                double co = 0;
                if (num[2] != 0)
                {
                    co = Math.Round((double)num[0] / (double)num[2], 2);
                }

                string result = "NumOK=" + num[0].ToString() + ";NumNG=" + num[1].ToString() + ";Sum=" + num[2].ToString() + ";OK/Sum=" + co.ToString() + "\r\n" + type;
                string all = timeNow + "\r\n" + time + "ms\r\n" + result + "\r\n";
                TxtFile txt = new TxtFile();
                txt.WriteText(path, all);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
    }
}
