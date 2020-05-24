using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using BasicClass;
using System.IO;

namespace DealPLC
{
    public class BaseRegPLC : BaseClass
    {
        #region 定义
        public string PathRegIni = "";//ini存储地址
        public string regStart = "";// 起始触发寄存器
        public virtual string[] PLCSendPC { get; set; }
        public virtual string[] PCSendPLC { get; set; }
        public virtual string[] Annotation { get; set; }
        public virtual double[] Co { get; set; }//数据寄存器的系数
        public List<RegPLC> Reg_L = new List<RegPLC>();//寄存器的集合

        public List<RegPLC> RegWrite_L = new List<RegPLC>();//可写入寄存器的集合

        #region int
       
        /// <summary>
        /// 寄存器个数
        /// </summary>
        public virtual int NumReg
        {
            get
            {
                return NumRegSet;
            }
            set
            {
                NumRegSet = value;
            }
        }

        int numRegSet = 10;
        public virtual int NumRegSet
        {
            get
            {
                //if (numRegSet < 20)
                //{
                //    return 20;
                //}
                return numRegSet;
            }
            set
            {
                numRegSet = value;
            }
        }

        public int Count
        {
            get
            {
                return Reg_L.Count;
            }
        }
        #endregion int

        /// <summary>
        /// 索引器
        /// </summary>
        public virtual RegPLC this[int index]
        {
            get
            {
                try
                {
                    return Reg_L[index];
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        #endregion 定义

        #region 初始化
        public virtual void Init()
        {

        }
        #endregion 初始化

        /// <summary>
        /// 写入保存Ini
        /// </summary>
        /// <returns></returns>
        public bool WriteIni()
        {
            try
            {
                //先删除旧文件
                if (File.Exists(PathRegIni))
                {
                    File.Delete(PathRegIni);
                }
                //起始寄存器
                IniFile.I_I.WriteIni("StartReg", "RegName", regStart, PathRegIni);
                IniFile.I_I.WriteIni("NumRegSet", "NumRegSet", NumRegSet.ToString(), PathRegIni);
                for (int i = 0; i < NumReg; i++)
                {
                    IniFile.I_I.WriteIni(Reg_L[i].No.ToString(), "No", Reg_L[i].No.ToString(), PathRegIni);
                    IniFile.I_I.WriteIni(Reg_L[i].No.ToString(), "RegName", Reg_L[i].NameReg, PathRegIni);
                    IniFile.I_I.WriteIni(Reg_L[i].No.ToString(), "NameCustomReg", Reg_L[i].NameCustomReg, PathRegIni);
                    IniFile.I_I.WriteIni(Reg_L[i].No.ToString(), "PLCSend", Reg_L[i].PLCSend, PathRegIni);
                    IniFile.I_I.WriteIni(Reg_L[i].No.ToString(), "PCSend", Reg_L[i].PCSend, PathRegIni);
                    IniFile.I_I.WriteIni(Reg_L[i].No.ToString(), "Co", Reg_L[i].Co.ToString(), PathRegIni);
                    IniFile.I_I.WriteIni(Reg_L[i].No.ToString(), "Min", Reg_L[i].DblMin.ToString(), PathRegIni);
                    IniFile.I_I.WriteIni(Reg_L[i].No.ToString(), "Max", Reg_L[i].DblMax.ToString(), PathRegIni);
                    IniFile.I_I.WriteIni(Reg_L[i].No.ToString(), "Annotation", Reg_L[i].Annotation, PathRegIni);
                }

                //生成写入PLC的寄存器
                CreateRegWrite();
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        ///  读取寄存器
        /// </summary>
        /// <returns></returns>
        public virtual bool ReadIni()
        {
            bool blRight = false;
            try
            {
                //起始寄存器
                regStart = IniFile.I_I.ReadIniStr("StartReg", "RegName", "D0", PathRegIni);
                //寄存器个数
                NumRegSet = IniFile.I_I.ReadIniInt("NumRegSet", "NumRegSet", PathRegIni);
                for (int i = 0; i < NumReg; i++)
                {
                    string nameSection = i.ToString();
                    string strSelect = IniFile.I_I.ReadIniStr(nameSection, "Select", PathRegIni);
                    string nameReg = IniFile.I_I.ReadIniStr(nameSection, "RegName", PathRegIni);
                    string customReg = IniFile.I_I.ReadIniStr(nameSection, "NameCustomReg", PathRegIni);
                    string PLCSend = IniFile.I_I.ReadIniStr(nameSection, "PLCSend", PathRegIni);
                    string PCSend = IniFile.I_I.ReadIniStr(nameSection, "PCSend", PathRegIni);
                    double min = IniFile.I_I.ReadIniDbl(nameSection, "Min", 0, PathRegIni);
                    double max = IniFile.I_I.ReadIniDbl(nameSection, "Max", int.MaxValue, PathRegIni);
                    double co = IniFile.I_I.ReadIniDbl(nameSection, "Co", 1, PathRegIni);
                    string annotation = IniFile.I_I.ReadIniStr(nameSection, "Annotation", PathRegIni);

                    RegPLC inst = new RegPLC()
                    {
                        No = i,
                        NameReg = nameReg,
                        NameCustomReg = customReg,
                        PLCSend = PLCSend,
                        PCSend = PCSend,
                        DblMin = min,
                        DblMax = max,
                        Co = co,
                        Annotation = annotation,
                        //Explain = Annotation[i],
                    };
                    Reg_L.Add(inst);
                }

                //生成写入PLC的寄存器
                CreateRegWrite();
            }
            catch (Exception ex)
            {
                blRight = false;
                Log.L_I.WriteError(NameClass, ex);
            }
            return blRight;
        }

        /// <summary>
        /// 创建可以写入PLC的寄存器
        /// </summary>
        void CreateRegWrite()
        {
            int no = 0;
            try
            {
                RegWrite_L.Clear();
                for (int i = 0; i < Reg_L.Count; i++)
                {
                    if (Reg_L[i].PCSend == "Y")
                    {
                        no++;
                        RegWrite_L.Add(new RegPLC()
                        {
                            No = no,
                            NameReg = Reg_L[i].NameReg,
                            PCSend = Reg_L[i].PCSend,
                            DblMin = Reg_L[i].DblMin,
                            DblMax = Reg_L[i].DblMax,
                            Co = Reg_L[i].Co,
                            Annotation = Reg_L[i].Annotation,
                            //Explain = Annotation[i],
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
    }

    public enum RegType_e
    {
        OneReg = 1,
        TwoReg = 2
    }

    //寄存器基类
    public class RegPLC:BaseClass
    {
        public string NameReg { get; set; } //寄存器名称       
        public string NameCustomReg { get; set; } //自定义寄存器
        public string PLCSend { get; set; } //PLC-->PC
        public string PCSend { get; set; } //PC-->DealPLC        
        public double DblMax { get; set; } //最大值
        public double DblMin { get; set; } //最小值
        public double Co { get; set; } //换算系数
        public string Explain { get; set; }//说明
    }
    /// <summary>
    /// 寄存器位数
    /// </summary>
    public enum BitReg_enum
    {
        OneReg,
        TwoReg
    }
}
