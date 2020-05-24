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

namespace Main
{
    public class DealComprehensiveResult3 : BaseDealComprehensiveResult
    {
        #region 静态类实例
        public static DealComprehensiveResult3 D_I = new DealComprehensiveResult3();
        #endregion 静态类实例

        #region 定义
        //List
        List<Point2D> PointError_L = new List<Point2D>();
        #endregion 定义

        #region 初始化
        public DealComprehensiveResult3()
        {
            try
            {
                base.NameClass = "DealComprehensiveResult3";
                //图像处理参数
                base.g_BaseParComprehensive = ParComprehensive3.P_I;
                base.g_DealComprehensiveBase = DealComprehensive3.D_I;
                g_ParIns = ParCam3.P_I;
                g_NoCamera = 3;
                //初始化PLC寄存器
                InitPLCReg();
                //注册事件
                Init_event();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("DealComprehensiveResult3", ex);
            }
        }

        //初始化PLC寄存器
        void InitPLCReg()
        {
            try
            {
                if (ParSetPLC.P_I.TypePLC_e != TypePLC_enum.Null)//三菱DealPLC
                {
                    if (ParCameraWork.NumCamera > 2)
                    {
                        base.g_regClearCamera = ParSetPLC.P_I.regClearCamera3;
                        base.g_regFinishPhoto = ParSetPLC.P_I.regFinishPhoto_Camera3;
                        base.g_regData_L.Add(ParSetPLC.P_I.regDataX_Camera3);
                        base.g_regData_L.Add(ParSetPLC.P_I.regDataY_Camera3);
                        base.g_regData_L.Add(ParSetPLC.P_I.regDataZ_Camera3);
                        base.g_regData_L.Add(ParSetPLC.P_I.regDataR_Jamera3);
                        base.g_regFinishData = ParSetPLC.P_I.regFinsihData_Camera3;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("DealComprehensiveResult3", ex);
            }
        }

        /// <summary>
        /// 事件注册
        /// </summary>
        void Init_event()
        {
            try
            {
                //FinishPreceise_event += new Action(DealComprehensiveResult_FinishPreceise_event);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("DealComprehensiveResult3", ex);
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
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            try
            {
                BlFinishPos_Cam3 = false;
                //new Task(CycPhoto).Start();
                StartLoop = true;
                ShowState("相机3-1循环开始");
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
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            try
            {
                BlFinishPos_Cam3 = false;
                //new Task(CycPhoto).Start();
                StartLoop = true;
                ShowState("相机3-2循环开始");
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
                BlFinishPos_Cam3 = false;
                //new Task(CycPhoto).Start();
                StartLoop = true;
                ShowState("相机3-3循环开始");
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
                g_DealComprehensiveBase.DisplayComprehensivePos(g_UCDisplayCamera, g_HtUCDisplay, pos, htResultNew, blResult);
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
                BlFinishPos_Cam3 = false;
                //new Task(CycPhoto).Start();
                StartLoop = true;
                ShowState("相机3-4循环开始");
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
                g_DealComprehensiveBase.DisplayComprehensivePos(g_UCDisplayCamera, g_HtUCDisplay, pos, htResultNew, blResult);
                //记录当前整体节拍
                RecordTact(sw, g_NoCamera, pos, htResultNew);
                //})).Start();
                #endregion 显示和日志记录
            }
        }
        #endregion 位置4拍照

    }
}
