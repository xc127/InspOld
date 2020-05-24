using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using BasicClass;
using Common;
using DealConfigFile;
using DealComInterface;

namespace DealRobot
{
    partial class ParSetRobot
    {
        /// <summary>
        /// 读取Ini参数
        /// </summary>
        /// <returns></returns>
        public bool ReadIniPar()
        {
            int NumError = 0;
            try
            {
                //机器人类型
                if (!ReadTypeRobot())
                {
                    NumError++;
                }

                if (NumError > 0)
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
        /// 读取机器人类型参数
        /// </summary>
        /// <returns></returns>
        public bool ReadTypeRobot()
        {
            try
            {
                //读取机器人类型
                string type = IniFile.I_I.ReadIniStr("DealRobot", "Type", "Null", ParSetRobot.c_PathTypeRobot);
                ParSetRobot.P_I.TypeRobot_e = (TypeRobot_enum)(Enum.Parse(typeof(TypeRobot_enum), type));
                //是否自动连接通信
                BlAutoConnect = IniFile.I_I.ReadIniBl("DealRobot", "BlAutoConnect", false, ParSetRobot.c_PathTypeRobot);
                
                //设置通信接口类实例
                SetParInterface(type);
                //读取参数                
                g_BaseParInterface.ReadPar();
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParSetRobot", ex);
                return false;
            }
        }
        #endregion 机器人类型

        #region 机器人数据限制
        void ReadIniRobotData()
        {
            try
            {
                for (int i = 0; i < ParCameraWork.NumCamera; i++)
                {
                    for (int j = 0; j < ParCameraWork.P_I[i + 1]; j++)//索引器获取拍照位置个数
                    {
                        string strXMin = IniFile.I_I.ReadIniStr("Camera" + (i + 1).ToString(), "XMin" + j.ToString(), c_PathRobotData);
                        string strXMax = IniFile.I_I.ReadIniStr("Camera" + (i + 1).ToString(), "XMax" + j.ToString(), c_PathRobotData);
                        string strYMin = IniFile.I_I.ReadIniStr("Camera" + (i + 1).ToString(), "YMin" + j.ToString(), c_PathRobotData);
                        string strYMax = IniFile.I_I.ReadIniStr("Camera" + (i + 1).ToString(), "YMax" + j.ToString(), c_PathRobotData);
                        string strRMin = IniFile.I_I.ReadIniStr("Camera" + (i + 1).ToString(), "RMin" + j.ToString(), c_PathRobotData);
                        string strRMax = IniFile.I_I.ReadIniStr("Camera" + (i + 1).ToString(), "RMax" + j.ToString(), c_PathRobotData);
                        string strTMin = IniFile.I_I.ReadIniStr("Camera" + (i + 1).ToString(), "TMin" + j.ToString(), c_PathRobotData);
                        string strTMax = IniFile.I_I.ReadIniStr("Camera" + (i + 1).ToString(), "TMax" + j.ToString(), c_PathRobotData);
                        string Annotation = IniFile.I_I.ReadIniStr("Camera" + (i + 1).ToString(), "Annotation" + j.ToString(), c_PathRobotData);

                        DataLimitRobot inst = new DataLimitRobot()
                        {
                            No = i * ParCameraWork.NumCamera + j,
                            IntCameraNo = i + 1,
                            IntPosNo = j + 1,
                            DblXMin = Convert.ToDouble(strXMin),
                            DblXMax = Convert.ToDouble(strXMax),
                            DblYMin = Convert.ToDouble(strTMin),
                            DblYMax = Convert.ToDouble(strTMax),
                            DblRMin = Convert.ToDouble(strTMin),
                            DblRMax = Convert.ToDouble(strTMax),
                            DblTMin = Convert.ToDouble(strTMin),
                            DblTMax = Convert.ToDouble(strTMax),
                            Annotation = Annotation
                        };
                        ParSetRobot.P_I.dataLimitRobot_L.Add(inst);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParSetRobot", ex);
            }
        }
        #endregion 机器人数据限制
    }
}
