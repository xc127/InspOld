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
using DealImageProcess;
using System.Diagnostics;
using DealMontionCtrler;
using DealAlgorithm;

namespace Main
{
    public partial class DealComprehensiveResult1 : BaseDealComprehensiveResult
    {
        #region 静态类实例
        public static DealComprehensiveResult1 D_I = new DealComprehensiveResult1();
        #endregion 静态类实例

        #region 定义

        #endregion 定义

        #region 初始化
        public DealComprehensiveResult1()
        {
            try
            {
                base.NameClass = "DealComprehensiveResult1";
                //图像处理参数
                base.g_BaseParComprehensive = ParComprehensive1.P_I;
                base.g_DealComprehensiveBase = DealComprehensive1.D_I;
                g_ParIns = ParCam1.P_I;
                g_NoCamera = 1;
                //初始化DealPLC寄存器
                InitPLCReg();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("DealComprehensiveResult1", ex);
            }
        }

        //初始化DealPLC寄存器
        void InitPLCReg()
        {
            try
            {
                if (ParSetPLC.P_I.TypePLC_e != TypePLC_enum.Null)//三菱DealPLC
                {
                    base.g_regClearCamera = ParSetPLC.P_I.regClearCamera1;
                    base.g_regFinishPhoto = ParSetPLC.P_I.regFinishPhoto_Camera1;
                    base.g_regData_L.Add(ParSetPLC.P_I.regDataX_Camera1);
                    base.g_regData_L.Add(ParSetPLC.P_I.regDataY_Camera1);
                    base.g_regData_L.Add(ParSetPLC.P_I.regDataZ_Camera1);
                    base.g_regData_L.Add(ParSetPLC.P_I.regDataR_Jamera1);
                    base.g_regFinishData = ParSetPLC.P_I.regFinsihData_Camera1;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        #region 位置1拍照
        /// <summary>
        /// 
        /// </summary>
        public override StateComprehensive_enum DealComprehensiveResultFun1(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            #region 定义
            htResult = null;
            int pos = 1;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion

            try
            {

               // RecordInspChipID(CodeNow);
                BlCodeReady = true;
                // g_UCResultInsp.ShowCode(CodeNow);
                SideIndex = 1;
                ShowState("清空拍照信号：" + SideIndex);
                FinishAllPhotoPLC();
                ShowState("清空PLC触发信号完成");
                // //初始化集合
                // ClearSingleDG();
                
                BlFinishPos_Cam1 = false;
                //new Task(CycPhoto).Start();
                StartLoop = true;

                return StateComprehensive_enum.True;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                Hashtable htResultNew = htResult;
                //new Task(new Action(() =>
                //    {
                //g_DealComprehensiveBase.DisplayComprehensivePos(g_UCDisplayCamera, g_HtUCDisplay, pos, htResultNew, blResult);
                //记录当前整体节拍
                //     RecordTact(sw, g_NoCamera, pos, htResultNew);
                //})).Start();

                //sw_Tact.Stop();
                ShowState("相机1触发返回");
            }
        }
        #endregion 位置1拍照

        #region 位置2拍照
        public override StateComprehensive_enum DealComprehensiveResultFun2(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            #region
            htResult = null;
            int pos = 2;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion
            try
            {
                SideIndex = 2;
                BlFinishPos_Cam1 = false;

                //new Task(CycPhoto).Start();
                StartLoop = true;
                ShowState("确认后的二维码为：" + CodeNow);
                BlCodeReady = true;
                ShowState("清空拍照信号：" + SideIndex);
                FinishAllPhotoPLC();

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
            #region
            htResult = null;
            int pos = 1;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion
            try
            {
                SideIndex = 3;
                BlFinishPos_Cam1 = false;

                //new Task(CycPhoto).Start();

                StartLoop = true; 
                
                ShowState("清空拍照信号：" + SideIndex);
                FinishAllPhotoPLC();

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
            #region
            htResult = null;
            int pos = 1;
            bool blResult = true;//结果是否正确
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            #endregion
            try
            {
                SideIndex = 4;
                BlFinishPos_Cam1 = false;
                //new Task(CycPhoto).Start();
                StartLoop = true;

                ShowState("清空拍照信号：" + SideIndex);
                FinishAllPhotoPLC();

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
