using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Common;
using DealFile;
using BasicClass;
using System.IO;

namespace DealConfigFile
{
    public class ParManageConfigPar
    {
        #region 静态类实例
        public static ParManageConfigPar P_I = new ParManageConfigPar();
        #endregion 静态类实例

        #region 定义
        #region Path
        //保存了当前配置文件路径
        public string PathConfigPar
        {
            get
            {
                string root = ParPathRoot.PathRoot+"Store\\ConfigFile\\";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                return root + "PathConfigPar.ini";
            }
        }
        #endregion Path

        //List
        public List<FileConfigPar> FileConfigPar_L = new List<FileConfigPar>();//文件路径
        #endregion 定义

        /// <summary>
        /// 读取当前配置参数路径
        /// </summary>
        /// <returns></returns>
        public bool ReadIniPathConfigPar()
        {
            try
            {
                if (!File.Exists(PathConfigPar))
                {
                    string root = ParPathRoot.PathRoot + "Store\\产品参数\\default\\";
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    string path = root + "Product.ini";
                    IniFile.I_I.WriteIni("ConfigParIni", "Path", "default", PathConfigPar);
                    ComConfigPar.C_I.PathConfigIni = path;
                }
                else
                {
                    string model = IniFile.I_I.ReadIniStr("ConfigParIni", "Path", PathConfigPar);
                    if (model.Trim() != "")
                    {
                        ComConfigPar.C_I.PathConfigIni = ParPathRoot.PathRoot + "Store\\产品参数\\" + model + "\\Product.ini";
                    }
                    else
                    {
                        ComConfigPar.C_I.PathConfigIni = ParPathRoot.PathRoot + "Store\\产品参数\\default\\Product.ini";
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParConfigPar", ex);
                return false;
            }
        }


        #region 读取文件列表
        /// <summary>
        /// 读取配置文件列表
        /// </summary>
        /// <returns></returns>
        public bool ReadFileConfigList()
        {
            try
            {
                FileConfigPar_L.Clear();

                DirectoryInfo pathRoot = new DirectoryInfo(ParPathRoot.PathRoot+"Store\\产品参数\\");
                int i = 0;
                foreach (DirectoryInfo files in pathRoot.GetDirectories())
                {
                    i++;
                    FileConfigPar fileConfigPar = new FileConfigPar();
                    fileConfigPar.No = i;
                    fileConfigPar.Model = files.Name;
                    fileConfigPar.PathPar = files.FullName + "\\Product.ini";
                    FileConfigPar_L.Add(fileConfigPar);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParConfigPar", ex);
                return false;
            }
        }
        #endregion 读取文件列表

        #region 保存产品参数路径
        /// <summary>
        /// 将当前配置文件路径进行保存
        /// </summary>
        /// <returns></returns>
        public bool WriteIniPathConfigPar()
        {
            try
            {
                IniFile.I_I.WriteIni("ConfigParIni", "Path", ComConfigPar.C_I.NameModel, PathConfigPar);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParManageConfigPar", ex);
                return false;
            }
        }
        #endregion 保存产品参数路径

    }
}
