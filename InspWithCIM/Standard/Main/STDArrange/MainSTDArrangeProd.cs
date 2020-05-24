using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealConfigFile;
using BasicClass;
using DealImageProcess;
using System.ComponentModel;
using DealCalibrate;

namespace Main
{
    public class MainSTDArrangeProd : INotifyPropertyChanged
    {
        #region 静态类实例
        public static MainSTDArrangeProd M_I = new MainSTDArrangeProd();

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region 通知变更
        public void CallChanging()
        {
            try
            {
                if (PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AngleInit"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AnglePrec"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DeltaVision"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DeltaReal"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("DeltaRobot"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AngleITOPlat"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("AnglePreciToITOPlat"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PosSTDITOPlat"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SizeGlassPlat"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("SizeGlassBaseOnITOCorner"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PosTransITOPlat"));
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("PosTransITOPlatReal"));
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region 玻璃基础参数
        /// <summary>
        /// 观察面玻璃X
        /// </summary>
        public double GlassX
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[0].DblValue;
            }
        }
        /// <summary>
        /// 观察面玻璃Y
        /// </summary>
        public double GlassY
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[1].DblValue;
            }
        }
        /// <summary>
        /// 观察面玻璃Z
        /// </summary>
        public double GlassZ
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[2].DblValue;
            }
        }

        /// <summary>
        /// 玻璃尺寸的2D表示
        /// </summary>
        public Point2D SizeGlass
        {
            get
            {
                return new Point2D(GlassX, GlassY);
            }
        }

        private double areaGlass;
        /// <summary>
        /// 玻璃面积
        /// </summary>
        public double AreaGlass
        {
            get
            {
                if (areaGlass == 0)
                {
                    areaGlass = GlassX * GlassY;
                    if (areaGlass == 0)
                    {
                        areaGlass = 1;
                    }
                }
                return areaGlass;
            }
        }
        /// <summary>
        /// 二维码X
        /// </summary>
        public double CodeX
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[3].DblValue;
            }
        }
        /// <summary>
        /// 二维码Y
        /// </summary>
        public double CodeY
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[4].DblValue;
            }
        }
        /// <summary>
        /// 上电极宽度
        /// </summary>
        public double ElectrodeTop
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[5].DblValue;
            }
        }
        /// <summary>
        /// 下电极宽度
        /// </summary>
        public double ElectrodeBottom
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[6].DblValue;
            }
        }
        /// <summary>
        /// 左电极宽度
        /// </summary>
        public double ElectrodeLeft
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[7].DblValue;
            }
        }
        /// <summary>
        /// 右电极宽度
        /// </summary>
        public double ElectrodeRight
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[8].DblValue;
            }
        }

        public Point4D PosCodeSTD
        {
            get
            {
                double stdX = ParStd.Value1("std5");
                double stdY = ParStd.Value2("std5");
                return new Point4D(stdX - GlassX / 2 + CodeX, stdY - GlassY / 2 + CodeY, 0, 0);
            }
        }

        public int TimeBeltRun
        {
            get
            {
                return Convert.ToInt32((GlassX + 20) * ParStd.Value1("std17") / 100);
            }
        }
        #endregion

        #region 取料
        /// <summary>
        /// 默认机器人吸抓的0°为 X>Y的方向
        /// </summary>
        public double AngleInit
        {
            get
            {
                return GlassX > GlassY ? 0 : 90;
            }
        }

        public double GlassSum
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[17].DblValue;
            }
        }
        #endregion

        #region 精确定位
        /// <summary>
        /// 精确定位时的角度，挂钩背光方向
        /// </summary>
        public double AnglePrec
        {
            get
            {
                double angle = (int)ParSTDArrange.P_I.TypePreci_E;

                return (GlassX > GlassY ? 90 : 0) + angle;
            }
        }

        /// <summary>
        /// 将视觉偏差转换为最终偏差
        /// </summary>
        /// <param name="deltaVision">视觉算出的偏差</param>
        /// <returns></returns>
        public Point2D GetRealDelta(Point2D deltaVision)
        {
            try
            {
                DeltaVision = deltaVision;

                DeltaRobot = GetRealDelta();
                //认为机器人的偏差位最终偏差
                return DeltaReal;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainSTDArrangeProd.GetRealDelta", ex);
                return new Point2D();
            }
        }

        public Point2D TransDelta(Point2D oriPt, double angle1, double angle2)
        {
            return MultMatrix(oriPt, angle1 - angle2);
        }

        Point2D MultMatrix(Point2D pt, double angle)
        {
            double radius = angle / 180 * Math.PI;
            double x = pt.DblValue1 * Math.Cos(radius) - pt.DblValue2 * Math.Sin(radius);
            double y = pt.DblValue1 * Math.Sin(radius) + pt.DblValue2 * Math.Cos(radius);
            return new Point2D(x, y);
        }

        /// <summary>
        /// 将视觉偏差转换为最终偏差
        /// </summary>
        /// <returns></returns>
        public Point2D GetRealDelta()
        {
            try
            {
                FunCalibRotate fun = new FunCalibRotate();
                //角度分两块 1 精确定位到残边平台
                //2 残边平台在转移到机器人坐标系
                //相当于将一组偏差转两次，设置好对应关系后相加即可

                //先得到实际坐标系偏差
                DeltaReal = fun.GetPoint_AfterRotation(
                    AnglePreciToITOPlat / 180 * Math.PI, new Point2D(0, 0), DeltaVision);

                DeltaRobot = fun.GetPoint_AfterRotation(
                    ((double)ParSTDArrange.P_I.TypeRobotCoor_E) / 180 * Math.PI, new Point2D(0, 0), DeltaReal);
                //认为机器人的偏差位最终偏差
                return DeltaReal;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainSTDArrangeProd.GetRealDelta", ex);
                return new Point2D();
            }
        }

        /// <summary>
        /// 视觉得到的精确定位偏差
        /// </summary>
        public Point2D DeltaVision { get; set; }

        /// <summary>
        /// 转换后的偏差
        /// </summary>
        public Point2D DeltaReal { get; set; }

        /// <summary>
        /// 给机器人偏差
        /// </summary>
        public Point2D DeltaRobot { get; set; }

        #endregion

        #region 残才平台
        public double AnglePlat
        {
            get
            {
                int angle = 0;
                if (ElectrodeTop > 0)
                {
                    if (ElectrodeLeft > 0)
                        angle -= 90;
                }
                else if (ElectrodeBottom > 0)
                {
                    angle += 90;
                    if (!(ElectrodeRight > 0))
                        angle += 90;
                }
                else if (ElectrodeLeft > 0)
                {
                    angle -= 90;
                }
                else if (ElectrodeRight > 0)
                {
                    angle += 90;
                }

                return (angle + 360) % 360;
            }
        }

        public double GetPlatDeltaX
        {
            get
            {
                return (AnglePlat % 180 > 0 ? GlassY : GlassX) / 2 - ParStd.Value1("std13");
            }
        }

        public double GetPlatDeltaY
        {
            get
            {
                return (AnglePlat % 180 > 0 ? GlassX : GlassY) / 2 - ParStd.Value2("std13");
            }
        }

        /// <summary>
        ///  残边平台玻璃尺寸
        /// </summary>
        public Point2D SizeGlassPlat
        {
            get
            {
                if (AngleITOPlat == 0 || AngleITOPlat == 180)
                {
                    return new Point2D(GlassX, GlassY);
                }
                else
                {
                    return new Point2D(GlassY, GlassX);
                }
            }
        }

        /// <summary>
        /// 根据平台角产生的玻璃带符号尺寸，用于计算机器人基准位置
        /// </summary>
        public Point2D SizeGlassBaseOnITOCorner
        {
            get
            {
                Point2D p1 = new Point2D();
                if (ParSTDArrange.P_I.ITOPlatSTDCorner_E == ITOPlatCorner_Enum.BottomLeft ||
                    ParSTDArrange.P_I.ITOPlatSTDCorner_E == ITOPlatCorner_Enum.TopLeft)
                {
                    p1.DblValue1 = SizeGlassPlat.DblValue1;
                }
                else
                {
                    p1.DblValue1 = -SizeGlassPlat.DblValue1;

                }

                if (ParSTDArrange.P_I.ITOPlatSTDCorner_E == ITOPlatCorner_Enum.BottomLeft ||
                    ParSTDArrange.P_I.ITOPlatSTDCorner_E == ITOPlatCorner_Enum.BottomRight)
                {
                    p1.DblValue2 = SizeGlassPlat.DblValue2;
                }
                else
                {
                    p1.DblValue2 = -SizeGlassPlat.DblValue2;
                }
                return p1;
            }
        }

        /// <summary>
        /// 残边个数
        /// </summary>
        public int NumIOT
        {
            get
            {
                int num = 0;
                if (ElectrodeTop != 0)
                {
                    num++;
                }
                if (ElectrodeBottom != 0)
                {
                    num++;
                }
                if (ElectrodeLeft != 0)
                {
                    num++;
                }
                if (ElectrodeRight != 0)
                {
                    num++;
                }
                return num;
            }
        }

        /// <summary>
        /// 电极边交点位置
        /// </summary>
        public ITOCross_Enum GlassITOCross_Enum
        {
            get
            {
                ITOCross_Enum itoCross = ITOCross_Enum.BottomLeft;
                if (NumIOT == 1)
                {
                    if (ElectrodeTop > 0)
                    {
                        itoCross = ITOCross_Enum.TopLeft;
                    }
                    if (ElectrodeLeft > 0)
                    {
                        itoCross = ITOCross_Enum.BottomLeft;
                    }
                    if (ElectrodeBottom > 0)
                    {
                        itoCross = ITOCross_Enum.BottomRight;
                    }
                    if (ElectrodeRight > 0)
                    {
                        itoCross = ITOCross_Enum.TopRight;
                    }
                }
                else if (NumIOT == 2)
                {
                    if (ElectrodeTop > 0 && ElectrodeLeft > 0)
                    {
                        itoCross = ITOCross_Enum.TopLeft;
                    }
                    if (ElectrodeLeft > 0 && ElectrodeBottom > 0)
                    {
                        itoCross = ITOCross_Enum.BottomLeft;
                    }
                    if (ElectrodeBottom > 0 && ElectrodeRight > 0)
                    {
                        itoCross = ITOCross_Enum.BottomRight;
                    }
                    if (ElectrodeRight > 0 && ElectrodeTop > 0)
                    {
                        itoCross = ITOCross_Enum.TopRight;
                    }
                }
                return itoCross;
            }
        }

        /// <summary>
        /// 残边平台玻璃角度
        /// </summary>
        public double AngleITOPlat
        {
            get
            {
                if (NumIOT == 1)
                {
                    if (ParSTDArrange.P_I.ITOPlatSTDCorner_E == ITOPlatCorner_Enum.BottomLeft || ParSTDArrange.P_I.ITOPlatSTDCorner_E == ITOPlatCorner_Enum.BottomRight)
                    {
                        return (-((double)GlassITOCross_Enum - 90) + 360) % 360;
                    }
                    else
                    {
                        return ((double)GlassITOCross_Enum - 90 + 360) % 360;
                    }
                }
                else
                {
                    return ((int)GlassITOCross_Enum - (int)ParSTDArrange.P_I.ITOPlatSTDCorner_E + 360) % 360;//-90到180
                }
            }
        }

        /// <summary>
        /// 精确定位到残边平台的角度
        /// </summary>
        double AnglePreciToITOPlat
        {
            get
            {
                return (AngleITOPlat - AnglePrec + 360) % 360;
            }
        }

        /// <summary>
        /// 机器人交接残边平台基准位置
        /// </summary>
        public Point4D PosTransITOPlat
        {
            get
            {
                //做一个和机器人坐标系有关系的旋转
                FunCalibRotate fun = new FunCalibRotate();
                //是为了将带有符号的玻璃尺寸统一给机器人
                Point2D p1 = fun.GetPoint_AfterRotation((double)ParSTDArrange.P_I.TypeRobotCoor_E / 180 * Math.PI, new Point2D(0, 0), SizeGlassBaseOnITOCorner);
                //XY轴分别移动旋转之后玻璃尺寸的一半
                Point4D posSTDITOPlat = new Point4D(PosSTDITOPlat.DblValue1 + p1.DblValue1 / 2, PosSTDITOPlat.DblValue2 + p1.DblValue2 / 2, 0, AngleITOPlat);
                return posSTDITOPlat;
            }
        }

        Point2D PosSTDITOPlat
        {
            get
            {
                double stdX = ParStd.Value1("std2");
                double stdY = ParStd.Value2("std2");
                return new Point2D(stdX, stdY);
            }
        }

        /// <summary>
        /// 最终交接位置
        /// </summary>
        public Point4D PosTransITOPlatReal
        {
            get
            {
                return Point4D.Add(PosTransITOPlat, new Point4D(DeltaRobot.DblValue1, DeltaRobot.DblValue2, 0, 0));
            }
        }
        #endregion

        #region 皮带线
        public int BeltInterval
        {
            get
            {
                double width = BeltAngleToPLC == 0 ? SizeGlassPlat.DblValue2 : SizeGlassPlat.DblValue1;
                double speed = 4.75;
                return (int)Math.Ceiling((width + 10) / speed);
            }
        }

        public double BeltAngle
        {
            get
            {
                return SizeGlassPlat.DblValue1 > SizeGlassPlat.DblValue2 ? 90 : 0;
            }
        }

        public double BeltAngleToPLC
        {
            get
            {
                return BeltAngle;
                int tmpAngle = GlassX > GlassY ? 0 : 90;
                return (AnglePlat + tmpAngle) % 180;
            }
        }
        #endregion

        #region 插栏

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
        /// 龙骨间距 ，作为插蓝的标准值来用
        /// </summary>
        public double KeelInterval
        {
            get
            {
                return ParConfigPar.P_I.ParProduct_L[14].DblValue;
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
        /// 生成所有龙骨位置集合
        /// </summary>
        /// <returns></returns>
        public List<List<Point2D>> CreatKeelPos_L()
        {
            List<List<Point2D>> pList = new List<List<Point2D>>();
            try
            {
                for (int i = 0; i < KeelCol; i++)
                {
                    List<Point2D> point = new List<Point2D>();
                    for (int j = 0; j < RowCST; j++)
                    {
                        point.Add(new Point2D(DblCSTDis[i], 0));
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
        /// 起始行列
        /// </summary>
        public int RowStart
        {
            get
            {
                return (int)ParConfigPar.P_I.ParProduct_L[11].DblValue;
            }
        }

        public int ColStart
        {
            get
            {
                return (int)ParConfigPar.P_I.ParProduct_L[12].DblValue;
            }
        }

        public bool BlIsSmallCST
        {
            get { return ParConfigPar.P_I[15].DblValue == 1; }
        }

        /// <summary>
        /// 实际插栏坐标,满卡塞
        /// </summary>
        /// <returns></returns>
        public List<List<Point2D>> RealCellPos_L()
        {
            try
            {
                List<List<Point2D>> pReal = new List<List<Point2D>>();
                //起始数目               
                List<List<Point2D>> pAll = CreatKeelPos_L();
                return pAll;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("VarConfigPWork", ex);
                return null;
            }
        }

        /// <summary>
        /// 各列相对于边的基准位置
        /// </summary>
        public double[] DblCSTDis
        {
            get
            {
                double[] dblValue = new double[ColCST];
                //靠边定位
                double width = BlIsSmallCST ? ParStd.Value1("std8") : ParStd.Value2("std8");

                //卡塞两边的间隙
                double dis = (width - ColCST * KeelInterval) / 2;
                try
                {
                    for (int i = 0; i < KeelCol; i++)  //KeelCol 龙骨个数
                    {
                        dblValue[i] = dis + KeelInterval * i;
                    }
                }
                catch (Exception)
                {

                }
                return dblValue;
            }
        }

        /// <summary>
        /// 每列龙骨位置
        /// </summary>
        public double[] KeelPos
        {
            get
            {
                double[] pos = new double[KeelCol];
                for (int i = 0; i < KeelCol; i++)
                {
                    pos[i] = ParConfigPar.P_I.ParProduct_L[21 + i].DblValue;
                }
                return pos;
            }
        }

        public double InsertDeltaY
        {
            get
            {
                double stdY = ParStd.Value1("std4");

                double y = stdY + ((AngleInsert == 0 || AngleInsert == 180) ? GlassY / 2 : GlassX / 2) - ParStd.Value2("std4");
                return y;
            }
        }

        public double AngleInsert
        {
            get
            {
                int tempAngle = 0;
                int direction = (int)ParConfigPar.P_I.ParProduct_L[13].DblValue;
                if (direction == 1)
                {
                    tempAngle = 0;
                }
                else if (direction == 2)
                {
                    tempAngle = 180;
                }
                else if (direction == 3)
                {
                    tempAngle = -90;
                }
                else if (direction == 4)
                {
                    tempAngle = 90;
                }

                double ret = tempAngle + 360;
                ret %= 360;
                return ret;
            }
        }

        #endregion

        #region 寻遍
        double AngleInsp
        {
            get
            {
                if (NumIOT == 1)
                {
                    return AngleITOPlat;
                }
                else
                {
                    if (SizeGlassPlat.DblValue1 > SizeGlassPlat.DblValue2)
                    {
                        return AngleITOPlat;
                    }
                    else
                    {
                        double angle = AngleITOPlat + 90;
                        if (angle == 270)
                        {
                            angle = -90;
                        }
                        return angle;
                    }
                }
            }

        }

        public Point2D SizeGlassInsp
        {
            get
            {
                if (AngleInsp == 0 || AngleInsp == 180)
                {
                    return new Point2D(GlassX, GlassY);
                }
                else
                {
                    return new Point2D(GlassY, GlassX);
                }
            }
        }
        /// <summary>
        /// 讯边检平台的基准位置
        /// </summary>
        public Point4D PosTransInsp
        {
            get
            {
                double stdX = ParStd.Value1("std3");
                double stdY = ParStd.Value2("std3");
                return new Point4D(stdX - SizeGlassInsp.DblValue1 / 2, stdY + SizeGlassInsp.DblValue2 / 2, 0, AngleInsp);
            }
        }
        #endregion
    }

    public enum AngleType_Enum
    {
        Normal = 0,
        Rotate90 = 90,
        Rotate180 = 180,
        NRotate90 = -90,
    }
    /// <summary>
    /// 电极边交点
    /// </summary>
    public enum ITOCross_Enum
    {
        TopLeft = -90,
        BottomLeft = 0,
        BottomRight = 90,
        TopRight = 180,
    }
}
