using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DealConfigFile;
using BasicClass;

namespace DealPLC
{
    public class RegCameraData : BaseRegPLC
    {
        #region 静态类实例
        public static RegCameraData R_I = new RegCameraData();
        #endregion 静态类实例

        #region 定义
        /// <summary>
        ///寄存器个数
        /// </summary>
        public override int NumReg //数据寄存器,根据相机数目不同，略有不同
        {
            get
            {
                int intNum = 0;
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)
                {
                    switch (ParCameraWork.NumCamera)
                    {
                        case 1:
                            intNum = 6;
                            break;
                        case 2:
                            intNum = 12;
                            break;
                        case 3:
                            intNum = 18;
                            break;
                        case 4:
                            intNum = 24;
                            break;
                        case 5:
                            intNum = 30;
                            break;
                        case 6:
                            intNum = 36;
                            break;
                        case 7:
                            intNum = 42;
                            break;
                        case 8:
                            intNum = 48;
                            break;
                    }
                }
                else
                {
                    switch (ParCameraWork.NumCamera)
                    {
                        case 1:
                            intNum = 7;
                            break;
                        case 2:
                            intNum = 14;
                            break;
                        case 3:
                            intNum = 21;
                            break;
                        case 4:
                            intNum = 28;
                            break;
                        case 5:
                            intNum = 35;
                            break;
                        case 6:
                            intNum = 42;
                            break;
                        case 7:
                            intNum = 49;
                            break;
                        case 8:
                            intNum = 56;
                            break;
                    }
                }
                return intNum;
            }
        }

        public override string[] PLCSendPC
        {
            get
            {
                string[] str = null;
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)
                {
                    str = new string[]
                     {
                        "","","","","","",//相机1
                        "","","","","","",//相机2
                        "","","","","","",//相机3
                        "","","","","","",//相机4
                        "","","","","","",//相机5
                        "","","","","","", //相机6
                        "","","","","","", //相机7
                        "","","","","","", //相机8
                     };
                }
                else
                {
                    str = new string[]
                     {
                        "Y","","","","","","",//相机1
                        "Y","","","","","","",//相机2
                        "Y","","","","","","",//相机3
                        "Y","","","","","","",//相机4
                        "Y","","","","","","",//相机5
                        "Y","","","","","","", //相机6
                        "Y","","","","","","", //相机7
                        "Y","","","","","","", //相机8
                     };
                }
                return str;
            }
        }

        public override string[] PCSendPLC
        {
            get
            {
                string[] str = null;
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)
                {
                    str = new string[]
                     {
                        "Y","Y","Y","Y","Y","Y",//相机1
                        "Y","Y","Y","Y","Y","Y",//相机2
                        "Y","Y","Y","Y","Y","Y",//相机3
                        "Y","Y","Y","Y","Y","Y",//相机4
                        "Y","Y","Y","Y","Y","Y",//相机5
                        "Y","Y","Y","Y","Y","Y", //相机6
                        "Y","Y","Y","Y","Y","Y", //相机7
                        "Y","Y","Y","Y","Y","Y", //相机8
                     };
                }
                else
                {
                    str = new string[]
                     {
                        "Y","Y","Y","Y","Y","Y","Y",//相机1
                        "Y","Y","Y","Y","Y","Y","Y",//相机2
                        "Y","Y","Y","Y","Y","Y","Y",//相机3
                        "Y","Y","Y","Y","Y","Y","Y",//相机4
                        "Y","Y","Y","Y","Y","Y","Y",//相机5
                        "Y","Y","Y","Y","Y","Y","Y", //相机6
                        "Y","Y","Y","Y","Y","Y","Y", //相机7
                        "Y","Y","Y","Y","Y","Y","Y", //相机8
                     };
                }
                return str;
            }
        }

        public override string[] Annotation
        {
            get
            {
                string[] str = null;
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)
                {
                    str = new string[]
                     {
                        "相机1拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机2拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机3拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机4拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机5拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机6拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机7拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "8相机拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                     };
                }
                else
                {
                    str = new string[]
                     {
                        "相机1拍照","拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机2拍照","拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机3拍照","拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机4拍照","拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机5拍照","拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机6拍照","拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机7拍照","拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                        "相机8拍照","拍照结果","X数值","Y数值","Z数值","R数值","数据传输完成",
                     };
                }
                return str;
            }
        }
        //数据寄存器的系数
        public override double[] Co
        {
            get
            {
                double[] co = null;
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)
                {
                    co = new double[]
                    {
                        1,0.0001,0.0001,0.0001,0.0001,1,
                        1,0.0001,0.0001,0.0001,0.0001,1,
                        1,0.0001,0.0001,0.0001,0.0001,1,
                        1,0.0001,0.0001,0.0001,0.0001,1,
                        1,0.0001,0.0001,0.0001,0.0001,1,
                        1,0.0001,0.0001,0.0001,0.0001,1,
                        1,0.0001,0.0001,0.0001,0.0001,1,
                        1,0.0001,0.0001,0.0001,0.0001,1,
                    };
                }
                else
                {
                    co = new double[]
                    {
                        1,1,0.0001,0.0001,0.0001,0.0001,1,
                        1,1,0.0001,0.0001,0.0001,0.0001,1,
                        1,1,0.0001,0.0001,0.0001,0.0001,1,
                        1,1,0.0001,0.0001,0.0001,0.0001,1,
                        1,1,0.0001,0.0001,0.0001,0.0001,1,
                        1,1,0.0001,0.0001,0.0001,0.0001,1,
                        1,1,0.0001,0.0001,0.0001,0.0001,1,
                        1,1,0.0001,0.0001,0.0001,0.0001,1,
                    };
                }
                return co;
            }
        }        
        #endregion 定义

        #region 初始化
        public RegCameraData()
        {
            base.NameClass = "RegCameraData";

            //ini存储地址
            base.PathRegIni = ParPathRoot.PathRoot + "Store\\PLC\\RegCameraData.ini";
        }
        #endregion 初始化
    }

}
