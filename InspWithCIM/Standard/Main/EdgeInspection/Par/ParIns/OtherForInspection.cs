using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using BasicClass;
using System.IO;

namespace Main
{
    public class ResultInspection
    {
        /// <summary>
        /// 图片是否有效
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// 是否为角
        /// </summary>
        public bool IsCorner = false;
        /// <summary>
        /// 图片保存的路径
        /// </summary>
        public string ImagePath { get; set; }
        public string ImageDefectPaintedPath { get; set; }
        public HTuple MeanGray;
        public HTuple Sharpness;
        /// <summary>
        /// 文件名
        /// </summary>
        public string ImageSafeName
        {
            get
            {
                return ImagePath.Substring(ImagePath.LastIndexOf(@"\") + 1);
            }
        }
        /// <summary>
        /// 该图片对应的二维码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 对应第几条边
        /// </summary>
        public int SideInfo { get; set; }

        /// <summary>
        /// 对应相机序号
        /// </summary>
        public int NoCamera { get; set; }

        List<FaultInfo> singleFalutInfo_L = new List<FaultInfo>();
        /// <summary>
        /// 单张图像缺陷列表
        /// </summary>
        public List<FaultInfo> SingleFalutInfo_L
        {
            get
            {
                return singleFalutInfo_L;
            }
            set
            {
                singleFalutInfo_L = value;
            }
        }

        /// <summary>
        /// 缺陷规格String
        /// </summary>
        public string FaultInfoStr
        {
            get
            {
                string str = "";
                if (SingleFalutInfo_L.Count > 0)
                {
                    foreach (FaultInfo faultinfo in SingleFalutInfo_L)
                    {
                        str += string.Format("({0},{1}) ", faultinfo.DepthFault.ToString("f3"), faultinfo.WidthFault.ToString("f3"));
                    }
                }
                return str;
            }
        }

        /// <summary>
        /// 缺陷位置String
        /// </summary>
        public string FaultPos
        {
            get
            {
                string str = "";
                if (SingleFalutInfo_L.Count > 0)
                {
                    foreach (FaultInfo faultinfo in SingleFalutInfo_L)
                    {
                        str += string.Format("({0},{1})  ", faultinfo.PosFalut.DblValue1.ToString("f4"), faultinfo.PosFalut.DblValue2.ToString("f4"));
                    }
                }
                return str.Trim();
            }
        }

        /// <summary>
        /// 缺陷类型统计
        /// </summary>
        public string FaultTypeStr
        {
            get
            {
                string info = "";
                try
                {
                    if (SingleFalutInfo_L.Count > 0)
                    {
                        info = "";
                        foreach (FaultInfo faultinfo in SingleFalutInfo_L)
                        {
                            info += faultinfo.FaultType_E.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError("ResultInspection", ex);
                }
                return info;
            }

        }

        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 此结果对应的图片序号
        /// </summary>
        public int IndexImage { get; set; }

    }

    public class FaultInfo
    {
        /// <summary>
        /// 缺陷类型,OK图片则为Null
        /// </summary>
        public FaultType_Enum FaultType_E { get; set; }
        /// <summary>
        /// 缺陷存在的位置
        /// </summary>
        public Point2D PosFalut { get; set; }
        /// <summary>
        /// 缺陷宽度
        /// </summary>
        public double WidthFault { get; set; }
        /// <summary>
        /// 缺陷深度
        /// </summary>
        public double DepthFault { get; set; }
    }

    public enum FaultType_Enum
    {
        无,
        破角,
        贝壳,
        凸边,
        凹边,
        位置异常,
    }
}
