using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using DealLog;
using System.IO;
using System.Collections;
using BasicClass;
using DealResult;
using System.Diagnostics;

namespace Main
{
    partial class BaseDealComprehensiveResult
    {
        #region 定义

        protected Stopwatch sw_Tact = new Stopwatch();
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

        #region 记录节拍
        /// <summary>
        /// 记录图像处理的节拍和整体的节拍
        /// </summary>
        protected void RecordTact(int noCamera, int pos, Hashtable htResult)
        {
            TxtFile t_I = new TxtFile();
            try
            {
                sw_Tact.Stop();
                long tFull = sw_Tact.ElapsedMilliseconds;

                string root = ParPathRoot.PathRoot + "软件运行记录\\RecordCamera\\";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                string pathDir = Log.CreateAllTimeFile(root);

                string path = pathDir + "Camera" + noCamera.ToString() + "-Pos" + pos.ToString() + ".txt";
                string name = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + ":" + DateTime.Now.Millisecond.ToString();
                t_I.WriteText(path, name);//写入时间

                //记录每个单子算子的时间
                List<int> key_L = new List<int>();
                foreach (string str in htResult.Keys)
                {
                    if (str.Contains("C"))
                    {
                        key_L.Add(int.Parse(str.Replace("C", "")));
                    }
                }
                key_L.Sort();

                //记录单元格运行时间
                foreach (int index in key_L)
                {
                    string nameCell = "C" + index.ToString();
                    if (htResult[nameCell] is BaseResultHObject)
                    {
                        if (((BaseResultHObject)htResult[nameCell]).Pos != pos)
                        {
                            continue;
                        }
                        //单元格运行时间
                        string cell = ((BaseResultHObject)htResult[nameCell]).NameCell + ((BaseResultHObject)htResult[nameCell]).Type;
                        double cellTime = ((BaseResultHObject)htResult[nameCell]).TactTime;
                        t_I.WriteText(path, cell + "=" + cellTime.ToString() + "ms");

                        //运行状态
                        NameCell = nameCell + "_Main";
                        BaseResult result = (BaseResult)htResult[nameCell];
                        double[] num = NumRunInfo;
                        if (result.BlResult)
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
                        string type = result.LevelError_e.ToString() + "-" + result.TypeErrorProcess_e.ToString() + "-" + result.Annotation;
                        string info = "NumOK=" + num[0].ToString() + ";NumNG=" + num[1].ToString() + ";Sum=" + num[2].ToString() + ";OK/Sum=" + co.ToString() + "**Result:" + type;
                        t_I.WriteText(path, info);
                    }
                }

                //记录图像处理时间
                long tImageP = long.Parse(htResult["TimeImageP-Pos" + pos.ToString()].ToString());
                t_I.WriteText(path, "图像处理=" + tImageP.ToString());

                //记录图像处理时间
                long tDisplay = long.Parse(htResult["TimeDisplay-Pos" + pos.ToString()].ToString());
                t_I.WriteText(path, "画面显示=" + tDisplay);

                //记录图像处理时间
                long tMemory = long.Parse(htResult["Memory-Rubbish" + pos.ToString()].ToString());
                t_I.WriteText(path, "垃圾清除+内存监控=" + tMemory);

                //记录整体节拍
                t_I.WriteText(path, "整体节拍" + tFull + "\n\r");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }


        protected void RecordTact(Stopwatch sw, int noCamera, int pos, Hashtable htResult)
        {
            TxtFile t_I = new TxtFile();
            try
            {
                sw.Stop();
                long tFull = sw.ElapsedMilliseconds;

                string root = ParPathRoot.PathRoot + "软件运行记录\\RecordCamera\\";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                string pathDir = Log.CreateAllTimeFile(root);

                string path = pathDir + "Camera" + noCamera.ToString() + "-Pos" + pos.ToString() + ".txt";
                string name = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + ":" + DateTime.Now.Millisecond.ToString();
                t_I.WriteText(path, name);//写入时间

                //记录每个单子算子的时间
                List<int> key_L = new List<int>();
                foreach (string str in htResult.Keys)
                {
                    if (str.Contains("C"))
                    {
                        key_L.Add(int.Parse(str.Replace("C", "")));
                    }
                }
                key_L.Sort();

                //记录单元格运行时间
                foreach (int index in key_L)
                {
                    string nameCell = "C" + index.ToString();
                    if (htResult[nameCell] is BaseResultHObject)
                    {
                        if (((BaseResultHObject)htResult[nameCell]).Pos != pos)
                        {
                            continue;
                        }
                        //单元格运行时间
                        string cell = ((BaseResultHObject)htResult[nameCell]).NameCell + ((BaseResultHObject)htResult[nameCell]).Type;
                        double cellTime = ((BaseResultHObject)htResult[nameCell]).TactTime;
                        t_I.WriteText(path, cell + "=" + cellTime.ToString() + "ms");

                        //运行状态
                        NameCell = nameCell + "_Main";
                        BaseResult result = (BaseResult)htResult[nameCell];
                        double[] num = NumRunInfo;
                        if (result.BlResult)
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
                        string type = result.LevelError_e.ToString() + "-" + result.TypeErrorProcess_e.ToString() + "-" + result.Annotation;
                        string info = "NumOK=" + num[0].ToString() + ";NumNG=" + num[1].ToString() + ";Sum=" + num[2].ToString() + ";OK/Sum=" + co.ToString() + "**Result:" + type;
                        t_I.WriteText(path, info);
                    }
                }

                //记录图像处理时间
                long tImageP = long.Parse(htResult["TimeImageP-Pos" + pos.ToString()].ToString());
                t_I.WriteText(path, "图像处理=" + tImageP.ToString());

                //记录图像处理时间
                long tDisplay = long.Parse(htResult["TimeDisplay-Pos" + pos.ToString()].ToString());
                t_I.WriteText(path, "画面显示=" + tDisplay);

                //记录图像处理时间
                long tMemory = long.Parse(htResult["Memory-Rubbish" + pos.ToString()].ToString());
                t_I.WriteText(path, "垃圾清除+内存监控=" + tMemory);

                //记录整体节拍
                t_I.WriteText(path, "整体节拍" + tFull + "\n\r");
                t_I.WriteText(path, "\n\r");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 记录节拍
    }
}
