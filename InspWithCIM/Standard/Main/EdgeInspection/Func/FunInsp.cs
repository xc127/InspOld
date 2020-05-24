using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using BasicClass;
using DealConfigFile;

namespace Main
{
    public class FunInsp
    {
        static int cornorCnt = 0;
        public void DealImage(ImageAll image, ParInspection par, ref ResultInspection result,ref int No)
        {

            #region 定义
            HObject ho_RawRegion, ho_RegionRawFillUp;
            HObject ho_RegionOpening, ho_RawRectangle, ho_RegionInsp, ho_RegionInspFillUp;
            HObject ho_RegionsDifferenceLacked, ho_FaultLacked, ho_FaultsLacked;
            HObject ho_Rectangle, ho_RegionsDifferenceRested, ho_FaultRested;
            HObject ho_FaultsRested;

            HTuple hv_Width = null, hv_Height = null, hv_Area = null;
            HTuple hv_AreaFault = null, hv_RowFault = null, hv_ColumnFault = null;
            HTuple hv_Row = null, hv_Column = null, hv_Row1Raw = null;
            HTuple hv_Column1Raw = null, hv_Row2Raw = null, hv_Column2Raw = null;
            HTuple hv_RowInsp1 = null, hv_ColumnInsp1 = null, hv_RowInsp2 = null;
            HTuple hv_ColumnInsp2 = null, hv_Rectangularity = null;
            HTuple hv_MinGray = null, hv_MaxGray = null, hv_Range = null;
            HTuple hv_RegionHeight = null;

            HObject ho_Region30 = null, ho_Region30Opening = null;
            HObject ho_ImageLaplace4 = null, ho_ImageLaplace8 = null, ImageResult_Laplace = null, ImageResult_LaplaceMult = null;


            #endregion

            #region 初始化
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_RawRegion);
            HOperatorSet.GenEmptyObj(out ho_RegionRawFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_RawRectangle);
            HOperatorSet.GenEmptyObj(out ho_RegionInsp);
            HOperatorSet.GenEmptyObj(out ho_RegionInspFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionsDifferenceLacked);
            HOperatorSet.GenEmptyObj(out ho_FaultLacked);
            HOperatorSet.GenEmptyObj(out ho_FaultsLacked);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_RegionsDifferenceRested);
            HOperatorSet.GenEmptyObj(out ho_FaultRested);
            HOperatorSet.GenEmptyObj(out ho_FaultsRested);
            HOperatorSet.GenEmptyObj(out ho_Region30);
            HOperatorSet.GenEmptyObj(out ho_Region30Opening);
            #endregion

            try
            {
                HOperatorSet.GetImageSize(image.Ho_Image, out hv_Width, out hv_Height);
                ho_RawRegion.Dispose();
                HOperatorSet.Threshold(image.Ho_Image, out ho_RawRegion, par.ThresholdEdge, 255);

                //获取区域高度                
                //HOperatorSet.RegionFeatures(ho_RawRegion,)
                ho_RegionRawFillUp.Dispose();
                HOperatorSet.FillUp(ho_RawRegion, out ho_RegionRawFillUp);
                ho_RegionOpening.Dispose();
                HOperatorSet.OpeningRectangle1(ho_RegionRawFillUp, out ho_RegionOpening, par.SmoothWidth, par.SmoothHeight);

                //获取平均灰度
                ho_Region30.Dispose();
                HOperatorSet.Threshold(image.Ho_Image, out ho_Region30, 30, 255);
                ho_Region30Opening.Dispose();
                HOperatorSet.Intensity(ho_Region30, image.Ho_Image, out result.MeanGray, out HTuple deviation);

                //获取清晰度
                //HOperatorSet.Laplace(image.Ho_Image, out ho_ImageLaplace4, "signed", 3, "n_4");
                //HOperatorSet.Laplace(image.Ho_Image, out ho_ImageLaplace8, "signed", 3, "n_8");
                //HOperatorSet.AddImage(ho_ImageLaplace4, ho_ImageLaplace4, out ImageResult_Laplace, 1, 0);
                //HOperatorSet.AddImage(ho_ImageLaplace4, ImageResult_Laplace, out ImageResult_Laplace, 1, 0);
                //HOperatorSet.AddImage(ho_ImageLaplace8, ImageResult_Laplace, out ImageResult_Laplace, 1, 0);
                //HOperatorSet.MultImage(ImageResult_Laplace, ImageResult_Laplace, out ImageResult_LaplaceMult, 1, 0);
                //HOperatorSet.Intensity(ImageResult_LaplaceMult, ImageResult_LaplaceMult, out result.Sharpness, out HTuple dev);

                HOperatorSet.AreaCenter(ho_RegionOpening, out hv_Area, out hv_Row, out hv_Column);

                if (hv_Area < 480)
                {
                    result.IsValid = false;
                    return;
                }
                //赋值图片有效
                //对自变量i进行递增操作
                ++No;
                result.IsValid = true;

                if (par.BlInspCF)
                {
                    DealCFOKContour(ho_RegionOpening, out hv_Row1Raw, out hv_Column1Raw, out hv_Row2Raw, out hv_Column2Raw);
                }
                else
                {
                    HOperatorSet.SmallestRectangle1(ho_RegionOpening, out hv_Row1Raw, out hv_Column1Raw, out hv_Row2Raw, out hv_Column2Raw);
                }

                if (hv_Column1Raw > 4 || hv_Column2Raw < hv_Width - 4)
                {
                    result.IsCorner = true;
                    cornorCnt = No;
                }

                ho_RawRectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_RawRectangle, hv_Row1Raw, hv_Column1Raw, hv_Row2Raw,
                    hv_Column2Raw);

                ho_RegionInsp.Dispose();
                HOperatorSet.Threshold(image.Ho_Image, out ho_RegionInsp, par.ThresholdGlass, 255);
                ho_RegionInspFillUp.Dispose();
                if (par.BlInspCF)
                {
                    DealCFRealContour(ho_RegionInsp, out ho_RegionInspFillUp);
                }
                else
                {
                    HOperatorSet.FillUp(ho_RawRegion, out ho_RegionInspFillUp);
                }

                #region 贝壳破角
                //贝壳破角检测
                ho_RegionsDifferenceLacked.Dispose();
                HOperatorSet.Difference(ho_RawRectangle, ho_RegionInspFillUp, out ho_RegionsDifferenceLacked);
                ho_FaultLacked.Dispose();
                HOperatorSet.OpeningRectangle1(ho_RegionsDifferenceLacked, out ho_FaultLacked, par.SmoothWidth, par.SmoothHeight);//筛选一部分

                ho_FaultsLacked.Dispose();
                HOperatorSet.Connection(ho_FaultLacked, out ho_FaultsLacked);                

                HOperatorSet.SmallestRectangle1(ho_FaultsLacked, out hv_RowInsp1, out hv_ColumnInsp1,
                    out hv_RowInsp2, out hv_ColumnInsp2);
                if (hv_RowInsp1.Length > 0)
                {
                    ho_Rectangle.Dispose();
                    HOperatorSet.Rectangularity(ho_FaultsLacked, out hv_Rectangularity);

                    HOperatorSet.GenRectangle1(out ho_Rectangle, hv_RowInsp1, hv_ColumnInsp1 - par.DblOutRate, hv_RowInsp2 + par.DblOutRate, hv_ColumnInsp2 + par.DblOutRate);
                    HOperatorSet.MinMaxGray(ho_FaultsLacked, image.Ho_Image, 0, out hv_MinGray, out hv_MaxGray, out hv_Range);
                    HOperatorSet.AreaCenter(ho_FaultsLacked, out hv_AreaFault, out hv_RowFault, out hv_ColumnFault);

                    for (int i = 0; i < hv_RowInsp1.Length; i++)
                    {
                        if (hv_RowInsp1.ToIArr()[i] > hv_Row1Raw.D + 8)
                        {
                            continue;
                        }

                        if (hv_Rectangularity[i] > par.ThRectangleularity)
                        {
                            continue;
                        }

                        double widPix = hv_ColumnInsp2.ToIArr()[i] - hv_ColumnInsp1.ToIArr()[i];
                        double depthPix = hv_RowInsp2.ToIArr()[i] - hv_RowInsp1.ToIArr()[i];
                        if (depthPix > widPix * 4)
                            continue;
                        if (depthPix > 100 && depthPix > widPix * 2)
                            continue;
                        double widthFault = widPix * par.Amp;
                        double depthFault = depthPix * par.Amp;

                        //if (depthFault < 0.1 || widthFault < 0.3)
                        //{
                        //    continue;
                        //}

                        double preY = par.ThPreiectionY;
                        if (result.IsCorner /*|| No <= cornorCnt + 1*/)
                        {

                            if ((depthFault > par.ThCornerY && widthFault > par.ThCornerX && !par.UsingORToCorner)
                                || ((depthFault > par.ThCornerY || widthFault > par.ThCornerX) && par.UsingORToCorner))
                            {
                                result.SingleFalutInfo_L.Add(new FaultInfo()
                                {
                                    PosFalut = new Point2D((hv_ColumnInsp2.ToIArr()[i] + hv_ColumnInsp1.ToIArr()[i]) / 2, (hv_RowInsp2.ToIArr()[i] + hv_RowInsp1.ToIArr()[i]) / 2),
                                    FaultType_E = FaultType_Enum.破角,
                                    DepthFault = depthFault,
                                    WidthFault = widthFault,
                                });
                            }
                        }
                        else
                        {
                            if (hv_RowInsp2.ToIArr()[i] - hv_RowInsp1.ToIArr()[i] > hv_Row2Raw - hv_Row1Raw - 20 //很长，几乎Y方向和玻璃一样长
                            || hv_AreaFault.ToDArr()[i] / (widPix * depthPix) < 0.4)//缺陷面积很小，只有举行的40%，认为是多方向的裂痕，这个判定主要是针对的CF和TFT在一起的位置
                            {
                                continue;
                            }
                            //这里所有的深度都大于0.05且矩形度都小

                            //增加对图片序号的判断
                            if (ModelParams.IfUsingSpl)
                            {
                                double X = par.ThShellX;
                                double Y = par.ThShellY;
                                int InBox = 0;
                                if (No > par.SplStartIndex1 && No < par.SplEndIndex1)
                                {
                                    X = par.SplThShellX1;
                                    Y = par.SplThShellY1;
                                    preY = par.SplThPerY1;
                                    InBox = 1;
                                }
                                else if (No > par.SplStartIndex2 && No < par.SplEndIndex2)
                                {
                                    X = par.SplThShellX2;
                                    Y = par.SplThShellY2;
                                    preY = par.SplThPerY2;
                                    InBox = 2;
                                }
                                else if (No > par.SplStartIndex3 && No < par.SplEndIndex3)
                                {
                                    X = par.SplThShellX3;
                                    Y = par.SplThShellY3;
                                    preY = par.SplThPerY3;
                                    InBox = 3;
                                }
                                else if (No > par.SplStartIndex4 && No < par.SplEndIndex4)
                                {
                                    X = par.SplThShellX4;
                                    Y = par.SplThShellY4;
                                    preY = par.SplThPerY4;
                                    InBox = 4;
                                }
                                else if (No > par.SplStartIndex5 && No < par.SplEndIndex5)
                                {
                                    X = par.SplThShellX5;
                                    Y = par.SplThShellY5;
                                    preY = par.SplThPerY5;
                                    InBox = 5;
                                }
                                else if (No > par.SplStartIndex6 && No < par.SplEndIndex6)
                                {
                                    X = par.SplThShellX6;
                                    Y = par.SplThShellY6;
                                    preY = par.SplThPerY6;
                                    InBox = 6;
                                }
                                else if (No > par.SplStartIndex7 && No < par.SplEndIndex7)
                                {
                                    X = par.SplThShellX7;
                                    Y = par.SplThShellY7;
                                    preY = par.SplThPerY7;
                                    InBox = 7;
                                }
                                else if (No > par.SplStartIndex8 && No < par.SplEndIndex8)
                                {
                                    X = par.SplThShellX8;
                                    Y = par.SplThShellY8;
                                    preY = par.SplThPerY8;
                                    InBox = 8;
                                }
                                else if (No > par.SplStartIndex9 && No < par.SplEndIndex9)
                                {
                                    X = par.SplThShellX9;
                                    Y = par.SplThShellY9;
                                    preY = par.SplThPerY9;
                                    InBox = 9;
                                }

                                double thShellX = X == 0 ? par.ThShellX : X;
                                double thShellY = Y == 0 ? par.ThShellY : Y;

                                if (InBox > 0)
                                {
                                    if (depthFault > thShellY || widthFault > thShellX || hv_MaxGray > par.ThITO)
                                    {
                                        result.SingleFalutInfo_L.Add(new FaultInfo()
                                        {
                                            PosFalut = new Point2D((hv_ColumnInsp2.ToIArr()[i] + hv_ColumnInsp1.ToIArr()[i]) / 2,
                                                                    (hv_RowInsp2.ToIArr()[i] + hv_RowInsp1.ToIArr()[i]) / 2),
                                            FaultType_E = FaultType_Enum.贝壳,
                                            DepthFault = depthFault,
                                            WidthFault = widthFault,
                                        });
                                    }
                                    continue;
                                }
                            }

                            double thPreY = preY == 0 ? par.ThPreiectionY : preY;
                            //如果不满足图片序号区间，那么默认不使用单独卡控
                            //长宽比小于指定系数的贝壳认为是凹边
                            if (depthFault / widthFault < ModelParams.ConvexJudgeRatio)
                            {
                                if (depthFault > thPreY || widthFault > par.ThPreiectionX)
                                    result.SingleFalutInfo_L.Add(new FaultInfo()
                                    {
                                        PosFalut = new Point2D((hv_ColumnInsp2.ToIArr()[i] + hv_ColumnInsp1.ToIArr()[i]) / 2, (hv_RowInsp2.ToIArr()[i] + hv_RowInsp1.ToIArr()[i]) / 2),
                                        FaultType_E = FaultType_Enum.凹边,
                                        DepthFault = depthFault,
                                        WidthFault = widthFault,
                                    });
                            }
                            else
                            {
                                if (depthFault > par.ThShellY || widthFault > par.ThShellX || hv_MaxGray > par.ThITO)
                                {
                                    result.SingleFalutInfo_L.Add(new FaultInfo()
                                    {
                                        PosFalut = new Point2D((hv_ColumnInsp2.ToIArr()[i] + hv_ColumnInsp1.ToIArr()[i]) / 2, (hv_RowInsp2.ToIArr()[i] + hv_RowInsp1.ToIArr()[i]) / 2),
                                        FaultType_E = FaultType_Enum.贝壳,
                                        DepthFault = depthFault,
                                        WidthFault = widthFault,
                                    });
                                }
                            }
                        }
                    }
                }

                #endregion

                //#region 凸边
                //ho_RegionsDifferenceRested.Dispose();
                //HOperatorSet.Difference(ho_RegionInsp, ho_RawRectangle, out ho_RegionsDifferenceRested);
                //ho_FaultRested.Dispose();
                //HOperatorSet.OpeningRectangle1(ho_RegionsDifferenceRested, out ho_FaultRested, par.SmoothWidth, par.SmoothHeight);

                //ho_FaultsRested.Dispose();
                //HOperatorSet.Connection(ho_FaultRested, out ho_FaultsRested);
                ////循环
                //HOperatorSet.SmallestRectangle1(ho_FaultsRested, out hv_RowInsp1, out hv_ColumnInsp1, out hv_RowInsp2, out hv_ColumnInsp2);

                //if (hv_RowInsp1.Length > 0)
                //{

                //    for (int i = 0; i < hv_RowInsp1.Length; i++)
                //    {
                //        double widthFault = (hv_ColumnInsp2.ToIArr()[i] - hv_ColumnInsp1.ToIArr()[i]) * par.Amp;
                //        double depthFault = (hv_RowInsp2.ToIArr()[i] - hv_RowInsp1.ToIArr()[i]) * par.Amp;

                //        if (depthFault < 0.56)
                //        {
                //            continue;
                //        }

                //        if (depthFault > par.ThPreiectionY || widthFault > par.ThPreiectionX)
                //        {
                //            result.SingleFalutInfo_L.Add(new FaultInfo()
                //            {
                //                PosFalut = new Point2D((hv_ColumnInsp2.ToIArr()[i] + hv_ColumnInsp1.ToIArr()[i]) / 2, (hv_RowInsp2.ToIArr()[i] + hv_RowInsp1.ToIArr()[i]) / 2),
                //                FaultType_E = FaultType_Enum.凸边,
                //                DepthFault = depthFault,
                //                WidthFault = widthFault,
                //            });
                //        }
                //    }
                //}
                //#endregion

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("FunInsp", ex);
            }
            finally
            {
                ho_RawRegion.Dispose();
                ho_RegionRawFillUp.Dispose();
                ho_RegionOpening.Dispose();
                ho_RawRectangle.Dispose();
                ho_RegionInsp.Dispose();
                ho_RegionInspFillUp.Dispose();
                ho_RegionsDifferenceLacked.Dispose();
                ho_FaultLacked.Dispose();
                ho_FaultsLacked.Dispose();
                ho_Rectangle.Dispose();
                ho_RegionsDifferenceRested.Dispose();
                ho_FaultRested.Dispose();
                ho_FaultsRested.Dispose();
            }
        }

        public void DealImage_Manual(ImageAll image, ParInspection par, ref ResultInspection result, ref int No)
        {

            #region 定义
            HObject ho_RawRegion, ho_RegionRawFillUp;
            HObject ho_RegionOpening, ho_RawRectangle, ho_RegionInsp, ho_RegionInspFillUp;
            HObject ho_RegionsDifferenceLacked, ho_FaultLacked, ho_FaultsLacked;
            HObject ho_Rectangle, ho_RegionsDifferenceRested, ho_FaultRested;
            HObject ho_FaultsRested;

            HTuple hv_Width = null, hv_Height = null, hv_Area = null;
            HTuple hv_AreaFault = null, hv_RowFault = null, hv_ColumnFault = null;
            HTuple hv_Row = null, hv_Column = null, hv_Row1Raw = null;
            HTuple hv_Column1Raw = null, hv_Row2Raw = null, hv_Column2Raw = null;
            HTuple hv_RowInsp1 = null, hv_ColumnInsp1 = null, hv_RowInsp2 = null;
            HTuple hv_ColumnInsp2 = null, hv_Rectangularity = null;
            HTuple hv_MinGray = null, hv_MaxGray = null, hv_Range = null;
            HTuple hv_RegionHeight = null;

            HObject ho_Region30 = null, ho_Region30Opening = null;
            HObject ho_ImageLaplace4 = null, ho_ImageLaplace8 = null, ImageResult_Laplace = null, ImageResult_LaplaceMult = null;


            #endregion

            #region 初始化
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_RawRegion);
            HOperatorSet.GenEmptyObj(out ho_RegionRawFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_RawRectangle);
            HOperatorSet.GenEmptyObj(out ho_RegionInsp);
            HOperatorSet.GenEmptyObj(out ho_RegionInspFillUp);
            HOperatorSet.GenEmptyObj(out ho_RegionsDifferenceLacked);
            HOperatorSet.GenEmptyObj(out ho_FaultLacked);
            HOperatorSet.GenEmptyObj(out ho_FaultsLacked);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_RegionsDifferenceRested);
            HOperatorSet.GenEmptyObj(out ho_FaultRested);
            HOperatorSet.GenEmptyObj(out ho_FaultsRested);
            HOperatorSet.GenEmptyObj(out ho_Region30);
            HOperatorSet.GenEmptyObj(out ho_Region30Opening);
            #endregion

            try
            {
                HOperatorSet.GetImageSize(image.Ho_Image, out hv_Width, out hv_Height);
                ho_RawRegion.Dispose();
                HOperatorSet.Threshold(image.Ho_Image, out ho_RawRegion, par.ThresholdEdge, 255);

                //获取区域高度                
                //HOperatorSet.RegionFeatures(ho_RawRegion,)
                ho_RegionRawFillUp.Dispose();
                HOperatorSet.FillUp(ho_RawRegion, out ho_RegionRawFillUp);
                ho_RegionOpening.Dispose();
                HOperatorSet.OpeningRectangle1(ho_RegionRawFillUp, out ho_RegionOpening, par.SmoothWidth, par.SmoothHeight);

                ////获取平均灰度
                //ho_Region30.Dispose();
                //HOperatorSet.Threshold(image.Ho_Image, out ho_Region30, 30, 255);
                //ho_Region30Opening.Dispose();
                //HOperatorSet.Intensity(ho_Region30, image.Ho_Image, out result.MeanGray, out HTuple deviation);

                //获取清晰度
                //HOperatorSet.Laplace(image.Ho_Image, out ho_ImageLaplace4, "signed", 3, "n_4");
                //HOperatorSet.Laplace(image.Ho_Image, out ho_ImageLaplace8, "signed", 3, "n_8");
                //HOperatorSet.AddImage(ho_ImageLaplace4, ho_ImageLaplace4, out ImageResult_Laplace, 1, 0);
                //HOperatorSet.AddImage(ho_ImageLaplace4, ImageResult_Laplace, out ImageResult_Laplace, 1, 0);
                //HOperatorSet.AddImage(ho_ImageLaplace8, ImageResult_Laplace, out ImageResult_Laplace, 1, 0);
                //HOperatorSet.MultImage(ImageResult_Laplace, ImageResult_Laplace, out ImageResult_LaplaceMult, 1, 0);
                //HOperatorSet.Intensity(ImageResult_LaplaceMult, ImageResult_LaplaceMult, out result.Sharpness, out HTuple dev);

                HOperatorSet.AreaCenter(ho_RegionOpening, out hv_Area, out hv_Row, out hv_Column);

                if (hv_Area < 480)
                {
                    result.IsValid = false;
                    return;
                }
                //赋值图片有效
                //对自变量i进行递增操作
                ++No;
                result.IsValid = true;

                if (par.BlInspCF)
                {
                    DealCFOKContour(ho_RegionOpening, out hv_Row1Raw, out hv_Column1Raw, out hv_Row2Raw, out hv_Column2Raw);
                }
                else
                {
                    HOperatorSet.SmallestRectangle1(ho_RegionOpening, out hv_Row1Raw, out hv_Column1Raw, out hv_Row2Raw, out hv_Column2Raw);
                }

                if (hv_Column1Raw > 4 || hv_Column2Raw < hv_Width - 4)
                {
                    result.IsCorner = true;
                    cornorCnt = No;
                }

                ho_RawRectangle.Dispose();
                HOperatorSet.GenRectangle1(out ho_RawRectangle, hv_Row1Raw, hv_Column1Raw, hv_Row2Raw,
                    hv_Column2Raw);

                ho_RegionInsp.Dispose();
                HOperatorSet.Threshold(image.Ho_Image, out ho_RegionInsp, par.ThresholdGlass, 255);
                ho_RegionInspFillUp.Dispose();
                if (par.BlInspCF)
                {
                    DealCFRealContour(ho_RegionInsp, out ho_RegionInspFillUp);
                }
                else
                {
                    HOperatorSet.FillUp(ho_RawRegion, out ho_RegionInspFillUp);
                }

                #region 贝壳破角
                //贝壳破角检测
                ho_RegionsDifferenceLacked.Dispose();
                HOperatorSet.Difference(ho_RawRectangle, ho_RegionInspFillUp, out ho_RegionsDifferenceLacked);
                ho_FaultLacked.Dispose();
                HOperatorSet.OpeningRectangle1(ho_RegionsDifferenceLacked, out ho_FaultLacked, par.SmoothWidth, par.SmoothHeight);//筛选一部分

                ho_FaultsLacked.Dispose();
                HOperatorSet.Connection(ho_FaultLacked, out ho_FaultsLacked);

                HOperatorSet.SmallestRectangle1(ho_FaultsLacked, out hv_RowInsp1, out hv_ColumnInsp1,
                    out hv_RowInsp2, out hv_ColumnInsp2);
                if (hv_RowInsp1.Length > 0)
                {
                    ho_Rectangle.Dispose();
                    HOperatorSet.Rectangularity(ho_FaultsLacked, out hv_Rectangularity);

                    HOperatorSet.GenRectangle1(out ho_Rectangle, hv_RowInsp1, hv_ColumnInsp1 - par.DblOutRate, hv_RowInsp2 + par.DblOutRate, hv_ColumnInsp2 + par.DblOutRate);
                    HOperatorSet.MinMaxGray(ho_FaultsLacked, image.Ho_Image, 0, out hv_MinGray, out hv_MaxGray, out hv_Range);
                    HOperatorSet.AreaCenter(ho_FaultsLacked, out hv_AreaFault, out hv_RowFault, out hv_ColumnFault);

                    for (int i = 0; i < hv_RowInsp1.Length; i++)
                    {
                        if (hv_RowInsp1.ToIArr()[i] > hv_Row1Raw.D + 8)
                        {
                            continue;
                        }

                        if (hv_Rectangularity[i] > par.ThRectangleularity)
                        {
                            continue;
                        }

                        double widPix = hv_ColumnInsp2.ToIArr()[i] - hv_ColumnInsp1.ToIArr()[i] + 1;
                        double depthPix = hv_RowInsp2.ToIArr()[i] - hv_RowInsp1.ToIArr()[i] + 1;
                        if (depthPix > 100 && depthPix > widPix * 2)
                            continue;
                        double widthFault = widPix * par.Amp;
                        double depthFault = depthPix * par.Amp;

                        //if (depthFault < 0.1 || widthFault < 0.3)
                        //{
                        //    continue;
                        //}

                        double preY = par.ThPreiectionY;
                        if (result.IsCorner/* || No <= cornorCnt + 3*/)
                        {

                            if ((depthFault > par.ThCornerY && widthFault > par.ThCornerX && !par.UsingORToCorner)
                                || ((depthFault > par.ThCornerY || widthFault > par.ThCornerX) && par.UsingORToCorner))
                            {
                                result.SingleFalutInfo_L.Add(new FaultInfo()
                                {
                                    PosFalut = new Point2D((hv_ColumnInsp2.ToIArr()[i] + hv_ColumnInsp1.ToIArr()[i]) / 2, (hv_RowInsp2.ToIArr()[i] + hv_RowInsp1.ToIArr()[i]) / 2),
                                    FaultType_E = FaultType_Enum.破角,
                                    DepthFault = depthFault,
                                    WidthFault = widthFault,
                                });
                            }
                        }
                        else
                        {
                            if (hv_RowInsp2.ToIArr()[i] - hv_RowInsp1.ToIArr()[i] > hv_Row2Raw - hv_Row1Raw - 20 //很长，几乎Y方向和玻璃一样长
                            || hv_AreaFault.ToDArr()[i] / (widPix * depthPix) < 0.4)//缺陷面积很小，只有举行的40%，认为是多方向的裂痕，这个判定主要是针对的CF和TFT在一起的位置
                            {
                                continue;
                            }
                            //这里所有的深度都大于0.05且矩形度都小

                            //增加对图片序号的判断
                            if (ModelParams.IfUsingSpl)
                            {
                                double X = par.ThShellX;
                                double Y = par.ThShellY;
                                int InBox = 0;
                                if (No > par.SplStartIndex1 && No < par.SplEndIndex1)
                                {
                                    X = par.SplThShellX1;
                                    Y = par.SplThShellY1;
                                    preY = par.SplThPerY1;
                                    InBox = 1;
                                }
                                else if (No > par.SplStartIndex2 && No < par.SplEndIndex2)
                                {
                                    X = par.SplThShellX2;
                                    Y = par.SplThShellY2;
                                    preY = par.SplThPerY2;
                                    InBox = 2;
                                }
                                else if (No > par.SplStartIndex3 && No < par.SplEndIndex3)
                                {
                                    X = par.SplThShellX3;
                                    Y = par.SplThShellY3;
                                    preY = par.SplThPerY3;
                                    InBox = 3;
                                }
                                else if (No > par.SplStartIndex4 && No < par.SplEndIndex4)
                                {
                                    X = par.SplThShellX4;
                                    Y = par.SplThShellY4;
                                    preY = par.SplThPerY4;
                                    InBox = 4;
                                }
                                else if (No > par.SplStartIndex5 && No < par.SplEndIndex5)
                                {
                                    X = par.SplThShellX5;
                                    Y = par.SplThShellY5;
                                    preY = par.SplThPerY5;
                                    InBox = 5;
                                }
                                else if (No > par.SplStartIndex6 && No < par.SplEndIndex6)
                                {
                                    X = par.SplThShellX6;
                                    Y = par.SplThShellY6;
                                    preY = par.SplThPerY6;
                                    InBox = 6;
                                }
                                else if (No > par.SplStartIndex7 && No < par.SplEndIndex7)
                                {
                                    X = par.SplThShellX7;
                                    Y = par.SplThShellY7;
                                    preY = par.SplThPerY7;
                                    InBox = 7;
                                }
                                else if (No > par.SplStartIndex8 && No < par.SplEndIndex8)
                                {
                                    X = par.SplThShellX8;
                                    Y = par.SplThShellY8;
                                    preY = par.SplThPerY8;
                                    InBox = 8;
                                }
                                else if (No > par.SplStartIndex9 && No < par.SplEndIndex9)
                                {
                                    X = par.SplThShellX9;
                                    Y = par.SplThShellY9;
                                    preY = par.SplThPerY9;
                                    InBox = 9;
                                }

                                double thShellX = X == 0 ? par.ThShellX : X;
                                double thShellY = Y == 0 ? par.ThShellY : Y;

                                if (InBox > 0)
                                {
                                    if (depthFault > thShellY || widthFault > thShellX || hv_MaxGray > par.ThITO)
                                    {
                                        result.SingleFalutInfo_L.Add(new FaultInfo()
                                        {
                                            PosFalut = new Point2D((hv_ColumnInsp2.ToIArr()[i] + hv_ColumnInsp1.ToIArr()[i]) / 2,
                                                                    (hv_RowInsp2.ToIArr()[i] + hv_RowInsp1.ToIArr()[i]) / 2),
                                            FaultType_E = FaultType_Enum.贝壳,
                                            DepthFault = depthFault,
                                            WidthFault = widthFault,
                                        });
                                    }
                                    continue;
                                }
                            }

                            double thPreY = preY == 0 ? par.ThPreiectionY : preY;
                            //如果不满足图片序号区间，那么默认不使用单独卡控
                            //长宽比小于指定系数的贝壳认为是凹边
                            if (depthFault / widthFault < ModelParams.ConvexJudgeRatio)
                            {
                                if (depthFault > thPreY || widthFault > par.ThPreiectionX)
                                    result.SingleFalutInfo_L.Add(new FaultInfo()
                                    {
                                        PosFalut = new Point2D((hv_ColumnInsp2.ToIArr()[i] + hv_ColumnInsp1.ToIArr()[i]) / 2, (hv_RowInsp2.ToIArr()[i] + hv_RowInsp1.ToIArr()[i]) / 2),
                                        FaultType_E = FaultType_Enum.凹边,
                                        DepthFault = depthFault,
                                        WidthFault = widthFault,
                                    });
                            }
                            else
                            {
                                if (depthFault > par.ThShellY || widthFault > par.ThShellX || hv_MaxGray > par.ThITO)
                                {
                                    result.SingleFalutInfo_L.Add(new FaultInfo()
                                    {
                                        PosFalut = new Point2D((hv_ColumnInsp2.ToIArr()[i] + hv_ColumnInsp1.ToIArr()[i]) / 2, (hv_RowInsp2.ToIArr()[i] + hv_RowInsp1.ToIArr()[i]) / 2),
                                        FaultType_E = FaultType_Enum.贝壳,
                                        DepthFault = depthFault,
                                        WidthFault = widthFault,
                                    });
                                }
                            }
                        }
                    }
                }

                #endregion

                //#region 凸边
                //ho_RegionsDifferenceRested.Dispose();
                //HOperatorSet.Difference(ho_RegionInsp, ho_RawRectangle, out ho_RegionsDifferenceRested);
                //ho_FaultRested.Dispose();
                //HOperatorSet.OpeningRectangle1(ho_RegionsDifferenceRested, out ho_FaultRested, par.SmoothWidth, par.SmoothHeight);

                //ho_FaultsRested.Dispose();
                //HOperatorSet.Connection(ho_FaultRested, out ho_FaultsRested);
                ////循环
                //HOperatorSet.SmallestRectangle1(ho_FaultsRested, out hv_RowInsp1, out hv_ColumnInsp1, out hv_RowInsp2, out hv_ColumnInsp2);

                //if (hv_RowInsp1.Length > 0)
                //{

                //    for (int i = 0; i < hv_RowInsp1.Length; i++)
                //    {
                //        double widthFault = (hv_ColumnInsp2.ToIArr()[i] - hv_ColumnInsp1.ToIArr()[i]) * par.Amp;
                //        double depthFault = (hv_RowInsp2.ToIArr()[i] - hv_RowInsp1.ToIArr()[i]) * par.Amp;

                //        if (depthFault < 0.56)
                //        {
                //            continue;
                //        }

                //        if (depthFault > par.ThPreiectionY || widthFault > par.ThPreiectionX)
                //        {
                //            result.SingleFalutInfo_L.Add(new FaultInfo()
                //            {
                //                PosFalut = new Point2D((hv_ColumnInsp2.ToIArr()[i] + hv_ColumnInsp1.ToIArr()[i]) / 2, (hv_RowInsp2.ToIArr()[i] + hv_RowInsp1.ToIArr()[i]) / 2),
                //                FaultType_E = FaultType_Enum.凸边,
                //                DepthFault = depthFault,
                //                WidthFault = widthFault,
                //            });
                //        }
                //    }
                //}
                //#endregion

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("FunInsp", ex);
            }
            finally
            {
                ho_RawRegion.Dispose();
                ho_RegionRawFillUp.Dispose();
                ho_RegionOpening.Dispose();
                ho_RawRectangle.Dispose();
                ho_RegionInsp.Dispose();
                ho_RegionInspFillUp.Dispose();
                ho_RegionsDifferenceLacked.Dispose();
                ho_FaultLacked.Dispose();
                ho_FaultsLacked.Dispose();
                ho_Rectangle.Dispose();
                ho_RegionsDifferenceRested.Dispose();
                ho_FaultRested.Dispose();
                ho_FaultsRested.Dispose();
            }
        }

        private void DealCFRealContour(HObject ho_RegionInsp, out HObject ho_RegionInspFillUp)
        {
            HOperatorSet.GenEmptyObj(out ho_RegionInspFillUp);

            HObject ho_ConnectedRegions, ho_SelectedRegions, ho_DestRegions;
            HTuple hv_Area = null, hv_Row = null, hv_Column = null;
            HTuple hv_Indices = null;
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_DestRegions);
            try
            {
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionInsp, out ho_ConnectedRegions);
                //select_region_point
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 150, 9999999);
                HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row, out hv_Column);
                HOperatorSet.TupleSortIndex(hv_Column, out hv_Indices);
                ho_DestRegions.Dispose();
                HOperatorSet.SelectRegionPoint(ho_ConnectedRegions, out ho_DestRegions, hv_Row.ToDArr()[hv_Indices.Length - 1], hv_Column.ToDArr()[hv_Indices.Length - 1]);
                ho_RegionInspFillUp.Dispose();
                HOperatorSet.FillUp(ho_DestRegions, out ho_RegionInspFillUp);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("FunInsp.DealCFRealContour", ex);
            }
            finally
            {
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_DestRegions.Dispose();
            }
        }

        void DealCFOKContour(HObject ho_RegionOpening, out HTuple hv_Row1Raw, out HTuple hv_Column1Raw, out HTuple hv_Row2Raw, out HTuple hv_Column2Raw)
        {
            hv_Row1Raw = new HTuple();
            hv_Row2Raw = new HTuple();
            hv_Column1Raw = new HTuple();
            hv_Column2Raw = new HTuple();

            HObject ho_ConnectedRegions, ho_SelectedRegions, ho_DestRegions;

            HTuple hv_Area = null, hv_Row = null, hv_Column = null;
            HTuple hv_Indices = null;
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_DestRegions);

            try
            {
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions);
                //select_region_point
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", 150, 9999999);
                HOperatorSet.AreaCenter(ho_SelectedRegions, out hv_Area, out hv_Row, out hv_Column);
                HOperatorSet.TupleSortIndex(hv_Column, out hv_Indices);
                ho_DestRegions.Dispose();
                HOperatorSet.SelectRegionPoint(ho_ConnectedRegions, out ho_DestRegions, hv_Row.ToDArr()[hv_Indices.Length - 1], hv_Column.ToDArr()[hv_Indices.Length - 1]);
                HOperatorSet.SmallestRectangle1(ho_DestRegions, out hv_Row1Raw, out hv_Column1Raw,
                    out hv_Row2Raw, out hv_Column2Raw);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("FunInsp.DealCFOKContour", ex);
            }
            finally
            {
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_DestRegions.Dispose();
            }
        }
        /// <summary>
        /// 输入图像和对应的NG结果输出将结果矩形打印到图片上的新图片
        /// </summary>
        /// <param name="im">输入图像</param>
        /// <param name="NgRegionPaintedImage">NG结果打印之后的图像</param>
        /// <param name="result">结果</param>
        /// <param name="Amp">实际距离/像素</param>
        public void PaintRegionToImage(ImageAll im,out ImageAll NgRegionPaintedImage,ResultInspection result,double Amp)
        {
            NgRegionPaintedImage = new ImageAll();
            HObject Rect = new HObject(),paintedImage = new HObject();
            HOperatorSet.GenEmptyObj(out Rect);
            HOperatorSet.GenEmptyObj(out paintedImage);
            HTuple Row1=new HTuple(), Col1 = new HTuple(), Row2 = new HTuple(), Col2 = new HTuple();
            
            try
            {

                for (int i = 0; i < result.SingleFalutInfo_L.Count; i++)
                {
                    //提取长宽位置
                    Point2D posXY = result.SingleFalutInfo_L[i].PosFalut;
                    double width = result.SingleFalutInfo_L[i].WidthFault / Amp;
                    double height = result.SingleFalutInfo_L[i].DepthFault / Amp;
                    //提取左上右下行列坐标
                    Row1 = Row1.TupleConcat(posXY.DblValue2 - height / 2);
                    Col1 = Col1.TupleConcat(posXY.DblValue1 - width / 2);
                    Row2 = Row2.TupleConcat(posXY.DblValue2 + height / 2);
                    Col2 = Col2.TupleConcat(posXY.DblValue1 + width / 2);
                }
                //生成缺陷矩形
                HOperatorSet.GenRectangle1(out Rect, Row1, Col1, Row2, Col2);
                HOperatorSet.PaintRegion(Rect, im.Ho_Image, out paintedImage, 255, "margin");
                NgRegionPaintedImage.Ho_Image = paintedImage;
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError("FunInsp", ex);
            }
            finally
            {
                Rect?.Dispose();
            }

        }

        [Obsolete]
        public void DealImageMLine(ImageAll image, ParInspection par, ref ResultInspection result)
        {


            #region 定义
            HObject ho_rawRegionThreshold, ho_RegionFillUp;
            HObject ho_ConnectedRegions, ho_SelectedRegions, ho_Contour;
            HObject ho_LastLine, ho_RegionReal, ho_RegionTrans, ho_def_Corner_Edge;
            HObject ho_ConnectedRegions1, ho_RectAdjITO, ho_def_Rest;
            HObject ho_ConnectedRegions2;


            // Local control variables 
            HTuple hv_Rows_L = null, hv_Columns_L = null, hv_Width = null;
            HTuple hv_Height = null, hv_AreaTotal = null, hv_RowTotal = null;
            HTuple hv_ColumnTotal = null, hv_maxArea = null, hv_RowStart = null;
            HTuple hv_ColumnStart = null, hv_RowEnd = null, hv_ColumnEnd = null;
            HTuple hv_Measure_H = new HTuple(), hv_Row_Measure = new HTuple();
            HTuple hv_Column_Measure = new HTuple(), hv_Amplitude_Measure = new HTuple();
            HTuple hv_Distance_Measure = new HTuple(), hv_RowBeginL1 = null;
            HTuple hv_ColBeginL1 = null, hv_RowEndL1 = null, hv_ColEndL1 = null;
            HTuple hv_Nr = null, hv_Nc = null, hv_Dist = null, hv_Distance = null;
            HTuple hv_Sorted = null, hv_Indices = null, hv_ClearIndex = null;
            HTuple hv_tempK = null, hv_RealRowBegin = null, hv_RealRowEnd = null;
            HTuple hv_RowDefect1 = null, hv_ColumnDefect1 = null;
            HTuple hv_RowDefect2 = null, hv_ColumnDefect2 = null;
            HTuple hv_Rectangularity = null, hv_Row11 = null, hv_Column11 = null;
            HTuple hv_Row21 = null, hv_Column21 = null;

            #endregion

            #region 初始化
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_rawRegionThreshold);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Contour);
            HOperatorSet.GenEmptyObj(out ho_LastLine);
            HOperatorSet.GenEmptyObj(out ho_RegionReal);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_def_Corner_Edge);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RectAdjITO);
            HOperatorSet.GenEmptyObj(out ho_def_Rest);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            #endregion

            hv_Rows_L = new HTuple();
            hv_Columns_L = new HTuple();

            try
            {

                HOperatorSet.GetImageSize(image.Ho_Image, out hv_Width, out hv_Height);

                ho_rawRegionThreshold.Dispose();
                HOperatorSet.Threshold(image.Ho_Image, out ho_rawRegionThreshold, par.ThresholdGlass, 255);

                //有效图片判断，输出面积，在C#补全
                ho_RegionFillUp.Dispose();
                HOperatorSet.FillUpShape(ho_rawRegionThreshold, out ho_RegionFillUp, "area",
                    1, 999999);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionFillUp, out ho_ConnectedRegions);

                HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_AreaTotal, out hv_RowTotal,
                    out hv_ColumnTotal);
                HOperatorSet.TupleMax(hv_AreaTotal, out hv_maxArea);

                if (hv_maxArea < 320)
                {
                    result.IsValid = false;
                    return;
                }
                else
                    result.IsValid = true;

                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                    "and", hv_maxArea, hv_maxArea);
                HOperatorSet.SmallestRectangle1(ho_SelectedRegions, out hv_RowStart, out hv_ColumnStart,
                    out hv_RowEnd, out hv_ColumnEnd);

                if (hv_ColumnStart > 4 || hv_ColumnEnd < hv_Width - 4)
                {
                    result.IsCorner = true;
                }

                ////处理轮廓
                //double widthRect = (hv_ColumnEnd - hv_ColumnStart) / par.NumRect;

                //for (int i = 0; i < par.NumRect; i++)
                //{
                //    double colCenter = (hv_ColumnStart + (widthRect * i)) + (widthRect / 2);
                //    if (colCenter > hv_ColumnEnd)
                //    {
                //        break;
                //    }
                //    HOperatorSet.GenMeasureRectangle2(hv_RowStart + 25, colCenter, -1.57, 50, widthRect / 2,
                //        hv_Width, hv_Height, "nearest_neighbor", out hv_Measure_H);
                //    //gen_rectangle2 (Rectangle, RowStart+25, col, -1.57, 50, widthRect/2)
                //    HOperatorSet.MeasurePos(image.Ho_Image, hv_Measure_H, par.Smooth, par.ThresholdEdge, par.LineTransition,
                //        par.LineSelect, out hv_Row_Measure, out hv_Column_Measure, out hv_Amplitude_Measure,
                //        out hv_Distance_Measure);
                //    //gen_cross_contour_xld (Cross, Row_Measure, Column_Measure, 30, 0)
                //    if ((int)(new HTuple(((hv_Row_Measure * hv_Column_Measure)).TupleNotEqual(0))) != 0)
                //    {
                //        HOperatorSet.TupleConcat(hv_Rows_L, hv_Row_Measure, out hv_Rows_L);
                //        HOperatorSet.TupleConcat(hv_Columns_L, hv_Column_Measure, out hv_Columns_L);
                //    }

                //    //关闭句柄，测试内存泄露
                //    //if (ParSetWork.P_I[5].BlSelect)
                //    {
                //        HOperatorSet.CloseMeasure(hv_Measure_H);
                //    }
                //}

                ////剔除
                //if (hv_Rows_L.Length < par.MinimunPoint)
                //{
                //    //视野里边太短，无法判断
                //    return;
                //}

                //ho_Contour.Dispose();
                //HOperatorSet.GenContourPolygonXld(out ho_Contour, hv_Rows_L, hv_Columns_L);
                //HOperatorSet.FitLineContourXld(ho_Contour, "tukey", -1, 2, 5, 0.5, out hv_RowBeginL1, out hv_ColBeginL1, out hv_RowEndL1, out hv_ColEndL1, out hv_Nr, out hv_Nc, out hv_Dist);
                //HOperatorSet.DistancePl(hv_Rows_L, hv_Columns_L, hv_RowBeginL1, hv_ColBeginL1, hv_RowEndL1, hv_ColEndL1, out hv_Distance);

                //HOperatorSet.TupleSort(hv_Distance, out hv_Sorted);
                //HOperatorSet.TupleSortIndex(hv_Distance, out hv_Indices);
                ////剔除到只剩10个点
                //hv_ClearIndex = hv_Indices.TupleSelectRange(par.MinimunPoint, hv_Indices.Length - 1);
                //HOperatorSet.TupleRemove(hv_Rows_L, hv_ClearIndex, out hv_Rows_L);
                //ho_LastLine.Dispose();
                //HOperatorSet.GenContourPolygonXld(out ho_LastLine, hv_RowBeginL1.TupleConcat(hv_RowEndL1), hv_ColBeginL1.TupleConcat(hv_ColEndL1));

                //hv_tempK = (hv_RowEndL1 - hv_RowBeginL1) / (hv_ColEndL1 - hv_ColBeginL1);
                //hv_RealRowBegin = hv_RowBeginL1 + (hv_tempK * (hv_ColumnStart - hv_ColBeginL1));
                //hv_RealRowEnd = hv_RowBeginL1 + (hv_tempK * (hv_ColumnEnd - hv_ColBeginL1));


                //ho_RegionReal.Dispose();
                //HOperatorSet.GenRegionPolygon(out ho_RegionReal, ((((hv_RealRowBegin.TupleConcat(hv_RealRowEnd))).TupleConcat(hv_RowEnd))).TupleConcat(hv_RowEnd), ((((hv_ColumnStart.TupleConcat(hv_ColumnEnd))).TupleConcat(hv_ColumnEnd))).TupleConcat(hv_ColumnStart));
                //ho_RegionTrans.Dispose();
                //HOperatorSet.ShapeTrans(ho_RegionReal, out ho_RegionTrans, "convex");
                ////贝壳 破角
                //ho_def_Corner_Edge.Dispose();
                //HOperatorSet.Difference(ho_RegionTrans, ho_RegionFillUp, out ho_def_Corner_Edge
                //    );
                //ho_ConnectedRegions1.Dispose();
                //HOperatorSet.Connection(ho_def_Corner_Edge, out ho_ConnectedRegions1);
                //HOperatorSet.SmallestRectangle1(ho_ConnectedRegions1, out hv_RowDefect1, out hv_ColumnDefect1, out hv_RowDefect2, out hv_ColumnDefect2);
                //HOperatorSet.Rectangularity(ho_ConnectedRegions1, out hv_Rectangularity);
                //ho_RectAdjITO.Dispose();
                //HOperatorSet.GenRectangle1(out ho_RectAdjITO, hv_RowDefect1, hv_ColumnDefect1 - par.DblOutRate, hv_RowDefect2 + par.DblOutRate, hv_ColumnDefect2 + par.DblOutRate);
                //HTuple hv_MinGray, hv_MaxGray, hv_RangeGray;
                //HOperatorSet.MinMaxGray(ho_RectAdjITO, image.Ho_Image, 0, out hv_MinGray, out hv_MaxGray, out hv_RangeGray);

                for (int i = 0; i < hv_RowDefect1.Length; i++)
                {
                    if (hv_RowDefect1.IArr[i] > hv_RowStart.D + 5)//排除赃物
                    {
                        continue;
                    }
                    double widthFault = (hv_ColumnDefect2.IArr[i] - hv_ColumnDefect1.IArr[i]) * par.Amp;
                    double depthFault = (hv_RowDefect2.IArr[i] - hv_RowDefect1.IArr[i]) * par.Amp;

                    if (result.IsCorner)
                    {
                        if (depthFault < 0.05)
                        {
                            continue;
                        }

                        if (depthFault < par.ThCornerY || widthFault < par.ThCornerX/* && hv_MaxGray.DArr[i] < par.ThITO*/)//这里判断是否伤到了mark
                        {
                            continue;
                        }

                        if (hv_Rectangularity.DArr[i] > par.ThRectangleularity)
                        {
                            continue;
                        }

                        result.SingleFalutInfo_L.Add(new FaultInfo
                        {
                            PosFalut = new Point2D((hv_ColumnDefect2.IArr[i] + hv_ColumnDefect1.IArr[i]) / 2, (hv_RowDefect2.IArr[i] + hv_RowDefect1.IArr[i]) / 2),
                            FaultType_E = FaultType_Enum.破角,
                            DepthFault = depthFault,
                            WidthFault = widthFault,
                        });
                    }
                    else
                    {
                        //if (depthFault < par.ThShellY && widthFault < par.ThShellX && hv_MaxGray.DArr[i] < par.ThITO)
                        //{
                        //    continue;
                        //}

                        //if (depthFault < par.ThShellY / 2)
                        //{
                        //    continue;
                        //}

                        //result.SingleFalutInfo_L.Add(new FalutInfo
                        //{
                        //    PosFalut = new Point2D((hv_ColumnDefect2.IArr[i] + hv_ColumnDefect1.IArr[i]) / 2, (hv_RowDefect2.IArr[i] + hv_RowDefect1.IArr[i]) / 2),
                        //    FaultType_E = FaultType_Enum.贝壳,
                        //    DepthFault = depthFault,
                        //    WidthFault = widthFault,
                        //});
                    }
                }

                //凸边
                ho_def_Rest.Dispose();
                HOperatorSet.Difference(ho_RegionFillUp, ho_RegionTrans, out ho_def_Rest);
                ho_ConnectedRegions2.Dispose();
                HOperatorSet.Connection(ho_def_Rest, out ho_ConnectedRegions2);
                HOperatorSet.SmallestRectangle1(ho_ConnectedRegions2, out hv_Row11, out hv_Column11, out hv_Row21, out hv_Column21);

                for (int i = 0; i < hv_Row11.Length; i++)
                {
                    double depthPreiection = (hv_Row21.IArr[i] - hv_Row11.IArr[i]) * par.Amp;
                    double widthPreiection = (hv_Column21.IArr[i] - hv_Column11.IArr[i]) * par.Amp;

                    if (depthPreiection < par.ThPreiectionY && widthPreiection < par.ThPreiectionX)
                    {
                        continue;
                    }

                    result.SingleFalutInfo_L.Add(new FaultInfo()
                    {
                        FaultType_E = FaultType_Enum.凸边,
                        PosFalut = new Point2D((hv_Column21.IArr[i] + hv_Column11.IArr[i]) / 2, (hv_Row21.IArr[i] + hv_Row11.IArr[i]) / 2),
                        DepthFault = depthPreiection,
                        WidthFault = widthPreiection,
                    });
                }
            }
            catch (Exception ex)
            {
                result.SingleFalutInfo_L.Add(new FaultInfo
                {
                    PosFalut = new Point2D(1, 1),
                    FaultType_E = FaultType_Enum.破角,
                    DepthFault = 1,
                    WidthFault = 1,
                });
            }
            finally
            {
                ho_rawRegionThreshold.Dispose();
                ho_RegionFillUp.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_Contour.Dispose();
                ho_LastLine.Dispose();
                ho_RegionReal.Dispose();
                ho_RegionTrans.Dispose();
                ho_def_Corner_Edge.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RectAdjITO.Dispose();
                ho_def_Rest.Dispose();
                ho_ConnectedRegions2.Dispose();
            }
        }
    }
}
