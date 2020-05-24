using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using BasicClass;

namespace Main
{
    public partial class ParAnalysis
    {
        #region 静态类实例
        public static ParAnalysis P_I = new ParAnalysis();
        #endregion

        public readonly string r_StrPathFile = ParPathRoot.PathRoot + "Store\\Custom\\ProductNumRecord.ini";

        //当前班次产量，班次信息等
        /// <summary>
        /// 当班生产情况
        /// </summary>
        public ProductNumInfo g_ProductNumInfoNow = new ProductNumInfo();


        /// <summary>
        /// 最近一个月生产情况
        /// </summary>
        public List<ProductNumInfo> g_NumPreMonth_L = new List<ProductNumInfo>();

        string dateOld = "";
        /// <summary>
        /// 当前日期、班次
        /// </summary>
        public string DateNow
        {
            get
            {
                string dateNow = "";
                try
                {
                    DateTime dt = DateTime.Now;
                    int hour = dt.Hour;
                    if (hour < 8)
                    {
                        dateNow = dt.AddDays(-1).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        dateNow = dt.ToString("yyyy-MM-dd");
                    }
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError("ParAnalysis", ex);
                }
                return dateNow;
            }
        }

        string shiftOld = "";
        /// <summary>
        /// 当前班次
        /// </summary>
        public string ShiftNow
        {
            get
            {
                double hour = DateTime.Now.Hour;
                string shiftNow = hour >= 20 || hour <= 8 ? "N" : "D";
                if (shiftNow != shiftOld || dateOld != DateNow)
                {
                    g_ProductNumInfoNow.NumAll = 0;
                    g_ProductNumInfoNow.NumOK = 0;
                    g_ProductNumInfoNow.NumNG = 0;
                    g_ProductNumInfoNow.NumNGShell = 0;
                    g_ProductNumInfoNow.NumNGCorner = 0;
                    g_ProductNumInfoNow.NumNGOther = 0;
                }
                shiftOld = shiftNow;
                dateOld = DateNow;
                return shiftNow;
            }
        }
    }

    public class ProductNumInfo
    {
        /// <summary>
        /// 不在getset里生成相关数据是为了方便这个类可以用于显示多班次的信息
        /// 使用属性块是为了绑定到界面
        /// </summary>
        public int NumAll { get; set; }
        public int NumOK { get; set; }
        public int NumNG { get; set; }
        public int NumNGShell { get; set; }
        public int NumNGCorner { get; set; }
        public int NumNGOther { get; set; }
    }
}
