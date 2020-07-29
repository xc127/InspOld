using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Threading.Tasks;
using DealPLC;
using Common;
using DealRobot;
using System.IO;
using DealFile;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using DealComprehensive;
using SetPar;
using BasicClass;
using DealConfigFile;
using DealCIM;

namespace Main
{
    partial class MainWindow
    {
        #region 定义
        int g_NumInsert = 0;
        bool BlCIMOn = false;
        #endregion 定义

        /// <summary>
        /// 保留触发1
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve1_event(TriggerSource_enum trigerSource_e, int i)
        {
            try
            {
                DealComprehensiveResult1.D_I.BlCyclePhotoStop = true;
                DealComprehensiveResult2.D_I.BlCyclePhotoStop = true;
                DealComprehensiveResult3.D_I.BlCyclePhotoStop = true;
                DealComprehensiveResult4.D_I.BlCyclePhotoStop = true;
                ShowState("PLC通知单边检测停止");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发2
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve2_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                //ShowState("PLC通知相机2检测停止");
                //DealComprehensiveResult2.D_I.BlCyclePhotoStop = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发3 表示PLC交接搬运成功
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve3_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                //ShowState("PLC通知相机3检测停止");
                //DealComprehensiveResult3.D_I.BlCyclePhotoStop = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发4
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve4_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                //ShowState("PLC通知相机4检测停止");
                //DealComprehensiveResult4.D_I.BlCyclePhotoStop = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发5
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve5_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                ShowState("PLC通知所有检测结束");
                BaseDealComprehensiveResult.CommunicateInspResult();
                if(ModelParams.IfSegmentDelImage)
                {
                    Log.L_I.WriteError("DELIMAGE", DealComprehensiveResult1.D_I.BasePathImageSave);
                    DeleteFilesOneByOnce(DealComprehensiveResult1.D_I.BasePathImageSave
                        , ModelParams.RetentionTime);
                    //DeleteFilesOneByOnce(@"E:\Image\NGImage\", 10);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        public void DeleteFilesOneByOnce(string rootPath, int offsetDays)
        {
            try
            {
                //get today
                DateTime datethreshold = DateTime.Today;
                //calculate threshold
                datethreshold = datethreshold.AddDays(-offsetDays);
                //get directory date which should be deleted
                string[] fdate = Directory.GetDirectories(rootPath).Where(x => Directory.GetCreationTime(x) < datethreshold).ToArray();

                if (fdate.Length > 0)
                {
                    foreach (string dirDate in fdate)
                    {
                        //get directories in first date directory-first directory named by hour
                        string[] fhour = Directory.GetDirectories(dirDate);
                        if (fhour.Length > 0)
                        {
                            foreach (string dirHour in fhour)
                            {
                                string[] fcode = Directory.GetDirectories(dirHour);
                                if (fcode.Length > 0)
                                {
                                    Directory.Delete(fcode[0], true);
                                    Log.L_I.WriteError("DELIMAGE", fcode[0]+ datethreshold.ToLongDateString());
                                    return;
                                }
                                else
                                {
                                    Log.L_I.WriteError("DELIMAGE", dirHour + datethreshold.ToLongDateString());
                                    Directory.Delete(dirHour);
                                }
                            }
                        }
                        else
                        {
                            Log.L_I.WriteError("DELIMAGE", dirDate + datethreshold.ToLongDateString());
                            Directory.Delete(dirDate);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError("DELIMAGE", ex);
            }
            finally
            {
            }
        }

        /// <summary>
        /// 保留触发6
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve6_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                TriggerVerifyRunCard();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发7
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve7_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                //TODO: 二维码拍照
                TriggerGetCode(isPLC);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发8
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve8_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                TransPlatCode(RegeditMain.R_I.CodeArm);
                BaseDealComprehensiveResult.Init();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发9
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve9_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                TransForkCode(RegeditMain.R_I.CodeArm2);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发10
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve10_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                ShowState("PLC通知插栏成功");
                if (ModelParams.IfCimOn)
                {
                    TriggerAppend();

                    int num = (int)LogicPLC.L_I.ReadRegData1((int)DataRegister1.NumInCST);
                    ShowState("PLC处统计插栏数量：" + num);
                    if (CIM.ChipIDCount == num)
                    {
                        ShowState("插栏数量核对OK");
                        LogicPLC.L_I.WriteRegData1((int)DataRegister1.HandConfirm, 1);
                    }
                    else
                    {
                        ShowAlarm("chipID数量与PLC插栏数量不匹配！");
                        LogicPLC.L_I.WriteRegData1((int)DataRegister1.HandConfirm, 2);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发11,发送泡棉
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve11_event(TriggerSource_enum trrigerSource_e, int i)
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
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发12
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve12_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                TransArm2Code(RegeditMain.R_I.CodePlat);
                //SetForkChipID(RegeditMain.R_I.CodePlat);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发13
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve13_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                ShowState("交接到下游成功:" + RegeditMain.R_I.CodeArm2);
                CIM.AppendChipIDList(RegeditMain.R_I.CodeArm2);
                ShowState("当前交接到下游账料数:" + CIM.ChipIDCount.ToString());

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 残材统计
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve14_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                string content = "残材平台处";
                if (i == 1)
                    content += "相机3有残材";
                else if (i == 2)
                    content += "相机4有残材";
                else if (i == 3)
                    content += "相机3/4都有残材";

                RecordWastageData(RegeditMain.R_I.CodeArm, content);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        protected void RecordWastageData(string chipid, string data)
        {
            TxtFile t_I = new TxtFile();
            try
            {
                string root = ParPathRoot.PathRoot + "软件运行记录\\RecordData\\Wastage\\";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                string pathDir = Log.CreateAllTimeFile(root);

                string path = pathDir + "WastageLog" + ".txt";
                string name = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() +
                    ":" + DateTime.Now.Second.ToString() + ":" + DateTime.Now.Millisecond.ToString();
                t_I.WriteText(path, name + "\n----->ID: " + chipid.ToString() + "\n----->" + data);//写入时间
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 保留触发15
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve15_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                ShowState("PLC触发换班，产能重置");                
                ProductivityReport.WriteReportIni(i);
                ProductivityReport.ClearReportData();

                AccessHelper acHelper = new AccessHelper(@"D:\Insp.accdb", TableEnum.InspTable);
                string tableName = TableEnum.InspTable.ToString();
                acHelper.CreateAccDB(@"D:\Insp.accdb", tableName);

                List<string> data = new List<string>();
                data.Add(RegeditMain.R_I.AccessIndex++.ToString());
                data.Add(DateTime.Today.ToOADate().ToString());
                data.Add(ParAnalysis.P_I.g_ProductNumInfoNow.NumAll.ToString());
                data.Add(ParAnalysis.P_I.g_ProductNumInfoNow.NumNGShell.ToString());
                data.Add(ParAnalysis.P_I.g_ProductNumInfoNow.NumNGCorner.ToString());
                data.Add(ParAnalysis.P_I.g_ProductNumInfoNow.NumNGOther.ToString());
                acHelper.Insert(data, tableName);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发16
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve16_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {
                ShowState("进行ByPiece过账");
                if (ModelParams.DefaultTrackOutOK)
                {
                    ShowState("ByPiece默认OK");
                    LogicPLC.L_I.WriteRegData1((int)DataRegister1.TrackOutResult, 1);
                }
                else
                {
                    UploadByPieces(RegeditMain.R_I.CodeFork);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发17
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve17_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发18
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve18_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发19
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve19_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }

        /// <summary>
        /// 保留触发20
        /// </summary>
        /// <param name="trrigerSource_e"></param>
        /// <param name="i"></param>
        void LogicPLC_Inst_Reserve20_event(TriggerSource_enum trrigerSource_e, int i)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
    }
}
