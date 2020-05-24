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
using System.Threading;
using System.Threading.Tasks;
using BasicClass;
using Common;
using DealFile;
using System.IO;
using System.Xml;
using System.Reflection;

namespace DealLog
{
    public class FunBackup:BaseClass
    {
        #region 静态类实例
        public static FunBackup F_I = new FunBackup();
        #endregion 静态类实例

        #region 定义
        /// <summary>
        /// 备份根目录
        /// </summary>
        public string RootBackup
        {
            get
            {
                string path = ParPathRoot.PathRoot + "软件运行记录\\数据备份\\";               
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }
        public string PathBackup
        {
            get
            {
                string path = Log.CreateDayFile(RootBackup);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 当前时间
        /// </summary>
        string TimeNow
        {
            get
            {
                //获取当前日期
                DateTime dt = DateTime.Now;
                //用时间命名               
                string hour = dt.Hour.ToString().PadLeft(2, '0');
                string minite = dt.Minute.ToString().PadLeft(2, '0');
                string second = dt.Millisecond.ToString().PadLeft(2, '0');
                string strPath = hour + "-" + minite + "-" + second + "\\";
                return strPath;
            }
        }

        /// <summary>
        /// 产品参数
        /// </summary>
        public string PathProduct
        {
            get
            {
                string root = PathBackup + "Product\\";
                string path = root + TimeNow;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 相机参数
        /// </summary>
        public string PathCamera
        {
            get
            {
                string root = PathBackup + "Camera\\";
                string path = root + TimeNow;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 系统参数
        /// </summary>
        public string PathParSysWork
        {
            get
            {
                string root = PathBackup + "ParSysWork\\";
                string path = root + TimeNow;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// 相机综合设置
        /// </summary>
        public string NoCamera { get; set; }

        public string PathComprehensive
        {
            get
            {
                string root = PathBackup + "相机综合设置" + NoCamera + "\\";
                string path = root + TimeNow;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// PLC
        /// </summary>
        public string PathPLC
        {
            get
            {
                string root = PathBackup + "PLC\\";
                string path = root + TimeNow;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// Robot
        /// </summary>
        public string PathRobot
        {
            get
            {
                string root = PathBackup + "Robot\\";
                string path = root + TimeNow;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// Adjust
        /// </summary>
        public string PathAdjust
        {
            get
            {
                string root = PathBackup + "Adjust\\";
                string path = root + TimeNow;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// Std
        /// </summary>
        public string PathStd
        {
            get
            {
                string root = PathBackup + "Std\\";
                string path = root + TimeNow;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        /// <summary>
        /// SetPar
        /// </summary>
        public string PathSetPar
        {
            get
            {
                string root = PathBackup + "SetPar\\";
                string path = root + TimeNow;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }      
        #endregion 定义

        #region 初始化
        public FunBackup()
        {
            NameClass = "FunBackup";
        }
        #endregion 初始化

        /// <summary>
        /// 备份产品参数
        /// </summary>
        public void BackupProduct()
        {
            try
            {
                string path = PathProduct;
                //读取产品参数
                DirectoryInfo theFolder = new DirectoryInfo(ComConfigPar.C_I.PathRoot);
                int num = theFolder.GetFiles().Length;
                if (num == 0)
                {
                    return;
                }
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    File.Copy(item.FullName, path + fileOld);
                }
                IniFile.I_I.WriteIni("Explain", "Model", ComConfigPar.C_I.NameModel, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Source", ComConfigPar.C_I.PathRoot.Replace(ParPathRoot.PathRoot, ""), path + "备份说明.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份产品参数数据失败!");
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 备份产品参数
        /// </summary>
        public void BackupProduct(string pathOld)
        {
            try
            {
                string path = PathProduct;
                //读取产品参数
                DirectoryInfo theFolder = new DirectoryInfo(pathOld);
                int num = theFolder.GetFiles().Length;
                if (num == 0)
                {
                    return;
                }
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    File.Copy(item.FullName, path + fileOld);
                }
                IniFile.I_I.WriteIni("Explain", "Model", ComConfigPar.C_I.NameModel, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Source", ComConfigPar.C_I.PathRoot.Replace(ParPathRoot.PathRoot, ""), path + "备份说明.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份产品参数数据失败!");
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        public void BackupProduct(string pathOld, string reason)
        {
            try
            {
                string path = PathProduct;
                //读取产品参数
                DirectoryInfo theFolder = new DirectoryInfo(pathOld);
                int num = theFolder.GetFiles().Length;
                if (num == 0)
                {
                    return;
                }
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    File.Copy(item.FullName, path + fileOld);
                }
                IniFile.I_I.WriteIni("Explain", "Model", ComConfigPar.C_I.NameModel, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Reason", reason, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Source", path.Replace(ParPathRoot.PathRoot, ""), path + "备份说明.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份产品参数数据失败!");
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 备份相机参数
        /// </summary>
        public void BackupCamera()
        {
            try
            {
                string path = PathCamera;
                DirectoryInfo theFolder = new DirectoryInfo(ParPathRoot.PathRoot + "Store\\Camera\\");
                int num=theFolder.GetFiles().Length;
                if (num == 0)
                {
                    return;
                }
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    File.Copy(item.FullName, path + fileOld);
                }
                IniFile.I_I.WriteIni("Explain", "Model", ComConfigPar.C_I.NameModel, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Source", "Store\\Camera\\", path + "备份说明.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份相机数据失败!");
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 备份运行参数
        /// </summary>
        public void BackupParSysWork()
        {
            try
            {
                string path = PathParSysWork;
                DirectoryInfo theFolder = new DirectoryInfo(ParPathRoot.PathRoot + "Store\\ParSysWork\\");
                int num = theFolder.GetFiles().Length;
                if (num == 0)
                {
                    return;
                }
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    File.Copy(item.FullName, path + fileOld);
                }
                IniFile.I_I.WriteIni("Explain", "Model", ComConfigPar.C_I.NameModel, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Source", "Store\\ParSysWork\\", path + "备份说明.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份工作方式数据失败!");
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 备份综合参数
        /// </summary>
        /// <param name="pathOld"></param>
        /// <param name="noCamera"></param>
        public void BackupComprehensive(string pathOld, int noCamera)
        {
            try
            {
                NoCamera = noCamera.ToString();//相机序号
                string path = PathComprehensive;                
                DirectoryInfo theFolder = new DirectoryInfo(pathOld);
                int num = theFolder.GetFiles().Length;
                if (num == 0)
                {
                    return;
                }
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    File.Copy(item.FullName, path + fileOld);
                }
                IniFile.I_I.WriteIni("Explain", "Model", ComConfigPar.C_I.NameModel, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Source", pathOld.Replace(ParPathRoot.PathRoot, ""), path + "备份说明.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份相机综合设置数据失败!");
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        public void BackupComprehensive(string pathOld, int noCamera, string reason)
        {
            try
            {
                NoCamera = noCamera.ToString();//相机序号
                string path = PathComprehensive;
                DirectoryInfo theFolder = new DirectoryInfo(pathOld);     
                int num = theFolder.GetFiles().Length;
                if (num == 0)
                {
                    return;
                }
                           
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    File.Copy(item.FullName, path + fileOld);
                }
                IniFile.I_I.WriteIni("Explain", "Model", ComConfigPar.C_I.NameModel, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Reason", reason, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Source", pathOld.Replace(ParPathRoot.PathRoot, ""), path + "备份说明.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份相机综合设置数据失败!");
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        public bool RecoverParcomprehensive(int noCamera, out string path)
        {
            path = "";
            try
            {
                string root = RootBackup + "相机综合设置" + noCamera + "\\" + ComConfigPar.C_I.NameModel + "\\";
                if (!Directory.Exists(root))
                {
                    return false;
                }
                //日期
                DirectoryInfo[] dirDate = new DirectoryInfo(root).GetDirectories();
                List<string> pathDate_L = new List<string>();
                foreach (DirectoryInfo item in dirDate)
                {
                    string fileOld = item.Name;
                    pathDate_L.Add(item.FullName);
                }
                pathDate_L.Sort();

                //时间
                string pathTime = pathDate_L[pathDate_L.Count - 1];
                DirectoryInfo[] dirTime = new DirectoryInfo(pathTime).GetDirectories();
                List<string> pathTime_L = new List<string>();
                foreach (DirectoryInfo itemTime in dirTime)
                {
                    pathTime_L.Add(itemTime.FullName);
                }
                pathTime_L.Sort();//排序，按照时间

                if (pathTime_L.Count < 1)
                {
                    return false;
                }

                FileInfo[] files = new DirectoryInfo(pathTime_L[pathTime_L.Count - 1]).GetFiles();
                int numFile = files.Length;
                string pathProduct = ComConfigPar.C_I.PathRoot + "Camera" + noCamera.ToString() + "\\";
                if (!Directory.Exists(pathProduct))
                {
                    Directory.CreateDirectory(pathProduct);
                }
                for (int t = 0; t < numFile; t++)
                {
                    File.Copy(files[t].FullName, pathProduct + files[t].Name, true);
                }
                path = pathTime_L[pathTime_L.Count - 1];
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

            /// <summary>
            /// 备份PLC参数
            /// </summary>
            public void BackupPLC()
        {
            try
            {
                string path = PathPLC;
                DirectoryInfo theFolder = new DirectoryInfo(ParPathRoot.PathRoot + "Store\\PLC\\");
                int num = theFolder.GetFiles().Length;
                if (num == 0)
                {
                    return;
                }
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    File.Copy(item.FullName, path + fileOld);
                }
                IniFile.I_I.WriteIni("Explain", "Model", ComConfigPar.C_I.NameModel, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Source", "Store\\PLC\\", path + "备份说明.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份PLC数据失败!");
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 备份机器人参数
        /// </summary>
        public void BackupRobot()
        {
            try
            {
                string path = PathRobot;
                DirectoryInfo theFolder = new DirectoryInfo(ParPathRoot.PathRoot + "Store\\Robot\\");
                int num = theFolder.GetFiles().Length;
                if (num == 0)
                {
                    return;
                }
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    File.Copy(item.FullName, path + fileOld);
                }
                IniFile.I_I.WriteIni("Explain", "Model", ComConfigPar.C_I.NameModel, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Source", "Store\\Robot\\", path + "备份说明.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份Robot数据失败!");
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 备份调整值
        /// </summary>
        public void BackupAdjust()
        {
            try
            {
                string path = PathAdjust;
                string root = ComConfigPar.C_I.PathConfigIni.Replace("Product.ini", "调整值");
                DirectoryInfo theFolder = new DirectoryInfo(root);
                int num = theFolder.GetFiles().Length;
                if (num == 0)
                {
                    return;
                }
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    File.Copy(item.FullName, path + fileOld);
                }
                IniFile.I_I.WriteIni("Explain", "Source", root.Replace(ParPathRoot.PathRoot, ""), path + "备份说明.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份调整值数据失败!");
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 备份基准值
        /// </summary>
        public void BackupStd()
        {
            try
            {
                string path = PathStd;
                DirectoryInfo theFolder = new DirectoryInfo(ParPathRoot.PathRoot + "Store\\AdjustStd\\");
                int num = theFolder.GetFiles().Length;
                if (num == 0)
                {
                    return;
                }
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    File.Copy(item.FullName, path + fileOld);
                }
                IniFile.I_I.WriteIni("Explain", "Model", ComConfigPar.C_I.NameModel, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Source", "Store\\AdjustStd\\", path + "备份说明.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份基准值数据失败!");
                Log.L_I.WriteError(NameClass, ex);
            }            
        }

        /// <summary>
        /// 备份SetPar参数
        /// </summary>
        public void BackupSetPar()
        {
            try
            {
                string path = PathSetPar;
                DirectoryInfo theFolder = new DirectoryInfo(ParPathRoot.PathRoot + "Store\\SetPar\\");
                int num = theFolder.GetFiles().Length;
                if (num == 0)
                {
                    return;
                }
                foreach (FileInfo item in theFolder.GetFiles())
                {
                    string fileOld = item.Name;
                    File.Copy(item.FullName, path + fileOld);
                }
                IniFile.I_I.WriteIni("Explain", "Model", ComConfigPar.C_I.NameModel, path + "备份说明.ini");
                IniFile.I_I.WriteIni("Explain", "Source", "Store\\SetPar\\", path + "备份说明.ini");
            }
            catch (Exception ex)
            {
                MessageBox.Show("备份SetPar数据失败!");
                Log.L_I.WriteError(NameClass, ex);
            }
           
        }
    }
}
