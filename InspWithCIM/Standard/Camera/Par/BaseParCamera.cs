using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using BasicClass;
using System.IO;
using Common;

namespace Camera
{
    [Serializable]
    public class BaseParCamera : BaseClass
    {
        #region 静态类实例

        #endregion 静态类实例

        #region 定义
        #region Path
        /// <summary>
        /// Path,图片的保存路径
        /// </summary>
        public string PathSaveImage//相机图像保存路径
        {
            get
            {
                string pathSaveImage = ComValue.C_I.PathRootLocalImage + StrNoCamera + "\\AllImage\\";
                if (!Directory.Exists(pathSaveImage))
                {
                    Directory.CreateDirectory(pathSaveImage);
                }
                return pathSaveImage;
            }
        }
        /// <summary>
        /// 保存OK图片到默认路径
        /// </summary>
        public string PathOKImage
        {
            get
            {
                string pathOKImage = ComValue.C_I.PathRootLocalImage + StrNoCamera + "\\OKImage\\";
                if (!Directory.Exists(pathOKImage))
                {
                    Directory.CreateDirectory(pathOKImage);
                }
                return pathOKImage;
            }
        }

        /// <summary>
        /// 保存NG图片到默认路径
        /// </summary>
        public string PathNGImage
        {
            get
            {
                string pathNGImage = ComValue.C_I.PathRootLocalImage + StrNoCamera + "\\NGImage\\";
                if (!Directory.Exists(pathNGImage))
                {
                    Directory.CreateDirectory(pathNGImage);
                }
                return pathNGImage;
            }
        }

        static string C_PathParCamera = ParPathRoot.PathRoot + "Store\\Camera\\ParCamera.ini";   //相机参数路径
        #endregion Path

        //int         
        public int NoCamera { get; set; }//相机序号   

        //string 
        public string Serial_Camera = "";//相机序列号       
        public string StrNoCamera
        {
            get
            {
                return "Camera" + NoCamera.ToString();
            }
        }

        //string typeSaveImage = "jpg 100";
        //public string TypeSaveImage //本地图像保存类型
        //{
        //    get
        //    {
        //        if (!typeSaveImage.Contains("jpg")
        //            && !typeSaveImage.Contains("bmp"))
        //        {
        //            return "jpg 100";
        //        }
        //        return typeSaveImage;
        //    }
        //    set
        //    {
        //        typeSaveImage = value;
        //    }
        //}


        //bool
        public bool BlUsingTrigger { get; set; }//是否使用软触发
        public bool BlNameCamera = false;//是否使用相机名称

        //enum
        public TypeCamera_enum TypeCamera_e = TypeCamera_enum.PGSDK;
        public TriggerSourceCamera_enum TriggerSource_e = TriggerSourceCamera_enum.Software;

        #endregion 定义

        #region 读写参数
        /// <summary>
        /// 从Ini中读取参数
        /// </summary>
        public void ReadIni()
        {
            try
            {
                //相机类型
                string typeCamera = IniFile.I_I.ReadIniStr(StrNoCamera, "TypeCamera", "HIKSDK", C_PathParCamera);
                if (typeCamera == "BaslerSDK")
                {
                    typeCamera = "BSLSDK";
                }
                TypeCamera_e = (TypeCamera_enum)Enum.Parse(typeof(TypeCamera_enum), typeCamera);

                //序列号
                Serial_Camera = IniFile.I_I.ReadIniStr(StrNoCamera, "SerialNumber", C_PathParCamera);
                //是否相机名称
                BlNameCamera = IniFile.I_I.ReadIniBl(StrNoCamera, "BlNameCamera", C_PathParCamera);

                //是否使用软触发
                BlUsingTrigger = IniFile.I_I.ReadIniBl(StrNoCamera, "BlUsingSoftTrriger", C_PathParCamera);

                //图片保存类型
                //TypeSaveImage = IniFile.I_I.ReadIniStr(StrNoCamera, "TypeSaveImage", C_PathParCamera);

                string triggerSource = IniFile.I_I.ReadIniStr(StrNoCamera, "TriggerSource", "Software", C_PathParCamera);
                TriggerSource_e = (TriggerSourceCamera_enum)Enum.Parse(typeof(TriggerSourceCamera_enum), triggerSource);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 写入参数
        /// </summary>
        /// <returns></returns>
        public bool WriteIni()
        {
            try
            {
                IniFile.I_I.WriteIni(StrNoCamera, "TypeCamera", TypeCamera_e.ToString(), C_PathParCamera);//相机类型
                IniFile.I_I.WriteIni(StrNoCamera, "SerialNumber", Serial_Camera.ToString(), C_PathParCamera);//序列号
                IniFile.I_I.WriteIni(StrNoCamera, "BlUsingSoftTrriger", BlUsingTrigger.ToString(), C_PathParCamera);//是否使用软触发
                IniFile.I_I.WriteIni(StrNoCamera, "BlNameCamera", BlNameCamera.ToString(), C_PathParCamera);//是否相机名称

                //IniFile.I_I.WriteIni(StrNoCamera, "TypeSaveImage", TypeSaveImage.ToString(), C_PathParCamera);//保存本地图片类型
                IniFile.I_I.WriteIni(StrNoCamera, "TriggerSource", TriggerSource_e.ToString(), C_PathParCamera);//保存本地图片类型
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 读写参数
    }

    //相机类型
    public enum TypeCamera_enum
    {
        PGSDK,
        //BaslerSDK,
        BSLSDK,
        HIKSDK,
    }

    /// <summary>
    /// 触发源
    /// </summary>
    public enum TriggerSourceCamera_enum
    {
        Line0 = 0,
        Line1 = 1,
        Line2 = 2,
        Line3 = 3,
        Counter = 4,
        Software = 7,
    }
}
