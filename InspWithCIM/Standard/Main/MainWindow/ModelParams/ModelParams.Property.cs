using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealConfigFile;

namespace Main
{
    partial class ModelParams
    {
        #region setwork
        /// <summary>
        /// 运行设定模式-是否记录中间数据
        /// </summary>
        public static bool DefaultRunCardOK
        {
            get
            {
                return !IfCimOn;//ParSetWork.P_I.WorkSelect_L[0].BlSelect || !DealCIM.CIM.PlcCimOn;
            }
        }

        /// <summary>
        /// 运行设定模式-是否读取二维码
        /// </summary>
        public static bool DefaultQrCodeOK
        {
            get
            {
                return !DealCIM.PostParams.P_I.BlCodeOn;//ParSetWork.P_I.WorkSelect_L[1].BlSelect;
            }
        }

        /// <summary>
        /// 运行设定模式-是否过账
        /// </summary>
        public static bool DefaultTrackOutOK
        {
            get
            {
                return !IfCimOn;//ParSetWork.P_I.WorkSelect_L[3].BlSelect || !DealCIM.CIM.PlcCimOn;
            }
        }

        public static bool DefaultChipIDOK
        {
            get
            {
                return !DealCIM.PostParams.P_I.BlCimOn || !DealCIM.PostParams.P_I.BlVerifyChipID;//ParSetWork.P_I.WorkSelect_L[2].BlSelect || !DealCIM.CIM.PlcCimOn;
            }
        }

        public static bool IfPassCodeNG
        {
            get
            {
                return DealCIM.PostParams.P_I.BlPassVerifyCode && DealCIM.PostParams.P_I.BlPassVerifyCodeEnabled;//ParSetWork.P_I.WorkSelect_L[4].BlSelect;
            }
        }

        public static bool IfCimOn
        {
            get
            {
                return DealCIM.PostParams.P_I.BlCodeOn && DealCIM.PostParams.P_I.BlCimOn;
            }
        }

        /// <summary>
        /// 是否碎片化删图
        /// </summary>
        public static bool IfSegmentDelImage
        {
            get
            {
                return true;//ParSetWork.P_I.WorkSelect_L[5].BlSelect;
            }
        }

        public static bool IfGetCodeFromPLC
        {
            get
            {
                return ParSetWork.P_I.WorkSelect_L[6].BlSelect;
            }
        }

        public static bool IfUsingSpl
        {
            get
            {
                return true;//ParSetWork.P_I.WorkSelect_L[7].BlSelect;
            }
        }

        public static bool IfLine13
        {
            get
            {
                return ParSetWork.P_I[7].BlSelect;
            }
        }

        /// <summary>
        /// 如果本软件不做code传递，那么在
        /// </summary>
        public static bool IfNoCodeTrans
        {
            get
            {
                return true;// ParSetWork.P_I.WorkSelect_L[8].BlSelect;
            }
        }

        public static bool IfSendResultToRobot
        {
            get
            {
                return ParSetWork.P_I.WorkSelect_L[8].BlSelect;
            }
        }
        #endregion
    }
}
