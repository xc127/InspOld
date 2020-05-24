using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using Common;
using BasicClass;
using System.IO;

namespace DealConfigFile
{
    public class ParSetWork :BaseClass
    {
        #region 静态类实例
        public static ParSetWork P_I = new ParSetWork();
        #endregion 静态类实例

        #region 定义      
        //Path
        string c_PathSetWork
        {
            get
            {
                string path = ParPathRoot.PathRoot + "store\\ParSysWork\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path + "SetWorkType.ini";
            }
        }

        //int 
        public const int C_NumWork = 10;

        //运行控制设置
        public List<WorkSelect> WorkSelect_L = new List<WorkSelect>();

        public WorkSelect this[int index]
        {
            get
            {
                try
                {
                    return WorkSelect_L[index];
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        #endregion 定义

        #region 读写Ini
        public void WriteIniPar()
        {
            try
            {
                for (int i = 0; i < C_NumWork; i++)
                {
                    IniFile.I_I.WriteIni("Type" + i.ToString(), "Select", WorkSelect_L[i].BlSelect.ToString(), c_PathSetWork);
                    IniFile.I_I.WriteIni("Type" + i.ToString(), "Annotation", WorkSelect_L[i].Annotation, c_PathSetWork);
                }
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError("ParSetWork", ex);
            }
        }

        public void ReadIniPar()
        {
            try
            {
                for (int i = 0; i < C_NumWork; i++)
                {
                    base.StrBool = IniFile.I_I.ReadIniStr("Type" + i.ToString(), "Select", c_PathSetWork);
                    bool blSelect = Convert.ToBoolean(base.StrBool);               
                    string Annotation = IniFile.I_I.ReadIniStr("Type" + i.ToString(), "Annotation", c_PathSetWork);

                    WorkSelect_L.Add(new WorkSelect()
                    {
                        No=i,
                        BlSelect = blSelect,
                        Annotation = Annotation
                    });                  
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParSetWork", ex);
            }
        }
        #endregion 读写Ini
    }

    public class WorkSelect:BaseClass
    {
        public bool BlSelect { get; set; }
    }
}
