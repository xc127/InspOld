using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;
using DealConfigFile;
using Common;
using BasicClass;
using DealCalibrate;
using DealImageProcess;
using DealPLC;
using DealMontionCtrler;
using System.Threading.Tasks;

namespace Main
{
    public partial class MainCom
    {
        #region 静态类实例
        public static MainCom M_I = new MainCom();
        #endregion 静态类实例

        #region 定义

        public int intUpCountBase = 1;
        public int intDownCountBase = 1;

        public int intCam1Cnt = 1;
        public int intCam2Cnt = 1;
        public int intCam3Cnt = 1;
        public int intCam4Cnt = 1;

        public int intProduct1Result = 1;
        public int intProduct2Result = 1;
        public int intProduct3Result = 1;
        public int intProduct4Result = 1;

        //每个产品的结果处理
        public ResultDeal resultDealP1 = new ResultDeal(1);
        public ResultDeal resultDealP2 = new ResultDeal(2);
        public ResultDeal resultDealP3 = new ResultDeal(3);
        public ResultDeal resultDealP4 = new ResultDeal(4);

        public bool ResetRobot = false;//机器人在RESET点
        public bool HomeRobot = true;//机器人在home点

        public StateMachine_enum StateMachine_e = StateMachine_enum.Null;

        public bool IsPhotoNearCST = false;
        
        public double XInset = 0;//插栏临时补偿
        public double RInset = 0;

        public bool g_BlMachineAlarm = false;
        public bool g_IsCycling = false;

        public bool IsDebugMode { get; set; }
        #endregion 定义

    }

    //这个类存储每个拍照点的结果，待存储数量达到之后，对结果进行分析
    public class ResultDeal
    {
        Mutex mutex = new Mutex();
        int product = 1;
        public ResultDeal(int p)
        {
            product = p;
        }

        public List<int> Result_L = new List<int>();

        public void InitResult()
        {
            Result_L.Clear();

        }

        public void ResultAdd(int result)
        {
            try
            {
                mutex.WaitOne();
                Result_L.Add(result);
                if (Result_L.Count == 4)
                {
                    //写入PLC对应的结果
                    if (Result_L.Contains(2))
                    {
                        LogicPLC.L_I.WriteRegData1(4 + product, 2);
                    }
                    else
                    {
                        LogicPLC.L_I.WriteRegData1(4 + product, 1);
                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                mutex.ReleaseMutex();
            }

        }
    }


    /// <summary>
    /// 随机器人传输的数据
    /// </summary>
    public class DataTransRobot
    {
        public string Code = "";
        public double DeltaX_AOI = 0;
        public double DeltaY_AOI = 0;

        public double DeltaX_Insert = 0;
        public double DeltaY_Insert = 0;
    }

    public class Light
    {
        public string Name { get; set; }
        public int IndexCan { get; set; }
        public int IndexIO { get; set; }
        public int FrameRate { get; set; }
        public bool BlContinueFlash { get; set; }
        public bool IsBright { get; set; }

        public Light(string name, int indexCan, int indexIO, int frameRate = 500)
        {
            this.Name = name;
            this.IndexCan = indexCan;
            this.IndexIO = indexIO;
            this.FrameRate = frameRate;
        }

        /// <summary>
        /// 灯光闪烁
        /// </summary>
        public void Flash()
        {
            BlContinueFlash = true;

            new Task(delegate()
            {
                while (BlContinueFlash)
                {
                    if (BlContinueFlash)
                    {
                        LogicMotionCtler.L_I.OnOutBit(IndexCan, IndexIO);
                        Thread.Sleep(FrameRate);
                    }
                    if (BlContinueFlash)
                    {
                        LogicMotionCtler.L_I.OffOutBit(IndexCan, IndexIO);
                        Thread.Sleep(FrameRate);
                    }
                }
            }).Start();
        }

        public void OpenLight()
        {
            BlContinueFlash = false;
            LogicMotionCtler.L_I.OnOutBit(IndexCan, IndexIO);
            IsBright = true;
        }

        public void CloseLight()
        {
            BlContinueFlash = false;
            LogicMotionCtler.L_I.OffOutBit(IndexCan, IndexIO);
            IsBright = false;
        }
    }

    /// <summary>
    /// 双控气缸
    /// </summary>
    public class AirCylinderDblCtl
    {
        public AirCylinderDblCtl(string str, int indexCanOn, int indexIOOn, int indexCanOff, int indexIOOff)
        {
            Name = str;
            IndexCanOn = indexCanOn;
            IndexIOOn = indexIOOn;
            IndexCanOff = indexCanOff;
            IndexIOOff = indexIOOff;
        }

        public string Name { get; set; }

        public int IndexCanOn { get; set; }

        public int IndexIOOn { get; set; }

        public int IndexCanOff { get; set; }

        public int IndexIOOff { get; set; }

        private PNStatue_Enum pNStatue_E;
        /// <summary>
        /// 状态
        /// </summary>
        public PNStatue_Enum PNStatue_E
        {
            get { return pNStatue_E; }
            set { pNStatue_E = value; }
        }
        /// <summary>
        /// 正向打开气缸
        /// </summary>
        /// <returns></returns>
        public bool OnAirCylinder(int time)
        {
            try
            {
                LogicMotionCtler.L_I.OffOutBit(IndexCanOff, IndexIOOff);
                LogicMotionCtler.L_I.OnOutBit(IndexCanOn, IndexIOOn);
                while (PNStatue_E != PNStatue_Enum.Positive)
                {

                }
                return true;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        /// <summary>
        /// 反向打开气缸
        /// </summary>
        /// <returns></returns>
        public bool OffAirCylinder()
        {
            try
            {
                LogicMotionCtler.L_I.OffOutBit(IndexCanOn, IndexIOOn);
                LogicMotionCtler.L_I.OnOutBit(IndexCanOff, IndexIOOff);
                while (PNStatue_E != PNStatue_Enum.Negative)
                {

                }
                return true;
            }
            catch (Exception ex)
            {

            }
            return false ;
        }

    }

    public class FunWait
    {
        /// <summary>
        /// 定时等待
        /// </summary>
        /// <param name="time"></param>
        /// <param name="blWait"></param>
        /// <returns></returns>
        public bool WaitForBool(int time, ref bool blWait)
        {
            int i = 0;
            while (!blWait)
            {
                if (MainCom.M_I.g_BlMachineAlarm || !MainCom.M_I.g_IsCycling)
                {
                    return false;
                }
                i++;
                if (i > time / 10)//10ms位单位
                {
                    return false;
                }
                Thread.Sleep(10);
            }
            return true;

        }

        /// <summary>
        /// 一直等待，报警时退出
        /// </summary>
        /// <param name="time"></param>
        /// <param name="blWait"></param>
        /// <returns></returns>
        public bool WaitForBool(ref bool blWait)
        {
            while (!blWait)
            {
                if (MainCom.M_I.g_BlMachineAlarm || !MainCom.M_I.g_IsCycling)
                {
                    return false;
                }

                Thread.Sleep(1);
            }
            return true;
        }
    }
}
