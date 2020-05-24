using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using System.IO;
using BasicClass;
using Common;

namespace SetPar
{
    public class ParMemory : BaseClass
    {
        #region 静态类实例
        public static ParMemory P_I = new ParMemory();
        #endregion 静态类实例

        #region 定义
        public string PathMemory
        {
            get
            {
                return ComValue.c_PathSetPar + "Memory.ini";
            }
        }
        //double
        public double Max = 700000;//内存允许的最大值

        public bool BlRecord = false;//是否记录内存
        public bool BlGCCollect = false;//是否记录内存
        #endregion 定义

        #region 初始化
        public ParMemory()
        {
            NameClass = "ParMemory";
        }
        #endregion 初始化


        /// <summary>
        /// 读取文件属性设置参数
        /// </summary>
        public void ReadIniPar()
        {
            try
            {
                Max = IniFile.I_I.ReadIniInt("Momery", "Max", 700000, PathMemory);
                BlRecord = IniFile.I_I.ReadIniBl("Momery", "Record", false, PathMemory);
                BlGCCollect = IniFile.I_I.ReadIniBl("Momery", "GCCollect", false, PathMemory);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool WriteIniPar()
        {
            try
            {
                IniFile.I_I.WriteIni("Momery", "Max", Max.ToString(), PathMemory);
                IniFile.I_I.WriteIni("Momery", "Record", BlRecord.ToString(), PathMemory);
                IniFile.I_I.WriteIni("Momery", "GCCollect", BlGCCollect.ToString(), PathMemory);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
    }
}
