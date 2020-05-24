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

        double[] regReserveData = null;
        /// <summary>
        /// 寄存器保留读值
        /// </summary>
        public double[] RegReserveData
        {
            get
            {
                if (regReserveData == null)
                {
                    regReserveData = new double[RegMonitor.R_I.NumRegSet];
                }
                return regReserveData;
            }
        }

        /// <summary>
        /// 寄存器保留读值1
        /// </summary>
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
        /// <summary>
        /// 寄存器保留读值2
        /// </summary>
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

        /// <summary>
        /// 寄存器保留读值3
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }
        /// <summary>
        /// 寄存器保留读值4
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }
        /// <summary>
        /// 寄存器保留读值5
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值6
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值7
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值8
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值9
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值10
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值11
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值12
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值13
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }
        /// <summary>
        /// 寄存器保留读值14
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值15
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值16
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值17
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值18
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值19
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
                    return double.MaxValue;
                }
            }
        }

        /// <summary>
        /// 寄存器保留读值20
        /// </summary>
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
                    Log.L_I.WriteError(NameClass, ex);
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
        public event TrrigerSourceAction_del CopyModelByNo_event;//通过序号进行型号切换
        public event TrrigerSourceAction_del DelModel_event;//删除型号

        //Restart
        public event TrrigerSourceAction_del RestartCommunicate_event;//重启外部通信

        //State
        public event TrrigerSourceAction_del VoiceState_event;//语音状态

        //TypeRun
        public event TrrigerSourceAction_del TypeRun_event;//设备运行模式

        public event Action RefreshRegValue_event;//寄存器值变化，触发刷新

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

        //大于八个相机
        public event TrrigerSourceAction_del Camera9_event;//相机9
        public event TrrigerSourceAction_del Camera10_event;//相机10
        public event TrrigerSourceAction_del Camera11_event;//相机11
        public event TrrigerSourceAction_del Camera12_event;//相机12

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
                if (RegMonitor.R_I.NumRegSet > 20)
                {
                    ReserveXPos(intValue[34], intValue[35]);
                }
                if (RegMonitor.R_I.NumRegSet > 21)
                {
                    ReserveYPos(intValue[36], intValue[37]);
                }
                if (RegMonitor.R_I.NumRegSet > 22)
                {
                    ReserveZPos(intValue[38], intValue[39]);
                }
                if (RegMonitor.R_I.NumRegSet > 23)
                {
                    ReserveRPos(intValue[40], intValue[41]);
                }
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regSoftReStart, intValue);//添加到读写日志中

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
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regPCShutDown, intValue);//添加到读写日志中

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
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regPCReStart, intValue);//添加到读写日志中


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


        bool blTriggerHeart = false;
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
                    blTriggerHeart = true;
                    //new Task(HeartBeatClear).Start();//新建线程是为了不占用读取时间
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
                ClearPLC(ParSetPLC.P_I.regHeartBeat);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 清除心跳的线程
        /// </summary>
        void CycClearHeart()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead)
                {
                    Thread.Sleep(1000);
                    if (blTriggerHeart)
                    {
                        blTriggerHeart = false;
                        HeartBeatClear();
                    }

                    //如果当前不处于清除状态，但是心跳寄存器有值
                    if (!blTriggerHeart)
                    {
                        if (OldHeartBeat != 0)
                        {
                            Log.L_I.WriteError(NameClass, "心跳在异常状态下清除");
                            HeartBeatClear();
                            Thread.Sleep(1000);
                        }
                    }
                }
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regPLCState, intValue);//添加到读写日志中

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
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regPLCAlarm, intValue);//添加到读写日志中


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


        int NoNew = 0;//新得型号序号
        /// <summary>
        /// 设备其他信息，包含了cim用的物料信息或者和型号相关的物料信息
        /// </summary>
        /// <param name="intValue"></param>
        void PLCMaterial(int intValue)
        {
            try
            {
                if (intValue != 0)//不支持0序号
                {
                    NoNew = intValue;//型号序号
                }
                
                if (intValue > OldPLCRun)
                {
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regPLCMaterial, intValue);//添加到读写日志中

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
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regRobotState, intValue);//添加到读写日志中

                    //RobotState_event(TrrigerSource_enum.PLC, intValue);
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regNewModel, intValue);//添加到读写日志中


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
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regChangeModel, intValue);//添加到读写日志中

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
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regDelModel, intValue);//添加到读写日志中

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
                            Thread.Sleep(100);
                            ReadModelName();//读取型号名称     
                            ChangeModel_event(TriggerSource_enum.PLC, intValue);
                            break;

                        case 3:
                            DelModel_event(TriggerSource_enum.PLC, intValue);
                            break;

                        //通过序号新建型号
                        case 11://型号名称里面包含了序号
                            Thread.Sleep(100);
                            ReadModelNo();//读取型号序号
                            ReadModelName();//读取型号名称                    
                            ReadConfigPar();//读取配置参数

                            NewModel_event(TriggerSource_enum.PLC, intValue);//触发事件
                            break;

                        //通过序号进行换型
                        case 12://型号名称里面包含了序号
                            Thread.Sleep(100);
                            ReadModelNo();//读取型号序号
                            ReadModelName();//读取型号名称                    
                            ReadConfigPar();//读取配置参数

                            ChangeModel_event(TriggerSource_enum.PLC, intValue);//触发事件
                            break;


                        //通过序号进行Copy,不改变当前应用的情况下，将当前信号copy到新的，只能支持序号的拷贝
                        case 101:
                            new Task(new Action(() =>
                            {
                                Thread.Sleep(500);

                                CopyModelByNo_event(TriggerSource_enum.PLC, NoNew);//触发事件
                                NoNew = 0;
                            })).Start();

                            break;

                        //通过序号进行删除
                        case 1001:
                            new Task(new Action(() =>
                            {
                                Thread.Sleep(500);

                                DelModel_event(TriggerSource_enum.PLC, NoNew);//触发事件
                                NoNew = 0;
                            })).Start();
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
                int[] intValue = new int[ParSetPLC.c_NumModelNameReg];
                if (ReadBlockReg(ParSetPLC.P_I.regModelName, ParSetPLC.c_NumModelNameReg, "ReadModelName", out intValue))
                {
                    ParLogicPLC.P_I.NameModel = "";

                    for (int i = 0; i < 18; i++)
                    {
                        char ch1 = ' ';
                        char ch2 = ' ';
                        if (ParSetPLC.P_I.TypePLC_e == TypePLC_enum.PAN_ModBusTCP
                            || ParSetPLC.P_I.TypePLC_e == TypePLC_enum.Keyence)//松下和基恩士的PLC高低位相反
                        {
                            ch1 = (char)(intValue[i] / 256);
                            ch2 = (char)(intValue[i] % 256);
                        }
                        else
                        {
                            ch1 = (char)(intValue[i] % 256);
                            ch2 = (char)(intValue[i] / 256);
                        }
                        
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

                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regModelNo, ParLogicPLC.P_I.intNo);//添加到读写日志中

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
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regRestartCommunicate, intValue);//添加到读写日志中


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
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regVoice, intValue);//添加到读写日志中


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

        #region 触发保留1
        bool BlReserve1 = false;
        int ValueReserve1 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[14].NameReg, intValue);//添加到读写日志中

                    BlReserve1 = true;
                    ValueReserve1 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        if (ParCameraWork.NumCamera > 8)//如果大于8个相机，寄存器用来触发相机9工作
                        {
                            Camera9_event(TriggerSource_enum.PLC, intValue);
                        }
                        else
                        {
                            new Task(TrrigerReserve1, intValue).Start();
                            ClearPLC(RegMonitor.R_I[14]);
                        }
                    }
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
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// PLC触发监控触发保留线程1
        /// </summary>
        void TriggerReserve1()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve1)
                    {
                        BlReserve1 = false;
                        if (ParCameraWork.NumCamera > 8)//如果大于8个相机，寄存器用来触发相机9工作
                        {
                            Camera9_event(TriggerSource_enum.PLC, ValueReserve1);
                        }
                        else
                        {
                            Reserve1_event(TriggerSource_enum.PLC, (int)ValueReserve1);
                            ClearPLC(RegMonitor.R_I[14]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留1

        #region 触发保留2
        bool BlReserve2 = false;
        int ValueReserve2 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[15].NameReg, intValue);//添加到读写日志中

                    BlReserve2 = true;
                    ValueReserve2 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        if (ParCameraWork.NumCamera > 9)//如果大于9个相机，寄存器用来触发相机10工作
                        {
                            Camera10_event(TriggerSource_enum.PLC, intValue);
                        }
                        else
                        {
                            new Task(TrrigerReserve2, intValue).Start();
                            ClearPLC(RegMonitor.R_I[15]);
                        }
                    }
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
        /// PLC触发监控触发保留线程2
        /// </summary>
        void TriggerReserve2()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve2)
                    {
                        BlReserve2 = false;
                        if (ParCameraWork.NumCamera > 9)//如果大于8个相机，寄存器用来触发相机9工作
                        {
                            Camera10_event(TriggerSource_enum.PLC, ValueReserve2);
                        }
                        else
                        {
                            Reserve2_event(TriggerSource_enum.PLC, (int)ValueReserve2);
                            ClearPLC(RegMonitor.R_I[15]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留2

        #region 触发保留3
        bool BlReserve3 = false;
        int ValueReserve3 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[16].NameReg, intValue);//添加到读写日志中

                    BlReserve3 = true;
                    ValueReserve3 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        if (ParCameraWork.NumCamera > 10)//如果大于10个相机，寄存器用来触发相机11工作
                        {
                            Camera11_event(TriggerSource_enum.PLC, intValue);
                        }
                        else
                        {
                            new Task(TrrigerReserve3, intValue).Start();
                            ClearPLC(RegMonitor.R_I[16]);
                        }
                    }
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
        /// PLC触发监控触发保留线程3
        /// </summary>
        void TriggerReserve3()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve3)
                    {
                        BlReserve3 = false;
                        if (ParCameraWork.NumCamera > 10)//如果大于8个相机，寄存器用来触发相机9工作
                        {
                            Camera11_event(TriggerSource_enum.PLC, ValueReserve3);
                        }
                        else
                        {
                            Reserve3_event(TriggerSource_enum.PLC, (int)ValueReserve3);
                            ClearPLC(RegMonitor.R_I[16]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留3

        #region 触发保留4
        bool BlReserve4 = false;
        int ValueReserve4 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[17].NameReg, intValue);//添加到读写日志中

                    BlReserve4 = true;
                    ValueReserve4 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        if (ParCameraWork.NumCamera > 11)//如果大于11个相机，寄存器用来触发相机12工作
                        {
                            Camera12_event(TriggerSource_enum.PLC, intValue);
                        }
                        else
                        {
                            new Task(TrrigerReserve4, intValue).Start();
                            ClearPLC(RegMonitor.R_I[17]);
                        }
                    }
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
        /// PLC触发监控触发保留线程4
        /// </summary>
        void TriggerReserve4()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve4)
                    {
                        BlReserve4 = false;
                        if (ParCameraWork.NumCamera > 11)//如果大于8个相机，寄存器用来触发相机9工作
                        {
                            Camera12_event(TriggerSource_enum.PLC, ValueReserve4);
                        }
                        else
                        {
                            Reserve4_event(TriggerSource_enum.PLC, (int)ValueReserve4);
                            ClearPLC(RegMonitor.R_I[17]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留4

        #region 触发保留5
        bool BlReserve5 = false;
        int ValueReserve5 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[18].NameReg, intValue);//添加到读写日志中

                    BlReserve5 = true;
                    ValueReserve5 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve5, intValue).Start();
                        ClearPLC(RegMonitor.R_I[18]);
                    }
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
        /// PLC触发监控触发保留线程5
        /// </summary>
        void TriggerReserve5()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve5)
                    {
                        BlReserve5 = false;

                        Reserve5_event(TriggerSource_enum.PLC, (int)ValueReserve5);
                        ClearPLC(RegMonitor.R_I[18]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留5

        #region 触发保留6
        bool BlReserve6 = false;
        int ValueReserve6 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[19].NameReg, intValue);//添加到读写日志中

                    BlReserve6 = true;
                    ValueReserve6 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve6, intValue).Start();
                        ClearPLC(RegMonitor.R_I[19]);
                    }
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
        /// PLC触发监控触发保留线程6
        /// </summary>
        void TriggerReserve6()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve6)
                    {
                        BlReserve6 = false;

                        Reserve6_event(TriggerSource_enum.PLC, (int)ValueReserve5);
                        ClearPLC(RegMonitor.R_I[19]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留6

        #region 触发保留7
        bool BlReserve7 = false;
        int ValueReserve7 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[20].NameReg, intValue);//添加到读写日志中

                    BlReserve7 = true;
                    ValueReserve7 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve7, intValue).Start();
                        ClearPLC(RegMonitor.R_I[20]);
                    }
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
        /// PLC触发监控触发保留线程7
        /// </summary>
        void TriggerReserve7()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve7)
                    {
                        BlReserve7 = false;

                        Reserve7_event(TriggerSource_enum.PLC, (int)ValueReserve7);
                        ClearPLC(RegMonitor.R_I[20]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留7

        #region 触发保留8
        bool BlReserve8 = false;
        int ValueReserve8 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[21].NameReg, intValue);//添加到读写日志中

                    BlReserve8 = true;
                    ValueReserve8 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve8, intValue).Start();
                        ClearPLC(RegMonitor.R_I[21]);
                    }
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
        /// PLC触发监控触发保留线程8
        /// </summary>
        void TriggerReserve8()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve8)
                    {
                        BlReserve8 = false;

                        Reserve8_event(TriggerSource_enum.PLC, (int)ValueReserve8);
                        ClearPLC(RegMonitor.R_I[21]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留8


        #region 触发保留9
        bool BlReserve9 = false;
        int ValueReserve9 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[22].NameReg, intValue);//添加到读写日志中

                    BlReserve9 = true;
                    ValueReserve9 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve9, intValue).Start();
                        ClearPLC(RegMonitor.R_I[22]);
                    }
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
        /// PLC触发监控触发保留线程9
        /// </summary>
        void TriggerReserve9()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve9)
                    {
                        BlReserve9 = false;

                        Reserve9_event(TriggerSource_enum.PLC, (int)ValueReserve9);
                        ClearPLC(RegMonitor.R_I[22]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留9

        #region 触发保留10
        bool BlReserve10 = false;
        int ValueReserve10 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[23].NameReg, intValue);//添加到读写日志中

                    BlReserve10 = true;
                    ValueReserve10 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve10, intValue).Start();
                        ClearPLC(RegMonitor.R_I[23]);
                    }
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
        /// PLC触发监控触发保留线程10
        /// </summary>
        void TriggerReserve10()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve10)
                    {
                        BlReserve10 = false;

                        Reserve10_event(TriggerSource_enum.PLC, (int)ValueReserve10);
                        ClearPLC(RegMonitor.R_I[23]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留10

        #region 触发保留11
        bool BlReserve11 = false;
        int ValueReserve11 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[24].NameReg, intValue);//添加到读写日志中

                    BlReserve11 = true;
                    ValueReserve11 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve11, intValue).Start();
                        ClearPLC(RegMonitor.R_I[24]);
                    }
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
        /// PLC触发监控触发保留线程11
        /// </summary>
        void TriggerReserve11()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve11)
                    {
                        BlReserve11 = false;

                        Reserve11_event(TriggerSource_enum.PLC, (int)ValueReserve11);
                        ClearPLC(RegMonitor.R_I[24]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留11

        #region 触发保留12
        bool BlReserve12 = false;
        int ValueReserve12 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[25].NameReg, intValue);//添加到读写日志中

                    BlReserve12 = true;
                    ValueReserve12 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve12, intValue).Start();
                        ClearPLC(RegMonitor.R_I[25]);
                    }
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
        /// PLC触发监控触发保留线程12
        /// </summary>
        void TriggerReserve12()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve12)
                    {
                        BlReserve12 = false;

                        Reserve12_event(TriggerSource_enum.PLC, (int)ValueReserve12);
                        ClearPLC(RegMonitor.R_I[25]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留12

        #region 触发保留13
        bool BlReserve13 = false;
        int ValueReserve13 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[26].NameReg, intValue);//添加到读写日志中

                    BlReserve13 = true;
                    ValueReserve13 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve13, intValue).Start();
                        ClearPLC(RegMonitor.R_I[26]);
                    }
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
        /// PLC触发监控触发保留线程13
        /// </summary>
        void TriggerReserve13()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve13)
                    {
                        BlReserve13 = false;

                        Reserve13_event(TriggerSource_enum.PLC, (int)ValueReserve13);
                        ClearPLC(RegMonitor.R_I[26]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留13


        #region 触发保留14
        bool BlReserve14 = false;
        int ValueReserve14 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[27].NameReg, intValue);//添加到读写日志中

                    BlReserve14 = true;
                    ValueReserve14 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve14, intValue).Start();
                        ClearPLC(RegMonitor.R_I[27]);
                    }
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
        /// PLC触发监控触发保留线程14
        /// </summary>
        void TriggerReserve14()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve14)
                    {
                        BlReserve14 = false;

                        Reserve14_event(TriggerSource_enum.PLC, (int)ValueReserve14);
                        ClearPLC(RegMonitor.R_I[27]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留14

        #region 触发保留15
        bool BlReserve15 = false;
        int ValueReserve15 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[28].NameReg, intValue);//添加到读写日志中

                    BlReserve15 = true;
                    ValueReserve15 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve15, intValue).Start();
                        ClearPLC(RegMonitor.R_I[28]);
                    }
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
        /// PLC触发监控触发保留线程15
        /// </summary>
        void TriggerReserve15()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve15)
                    {
                        BlReserve15 = false;

                        Reserve15_event(TriggerSource_enum.PLC, (int)ValueReserve15);
                        ClearPLC(RegMonitor.R_I[28]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留15


        #region 触发保留16
        bool BlReserve16 = false;
        int ValueReserve16 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[29].NameReg, intValue);//添加到读写日志中

                    BlReserve16 = true;
                    ValueReserve16 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve16, intValue).Start();
                        ClearPLC(RegMonitor.R_I[29]);
                    }
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
        /// PLC触发监控触发保留线程16
        /// </summary>
        void TriggerReserve16()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve16)
                    {
                        BlReserve16 = false;

                        Reserve16_event(TriggerSource_enum.PLC, (int)ValueReserve16);
                        ClearPLC(RegMonitor.R_I[29]);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留16

        #region 触发保留17
        bool BlReserve17 = false;
        int ValueReserve17 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[30].NameReg, intValue);//添加到读写日志中

                    BlReserve17 = true;
                    ValueReserve17 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve17, intValue).Start();
                        ClearPLC(RegMonitor.R_I[30]);
                    }
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
        /// PLC触发监控触发保留线程17
        /// </summary>
        void TriggerReserve17()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve17)
                    {
                        BlReserve17 = false;

                        Reserve17_event(TriggerSource_enum.PLC, (int)ValueReserve17);
                        ClearPLC(RegMonitor.R_I[30]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留17

        #region 触发保留18
        bool BlReserve18 = false;
        int ValueReserve18 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[31].NameReg, intValue);//添加到读写日志中

                    BlReserve18 = true;
                    ValueReserve18 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve18, intValue).Start();
                        ClearPLC(RegMonitor.R_I[31]);
                    }
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
        /// PLC触发监控触发保留线程18
        /// </summary>
        void TriggerReserve18()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve18)
                    {
                        BlReserve18 = false;

                        Reserve18_event(TriggerSource_enum.PLC, (int)ValueReserve18);
                        ClearPLC(RegMonitor.R_I[31]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留18

        #region 触发保留19
        bool BlReserve19 = false;
        int ValueReserve19 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[32].NameReg, intValue);//添加到读写日志中

                    BlReserve19 = true;
                    ValueReserve19 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve19, intValue).Start();
                        ClearPLC(RegMonitor.R_I[32]);
                    }
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
        /// PLC触发监控触发保留线程19
        /// </summary>
        void TriggerReserve19()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve19)
                    {
                        BlReserve19 = false;

                        Reserve19_event(TriggerSource_enum.PLC, (int)ValueReserve19);
                        ClearPLC(RegMonitor.R_I[32]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留19

        #region 触发保留20
        bool BlReserve20 = false;
        int ValueReserve20 = 0;
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
                    ParLogReadWritePLC.P_I.AddInfo_Read(RegMonitor.R_I[33].NameReg, intValue);//添加到读写日志中

                    BlReserve20 = true;
                    ValueReserve20 = intValue;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//如果不采用单线程
                    {
                        new Task(TrrigerReserve20, intValue).Start();
                        ClearPLC(RegMonitor.R_I[33]);
                    }
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

        /// <summary>
        /// PLC触发监控触发保留线程20
        /// </summary>
        void TriggerReserve20()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
                    if (BlReserve20)
                    {
                        BlReserve20 = false;

                        Reserve20_event(TriggerSource_enum.PLC, (int)ValueReserve20);
                        ClearPLC(RegMonitor.R_I[33]);

                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 触发保留20
        #endregion 保留触发

        #region 当前轴位置
        /// <summary>
        /// 当前轴位置
        /// </summary>
        /// <param name="low"></param>
        /// <param name="high"></param>
        void ReserveXPos(int low, int high)
        {
            try
            {
                XPos = ConvertReadData(low, high, RegMonitor.R_I[34]);
                ComValue.C_I.AxisPos1 = XPos;

                ComValue.C_I.ReserveValue_L[0].Value = XPos;
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
                ComValue.C_I.AxisPos2 = YPos;

                ComValue.C_I.ReserveValue_L[1].Value = YPos;
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
                ComValue.C_I.AxisPos3 = ZPos;

                ComValue.C_I.ReserveValue_L[2].Value = ZPos;
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
                ComValue.C_I.AxisPos4 = RPos;

                ComValue.C_I.ReserveValue_L[3].Value = RPos;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 当前轴位置

        #region 读值保留
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
                    double value = 0;
                    if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//精简版本协议，起始从轴坐标开始
                    {
                        value = ConvertReadData(data[numStart + 2 * i], data[numStart + 2 * i + 1], RegMonitor.R_I[i + numStart]);
                    }
                    else//旧版协议，起始不包含轴坐标
                    {
                        value = ConvertReadData(data[numStart + 2 * i], data[numStart + 2 * i + 1], RegMonitor.R_I[i + numStart - 4]);//-4是因为轴坐标的寄存器是32位错位了，
                    }
                    RegReserveData[i] = value;

                    ComValue.C_I.ReserveValue_L[i].Value = value;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 读值保留

        #region 相机
        /// <summary>
        /// 相机1拍照，最新的版后本20191025，Main_EX中更新了相机线程监控，所以PLC端可以不用采用监控线程了，
        /// </summary>
        /// <param name="intValue">PLC寄存器的值</param>
        void Camera1(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera1)
                {
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regClearCamera1, intValue);//添加到读写日志中

                    NoCamera1 = intValue;
                    BlCamera1 = true;

                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        //new Task(new Action(() =>
                        //    {
                        Camera1_event(TriggerSource_enum.PLC, intValue);
                        //})).Start();
                    }
                }
                OldCamera1 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// PLC触发监控线程
        /// </summary>
        void TriggerCamera1()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                       && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
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
        /// <param name="intValue">PLC寄存器的值</param>
        void Camera2(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera2)
                {
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regClearCamera2, intValue);//添加到读写日志中

                    NoCamera2 = intValue;
                    BlCamera2 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        //new Task(new Action(() =>
                        //{
                        Camera2_event(TriggerSource_enum.PLC, intValue);

                        //})).Start();
                    }
                }
                OldCamera2 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// PLC触发监控线程
        /// </summary>
        void TriggerCamera2()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
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
        /// <param name="intValue">PLC寄存器的值</param>
        void Camera3(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera3)
                {
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regClearCamera3, intValue);//添加到读写日志中

                    NoCamera3 = intValue;
                    BlCamera3 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        //new Task(new Action(() =>
                        //{
                        Camera3_event(TriggerSource_enum.PLC, intValue);
                        //})).Start();
                    }
                }
                OldCamera3 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// PLC触发监控线程
        /// </summary>
        void TriggerCamera3()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
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
        /// <param name="intValue">PLC寄存器的值</param>
        void Camera4(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera4)
                {
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regClearCamera4, intValue);//添加到读写日志中

                    NoCamera4 = intValue;
                    BlCamera4 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        //new Task(new Action(() =>
                        //{
                        Camera4_event(TriggerSource_enum.PLC, intValue);
                        //})).Start();
                    }
                }
                OldCamera4 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// PLC触发监控线程
        /// </summary>
        void TriggerCamera4()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
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
        /// <param name="intValue">PLC寄存器的值</param>
        void Camera5(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera5)
                {
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regClearCamera5, intValue);//添加到读写日志中

                    NoCamera5 = intValue;
                    BlCamera5 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        //new Task(new Action(() =>
                        //{
                        Camera5_event(TriggerSource_enum.PLC, intValue);
                        //})).Start();
                    }
                }
                OldCamera5 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// PLC触发监控线程
        /// </summary>
        void TriggerCamera5()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
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
        /// <param name="intValue">PLC寄存器的值</param>
        void Camera6(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera6)
                {
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regClearCamera6, intValue);//添加到读写日志中

                    NoCamera6 = intValue;
                    BlCamera6 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        //new Task(new Action(() =>
                        //{
                        Camera6_event(TriggerSource_enum.PLC, intValue);
                        //})).Start();
                    }
                }
                OldCamera6 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// PLC触发监控线程
        /// </summary>
        void TriggerCamera6()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
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
        /// <param name="intValue">PLC寄存器的值</param>
        void Camera7(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera7)
                {
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regClearCamera7, intValue);//添加到读写日志中

                    NoCamera7 = intValue;
                    BlCamera7 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        //new Task(new Action(() =>
                        //{
                        Camera7_event(TriggerSource_enum.PLC, intValue);

                        //})).Start();
                    }
                }
                OldCamera7 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// PLC触发监控线程
        /// </summary>
        void TriggerCamera7()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
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
        /// <param name="intValue">PLC寄存器的值</param>
        void Camera8(int intValue)
        {
            try
            {
                if (intValue != 0
                    && intValue != OldCamera8)
                {
                    ParLogReadWritePLC.P_I.AddInfo_Read(ParSetPLC.P_I.regClearCamera8, intValue);//添加到读写日志中

                    NoCamera8 = intValue;
                    BlCamera8 = true;
                    if (!ParSetPLC.P_I.BlRSingleTaskCamera)//不采用单线程
                    {
                        //new Task(new Action(() =>
                        //{
                        Camera8_event(TriggerSource_enum.PLC, intValue);
                        //})).Start();
                    }
                }
                OldCamera8 = intValue;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// PLC触发监控线程
        /// </summary>
        void TriggerCamera8()
        {
            try
            {
                while (ParLogicPLC.P_I.BlRead
                    && ParSetPLC.P_I.BlRSingleTaskCamera)
                {
                    Thread.Sleep(10);
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
                //当数据发生变化时，开始记录
                CheckReg.C_I.AddValue(intValue);
                if (RefreshRegValue_event != null)
                {
                    RefreshRegValue_event();
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
        /// 清除Old值
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
