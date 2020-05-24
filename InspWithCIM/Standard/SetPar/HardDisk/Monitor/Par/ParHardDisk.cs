using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;
using Common;
using DealFile;
using DealLog;

namespace SetPar
{
    public class ParHardDisk:BaseClass
    {
        #region 静态类实例
        public static ParHardDisk P_I = new ParHardDisk();
        #endregion 静态类实例

        #region 定义
        //Path
        public static string c_PathSetHardDisk = ComValue.c_PathSetPar + "SetHardDisk.ini";

        /// <summary>
        /// 驱动盘
        /// </summary>
        string nameDrive = "D:\\";
        public string NameDrive
        {
            get
            {
                return nameDrive;
            }
            set
            {
                nameDrive = value;
            }
        }

        /// <summary>
        /// 报警的阈值
        /// </summary>
        double spaceMin=0;
        public double SpaceMin
        {
            get
            {
                if (spaceMin < 2)
                {
                    return 2;
                }
                return spaceMin;
            }
            set
            {
                spaceMin = value;
            }
        }
        #endregion 定义

        #region 初始化
        public ParHardDisk()
        {
            NameClass = "ParHardDisk";
        }
        #endregion 初始化

        #region 读写参数
        /// <summary>
        /// 读取参数
        /// </summary>
        /// <returns></returns>
        public bool ReadIniPar()
        {
            try
            {
                NameDrive = IniFile.I_I.ReadIniStr("HardDisk", "NameDrive", "D:\\", c_PathSetHardDisk);
                SpaceMin = IniFile.I_I.ReadIniDbl("HardDisk", "SpaceMin", 2.0, c_PathSetHardDisk);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
        /// <summary>
        /// 写入参数
        /// </summary>
        /// <returns></returns>
        public bool WriteIniPar()
        {
            try
            {
                IniFile.I_I.WriteIni("HardDisk", "NameDrive", NameDrive, c_PathSetHardDisk);
                IniFile.I_I.WriteIni("HardDisk", "SpaceMin", SpaceMin.ToString(), c_PathSetHardDisk);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            finally
            {
                //按钮日志
                FunLogButton.P_I.AddInfo("btnSave保存所有参数", "设置硬盘监控");
            }
        }
        #endregion 读写参数
    }
}
