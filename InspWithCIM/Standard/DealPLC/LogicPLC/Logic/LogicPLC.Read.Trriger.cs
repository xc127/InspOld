using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Common;
using BasicClass;
using DealPLC;
using DealConfigFile;

namespace DealPLC
{
    public partial class LogicPLC
    {
        #region 定义
        #region Old
        /// <summary>
        /// 存储触发时上一次的值
        /// </summary>
        public int OldSoftRestart = 0;//软件重启
        public int OldShutdown = 0;//关机
        public int OldRestart = 0;//重启        
        public int OldHeartBeat = 0;//心跳  
        public int OldPLCReset = 0;//复位
        public int OldPLCRun = 0;//启动
        public int OldPLCPause = 0;//暂停
        public int OldPLCAlarm = 0;//报警
        public int OldPLCEmergency = 0;//急停

        public int OldNewModel = 0;//增加新的型号    
        public int OldChangeModel = 0;//切换型号
        public int OldDelModel = 0;//删除型号
        public int OldModel = 0;//型号
        public int OldRestartCommunicate = 0;

        #region 触发保留
        public int OldReserve1 = 0;
        public int OldReserve2 = 0;
        public int OldReserve3 = 0;
        public int OldReserve4 = 0;
        public int OldReserve5 = 0;
        public int OldReserve6 = 0;
        public int OldReserve7 = 0;
        public int OldReserve8 = 0;
        public int OldReserve9 = 0;
        public int OldReserve10 = 0;
        public int OldReserve11 = 0;
        public int OldReserve12 = 0;
        public int OldReserve13 = 0;
        public int OldReserve14 = 0;
        public int OldReserve15 = 0;
        public int OldReserve16 = 0;
        public int OldReserve17 = 0;
        public int OldReserve18 = 0;
        public int OldReserve19 = 0;
        public int OldReserve20 = 0;
        #endregion 触发保留

        //相机
        public int OldCamera1 = 0;
        public int OldCamera2 = 0;
        public int OldCamera3 = 0;
        public int OldCamera4 = 0;
        public int OldCamera5 = 0;
        public int OldCamera6 = 0;
        public int OldCamera7 = 0;
        public int OldCamera8 = 0;
        #endregion Old

        #region 寄存器中的值
        public double XPos = 0;
        public double YPos = 0;
        public double ZPos = 0;
        public double RPos = 0;

        public double[] RegReserveData
        {
            get
            {
                double[] value = null;
                if (RegMonitor.R_I.NumRegSet > 0)
                {
                    value = new double[RegMonitor.R_I.NumRegSet];
                }
                return value;
            }
        }

        public double RegReserveData1
        {
            get
            {
                try
                {
                    return RegReserveData[0];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData2
        {
            get
            {
                try
                {
                    return RegReserveData[1];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData3
        {
            get
            {
                try
                {
                    return RegReserveData[2];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData4
        {
            get
            {
                try
                {
                    return RegReserveData[3];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData5
        {
            get
            {
                try
                {
                    return RegReserveData[4];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData6
        {
            get
            {
                try
                {
                    return RegReserveData[5];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData7
        {
            get
            {
                try
                {
                    return RegReserveData[6];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData8
        {
            get
            {
                try
                {
                    return RegReserveData[7];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData9
        {
            get
            {
                try
                {
                    return RegReserveData[8];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData10
        {
            get
            {
                try
                {
                    return RegReserveData[9];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData11
        {
            get
            {
                try
                {
                    return RegReserveData[10];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData12
        {
            get
            {
                try
                {
                    return RegReserveData[11];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData13
        {
            get
            {
                try
                {
                    return RegReserveData[12];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData14
        {
            get
            {
                try
                {
                    return RegReserveData[13];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData15
        {
            get
            {
                try
                {
                    return RegReserveData[14];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData16
        {
            get
            {
                try
                {
                    return RegReserveData[15];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData17
        {
            get
            {
                try
                {
                    return RegReserveData[16];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData18
        {
            get
            {
                try
                {
                    return RegReserveData[17];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData19
        {
            get
            {
                try
                {
                    return RegReserveData[18];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }
        public double RegReserveData20
        {
            get
            {
                try
                {
                    return RegReserveData[19];
                }
                catch (Exception ex)
                {
                    return double.MaxValue;
                }
            }
        }


        /// <summary>
        /// 输出bool型的变量
        /// </summary>
        public bool BlRegReserveData1
        {
            get
            {
                if (RegReserveData1 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData2
        {
            get
            {
                if (RegReserveData2 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData3
        {
            get
            {
                if (RegReserveData3 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData4
        {
            get
            {
                if (RegReserveData4 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData5
        {
            get
            {
                if (RegReserveData5 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData6
        {
            get
            {
                if (RegReserveData6 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData7
        {
            get
            {
                if (RegReserveData7 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData8
        {
            get
            {
                if (RegReserveData8 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData9
        {
            get
            {
                if (RegReserveData9 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData10
        {
            get
            {
                if (RegReserveData10 > 0)
                {
                    return true;
                }
                return false;
            }
        }


        public bool BlRegReserveData11
        {
            get
            {
                if (RegReserveData11 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData12
        {
            get
            {
                if (RegReserveData12 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData13
        {
            get
            {
                if (RegReserveData13 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData14
        {
            get
            {
                if (RegReserveData14 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData15
        {
            get
            {
                if (RegReserveData15 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData16
        {
            get
            {
                if (RegReserveData16 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData17
        {
            get
            {
                if (RegReserveData17 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData18
        {
            get
            {
                if (RegReserveData18 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData19
        {
            get
            {
                if (RegReserveData19 > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool BlRegReserveData20
        {
            get
            {
                if (RegReserveData20 > 0)
                {
                    return true;
                }
                return false;
            }
        }

        #endregion 寄存器中的值

        #region 相机触发
        bool BlCamera1 = false;
        bool BlCamera2 = false;
        bool BlCamera3 = false;
        bool BlCamera4 = false;
        bool BlCamera5 = false;
        bool BlCamera6 = false;
        bool BlCamera7 = false;
        bool BlCamera8 = false;

        int NoCamera1 = 0;
        int NoCamera2 = 0;
        int NoCamera3 = 0;
        int NoCamera4 = 0;
        int NoCamera5 = 0;
        int NoCamera6 = 0;
        int NoCamera7 = 0;
        int NoCamera8 = 0;
        #endregion 相机触发
        #endregion 定义

        #region 事件定义
        //PC 
        public event TrrigerSourceAction_del SoftRestart_event; //重启软件
        public event TrrigerSourceAction_del PCShutdown_event;  //关机        
        public event TrrigerSourceAction_del PCRestart_event;//重启电脑     

        //PLC操作
        public event TrrigerSourceAction_del PLCState_event;//设备运行状态      
        public event TrrigerSourceAction_del PLCAlarm_event;//设备报警信息      
        public event TrrigerSourceAction_del PLCMaterial_event;//设备物料状态
        public event TrrigerSourceAction_del RobotState_event;//机器人状态

        //Model
        public event TrrigerSourceAction_del NewModel_event;  //增加型号        
        public event TrrigerSourceAction_del ChangeModel_event;//换型
        public event TrrigerSourceAction_del DelModel_event;//删除型号

        //Restart
        public event TrrigerSourceAction_del RestartCommunicate_event;//重启外部通信

        //State
        public event TrrigerSourceAction_del VoiceState_event;//语音状态

        //TypeRun
        public event TrrigerSourceAction_del TypeRun_event;//设备运行模式

        #region 触发保留
        public event TrrigerSourceAction_del Reserve1_event;
        public event TrrigerSourceAction_del Reserve2_event;
        public event TrrigerSourceAction_del Reserve3_event;
        public event TrrigerSourceAction_del Reserve4_event;
        public event TrrigerSourceAction_del Reserve5_event;
        public event TrrigerSourceAction_del Reserve6_event;
        public event TrrigerSourceAction_del Reserve7_event;
        public event TrrigerSourceAction_del Reserve8_event;
        public event TrrigerSourceAction_del Reserve9_event;
        public event TrrigerSourceAction_del Reserve10_event;
        public event TrrigerSourceAction_del Reserve11_event;
        public event TrrigerSourceAction_del Reserve12_event;
        public event TrrigerSourceAction_del Reserve13_event;
        public event TrrigerSourceAction_del Reserve14_event;
        public event TrrigerSourceAction_del Reserve15_event;
        public event TrrigerSourceAction_del Reserve16_event;
        public event TrrigerSourceAction_del Reserve17_event;
        public event TrrigerSourceAction_del Reserve18_event;
        public event TrrigerSourceAction_del Reserve19_event;
        public event TrrigerSourceAction_del Reserve20_event;
        #endregion 触发保留


        //PLC读写状态
        public event Str2Action CommunicationState_event;

        //配置参数不正确
        public event StrAction ConfigParError_event;

        //相机
        public event TrrigerSourceAction_del Camera1_event;//相机1
        public event TrrigerSourceAction_del Camera2_event;//相机2
        public event TrrigerSourceAction_del Camera3_event;//相机3
        public event TrrigerSourceAction_del Camera4_event;//相机4
        public event TrrigerSourceAction_del Camera5_event;//相机5
        public event TrrigerSourceAction_del Camera6_event;//相机6
        public event TrrigerSourceAction_del Camera7_event;//相机7
        public event TrrigerSourceAction_del Camera8_event;//相机8
        #endregion 事件定义

        #region 触发
        /// <summary>
        /// 通过输入的数组值，分解寄存器的值，进行触发处理,监控寄存器和相机都只保留一个
        /// </summary>
        void DealTrrigerSingle(int[] intValue)
        {
            try
            {
                #region PLC控制PC
                SoftRestart(intValue[0]);
                PCShutdown(intValue[1]);
                PCRestart(intValue[2]);
                HeartBeat(intValue[3]);
                #endregion PC控制

                #region PLC操作
                PLCState(intValue[4]);//设备运行状态
                PLCAlarm(intValue[15]);
                PLCMaterial(intValue[6]);
                RobotState(intValue[7]);
                #endregion PLC操作

                #region 参数设置
                NewModel(intValue[8]);//新建型号
                ChangeModel(intValue[9]);//切换型号
                DelModel(intValue[10]);//删除
                #endregion 参数设置

                #region 操作和状态,32位但是只使用低位
                RestartCommunicate(intValue[11]);//重启外部通信           
                VoiceState(intValue[12]);//语音信息
                //保留一位
                #endregion 操作和状态

                //根据相机个数确定寄存器数量和序号
                int numEndCamera = 0;
                switch (ParCameraWork.NumCamera)
                {
                    case 1:
                        numEndCamera = 34;
                        break;
                    case 2:
                        numEndCamera = 35;
                        break;
                    case 3:
                        numEndCamera = 36;
                        break;
                    case 4:
                        numEndCamera = 37;
                        break;
                    case 5:
                        numEndCamera = 38;
                        break;
                    case 6:
                        numEndCamera = 39;
                        break;

                    case 7:
                        numEndCamera = 40;
                        break;

                    case 8:
                        numEndCamera = 41;
                        break;
                }
                //先读值，再触发
                #region 读取PLC寄存器值
                if (RegMonitor.R_I.NumRegSet > 0)
                {
                    ReserveXPos(intValue[numEndCamera + 1], intValue[numEndCamera + 2]);
                }
                if (RegMonitor.R_I.NumRegSet > 1)
                {
                    ReserveYPos(intValue[numEndCamera + 3], intValue[numEndCamera + 4]);
                }
                if (RegMonitor.R_I.NumRegSet > 2)
                {
                    ReserveZPos(intValue[numEndCamera + 5], intValue[numEndCamera + 6]);
                }
                if (RegMonitor.R_I.NumRegSet > 3)
                {
                    ReserveRPos(intValue[numEndCamera + 7], intValue[numEndCamera + 8]);
                }
                if (RegMonitor.R_I.NumRegSet > 4)
                {
                    ReserveData(numEndCamera + 9, intValue);
                }
                #endregion 读取PLC寄存器值

                #region 触发保留
                Reserve1(intValue[14]);
                Reserve2(intValue[15]);
                Reserve3(intValue[16]);
                Reserve4(intValue[17]);
                Reserve5(intValue[18]);
                Reserve6(intValue[19]);
                Reserve7(intValue[20]);
                Reserve8(intValue[21]);
                Reserve9(intValue[22]);
                Reserve10(intValue[23]);
                Reserve11(intValue[24]);
                Reserve12(intValue[25]);
                Reserve13(intValue[26]);
                Reserve14(intValue[27]);
                Reserve15(intValue[28]);
                Reserve16(intValue[29]);
                Reserve17(intValue[30]);
                Reserve18(intValue[31]);
                Reserve19(intValue[32]);
                Reserve20(intValue[33]);
                #endregion 触发保留

                #region 相机
                switch (ParCameraWork.NumCamera)
                {
                    case 1:
                        Camera1(intValue[34]);
                        break;
                    case 2:
                        Camera1(intValue[34]);
                        Camera2(intValue[35]);
                        break;
                    case 3:
                        Camera1(intValue[34]);
                        Camera2(intValue[35]);
                        Camera3(intValue[36]);
                        break;
                    case 4:
                        Camera1(intValue[34]);
                        Camera2(intValue[35]);
                        Camera3(intValue[36]);
                        Camera4(intValue[37]);
                        break;
                    case 5:
                        Camera1(intValue[34]);
                        Camera2(intValue[35]);
                        Camera3(intValue[36]);
                        Camera4(intValue[37]);
                        Camera5(intValue[38]);
                        break;
                    case 6:
                        Camera1(intValue[34]);
                        Camera2(intValue[35]);
                        Camera3(intValue[36]);
                        Camera4(intValue[37]);
                        Camera5(intValue[38]);
                        Camera6(intValue[39]);
                        break;

                    case 7:
                        Camera1(intValue[34]);
                        Camera2(intValue[35]);
                        Camera3(intValue[36]);
                        Camera4(intValue[37]);
                        Camera5(intValue[38]);
                        Camera6(intValue[39]);
                        Camera7(intValue[40]);
                        break;

                    case 8:
                        Camera1(intValue[34]);
                        Camera2(intValue[35]);
                        Camera3(intValue[36]);
                        Camera4(intValue[37]);
                        Camera5(intValue[38]);
                        Camera6(intValue[39]);
                        Camera7(intValue[40]);
                        Camera8(intValue[41]);
                        break;
                }
                #endregion 相机
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 新协议
        /// </summary>
        void DealTrrigerSingle_New(int[] intValue)
        {
            try
            {
                #region PLC控制PC
                HeartBeat(intValue[0]);
                #endregion PC控制

                #region PLC操作
                PLCState(intValue[1]);//设备运行状态
                PLCAlarm(intValue[2]);
                #endregion PLC操作

                #region 参数设置
                Model(intValue[3]);//型号          
                #endregion 参数设置

                #region 其他
                RobotState(intValue[4]);
                PLCMaterial(intValue[5]);
                #endregion 其他

                //先读值，再触发
                #region 读取PLC寄存器值
                ReserveData(RegMonitor.R_I.NumTrigger, intValue);
                #endregion 读取PLC寄存器值

                #region 相机
                Camera1(intValue[6]);
                Camera2(intValue[7]);
                Camera3(intValue[8]);
                Camera4(intValue[9]);
                Camera5(intValue[10]);
                Camera6(intValue[11]);
                Camera7(intValue[12]);
                Camera8(intValue[13]);
                #endregion 相机

                #region 触发保留
                if (RegMonitor.R_I.NumRegSet > 0)
                {
                    Reserve1(intValue[14]);
                }
                if (RegMonitor.R_I.NumRegSet > 1)
                {
                    Reserve2(intValue[15]);
                }
                if (RegMonitor.R_I.NumRegSet > 2)
                {
                    Reserve3(intValue[16]);
                }
                if (RegMonitor.R_I.NumRegSet > 3)
                {
                    Reserve4(intValue[17]);
                }
                if (RegMonitor.R_I.NumRegSet > 4)
                {
                    Reserve5(intValue[18]);
                }
                if (RegMonitor.R_I.NumRegSet > 5)
                {
                    Reserve6(intValue[19]);
                }
                if (RegMonitor.R_I.NumRegSet > 6)
                {
                    Reserve7(intValue[20]);
                }
                if (RegMonitor.R_I.NumRegSet > 7)
                {
                    Reserve8(intValue[21]);
                }
                if (RegMonitor.R_I.NumRegSet > 8)
                {
                    Reserve9(intValue[22]);
                }
                if (RegMonitor.R_I.NumRegSet > 9)
                {
                    Reserve10(intValue[23]);
                }
                if (RegMonitor.R_I.NumRegSet > 10)
                {
                    Reserve11(intValue[24]);
                }
                if (RegMonitor.R_I.NumRegSet > 11)
                {
                    Reserve12(intValue[25]);
                }
                if (RegMonitor.R_I.NumRegSet > 12)
                {
                    Reserve13(intValue[26]);
                }
                if (RegMonitor.R_I.NumRegSet > 13)
                {
                    Reserve14(intValue[27]);
                }
                if (RegMonitor.R_I.NumRegSet > 14)
                {
                    Reserve15(intValue[28]);
                }
                if (RegMonitor.R_I.NumRegSet > 15)
                {
                    Reserve16(intValue[29]);
                }
                if (RegMonitor.R_I.NumRegSet > 16)
                {
                    Reserve17(intValue[30]);
                }
                if (RegMonitor.R_I.NumRegSet > 17)
                {
                    Reserve18(intValue[31]);
                }
                if (RegMonitor.R_I.NumRegSet > 18)
                {
                    Reserve19(intValue[32]);
                }
                if (RegMonitor.R_I.NumRegSet > 19)
                {
                    Reserve20(intValue[33]);
                }
                #endregion 触发保留
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发

        #region PLC控制PC
        /// <summary>
        /// 软件重启
        /// </summary>
        /// <param name="intValue"></param>
        void SoftRestart(int intValue)
        {
            try
            {
                if (intValue > OldSoftRestart)
                {
                    SoftRestart_event(TriggerSource_enum.PLC, intValue);
                    Thread.Sleep(100);
                    ClearPLC(ParSetPLC.P_I.regSoftReStart);

                }
                OldSoftRestart = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// PC关机
        /// </summary>
        /// <param name="intValue"></param>
        void PCShutdown(int intValue)
        {
            try
            {
                if (intValue > OldShutdown)
                {
                    PCShutdown_event(TriggerSource_enum.PLC, intValue);
                    Thread.Sleep(100);
                    ClearPLC(ParSetPLC.P_I.regPCShutDown);
                }
                OldShutdown = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 电脑重启
        /// </summary>
        /// <param name="intValue"></param>
        void PCRestart(int intValue)
        {
            try
            {
                if (intValue > OldRestart)
                {
                    PCRestart_event(TriggerSource_enum.PLC, intValue);
                    Thread.Sleep(100);
                    ClearPLC(ParSetPLC.P_I.regPCReStart);
                }
                OldRestart = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 和PLC进行心跳,只要监测到PLC为1
        /// </summary>
        /// <param name="intValue"></param>
        void HeartBeat(int intValue)
        {
            try
            {
                if (intValue > OldHeartBeat)//上升沿清除
                {
                    new Task(HeartBeatClear).Start();//新建线程是为了不占用读取时间
                }
                OldHeartBeat = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void HeartBeatClear()
        {
            try
            {
                //Thread.Sleep(300);
                ClearPLC(ParSetPLC.P_I.regHeartBeat);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion PLC控制PC

        #region PLC操作
        /// <summary>
        /// 设备状态
        /// </summary>
        /// <param name="intValue"></param>
        void PLCState(int intValue)
        {
            try
            {
                if (intValue != OldPLCReset)
                {
                    PLCState_event(TriggerSource_enum.PLC, intValue);
                }
                OldPLCReset = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        ///  PLC报警
        /// </summary>
        /// <param name="intValue"></param>
        void PLCAlarm(int intValue)
        {
            try
            {
                if (intValue > OldPLCAlarm)
                {
                    PLCAlarm_event(TriggerSource_enum.PLC, intValue);
                    Thread.Sleep(100);
                    ClearPLC(ParSetPLC.P_I.regPLCAlarm);
                }
                OldPLCAlarm = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 设备物料信息
        /// </summary>
        /// <param name="intValue"></param>
        void PLCMaterial(int intValue)
        {
            try
            {
                if (intValue > OldPLCRun)
                {
                    PLCMaterial_event(TriggerSource_enum.PLC, intValue);
                    Thread.Sleep(100);
                    ClearPLC(ParSetPLC.P_I.regPLCMaterial);
                }
                OldPLCRun = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        ///机器人状态
        /// </summary>
        /// <param name="intValue"></param>
        void RobotState(int intValue)
        {
            try
            {
                if (intValue > OldPLCPause)
                {
                    //RobotState_event(TriggerSource_enum.PLC, intValue);
                    RestartCommunicate_event(TriggerSource_enum.PLC, intValue);
                    Thread.Sleep(100);
                    ClearPLC(ParSetPLC.P_I.regRobotState);
                }
                OldPLCPause = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion PLC控制

        #region 型号
        /// <summary>
        /// 增加新的型号
        /// </summary>
        void NewModel(int intValue)
        {
            try
            {
                if (intValue > OldNewModel)
                {
                    Thread.Sleep(100);
                    ReadModelNo();//读取型号序号
                    ReadModelName();//读取型号名称                    
                    ReadConfigPar();//读取配置参数

                    NewModel_event(TriggerSource_enum.PLC, intValue);//触发事件
                    Thread.Sleep(100);
                    ClearPLC(ParSetPLC.P_I.regNewModel);
                }
                OldNewModel = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 切换型号
        void ChangeModel(int intValue)
        {
            try
            {
                if (intValue > OldNewModel)
                {
                    ChangeModel_event(TriggerSource_enum.PLC, intValue);
                    Thread.Sleep(100);
                    ClearPLC(ParSetPLC.P_I.regChangeModel);
                }
                OldNewModel = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 删除型号
        /// </summary>
        void DelModel(int intValue)
        {
            try
            {
                if (intValue > OldDelModel)
                {
                    DelModel_event(TriggerSource_enum.PLC, intValue);
                    Thread.Sleep(100);
                    ClearPLC(ParSetPLC.P_I.regDelModel);
                }
                OldDelModel = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 精简版本协议综合处理
        /// </summary>
        void Model(int intValue)
        {
            try
            {
                if (intValue != OldModel
                    && intValue > 0)
                {
                    switch (intValue)
                    {
                        case 1:
                            Thread.Sleep(100);
                            ReadModelNo();//读取型号序号
                            ReadModelName();//读取型号名称                    
                            ReadConfigPar();//读取配置参数

                            NewModel_event(TriggerSource_enum.PLC, intValue);//触发事件
                            break;

                        case 2:
                            ChangeModel_event(TriggerSource_enum.PLC, intValue);
                            break;

                        case 3:
                            DelModel_event(TriggerSource_enum.PLC, intValue);
                            break;
                    }

                    Thread.Sleep(100);
                    ClearPLC(ParSetPLC.P_I.regNewModel);
                }
                OldModel = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 读取型号名称
        /// </summary>
        void ReadModelName()
        {
            try
            {
                ParLogicPLC.P_I.NameModel = "";
                int[] intValue = new int[ParSetPLC.c_NumModelNameReg];
                if (ReadBlockReg(ParSetPLC.P_I.regModelName, ParSetPLC.c_NumModelNameReg, "ReadModelName", out intValue))
                {
                    for (int i = 0; i < 18; i++)
                    {
                        char ch1 = (char)(intValue[i] % 256);
                        char ch2 = (char)(intValue[i] / 256);

                        ParLogicPLC.P_I.NameModel += ch1.ToString() + ch2.ToString();
                    }
                    ParLogicPLC.P_I.NameModel = (ParLogicPLC.P_I.NameModel.Replace("\0", "").Replace(" ", "")).Trim();
                }
                else
                {
                    CommunicationState_event("读取配置名称失败！", "red");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 读取配置文件序号
        /// </summary>
        void ReadModelNo()
        {
            try
            {
                int[] intValue = new int[2];
                if (ReadBlockReg(ParSetPLC.P_I.regModelNo, 2, "ReadModelNo", out intValue))
                {
                    if (intValue[0] != 0)
                    {
                        ParLogicPLC.P_I.intNo = intValue[0];
                    }
                    else if (intValue[1] != 0)
                    {
                        ParLogicPLC.P_I.intNo = intValue[1];
                    }
                }
                else
                {
                    CommunicationState_event("读取配置序号失败！", "red");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 读取配置参数
        /// </summary>
        void ReadConfigPar()
        {
            try
            {
                int NumReg = RegConfigPar.R_I.NumReg - 10;//产品参数和拍照位置
                int[] intValue = new int[2 * NumReg];
                bool BlValueRight = false;
                string strErrorNum = "";//错误参数序号
                if (ReadBlockReg(ParSetPLC.P_I.regConfigPar, NumReg * 2, "ReadConfigPar", out intValue))
                {
                    try
                    {
                        ParLogicPLC.P_I.ParProduct_L.Clear();
                        for (int i = 10; i < RegConfigPar.R_I.Reg_L.Count; i++)
                        {
                            ParLogicPLC.P_I.ParProduct_L.Add(new ParProduct()
                            {
                                DblMax = RegConfigPar.R_I.Reg_L[i].DblMax,
                                DblMin = RegConfigPar.R_I.Reg_L[i].DblMin,
                                Annotation = RegConfigPar.R_I.Reg_L[i].Annotation,
                                Co = RegConfigPar.R_I.Reg_L[i].Co,
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.L_I.WriteError("LogicPLC", ex);
                    }
                    for (int i = 0; i < NumReg; i++)
                    {
                        double dblValue = (double)(ConvertReadData(intValue[2 * i], intValue[2 * i + 1])) * ParLogicPLC.P_I.ParProduct_L[i].Co;

                        //判断数据范围是否正确
                        if (dblValue < ParLogicPLC.P_I.ParProduct_L[i].DblMax
                            && dblValue > ParLogicPLC.P_I.ParProduct_L[i].DblMin)
                        {
                            ParLogicPLC.P_I.ParProduct_L[i].BlValueRight = true;
                        }
                        else
                        {
                            BlValueRight = true;//有数据不正确
                            strErrorNum += i.ToString() + ",";
                            ParLogicPLC.P_I.ParProduct_L[i].BlValueRight = false;
                        }
                        ParLogicPLC.P_I.ParProduct_L[i].DblValue = dblValue;
                    }
                    //如果有数据不正确
                    if (BlValueRight)
                    {
                        ConfigParError_event(strErrorNum);
                    }
                }
                else
                {
                    CommunicationState_event("读取配置参数失败！", "red");
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 型号

        #region 重启通信
        void RestartCommunicate(int intValue)
        {
            try
            {
                if (intValue > OldRestartCommunicate)
                {
                    RestartCommunicate_event(TriggerSource_enum.PLC, intValue);
                    Thread.Sleep(100);
                    ClearPLC(ParSetPLC.P_I.regRestartCommunicate);
                }
                OldRestartCommunicate = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 重启通信

        #region 语音信息
        /// <summary>
        ///
        /// </summary>
        /// <param name="intValue"></param>
        void VoiceState(int intValue)
        {
            try
            {
                if (intValue != 0)
                {
                    VoiceState_event(TriggerSource_enum.PLC, intValue);
                    Thread.Sleep(100);
                    int[] intClearValue = new int[2];
                    ClearPLC(ParSetPLC.P_I.regVoice);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 语音信息

        #region 触发保留

        /// <summary>
        /// 保留触发1
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve1(int intValue)
        {
            try
            {
                if (intValue != OldReserve1
                    && intValue != 0)
                {
                    new Task(TrrigerReserve1, intValue).Start();
                    ClearPLC(RegMonitor.R_I[14]);
                }
                OldReserve1 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve1(object intValue)
        {
            try
            {
                Reserve1_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
        }

        /// <summary>
        ///保留触发2
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve2(int intValue)
        {
            try
            {
                if (intValue != OldReserve2
                    && intValue != 0)
                {
                    new Task(TrrigerReserve2, intValue).Start();
                    ClearPLC(RegMonitor.R_I[15]);
                }
                OldReserve2 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve2(object intValue)
        {
            try
            {
                Reserve2_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 保留触发3
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve3(int intValue)
        {
            try
            {
                if (intValue != OldReserve3
                    && intValue != 0)
                {
                    new Task(TrrigerReserve3, intValue).Start();
                    ClearPLC(RegMonitor.R_I[16]);
                }
                OldReserve3 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve3(object intValue)
        {
            try
            {
                Reserve3_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 保留触发4
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve4(int intValue)
        {
            try
            {
                if (intValue != OldReserve4
                    && intValue != 0)
                {
                    new Task(TrrigerReserve4, intValue).Start();
                    ClearPLC(RegMonitor.R_I[17]);
                }
                OldReserve4 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve4(object intValue)
        {
            try
            {
                Reserve4_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 保留触发5
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve5(int intValue)
        {
            try
            {
                if (intValue != OldReserve5
                    && intValue != 0)
                {
                    new Task(TrrigerReserve5, intValue).Start();
                    ClearPLC(RegMonitor.R_I[18]);
                }
                OldReserve5 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve5(object intValue)
        {
            try
            {
                Reserve5_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 保留触发6
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve6(int intValue)
        {
            try
            {
                if (intValue != OldReserve6
                    && intValue != 0)
                {
                    new Task(TrrigerReserve6, intValue).Start();
                    ClearPLC(RegMonitor.R_I[19]);
                }
                OldReserve6 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve6(object intValue)
        {
            try
            {
                Reserve6_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 保留触发7
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve7(int intValue)
        {
            try
            {
                if (intValue != OldReserve7
                    && intValue != 0)
                {
                    new Task(TrrigerReserve7, intValue).Start();
                    ClearPLC(RegMonitor.R_I[20]);
                }
                OldReserve7 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve7(object intValue)
        {
            try
            {
                Reserve7_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 保留触发8
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve8(int intValue)
        {
            try
            {
                if (intValue != OldReserve8
                    && intValue != 0)
                {
                    new Task(TrrigerReserve8, intValue).Start();
                    ClearPLC(RegMonitor.R_I[21]);
                }
                OldReserve8 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve8(object intValue)
        {
            try
            {
                Reserve8_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 保留触发9
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve9(int intValue)
        {
            try
            {
                if (intValue != OldReserve9
                    && intValue != 0)
                {
                    new Task(TrrigerReserve9, intValue).Start();
                    ClearPLC(RegMonitor.R_I[22]);
                }
                OldReserve9 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve9(object intValue)
        {
            try
            {
                Reserve9_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 保留触发10
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve10(int intValue)
        {
            try
            {
                if (intValue != OldReserve10
                    && intValue != 0)
                {
                    new Task(TrrigerReserve10, intValue).Start();
                    ClearPLC(RegMonitor.R_I[23]);
                }
                OldReserve10 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve10(object intValue)
        {
            try
            {
                Reserve10_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 保留触发11
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve11(int intValue)
        {
            try
            {
                if (intValue != OldReserve11
                    && intValue != 0)
                {
                    new Task(TrrigerReserve11, intValue).Start();
                    ClearPLC(RegMonitor.R_I[24]);
                }
                OldReserve11 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve11(object intValue)
        {
            try
            {
                Reserve11_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("LogicPLC", ex);
            }
        }
        /// <summary>
        /// 保留触发12
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve12(int intValue)
        {
            try
            {
                if (intValue != OldReserve12
                    && intValue != 0)
                {
                    new Task(TrrigerReserve12, intValue).Start();
                    ClearPLC(RegMonitor.R_I[25]);
                }
                OldReserve12 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve12(object intValue)
        {
            try
            {
                Reserve12_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 保留触发13
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve13(int intValue)
        {
            try
            {
                if (intValue != OldReserve13
                    && intValue != 0)
                {
                    new Task(TrrigerReserve13, intValue).Start();
                    ClearPLC(RegMonitor.R_I[26]);
                }
                OldReserve13 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve13(object intValue)
        {
            try
            {
                Reserve13_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 保留触发14
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve14(int intValue)
        {
            try
            {
                if (intValue != OldReserve14
                    && intValue != 0)
                {
                    new Task(TrrigerReserve14, intValue).Start();
                    ClearPLC(RegMonitor.R_I[27]);
                }
                OldReserve14 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve14(object intValue)
        {
            try
            {
                Reserve14_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 保留触发15
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve15(int intValue)
        {
            try
            {
                if (intValue != OldReserve15
                    && intValue != 0)
                {
                    new Task(TrrigerReserve15, intValue).Start();
                    ClearPLC(RegMonitor.R_I[28]);
                }
                OldReserve15 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve15(object intValue)
        {
            try
            {
                Reserve15_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 保留触发16
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve16(int intValue)
        {
            try
            {
                if (intValue != OldReserve16
                    && intValue != 0)
                {
                    new Task(TrrigerReserve16, intValue).Start();
                    ClearPLC(RegMonitor.R_I[29]);
                }
                OldReserve16 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve16(object intValue)
        {
            try
            {
                Reserve16_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 保留触发17
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve17(int intValue)
        {
            try
            {
                if (intValue != OldReserve17
                    && intValue != 0)
                {
                    new Task(TrrigerReserve17, intValue).Start();
                    ClearPLC(RegMonitor.R_I[30]);
                }
                OldReserve17 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve17(object intValue)
        {
            try
            {
                Reserve17_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 保留触发18
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve18(int intValue)
        {
            try
            {
                if (intValue != OldReserve18
                    && intValue != 0)
                {
                    new Task(TrrigerReserve18, intValue).Start();
                    ClearPLC(RegMonitor.R_I[31]);
                }
                OldReserve18 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve18(object intValue)
        {
            try
            {
                Reserve18_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 保留触发19
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve19(int intValue)
        {
            try
            {
                if (intValue != OldReserve19
                    && intValue != 0)
                {
                    new Task(TrrigerReserve19, intValue).Start();
                    ClearPLC(RegMonitor.R_I[32]);
                }
                OldReserve19 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve19(object intValue)
        {
            try
            {
                Reserve19_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 保留触发20
        /// </summary>
        /// <param name="intValue"></param>
        void Reserve20(int intValue)
        {
            try
            {
                if (intValue != OldReserve20
                    && intValue != 0)
                {
                    new Task(TrrigerReserve20, intValue).Start();
                    ClearPLC(RegMonitor.R_I[33]);
                }
                OldReserve20 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerReserve20(object intValue)
        {
            try
            {
                Reserve20_event(TriggerSource_enum.PLC, (int)intValue);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 保留触发

        #region 读值保留
        public double ReadRegReserveData(int index)
        {
            g_MtData.WaitOne();
            try
            {
                //数据判断
                int[] intValue = new int[2];
                bool blResult = ReadBlockReg(RegMonitor.R_I[index].NameReg.Replace("\\n", "\n"), 2, "ReadRegReserveData", out intValue);
                if (!blResult)
                {
                    CommunicationState_event("PLC读RegData寄存器失败:" + RegMonitor.R_I[index].NameReg, "red");
                    return 0;
                }
                double dblValue = ConvertReadData(intValue[0], intValue[1], RegMonitor.R_I[index]);
                return dblValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return int.MaxValue;
            }
            finally
            {
                g_MtData.ReleaseMutex();
            }
        }
        #endregion 读值保留

        #region 当前轴位置
        /// <summary>
        /// 当前轴位置
        /// </summary>
        /// <param name="intValue"></param>
        void ReserveXPos(int low, int high)
        {
            try
            {
                XPos = ConvertReadData(low, high, RegMonitor.R_I[34]);
                //ComValue.C_I.XPos = XPos;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void ReserveYPos(int low, int high)
        {
            try
            {
                YPos = ConvertReadData(low, high, RegMonitor.R_I[35]);
                //ComValue.C_I.YPos = YPos;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void ReserveZPos(int low, int high)
        {
            try
            {
                ZPos = ConvertReadData(low, high, RegMonitor.R_I[36]);
                //ComValue.C_I.ZPos = ZPos;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void ReserveRPos(int low, int high)
        {
            try
            {
                RPos = ConvertReadData(low, high, RegMonitor.R_I[37]);
                //ComValue.C_I.RPos = RPos;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 当前轴位置

        #region 读取PLC寄存器值
        /// <summary>
        /// 读值保留
        /// </summary>
        /// <param name="intValue"></param>
        void ReserveData(int numStart, int[] data)
        {
            try
            {
                for (int i = 0; i < (data.Length - numStart) / 2; i++)
                {
                    double value = ConvertReadData(data[numStart + 2 * i], data[numStart + 2 * i + 1], RegMonitor.R_I[i]);
                    RegReserveData[i] = value;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 读取PLC寄存器值

        #region 相机
        /// <summary>
        /// 相机1拍照
        /// </summary>
        /// <param name="intValue"></param>
        void Camera1(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera1)
                {
                    NoCamera1 = intValue;
                    BlCamera1 = true;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        new Task(new Action(() =>
                            {
                                Camera1_event(TriggerSource_enum.PLC, intValue);

                            })).Start();
                    }
                }
                OldCamera1 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void TrrigerCamera1()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(20);
                    if (BlCamera1)
                    {
                        BlCamera1 = false;
                        Camera1_event(TriggerSource_enum.PLC, NoCamera1);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机2拍照
        /// </summary>
        /// <param name="intValue"></param>
        void Camera2(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera2)
                {
                    NoCamera2 = intValue;
                    BlCamera2 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        new Task(new Action(() =>
                        {
                            Camera2_event(TriggerSource_enum.PLC, intValue);

                        })).Start();
                    }
                }
                OldCamera2 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerCamera2()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(20);
                    if (BlCamera2)
                    {
                        BlCamera2 = false;
                        Camera2_event(TriggerSource_enum.PLC, NoCamera2);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 相机3拍照
        /// </summary>
        /// <param name="intValue"></param>
        void Camera3(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera3)
                {
                    NoCamera3 = intValue;
                    BlCamera3 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        new Task(new Action(() =>
                        {
                            Camera3_event(TriggerSource_enum.PLC, intValue);

                        })).Start();
                    }
                }
                OldCamera3 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerCamera3()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(20);
                    if (BlCamera3)
                    {
                        BlCamera3 = false;
                        Camera3_event(TriggerSource_enum.PLC, NoCamera3);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 相机4拍照
        /// </summary>
        /// <param name="intValue"></param>
        void Camera4(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera4)
                {
                    NoCamera4 = intValue;
                    BlCamera4 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        new Task(new Action(() =>
                        {
                            Camera4_event(TriggerSource_enum.PLC, intValue);

                        })).Start();
                    }
                }
                OldCamera4 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerCamera4()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(20);
                    if (BlCamera4)
                    {
                        BlCamera4 = false;
                        Camera4_event(TriggerSource_enum.PLC, NoCamera4);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 相机5拍照
        /// </summary>
        /// <param name="intValue"></param>
        void Camera5(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera5)
                {
                    NoCamera5 = intValue;
                    BlCamera5 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        new Task(new Action(() =>
                        {
                            Camera5_event(TriggerSource_enum.PLC, intValue);

                        })).Start();
                    }
                }
                OldCamera5 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerCamera5()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(20);
                    if (BlCamera5)
                    {
                        BlCamera5 = false;
                        Camera5_event(TriggerSource_enum.PLC, NoCamera5);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 相机6拍照
        /// </summary>
        /// <param name="intValue"></param>
        void Camera6(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera6)
                {
                    NoCamera6 = intValue;
                    BlCamera6 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        new Task(new Action(() =>
                        {
                            Camera6_event(TriggerSource_enum.PLC, intValue);

                        })).Start();
                    }
                }
                OldCamera6 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerCamera6()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(20);
                    if (BlCamera6)
                    {
                        BlCamera6 = false;
                        Camera6_event(TriggerSource_enum.PLC, NoCamera6);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机7拍照
        /// </summary>
        /// <param name="intValue"></param>
        void Camera7(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera7)
                {
                    NoCamera7 = intValue;
                    BlCamera7 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        new Task(new Action(() =>
                        {
                            Camera7_event(TriggerSource_enum.PLC, intValue);

                        })).Start();
                    }
                }
                OldCamera6 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerCamera7()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(20);
                    if (BlCamera7)
                    {
                        BlCamera7 = false;
                        Camera7_event(TriggerSource_enum.PLC, NoCamera7);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机8拍照
        /// </summary>
        /// <param name="intValue"></param>
        void Camera8(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera8)
                {
                    NoCamera8 = intValue;
                    BlCamera8 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        new Task(new Action(() =>
                        {
                            Camera8_event(TriggerSource_enum.PLC, intValue);

                        })).Start();
                    }
                }
                OldCamera6 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        void TrrigerCamera8()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(20);
                    if (BlCamera8)
                    {
                        BlCamera8 = false;
                        Camera8_event(TriggerSource_enum.PLC, NoCamera8);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 运行控制

        #region 模拟触发
        /// <summary>
        /// 手动模拟PLC触发，传入信号
        /// </summary>
        public void ManualTrriger()
        {
            try
            {
                int[] intValue = new int[ParSetPLC.P_I.IntNumCycReg];
                for (int j = 0; j < ParLogicPLC.P_I.intTrrigerValue.Length; j++)
                {
                    intValue[j] = ParLogicPLC.P_I.intTrrigerValue[j];
                }
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//精简版本协议
                {
                    DealTrrigerSingle_New(intValue);
                }
                else
                {
                    DealTrrigerSingle(intValue);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 清楚Old值
        /// </summary>
        public void ClearOld()
        {
            try
            {
                OldSoftRestart = 0;//软件重启
                OldShutdown = 0;//关机
                OldRestart = 0;//重启        

                OldPLCReset = 0;//复位
                OldPLCRun = 0;//启动
                OldPLCPause = 0;//暂停
                OldPLCAlarm = 0;//报警
                OldPLCEmergency = 0;//急停

                OldNewModel = 0;//增加新的型号    
                OldChangeModel = 0;//切换型号
                OldDelModel = 0;//删除型号
                OldModel = 0;
                //保留
                OldReserve1 = 0;
                OldReserve2 = 0;
                OldReserve3 = 0;
                OldReserve4 = 0;
                OldReserve5 = 0;
                OldReserve6 = 0;
                OldReserve7 = 0;
                OldReserve8 = 0;
                OldReserve9 = 0;
                OldReserve10 = 0;
                OldReserve11 = 0;
                OldReserve12 = 0;
                OldReserve13 = 0;

                //相机
                OldCamera1 = 0;
                OldCamera2 = 0;
                OldCamera3 = 0;
                OldCamera4 = 0;
                OldCamera5 = 0;
                OldCamera6 = 0;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 模拟触发
    }
}
