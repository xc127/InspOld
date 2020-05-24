using BasicClass;
using DealConfigFile;
using System;

namespace Main
{
    public partial class ModelParams
    {
        #region 静态类实例
        //public static ModelParams M_I = new ModelParams();
        #endregion 静态类实例

        static string codeWaitTime = "adj1";
        static string grabInterval = "std1";
        static string convexJudgeRatio = "std2";
        static string retentionTime = "std3";
        public static int CodeWaitTimeBefore
        {
            get
            {
                return (int)ParAdjust.Value1(codeWaitTime);
            }
        }

        public static int CodeWaitTimeAfter
        {
            get
            {
                return (int)ParAdjust.Value2(codeWaitTime);
            }
        }

        public static int GrabInterval
        {
            get
            {
                int interval = (int)ParStd.Value1(grabInterval);
                return interval == 0 ? 5 : interval;
            }
        }

        public static int CodeLength
        {
            get
            {
                int len= (int)ParAdjust.Value1(strCodeLenght);
                return len == 0 ? 5 : len;
            }
        }

        public static double ConvexJudgeRatio
        {
            get
            {
                return ParStd.Value1(convexJudgeRatio) == 0 ? (double)1 / 3 : ParStd.Value1(convexJudgeRatio);
            }
        }

        public static int RetentionTime
        {
            get
            {
                return (int)(ParStd.Value1(retentionTime) == 0 ? 30 : ParStd.Value1(retentionTime));
            }
        }

        const string Plat1Com = "adj2";
        const string Plat2Com = "adj3";
        const string strCodeLenght = "adj4";

        public static double Plat1ComX
        {
            get
            {
                return ParAdjust.Value1(Plat1Com);
            }
        }

        public static double Plat1ComY
        {
            get
            {
                return ParAdjust.Value2(Plat1Com);
            }
        }

        public static double Plat2ComX
        {
            get
            {
                return ParAdjust.Value1(Plat2Com);
            }
        }

        public static double Plat2ComY
        {
            get
            {
                return ParAdjust.Value2(Plat2Com);
            }
        }
        /// <summary>
        /// 机器人读码结果
        /// </summary>
        public static string cmd_GetCodeOK = "31";
        public static string cmd_GetCodeNG = "31";
        /// <summary>
        /// 机器人chipid过账结果
        /// </summary>
        public static string cmd_ChipIDValid = "32";
        public static string cmd_ChipIDInvalid = "32";
    }
}
