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
using DealConfigFile;
using DealCalibrate;
using DealCIM;

namespace Main
{
    public partial class BaseDealComprehensiveResult
    {
        #region 定义
        //double

        //Int
        protected int g_NoCamera = 1;

        //string 
        public List<RegPLC> g_regData_L = new List<RegPLC>();
        public string g_regFinishData = "";
        public string g_regClearCamera = "";//清除相机触发信号
        public string g_regFinishPhoto = "";//拍照OK或者NG

        //图像处理参数        
        public BaseParComprehensive g_BaseParComprehensive = null;
        //图像处理方法  
        public BaseDealComprehensive g_DealComprehensiveBase = null;

        //Class
        public UCResult g_UCResult = null;
        public static UCAlarm g_UCAlarm = null;
        public static UCStateWork g_UCStateWork = null;
        public static CIMStatus g_UCCimStatus = null;
        public Hashtable g_HtUCDisplay = null;//显示窗体
        public BaseUCDisplayCamera g_UCDisplayCamera = null;
        /// <summary>
        /// 当前相机AMP系数
        /// </summary>
        public double Amp
        {
            get
            {
                return ParCalibWorld.V_I[g_NoCamera];
            }
        }

        #endregion 定义

        #region 初始化
        public BaseDealComprehensiveResult()
        {
            NameClass = "BaseDealComprehensiveResult";
        }
        /// <summary>
        /// 初始化为Halcon窗体
        /// </summary>
        /// <param name="uICameraBase">halcon窗体界面</param>
        /// <param name="uCResult"></param>
        /// <param name="uCAlarm"></param>
        /// <param name="uCStateWork"></param>
        public virtual void Init(BaseUCDisplayCamera baseUCDisplayCamera, Hashtable htUCDisplay, UCResult uCResult, UCAlarm uCAlarm, UCStateWork uCStateWork)
        {
            try
            {
                g_UCDisplayCamera = baseUCDisplayCamera;
                g_HtUCDisplay = htUCDisplay;
                g_UCResult = uCResult;
                g_UCAlarm = uCAlarm;
                g_UCStateWork = uCStateWork;

                if (ParCameraWork.NumCamera >= g_NoCamera)
                {
                    Task.Factory.StartNew(CycPhoto, TaskCreationOptions.LongRunning);
                    Task.Factory.StartNew(DealQueue, TaskCreationOptions.LongRunning);
                }

                LoginEvent();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 事件注册
        /// </summary>
        void LoginEvent()
        {
            try
            {
                g_DealComprehensiveBase.AlarmMemory_event += new StrAction(g_DealComprehensiveBase_AlarmMemory_event);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        #endregion 初始化

        //获取错误单元
        string GetErrorCell(Hashtable hashtable)
        {
            try
            {
                if (hashtable == null)
                {
                    return "";
                }
                if (hashtable.Keys == null)
                {
                    return "";
                }
                foreach (string key in hashtable.Keys)
                {
                    if (hashtable[key] == null)
                    {
                        string str = "相机" + g_NoCamera.ToString() + "处理异常:";
                        BaseParComprehensive baseParComprehensive = g_BaseParComprehensive.GetCellClass(key);
                        string annotation = baseParComprehensive.Annotation;
                        g_UCAlarm.AddInfo(str + key + ":" + annotation);
                        g_UCStateWork.AddInfo(str + key + ":" + annotation);
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return "";
            }
        }

        #region 响应外部触发
        public StateComprehensive_enum DealComprehensiveResultFun(TriggerSource_enum trigerSource_e, int i)
        {
            try
            {
                Hashtable hashtable = null;
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.False;
                switch (i)
                {
                    case 1:
                        stateComprehensive_e = DealComprehensiveResultFun1(trigerSource_e, out hashtable);
                        break;

                    case 2:
                        stateComprehensive_e = DealComprehensiveResultFun2(trigerSource_e, out hashtable);
                        break;

                    case 3:
                        stateComprehensive_e = DealComprehensiveResultFun3(trigerSource_e, out hashtable);
                        break;

                    case 4:
                        stateComprehensive_e = DealComprehensiveResultFun4(trigerSource_e, out hashtable);
                        break;
                }
                //g_UCResult.AddResult(hashtable, g_NoCamera);
                GetErrorCell(hashtable);//显示错误信息
                return stateComprehensive_e;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
        }

        /// <summary>
        /// 传入参考值
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public StateComprehensive_enum DealComprehensiveResultFun(TriggerSource_enum trigerSource_e, int i, int index)
        {
            try
            {
                Hashtable hashtable = null;
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.False;
                switch (i)
                {
                    case 1:
                        stateComprehensive_e = DealComprehensiveResultFun1(trigerSource_e, index, out hashtable);
                        break;

                    case 2:
                        stateComprehensive_e = DealComprehensiveResultFun2(trigerSource_e, index, out hashtable);
                        break;

                    case 3:
                        stateComprehensive_e = DealComprehensiveResultFun3(trigerSource_e, index, out hashtable);
                        break;

                    case 4:
                        stateComprehensive_e = DealComprehensiveResultFun4(trigerSource_e, index, out hashtable);
                        break;
                }
                //g_UCResult.AddResult(hashtable, g_NoCamera);
                GetErrorCell(hashtable);//显示错误信息
                return stateComprehensive_e;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
        }

        /// <summary>
        /// 传入参考数组
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public StateComprehensive_enum DealComprehensiveResultFun(TriggerSource_enum trigerSource_e, int i, int[] index)
        {
            try
            {
                Hashtable hashtable = null;
                StateComprehensive_enum stateComprehensive_e = StateComprehensive_enum.False;
                switch (i)
                {
                    case 1:
                        stateComprehensive_e = DealComprehensiveResultFun1(trigerSource_e, index, out hashtable);
                        break;

                    case 2:
                        stateComprehensive_e = DealComprehensiveResultFun2(trigerSource_e, index, out hashtable);
                        break;

                    case 3:
                        stateComprehensive_e = DealComprehensiveResultFun3(trigerSource_e, index, out hashtable);
                        break;

                    case 4:
                        stateComprehensive_e = DealComprehensiveResultFun4(trigerSource_e, index, out hashtable);
                        break;

                    case 5:
                        stateComprehensive_e = DealComprehensiveResultFun5(trigerSource_e, index, out hashtable);
                        break;
                }
                g_UCResult.AddResult(hashtable, g_NoCamera);
                GetErrorCell(hashtable);//显示错误信息
                return stateComprehensive_e;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return StateComprehensive_enum.False;
            }
        }

        #region 第1次拍照
        public virtual StateComprehensive_enum DealComprehensiveResultFun1(TriggerSource_enum trigerSource_e, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }

        public virtual StateComprehensive_enum DealComprehensiveResultFun1(TriggerSource_enum trigerSource_e, int index, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }

        public virtual StateComprehensive_enum DealComprehensiveResultFun1(TriggerSource_enum trigerSource_e, int[] index, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }
        #endregion 第1次拍照

        #region 第2次拍照
        public virtual StateComprehensive_enum DealComprehensiveResultFun2(TriggerSource_enum trigerSource_e, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }

        public virtual StateComprehensive_enum DealComprehensiveResultFun2(TriggerSource_enum trigerSource_e, int index, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }
        public virtual StateComprehensive_enum DealComprehensiveResultFun2(TriggerSource_enum trigerSource_e, int[] index, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }
        #endregion 第1次拍照

        #region 第3次拍照
        public virtual StateComprehensive_enum DealComprehensiveResultFun3(TriggerSource_enum trigerSource_e, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }

        public virtual StateComprehensive_enum DealComprehensiveResultFun3(TriggerSource_enum trigerSource_e, int index, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }

        public virtual StateComprehensive_enum DealComprehensiveResultFun3(TriggerSource_enum trigerSource_e, int[] index, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }
        #endregion 第3次拍照

        #region 第4次拍照
        public virtual StateComprehensive_enum DealComprehensiveResultFun4(TriggerSource_enum trigerSource_e, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }

        public virtual StateComprehensive_enum DealComprehensiveResultFun4(TriggerSource_enum trigerSource_e, int index, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }

        public virtual StateComprehensive_enum DealComprehensiveResultFun4(TriggerSource_enum trigerSource_e, int[] index, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }

        #endregion 第4次拍照

        #region 第5次拍照
        public virtual StateComprehensive_enum DealComprehensiveResultFun5(TriggerSource_enum trigerSource_e, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }

        public virtual StateComprehensive_enum DealComprehensiveResultFun5(TriggerSource_enum trigerSource_e, int index, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }

        public virtual StateComprehensive_enum DealComprehensiveResultFun5(TriggerSource_enum trigerSource_e, int[] index, out Hashtable hashtable)
        {
            hashtable = null;
            return StateComprehensive_enum.False;
        }

        #endregion 第5次拍照

        #endregion 响应外部触发

        #region 内存泄露
        void g_DealComprehensiveBase_AlarmMemory_event(string str)
        {
            try
            {
                g_UCAlarm.AddInfo(str);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 内存泄露

        #region 添加显示日志
        protected static void ShowState(string str)
        {
            try
            {
                g_UCStateWork.AddInfo(str);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 记录日志 但是不显示
        /// </summary>
        /// <param name="str"></param>
        protected static void ShowState_Hidden(string str)
        {
            g_UCStateWork.AddInfo(str, false);

        }

        protected static void ShowAlarm(string str)
        {
            try
            {
                g_UCAlarm.AddInfo(str);
                ShowState(str);
            }
            catch (Exception ex)
            {

            }
        }
        #endregion 添加显示日志

        public string GetTimeName(DateTime dt)
        {
            try
            {
                //用时间命名
                string Year = dt.Year.ToString();
                string Month = dt.Month.ToString().PadLeft(2, '0');
                string Day = dt.Day.ToString().PadLeft(2, '0');
                string Hour = dt.Hour.ToString().PadLeft(2, '0');
                string Minute = dt.Minute.ToString().PadLeft(2, '0');
                string Second = dt.Second.ToString().PadLeft(2, '0');
                string Mill = dt.Millisecond.ToString().PadLeft(3, '0');
                string name = Year + "-" + Month + "-" + Day + "-" + Hour + "-" + Minute + "-" + Second + "-" + Mill;

                return name;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
