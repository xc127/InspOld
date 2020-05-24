using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealPLC;
using System.Threading;
using System.Threading.Tasks;
using DealFile;
using DealComprehensive;
using DealRobot;
using Common;
using ParComprehensive;
using BasicClass;
using BasicComprehensive;
using Camera;
using System.Collections;
using DealComInterface;
using DealResult;
using DealLog;

namespace Main
{
    partial class BaseDealComprehensiveResult
    {
        #region 定义
        //bool
        public static bool BlFinishPos_Cam1 =true;//相机1处理完成
        public static bool BlFinishPos_Cam2 =true;//相机2处理完成
        public static bool BlFinishPos_Cam3 =true;//相机3处理完成
        public static bool BlFinishPos_Cam4 =true;//相机4处理完成
        public static bool BlFinishPos_Cam5 =true;//相机5处理完成
        public static bool BlFinishPos_Cam6 =true;//相机6处理完成

        //Hashtable
        protected static Hashtable HtResult_Cam1 = null;//相机1的结果
        protected static Hashtable HtResult_Cam2 = null;
        protected static Hashtable HtResult_Cam3 = null;
        protected static Hashtable HtResult_Cam4 = null;
        protected static Hashtable HtResult_Cam5 = null;
        protected static Hashtable HtResult_Cam6 = null;

       
        #endregion 定义

        #region 拍照结果
        public void FinishPhotoPLC(StateComprehensive_enum stateComprehensive_e)
        {
            if (ParSetPLC.P_I.TypePLC_e == TypePLC_enum.Null)
            {
                return;
            }
            int result = 2;
            if (stateComprehensive_e == StateComprehensive_enum.False)
            {
                result = 2;
            }
            else
            {
                result = 1;
            }
            LogicPLC.L_I.FinishPhoto(g_regClearCamera + g_regFinishPhoto, result);
        }

        public void FinishPhotoPLC(int result)
        {
            if (ParSetPLC.P_I.TypePLC_e == TypePLC_enum.Null)
            {
                return;
            }

            LogicPLC.L_I.FinishPhoto(g_regClearCamera + g_regFinishPhoto, result);
        }

        public void FinishPhotoRobot(int cameraNo, int pos, StateComprehensive_enum stateComprehensive_e)
        {
            if (ParSetRobot.P_I.TypeRobot_e == TypeRobot_enum.Null)
            {
                return;
            }
            int result = 0;
            if (stateComprehensive_e == StateComprehensive_enum.False)
            {
                result = 2;
            }
            else
            {
                result = 1;
            }
            //添加机器人拍照正确或错误处理

            //
        }

        public void FinishPhotoComInterface(int cameraNo, int pos, StateComprehensive_enum stateComprehensive_e)
        {
            if (ParComInterface.P_I.TypeComInterface_e == TypeComInterface_enum.Null)
            {
                return;
            }
            string result = "NG";
            if (stateComprehensive_e == StateComprehensive_enum.False)
            {
                result = "NG";
            }
            else
            {
                result = "OK";
            }
            LogicComInterface.L_I.PhotoResult(cameraNo, pos, result);
        }

        #endregion 拍照结果

        #region 计算数据
        public void WriteResultPLC(int cameraNo, int pos, Hashtable hashtable)
        {
            if (ParSetPLC.P_I.TypePLC_e == TypePLC_enum.Null)
            {
                return;
            }
            //添加PLC计算处理
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseDealComprehensiveResult", ex);
            }
        }

        public void WriteResultRobot(int cameraNo, int pos, Hashtable hashtable)
        {
            if (ParSetRobot.P_I.TypeRobot_e == TypeRobot_enum.Null)
            {
                return;
            }
            //添加机器人计算处理
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseDealComprehensiveResult", ex);
            }
            //
        }

        public void WriteResultComInterface(int cameraNo, int pos, Hashtable hashtable)
        {
            if (ParComInterface.P_I.TypeComInterface_e == TypeComInterface_enum.Null)
            {
                return;
            }
            //添加通用端口计算处理
            try
            {
                string name = "";//C?
                BaseResult resultBase = null;
                switch (pos)
                {
                    //----添加位置1的数据处理----//
                    case 1:
                        name = "";//C?
                        resultBase = (BaseResult)hashtable[name];
                        double[] data1 = new double[1];
                        data1[0] = resultBase.R;//先补正角度
                        LogicComInterface.L_I.DataTrans(1, 1, data1);
                        break;
                    //----添加位置2的数据处理----//
                    case 2:
                        name = "";//C?
                        resultBase = (BaseResult)hashtable[name];
                        double[] data2 = new double[2];
                        data2[0] = resultBase.X;//再补正X,Y
                        data2[1] = resultBase.Y;//再补正X,Y
                        LogicComInterface.L_I.DataTrans(1, 2, data2);
                        break;

                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseDealComprehensiveResult", ex);
            }
        }
        #endregion 计算数据

        #region 算法结果类型处理
        protected bool DealTypeResult(BaseResult result)
        {
            try
            {
                //如果为错误
                if (result.LevelError_e == LevelError_enum.Error)
                {
                    string level = result.LevelError_e.ToString();
                    string type = result.TypeErrorProcess_e.ToString();
                    string annotation = result.Annotation;
                    string info = string.Format("{0},类型:{1};{2}", level, type, annotation);                    

                    switch(result.TypeErrorProcess_e)
                    {
                        case TypeErrorProcess_enum.OutMemory:
                             WinError.GetWinInst().ShowError(info + ",请重新启动软件!");
                            break;

                        case TypeErrorProcess_enum.CameraImageError:
                            WinError.GetWinInst().ShowError(info + ",请重新启动软件，检测相机连接!");
                            break;

                        default:
                            g_UCAlarm.AddInfo(info);
                            break;
                    }
                    
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                
                Log.L_I.WriteError("MainBaseDealComprehensiveResult", ex);
                return false;
            }
        }
        #endregion 算法结果类型处理
    }
}
