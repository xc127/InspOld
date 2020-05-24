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

namespace Main
{
    partial class MainWindow
    {
        #region 定义
        //bool 
        bool BlPLCReset = false;//plc复位       
        bool BlPLCRun = false; //启动        
        bool BlPLCPause = false; //暂停   
        bool BlPLCAlarm = false; //报警       
        bool BlPLCEmergency = false; //急停 

        //int

        #endregion 定义

        #region 初始化
        //事件注册
        void LoginEvent_PLC()
        {
            try
            {
                //如果不使用DealPLC通信
                if (ParSetPLC.P_I.TypePLC_e == TypePLC_enum.Null)
                {
                    return;
                }

                //关机重启
                LogicPLC.L_I.SoftRestart_event += new TrrigerSourceAction_del(LogicPLC_Inst_SoftRestart_event);
                LogicPLC.L_I.PCShutdown_event += new TrrigerSourceAction_del(LogicPLC_Inst_PCShutdown_event);
                LogicPLC.L_I.PCRestart_event += new TrrigerSourceAction_del(LogicPLC_Inst_PCRestart_event);

                //PLC操作
                LogicPLC.L_I.PLCState_event += new TrrigerSourceAction_del(L_I_PLCState_event);
                LogicPLC.L_I.PLCAlarm_event += new TrrigerSourceAction_del(LogicPLC_Inst_PLCAlarm_event);
                LogicPLC.L_I.PLCMaterial_event += new TrrigerSourceAction_del(L_I_PLCMaterial_event);

                //型号
                LogicPLC.L_I.NewModel_event += new TrrigerSourceAction_del(LogicPLC_Inst_NewModel_event);
                LogicPLC.L_I.ChangeModel_event += new TrrigerSourceAction_del(LogicPLC_Inst_ChangeModel_event);
                LogicPLC.L_I.DelModel_event += new TrrigerSourceAction_del(LogicPLC_Inst_DelModel_event);

                //状态                ;
                LogicPLC.L_I.RobotState_event += new TrrigerSourceAction_del(LogicPLC_Inst_RobotState_event);
                LogicPLC.L_I.RestartCommunicate_event += new TrrigerSourceAction_del(L_I_RestartCommunicate_event);
                LogicPLC.L_I.VoiceState_event += new TrrigerSourceAction_del(L_I_VoiceState_event);

                //保留
                LogicPLC.L_I.Reserve1_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve1_event);
                LogicPLC.L_I.Reserve2_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve2_event);
                LogicPLC.L_I.Reserve3_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve3_event);
                LogicPLC.L_I.Reserve4_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve4_event);
                LogicPLC.L_I.Reserve5_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve5_event);
                LogicPLC.L_I.Reserve6_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve6_event);
                LogicPLC.L_I.Reserve7_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve7_event);
                LogicPLC.L_I.Reserve8_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve8_event);
                LogicPLC.L_I.Reserve9_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve9_event);
                LogicPLC.L_I.Reserve10_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve10_event);
                LogicPLC.L_I.Reserve11_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve11_event);
                LogicPLC.L_I.Reserve12_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve12_event);
                LogicPLC.L_I.Reserve13_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve13_event);
                LogicPLC.L_I.Reserve14_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve14_event);
                LogicPLC.L_I.Reserve15_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve15_event);
                LogicPLC.L_I.Reserve16_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve16_event);
                LogicPLC.L_I.Reserve17_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve17_event);
                LogicPLC.L_I.Reserve18_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve18_event);
                LogicPLC.L_I.Reserve19_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve19_event);
                LogicPLC.L_I.Reserve20_event += new TrrigerSourceAction_del(LogicPLC_Inst_Reserve20_event);

                //PLC读写状态
                LogicPLC.L_I.CommunicationState_event += new Str2Action(LogicPLC_Inst_CommunicationState_event);

                //配置参数错误
                LogicPLC.L_I.ConfigParError_event += new StrAction(LogicPLC_Inst_ConfigParError_event);

                //相机
                LogicPLC.L_I.Camera1_event += new TrrigerSourceAction_del(DealComprehensive_Camera1_event);
                //xc-0323，只触发相机1，由相机1触发234
                //LogicPLC.L_I.Camera2_event += new TrrigerSourceAction_del(DealComprehensive_Camera2_event);
                //LogicPLC.L_I.Camera3_event += new TrrigerSourceAction_del(DealComprehensive_Camera3_event);
                //LogicPLC.L_I.Camera4_event += new TrrigerSourceAction_del(DealComprehensive_Camera4_event);
                //LogicPLC.L_I.Camera5_event += new TrrigerSourceAction_del(DealComprehensive_Camera5_event);
                //LogicPLC.L_I.Camera6_event += new TrrigerSourceAction_del(DealComprehensive_Camera6_event);
                //LogicPLC.L_I.Camera7_event += new TrrigerSourceAction_del(DealComprehensive_Camera7_event);
                //LogicPLC.L_I.Camera8_event += new TrrigerSourceAction_del(DealComprehensive_Camera8_event);

                //数据超限
                LogicPLC.L_I.WriteDataOverFlow += new StrAction(L_I_WriteDataOverFlow);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void Init_PLC()
        {
            try
            {
                //如果不使用DealPLC通信
                if (ParSetPLC.P_I.TypePLC_e == TypePLC_enum.Null)
                {
                    return;
                }
                string error = "";
                if (LogicPLC.L_I.OpenPort(out error))//打开PLC通信
                {
                    ShowState("PLC连接成功！");
                    SendOpenToPLC();
                }
                else
                {
                    //显示报警窗口
                    ShowWinError_Invoke("PLC连接失败！" + error);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void SendOpenToPLC()
        {
            //如果不使用DealPLC通信
            if (ParSetPLC.P_I.TypePLC_e == TypePLC_enum.Null)
            {
                return;
            }

            LogicPLC.L_I.WriteRegData1(0, 1);
        }

        void SendCloseToPLC()
        {
            //如果不使用DealPLC通信
            if (ParSetPLC.P_I.TypePLC_e == TypePLC_enum.Null)
            {
                return;
            }

            LogicPLC.L_I.WriteRegData1(0, 0);
        }
        #endregion 初始化

        #region PC控制
        //软件重启
        void LogicPLC_Inst_SoftRestart_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                ShowState("PLC触发软件重启！");
                this.Dispatcher.Invoke(new Action(RestartSoft));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        //重启PC
        void LogicPLC_Inst_PCRestart_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                ShowState("PLC触发PC重启！");
                this.Dispatcher.Invoke(new Action(RestartPC));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //关机PC
        void LogicPLC_Inst_PCShutdown_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                ShowState("PLC触发PC关机！");
                this.Dispatcher.Invoke(new Action(ShutDownPC));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion PC控制

        #region PLC状态
        void L_I_PLCState_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                if (i > 0)
                {
                    MainCom.M_I.StateMachine_e = (StateMachine_enum)i;
                    ShowStateMachine();
                }
            }
            catch (Exception ex)
            {

            }
        }

        //报警
        void LogicPLC_Inst_PLCAlarm_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {

                ShowState("设备发送报警信息!");
            }
            catch (Exception ex)
            {

            }

        }

        //PLC物料信息
        void L_I_PLCMaterial_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {

                ShowState("设备发送物料信息!");
            }
            catch (Exception ex)
            {

            }
        }
        #endregion PLC状态

        #region 语音信息
        void L_I_VoiceState_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                ShowVoice(i);//显示语音
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 语音信息

        #region 型号
        /// <summary>
        /// 响应PLC新建型号的事件
        /// </summary>
        void LogicPLC_Inst_NewModel_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                switch (i)
                {
                    case 1://新建型号
                        PLCNewModel();
                        break;

                    case 2:
                        PLCRefreshPar();//PLC触发更新产品参数
                        break;
                }
                LogicPLC.L_I.WriteRegData1(3, 1);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// PLC触发新建型号
        /// </summary>
        void PLCNewModel()
        {
            try
            {
                ShowState("PLC触发新建型号");

                //判断是否存在非法字符
                int num = ParLogicPLC.P_I.NameModel.IndexOfAny(Path.GetInvalidFileNameChars());
                if (num > -1)
                {
                    ShowWinError_Invoke("新建型号失败，名称中含有非法字符：\\ / : * ？ \" < > |");
                    return;
                }

                #region 将PLC中读取的参数复制到配置文件类
                ReadParProductFromPLC();
                ReadPosPhotoFromPLC();
                #endregion 将DealPLC中读取的参数复制到配置文件类

                ComConfigPar.C_I.PathOldConfigIni = ComConfigPar.C_I.PathConfigIni;
                //新建文件路径
                ComConfigPar.C_I.PathConfigIni = ComValue.c_PathPar + ParLogicPLC.P_I.NameModel + "\\Product.ini";

                //如果新建的型号相同
                if (ComConfigPar.C_I.NameModel == ParLogicPLC.P_I.NameModel)
                {
                    g_BlModelSame = true;
                }
                else
                {
                    g_BlModelSame = false;
                    ComConfigPar.C_I.NameModel = ParLogicPLC.P_I.NameModel;//新的型号
                }

                //换型
                if (NewModel())
                {
                    ShowState("PLC触发新建型号");
                }
                else
                {
                    ShowAlarm("PLC触发新建型号失败");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// PLC触发更新数据
        /// </summary>
        void PLCRefreshPar()
        {
            try
            {
                ShowState("PLC触发更新参数数据");
                //如果新建的型号相同
                if (ComConfigPar.C_I.NameModel != ParLogicPLC.P_I.NameModel)
                {
                    MessageBox.Show("当前名称型号参数不存在，请新建型号！");
                    return;
                }
                #region 将PLC中读取的参数复制到配置文件类
                ReadParProductFromPLC();
                ReadPosPhotoFromPLC();
                #endregion 将DealPLC中读取的参数复制到配置文件类

                //换型
                if (RefreshPar())
                {
                    ShowState("PLC触发更新产品参数");
                }
                else
                {
                    ShowAlarm("PLC触发更新产品参数失败");
                }
                //每次换型时，需要写入PLC的值
                WritePLCModelPar();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 读取产品参数
        /// </summary>
        void ReadParProductFromPLC()
        {
            try
            {
                ParConfigPar.P_I.No = ParLogicPLC.P_I.intNo;
                for (int i = 0; i < ParLogicPLC.P_I.ParProduct_L.Count; i++)
                {
                    ParConfigPar.P_I.ParProduct_L[i].DblValue = ParLogicPLC.P_I.ParProduct_L[i].DblValue;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 读取拍照位置参数
        /// </summary>
        void ReadPosPhotoFromPLC()
        {
            try
            {
                for (int i = 0; i < ParCameraWork.NumCamera; i++)
                {
                    int j = i * 4 + RegConfigPar.R_I.NumRegSet;
                    ParConfigPar.P_I.PosPhoto_L[i].X = ParLogicPLC.P_I.ParProduct_L[j].DblValue;
                    ParConfigPar.P_I.PosPhoto_L[i].Y = ParLogicPLC.P_I.ParProduct_L[j + 1].DblValue;
                    ParConfigPar.P_I.PosPhoto_L[i].Z = ParLogicPLC.P_I.ParProduct_L[j + 2].DblValue;
                    ParConfigPar.P_I.PosPhoto_L[i].R = ParLogicPLC.P_I.ParProduct_L[j + 3].DblValue;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }


        //换型
        void LogicPLC_Inst_ChangeModel_event(TriggerSource_enum trrigerSource_e, int i)
        {
            ShowState("切换型号成功");
        }

        /// <summary>
        /// 删除配置参数
        /// </summary>
        void LogicPLC_Inst_DelModel_event(TriggerSource_enum trrigerSource_e, int i)
        {
            ShowState("删除型号成功");
        }

        //配置参数错误
        void LogicPLC_Inst_ConfigParError_event(string str)
        {
            try
            {
                ShowAlarm("配置参数错误，序号：" + str);
                //PC报警
                LogicPLC.L_I.PCAlarm();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 型号

        #region 重启外部通信
        void L_I_RestartCommunicate_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                switch (i)
                {
                    case 1:
                        RobotReStart();
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 重启外部通信

        #region PLC状态
        //设备状态
        void LogicPLC_Inst_PLCState_event(TriggerSource_enum trrigerSource_e, int intState)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //机器人状态
        void LogicPLC_Inst_RobotState_event(TriggerSource_enum trrigerSource_e, int intState)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="color"></param>
        void LogicPLC_Inst_CommunicationState_event(string str, string color)
        {
            if (str == "循环读取PLC监控寄存器失败！")
            {
                ShowState(str);
            }
            else
            {
                ShowAlarm(str);
            }
        }
        #endregion DealPLC状态

        #region 数据超限
        void L_I_WriteDataOverFlow(string str)
        {
            ShowAlarm("PLC输出数据超出范围");

            LogicPLC.L_I.PCAlarm();
        }
        #endregion 数据超限

        #region 关闭PLC
        void Close_PLC()
        {
            try
            {
                //如果不使用DealPLC通信
                if (ParSetPLC.P_I.TypePLC_e == TypePLC_enum.Null)
                {
                    return;
                }
                //关闭PLC通信
                LogicPLC.L_I.ClosePLC();

                //注销事件
                EventLogout_PLC();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        //事件注销
        void EventLogout_PLC()
        {
            //如果不使用DealPLC通信
            if (ParSetPLC.P_I.TypePLC_e == TypePLC_enum.Null)
            {
                return;
            }
            //关机重启
            LogicPLC.L_I.SoftRestart_event -= new TrrigerSourceAction_del(LogicPLC_Inst_SoftRestart_event);
            LogicPLC.L_I.PCShutdown_event -= new TrrigerSourceAction_del(LogicPLC_Inst_PCShutdown_event);
            LogicPLC.L_I.PCRestart_event -= new TrrigerSourceAction_del(LogicPLC_Inst_PCRestart_event);

            //DealPLC操作
            LogicPLC.L_I.PLCState_event -= new TrrigerSourceAction_del(L_I_PLCState_event);
            LogicPLC.L_I.PLCAlarm_event -= new TrrigerSourceAction_del(LogicPLC_Inst_PLCAlarm_event);
            LogicPLC.L_I.PLCMaterial_event -= new TrrigerSourceAction_del(L_I_PLCMaterial_event);

            //型号
            LogicPLC.L_I.NewModel_event -= new TrrigerSourceAction_del(LogicPLC_Inst_NewModel_event);
            LogicPLC.L_I.ChangeModel_event -= new TrrigerSourceAction_del(LogicPLC_Inst_ChangeModel_event);
            LogicPLC.L_I.DelModel_event -= new TrrigerSourceAction_del(LogicPLC_Inst_DelModel_event);

            //状态
            LogicPLC.L_I.RobotState_event -= new TrrigerSourceAction_del(LogicPLC_Inst_RobotState_event);
            LogicPLC.L_I.RestartCommunicate_event -= new TrrigerSourceAction_del(L_I_RestartCommunicate_event);
            LogicPLC.L_I.VoiceState_event -= new TrrigerSourceAction_del(L_I_VoiceState_event);

            //DealPLC读写状态
            LogicPLC.L_I.CommunicationState_event -= new Str2Action(LogicPLC_Inst_CommunicationState_event);
        }
        #endregion 关闭PLC

    }
}
