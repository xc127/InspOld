using BasicClass;
using DealCIM;
using DealPLC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Main
{
    partial class MainWindow
    {

        /// <summary>
        /// 注册
        /// </summary>
        public void LoginEvent_CIM()
        {
            try
            {
                QRCodeBase.GetData_event += new StrAction(C_I_ReadData_event);
                QRCodeBase.OverTime_event += new Action(OverTime_event);
                XMLHelpler.UpdataLotNum_event += new IntAction(UpdateLotNum_event);
                XmlParserBase.UpdataLotNum_event += new IntAction(UpdateLotNum_event);
                XMLHelpler.ErrorMsg_event += new StrAction(ErrorMsg_event);
                //XMLHelpler.JudgeOX_event += new StrAction(JudgeOX_event);
                XmlParserBase.JudgeOX_event += new StrAction(JudgeOX_event);
                XmlParserBase.PatternList_event += new StrAction(PatternList_event);
                PostTrackout.UpLoadChipID += new Action<object>(UploadChipid);
                PostTrackout.UpLoadLot += new Action(UploadLot);
                PostTrackout.UpLoadTrackOut += new Action(UploadTrackOut);
                PostTrackout.AddAccount += new StrAction(AddAccount_event);
                PostLotWnd.UpLoadLot += new Action(UploadLot);
                WndCimMode.SetCimStatus_event += new Action(SetCimStatus);
                ReceiveHelper.DataFlowAlarm_event += new Action(DataFlowAlarm_event);

                ReceiveHelper.Monitor_event += new IntAction(Monitor_event);
                ReceiveHelper.FinishMonitor_event += new Action(FinishMonitor_event);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 二维码读取事件处理程序
        /// </summary>
        /// <param name="strCode"></param>
        void C_I_ReadData_event(string strCode)
        {
            try
            {
                //string chipid = Regex.Replace(strCode, "[^a-z0-9]", "", RegexOptions.IgnoreCase);
                string chipid = Regex.Match(strCode, @"[A-z0-9]+-?[A-z0-9]+").ToString();
                if (chipid == "")
                {
                    CIM.CodeNGCount++;
                    RegeditMain.R_I.CodeArm = "FAILED";
                    ShowState("读码失败,Arm持有ChipID:" + RegeditMain.R_I.CodeArm);
                    ShowAlarm("二维码读取失败");
                    if (ModelParams.IfPassCodeNG)
                    {
                        SendCodeResult(OK);
                        ShowState("启用PASS读码失败，不抛料");
                    }
                    else
                    {
                        SendCodeResult(NG);
                    }
                    return;
                }
                else if (chipid.Length < ModelParams.CodeLength)
                {
                    CIM.CodeNGCount++;
                    RegeditMain.R_I.CodeArm = chipid;
                    ShowState("二维码长度与设定不符,Arm持有ChipID:" + RegeditMain.R_I.CodeArm);
                    ShowAlarm("二维码长度与设定不符");
                    if (ModelParams.IfPassCodeNG)
                    {
                        SendCodeResult(OK);
                        ShowState("启用PASS读码失败，不抛料");
                    }
                    else
                    {
                        SendCodeResult(NG);
                    }
                    return;
                }
                else
                {
                    SendCodeResult(OK);
                    RegeditMain.R_I.CodeArm = chipid;
                    ShowState("读码成功,Arm持有ChipID:" + RegeditMain.R_I.CodeArm);

                    if (CIM.CheckDup(RegeditMain.R_I.CodeArm))
                    {
                        ShowAlarm("读取到重复ChipID：" + RegeditMain.R_I.CodeArm);
                        LogicPLC.L_I.WriteRegData1((int)DataRegister1.QrCodeResult, NG);
                        return;
                    }
                }

                //如果软件运行模式确定要进行过账，则开线程进行chipid的上报
                if (!ModelParams.DefaultChipIDOK)
                    Task.Factory.StartNew(new Action<object>(UploadChipid), chipid);
                else
                {
                    LogicPLC.L_I.WriteRegData1((int)DataRegister1.ChipIDResult, OK);
                    ShowState("ChipID过账默认OK，通知plc过账成功");
                }
                WriteCodeToRegister(chipid);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 从lot中读取lotnum
        /// </summary>
        /// <param name="lotnum"></param>
        void UpdateLotNum_event(int lotnum)
        {
            try
            {
                ShowState("从Lot中获取LotNum：" + lotnum.ToString() + ",并发送给PLC");
                CIM.LotNum = lotnum;
                PostParams.P_I.StrLot = PostParams.P_I.StrTempLot;
                SetLot();
                WriteLotNum(lotnum);
                PostParams.P_I.WriteCimConfig();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 读码超时事件
        /// </summary>
        void OverTime_event()
        {
            ShowAlarm("二维码读取超时");
            CIM.CodeNGCount++;
            SendCodeResult(NG);
        }
        /// <summary>
        /// 读取cim反馈结果ng时，显示errormessage
        /// </summary>
        /// <param name="msg"></param>
        void ErrorMsg_event(string msg)
        {
            ShowAlarm(msg);
        }
        void JudgeOX_event(string msg)
        {
            ShowState("OX结果为:" + msg);
            if (msg == "X")
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.IfRisk, 1);
        }

        void PatternList_event(string msg)
        {
            ShowState("Pattern_List结果为:" + msg);
            if (!string.IsNullOrEmpty(msg))
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.IfPatternListValid, 1);
        }
        /// <summary>
        /// 手动加账功能
        /// </summary>
        void AddAccount_event(string code)
        {
            try
            {
                if (code != string.Empty)
                    RegeditMain.R_I.CodeArm = code;

                if (RegeditMain.R_I.CodeArm == string.Empty || RegeditMain.R_I.CodeArm == "FAILED")
                {
                    ShowAlarm("当前Arm持有ChipID为空，加账失败");
                    return;
                }

                if (!CIM.AppendChipIDList(RegeditMain.R_I.CodeArm))
                {
                    ShowAlarm("重复二维码：" + RegeditMain.R_I.CodeArm);
                    return;
                }
                ShowState("加账成功:" + RegeditMain.R_I.CodeArm);
                ShowState("目前账料总数：" + CIM.GetChipIDCnt().ToString());
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        public void DataFlowAlarm_event()
        {
            LogicPLC.L_I.WriteRegData1((int)DataRegister1.PCAlarm, (int)PCArarm_Enum.DataFlow);
        }

        public void Monitor_event(int cnt)
        {
            ShowState(string.Format("开始第{0}次从CIM系统读取返回结果", cnt));
        }

        public void FinishMonitor_event()
        {
            ShowState("从CIM系统读取返回结果完成");
        }

        void SetCimStatus()
        {
            if (!ModelParams.DefaultQrCodeOK)
            {
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.IfCodeOn, ON);
                ConnectCode();
                //AUO特需
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.IfReadCode, 1);
            }
            else
            {
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.IfCodeOn, OFF);
                DisconnectCode();
                //AUO特需
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.IfReadCode, 0);
            }

            if (ModelParams.IfCimOn)
            {
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.IfCimOn, ON);
                ConnectCIM();
                //AUO特需
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.IfCheckTrackout, 1);
            }
            else
            {
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.IfCimOn, OFF);
                CloseCIM();
                //AUO特需
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.IfCheckTrackout, 0);
            }

            //AUO特需
            if (!ModelParams.DefaultChipIDOK)
            {
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.IfCheckChipID, 1);
            }
            else
            {
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.IfCheckChipID, 0);
            }
        }

        void WriteCodeToRegister(string code)
        {
            try
            {
                int firstAddr = (int)DataRegister1.CodeToPLC;

                int length = code.Length;

                int pair = length / 2;
                int single = length % 2;

                int[] RegData = new int[pair + single];

                for (int i = 0; i < RegData.Length - 1; ++i)
                {
                    RegData[i] = (int)code[2 * i + 1] * 256 + (int)code[2 * i];
                }
                if (single == 1)
                {
                    RegData[pair] = (int)code[length - 1] * 256;
                }
                else
                {
                    RegData[pair - 1] = (int)code[2 * (pair - 1) + 1] * 256 + (int)code[2 * (pair - 1)];
                }


                int solidAmp = 65536;
                int first = RegData[0];
                int second = length;
                int RealRegSend = first * solidAmp + second;

                LogicPLC.L_I.WriteRegData1(firstAddr, RealRegSend);

                for (int j = 0; j < (RegData.Length - 1) / 2; ++j)
                {
                    first = RegData[j * 2 + 2];
                    second = RegData[j * 2 + 1];
                    RealRegSend = first * solidAmp + second;
                    LogicPLC.L_I.WriteRegData1(firstAddr + j + 1, RealRegSend);
                }
                if ((RegData.Length - 1) % 2 == 1)
                {
                    second = RegData[RegData.Length - 1];
                    RealRegSend = second;
                    LogicPLC.L_I.WriteRegData1(firstAddr + (RegData.Length - 1) / 2 + 1, RealRegSend);
                }
                ShowState("二维码发送到PLC");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("CODE", ex);
                ShowState("二维码发送到PLC失败");
                return;
            }
        }
    }
}
