using BasicClass;
using DealFile;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DealCIM
{
    partial class PostParams
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region 读写保存参数的接口
        /// <summary>
        /// 从配置文件读取保存在本地的CIM连接配置
        /// </summary>
        public void InitParams()
        {
            try
            {
                #region params
                ETypeMode = (TypeMode)Enum.Parse(typeof(TypeMode), GetCimConfig(CIM_PARAMS.ModeType.ToString()));
                StrSendQueue = GetCimConfig(CIM_PARAMS.SendQueue.ToString(), ETypeMode.ToString());
                StrReadQueue = GetCimConfig(CIM_PARAMS.ReadQueue.ToString(), ETypeMode.ToString());
                StrIP = GetCimConfig(CIM_PARAMS.IP.ToString(), ETypeMode.ToString());
                StrPort = GetCimConfig(CIM_PARAMS.Port.ToString(), ETypeMode.ToString());
                iCycTimes = short.Parse(GetCimConfig(CIM_PARAMS.CycTimes.ToString()));
                StrUserID = GetCimConfig(CIM_PARAMS.UserID.ToString());
                StrFab = GetCimConfig(CIM_PARAMS.Fab.ToString());
                StrArea = GetCimConfig(CIM_PARAMS.Area.ToString());
                StrLine = GetCimConfig(CIM_PARAMS.Line.ToString());
                StrOperation = GetCimConfig(CIM_PARAMS.Operation.ToString());
                StrLot = GetCimConfig(CIM_PARAMS.RunCard.ToString());
                StrModelNo = GetCimConfig(CIM_PARAMS.ModelNo.ToString());
                StrCom = GetCimConfig(CIM_PARAMS.COM.ToString());
                iBaudrate = short.Parse(GetCimConfig(CIM_PARAMS.Baudrate.ToString()));
                iCodeDelay = short.Parse(GetCimConfig(CIM_PARAMS.CodeDelay.ToString()));
                ETypeCode = (TypeCode_enum)Enum.Parse(typeof(TypeCode_enum), GetCimConfig(CIM_PARAMS.CodeType.ToString()));
                BlLog = bool.Parse(GetCimConfig(CIM_PARAMS.BlLog.ToString()));
                #endregion                
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }

            InitCimMode();
        }

        void InitCimMode()
        {
            try
            {
                #region mode                
                BlCodeOn = bool.Parse(GetCimConfig(CIMMODE_ENUM.CodeOn.ToString(), "CIMMODE"));
                BlCimOn = bool.Parse(GetCimConfig(CIMMODE_ENUM.CimOn.ToString(), "CIMMODE"));
                BlVerifyChipID = bool.Parse(GetCimConfig(CIMMODE_ENUM.VerifyChipid.ToString(), "CIMMODE"));
                BlPassVerifyCode = bool.Parse(GetCimConfig(CIMMODE_ENUM.PassVerifyCode.ToString(), "CIMMODE"));
                #endregion
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        /// <summary>
        /// 以不同的mode作为section保存部分参数到ini
        /// </summary>
        /// <param name="mode"></param>
        public void ReadCimConfig(string mode)
        {
            try
            {
                StrSendQueue = GetCimConfig(CIM_PARAMS.SendQueue.ToString(), mode.ToString());
                StrReadQueue = GetCimConfig(CIM_PARAMS.ReadQueue.ToString(), mode.ToString());
                StrIP = GetCimConfig(CIM_PARAMS.IP.ToString(), mode.ToString());
                StrPort = GetCimConfig(CIM_PARAMS.Port.ToString(), mode.ToString());
                //iCycTimes = Int16.Parse(GetCimConfig(CIM_PARAMS.CycTimes.ToString()));
                //StrUserID = GetCimConfig(CIM_PARAMS.UserID.ToString());
                //StrFab = GetCimConfig(CIM_PARAMS.Fab.ToString());
                //StrArea = GetCimConfig(CIM_PARAMS.Area.ToString());
                //StrLine = GetCimConfig(CIM_PARAMS.Line.ToString());
                //StrOperation = GetCimConfig(CIM_PARAMS.Operation.ToString());
                //StrLot = GetCimConfig(CIM_PARAMS.RunCard.ToString());
                //StrModelNo = GetCimConfig(CIM_PARAMS.ModelNo.ToString());
                //StrCom = GetCimConfig(CIM_PARAMS.COM.ToString());
                //iBaudrate = Int16.Parse(GetCimConfig(CIM_PARAMS.Baudrate.ToString()));
                //ETypeCode = (TypeCode_enum)Enum.Parse(typeof(TypeCode_enum), GetCimConfig(CIM_PARAMS.CodeType.ToString()));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }

        /// <summary>
        /// 封装个接口，少写几个变量，方便统一改动
        /// </summary>
        /// <param name="key">需要读取的变量</param>
        /// <param name="section">部分参数根据mode保存，也要根据mode读取</param>
        /// <returns></returns>
        private string GetCimConfig(string key, string section = commonSection)
        {
            try
            {
                return IniFile.I_I.ReadIniStr(section, key, "", Path_Config);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 将所有CIM参数写入到ini，部分以mode为section保存，其余是公共参数
        /// </summary>
        public void WriteCimConfig()
        {
            #region params
            WriteCimConfig(CIM_PARAMS.SendQueue.ToString(), StrSendQueue, ETypeMode.ToString());
            WriteCimConfig(CIM_PARAMS.ReadQueue.ToString(), StrReadQueue, ETypeMode.ToString());
            WriteCimConfig(CIM_PARAMS.IP.ToString(), StrIP, ETypeMode.ToString());
            WriteCimConfig(CIM_PARAMS.Port.ToString(), StrPort, ETypeMode.ToString());
            WriteCimConfig(CIM_PARAMS.CycTimes.ToString(), iCycTimes.ToString());
            WriteCimConfig(CIM_PARAMS.UserID.ToString(), StrUserID);
            WriteCimConfig(CIM_PARAMS.Fab.ToString(), StrFab);
            WriteCimConfig(CIM_PARAMS.Area.ToString(), StrArea);
            WriteCimConfig(CIM_PARAMS.Line.ToString(), StrLine);
            WriteCimConfig(CIM_PARAMS.Operation.ToString(), StrOperation);
            WriteCimConfig(CIM_PARAMS.ModelNo.ToString(), StrModelNo);
            WriteCimConfig(CIM_PARAMS.RunCard.ToString(), StrLot);
            WriteCimConfig(CIM_PARAMS.COM.ToString(), StrCom);
            WriteCimConfig(CIM_PARAMS.CodeDelay.ToString(), iCodeDelay.ToString());
            WriteCimConfig(CIM_PARAMS.Baudrate.ToString(), iBaudrate.ToString());
            WriteCimConfig(CIM_PARAMS.CodeType.ToString(), ETypeCode.ToString());
            WriteCimConfig(CIM_PARAMS.ModeType.ToString(), ETypeMode.ToString());
            WriteCimConfig(CIM_PARAMS.BlLog.ToString(), BlLog.ToString());
            #endregion            
        }

        public void WriteCimModeConfig()
        {
            #region mode
            WriteCimConfig(CIMMODE_ENUM.CodeOn.ToString(), BlCodeOn.ToString(), "CIMMODE");
            WriteCimConfig(CIMMODE_ENUM.CimOn.ToString(), BlCimOn.ToString(), "CIMMODE");
            WriteCimConfig(CIMMODE_ENUM.VerifyChipid.ToString(), BlVerifyChipID.ToString(), "CIMMODE");
            WriteCimConfig(CIMMODE_ENUM.PassVerifyCode.ToString(), BlPassVerifyCode.ToString(), "CIMMODE");
            #endregion
        }

        /// <summary>
        /// 将CIM参数记录到本地
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void WriteCimConfig(string key, string value, string section = commonSection)
        {
            try
            {
                IniFile.I_I.WriteIni(section, key, value, Path_Config);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
        }
        #endregion
    }
}
