using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using BasicClass;
using System.IO;

namespace Main
{
    /// <summary>
    /// 巡边检测参数
    /// 根据目前用到的值生产字段
    /// </summary>
    public partial class ParInspection
    {
        public ParInspection(string nameCam)
        {
            NameCam = nameCam;
        }
        #region 定义
        public string BasePathIni
        {
            get
            {
                return Common.ComConfigPar.C_I.PathRoot + "ParInsp.ini";
            }
        }

        #endregion
        public string NameCam { get; set; }

        //六个阈值
        public double ThShellX { get; set; }
        public double ThShellY { get; set; }
        public double ThCornerX { get; set; }
        public double ThCornerY { get; set; }
        /// <summary>
        /// 且或或的逻辑关系判断破角
        /// </summary>
        public bool UsingORToCorner { get; set; }
        public bool IsCrackEnabled { get; set; } = false;
        /// <summary>
        /// 凹边
        /// </summary>
        public double ThPreiectionX { get; set; }
        public double ThPreiectionY { get; set; }
        /// <summary>
        /// 凸边
        /// </summary>
        public double ThConvexX { get; set; }
        public double ThConvexY { get; set; }
        /// <summary>
        /// 裂纹宽度阈值
        /// </summary>
        public double ThCrackX { get; set; }
        /// <summary>
        /// 裂纹深度阈值
        /// </summary>
        public double ThCrackY { get; set; }
        /// <summary>
        /// 二值化阈值 1
        /// </summary>
        public int ThresholdGlass { get; set; }
        /// <summary>
        /// 边界阈值 1
        /// </summary>
        public int ThresholdEdge { get; set; }
        /// <summary>
        /// 毛刺剔除宽度高度
        /// </summary>
        public double SmoothWidth { get; set; }
        public double SmoothHeight { get; set; }
        /// <summary>
        /// 电极检测外扩像素
        /// </summary>
        public double DblOutRate { get; set; }
        /// <summary>
        /// 电极的灰度值阈值
        /// </summary>
        public int ThITO { get; set; }
        /// <summary>
        /// 放大系数
        /// </summary>
        public double Amp { get; set; }
        /// <summary>
        /// 矩形度阈值
        /// </summary>
        public double ThRectangleularity { get; set; }

        /// <summary>
        /// 忽略边1缺陷
        /// </summary>
        public bool BlIgnoreThisSideFault { get; set; }

        /// <summary>
        /// 标记为CF面检测
        /// </summary>
        public bool BlInspCF { get; set; }

        /// <summary>
        /// 此参数对应的边
        /// </summary>
        public string StrSidesMatch { get; set; }

        public int[] IntArrSidesMatch
        {
            get
            {
                string[] str = StrSidesMatch.Split(',');
                if (str[0] != "" && str.Length != 0)
                {
                    //return Array.ConvertAll<string, int>(str, (string s) => { return Convert.ToInt32(s); });
                    return Array.ConvertAll<string, int>(str, Convert.ToInt32);
                }
                else
                {
                    return new int[] { 0 };
                }
            }
        }

        /// <summary>
        /// 被定义为角的开始位置时玻璃序号
        /// </summary>
        public int IntNumDefCornerWhenStart { get; set; }

        /// <summary>
        /// 被定义为角的结束位置的玻璃序号
        /// </summary>
        public int IntNumDefCornerWhenEnd { get; set; }
        /// <summary>
        /// 不通过Mark来判断OK NG
        /// </summary>
        public bool JudgeMentMark { get; set; }

        /// <summary>
        /// 戴金林处附加功能，图片起始序号
        /// </summary>
        public int SplStartIndex1 { get; set; }

        /// <summary>
        ///图片结束序号 
        /// </summary>
        public int SplEndIndex1 { get; set; }

        /// <summary>
        /// 戴金林处附加功能，图片起始序号
        /// </summary>
        public int SplStartIndex2 { get; set; }

        /// <summary>
        ///图片结束序号 
        /// </summary>
        public int SplEndIndex2 { get; set; }

        /// <summary>
        /// 戴金林处附加功能，图片起始序号
        /// </summary>
        public int SplStartIndex3 { get; set; }

        /// <summary>
        ///图片结束序号 
        /// </summary>
        public int SplEndIndex3 { get; set; }

        /// <summary>
        /// 戴金林处附加功能，图片起始序号
        /// </summary>
        public int SplStartIndex4 { get; set; }

        /// <summary>
        ///图片结束序号 
        /// </summary>
        public int SplEndIndex4 { get; set; }


        /// <summary>
        /// 戴金林处附加功能，图片起始序号
        /// </summary>
        public int SplStartIndex5 { get; set; }

        /// <summary>
        ///图片结束序号 
        /// </summary>
        public int SplEndIndex5 { get; set; }

        /// <summary>
        /// 戴金林处附加功能，图片起始序号
        /// </summary>
        public int SplStartIndex6 { get; set; }

        /// <summary>
        ///图片结束序号 
        /// </summary>
        public int SplEndIndex6 { get; set; }

        /// <summary>
        /// 戴金林处附加功能，图片起始序号
        /// </summary>
        public int SplStartIndex7 { get; set; }

        /// <summary>
        ///图片结束序号 
        /// </summary>
        public int SplEndIndex7 { get; set; }

        /// <summary>
        /// 戴金林处附加功能，图片起始序号
        /// </summary>
        public int SplStartIndex8 { get; set; }

        /// <summary>
        ///图片结束序号 
        /// </summary>
        public int SplEndIndex8 { get; set; }

        /// <summary>
        /// 戴金林处附加功能，图片起始序号
        /// </summary>
        public int SplStartIndex9 { get; set; }

        /// <summary>
        ///图片结束序号 
        /// </summary>
        public int SplEndIndex9 { get; set; }

        /// <summary>
        /// 截取序号的贝壳深度卡控阈值
        /// </summary>
        public double SplThShellY1 { get; set; }

        /// <summary>
        /// 截取序号的贝壳宽度卡控阈值
        /// </summary>
        public double SplThShellX1 { get; set; }

        /// <summary>
        /// 截取序号的贝壳深度卡控阈值
        /// </summary>
        public double SplThShellY2 { get; set; }

        /// <summary>
        /// 截取序号的贝壳宽度卡控阈值
        /// </summary>
        public double SplThShellX2 { get; set; }

        /// <summary>
        /// 截取序号的贝壳深度卡控阈值
        /// </summary>
        public double SplThShellY3 { get; set; }

        /// <summary>
        /// 截取序号的贝壳宽度卡控阈值
        /// </summary>
        public double SplThShellX3 { get; set; }

        /// <summary>
        /// 截取序号的贝壳深度卡控阈值
        /// </summary>
        public double SplThShellY4 { get; set; }

        /// <summary>
        /// 截取序号的贝壳宽度卡控阈值
        /// </summary>
        public double SplThShellX4 { get; set; }

        /// <summary>
        /// 截取序号的贝壳深度卡控阈值
        /// </summary>
        public double SplThShellY5 { get; set; }

        /// <summary>
        /// 截取序号的贝壳宽度卡控阈值
        /// </summary>
        public double SplThShellX5 { get; set; }

        /// <summary>
        /// 截取序号的贝壳深度卡控阈值
        /// </summary>
        public double SplThShellY6 { get; set; }

        /// <summary>
        /// 截取序号的贝壳宽度卡控阈值
        /// </summary>
        public double SplThShellX6 { get; set; }

        /// <summary>
        /// 截取序号的贝壳深度卡控阈值
        /// </summary>
        public double SplThShellY7 { get; set; }

        /// <summary>
        /// 截取序号的贝壳宽度卡控阈值
        /// </summary>
        public double SplThShellX7 { get; set; }

        /// <summary>
        /// 截取序号的贝壳深度卡控阈值
        /// </summary>
        public double SplThShellY8 { get; set; }

        /// <summary>
        /// 截取序号的贝壳宽度卡控阈值
        /// </summary>
        public double SplThShellX8 { get; set; }

        /// <summary>
        /// 截取序号的贝壳深度卡控阈值
        /// </summary>
        public double SplThShellY9 { get; set; }

        /// <summary>
        /// 截取序号的贝壳宽度卡控阈值
        /// </summary>
        public double SplThShellX9 { get; set; }

        /// <summary>
        /// 截取序号的凹边深度卡控
        /// </summary>
        public double SplThPerY1 { get; set; }
        /// <summary>
        /// 截取序号的凹边深度卡控
        /// </summary>
        public double SplThPerY2 { get; set; }
        /// <summary>
        /// 截取序号的凹边深度卡控
        /// </summary>
        public double SplThPerY3 { get; set; }
        /// <summary>
        /// 截取序号的凹边深度卡控
        /// </summary>
        public double SplThPerY4 { get; set; }
        /// <summary>
        /// 截取序号的凹边深度卡控
        /// </summary>
        public double SplThPerY5 { get; set; }
        /// <summary>
        /// 截取序号的凹边深度卡控
        /// </summary>
        public double SplThPerY6 { get; set; }
        /// <summary>
        /// 截取序号的凹边深度卡控
        /// </summary>
        public double SplThPerY7 { get; set; }
        /// <summary>
        /// 截取序号的凹边深度卡控
        /// </summary>
        public double SplThPerY8 { get; set; }
        /// <summary>
        /// 截取序号的凹边深度卡控
        /// </summary>
        public double SplThPerY9 { get; set; }
    }
}
