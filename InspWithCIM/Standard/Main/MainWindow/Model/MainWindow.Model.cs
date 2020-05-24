using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Threading.Tasks;
using DealPLC;
using Common;
using DealRobot;
using System.IO;
using DealFile;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using DealComprehensive;
using SetPar;
using ParComprehensive;
using BasicClass;
using DealConfigFile;
using DealLog;

namespace Main
{
    partial class MainWindow
    {
        #region 定义
        //int 
        int NumError = 0;

        //bool
        bool g_BlFinishBasePar = false;

        bool g_BlFinishComprehensive1 = false;
        bool g_BlFinishComprehensive2 = false;
        bool g_BlFinishComprehensive3 = false;
        bool g_BlFinishComprehensive4 = false;
        bool g_BlFinishComprehensive5 = false;
        bool g_BlFinishComprehensive6 = false;

        bool g_BlFinishComprehensive = false;

        bool g_BlModelSame = false;//型号相同
        #endregion 定义

        #region 新建的型号
        /// <summary>
        /// 处理新建型号
        /// </summary>
        /// <returns></returns>
        bool NewModel()
        {
            try
            {
                int numError = 0;
                ShowState("正在新建文件");
                //删除旧配置文件文件
                if (File.Exists(ComConfigPar.C_I.PathConfigIni))
                {
                    File.Delete(ComConfigPar.C_I.PathConfigIni);
                }

                //保存当前配置文件路径
                if (ParManageConfigPar.P_I.WriteIniPathConfigPar())
                {
                    ShowState("新文件路径更改成功");
                }

                //存储配置文件
                if (ParConfigPar.P_I.WriteConfigIni())
                {
                    ShowState("产品参数新建成功");
                }

                //拷贝巡边检参数
                CopyInspIni();

                CopyXmlComprehensive();
                ////拷贝图像处理参数文件
                //if (!CopyXmlComprehensive())
                //{
                //    numError++;
                //}

                //ShowState("读取相机配置文件");
                //ChangeXmlComprehensive();

                //拷贝补偿值文件
                CopyAdjustIni();

                ShowState("程序应用参数新建成功");

                //处理型号文件其他相关的文件
                DealOthersModel();

                //显示型号
                ShowParProduct();

                if (numError == 0)
                {
                    ShowState("新建型号成功");
                }
                else
                {
                    ShowAlarm("新建型号失败");
                    MessageBox.Show("新建型号失败");
                }

                //每次换型时，需要写入PLC的值
                WritePLCModelPar();
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
                return false;
            }
        }

        void CopyInspIni()
        {
            try
            {
                if (!File.Exists(ParCam1.P_I[0].BasePathIni))
                {
                    File.Copy(ComConfigPar.C_I.PathOldConfigIni.Replace("Product.ini", "ParInsp.ini"), ParCam1.P_I[0].BasePathIni);
                }

                ParCam1.P_I.ReadIni();
                if (ParCameraWork.NumCamera > 1)
                {
                    ParCam2.P_I.ReadIni();
                }
                if (ParCameraWork.NumCamera > 2)
                {
                    ParCam3.P_I.ReadIni();
                }
                if (ParCameraWork.NumCamera > 3)
                {
                    ParCam4.P_I.ReadIni();
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 更新产品数据
        /// </summary>
        bool RefreshPar()
        {
            try
            {
                //删除旧配置文件文件
                if (File.Exists(ComConfigPar.C_I.PathConfigIni))
                {
                    File.Delete(ComConfigPar.C_I.PathConfigIni);
                }

                //存储配置文件
                if (ParConfigPar.P_I.WriteConfigIni())
                {
                    ShowState("产品参数更新成功");
                }

                //处理型号文件其他相关的文件
                DealOthersModel();
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
                return false;
            }
        }

        //处理图像处理参数文件
        bool CopyXmlComprehensive()
        {
            try
            {
                int numError = 0;
                if (g_BlModelSame)//如果新建的型号相同
                {
                    return true;
                }
                //相机1
                if (!ParComprehensive1.P_I.CopyDealImageP())
                {
                    numError++;
                }
                //相机2 
                if (ParCameraWork.NumCamera > 1)
                {
                    if (!ParComprehensive2.P_I.CopyDealImageP())
                    {
                        numError++;
                    }
                }
                //相机3 
                if (ParCameraWork.NumCamera > 2)
                {
                    if (!ParComprehensive3.P_I.CopyDealImageP())
                    {
                        numError++;
                    }
                }
                //相机4  
                if (ParCameraWork.NumCamera > 3)
                {
                    ParComprehensive4.P_I.CopyDealImageP();
                }
                //相机5  
                if (ParCameraWork.NumCamera > 4)
                {
                    if (!ParComprehensive5.P_I.CopyDealImageP())
                    {
                        numError++;
                    }
                }

                //相机6 
                if (ParCameraWork.NumCamera > 5)
                {
                    if (!ParComprehensive6.P_I.CopyDealImageP())
                    {
                        numError++;
                    }
                }
                if (numError > 0)
                {
                    ShowAlarm("拷贝相机综合设置文件失败！");
                    return false;
                }
                ShowState("相机综合设置参数新建成功");
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
                return false;
            }
        }
        //处理补偿值文件
        void CopyAdjustIni()
        {
            try
            {
                //如果存在则读取           
                if (File.Exists(ParAdjust.PathAdjust))
                {

                }
                else//不存在则拷贝
                {
                    if (File.Exists(ParAdjust.PathOldAdjust))
                    {
                        File.Copy(ParAdjust.PathOldAdjust, ParAdjust.PathAdjust);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }


        /// <summary>
        /// 打开保存对话框
        /// </summary>
        /// <returns></returns>
        bool OpenSaveFileDialog()
        {
            try
            {
                Microsoft.Win32.SaveFileDialog sfdSaveNew = new Microsoft.Win32.SaveFileDialog();
                sfdSaveNew.Filter = "INI files (*.ini)|*.ini";
                string strPath = ParPathRoot.PathRoot + "Store\\" + "ParProduct\\";   //获取当前可执行文件的路径
                sfdSaveNew.ValidateNames = true;//只接受有效的文件名称

                // 判断是否存在目录夹
                if (Directory.Exists(strPath) == false)
                {
                    Directory.CreateDirectory(strPath);
                }

                sfdSaveNew.InitialDirectory = strPath;
                if (!(bool)sfdSaveNew.ShowDialog())
                {
                    return false;
                }
                ComConfigPar.C_I.NameModel = sfdSaveNew.SafeFileName;//新的配置文件名称
                string newPathFile = sfdSaveNew.FileName;//新的保存路径
                if (!newPathFile.Contains(".ini"))//确保保存的后缀名是ini
                {
                    newPathFile += ".ini";
                }

                //设置系统路径
                ComConfigPar.C_I.PathOldConfigIni = ComConfigPar.C_I.PathConfigIni;//记录旧型号路径
                ComConfigPar.C_I.PathConfigIni = newPathFile;//新型号 
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinConfigPar", ex);
                return false;
            }
        }
        #endregion 新建的型号

        #region 删除文件
        /// <summary>
        /// 删除配置文件
        /// </summary>
        /// <returns></returns>
        bool DelModel(string name)
        {
            try
            {
                if (name == ComConfigPar.C_I.NameModel)
                {
                    ShowAlarm("禁止删除正在使用的配置文件!");
                    MessageBox.Show("禁止删除当前正在使用的配置文件!");
                    return false;
                }
                string path = ParPathRoot.PathRoot + "Store\\产品参数\\" + name;
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                ShowState("删除文件成功");
                MessageBox.Show("删除成功");

                //备份产品参数
                FunBackup.F_I.BackupProduct(path, "删除参数");
                return true;
            }
            catch (Exception ex)
            {
                ShowAlarm("删除文件失败");
                Log.L_I.WriteError("MainWindow", ex);
                return false;
            }
            finally
            {
                //按钮日志
                FunLogButton.P_I.AddInfo("DelModel删除配置文件",
                "外部触发删除配置文件,Main窗体");
            }
        }
        #endregion 删除文件

        #region 换型文件
        /// <summary>
        /// 进行换型后所有参数的替换
        /// </summary>
        /// <returns></returns>
        void ChangeModel()
        {
            try
            {
                g_BlFinishBasePar = false;

                ShowState("正在换型,请稍等......");

                //将配置文件路径写入文件
                if (!ParManageConfigPar.P_I.WriteIniPathConfigPar())
                {
                    NumError++;
                    ShowAlarm("新配置文件路径更改失败");
                }
                else
                {
                    ShowState("新配置文件路径更改成功");
                }
                //读取配置参数 
                if (!ParConfigPar.P_I.ReadIniConfigPar())
                {
                    NumError++;
                    ShowAlarm("读取新的产品参数失败");
                }
                else
                {
                    ShowState("读取新的产品参数");
                }

                if (!ParDelFolder.P_I.ReadIniPar())//删除文件设定
                {
                    NumError++;
                    ShowAlarm("读取文件监控参数失败");
                }
                else
                {
                    ShowState("读取文件监控参数成功");
                }
                ShowState("正在读取相机参数文件");
                //读取综合处理文件
                ChangeXmlComprehensive();

                //拷贝巡边检参数
                CopyInspIni();

                //处理型号其他相关
                DealOthersModel();

                //显示型号
                ShowParProduct();

                //每次换型时，需要写入PLC的值
                WritePLCModelPar();
            }
            catch (Exception ex)
            {
                ShowAlarm("换型失败");
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_BlFinishBasePar = true;
                ShowResultChangeModel();//显示换型结果
            }
        }

        #region 读取相机配置文件
        /// <summary>
        /// 读取相机综合配置文件,并加载模板
        /// </summary>
        void ChangeXmlComprehensive()
        {
            try
            {

                g_BlFinishComprehensive = false;
                g_BlFinishComprehensive1 = false;
                g_BlFinishComprehensive2 = false;
                g_BlFinishComprehensive3 = false;
                g_BlFinishComprehensive4 = false;
                g_BlFinishComprehensive5 = false;
                g_BlFinishComprehensive6 = false;

                TaskFactory taskFactory = new TaskFactory();
                Task[] task = new Task[]
                {
                    taskFactory.StartNew(ChangeXmlComprehensive1),
                    taskFactory.StartNew(ChangeXmlComprehensive2),
                    taskFactory.StartNew(ChangeXmlComprehensive3),
                    taskFactory.StartNew(ChangeXmlComprehensive4),
                    taskFactory.StartNew(ChangeXmlComprehensive5),
                    taskFactory.StartNew(ChangeXmlComprehensive6),
                };
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        /// <summary>
        /// 相机1
        /// </summary>
        void ChangeXmlComprehensive1()
        {
            try
            {
                g_BlFinishComprehensive1 = false;
                if (!ParComprehensive1.P_I.ReadXmlXDoc())
                {
                    NumError++;
                    ShowAlarm("相机1参数读取失败");
                }
                else
                {
                    ShowState("相机1参数读取成功");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_BlFinishComprehensive1 = true;
                FinishComprehensive();
            }
        }
        /// <summary>
        /// 相机2
        /// </summary>
        void ChangeXmlComprehensive2()
        {
            try
            {
                g_BlFinishComprehensive2 = false;
                if (ParCameraWork.NumCamera < 2)
                {
                    return;
                }
                if (!ParComprehensive2.P_I.ReadXmlXDoc())
                {
                    NumError++;
                    ShowAlarm("相机2参数读取失败");
                }
                else
                {
                    ShowState("相机2参数读取成功");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_BlFinishComprehensive2 = true;
                FinishComprehensive();
            }
        }
        /// <summary>
        /// 相机3
        /// </summary>
        void ChangeXmlComprehensive3()
        {
            try
            {
                g_BlFinishComprehensive3 = false;
                if (ParCameraWork.NumCamera < 3)
                {
                    return;
                }
                if (!ParComprehensive3.P_I.ReadXmlXDoc())
                {
                    NumError++;
                    ShowAlarm("相机3参数读取失败");
                }
                else
                {
                    ShowState("相机3参数读取成功");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_BlFinishComprehensive3 = true;
                FinishComprehensive();
            }
        }
        /// <summary>
        /// 相机4
        /// </summary>
        void ChangeXmlComprehensive4()
        {
            try
            {
                g_BlFinishComprehensive4 = false;
                if (ParCameraWork.NumCamera < 4)
                {
                    return;
                }
                if (!ParComprehensive4.P_I.ReadXmlXDoc())
                {
                    NumError++;
                    ShowAlarm("相机4参数读取失败");
                }
                else
                {
                    ShowState("相机4参数读取成功");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_BlFinishComprehensive4 = true;
                FinishComprehensive();
            }
        }
        /// <summary>
        /// 相机5
        /// </summary>
        void ChangeXmlComprehensive5()
        {
            try
            {
                g_BlFinishComprehensive5 = false;
                if (ParCameraWork.NumCamera < 5)
                {
                    return;
                }
                if (!ParComprehensive1.P_I.ReadXmlXDoc())
                {
                    NumError++;
                    ShowAlarm("相机5参数读取失败");
                }
                else
                {
                    ShowState("相机5参数读取成功");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_BlFinishComprehensive5 = true;
                FinishComprehensive();
            }
        }
        /// <summary>
        /// 相机6
        /// </summary>
        void ChangeXmlComprehensive6()
        {
            try
            {
                g_BlFinishComprehensive6 = false;
                if (ParCameraWork.NumCamera < 6)
                {
                    return;
                }
                if (!ParComprehensive6.P_I.ReadXmlXDoc())
                {
                    NumError++;
                    ShowAlarm("相机6参数读取失败");
                }
                else
                {
                    ShowState("相机6参数读取成功");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_BlFinishComprehensive6 = true;
                FinishComprehensive();
            }
        }

        /// <summary>
        /// 判断是否所有的换型都结束
        /// </summary>
        void FinishComprehensive()
        {
            try
            {
                if (g_BlFinishComprehensive1
                    && g_BlFinishComprehensive2
                    && g_BlFinishComprehensive3
                    && g_BlFinishComprehensive4
                    && g_BlFinishComprehensive5
                    && g_BlFinishComprehensive6)
                {
                    g_BlFinishComprehensive = true;
                    //加载模板
                    CreateTemp(false);
                    //初始化校准
                    InitCalib();
                    ShowResultChangeModel();
                }
                else
                {
                    g_BlFinishComprehensive = false;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 读取相机配置文件
        /// <summary>
        /// 打开文件对话框
        /// </summary>
        /// <returns></returns>
        bool OpenFileDialog()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

                openFileDialog.FileName = "";
                openFileDialog.Filter = "INI files (*.ini)|*.ini";
                string root = ParPathRoot.PathRoot + "Store\\产品参数\\";
                openFileDialog.InitialDirectory = root;

                if (!(bool)openFileDialog.ShowDialog())
                {
                    return false;
                }
                ComConfigPar.C_I.PathConfigIni = openFileDialog.FileName;
                ComConfigPar.C_I.NameModel = openFileDialog.FileName.Replace(root, "").Replace("Product.ini", "").Replace("\\", "");
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
                return false;
            }
        }
        #endregion 换型文件

        #region 换型后通信相关
        /// <summary>
        /// 处理换型后的同学相关
        /// </summary>
        void DealOthersModel()
        {
            try
            {
                //机器人换型
                if (ParSetRobot.P_I.TypeRobot_e != TypeRobot_enum.Null)
                {
                    ConfigRobot_Task();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 换型后通信相关

        /// <summary>
        /// 显示换型结果
        /// </summary>
        void ShowResultChangeModel()
        {
            try
            {
                if (g_BlFinishBasePar
                    && g_BlFinishComprehensive)
                {
                    if (NumError > 0)
                    {
                        ShowAlarm("换型失败");
                        MessageBox.Show("换型失败!");
                    }
                    else
                    {
                        ShowState("换型成功");
                        MessageBox.Show("换型成功!");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
    }
}
