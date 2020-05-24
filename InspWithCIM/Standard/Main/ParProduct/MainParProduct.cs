using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealConfigFile;
using BasicClass;
using DealImageProcess;
using System.ComponentModel;

namespace Main
{
    public class MainParProduct : INotifyPropertyChanged
    {
        #region 静态类实例
        public static MainParProduct M_I = new MainParProduct();

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region 玻璃相关参数

        /// <summary>
        /// 玻璃尺寸X
        /// </summary>
        public double XGlass
        {
            get
            {
                return ParConfigPar.P_I[0].DblValue;
            }
        }

        /// <summary>
        /// 玻璃尺寸Y
        /// </summary>
        public double YGlass
        {
            get
            {
                return ParConfigPar.P_I[1].DblValue;
            }
        }

        /// <summary>
        /// 玻璃尺寸Z
        /// </summary>
        public double ZGlass
        {
            get
            {
                return ParConfigPar.P_I[8].DblValue;
            }
        }

        /// <summary>
        /// 对位方式，上下左右
        /// </summary>
        public TypeOrd_enum TypeAligner_Enum
        {
            get
            {
                return (TypeOrd_enum)ParConfigPar.P_I[3].DblValue;
            }
        }

        /// <summary>
        /// mark间距
        /// </summary>
        public double LengthMark
        {
            get { return ParConfigPar.P_I[6].DblValue; }
        }

        /// <summary>
        /// markX方向位置
        /// </summary>
        public double XMark
        {
            get
            {
                return ParConfigPar.P_I[4].DblValue;
            }
        }

        /// <summary>
        /// markY方向位置
        /// </summary>
        public double YXMark
        {
            get
            {
                return ParConfigPar.P_I[5].DblValue;
            }
        }

        #region 电极
        /// <summary>
        /// 上电极宽度
        /// </summary>
        public double ITOTop
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[4].DblValue;
            }
        }

        /// <summary>
        /// 下电极宽度
        /// </summary>
        public double ITODown
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[5].DblValue;
            }
        }

        /// <summary>
        /// 左电极宽度
        /// </summary>
        public double ITOLeft
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[6].DblValue;
            }
        }

        /// <summary>
        /// 右电极宽度
        /// </summary>
        public double ITORight
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[7].DblValue;
            }
        }

        public int NumITO
        {
            get
            {
                int num = 0;
                if (ITOTop > 0) num++;
                if (ITODown > 0) num++;
                if (ITOLeft > 0) num++;
                if (ITORight > 0) num++;

                return num;
            }
        }

        #endregion 电极

        #endregion 玻璃相关参数

        #region 讯边检
        public double AnlgeFirstInsSide
        {
            get
            {
                if (NumITO == 1)
                {
                    if (ITOTop > 0)
                    {
                        return 90;
                    }
                    else if (ITODown > 0)
                    {
                        return -90;
                    }
                    else if (ITORight > 0)
                    {
                        return 180;
                    }
                    else if (ITOLeft > 0)
                    {
                        return 0;
                    }

                }
                else
                {
                    if (ITORight > 0)
                    {
                        return 180;
                    }
                    else if (ITOLeft > 0)
                    {
                        return 0;
                    }
                }
                return 0;
            }
        }

        public double AnlgeSecondInsSide
        {
            get
            {
                if (NumITO == 1)
                {
                    return AnlgeFirstInsSide - 90;
                }
                if (ITOTop > 0)
                {
                    return 90;
                }
                else if (ITODown > 0)
                {
                    return -90;
                }
                return 0;
            }
        }

        #endregion

        #region 二维码
        /// <summary>
        /// 二维码位置玻璃的尺寸X
        /// </summary>
        public double XGlassCode
        {
            get
            {
                return XGlass;
            }
        }

        /// <summary>
        /// 二维码位置玻璃的尺寸Y
        /// </summary>
        public double YGlassCode
        {
            get
            {
                return YGlass;
            }
        }

        /// <summary>
        /// 二维码X
        /// </summary>
        public double XCode
        {
            get
            {
                return ParConfigPar.P_I[2].DblValue;
            }
        }

        /// <summary>
        /// 二维码Y
        /// </summary>
        public double YCode
        {
            get
            {
                return ParConfigPar.P_I[3].DblValue;
            }
        }

        /// <summary>
        /// 实际二维码X
        /// </summary>
        public double XCodeReal
        {
            get
            {
                return XCode;
            }
        }

        /// <summary>
        /// 实际二维码Y
        /// </summary>
        public double YCodeReal
        {
            get
            {
                return YCode;
            }
        }

        /// <summary>
        ///二维码的实际位置
        /// </summary>
        public Point2D PosCodeStd
        {
            get
            {
                return new Point2D(XCodeReal, YCodeReal);
            }
        }

        #endregion

        #region 插栏

        /// <summary>
        /// 插栏位置玻璃的尺寸X
        /// </summary>
        public double XGlassInsert
        {
            get
            {
                if (AngleInsert == 0 || AngleInsert == 180)
                {
                    return XGlass;
                }
                else
                {
                    return YGlass;
                }

            }
        }

        /// <summary>
        /// 插栏位置玻璃的尺寸Y
        /// </summary>
        public double YGlassInsert
        {
            get
            {
                if (AngleInsert == 0 || AngleInsert == 180)
                {
                    return YGlass;
                }
                else
                {
                    return XGlass;
                }
            }
        }

        /// <summary>
        /// 起始行
        /// </summary>
        public int RowStart
        {
            get
            {
                return 0;
                //return (int)ParConfigPar.P_I.ParProduct_L[9].DblValue;
            }
        }

        /// <summary>
        /// 起始列
        /// </summary>
        public int ColStart
        {
            get
            {
                //return (int)ParConfigPar.P_I.ParProduct_L[10].DblValue;
                return 0;
            }
        }

        /// <summary>
        /// 插栏行数
        /// </summary>
        public int RowCST
        {
            get
            {
                return (int)ParConfigPar.P_I.ParProduct_L[9].DblValue;
            }
        }

        /// <summary>
        /// 插栏列数
        /// </summary>
        public int ColCST
        {
            get
            {
                return (int)ParConfigPar.P_I.ParProduct_L[10].DblValue;
            }
        }

        /// <summary>
        /// 插栏总数
        /// </summary>
        public int SumGlass
        {
            get
            {
                return ColCST * RowCST;
                //return (int)ParConfigPar.P_I.ParProduct_L[12].DblValue;
            }
        }

        /// <summary>
        /// 龙骨间距
        /// </summary>
        public double KeelInterval
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[11].DblValue;
            }
        }

        /// <summary>
        /// 龙骨列数
        /// </summary>
        public int KeelCol
        {
            get
            {
                return ColCST + 1;
            }
        }

        /// <summary>
        /// 大小卡塞,0为小
        /// </summary>
        bool IsSmallCST
        {
            get
            {
                if (ParConfigPar.P_I.ParProduct_L[16].DblValue == 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 卡塞宽度
        /// </summary>
        public double WidthCST
        {
            get
            {
                if (IsSmallCST)
                {
                    return ParStd.Value1("Std8");
                }
                else
                {
                    return ParStd.Value2("Std8");
                }
            }
        }

        /// <summary>
        /// 龙骨各列相对于边的位置
        /// </summary>
        public double[] DisCSTToBoard
        {
            get
            {
                //卡塞两边的间隙
                double dblDis = Math.Round((WidthCST - ColCST * KeelInterval) / 2, 2);

                double[] dblValue = new double[6];

                try
                {
                    //外框-龙骨间距-龙骨宽度=两边的间隙

                    double value = 0;
                    if (!IsSmallCST)
                    {
                        value = ParStd.Value2("Std8") - ParStd.Value1("Std8");
                    }
                    for (int i = 0; i < KeelCol; i++)
                    {
                        dblValue[i] = dblDis + KeelInterval * i + value / 2;
                    }
                }
                catch (Exception)
                {

                }
                return dblValue;
            }
        }

        /// <summary>
        /// 生成所有标准龙骨位置集合
        /// </summary>
        /// <returns></returns>
        public List<List<Point2D>> CreatKeelPos_L()
        {
            List<List<Point2D>> pList = new List<List<Point2D>>();
            try
            {
                double xStd = ParStd.Value1("std7");
                for (int i = 0; i < KeelCol; i++)
                {
                    List<Point2D> point = new List<Point2D>();
                    for (int j = 0; j < RowCST; j++)
                    {
                        double x = xStd + DisCSTToBoard[i];
                        point.Add(new Point2D(x, 0));
                    }
                    pList.Add(point);
                }
            }
            catch (Exception ex)
            {

            }
            return pList;
        }

        /// <summary>
        /// 实际插栏坐标
        /// </summary>
        /// <returns></returns>
        public List<List<Point2D>> RealCellPos_L()
        {
            try
            {
                List<List<Point2D>> pReal = new List<List<Point2D>>();
                //起始数目               
                List<List<Point2D>> pAll = CreatKeelPos_L();
                for (int i = 0; i < ColCST; i++)
                {
                    List<Point2D> pRow = new List<Point2D>();
                    for (int j = 0; j < RowCST; j++)
                    {
                        double x = (pAll[i][j].DblValue1 + pAll[i + 1][j].DblValue1) / 2;
                        pRow.Add(new Point2D(Math.Round((float)x, 3), 0));
                    }
                    pReal.Add(pRow);
                }
                return pReal;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("VarConfigPWork", ex);
                return null;
            }
        }

        /// <summary>
        /// 插栏方向
        /// </summary>
        public TypeOrd_enum InsertDir
        {
            get
            {
                if (ParConfigPar.P_I[13].DblValue == 1)
                {
                    return TypeOrd_enum.Top;
                }
                if (ParConfigPar.P_I[13].DblValue == 2)
                {
                    return TypeOrd_enum.Down;
                }
                if (ParConfigPar.P_I[13].DblValue == 4)
                {
                    return TypeOrd_enum.Left;
                }
                if (ParConfigPar.P_I[13].DblValue == 8)
                {
                    return TypeOrd_enum.Right;
                }
                return TypeOrd_enum.Right;
            }
        }

        /// <summary>
        /// 插栏需要旋转的角度
        /// </summary>
        public double AngleInsert
        {
            get
            {
                double angle = 0;
                switch (InsertDir)
                {
                    case TypeOrd_enum.Top:
                        angle = 180;
                        break;
                    case TypeOrd_enum.Down:
                        angle = 0;
                        break;
                    case TypeOrd_enum.Left:
                        angle = -90;
                        break;
                    case TypeOrd_enum.Right:
                        angle = 90;
                        break;
                    default:
                        break;
                }
                return angle;
            }
        }

        public double YTransInsert
        {
            get
            {
                double stdX = ParStd.Value1("std2");
                return stdX + YGlassInsert / 2;
            }
        }

        #endregion

        #region 抛皮带线
        public int TimeBeltRun
        {
            get
            {
                return Convert.ToInt32((XGlassBelt + 20) * ParStd.Value1("std17") / 100);
            }
        }

        public double AngleBelt
        {
            get
            {
                double angle = 0;

                //几个特殊情况
                if (YGlassInsert > ParStd.Value2("std17"))
                {
                    return 90;
                }
                if (XGlassInsert > ParStd.Value2("std17"))
                {
                    return 0;
                }

                //正常情况
                if (XGlassInsert > YGlassInsert)
                {
                    angle = 0;
                }
                else
                {
                    angle = 90;
                }
                return angle;
            }
        }

        public double XGlassBelt
        {
            get
            {
                if (XGlass > YGlass)
                {
                    return YGlass;
                }
                return XGlass;
            }
        }
        
        #endregion
    }

    /// <summary>
    /// 方向枚举类，上下左右对应1234
    /// </summary>
    public enum TypeOrd_enum
    {
        Top = 1,
        Down = 2,
        Left = 3,
        Right = 4,
    }
}
