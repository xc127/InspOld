using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using BasicClass;

namespace DealConfigFile
{
    public class ParSetAdjust : BaseClass
    {
        #region 静态类实例
        public static ParSetAdjust P_I = new ParSetAdjust();
        #endregion 静态类实例

        #region 定义
        //Path
        public static string C_PathSavePar = ParPathRoot.PathRoot + "Store\\AdjustStd\\SetParAdjust.ini";
        //int
        const int C_NumValue = 4;//总共四个调整值

        //string
        string g_Section = "";

        public string g_Title = "";
        public string Title
        {
            get
            {
                string str = IniFile.I_I.ReadIniStr(g_Section, "Title", C_PathSavePar);
                return str;
            }         
        }

        //List
        /// <summary>
        ///四个调整值相关的设置参数
        /// </summary>
        public List<BaseParSetAdjust> g_ParSetAdjust_L = new List<BaseParSetAdjust>();

        //索引器
        public List<BaseParSetAdjust> this[string section]
        {
            get
            {
                if (section != g_Section)
                {
                     g_Section = section;
                     ReadIniStr();
                }               
                return g_ParSetAdjust_L;
            }
        }
        #endregion 定义

        #region 初始化
    
        #endregion 初始化

        #region 读取参数
        /// <summary>
        /// 从Ini文件中读取参数
        /// </summary>
        public void ReadIniStr()
        {
            try
            {
                g_ParSetAdjust_L.Clear();
                //title
               

                for (int i = 0; i < C_NumValue; i++)
                {
                    string basekey = "Value" + (i + 1).ToString();

                    //名称
                    string name = IniFile.I_I.ReadIniStr(g_Section, "Name" + basekey, C_PathSavePar);
                    if (name == "")
                    {
                        name = "Value" + (i + 1).ToString();
                    }

                    //小数点个数
                    string strIncrement = IniFile.I_I.ReadIniStr(g_Section, "Increment" + basekey, C_PathSavePar);
                    if (!strIncrement.Contains("Num"))
                    {
                        strIncrement = "Num2";
                    }


                    //最大最小值
                    base.StrDouble = IniFile.I_I.ReadIniStr(g_Section, "Min" + basekey, C_PathSavePar);
                    double min = Convert.ToDouble(base.StrDouble);
                    base.StrDouble = IniFile.I_I.ReadIniStr(g_Section, "Max" + basekey, C_PathSavePar);
                    double max = Convert.ToDouble(base.StrDouble);
                    if (min == 0
                        && max == 0)
                    {
                        min = int.MinValue;
                        max = int.MaxValue;
                    }

                    //权限
                    bool blWorker = true;
                    string strWorker = IniFile.I_I.ReadIniStr(g_Section, "Worker" + basekey, C_PathSavePar);
                    if (strWorker == "")
                    {

                    }
                    else
                    {
                        try
                        {
                            blWorker = Boolean.Parse(strWorker);
                        }
                        catch (Exception)
                        {

                            blWorker = true;
                        }
                    }

                    bool blEngineer = true;
                    string strEngineer = IniFile.I_I.ReadIniStr(g_Section, "Engineer" + basekey, C_PathSavePar);
                    if (strEngineer == "")
                    {

                    }
                    else
                    {
                        try
                        {
                            blEngineer = Boolean.Parse(strEngineer);
                        }
                        catch (Exception)
                        {
                            blEngineer = true;
                        }
                    }

                    BaseParSetAdjust baseParSetAdjust = new BaseParSetAdjust()
                    {
                        No = i,
                        Name = name,
                        StrIncrement = strIncrement,
                        Min = min,
                        Max = max,

                        Worker = blWorker,
                        Engineer = blEngineer
                    };
                    g_ParSetAdjust_L.Add(baseParSetAdjust);
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 读取参数

        #region 写入参数
        /// <summary>
        /// 将参数写入Ini
        /// </summary>
        public void WriteIni()
        {
            try
            {
                for (int i = 0; i < C_NumValue; i++)
                {
                    string  basekey = "Value" + (i + 1).ToString();
                    IniFile.I_I.WriteIni(g_Section, "Name" + basekey, g_ParSetAdjust_L[i].Name, C_PathSavePar);
                    IniFile.I_I.WriteIni(g_Section, "Increment" + basekey, g_ParSetAdjust_L[i].StrIncrement, C_PathSavePar);
                    IniFile.I_I.WriteIni(g_Section, "Min" + basekey, g_ParSetAdjust_L[i].Min.ToString(), C_PathSavePar);
                    IniFile.I_I.WriteIni(g_Section, "Max" + basekey, g_ParSetAdjust_L[i].Max.ToString(), C_PathSavePar);

                    IniFile.I_I.WriteIni(g_Section, "Worker" + basekey, g_ParSetAdjust_L[i].Worker.ToString(), C_PathSavePar);
                    IniFile.I_I.WriteIni(g_Section, "Engineer" + basekey, g_ParSetAdjust_L[i].Engineer.ToString(), C_PathSavePar);
                }
                IniFile.I_I.WriteIni(g_Section, "Title", ParSetAdjust.P_I.g_Title, C_PathSavePar);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParSetAdjust", ex);
            }
        }
        #endregion 写入参数
    }


    public class BaseParSetAdjust
    {
        //序号
        public int No { set; get; }
        //数据类型
        public string Type
        {
            get
            {
                return "Value" + (No + 1).ToString();
            }
        }
        public string Name { set; get; }

        //小数点
        public int Increment
        {
            get
            {
                try
                {
                    return int.Parse(StrIncrement.Replace("Num",""));
                }
                catch
                {
                    return 0;
                }
            }
        }
        public string StrIncrement { set; get; }
        //最小最大值
        public double Min { set; get; }
        public double Max { set; get; }

        //权限
        public bool Worker { set; get; }
        public bool Engineer { set; get; }

        public bool BlChange//是否设置过
        {
            get
            {
                if (Name == "Value" + (No + 1).ToString()
                    && StrIncrement == "Num2"
                    && Min == int.MinValue
                    && Max == int.MaxValue
                    && Worker == true
                    && Engineer == true)
                {
                    return false;
                }
                return true;
            }
        }
    }

}
