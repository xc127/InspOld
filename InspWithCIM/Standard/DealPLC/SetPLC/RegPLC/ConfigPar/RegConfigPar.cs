using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using BasicClass;
using DealConfigFile;

namespace DealPLC
{
    public class RegConfigPar : BaseRegPLC
    {
        #region 静态类实例
        public static RegConfigPar R_I = new RegConfigPar();
        #endregion 静态类实例

        #region 定义
        string[] g_AnnotationProduct = new string[]
        { 
            "配置文件序号","配置文件名称","","","","","","","","",
            "产品参数","","","","","","","","","",
            "","","","","","","","","","",
            "","","","","","","","","","",
            "","","","","","","","","","",
            "","","","","","","","","","",
            "","","","","","","","","","",
            "","","","","","","","","","",
            "","","","","","","","","","",
            "","","","","","","","","","",
        };

        public override string []Annotation
        {
            get
            {
                return GetAnnotaion();
            }
        }         

        public override int NumReg
        {
            get
            {
                int intNum = 0;
                switch (ParCameraWork.NumCamera)
                {
                    case 1:
                        intNum= ParCameraWork.P_I.Sum1 * 4;
                        break;
                    case 2:
                        intNum = ParCameraWork.P_I.Sum2 * 4;
                        break;
                    case 3:
                        intNum = ParCameraWork.P_I.Sum3 * 4;
                        break;
                    case 4:
                        intNum = ParCameraWork.P_I.Sum4 * 4;
                        break;
                    case 5:
                        intNum =  ParCameraWork.P_I.Sum5 * 4;
                        break;
                    case 6:
                        intNum =  ParCameraWork.P_I.Sum6 * 4;
                        break;
                    case 7:
                        intNum = ParCameraWork.P_I.Sum7 * 4;
                        break;
                    case 8:
                        intNum = ParCameraWork.P_I.Sum8 * 4;
                        break;
                }

                return 10 + NumRegSet + intNum;            
            }
            set
            {

            }
        }
        #endregion 定义

        public RegConfigPar()
        {
            base.NameClass = "RegConfigPar";

            //ini存储地址
            base.PathRegIni = ParPathRoot.PathRoot + "\\Store\\PLC\\RegConfigPar.ini";

            base.PCSendPLC = new string[]
            { 
                "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","", 
                "","","","","","","","", 
                "","","","","","","","", 
                "","","","","","","","", 
                "","","","","","","","", 

                 "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","",
                "","","","","","","","", 
                "","","","","","","","", 
                "","","","","","","","", 
                "","","","","","","","", 
                "","","","","","","","", 
            };

            base.PLCSendPC = new string[]
            { 
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y", 
                "Y","Y","Y","Y","Y","Y","Y","Y", 
                "Y","Y","Y","Y","Y","Y","Y","Y", 
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y",

                 "Y","Y","Y","Y","Y","Y","Y","Y", 
                "Y","Y","Y","Y","Y","Y","Y","Y", 
                "Y","Y","Y","Y","Y","Y","Y","Y", 
                "Y","Y","Y","Y","Y","Y","Y","Y",
                "Y","Y","Y","Y","Y","Y","Y","Y"
             };


            //数据寄存器的系数
            base.Co = new double[]
            { 
                1,1,1,1,1,1,1,1,1,1,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
                0.001,0.001,0.001,0.001,0.001,0.001,0.001,0.001,
             };
        }

        /// <summary>
        /// 获取注释
        /// </summary>
        /// <returns></returns>
        string[] GetAnnotaion()
        {
            try
            {
                string[] str = new string[NumReg];
                for (int i = 0; i < NumReg; i++)
                {
                    //产品参数
                    if (i < 10 + NumRegSet)
                    {
                        str[i] = g_AnnotationProduct[i];
                    }
                    else
                    {
                        str[i] = GetAnnotaionPosPhoto()[i - (10 + NumRegSet)];//相机注释
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("RegConfigPar", ex);
                return null;
            }
        }

        /// <summary>
        /// 相机拍照位置注释
        /// </summary>
        /// <returns></returns>
        public List<string> GetAnnotaionPosPhoto()
        {
            try
            {
                List<string> str_List = new List<string>();
                for (int i = 0; i < ParCameraWork.NumCamera; i++)
                {
                    for (int j = 0; j < ParCameraWork.P_I[i + 1]; j++)
                    {
                        string strX = "相机" + (i + 1).ToString() + "位置" + (j + 1).ToString() + "X";
                        str_List.Add(strX);
                        string strY = "相机" + (i + 1).ToString() + "位置" + (j + 1).ToString() + "Y";
                        str_List.Add(strY);
                        string strZ = "相机" + (i + 1).ToString() + "位置" + (j + 1).ToString() + "Z";
                        str_List.Add(strZ);
                        string strR = "相机" + (i + 1).ToString() + "位置" + (j + 1).ToString() + "R";
                        str_List.Add(strR);
                    }
                }
                return str_List;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("RegConfigPar", ex);
                return null;
            }
        }
    }
}
