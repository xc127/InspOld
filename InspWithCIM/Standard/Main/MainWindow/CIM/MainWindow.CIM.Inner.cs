using BasicClass;
using DealCIM;
using DealPLC;
using DealRobot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Main
{
    partial class MainWindow
    {
        /// <summary>
        /// CIM重连
        /// </summary>
        public void ReconnectCIM()
        {
            if (CIM.C_I.ReConnect())
                SetCimStatus("CIM连接成功", false);
            else
                SetCimStatus("CIM连接失败", true);
        }

        /// <summary>
        /// 关闭CIM端口
        /// </summary>
        public void CloseCIM()
        {
            try
            {
                //Code.Close();
                //SetCodeStatus("二维码连接断开", true);
                StopMonitor();
                CIM.C_I.DisConnect();
                SetCimStatus("CIM连接断开", true);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        public void StopMonitor()
        {
            new Task(() =>
            {
                if (MonitorTask != null && !MonitorTask.IsCanceled)
                {
                    ReceiveHelper.cts.Cancel();
                    Task.WaitAll(MonitorTask);
                    ShowState("CIM返回结果扫描停止");
                    ReceiveHelper.cts.Dispose();
                    ReceiveHelper.cts = new CancellationTokenSource();
                }
            }).Start();
        }

        public void DisconnectCode()
        {
            try
            {
                Code.Close();
                SetCodeStatus("二维码连接断开", true);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 根据cim返回结果，发送对应的数据到寄存器或者机器人
        /// </summary>
        /// <param name="type">结果类型，chipid/lot/trackout</param>
        /// <param name="result"></param>
        void SendCIMResult(PostType type, int result)
        {
            try
            {
                if (type == PostType.ChipID && !isPLC)
                {
                    LogicRobot.L_I.WriteRobotCMD(new Point4D(result, 0, 0, 0), ModelParams.cmd_ChipIDValid);
                    return;
                }

                LogicPLC.L_I.WriteRegData1(plc_addr[(int)type], result);

                if (result == NG)
                {
                    if (type == PostType.ChipID)
                        CIM.ChipIDNGCount++;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 发送读码结果
        /// </summary>
        /// <param name="result"></param>
        void SendCodeResult(int result)
        {
            try
            {
                if (isPLC)
                    LogicPLC.L_I.WriteRegData1((int)DataRegister1.QrCodeResult, result);
                else
                    LogicRobot.L_I.WriteRobotCMD(new Point4D(result, 0, 0, 0), ModelParams.cmd_GetCodeOK);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 发送lotnum给plc
        /// </summary>
        /// <param name="num"></param>
        void WriteLotNum(int num)
        {
            LogicPLC.L_I.WriteRegData1((int)DataRegister1.LotNum, num);
        }
    }
}
