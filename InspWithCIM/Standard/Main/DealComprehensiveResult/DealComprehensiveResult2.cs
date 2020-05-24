using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealPLC;
using System.Threading;
using System.Threading.Tasks;
using DealFile;
using DealComprehensive;
using Common;
using SetPar;
using ParComprehensive;
using BasicClass;
using Camera;
using System.Collections;
using DealResult;
using DealConfigFile;
using DealCalibrate;
using DealRobot;
using DealMath;
using DealImageProcess;
using BasicComprehensive;
using System.Diagnostics;
using System.IO;

namespace Main
{
    public partial class DealComprehensiveResult2 : BaseDealComprehensiveResult
    {
        #region 静态类实例
        public static DealComprehensiveResult2 D_I = new DealComprehensiveResult2();
        #endregion 静态类实例

        #region 定义
        Point2D posCenter1 = null;
        Point2D posCenter2 = null;

        public event IntAction ErrorCamera_event;//相机处理出错
        #endregion 定义

        #region 初始化
        public DealComprehensiveResult2()
        {
            try
            {
                base.NameClass = "DealComprehensiveResult2";
                //图像处理参数
                base.g_BaseParComprehensive = ParComprehensive2.P_I;
                g_ParIns = ParCam2.P_I;
                base.g_DealComprehensiveBase = DealComprehensive2.D_I;
                g_NoCamera = 2;
                //初始化PLC寄存器
                InitPLCReg();
                //注册事件
                Init_event();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("DealComprehensiveResult2", ex);
            }
        }

        //初始化PLC寄存器
        void InitPLCReg()
        {
            try
            {
                if (ParSetPLC.P_I.TypePLC_e != TypePLC_enum.Null)//三菱DealPLC
                {
                    if (ParCameraWork.NumCamera > 1)
                    {
                        base.g_regClearCamera = ParSetPLC.P_I.regClearCamera2;
                        base.g_regFinishPhoto = ParSetPLC.P_I.regFinishPhoto_Camera2;
                        base.g_regData_L.Add(ParSetPLC.P_I.regDataX_Camera2);
                        base.g_regData_L.Add(ParSetPLC.P_I.regDataY_Camera2);
                        base.g_regData_L.Add(ParSetPLC.P_I.regDataZ_Camera2);
                        base.g_regData_L.Add(ParSetPLC.P_I.regDataR_Jamera2);
                        base.g_regFinishData = ParSetPLC.P_I.regFinsihData_Camera2;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("DealComprehensiveResult2", ex);
            }
        }

        /// <summary>
        /// 事件注册
        /// </summary>
        void Init_event()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("DealComprehensiveResult2", ex);
            }
        }
        #endregion 初始化

        #region 位置1拍照
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public override StateComprehensive_enum DealComprehensiveResultFun1(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            htResult = null;
            int pos = 1;
            bool blResult = true;//结果是否正确
            double[] data = new double[4];

            if (MainCom.M_I.StateMachine_e == StateMachine_enum.NullRun)
            {
                return StateComprehensive_enum.True;
            }
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            try
            {
                BlFinishPos_Cam2 = false;
                //new Task(CycPhoto).Start();
                StartLoop = true;
                ShowState("相机2-1循环开始");
                return StateComprehensive_enum.True;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录
                Hashtable htResultNew = htResult;
                //new Task(new Action(() =>
                //{
                //g_DealComprehensiveBase.DisplayComprehensivePos(g_UCDisplayCamera, g_HtUCDisplay, pos, htResultNew, blResult);
                //记录当前整体节拍
                //RecordTact(sw, g_NoCamera, pos, htResultNew);
                //})).Start();
                #endregion 显示和日志记录
            }
        }
        #endregion 位置1拍照

        #region 位置2拍照
        public override StateComprehensive_enum DealComprehensiveResultFun2(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            htResult = null;
            int pos = 2;
            bool blResult = true;//结果是否正确
            double[] data = new double[4];

            Stopwatch sw = new Stopwatch();
            sw.Restart();
            try
            {
                BlFinishPos_Cam2 = false;
                //new Task(CycPhoto).Start();
                StartLoop = true;
                ShowState("相机2-2循环开始");
                return StateComprehensive_enum.True;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录
                Hashtable htResultNew = htResult;
                //new Task(new Action(() =>
                //{
                //g_DealComprehensiveBase.DisplayComprehensivePos(g_UCDisplayCamera, g_HtUCDisplay, pos, htResultNew, blResult);
                //记录当前整体节拍
                //RecordTact(sw, g_NoCamera, pos, htResultNew);
                //})).Start();
                #endregion 显示和日志记录
            }
        }
        #endregion 位置2拍照

        #region 位置3拍照
        /// <summary>
        /// 
        /// </summary>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public override StateComprehensive_enum DealComprehensiveResultFun3(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            htResult = null;
            int pos = 1;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            try
            {
                BlFinishPos_Cam2 = false;
                //new Task(CycPhoto).Start();

                StartLoop = true;
                ShowState("相机2-3循环开始");
                return StateComprehensive_enum.True;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录
                Hashtable htResultNew = htResult;
                //new Task(new Action(() =>
                //{
                //g_DealComprehensiveBase.DisplayComprehensivePos(g_UCDisplayCamera, g_HtUCDisplay, pos, htResultNew, blResult);
                //记录当前整体节拍
                RecordTact(sw, g_NoCamera, pos, htResultNew);
                //})).Start();
                #endregion 显示和日志记录
            }
        }
        #endregion 位置3拍照

        #region 位置4拍照
        public override StateComprehensive_enum DealComprehensiveResultFun4(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            htResult = null;
            int pos = 4;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            try
            {
                BlFinishPos_Cam2 = false;
                //new Task(CycPhoto).Start();
                StartLoop = true;
                ShowState("相机2-4循环开始");
                return StateComprehensive_enum.True;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                #region 显示和日志记录
                Hashtable htResultNew = htResult;
                //new Task(new Action(() =>
                //{
                //g_DealComprehensiveBase.DisplayComprehensivePos(g_UCDisplayCamera, g_HtUCDisplay, pos, htResultNew, blResult);
                //记录当前整体节拍
                RecordTact(sw, g_NoCamera, pos, htResultNew);
                //})).Start();
                #endregion 显示和日志记录
            }
        }
        #endregion 位置4拍照
    }
}
