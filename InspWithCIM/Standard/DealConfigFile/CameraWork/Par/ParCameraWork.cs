using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace DealConfigFile
{
    public partial class ParCameraWork : BaseClass
    {
        #region 静态类实例
        public static ParCameraWork P_I = new ParCameraWork();
        #endregion 静态类实例

        #region 定义
        //相机个数
        static int numCamera = 1;
        public static int NumCamera
        {
            get
            {
                if (numCamera < 1 || numCamera > 8)
                {
                    return 1;
                }
                return numCamera;
            }
            set
            {
                numCamera = value;
            }
        }

        //List
        public List<ParCameraWork> ParCameraWork_L = new List<ParCameraWork>();
        public ParCameraWork this[string cam]
        {
            get
            {
                try
                {
                    int index = 0;
                    try
                    {
                        index = int.Parse(cam.ToLower().Replace("cam", "")) - 1;
                    }
                    catch (Exception ex)
                    {

                    }
                    return ParCameraWork_L[index];
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError("ParCameraWork", ex);
                    return null;
                }
            }
        }

        //图片保存格式
        public FormatImage_enum FormatImage_e { set; get; }

        //坐标系类型
        public TypeImageCoord_enum TypeImageCoord_e { set; get; }

        #region 拍照位置个数
        public NumPhoto_enum NumPhoto_e { set; get; }

        //相机1
        public int NumPosCam1
        {
            get
            {
                return (int)ParCameraWork_L[0].NumPhoto_e;
            }        
        }

        //相机2
        public int NumPosCam2
        {
            get
            {
                return (int)ParCameraWork_L[1].NumPhoto_e;
            }   
        }

        //相机3
        public int NumPosCam3
        {
            get
            {
                return (int)ParCameraWork_L[2].NumPhoto_e;
            }   
        }

        //相机4
        public int NumPosCam4
        {
            get
            {
                return (int)ParCameraWork_L[3].NumPhoto_e;
            }   
        }

        //相机5
        public int NumPosCam5
        {
            get
            {
                return (int)ParCameraWork_L[4].NumPhoto_e;
            }   
        }

        //相机6
        public int NumPosCam6
        {
            get
            {
                return (int)ParCameraWork_L[5].NumPhoto_e;
            }   
        }

        public int NumPosCam7
        {
            get
            {
                return (int)ParCameraWork_L[6].NumPhoto_e;
            }   
        }

        public int NumPosCam8
        {
            get
            {
                return (int)ParCameraWork_L[7].NumPhoto_e;
            }   
        }

        //索引器
        public int this[int index]
        {
            get
            {
                try
                {
                    int num = 0;
                    switch (index)
                    {
                        case 1:
                            num = NumPosCam1;
                            break;

                        case 2:
                            num = NumPosCam2;
                            break;

                        case 3:
                            num = NumPosCam3;
                            break;

                        case 4:
                            num = NumPosCam4;
                            break;

                        case 5:
                            num = NumPosCam5;
                            break;

                        case 6:
                            num = NumPosCam6;
                            break;

                        case 7:
                            num = NumPosCam7;
                            break;

                        case 8:
                            num = NumPosCam8;
                            break;
                    }
                    return num;
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError("ParCameraWork", ex);
                    return 1;
                }
            }
        }

        /// <summary>
        /// 一个相机总的拍照位置数目
        /// </summary>
        public int Sum1
        {
            get
            {
                return NumPosCam1;
            }
        }

        /// <summary>
        /// 2个相机总的拍照位置数目
        /// </summary>
        public int Sum2
        {
            get
            {
                return NumPosCam1 + NumPosCam2;
            }
        }

        /// <summary>
        /// 3个相机总的拍照位置数目
        /// </summary>
        public int Sum3
        {
            get
            {
                return NumPosCam1 + NumPosCam2 + NumPosCam3;
            }
        }

        /// <summary>
        /// 4个相机总的拍照位置数目
        /// </summary>
        public int Sum4
        {
            get
            {
                return NumPosCam1 + NumPosCam2 + NumPosCam3 + NumPosCam4;
            }
        }

        /// <summary>
        /// 5个相机总的拍照位置数目
        /// </summary>
        public int Sum5
        {
            get
            {
                return NumPosCam1 + NumPosCam2 + NumPosCam3 + NumPosCam4 + NumPosCam5;
            }
        }

        /// <summary>
        /// 6个相机总的拍照位置数目
        /// </summary>
        public int Sum6
        {
            get
            {
                return NumPosCam1 + NumPosCam2 + NumPosCam3 + NumPosCam4 + NumPosCam5 + NumPosCam6;
            }
        }

        /// <summary>
        /// 7个相机总的拍照位置数目
        /// </summary>
        public int Sum7
        {
            get
            {
                return NumPosCam1 + NumPosCam2 + NumPosCam3 + NumPosCam4 + NumPosCam5 + NumPosCam6 + NumPosCam7;
            }
        }

        /// <summary>
        /// 8个相机总的拍照位置数目
        /// </summary>
        public int Sum8
        {
            get
            {
                return NumPosCam1 + NumPosCam2 + NumPosCam3 + NumPosCam4 + NumPosCam5 + NumPosCam6 + NumPosCam7 + NumPosCam8;
            }
        }
        #endregion 拍照位置
        #endregion 定义

        #region 初始化
        public ParCameraWork()
        {
            NameClass = "ParCameraWork";
        }
        #endregion 初始化
    }

    //拍照次数
    public enum NumPhoto_enum
    {
        Num1 = 1,
        Num2 = 2,
        Num3 = 3,
        Num4 = 4,
        Num5 = 5,
        Num6 = 6,
        Num7 = 7,
        Num8 = 8,
    }

    //坐标系类型
    public enum TypeImageCoord_enum
    {
        LeftTop = 1,
        LeftButtom = 3,  
        Center = 2,           
    }
}
