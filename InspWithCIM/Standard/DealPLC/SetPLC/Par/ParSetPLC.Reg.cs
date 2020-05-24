using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Common;
using DealConfigFile;
using BasicClass;

namespace DealPLC
{
    partial class ParSetPLC
    {
        #region 定义
        #region Path
        public static string c_PathPLC = ParPathRoot.PathRoot + "Store\\PLC\\TypePLC.ini";//DealPLC
        public static string c_PathCyc = ParPathRoot.PathRoot + "Store\\PLC\\RegCyc.ini";//触发寄存器存储文件路径
        public static string c_PathCustomReg = ParPathRoot.PathRoot + "Store\\PLC\\RegData.ini";//自定义寄存器参数公共类
        public static string c_PathConfigPar = ParPathRoot.PathRoot + "Store\\PLC\\RegConfigPar.ini";
        #endregion Path

        #region string
        public string RegCyc = "";//循环读取的寄存器
        #endregion string

        #region int
        //循环读取寄存器的个数
        public int IntNumCycReg
        {
            get
            {
                int num = RegMonitor.R_I.NumRegSingle + ParCameraWork.NumCamera;
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)
                {
                    num = (RegMonitor.R_I.NumRegSingle);
                }
                return num;
            }
        }       
        #endregion int    
        #endregion 定义

        #region 监控寄存器
        //PC
        public string regSoftReStart//软件重启
        {
            get
            {
                return RegMonitor.R_I[0].NameReg.Replace("\\n", "\n");
            }
        }
        public string regPCShutDown//PC关机
        {
            get
            {
                return RegMonitor.R_I[1].NameReg.Replace("\\n", "\n");
            }
        }
        public string regPCReStart//软件重启
        {
            get
            {
                return RegMonitor.R_I[2].NameReg.Replace("\\n", "\n");
            }
        }
        
        public string regHeartBeat//心跳
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[0].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegMonitor.R_I[3].NameReg.Replace("\\n", "\n");
                }
            }
        }


        public string regPLCState//设备运行状态
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[1].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegMonitor.R_I[4].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public string regPLCAlarm//设备报警信息
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[2].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegMonitor.R_I[5].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public string regPLCMaterial//设备物料信息
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[7].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegMonitor.R_I[6].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public string regRobotState//机器人状态
        {
            get
            {
                return RegMonitor.R_I[4].NameReg.Replace("\\n", "\n");
            }
        }
      
        //Model
        public string regNewModel//新建配置文件
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[3].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegMonitor.R_I[8].NameReg.Replace("\\n", "\n");
                }
            }
        }

        public string regChangeModel//切换配置文件
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[4].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegMonitor.R_I[9].NameReg.Replace("\\n", "\n");
                }
            }
        }

        public string regDelModel//删除配置文件
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[5].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegMonitor.R_I[10].NameReg.Replace("\\n", "\n");
                }
            }
        }

        public string regRestartCommunicate//重启机器人通用端口等通信
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[6].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegMonitor.R_I[11].NameReg.Replace("\\n", "\n");
                }
            }
        }

        public string regVoice//语音信息
        {
            get
            {
                return RegMonitor.R_I[12].NameReg.Replace("\\n", "\n");
            }
        }

        public string regTypeRun//设备运行状态
        {
            get
            {
                return RegMonitor.R_I[14].NameReg.Replace("\\n", "\n");
            }
        }  
        #endregion 监控寄存器

        #region 相机数据寄存器
        #region 相机1
        public string regClearCamera1//相机1拍照
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[6].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[0].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public string regFinishPhoto_Camera1//拍照完成及结果
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[0].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[1].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public RegPLC regDataX_Camera1//X
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[1].Co;
                    inst.DblMax = RegCameraData.R_I[1].DblMax;
                    inst.DblMin = RegCameraData.R_I[1].DblMin;
                    inst.NameReg = RegCameraData.R_I[1].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[2].Co;
                    inst.DblMax = RegCameraData.R_I[2].DblMax;
                    inst.DblMin = RegCameraData.R_I[2].DblMin;
                    inst.NameReg = RegCameraData.R_I[2].NameReg.Replace("\\n", "\n");
                }
                return inst;                
            }
        }
        public RegPLC regDataY_Camera1//Y
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[2].Co;
                    inst.DblMax = RegCameraData.R_I[2].DblMax;
                    inst.DblMin = RegCameraData.R_I[2].DblMin;
                    inst.NameReg = RegCameraData.R_I[2].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[3].Co;
                    inst.DblMax = RegCameraData.R_I[3].DblMax;
                    inst.DblMin = RegCameraData.R_I[3].DblMin;
                    inst.NameReg = RegCameraData.R_I[3].NameReg.Replace("\\n", "\n");
                }
                return inst;    
            }
        }

        public RegPLC regDataZ_Camera1//Z
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[3].Co;
                    inst.DblMax = RegCameraData.R_I[3].DblMax;
                    inst.DblMin = RegCameraData.R_I[3].DblMin;
                    inst.NameReg = RegCameraData.R_I[3].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[4].Co;
                    inst.DblMax = RegCameraData.R_I[4].DblMax;
                    inst.DblMin = RegCameraData.R_I[4].DblMin;
                    inst.NameReg = RegCameraData.R_I[4].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataR_Jamera1//R
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[4].Co;
                    inst.DblMax = RegCameraData.R_I[4].DblMax;
                    inst.DblMin = RegCameraData.R_I[4].DblMin;
                    inst.NameReg = RegCameraData.R_I[4].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[5].Co;
                    inst.DblMax = RegCameraData.R_I[5].DblMax;
                    inst.DblMin = RegCameraData.R_I[5].DblMin;
                    inst.NameReg = RegCameraData.R_I[5].NameReg.Replace("\\n", "\n");
                }
                return inst;    
            }
        }
        public string regFinsihData_Camera1//数据传输完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[5].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[6].NameReg.Replace("\\n", "\n");
                }
            }
        }
        #endregion 相机1

        #region 相机2
        public string regClearCamera2//相机2拍照
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[7].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[7].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public string regFinishPhoto_Camera2//拍照完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[6].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[8].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public RegPLC regDataX_Camera2//X
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[7].Co;
                    inst.DblMax = RegCameraData.R_I[7].DblMax;
                    inst.DblMin = RegCameraData.R_I[7].DblMin;
                    inst.NameReg = RegCameraData.R_I[7].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[9].Co;
                    inst.DblMax = RegCameraData.R_I[9].DblMax;
                    inst.DblMin = RegCameraData.R_I[9].DblMin;
                    inst.NameReg = RegCameraData.R_I[9].NameReg.Replace("\\n", "\n");
                }
                return inst;                 
            }
        }
        public RegPLC regDataY_Camera2//Y
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[8].Co;
                    inst.DblMax = RegCameraData.R_I[8].DblMax;
                    inst.DblMin = RegCameraData.R_I[8].DblMin;
                    inst.NameReg = RegCameraData.R_I[8].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[10].Co;
                    inst.DblMax = RegCameraData.R_I[10].DblMax;
                    inst.DblMin = RegCameraData.R_I[10].DblMin;
                    inst.NameReg = RegCameraData.R_I[10].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }

        public RegPLC regDataZ_Camera2//Z
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[9].Co;
                    inst.DblMax = RegCameraData.R_I[9].DblMax;
                    inst.DblMin = RegCameraData.R_I[9].DblMin;
                    inst.NameReg = RegCameraData.R_I[9].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[11].Co;
                    inst.DblMax = RegCameraData.R_I[11].DblMax;
                    inst.DblMin = RegCameraData.R_I[11].DblMin;
                    inst.NameReg = RegCameraData.R_I[11].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }

        public RegPLC regDataR_Jamera2//R
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[10].Co;
                    inst.DblMax = RegCameraData.R_I[10].DblMax;
                    inst.DblMin = RegCameraData.R_I[10].DblMin;
                    inst.NameReg = RegCameraData.R_I[10].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[12].Co;
                    inst.DblMax = RegCameraData.R_I[12].DblMax;
                    inst.DblMin = RegCameraData.R_I[12].DblMin;
                    inst.NameReg = RegCameraData.R_I[12].NameReg.Replace("\\n", "\n");
                }
                return inst;   
            }
        }
        public string regFinsihData_Camera2//数据传输完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[11].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[13].NameReg.Replace("\\n", "\n");
                }
            }
        }
        #endregion 相机2

        #region 相机3
        public string regClearCamera3//相机3拍照
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[8].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[14].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public string regFinishPhoto_Camera3//拍照完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[12].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[15].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public RegPLC regDataX_Camera3//X
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[13].Co;
                    inst.DblMax = RegCameraData.R_I[13].DblMax;
                    inst.DblMin = RegCameraData.R_I[13].DblMin;
                    inst.NameReg = RegCameraData.R_I[13].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[16].Co;
                    inst.DblMax = RegCameraData.R_I[16].DblMax;
                    inst.DblMin = RegCameraData.R_I[16].DblMin;
                    inst.NameReg = RegCameraData.R_I[16].NameReg.Replace("\\n", "\n");
                }
                return inst;   
            }
        }
        public RegPLC regDataY_Camera3//Y
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[14].Co;
                    inst.DblMax = RegCameraData.R_I[14].DblMax;
                    inst.DblMin = RegCameraData.R_I[14].DblMin;
                    inst.NameReg = RegCameraData.R_I[14].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[17].Co;
                    inst.DblMax = RegCameraData.R_I[17].DblMax;
                    inst.DblMin = RegCameraData.R_I[17].DblMin;
                    inst.NameReg = RegCameraData.R_I[17].NameReg.Replace("\\n", "\n");
                }
                return inst;   
            }
        }

        public RegPLC regDataZ_Camera3//Z
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[15].Co;
                    inst.DblMax = RegCameraData.R_I[15].DblMax;
                    inst.DblMin = RegCameraData.R_I[15].DblMin;
                    inst.NameReg = RegCameraData.R_I[15].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[18].Co;
                    inst.DblMax = RegCameraData.R_I[18].DblMax;
                    inst.DblMin = RegCameraData.R_I[18].DblMin;
                    inst.NameReg = RegCameraData.R_I[18].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataR_Jamera3//R
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[16].Co;
                    inst.DblMax = RegCameraData.R_I[16].DblMax;
                    inst.DblMin = RegCameraData.R_I[16].DblMin;
                    inst.NameReg = RegCameraData.R_I[16].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[19].Co;
                    inst.DblMax = RegCameraData.R_I[19].DblMax;
                    inst.DblMin = RegCameraData.R_I[19].DblMin;
                    inst.NameReg = RegCameraData.R_I[19].NameReg.Replace("\\n", "\n");
                }
                return inst;   
            }
        }
        public string regFinsihData_Camera3//数据传输完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[17].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[20].NameReg.Replace("\\n", "\n");
                }
            }
        }
        #endregion 相机3

        #region 相机4
        public string regClearCamera4//相机4拍照
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[9].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[21].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public string regFinishPhoto_Camera4//拍照完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[18].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[22].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public RegPLC regDataX_Camera4//X
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[19].Co;
                    inst.DblMax = RegCameraData.R_I[19].DblMax;
                    inst.DblMin = RegCameraData.R_I[19].DblMin;
                    inst.NameReg = RegCameraData.R_I[19].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[23].Co;
                    inst.DblMax = RegCameraData.R_I[23].DblMax;
                    inst.DblMin = RegCameraData.R_I[23].DblMin;
                    inst.NameReg = RegCameraData.R_I[23].NameReg.Replace("\\n", "\n");
                }
                return inst;   
            }
        }
        public RegPLC regDataY_Camera4//Y
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[20].Co;
                    inst.DblMax = RegCameraData.R_I[20].DblMax;
                    inst.DblMin = RegCameraData.R_I[20].DblMin;
                    inst.NameReg = RegCameraData.R_I[20].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[24].Co;
                    inst.DblMax = RegCameraData.R_I[24].DblMax;
                    inst.DblMin = RegCameraData.R_I[24].DblMin;
                    inst.NameReg = RegCameraData.R_I[24].NameReg.Replace("\\n", "\n");
                }
                return inst;  
            }
        }
        public RegPLC regDataZ_Camera4//Z
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[21].Co;
                    inst.DblMax = RegCameraData.R_I[21].DblMax;
                    inst.DblMin = RegCameraData.R_I[21].DblMin;
                    inst.NameReg = RegCameraData.R_I[21].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[25].Co;
                    inst.DblMax = RegCameraData.R_I[25].DblMax;
                    inst.DblMin = RegCameraData.R_I[25].DblMin;
                    inst.NameReg = RegCameraData.R_I[25].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataR_Jamera4//R
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[22].Co;
                    inst.DblMax = RegCameraData.R_I[22].DblMax;
                    inst.DblMin = RegCameraData.R_I[22].DblMin;
                    inst.NameReg = RegCameraData.R_I[22].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[26].Co;
                    inst.DblMax = RegCameraData.R_I[26].DblMax;
                    inst.DblMin = RegCameraData.R_I[26].DblMin;
                    inst.NameReg = RegCameraData.R_I[26].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public string regFinsihData_Camera4//数据传输完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[23].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[27].NameReg.Replace("\\n", "\n");
                }
            }
        }
        #endregion 相机4

        #region 相机5
        public string regClearCamera5//相机5拍照
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[10].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[28].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public string regFinishPhoto_Camera5//拍照完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[24].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[29].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public RegPLC regDataX_Camera5//X
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[25].Co;
                    inst.DblMax = RegCameraData.R_I[25].DblMax;
                    inst.DblMin = RegCameraData.R_I[25].DblMin;
                    inst.NameReg = RegCameraData.R_I[25].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[30].Co;
                    inst.DblMax = RegCameraData.R_I[30].DblMax;
                    inst.DblMin = RegCameraData.R_I[30].DblMin;
                    inst.NameReg = RegCameraData.R_I[30].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataY_Camera5//Y
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[26].Co;
                    inst.DblMax = RegCameraData.R_I[26].DblMax;
                    inst.DblMin = RegCameraData.R_I[26].DblMin;
                    inst.NameReg = RegCameraData.R_I[26].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[31].Co;
                    inst.DblMax = RegCameraData.R_I[31].DblMax;
                    inst.DblMin = RegCameraData.R_I[31].DblMin;
                    inst.NameReg = RegCameraData.R_I[31].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataZ_Camera5//Y
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[27].Co;
                    inst.DblMax = RegCameraData.R_I[27].DblMax;
                    inst.DblMin = RegCameraData.R_I[27].DblMin;
                    inst.NameReg = RegCameraData.R_I[27].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[32].Co;
                    inst.DblMax = RegCameraData.R_I[32].DblMax;
                    inst.DblMin = RegCameraData.R_I[32].DblMin;
                    inst.NameReg = RegCameraData.R_I[32].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataR_Jamera5//R
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[28].Co;
                    inst.DblMax = RegCameraData.R_I[28].DblMax;
                    inst.DblMin = RegCameraData.R_I[28].DblMin;
                    inst.NameReg = RegCameraData.R_I[28].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[33].Co;
                    inst.DblMax = RegCameraData.R_I[33].DblMax;
                    inst.DblMin = RegCameraData.R_I[33].DblMin;
                    inst.NameReg = RegCameraData.R_I[33].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public string regFinsihData_Camera5//数据传输完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[29].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[34].NameReg.Replace("\\n", "\n");
                }
            }
        }
        #endregion 相机5

        #region 相机6
        public string regClearCamera6//相机6拍照
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[11].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[35].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public string regFinishPhoto_Camera6//拍照完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[30].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[36].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public RegPLC regDataX_Camera6//X
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[31].Co;
                    inst.DblMax = RegCameraData.R_I[31].DblMax;
                    inst.DblMin = RegCameraData.R_I[31].DblMin;
                    inst.NameReg = RegCameraData.R_I[31].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[37].Co;
                    inst.DblMax = RegCameraData.R_I[37].DblMax;
                    inst.DblMin = RegCameraData.R_I[37].DblMin;
                    inst.NameReg = RegCameraData.R_I[37].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataY_Camera6//Y
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[32].Co;
                    inst.DblMax = RegCameraData.R_I[32].DblMax;
                    inst.DblMin = RegCameraData.R_I[32].DblMin;
                    inst.NameReg = RegCameraData.R_I[32].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[38].Co;
                    inst.DblMax = RegCameraData.R_I[38].DblMax;
                    inst.DblMin = RegCameraData.R_I[38].DblMin;
                    inst.NameReg = RegCameraData.R_I[38].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataZ_Camera6//Z
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[33].Co;
                    inst.DblMax = RegCameraData.R_I[33].DblMax;
                    inst.DblMin = RegCameraData.R_I[33].DblMin;
                    inst.NameReg = RegCameraData.R_I[33].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[39].Co;
                    inst.DblMax = RegCameraData.R_I[39].DblMax;
                    inst.DblMin = RegCameraData.R_I[39].DblMin;
                    inst.NameReg = RegCameraData.R_I[39].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataR_Jamera6//R
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[34].Co;
                    inst.DblMax = RegCameraData.R_I[34].DblMax;
                    inst.DblMin = RegCameraData.R_I[34].DblMin;
                    inst.NameReg = RegCameraData.R_I[34].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[40].Co;
                    inst.DblMax = RegCameraData.R_I[40].DblMax;
                    inst.DblMin = RegCameraData.R_I[40].DblMin;
                    inst.NameReg = RegCameraData.R_I[40].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public string regFinsihData_Camera6//数据传输完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[35].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[41].NameReg.Replace("\\n", "\n");
                }
            }
        }
        #endregion 相机6
        
        #region 相机7
        public string regClearCamera7//相机6拍照
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[12].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[42].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public string regFinishPhoto_Camera7//拍照完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[36].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[43].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public RegPLC regDataX_Camera7//X
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[37].Co;
                    inst.DblMax = RegCameraData.R_I[37].DblMax;
                    inst.DblMin = RegCameraData.R_I[37].DblMin;
                    inst.NameReg = RegCameraData.R_I[37].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[44].Co;
                    inst.DblMax = RegCameraData.R_I[44].DblMax;
                    inst.DblMin = RegCameraData.R_I[44].DblMin;
                    inst.NameReg = RegCameraData.R_I[44].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataY_Camera7//Y
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[38].Co;
                    inst.DblMax = RegCameraData.R_I[38].DblMax;
                    inst.DblMin = RegCameraData.R_I[38].DblMin;
                    inst.NameReg = RegCameraData.R_I[38].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[45].Co;
                    inst.DblMax = RegCameraData.R_I[45].DblMax;
                    inst.DblMin = RegCameraData.R_I[45].DblMin;
                    inst.NameReg = RegCameraData.R_I[45].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataZ_Camera7//Z
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[39].Co;
                    inst.DblMax = RegCameraData.R_I[39].DblMax;
                    inst.DblMin = RegCameraData.R_I[39].DblMin;
                    inst.NameReg = RegCameraData.R_I[39].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[46].Co;
                    inst.DblMax = RegCameraData.R_I[46].DblMax;
                    inst.DblMin = RegCameraData.R_I[46].DblMin;
                    inst.NameReg = RegCameraData.R_I[46].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataR_Jamera7//R
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[40].Co;
                    inst.DblMax = RegCameraData.R_I[40].DblMax;
                    inst.DblMin = RegCameraData.R_I[40].DblMin;
                    inst.NameReg = RegCameraData.R_I[40].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[47].Co;
                    inst.DblMax = RegCameraData.R_I[47].DblMax;
                    inst.DblMin = RegCameraData.R_I[47].DblMin;
                    inst.NameReg = RegCameraData.R_I[47].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public string regFinsihData_Camera7//数据传输完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[41].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[48].NameReg.Replace("\\n", "\n");
                }
            }
        }
        #endregion 相机7

        #region 相机8
        public string regClearCamera8//相机6拍照
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegMonitor.R_I[13].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[49].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public string regFinishPhoto_Camera8//拍照完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[42].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[50].NameReg.Replace("\\n", "\n");
                }
            }
        }
        public RegPLC regDataX_Camera8//X
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[43].Co;
                    inst.DblMax = RegCameraData.R_I[43].DblMax;
                    inst.DblMin = RegCameraData.R_I[43].DblMin;
                    inst.NameReg = RegCameraData.R_I[43].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[51].Co;
                    inst.DblMax = RegCameraData.R_I[51].DblMax;
                    inst.DblMin = RegCameraData.R_I[51].DblMin;
                    inst.NameReg = RegCameraData.R_I[51].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataY_Camera8//Y
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[44].Co;
                    inst.DblMax = RegCameraData.R_I[44].DblMax;
                    inst.DblMin = RegCameraData.R_I[44].DblMin;
                    inst.NameReg = RegCameraData.R_I[44].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[52].Co;
                    inst.DblMax = RegCameraData.R_I[52].DblMax;
                    inst.DblMin = RegCameraData.R_I[52].DblMin;
                    inst.NameReg = RegCameraData.R_I[52].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataZ_Camera8//Z
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[45].Co;
                    inst.DblMax = RegCameraData.R_I[45].DblMax;
                    inst.DblMin = RegCameraData.R_I[45].DblMin;
                    inst.NameReg = RegCameraData.R_I[45].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[53].Co;
                    inst.DblMax = RegCameraData.R_I[53].DblMax;
                    inst.DblMin = RegCameraData.R_I[53].DblMin;
                    inst.NameReg = RegCameraData.R_I[53].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public RegPLC regDataR_Jamera8//R
        {
            get
            {
                RegPLC inst = new RegPLC();
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    inst.Co = RegCameraData.R_I[46].Co;
                    inst.DblMax = RegCameraData.R_I[46].DblMax;
                    inst.DblMin = RegCameraData.R_I[46].DblMin;
                    inst.NameReg = RegCameraData.R_I[46].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    inst.Co = RegCameraData.R_I[54].Co;
                    inst.DblMax = RegCameraData.R_I[54].DblMax;
                    inst.DblMin = RegCameraData.R_I[54].DblMin;
                    inst.NameReg = RegCameraData.R_I[54].NameReg.Replace("\\n", "\n");
                }
                return inst;
            }
        }
        public string regFinsihData_Camera8//数据传输完成
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//新版本协议，是连续地址
                {
                    return RegCameraData.R_I[47].NameReg.Replace("\\n", "\n");
                }
                else
                {
                    return RegCameraData.R_I[55].NameReg.Replace("\\n", "\n");
                }
            }
        }
        #endregion 相机8
        #endregion 相机数据寄存器

        #region 配置参数
        //配置参数名称
        public const int c_NumModelNameReg = 18;
        public string regModelName
        {
            get
            {
                string strReg = "";
                for (int i = 1; i < 10; i++)
                {
                    strReg += RegConfigPar.R_I[i].NameReg.Replace("\\n", "\n");
                }
                return strReg;
            }
        }
        //配置参数序号
        public string regModelNo
        {
            get
            {
                return RegConfigPar.R_I[0].NameReg.Replace("\\n", "\n");
            }
        }

        //配置参数      
        public string regConfigPar
        {
            get
            {
                string strReg = "";
                for (int i = 10; i < RegConfigPar.R_I.Count; i++)
                {
                    strReg += RegConfigPar.R_I[i].NameReg.Replace("\\n", "\n");
                }
                return strReg;
            }
        }
        #endregion 配置参数

        #region 数据寄存器
        public string regPCConnectPLC//发送信号给PLC，确认连接
        {
            get
            {
                return RegData.R_I[0].NameReg.Replace("\\n", "\n");
            }
        }

        public string regPCALarm//PC报警
        {
            get
            {
                return RegData.R_I[1].NameReg.Replace("\\n", "\n");
            }
        }
        #endregion 数据寄存器

    }   

}