using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControlLib;
using Common;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Camera;
using HalconDotNet;
using DealFile;
using SetPar;
using DealPLC;
using DealConfigFile;
using BasicClass;

namespace Main
{
    partial class MainWindow
    {
        #region 定义
        //bool

        //Mutex
        Mutex g_mutexError = new Mutex();
        #endregion 定义

        #region 初始化
        //事件注册
        void LoginEvent_Others()
        {
            try
            {
                ParConfigPar.P_I.ConfigParError_Event += new System.Action(ConfigPWorkIni_Inst_ConfigParError_Event);
                //错误事件
                Log.L_I.ErrorInfo_event += new ErrorAction(Log_Inst_ErrorInfo_event);
               
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        void Init_Others()
        {
                   
        }

        void ConfigPWorkIni_Inst_ConfigParError_Event()
        {
            ShowAlarm("配置参数读取错误");
            LogicPLC.L_I.PCAlarm();
        }        
        #endregion 初始化       

        #region 显示错误信息
        void Log_Inst_ErrorInfo_event(Exception ex,string strTime)
        {
          
        }
        #endregion 显示错误信息

        #region 事件注销
        void EventLogOut_Others()
        {
            try
            {
                ParConfigPar.P_I.ConfigParError_Event -= new System.Action(ConfigPWorkIni_Inst_ConfigParError_Event);
                Log.L_I.ErrorInfo_event -= new ErrorAction(Log_Inst_ErrorInfo_event);
               
            }
            catch(Exception  ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 事件注销
    }
}
