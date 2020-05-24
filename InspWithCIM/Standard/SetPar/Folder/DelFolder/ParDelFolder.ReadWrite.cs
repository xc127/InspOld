using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using Common;
using BasicClass;

namespace SetPar
{
    public partial class ParDelFolder : BaseClass
    {
        /// <summary>
        /// 读取参数
        /// </summary>
        /// <returns></returns>
        public bool ReadIniPar()
        {
            try
            {
                // 异常信息日志
                ErrorLog.Num = IniFile.I_I.ReadIniInt("Num", "ErrorLog", 3, c_PathSetDelete);
                ErrorLog.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "ErrorLog", true, c_PathSetDelete);

                //机器人日志
                RobotLog.Num = IniFile.I_I.ReadIniInt("Num", "RobotLog", 3, c_PathSetDelete);
                RobotLog.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "RobotLog", true, c_PathSetDelete);

                //PLC日志
                PLCLog.Num = IniFile.I_I.ReadIniInt("Num", "PLCLog", 3, c_PathSetDelete);
                PLCLog.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "PLCLog", true, c_PathSetDelete);

                //Custom通信日志
                CustomLog.Num = IniFile.I_I.ReadIniInt("Num", "CustomLog", 3, c_PathSetDelete);
                CustomLog.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "CustomLog", true, c_PathSetDelete);

                //Result通信日志
                ResultLog.Num = IniFile.I_I.ReadIniInt("Num", "ResultLog", 3, c_PathSetDelete);
                ResultLog.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "ResultLog", true, c_PathSetDelete);

                //运行及报警日志
                AlarmWorkLog.Num = IniFile.I_I.ReadIniInt("Num", "AlarmWorkLog", 3, c_PathSetDelete);
                AlarmWorkLog.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "AlarmWorkLog", true, c_PathSetDelete);

                BackUp.Num = IniFile.I_I.ReadIniInt("Num", "BackUp", 1, c_PathSetDelete);
                BackUp.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "BackUp", true, c_PathSetDelete);
                if (BackUp.Num < 3)
                {
                    BackUp.Num = 3;
                }

                CIM.Num = IniFile.I_I.ReadIniInt("Num", "CIM", 3, c_PathSetDelete);
                CIM.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "CIM", true, c_PathSetDelete);

                #region 图片删除
                ImageRecord.Num = IniFile.I_I.ReadIniInt("Num", "ImageRecord", 1, ParDelFolder.c_PathSetDelete);
                ImageRecord.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "ImageRecord", true, ParDelFolder.c_PathSetDelete);

                OldImageRecord.Num = IniFile.I_I.ReadIniInt("Num", "OldImageRecord", 1, ParDelFolder.c_PathSetDelete);
                OldImageRecord.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "OldImageRecord", true, ParDelFolder.c_PathSetDelete);
                #endregion 图片删除

                //信息日志
                Tact_DealCell.Num = IniFile.I_I.ReadIniInt("Num", "Tact_DealCell", 3, c_PathSetDelete);
                Tact_DealCell.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "Tact_DealCell", true, c_PathSetDelete);

                Tact_Camera.Num = IniFile.I_I.ReadIniInt("Num", "Tact_Camera", 3, c_PathSetDelete);
                Tact_Camera.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "Tact_Camera", true, c_PathSetDelete);

                ImageInsp.Num = IniFile.I_I.ReadIniInt("Num", "ImageInsp", 3, c_PathSetDelete);
                ImageInsp.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "ImageInsp", true, c_PathSetDelete);

                ImageInspNG.Num = IniFile.I_I.ReadIniInt("Num", "ImageInspNG", 3, c_PathSetDelete);
                ImageInspNG.BlExcute = IniFile.I_I.ReadIniBl("BlExcute", "ImageInspNG", true, c_PathSetDelete);

                AddParDelFolder();//将设置添加到集合里

                //删除时间
                BlDel1 = false;//IniFile.I_I.ReadIniBl("Del", "BlDel1", true, ParDelFolder.c_PathSetDelete);
                BlDel2 = false;//IniFile.I_I.ReadIniBl("Del", "BlDel2", true, ParDelFolder.c_PathSetDelete);

                Time1 = IniFile.I_I.ReadIniStr("Del", "Time1", "8:00", ParDelFolder.c_PathSetDelete);
                Time2 = IniFile.I_I.ReadIniStr("Del", "Time2", "20:00", ParDelFolder.c_PathSetDelete);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }

        /// <summary>
        /// 写入
        /// </summary>
        /// <returns></returns>
        public bool WriteDeleteIni()
        {
            try
            {
                IniFile.I_I.WriteIni("Num", "ErrorLog", ParDelFolder.P_I.BaseParDelFolder_L[0].Num.ToString(), c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "ErrorLog", ParDelFolder.P_I.BaseParDelFolder_L[0].BlExcute.ToString(), c_PathSetDelete);

                IniFile.I_I.WriteIni("Num", "RobotLog", ParDelFolder.P_I.BaseParDelFolder_L[1].Num.ToString(), c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "RobotLog", ParDelFolder.P_I.BaseParDelFolder_L[1].BlExcute.ToString(), c_PathSetDelete);

                IniFile.I_I.WriteIni("Num", "PLCLog", ParDelFolder.P_I.BaseParDelFolder_L[2].Num.ToString(), c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "PLCLog", ParDelFolder.P_I.BaseParDelFolder_L[2].BlExcute.ToString(), c_PathSetDelete);

                IniFile.I_I.WriteIni("Num", "CustomLog", ParDelFolder.P_I.BaseParDelFolder_L[3].Num.ToString(), c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "CustomLog", ParDelFolder.P_I.BaseParDelFolder_L[3].BlExcute.ToString(), c_PathSetDelete);

                IniFile.I_I.WriteIni("Num", "ResultLog", ParDelFolder.P_I.BaseParDelFolder_L[4].Num.ToString(), c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "ResultLog", ParDelFolder.P_I.BaseParDelFolder_L[4].BlExcute.ToString(), c_PathSetDelete);

                IniFile.I_I.WriteIni("Num", "AlarmWorkLog", ParDelFolder.P_I.BaseParDelFolder_L[5].Num.ToString(), c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "AlarmWorkLog", ParDelFolder.P_I.BaseParDelFolder_L[5].BlExcute.ToString(), c_PathSetDelete);

                IniFile.I_I.WriteIni("Num", "BackUp", ParDelFolder.P_I.BaseParDelFolder_L[6].Num.ToString(), c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "BackUp", ParDelFolder.P_I.BaseParDelFolder_L[6].BlExcute.ToString(), c_PathSetDelete);

                IniFile.I_I.WriteIni("Num", "CIM", ParDelFolder.P_I.BaseParDelFolder_L[7].Num.ToString(), c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "CIM", ParDelFolder.P_I.BaseParDelFolder_L[7].BlExcute.ToString(), c_PathSetDelete);

                #region 图片删除
                IniFile.I_I.WriteIni("Num", "ImageRecord", ParDelFolder.P_I.BaseParDelFolder_L[10].Num.ToString(), ParDelFolder.c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "ImageRecord", ParDelFolder.P_I.BaseParDelFolder_L[10].BlExcute.ToString(), ParDelFolder.c_PathSetDelete);

                IniFile.I_I.WriteIni("Num", "OldImageRecord", ParDelFolder.P_I.BaseParDelFolder_L[11].Num.ToString(), ParDelFolder.c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "OldImageRecord", ParDelFolder.P_I.BaseParDelFolder_L[11].BlExcute.ToString(), ParDelFolder.c_PathSetDelete);
                #endregion 图片删除

                //删除日志
                IniFile.I_I.WriteIni("Num", "Tact_DealCell", ParDelFolder.P_I.BaseParDelFolder_L[8].Num.ToString(), c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "Tact_DealCell", ParDelFolder.P_I.BaseParDelFolder_L[8].BlExcute.ToString(), c_PathSetDelete);

                IniFile.I_I.WriteIni("Num", "Tact_Camera", ParDelFolder.P_I.BaseParDelFolder_L[9].Num.ToString(), c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "Tact_Camera", ParDelFolder.P_I.BaseParDelFolder_L[9].BlExcute.ToString(), c_PathSetDelete);

                IniFile.I_I.WriteIni("Num", "ImageInsp", ParDelFolder.P_I.BaseParDelFolder_L[12].Num.ToString(), c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "ImageInsp", ParDelFolder.P_I.BaseParDelFolder_L[12].BlExcute.ToString(), c_PathSetDelete);

                IniFile.I_I.WriteIni("Num", "ImageInspNG", ParDelFolder.P_I.BaseParDelFolder_L[13].Num.ToString(), c_PathSetDelete);
                IniFile.I_I.WriteIni("BlExcute", "ImageInspNG", ParDelFolder.P_I.BaseParDelFolder_L[13].BlExcute.ToString(), c_PathSetDelete);

                //删除时间
                IniFile.I_I.WriteIni("Del", "BlDel1", BlDel1.ToString(), ParDelFolder.c_PathSetDelete);
                IniFile.I_I.WriteIni("Del", "BlDel2", BlDel2.ToString(), ParDelFolder.c_PathSetDelete);

                IniFile.I_I.WriteIni("Del", "Time1", Time1, ParDelFolder.c_PathSetDelete);
                IniFile.I_I.WriteIni("Del", "Time2", Time2, ParDelFolder.c_PathSetDelete);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
        }
    }
}
