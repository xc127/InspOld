using BasicClass;
using DealCIM;
using DealPLC;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Main
{
    partial class MainWindow
    {
        #region 定义    
        PostInfo postInfo = new PostInfo();

        delegate void SetCimDelegate(string content, bool isAlarm);
        SetCimDelegate[] SetCimResult = new SetCimDelegate[3] { SetChipIDResult, SetLotResult, SetTrackOutResult };
        #endregion

        #region 上报
        public void UploadChipid(object chipid)
        {
            new Task(new Action(() =>
            {
                string id = chipid as string;
                postInfo.type = PostType.ChipID;
                SetArmChipID(id);
                SetCimResult[(int)postInfo.type]("ChipID上报中...", false);
                //string returnValue = PostHelper.PostChipID(id, PostParams.P_I.StrModelNo, out string key);
                string returnValue = PostHelper.PostChipID(id, PostParams.P_I.StrModelNo, xmlCreater, out string key);
                if (returnValue == CIM.ReturnOK)
                {
                    SetCimResult[(int)postInfo.type]("ChipID上报成功", false);
                    postInfo.correlationID = key;
                    Task.Factory.StartNew(FindReception, postInfo);
                    //FindReception(postInfo);
                }
                else
                {
                    SetChipIDResult(returnValue + "-ChipID上报失败:" + id, true);
                    //TODO 通知plc？Bot?
                    SendCIMResult(postInfo.type, NG);
                }
            })).Start();
        }

        public void UploadLot()
        {
            new Task(new Action(() =>
            {
                postInfo.type = PostType.Lot;
                SetCimResult[(int)postInfo.type]("Lot上报中...", false);
                SetLot();

                //string returnValue = PostHelper.PostLot(PostParams.P_I.StrModelNo, out string key);
                string returnValue = PostHelper.PostLot(PostParams.P_I.StrModelNo, xmlCreater, out string key);
                if (returnValue == CIM.ReturnOK)
                {
                    SetCimResult[(int)postInfo.type]("Lot上报成功", false);
                    postInfo.correlationID = key;
                    Task.Factory.StartNew(FindReception, postInfo);
                }
                else
                {
                    SetLotResult(returnValue + "-Lot上报失败", true);
                    //TODO 通知plc?
                    SendCIMResult(postInfo.type, NG);
                }
            })).Start();
        }

        public void UploadTrackOut()
        {
            new Task(new Action(() =>
            {
                postInfo.type = PostType.TrackOut;
                SetCimResult[(int)postInfo.type]("Trackout上报中...", false);
                CIM.LoadList(CIM.LotNum);
                ShowState("当前卡塞统计账料数：" + CIM.GetChipIDCnt().ToString());
                if (CIM.ChipIDCount != CIM.LotNum)
                {
                    ShowAlarm("统计账料数与LotNum不符，无法过账，请重新确认");
                    SendCIMResult(postInfo.type, NG);
                    return;
                }

                //string returnValue = PostHelper.PostTrackOut(CIM.GetList(), PostParams.P_I.StrModelNo, out string key);
                string returnValue = PostHelper.PostTrackOut(
                    CIM.GetList(), PostParams.P_I.StrModelNo, xmlCreater, out string key);
                if (returnValue == CIM.ReturnOK)
                {
                    SetCimResult[(int)postInfo.type]("Trackout上报成功", false);
                    postInfo.correlationID = key;
                    Task.Factory.StartNew(FindReception, postInfo);
                }
                else
                {
                    SetTrackOutResult(returnValue + "-Trackout上报失败", true);
                    //TODO 通知plc？
                    SendCIMResult(postInfo.type, NG);
                }
            })).Start();
        }
        #endregion

        /// <summary>
        /// 查询对应的cim返回结果
        /// </summary>
        /// <param name="param"></param>
        public void FindReception(object param)
        {
            try
            {
                //TODO
                PostInfo info = (PostInfo)param;
                ShowState("开始读取CIM返回结果");
                CIM.AddIDToList(info.correlationID);

                ShowState("读取过账结果");

                int cnt = 0;
                while (cnt < PostParams.P_I.iCycTimes)
                {
                    Thread.Sleep(150);
                    if (CheckCimResult(info))
                        break;
                    cnt++;
                }
                if (cnt == PostParams.P_I.iCycTimes)
                {
                    SendCIMResult(info.type, NG);
                    SetCimResult[(int)info.type]("获取" + info.type.ToString() + "返回结果超时", true);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("CIM", ex);
            }
        }

        public bool CheckCimResult(PostInfo info)
        {
            try
            {
                if (ReceiveHelper.CheckResult(info.correlationID, out bool value))
                {
                    if (value)
                    {
                        //CIM返回结果是有效
                        //TODO
                        //通知plc可继续运行
                        SetCimResult[(int)info.type]("获取" + info.type.ToString() + "返回结果OK", false);
                        SendCIMResult(info.type, OK);
                    }
                    else
                    {
                        //返回结果无效
                        //TODO
                        //通知plc抛料
                        ShowAlarm(xmlParser.GetErrorMsg());
                        SetCimResult[(int)info.type]("获取" + info.type.ToString() + "返回结果NG", true);
                        SendCIMResult(info.type, NG);
                    }
                    return true;
                }
                //TODO返回结果超时
                return false;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("CIM", ex);
                return false;
            }
        }
    }
}
