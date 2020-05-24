using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using BasicClass;
using DealFile;

namespace DealPLC
{
    public class RegData : BaseRegPLC
    {
        #region 静态类实例
        public static RegData R_I = new RegData();
        #endregion 静态类实例

        #region 定义
        //索引器
        public new RegPLC this[int index]
        {
            get
            {
                return Reg_L[index];
            }                   
        }

        string[] PCSendPLC_Custom
        {
            get
            {
                string[] reg = new string[base.NumReg];
                for (int i = 0; i < base.NumReg; i++)
                {
                    reg[i]="Y";
                }
                return reg;
            }
        }
        /// <summary>
        /// 注释
        /// </summary>
        public override string[] Annotation
        {
            get
            {
                try
                {
                    string[] annotation = new string[base.NumReg];
                    for (int i = 0; i < base.NumReg; i++)
                    {
                        annotation[i] = "";
                    }
                    annotation[0] = "通知PLC软件是否打开";
                    annotation[1] = "通知PLC软件报警";
                    annotation[2] = "PC机器人握手状态";
                    annotation[3] = "通知PLC写入配方数据完成";
                    annotation[4] = "PLC皮带线单次运行时间";
                    return annotation;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }            
        }

        double[] Co_Custom
        {
            get
            {
                double[] co = new double[base.NumReg];
                for (int i = 0; i < base.NumReg; i++)
                {
                    co[i] = 10000;
                }
                co[0] = 1;
                co[1] = 1;
                co[2] = 1;
                co[3] = 1;
                return co;
            }
        }
        #endregion 定义

        #region 初始化
        public RegData()
        {
            base.NameClass = "RegData";

            //ini存储地址
            base.PathRegIni = ParPathRoot.PathRoot + "Store\\PLC\\RegData.ini";           
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            try
            {
                base.PCSendPLC = PCSendPLC_Custom;
                base.PLCSendPC = new string[base.NumReg];

                //数据寄存器的系数
                base.Co = Co_Custom;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化     
    }
}
