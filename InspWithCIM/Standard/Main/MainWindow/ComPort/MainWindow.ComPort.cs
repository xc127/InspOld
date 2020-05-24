using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealComInterface;
using BasicClass;
using Common;
using System.Threading;
using System.Threading.Tasks;
using DealConfigFile;
using System.Windows;

namespace Main
{
    public partial class MainWindow
    {
        #region 定义
        bool g_blConnectAckReceived;
        bool g_blNewModelAckReceived;
        bool g_blNewModelResult;
        #endregion 定义

        #region 初始化
        //事件注册
        void LoginEvent_ComInterface()
        {
            try
            {
                //如果没有机器人通信
                if (ParComInterface.P_I.TypeComInterface_e == TypeComInterface_enum.Null)
                {
                    return;
                }
                //触发拍照
                LogicComInterface.L_I.g_DealProtocol.expTrigCamera.Camera1_event += new TrrigerSourceAction_del(DealComprehensive_Camera1_event);
                LogicComInterface.L_I.g_DealProtocol.expTrigCamera.Camera2_event += new TrrigerSourceAction_del(DealComprehensive_Camera2_event);
                LogicComInterface.L_I.g_DealProtocol.expTrigCamera.Camera3_event += new TrrigerSourceAction_del(DealComprehensive_Camera3_event);
                LogicComInterface.L_I.g_DealProtocol.expTrigCamera.Camera4_event += new TrrigerSourceAction_del(DealComprehensive_Camera4_event);
                LogicComInterface.L_I.g_DealProtocol.expTrigCamera.Camera5_event += new TrrigerSourceAction_del(DealComprehensive_Camera5_event);
                LogicComInterface.L_I.g_DealProtocol.expTrigCamera.Camera6_event += new TrrigerSourceAction_del(DealComprehensive_Camera6_event);
                //新建型号--运控端发起
                LogicComInterface.L_I.g_DealProtocol.expNewProduction.NewProduct_event += new Str2Action(LogicComInterface_Inst_NewModel_event);
                //新建型号确认--视觉端发起，接收运控端的确认信号
                LogicComInterface.L_I.g_DealProtocol.expNewProduction.NewProductResult_event += new StrAction(LogicComInterface_Inst_NewModelResult_event);


            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        #endregion  初始化

        #region 通用接口
        //打开通用接口       
        void Init_ComInterface()
        {
            try
            {
                if (ParComInterface.P_I.TypeComInterface_e == TypeComInterface_enum.Null)
                {
                    return;
                }
                switch (ParComInterface.P_I.StateComInterface_e)
                {
                    case StateComInterface_enum.AllError:
                        ShowAlarm("通用端口通信失败");
                        break;

                    case StateComInterface_enum.AllTrue:
                        ShowState("通用端口通信成功");

                        //开始握手
                        Task task = new Task(ComInterfaceShake);
                        task.Start();

                        break;

                    case StateComInterface_enum.ReadTrue:
                        ShowState("通用写入端口通信失败");
                        ShowAlarm("通用写入端口通信失败");
                        break;

                    case StateComInterface_enum.WriteTrue:
                        ShowState("通用读取端口通信失败");
                        ShowAlarm("通用读取端口通信失败");
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        #endregion 通用接口

        #region 握手
        void ComInterfaceShake()
        {
            //发送连接成功的确认信号
            try
            {
                g_blConnectAckReceived = false;
                LogicComInterface.L_I.g_DealProtocol.CmdHeader = "M1 V1 ";//由通用端口1发送给视觉1的信号
                LogicComInterface.L_I.CmdHeader = "V1 M1 ";//由视觉1发给通用端口1的信号
                //注册事件
                LogicComInterface.L_I.g_DealProtocol.expOkAck.ConnectAck_event += new Action(ReceiveConnectAck);
                LogicComInterface.L_I.Connect();//发送确认信号  CN 01000000 #
                //握手确认信号
                //超时时间为100ms*400，即40秒
                int timeOut = 0;
                while (timeOut < 400 && !g_blConnectAckReceived)//未超时且未收到确认，则一直等待
                {
                    Thread.Sleep(100);
                    timeOut++;
                }
                if (g_blConnectAckReceived)
                {
                    ShowState("通用端口握手成功！");
                }
                else
                {
                    ShowState("通用端口握手超时！");
                }
                //注销连接成功的事件
                LogicComInterface.L_I.g_DealProtocol.expOkAck.ConnectAck_event -= new Action(ReceiveConnectAck);
            }
            catch (Exception)
            {
                ShowState("通用端口握手出错！");
            }
        }

        //判断是否收到运控卡的确认信号
        void ReceiveConnectAck()
        {
            g_blConnectAckReceived = true;
        }

        #endregion 握手

        #region 换型处理
        void LogicComInterface_Inst_NewModel_event(string No, string Name)
        {
            try
            {
                if (ComConfigPar.C_I.NameModel == Name.Trim())
                {
                    MessageBox.Show("新建的配置文件已经存在，禁止再次创建!");
                    return;
                }

                ParComInterface.P_I.NameModel = Name; //读取配置文件名称
                ParComInterface.P_I.intNo = int.Parse(No); //读取配置文件序号
                //读取配置参数（是单独信号触发，这里比DealPLC暂略）

                ComConfigPar.C_I.PathOldConfigIni = ComConfigPar.C_I.PathConfigIni;
                //新建文件路径
                ComConfigPar.C_I.PathConfigIni = ComValue.c_PathPar + ParComInterface.P_I.NameModel + "Product.ini";
                ComConfigPar.C_I.NameModel = ParComInterface.P_I.NameModel;//新的型号

                NewModel();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        //上位机换型则触发，
        bool ComInterfaceNewModel()
        {
            try
            {
                g_blNewModelAckReceived = false;
                LogicComInterface.L_I.g_DealProtocol.expNewProduction.NewProductResult_event += new StrAction(LogicComInterface_Inst_NewModelResult_event);
                LogicComInterface.L_I.NewProduct(0, ComConfigPar.C_I.NameModel);//忽略型号的序号，只保留型号名称
                int timeOut = 0; //等待换型成功的Ack超时时间设置为5s
                while (timeOut < 50 && !g_blNewModelAckReceived)//未超时且未收到确认，则一直等待
                {
                    Thread.Sleep(100);
                    timeOut++;
                }
                if (g_blNewModelAckReceived)
                {
                    if (g_blNewModelResult)
                    {
                        ShowState("通用控制器换型成功！");
                    }
                    else
                    {
                        ShowState("通用控制器换型出错！");
                    }
                }
                else
                {
                    ShowState("通用控制器换型超时！");
                }
                return true;
            }
            catch
            {
                ShowState("通用控制器换型出错！");
                return false;
            }
        }

        void LogicComInterface_Inst_NewModelResult_event(string State)
        {
            if (State == "OK")
            {
                g_blNewModelResult = true; //换型结果
            }
            else
            {
                g_blNewModelResult = false;
            }
            g_blNewModelAckReceived = true; //收到换型反馈
        }

        #endregion 换型处理
    }
}
