using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealConfigFile;
using BasicClass;

namespace Main
{
    /// <summary>
    /// 此类为设备参数类，主要规划不同设备硬件关系差别
    /// </summary>
    public partial class ParSTDArrange : BaseClass
    {
        #region 静态类实例
        public static ParSTDArrange P_I = new ParSTDArrange();
        #endregion

        #region 是否为收片机
        public bool IsArrange { get; set; }
        #endregion

        #region 大背光的方向
        private TypePreciLight_Enum typePreci = TypePreciLight_Enum.Vertical;
        /// <summary>
        /// 背光方向 默认竖向
        /// </summary>
        public TypePreciLight_Enum TypePreci_E
        {
            get
            {
                return typePreci;
            }
            set
            {
                typePreci = value;
            }
        }
        #endregion

        #region 机器人坐标系角度
        private TypeRobotCoor_Enum typeRobotCoor = TypeRobotCoor_Enum.Front;

        /// <summary>
        /// 机器人坐标系的角度,表示从机器人坐标系到正常坐标系的夹角
        /// </summary>
        public TypeRobotCoor_Enum TypeRobotCoor_E
        {
            get
            {
                return typeRobotCoor;
            }
            set
            {
                typeRobotCoor = value;
            }
        }
        #endregion

        #region 平台交接方式
        private TypePlatWork_Enum typePlatWork = TypePlatWork_Enum.MotionLess;
        /// <summary>
        /// 记录每天的残边平台工作方式，比如X方向翻转，不动作等等
        /// </summary>
        public TypePlatWork_Enum TypePlatWork_E
        {
            get { return typePlatWork; }
            set { typePlatWork = value; }
        }
        #endregion

        #region 残边平台基准点
        private ITOPlatCorner_Enum platSTDCorner = ITOPlatCorner_Enum.BottomLeft;
        /// <summary>
        /// 残边平台的基准位置
        /// </summary>
        public ITOPlatCorner_Enum ITOPlatSTDCorner_E
        {
            get
            {
                return platSTDCorner;
            }
            set
            {
                platSTDCorner = value;
            }
        }

        #endregion

        #region 相机1功能
        private FunCam_Enum funCam1_E = FunCam_Enum.不使用;
        /// <summary>
        /// 相机1功能
        /// </summary>
        public FunCam_Enum FunCam1_E
        {
            get { return funCam1_E; }
            set { funCam1_E = value; }
        }

        #endregion

        #region 相机2功能
        private FunCam_Enum funCam2_E = FunCam_Enum.不使用;
        /// <summary>
        /// i相机2功能
        /// </summary>
        public FunCam_Enum FunCam2_E
        {
            get { return funCam2_E; }
            set { funCam2_E = value; }
        }

        #endregion

        #region 相机3功能
        private FunCam_Enum funCam3_E = FunCam_Enum.不使用;
        /// <summary>
        /// 相机3功能
        /// </summary>
        public FunCam_Enum FunCam3_E
        {
            get { return funCam3_E; }
            set { funCam3_E = value; }
        }

        #endregion

        #region 相机4功能
        private FunCam_Enum funCam4_E = FunCam_Enum.不使用;
        /// <summary>
        /// 相机3功能
        /// </summary>
        public FunCam_Enum FunCam4_E
        {
            get { return funCam4_E; }
            set { funCam4_E = value; }
        }
        #endregion

        #region 相机5功能
        private FunCam_Enum funCam5_E = FunCam_Enum.不使用;
        public FunCam_Enum FunCam5_E
        {
            get { return funCam5_E; }
            set { funCam5_E = value; }
        }
        #endregion

        #region 相机6功能
        private FunCam_Enum funCam6_E = FunCam_Enum.不使用;
        /// <summary>
        /// 相机6功能
        /// </summary>
        public FunCam_Enum FunCam6_E
        {
            get { return funCam6_E; }
            set { funCam6_E = value; }
        }
        #endregion
    }

    /// <summary>
    /// 表示大背光的方向类型
    /// </summary>
    public enum TypePreciLight_Enum
    {
        /// <summary>
        /// 横向的，即X>Y
        /// </summary>
        Horizontal = 0,
        /// <summary>
        /// 竖向的，即
        ///  Y>X
        /// </summary>
        Vertical = 90,
    }

    /// <summary>
    /// 机器人坐标系的角度,表示从机器人坐标系-正常坐标系的夹角
    /// </summary>
    public enum TypeRobotCoor_Enum
    {
        /// <summary>
        /// 正常
        /// </summary>
        Front = 0,

        Right = 90,
        Back = 180,
        Left = -90,
    }

    /// <summary>
    /// 平台交接方式
    /// </summary>
    public enum TypePlatWork_Enum
    {
        /// <summary>
        /// 正常
        /// </summary>
        MotionLess,
        /// <summary>
        /// X方向翻转
        /// </summary>
        RotateOnX = 1,
        /// <summary>
        /// Y方向翻转
        /// </summary>
        RotateOnY = -1,
    }

    /// <summary>
    /// 相对于观察面的残才角
    /// </summary>
    public enum ITOPlatCorner_Enum
    {
        /// <summary>
        /// 左上角为基准
        /// </summary>
        TopLeft = -90,
        /// <summary>
        /// 右上角为基准
        /// </summary>
        TopRight = 180,
        /// <summary>
        /// 左下角为基准
        /// </summary>
        BottomLeft = 0,
        /// <summary>
        /// 右下角为基准
        /// </summary>
        BottomRight = 90,
    }
}
