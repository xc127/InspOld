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

namespace Main
{
    public class DealComprehensiveResult8 : BaseDealComprehensiveResult
    {
        #region 静态类实例
        public static DealComprehensiveResult8 D_I = new DealComprehensiveResult8();
        #endregion 静态类实例

        #region 初始化
        public DealComprehensiveResult8()
        {
            try
            {
                base.NameClass = "DealComprehensiveResult8";
                //图像处理参数
                base.g_BaseParComprehensive = ParComprehensive8.P_I;
                base.g_DealComprehensiveBase = DealComprehensive8.D_I;
                g_NoCamera = 8;
                //初始化DealPLC寄存器
                InitPLCReg();

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        //初始化DealPLC寄存器
        void InitPLCReg()
        {
            try
            {
                if (ParSetPLC.P_I.TypePLC_e != TypePLC_enum.Null)//三菱DealPLC
                {
                    if (ParCameraWork.NumCamera > 7)
                    {
                        base.g_regClearCamera = ParSetPLC.P_I.regClearCamera8;
                        base.g_regFinishPhoto = ParSetPLC.P_I.regFinishPhoto_Camera8;
                        base.g_regData_L.Add(ParSetPLC.P_I.regDataX_Camera8);
                        base.g_regData_L.Add(ParSetPLC.P_I.regDataY_Camera8);
                        base.g_regData_L.Add(ParSetPLC.P_I.regDataZ_Camera8);
                        base.g_regData_L.Add(ParSetPLC.P_I.regDataR_Jamera8);
                        base.g_regFinishData = ParSetPLC.P_I.regFinsihData_Camera8;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

        /// <summary>
        /// 位置1处理
        /// </summary>
        /// <param name="hashtable"></param>
        /// <returns></returns>
        public override StateComprehensive_enum DealComprehensiveResultFun1(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            htResult = null;
            int pos = 1;
            sw_Tact.Restart();
            try
            {
                StateComprehensive_enum stateComprehensive_e = g_DealComprehensiveBase.DealComprehensivePosNoDisplay(g_UCDisplayCamera, g_HtUCDisplay, pos, out htResult);
                return stateComprehensive_e;
            }
            catch (Exception ex)
            {
                LogicPLC.L_I.FinishPhoto(g_regClearCamera + g_regFinishPhoto, 2);
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                //记录当前整体节拍
                RecordTact(g_NoCamera, pos, htResult);
                g_DealComprehensiveBase.DisplayComprehensivePos(g_UCDisplayCamera, g_HtUCDisplay, pos, htResult);
            }
        }

        /// <summary>
        /// 位置2
        /// </summary>
        /// <param name="htResult"></param>
        /// <returns></returns>
        public override StateComprehensive_enum DealComprehensiveResultFun2(TriggerSource_enum trigerSource_e, out Hashtable htResult)
        {
            htResult = null;
            int pos = 1;
            sw_Tact.Restart();
            try
            {
                StateComprehensive_enum stateComprehensive_e = g_DealComprehensiveBase.DealComprehensivePosNoDisplay(g_UCDisplayCamera, g_HtUCDisplay, pos, out htResult);
                return stateComprehensive_e;
            }
            catch (Exception ex)
            {
                LogicPLC.L_I.FinishPhoto(g_regClearCamera + g_regFinishPhoto, 2);
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
            finally
            {
                //记录当前整体节拍
                RecordTact(g_NoCamera, pos, htResult);
                g_DealComprehensiveBase.DisplayComprehensivePos(g_UCDisplayCamera, g_HtUCDisplay, pos, htResult);
            }
        }
    }
}
