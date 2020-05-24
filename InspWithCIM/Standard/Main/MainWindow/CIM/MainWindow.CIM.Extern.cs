using BasicClass;
using DealCIM;
using DealPLC;
using DealRobot;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Main
{
    partial class MainWindow
    {
        /// <summary>
        /// 在plc进行上卡塞的时候触发，重置cim相关数据，主要是上一卡塞的chipid数据
        /// </summary>
        public void TriggerResetCimData()
        {
            try
            {
                if (File.Exists(CIM.Path_ChipIDList))
                    File.Delete(CIM.Path_ChipIDList);
                CIM.ClearChipID();
                ShowState(string.Format("重置数据，当前卡塞记录:{0}", CIM.GetChipIDCnt()));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 进行runcard认证
        /// </summary>
        public void TriggerVerifyRunCard()
        {
            try
            {
                ShowState("收到RunCard请求");
                if (ModelParams.DefaultRunCardOK)
                {
                    ShowState("默认RunCardOK，通知PLC上卡塞");
                    LogicPLC.L_I.WriteRegData1((int)DataRegister1.LotNum, 60);
                    LogicPLC.L_I.WriteRegData1((int)DataRegister1.RunCardResult, 1);
                }
                else
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        PostLotWnd wnd = PostLotWnd.GetInstance();
                        wnd.Show();
                    }));
                }

                TriggerResetCimData();
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 外部触发读码，区分plc和robot
        /// </summary>
        public void TriggerGetCode(bool isPLC)
        {
            try
            {
                ShowState("触发读码");
                if (!ModelParams.DefaultQrCodeOK)
                {
                    Thread.Sleep(ModelParams.CodeWaitTimeBefore);
                    Code.Write();
                    Thread.Sleep(ModelParams.CodeWaitTimeAfter);
                    Code.StartMonitor(PostParams.P_I.iCodeDelay);
                }
                else
                {
                    if (isPLC)
                    {
                        ShowState("屏蔽二维码，默认OK");
                        new Task(() =>//直接吹会拖慢PLC读取线程
                        {
                            //为了在调机阶段准确估算节拍
                            Thread.Sleep(500);
                            LogicPLC.L_I.WriteRegData1((int)DataRegister1.QrCodeResult, 1);
                            LogicPLC.L_I.WriteRegData1((int)DataRegister1.ChipIDResult, 1);
                        }).Start();

                    }
                    else
                    {
                        SendCodeResult(OK);
                        SendCIMResult(PostType.ChipID, OK);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 将二维码转移到平台
        /// </summary>
        /// <param name="code"></param>
        public void TransPlatCode(string code)
        {
            try
            {
                RegeditMain.R_I.CodePlat = code;
                ShowState("Plat交接成功，Plat持有ChipID：" + RegeditMain.R_I.CodePlat);
                SetForkChipID(RegeditMain.R_I.CodePlat);
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 将二维码转移到arm2
        /// </summary>
        /// <param name="code"></param>
        public void TransArm2Code(string code)
        {
            try
            {
                RegeditMain.R_I.CodeArm2 = code;
                ShowState("Arm2交接成功，Arm2持有ChipID：" + RegeditMain.R_I.CodeArm2);
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 插栏完成,的时候把fork上的chipid加到list当中保存
        /// </summary>
        public void TriggerAppend()
        {
            try
            {
                ShowState("插栏成功:" + RegeditMain.R_I.CodeFork);
                CIM.AppendChipIDList(RegeditMain.R_I.CodeFork);
                ShowState("当前卡塞账料数:" + CIM.ChipIDCount.ToString());
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 卡塞插栏完成需要出卡塞的时候触发，进行trackout过账的同时，弹出下一个刷lot的界面
        /// </summary>
        public void TriggerUploadTrackout()
        {
            try
            {
                ShowState("进行TrackOut过账");
                if (ModelParams.DefaultTrackOutOK)
                {
                    ShowState("TrackOut默认OK，通知PLC出卡塞");
                    LogicPLC.L_I.WriteRegData1((int)DataRegister1.TrackOutResult, 1);
                }
                else
                {
                    UploadTrackOut();
                }

                ShowState("收到RunCard请求");
                if (ModelParams.DefaultRunCardOK)
                {
                    ShowState("默认RunCardOK，通知PLC上卡塞");
                    LogicPLC.L_I.WriteRegData1((int)DataRegister1.RunCardResult, 1);
                }
                else
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        PostLotWnd wnd = PostLotWnd.GetInstance();
                        wnd.Show();
                    }));
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// fork交接成功时触发，将chipid转移到fork上，以供后续插栏
        /// </summary>
        /// <param name="code"></param>
        public void TransForkCode(string code)
        {
            try
            {
                RegeditMain.R_I.CodeFork = code;
                if (ModelParams.IfCimOn && CIM.CheckDup(RegeditMain.R_I.CodeFork))
                {
                    ShowAlarm("交接Fork重复ChipID：" + RegeditMain.R_I.CodeFork);
                    LogicPLC.L_I.WriteRegData1((int)DataRegister1.PCAlarm, (int)PCArarm_Enum.ChipIDError);
                    return;
                }
                ShowState("Fork交接成功，Fork持有ChipID：" + RegeditMain.R_I.CodeFork);
                SetForkChipID(RegeditMain.R_I.CodeFork);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
    }
}
