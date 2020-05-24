using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using BasicClass;
using DealFile;

namespace DealPLC
{
    public class RegData4 : BaseRegPLC
    {
        #region 静态类实例
        public static RegData4 R_I = new RegData4();
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
                    reg[i] = "Y";
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
                return co;
            }
        }
        #endregion 定义

        #region 初始化
        public RegData4()
        {
            base.NameClass = "RegData4";

            //ini存储地址
            base.PathRegIni = ParPathRoot.PathRoot + "Store\\PLC\\RegData4.ini";
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
                Log.L_I.WriteError("ParSetPLC", ex);
            }
        }
        #endregion 初始化

        
    }
}
