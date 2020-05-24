using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using BasicClass;
using Common;
using DealConfigFile;

namespace DealRobot
{
    partial class ParSetRobot
    {
        //写入Ini
        public bool WriteIniPar()
        {
            int numError = 0;
            try
            {
                //机器人类型
                if (!WriteTypeRobot())
                {
                    numError++;
                }
                if (numError > 0)
                {
                    return false;
                }
                return true;       
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParSetRobot", ex);
                return false;
            }            
        }

        #region 机器人类型
        /// <summary>
        /// 写入机器人参数
        /// </summary>
        /// <returns></returns>
        public bool WriteTypeRobot()
        {
            try
            {
                IniFile.I_I.WriteIni("DealRobot", "Type", ParSetRobot.P_I.TypeRobot_e.ToString(), ParSetRobot.c_PathTypeRobot);
                IniFile.I_I.WriteIni("DealRobot", "BlAutoConnect", ParSetRobot.P_I.BlAutoConnect.ToString(), ParSetRobot.c_PathTypeRobot);
           
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParSetRobot", ex);
                return false;
            }
        }
        #endregion 机器人类型

        #region 工作方式
        public bool WriteIniWork()
        {
            try
            {
                IniFile.I_I.WriteIni("Work", "AlarmDataOverflow", blDataOverflow.ToString(), c_PathRobotWork);
                IniFile.I_I.WriteIni("Work", "AlarmShake", blAlarmShake.ToString(), c_PathRobotWork);
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParSetRobot", ex);
                return false;
            }
        }
        #endregion 工作方式

        #region 机器人数据限制
        public bool WriteIniRobotData()
        {
            try
            {
                for (int i = 0; i < ParCameraWork.NumCamera; i++)
                {
                    for (int j = 0; j < ParCameraWork.P_I[i + 1]; j++)//索引器获取拍照位置个数
                    {
                        int m = i * ParCameraWork.NumCamera + j;
                        IniFile.I_I.WriteIni("Camera" + (i + 1).ToString(), "XMin" + j.ToString(), dataLimitRobot_L[m].DblXMin.ToString(), c_PathRobotData);
                        IniFile.I_I.WriteIni("Camera" + (i + 1).ToString(), "XMax" + j.ToString(), dataLimitRobot_L[m].DblXMax.ToString(), c_PathRobotData);
                        IniFile.I_I.WriteIni("Camera" + (i + 1).ToString(), "YMin" + j.ToString(), dataLimitRobot_L[m].DblYMin.ToString(), c_PathRobotData);
                        IniFile.I_I.WriteIni("Camera" + (i + 1).ToString(), "YMax" + j.ToString(), dataLimitRobot_L[m].DblYMax.ToString(), c_PathRobotData);
                        IniFile.I_I.WriteIni("Camera" + (i + 1).ToString(), "RMin" + j.ToString(), dataLimitRobot_L[m].DblRMin.ToString(), c_PathRobotData);
                        IniFile.I_I.WriteIni("Camera" + (i + 1).ToString(), "RMax" + j.ToString(), dataLimitRobot_L[m].DblRMax.ToString(), c_PathRobotData);
                        IniFile.I_I.WriteIni("Camera" + (i + 1).ToString(), "TMin" + j.ToString(), dataLimitRobot_L[m].DblTMin.ToString(), c_PathRobotData);
                        IniFile.I_I.WriteIni("Camera" + (i + 1).ToString(), "TMax" + j.ToString(), dataLimitRobot_L[m].DblTMax.ToString(), c_PathRobotData);
                        IniFile.I_I.WriteIni("Camera" + (i + 1).ToString(), "Annotation" + j.ToString(), dataLimitRobot_L[m].Annotation.ToString(), c_PathRobotData);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParSetRobot", ex);
                return false;
            }
        }
        #endregion 机器人数据限制
    }
    
}
