using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealLog;
using BasicClass;
using System.IO;
using DealConfigFile;
using Common;
using DealFile;

namespace SetPar
{
    public class FunDelFolder : BaseClass
    {
        #region 静态类实例
        public static FunDelFolder F_I = new FunDelFolder();
        #endregion 静态类实例

        #region 初始化
        public FunDelFolder()
        {
            NameClass = "FunDelFolder";
        }
        #endregion 初始化

        /// <summary>
        /// 监控文件是否可以删除
        /// </summary>
        /// <param name="uCStateWork"></param>
        public void DelteFolder(UCStateWork uCStateWork)
        {
            try
            {
                bool blDel1 = true;//是否允许删除
                foreach (BaseParDelFolder baseParDelFolder in ParDelFolder.P_I.BaseParDelFolder_L)
                {
                    #region 是否定时删除
                    int hour = DateTime.Now.Hour;
                    int minute = DateTime.Now.Minute;
                    //时间1
                    if (ParDelFolder.P_I.BlDel1)
                    {
                        if (hour == ParDelFolder.P_I.Hour1
                            && minute > ParDelFolder.P_I.Minute1)
                        {
                            blDel1 = true;
                        }
                        else
                        {
                            blDel1 = false;
                        }
                    }

                    //时间2
                    bool blDel2 = true;//是否允许删除
                    if (ParDelFolder.P_I.BlDel2)
                    {
                        if (hour == ParDelFolder.P_I.Hour2
                            && minute > ParDelFolder.P_I.Minute2)
                        {
                            blDel2 = true;
                        }
                        else
                        {
                            blDel2 = false;
                        }
                    }
                    #endregion 是否定时删除

                    if (blDel1
                        || blDel2)
                    {
                        //删除本地记录
                        string path = baseParDelFolder.PathFolder;
                        if (baseParDelFolder.Annotation.Contains("未使用型号图片记录")
                            && baseParDelFolder.BlExcute)
                        {
                            DelteOldImage(baseParDelFolder, uCStateWork);
                        }
                        else if (baseParDelFolder.Annotation.Contains("算法单元节拍")
                            && baseParDelFolder.BlExcute)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                string pathDel = path + "Camera" + (i + 1).ToString() + "\\";
                                if (Log.L_I.GetDirectoryNum(pathDel) > baseParDelFolder.Num)
                                {
                                    Log.L_I.DeleteDir(pathDel, baseParDelFolder.Num);
                                    uCStateWork.AddInfo(string.Format("根据设定，删除多余文件:{0}", pathDel));
                                }
                            }
                        }
                        else if (Log.L_I.GetDirectoryNum(path) > baseParDelFolder.Num)
                        {
                            if (baseParDelFolder.BlExcute)//是否执行删除文件
                            {
                                Log.L_I.DeleteDir(path, baseParDelFolder.Num);
                                uCStateWork.AddInfo(string.Format("根据设定，删除多余文件:{0}", baseParDelFolder.PathFolder));
                            }
                        }

                        //删除注册表记录
                        ClearNumRecordCell();//删除单元格运行计数
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 删除旧型号
        /// </summary>
        void DelteOldImage(BaseParDelFolder baseParDelFolder, UCStateWork uCStateWork)
        {
            try
            {
                DateTime dt = DateTime.Now;
                DateTime dtStd = dt.AddDays(-baseParDelFolder.Num);

                //用时间命名
                string Year = dtStd.Year.ToString();
                string Month = dtStd.Month.ToString().PadLeft(2, '0');
                string Day = dtStd.Day.ToString().PadLeft(2, '0');
                string date = Year + "-" + Month + "-" + Day + "\\";

                DirectoryInfo strBaseLog = new DirectoryInfo(baseParDelFolder.PathFolder);
                //包含的文件夹
                foreach (DirectoryInfo dirModel in strBaseLog.GetDirectories())
                {
                    //删除除当前型号之外的型号图片文件
                    if (dirModel.Name != ComConfigPar.C_I.NameModel)
                    {
                        foreach (DirectoryInfo dirDate in dirModel.GetDirectories())
                        {
                            string dateOld = dirDate.Name;
                            DateTime dtOld;
                            if (DateTime.TryParse(dateOld, out dtOld))
                            {
                                TimeSpan ts = dtOld.Subtract(dtStd);
                                if ((int)ts.TotalDays < 0)//如果日期比删除指定日期提前，则删除
                                {
                                    Directory.Delete(dirDate.FullName, true);//删除文件夹，以及子文件夹子文件
                                    uCStateWork.AddInfo(string.Format("根据设定，删除多余文件:{0}", dirDate.FullName));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        #region 清空算子计数
        void ClearNumRecordCell()
        {
            try
            {
                RegeditFile r_I = new RegeditFile();
                for (int i = 0; i < 200; i++)
                {
                    string cell = "C" + i.ToString();
                    r_I.WriteRegedit(cell, "");
                    r_I.WriteRegedit(cell + "_Main", "");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 清空算子计数
    }
}
