using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HalconDotNet;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Common;
using DealFile;
using System.ComponentModel;
using Microsoft.Win32;
using BasicClass;
using ControlLib;
using System.Diagnostics;

namespace Camera
{
    partial class BaseUCDisplayCamera
    {
        #region 定义
        #region Path
        /// <summary>
        /// 保存图片路径
        /// </summary>
        public string PathSaveImage//保存图像路径
        {
            get
            {
                return g_BaseParCamera.PathSaveImage;
            }
        }

        /// <summary>
        /// 保存OK图片默认路径
        /// </summary>
        public string PathOKImage//保存图像路径
        {
            get
            {
                return g_BaseParCamera.PathOKImage;
            }
        }

        /// <summary>
        /// 保存NG图片默认路径
        /// </summary>
        public string PathNGImage//保存图像路径
        {
            get
            {
                return g_BaseParCamera.PathNGImage;
            }
        }
        #endregion Path
        #endregion 定义

        /// <summary>
        /// 抓取新的图像进行保存
        /// </summary>
        public bool GrabAndSaveImage(string strPath)
        {
            try
            {               
                ImageAll imageAll = GrabImageAll();

                //保存图像
                if (imageAll == null)
                {
                    return false;
                }
                if (SaveHoImage(imageAll.Ho_Image, strPath))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        #region  保存所有图像
        /// <summary>
        /// 默认路径保存
        /// </summary>
        public bool SaveImageDefaultPath(ImageAll imageAll)
        {
            try
            {
                return SaveImage(imageAll, PathSaveImage);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 短名称
        /// </summary>
        /// <param name="imageAll"></param>
        /// <returns></returns>
        public bool SaveImageDefaultPath_Short(ImageAll imageAll)
        {
            try
            {
                return SaveImage_Short(imageAll, PathSaveImage);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }


        /// <summary>
        /// 保存图片到默认路径
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="nameFolder">按照拍照设置的文件夹名称</param>
        /// <returns></returns>
        public bool SaveImageDefaultPath(ImageAll imageAll, string nameFolder)
        {
            try
            {
                string root = PathSaveImage + nameFolder + "\\";
                return SaveImage(imageAll, root);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 短名称
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="nameFolder"></param>
        /// <returns></returns>
        public bool SaveImageDefaultPath_Short(ImageAll imageAll, string nameFolder)
        {
            try
            {
                string root = PathSaveImage + nameFolder + "\\";
                return SaveImage_Short(imageAll, root);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion  保存所有图像

        #region 保存OK图像
        /// <summary>
        ///  保存OK图像到默认路径
        /// </summary>
        /// <param name="imageAll"></param>
        /// <returns></returns>
        public bool SaveOKImageDefaultPath(ImageAll imageAll)
        {
            try
            {
                return SaveImage(imageAll, PathOKImage);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 短名称
        /// </summary>
        /// <param name="imageAll"></param>
        /// <returns></returns>
        public bool SaveOKImageDefaultPath_Short(ImageAll imageAll)
        {
            try
            {
                return SaveImage_Short(imageAll, PathOKImage);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }


        /// <summary>
        /// 保存OK图像到指定路径
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="name"></param>
        /// <param name="nameFolder"></param>
        /// <returns></returns>
        public bool SaveOKImagePath(ImageAll imageAll, string nameFolder)
        {
            try
            {
                string root = PathOKImage + nameFolder + "\\";
                return SaveImage(imageAll, root);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 短名称
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="nameFolder"></param>
        /// <returns></returns>
        public bool SaveOKImagePath_Short(ImageAll imageAll, string nameFolder)
        {
            try
            {
                string root = PathOKImage + nameFolder + "\\";
                return SaveImage_Short(imageAll, root);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 图像名称+时间
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="nameImage"></param>
        /// <param name="nameFolder"></param>
        /// <returns></returns>
        public bool SaveOKImagePath(ImageAll imageAll, string nameImage, string nameFolder)
        {
            try
            {
                string root = PathOKImage + nameFolder + "\\";
                return SaveImage(imageAll, nameImage, root);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        /// <summary>
        /// 短名称
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="nameImage"></param>
        /// <param name="nameFolder"></param>
        /// <returns></returns>
        public bool SaveOKImagePath_Short(ImageAll imageAll, string nameImage, string nameFolder)
        {
            try
            {
                string root = PathOKImage + nameFolder + "\\";
                return SaveImage(imageAll, nameImage, root);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 保存OK图像

        #region 保存NG图像
        /// <summary>
        ///  保存NG图像
        /// </summary>
        /// <param name="imageAll"></param>
        /// <returns></returns>
        public bool SaveNGImageDefaultPath(ImageAll imageAll)
        {
            try
            {
                return SaveImage(imageAll, PathNGImage);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 短名称
        /// </summary>
        /// <param name="imageAll"></param>
        /// <returns></returns>
        public bool SaveNGImageDefaultPath_Short(ImageAll imageAll)
        {
            try
            {
                return SaveImage_Short(imageAll, PathNGImage);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 保存NG图像到默认路径
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="nameFolder"></param>
        /// <returns></returns>
        public bool SaveNGImagePath(ImageAll imageAll, string nameFolder)
        {
            try
            {
                string root = PathNGImage + nameFolder + "\\";
                return SaveImage(imageAll, root);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 短名称
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="nameFolder"></param>
        /// <returns></returns>
        public bool SaveNGImagePath_Short(ImageAll imageAll, string nameFolder)
        {
            try
            {
                string root = PathNGImage + nameFolder + "\\";
                return SaveImage_Short(imageAll, root);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 名称+时间
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="nameFolder"></param>
        /// <returns></returns>
        public bool SaveNGImagePath(ImageAll imageAll, string nameImage, string nameFolder)
        {
            try
            {
                string root = PathNGImage + nameFolder + "\\";
                return SaveImage(imageAll, nameImage, root);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 短名称
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="nameImage"></param>
        /// <param name="nameFolder"></param>
        /// <returns></returns>
        public bool SaveNGImagePath_Short(ImageAll imageAll, string nameImage, string nameFolder)
        {
            try
            {
                string root = PathNGImage + nameFolder + "\\";
                return SaveImage_Short(imageAll, nameImage, root);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 保存NG图像

        #region 基础函数
        /// <summary>
        /// 保存图像
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public override bool SaveImage(ImageAll imageAll, string strPath)
        {
            try
            {
                if (!BlLocalImage
                     || (BlPIForce && imageAll.Name == "PI"))//本地图像不保存图像,或者强制截屏不保存
                {
                    // 判断是否存在目录夹
                    if (Directory.Exists(strPath) == false)
                    {
                        //创建文件夹
                        Directory.CreateDirectory(strPath);
                    }

                    //获取时间路径
                    string pathFile = Log.CreateHourFile(strPath) + imageAll.Time + "." + TypeSaveImage;
                    string type = TypeSaveImage;
              
                    if (imageAll.Name == "PI")//屏幕截图
                    {
                        pathFile = Log.CreateHourFile(strPath) + imageAll.Time + "_" + imageAll.Name + "." + "jpg";
                        type = "jpeg";
                    }
                    else
                    {
                        pathFile = Log.CreateHourFile(strPath) + imageAll.Time + "_" + imageAll.Name + "." + TypeSaveImage;
                    }

                    //保存图像
                    if (imageAll == null)
                    {
                        return false;
                    }
                    if (SaveHoImage(imageAll.Ho_Image, type, pathFile))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                if (imageAll != null)
                {
                    if (imageAll.Name == "PI")//屏幕截图
                    {
                        imageAll.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 短名称
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public bool SaveImage_Short(ImageAll imageAll, string strPath)
        {
            try
            {
                if (!BlLocalImage
                     || (BlPIForce && imageAll.Name == "PI"))//本地图像不保存图像,或者强制截屏不保存
                {
                    // 判断是否存在目录夹
                    if (Directory.Exists(strPath) == false)
                    {
                        //创建文件夹
                        Directory.CreateDirectory(strPath);
                    }

                    //获取时间路径
                    string pathFile = Log.CreateHourFile(strPath) + imageAll.TimeShort + "." + TypeSaveImage;
                    string type = TypeSaveImage;
                    if (imageAll.Name == "PI")//屏幕截图
                    {
                        pathFile = Log.CreateHourFile(strPath) + imageAll.TimeShort + "_" + imageAll.Name + "." + "jpg";
                        type = "jpeg";
                    }
                    else
                    {
                        pathFile = Log.CreateHourFile(strPath) + imageAll.TimeShort + "_" + imageAll.Name + "." + TypeSaveImage;
                    }

                    //保存图像
                    if (imageAll == null)
                    {
                        return false;
                    }
                    if (SaveHoImage(imageAll.Ho_Image, type, pathFile))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                if (imageAll != null)
                {
                    if (imageAll.Name == "PI")//屏幕截图
                    {
                        imageAll.Dispose();
                    }
                }
            }
        }


        /// <summary>
        /// 图像，加上名称+时间
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="nameImage"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public bool SaveImage(ImageAll imageAll, string nameImage, string strPath)
        {
            try
            {
                if (!BlLocalImage
                    || (BlPIForce && imageAll.Name == "PI"))//本地图像不保存图像,或者强制截屏不保存
                {
                    // 判断是否存在目录夹
                    if (Directory.Exists(strPath) == false)
                    {
                        //创建文件夹
                        Directory.CreateDirectory(strPath);
                    }

                    //获取时间路径
                    //strPath = Log.CreateHourFile(strPath) + g_PathDirectory.GetTimeName() + "." + g_BaseParCamera.TypeSaveImage;
                    string pathFile = Log.CreateHourFile(strPath) + nameImage + imageAll.Time + "." + TypeSaveImage;
                    string type = TypeSaveImage;
                    if (imageAll.Name == "PI")//屏幕截图
                    {
                        pathFile = Log.CreateHourFile(strPath) + nameImage + imageAll.Time + "_" + imageAll.Name + "." + "jpg";
                        type = "jpeg";
                    }
                    else
                    {
                        pathFile = Log.CreateHourFile(strPath) + nameImage + imageAll.Time + "_" + imageAll.Name + "." + TypeSaveImage;
                    }

                    //保存图像
                    if (imageAll == null)
                    {
                        return false;
                    }
                    if (SaveHoImage(imageAll.Ho_Image, type, pathFile))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                if (imageAll != null)
                {
                    if (imageAll.Name == "PI")//屏幕截图
                    {
                        imageAll.Dispose();
                    }
                }
            }
        }


        /// <summary>
        /// 短名称
        /// </summary>
        /// <param name="imageAll"></param>
        /// <param name="nameImage"></param>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public bool SaveImage_Short(ImageAll imageAll, string nameImage, string strPath)
        {
            try
            {
                if (BlLocalImage)//本地图像不保存图像
                {
                    return true;
                }
                // 判断是否存在目录夹
                if (Directory.Exists(strPath) == false)
                {
                    //创建文件夹
                    Directory.CreateDirectory(strPath);
                }

                //获取时间路径
                string pathFile = Log.CreateHourFile(strPath) + nameImage + imageAll.TimeShort + "." + TypeSaveImage;
                string type = TypeSaveImage;
                if (imageAll.Name == "PI")//屏幕截图
                {
                    pathFile = Log.CreateHourFile(strPath) + nameImage + imageAll.TimeShort + "_" + imageAll.Name + "." + "jpg";
                    type = "jpeg";
                }
                else
                {
                    pathFile = Log.CreateHourFile(strPath) + nameImage + imageAll.TimeShort + "_" + imageAll.Name + "." + TypeSaveImage;
                }

                //保存图像
                if (imageAll == null)
                {
                    return false;
                }
                if (SaveHoImage(imageAll.Ho_Image, type, pathFile))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                if (imageAll != null)
                {
                    if (imageAll.Name == "PI")//屏幕截图
                    {
                        imageAll.Dispose();
                    }
                }
            }
        }
       

        /// <summary>
        /// 保存Halcon图像，并按照设置的格式保存
        /// </summary>
        public bool SaveHoImage(HObject ho_Image, string path)
        {
            try
            {
                if (ho_Image != null)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    string pathNew = path.Replace(" 100", "").Replace(" 90", "");
                    HOperatorSet.WriteImage(ho_Image, TypeSaveImage, 255, pathNew);
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        #endregion 基础函数
    }
}
