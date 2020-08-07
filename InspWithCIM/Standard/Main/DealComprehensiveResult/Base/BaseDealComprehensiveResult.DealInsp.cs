using System;
using System.Collections.Generic;
using System.Text;
using DealPLC;
using System.Threading;
using BasicClass;
using System.IO;
using BasicDisplay;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using DealConfigFile;
using DealImageProcess;
using ParComprehensive;
using DealResult;
using HalconDotNet;
using System.Collections;
using Microsoft.VisualBasic.Devices;
using System.Linq;
using DealFile;
using DealRobot;
using System.Configuration;

namespace Main
{
    public partial class BaseDealComprehensiveResult : BaseClass
    {
        #region 定义
        readonly string BaseDir = ConfigurationManager.AppSettings["dir"];

        public static int NoPicture_Cam1Side1 = 0;
        public static int NoPicture_Cam1Side2 = 0;
        public static int NoPicture_Cam2Side1 = 0;
        public static int NoPicture_Cam2Side2 = 0;
        public static int NoPicture_Cam3Side1 = 0;
        public static int NoPicture_Cam3Side2 = 0;
        public static int NoPicture_Cam4Side1 = 0;
        public static int NoPicture_Cam4Side2 = 0;

        public static bool IsAllCompleted = false;

        public ParInspAll g_ParIns = new ParInspAll();
        /// <summary>
        /// 静态的边序号
        /// </summary>
        public static int SideIndex = 0;

        double g_NumAllImage = 0;
        double g_NumInvalid = 0;
        static int NumNG = 0;
        /// <summary>
        /// 一条边的有效图片数
        /// </summary>
        int g_NumImageSide = 0;

        public double RatioInvalid
        {
            get
            {
                double ratio = 0;
                if (g_ParIns != null && g_ParIns.BaseParInspection_L.Count == 2)
                {
                    if (g_ParIns.BaseParInspection_L[0].BlIgnoreThisSideFault
                    || g_ParIns.BaseParInspection_L[1].BlIgnoreThisSideFault)
                    {
                        ratio = 0;
                    }
                    else
                    {
                        g_NumAllImage = g_NumAllImage + 1;
                        ratio = g_NumInvalid / g_NumAllImage;
                    }
                    ShowState(string.Format("相机{0}无效图片比例{1}", g_NoCamera, ratio));
                    return ratio;
                }
                return ratio;
            }
        }

        /// <summary>
        /// 启动时未拍照，停止位true
        /// </summary>
        public bool BlCyclePhotoStop = true;
        public bool BlGrabFinished = false;
        public static bool BlAllFinish = false;
        /// <summary>
        /// 静态的互斥
        /// </summary>
        static Mutex mtxEditResult = new Mutex();
        static Mutex mtxEditSingleResult = new Mutex();

        static AutoResetEvent InspectionCompleted = new AutoResetEvent(false);

        public UCRecord g_UCRecord = null;
        public UCRecordTemp g_UCSingleRecord = null;
        public static UCResultIns g_UCResultInsp = null;

        /// <summary>
        /// 基目录，相机1拍照时初始化
        /// </summary>
        public string BasePathImageSave = ConfigurationManager.AppSettings["dir"];
        public string BasePathNGImageSave = "";
        public string BasePathNGImageDefectPaintedSave = "";
        /// <summary>
        /// 静态的二维码
        /// </summary>
        protected static string CodeNow = "";
        /// <summary>
        /// 静态的二维码状态
        /// </summary>
        protected static bool BlCodeReady = false;
        /// <summary>
        /// 静态的进50次缺陷信息
        /// </summary>
        public static List<ResultInspection> ResultInspCell_L = new List<ResultInspection>() { new ResultInspection() };

        static int NumRemoveWhenReCheck = 0;

        protected bool StartLoop { get; set; }

        protected bool StartProcess { get; set; }
        #endregion
        /// <summary>
        /// 静态变量 存在其特殊性 在相机1第一次拍照之前清空
        /// </summary>
        public static List<ResultInspection> ResultInspSingeCell_L = new List<ResultInspection>();

        #region 图像
        protected Queue<DataImage> ImageNeedDeal_Q = new Queue<DataImage>();

        object sycObjQuene = new object();


        [Obsolete]
        DataImage GetLatestImage()
        {
            DataImage dm;
            try
            {
                //lock (sycObjQuene)
                //{
                dm = ImageNeedDeal_Q.Dequeue();
                //}
                return dm;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //采图间隔时间
        int TimeRest
        {
            get
            {
                int t = Convert.ToInt32(ModelParams.GrabInterval - this.sw.ElapsedMilliseconds);//这里设置位100fps
                return t > 0 ? t : 0;
            }
        }
        /// <summary>
        /// 帧率把控
        /// </summary>
        Stopwatch sw = new Stopwatch();
        Stopwatch swTask = new Stopwatch();
        #endregion

        int imageGrabCnt = 0;
        int imageProcessCnt = 0;
        int ContinueCnt = 0;
        //角结束，用于边起始，边从角开始，先拍到角
        int cornerEnd = 0;
        //角开始，用于边结尾
        int cornerStart = 0;

        bool isCornerPre = false;

        #region 主流程
        protected void CycPhoto()
        {
            ShowState("相机" + g_NoCamera + "采图准备就绪");
            while (true)
            {
                if (!StartLoop)
                {
                    Thread.Sleep(100);
                    continue;
                }

                StartLoop = false;
                //imageGrabCnt = 0;
                try
                {
                    //-xc
                    //while (!BlCodeReady)
                    //{
                    //    Thread.Sleep(3);
                    //}
                    //-
                    ShowState(ParSetDisplay.P_I.g_BaseParSetDisplay_L[g_NoCamera - 1].Title + "开始循环采图");
                    if (ImageNeedDeal_Q.Count > 0)
                        ShowState("相机" + g_NoCamera + "未处理图片：" + ImageNeedDeal_Q.Count);

                    BlGrabFinished = false;
                    BlCyclePhotoStop = false;
                    g_UCDisplayCamera.BlLocalImage = false;

                    //-xc
                    //while (!BlCodeReady)
                    //{
                    //    Thread.Sleep(3);
                    //}
                    //-
                    BasePathImageSave = CreateImageDir();
                    //xc-0115
                    StartProcess = true;
                    ImageNeedDeal_Q.Clear();

                    //xc-0312
                    if (SideIndex == 1)
                    {

                        g_UCDisplayCamera.ClearHalWin();
                    }
                    g_NumImageSide = 0;
                    //FinishPhotoPLC(0);

                    while (!BlCyclePhotoStop)
                    {

                        //xc-0312
                        if (MainCom.M_I.StateMachine_e == StateMachine_enum.Auto)
                        {
                            //g_UCDisplayCamera.BlLocalImage = true;
                            try
                            {
                                sw.Restart();
                                ImageAll im = g_UCDisplayCamera.GrabImageHal();
                                lock (sycObjQuene)
                                {
                                    ImageNeedDeal_Q.Enqueue(new DataImage(im));
                                    imageGrabCnt++;
                                    g_NumAllImage++;
                                }
                                sw.Stop();
                            }
                            catch (Exception ex)
                            {
                                Log.L_I.WriteError("BaseInsp.Grabing", ex.InnerException);
                            }
                        }
                        Thread.Sleep(TimeRest);
                    }
                    ShowState(string.Format(ParSetDisplay.P_I.g_BaseParSetDisplay_L[g_NoCamera - 1].Title + "采图数量：{0}", imageGrabCnt));
                    ShowState(ParSetDisplay.P_I.g_BaseParSetDisplay_L[g_NoCamera - 1].Title + "循环采图结束");
                    BlGrabFinished = true;
                    //-xc
                    BlCodeReady = false;
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError("BaseInsp.CycPhoto", ex.InnerException); ;
                }
            }
        }

        public void DealQueue()
        {
            ShowState("相机" + g_NoCamera + "处理准备就绪");

            while (true)
            {
                if (!StartProcess)
                {
                    Thread.Sleep(100);
                    continue;
                }
                StartProcess = false;

                try
                {
                    ShowState(ParSetDisplay.P_I.g_BaseParSetDisplay_L[g_NoCamera - 1].Title + "开始处理图像");
                    swTask.Restart();
                    string str = "";
                    //if (SideIndex == 1)
                    //{
                    //    NoPicture_Cam1 = 0;
                    //    NoPicture_Cam2 = 0;
                    //}
                    
                    while (ImageNeedDeal_Q.Count > 0 || !BlCyclePhotoStop || g_BlManualCycleTest)
                    {
                        if (ImageNeedDeal_Q.Count == 0)
                        {
                            if (g_BlManualCycleTest)
                            {
                                g_BlManualCycleTest = false;
                                break;
                            }
                            Thread.Sleep(12);
                            continue;
                        }
                        //new Thread(delegate()
                        //{
                        imageProcessCnt++;
                        
                        DataImage dataImage = ImageNeedDeal_Q.Dequeue();
                        if (dataImage == null)
                        {
                            continue;                           
                        }
                            
                        ImageAll im = dataImage.Image;
                        if (im == null)
                        {
                            ShowState("Cam" + g_NoCamera + "im Null Image");
                            LogicPLC.L_I.WriteRegData1((int)DataRegister1.PCAlarm, 1);
                            WinMsgBox.ShowMsgBox("相机" + g_NoCamera + "采集图像im为空 检查相机连接！\r可能需要重新启动软件！");
                            continue;
                        }

                        if (im.Ho_Image == null)
                        {
                            ShowState("Cam" + g_NoCamera + "ho Null Image");
                            LogicPLC.L_I.WriteRegData1((int)DataRegister1.PCAlarm, 1);
                            WinMsgBox.ShowMsgBox("相机" + g_NoCamera + "采集图像ho为空 检查相机连接！\r可能需要重新启动软件！");
                            continue;
                        }

                        try
                        {
                            string type = im.Ho_Image.GetObjClass();
                            if (type == "image")
                            {

                            }
                            else
                            {
                                ShowState("Cam" + g_NoCamera + "type Null Image");
                                LogicPLC.L_I.WriteRegData1((int)DataRegister1.PCAlarm, 1);
                                WinMsgBox.ShowMsgBox("相机" + g_NoCamera + "采集图像type为空 检查相机连接！\r可能需要重新启动软件！");
                                continue;
                            }

                        }
                        catch (Exception ex)
                        {
                            ShowState("Cam" + g_NoCamera + "ex Null Image");
                            LogicPLC.L_I.WriteRegData1((int)DataRegister1.PCAlarm, 1);
                            WinMsgBox.ShowMsgBox("相机" + g_NoCamera + "采集图像ex为空 检查相机连接！\r可能需要重新启动软件！");
                            continue;
                        }

                        try
                        {
                            ResultInspection result = new ResultInspection();

                            //算法
                            var parInsp = g_ParIns.GetParBySideIndex(SideIndex);                            

                            if (g_NoCamera == 1)
                            {
                                if (SideIndex == 1)
                                {
                                    new FunInsp().DealImage(im, parInsp, ref result, ref NoPicture_Cam1Side1);
                                    g_NumImageSide = NoPicture_Cam1Side1;
                                }                                    
                                else
                                {
                                    new FunInsp().DealImage(im, parInsp, ref result, ref NoPicture_Cam1Side2);
                                    g_NumImageSide = NoPicture_Cam1Side2;
                                }                                    
                            }
                            else if (g_NoCamera == 2)
                            {
                                if (SideIndex == 1)
                                {
                                    new FunInsp().DealImage(im, parInsp, ref result, ref NoPicture_Cam2Side1);
                                    g_NumImageSide = NoPicture_Cam2Side1;
                                }                                    
                                else
                                {
                                    new FunInsp().DealImage(im, parInsp, ref result, ref NoPicture_Cam2Side2);
                                    g_NumImageSide = NoPicture_Cam2Side2;
                                }                                    
                            }
                            else if (g_NoCamera == 3)
                            {
                                if (SideIndex == 1)
                                {
                                    new FunInsp().DealImage(im, parInsp, ref result, ref NoPicture_Cam3Side1);
                                    g_NumImageSide = NoPicture_Cam3Side1;
                                }                                    
                                else
                                {
                                    new FunInsp().DealImage(im, parInsp, ref result, ref NoPicture_Cam3Side2);
                                    g_NumImageSide = NoPicture_Cam3Side2;
                                }                                    
                            }
                            else if (g_NoCamera == 4)
                            {
                                if (SideIndex == 1)
                                {
                                    new FunInsp().DealImage(im, parInsp, ref result, ref NoPicture_Cam4Side1);
                                    g_NumImageSide = NoPicture_Cam4Side1;
                                }                                    
                                else
                                {
                                    new FunInsp().DealImage(im, parInsp, ref result, ref NoPicture_Cam4Side2);
                                    g_NumImageSide = NoPicture_Cam4Side2;
                                }                                    
                            }

                            if (!result.IsValid)
                            {
                                g_NumInvalid++;
                                continue;
                            }

                            #region 角序号判定
                            //前一张是角，这一张不是角
                            //说明是一边的起始，把最后一张角的序号记录下来
                            if (isCornerPre == true && result.IsCorner == false)
                            {
                                cornerEnd = g_NumImageSide - 1;
                            }
                            //前一张不是角，这一张是角
                            //说明是一边的结尾，把是角的第一张记下来
                            //开软件的第一片开头拍到角会进这里，但只要拍到结尾的角，数据就会被更新，不会影响
                            if (isCornerPre == false && result.IsCorner == true)
                            {
                                cornerStart = g_NumImageSide;
                            }
                            //状态记录
                            isCornerPre = result.IsCorner;
                            #endregion

                            //保存所有图片
                            //针对戴金林处进行超凡入圣的改动
                            string fileName;
                            if (g_NoCamera == 1)
                            {
                                if (SideIndex == 1)
                                {
                                    fileName = SideIndex.ToString() + NoPicture_Cam1Side1.ToString("d3") +
                                        '_' + GetTimeName(dataImage.DtNow) + "_Cam" + g_NoCamera.ToString() + "_Side" + SideIndex.ToString();                                    
                                }                                    
                                else
                                {
                                    fileName = SideIndex.ToString() + NoPicture_Cam1Side2.ToString("d3") + 
                                        '_' + GetTimeName(dataImage.DtNow) + "_Cam" + g_NoCamera.ToString() + "_Side" + SideIndex.ToString();                                    
                                }                                    
                            }
                            else if (g_NoCamera == 2)
                            {
                                if (SideIndex == 1)
                                {
                                    fileName = SideIndex.ToString() + NoPicture_Cam2Side1.ToString("d3") + 
                                        '_' + GetTimeName(dataImage.DtNow) + "_Cam" + g_NoCamera.ToString() + "_Side" + SideIndex.ToString();                                    
                                }                                    
                                else
                                {
                                    fileName = SideIndex.ToString() + NoPicture_Cam2Side2.ToString("d3") +
                                        '_' + GetTimeName(dataImage.DtNow) + "_Cam" + g_NoCamera.ToString() + "_Side" + SideIndex.ToString();                                    
                                }                                    
                            }
                            else if (g_NoCamera == 3)
                            {
                                if (SideIndex == 1)
                                {
                                    fileName = SideIndex.ToString() + NoPicture_Cam3Side1.ToString("d3") + 
                                        '_' + GetTimeName(dataImage.DtNow) + "_Cam" + g_NoCamera.ToString() + "_Side" + SideIndex.ToString();                                    
                                }                                    
                                else
                                {
                                    fileName = SideIndex.ToString() + NoPicture_Cam3Side2.ToString("d3") + 
                                        '_' + GetTimeName(dataImage.DtNow) + "_Cam" + g_NoCamera.ToString() + "_Side" + SideIndex.ToString();                                    
                                }                                    
                            }
                            //相机4
                            else
                            {
                                if (SideIndex == 1)
                                {
                                    fileName = SideIndex.ToString() + NoPicture_Cam4Side1.ToString("d3") +
                                        '_' + GetTimeName(dataImage.DtNow) + "_Cam" + g_NoCamera.ToString() + "_Side" + SideIndex.ToString();                                    
                                }                                    
                                else
                                {
                                    fileName = SideIndex.ToString() + NoPicture_Cam4Side2.ToString("d3") + 
                                        '_' + GetTimeName(dataImage.DtNow) + "_Cam" + g_NoCamera.ToString() + "_Side" + SideIndex.ToString();                                    
                                }                                   
                            }

                            //g_NumImageSide++;
                            //所有图片保存
                            if (ParWorkInsp.P_I.SaveAllImage)
                            {
                                g_UCDisplayCamera.SaveHoImage(im.Ho_Image, "jpeg 20", BasePathImageSave + fileName + ".jpg");
                            }

                            if (result.SingleFalutInfo_L.Count > 0)
                            {
                                result.IndexImage = g_NumImageSide - 1;
                                //尝试对NG图片进行Mark的匹配，确定角的区域进行排除，但目前应该都没有使用20191212
                                if (parInsp.JudgeMentMark && !result.FaultTypeStr.Contains("破角"))//需判断Mark 并且缺陷类型不是破角
                                {
                                    ParScaledShapeT par = ParComprehensive1.P_I.GetCellParImageProcessByType("形状匹配T", 1) as ParScaledShapeT;
                                    if (par != null)
                                    {
                                        Hashtable htResult = null;
                                        //g_DealComprehensiveBase.DealComprehensivePos(g_UCDisplayCamera, g_UCDisplayCamera, g_HtUCDisplay, 1, out htResult);
                                        //var baseResult = htResult[par.NameCell] as BaseResult;
                                        var baseResult = DealScaledShape(im.Ho_Image, par);
                                        if (baseResult.Num > 0)
                                        {
                                            continue;
                                        }
                                    }
                                }
                                NumNG++;
                                result.SideInfo = SideIndex;
                                result.NoCamera = g_NoCamera;
                                result.Code = CodeNow;
                                DispImageResult(im, result);
                                //把缺陷区域打印到图上

                                new FunInsp().PaintRegionToImage(im, out ImageAll NgRegionPaintedImage, result, g_ParIns.GetParBySideIndex(SideIndex).Amp);


                                BasePathNGImageSave = BasePathImageSave.Replace("AllImage", "NGImage");                                
                                BasePathNGImageDefectPaintedSave = BasePathImageSave.Replace("AllImage", "NgPImage");
                                if (!Directory.Exists(BasePathNGImageSave))
                                {
                                    Directory.CreateDirectory(BasePathNGImageSave);
                                }
                                if (!Directory.Exists(BasePathNGImageDefectPaintedSave))
                                {
                                    Directory.CreateDirectory(BasePathNGImageDefectPaintedSave);
                                }
                                //保存NG图片
                                result.ImagePath = BasePathNGImageSave + CodeNow + "_" + fileName + "_No" + result.IndexImage + result.FaultTypeStr + "_" + result.FaultInfoStr + ".jpg";
                                result.ImageDefectPaintedPath = BasePathNGImageDefectPaintedSave + CodeNow + "_" + fileName + "_No" + result.IndexImage + result.FaultTypeStr + "_" + result.FaultInfoStr + ".jpg";
                                result.Time = dataImage.DtNow.ToShortTimeString();
                                g_UCDisplayCamera.BlLocalImage = false;
                                g_UCDisplayCamera.SaveHoImage(im.Ho_Image, "jpeg 100", result.ImagePath);
                                //存储缺陷区域打印的图片，方便查看和二次元对比（缩放到像素级别）
                                g_UCDisplayCamera.SaveHoImage(NgRegionPaintedImage.Ho_Image, "jpeg 100", result.ImageDefectPaintedPath);


                                //列表
                                if (g_ParIns.GetParBySideIndex(SideIndex).BlIgnoreThisSideFault)
                                {
                                    continue;
                                }

                                RefreshSingleDG(result);
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            Log.L_I.WriteError("BaseDealComprehensiveResult", ex);
                        }
                        finally
                        {
                            if (im != null)
                                im.Dispose();
                            //swTask.Stop();
                            //str += swTask.ElapsedMilliseconds + "-";
                        }
                        //ShowState("相机" + g_NoCamera + "第" + SideIndex + "边,第" + imageProcessCnt + "张" + swTask.Elapsed.ToString());
                        //ShowState("相机" + g_NoCamera + "第" + SideIndex + "边,第" + imageProcessCnt + "张" + swTask.Elapsed.ToString());
                        //swTask.Restart();
                        //}).Start();
                        //ShowState("相机" + g_NoCamera + "处理图像数：" + imageProcessCnt);
                    }
                    //对前N张图片和后N张图片重新进行破角清算，未达到破角阈值的缺陷，直接踢出缺陷结果
                    
                    ReCheckCorner();
                    if (NumRemoveWhenReCheck != 0)
                    {
                        ShowState(string.Format("相机{0}边{1}有{2}个检测结果复检时移除", g_NoCamera, SideIndex, NumRemoveWhenReCheck));
                    }
                    //这个赋值放在recheck后面，这样下一次recheck得时候用的就是上一次处理完得图片编号
                    //但这个值一样要在初始化得时候清零，不然下一片得时候这个值不是0影响第一边开头得recheck
                    ContinueCnt = g_NumImageSide;

                    //ShowState(ParSetDisplay.P_I.g_BaseParSetDisplay_L[g_NoCamera - 1].Title + "图像处理结束" + str);
                    ShowState(ParSetDisplay.P_I.g_BaseParSetDisplay_L[g_NoCamera - 1].Title + "图像处理结束" + swTask.Elapsed.ToString());
                    ShowState("相机" + g_NoCamera + "处理" + "第" + SideIndex + "边" + "图像数：" + imageProcessCnt);
                    imageProcessCnt = 0;
                    //每一边处理完成，重置成false
                    //这个变量完全可以不用重置，应该不会导致逻辑问题
                    isCornerPre = false;
                    //重置，以防拍照速度和位置导致角漏拍造成误判
                    cornerStart = cornerEnd = 0;
                    CheckFinish();
                }
                catch (Exception ex)
                {                    
                    Log.L_I.WriteError("DealInsp", ex);
                }
            }
        }

        protected static void RecordInspChipID(string chipid)
        {
            TxtFile t_I = new TxtFile();
            try
            {
                string root = ParPathRoot.PathRoot + "软件运行记录\\RecordData\\Insp\\";
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                string pathDir = Log.CreateAllTimeFile(root);

                string path = pathDir + "ChipIDLog" + ".txt";
                string name = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() +
                    ":" + DateTime.Now.Second.ToString() + ":" + DateTime.Now.Millisecond.ToString();
                t_I.WriteText(path, name + "\n----->ChipID: " + chipid);//写入时间
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseDealComprehensive", ex);
            }
        }

        public void DealQueue_Sample(int sideIndex, ref List<ResultInspection> list, bool ifSave = true)
        {
            ShowState("相机" + g_NoCamera + "处理准备就绪");

            try
            {
                ShowState(ParSetDisplay.P_I.g_BaseParSetDisplay_L[g_NoCamera - 1].Title + "开始处理图像");
                string str = "";
                int i = 0;
                int cnt = 0;
                while (ImageNeedDeal_Q.Count > 0 || !BlCyclePhotoStop || g_BlManualCycleTest)
                {
                    if (ImageNeedDeal_Q.Count == 0)
                    {
                        if (g_BlManualCycleTest)
                        {
                            g_BlManualCycleTest = false;
                            break;
                        }
                        Thread.Sleep(12);
                        continue;
                    }

                    swTask.Restart();

                    DataImage dataImage = ImageNeedDeal_Q.Dequeue();
                    ImageAll im = dataImage.Image;
                    if (im == null)
                    {
                        ShowState("Cam" + g_NoCamera + "Null Image");
                        LogicPLC.L_I.WriteRegData1(1, (int)PCArarm_Enum.CameraDown);
                        WinMsgBox.ShowMsgBox("相机" + g_NoCamera + "采集图像为空 检查相机连接！\r可能需要重新启动软件！");
                        return;
                    }

                    try
                    {
                        ResultInspection result = new ResultInspection();

                        //保存所有图片
                        string fileName = i.ToString();// GetTimeName(dataImage.DtNow) + "_Cam" + g_NoCamera.ToString() + "_Side" + SideIndex.ToString();

                        //算法
                        var parInsp = g_ParIns.GetParBySideIndex(sideIndex);
                        new FunInsp().DealImage(im, parInsp, ref result, ref i);

                        if (!result.IsValid)
                        {
                            g_NumInvalid++;
                            continue;
                        }

                        g_NumImageSide++;

                        //  if (!Directory.Exists(SampleManager.SampleImagePath))
                        Directory.CreateDirectory(SampleManager.SampleImagePath + sideIndex + @"\");
                        result.ImagePath = SampleManager.SampleImagePath + sideIndex + @"\" + fileName + ".jpg";
                        list[cnt++] = result;
                        if (ifSave)
                            g_UCDisplayCamera.SaveHoImage(im.Ho_Image, "jpeg 100", result.ImagePath);
                    }
                    catch (Exception ex)
                    {
                        Log.L_I.WriteError("BaseDealComprehensiveResult", ex);
                    }
                    finally
                    {
                        if (im != null)
                            im.Dispose();
                        swTask.Stop();
                        str += swTask.ElapsedMilliseconds + "-";
                    }
                    //}).Start();
                }

                ReCheckCorner();
                if (NumRemoveWhenReCheck != 0)
                {
                    ShowState(string.Format("相机{0}边{1}有{2}个检测结果复检时移除", g_NoCamera, SideIndex, NumRemoveWhenReCheck));
                }

                //ShowState(ParSetDisplay.P_I.g_BaseParSetDisplay_L[g_NoCamera - 1].Title + "图像处理结束" + str);
                ShowState(ParSetDisplay.P_I.g_BaseParSetDisplay_L[g_NoCamera - 1].Title + "图像处理结束");
                CheckFinish();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 重新检查结果，返回剔除掉的结果数量
        /// </summary>
        /// <returns></returns>
        void ReCheckCorner()
        {
            mtxEditSingleResult.WaitOne();
            try
            {
                ParInspection par = g_ParIns.GetParBySideIndex(SideIndex);
                //根据设定，定义“角”的序号
                //xc-1219,应该是，如果图片数量已经小于设置的图片起始+结束张数，就不做处理
                //if (g_NumImageSide < par.IntNumDefCornerWhenStart + par.IntNumDefCornerWhenEnd)
                //{
                //    return;
                //}
                ShowState("边起始：" + cornerEnd + ",边结束：" + cornerStart);

                List<int> cornerIndex_L = new List<int>();
                //xc-应该是把首尾图片序号统计出来
                for (int i = 0; i < par.IntNumDefCornerWhenStart + par.IntNumDefCornerWhenEnd; i++)//5
                {
                    //改成用角起始和角结束判断
                    //if (i < par.IntNumDefCornerWhenStart + ContinueCnt)
                    if (i < par.IntNumDefCornerWhenStart + cornerEnd)
                    {
                        cornerIndex_L.Add(i);
                    }
                    //else
                    else if (i > cornerStart - par.IntNumDefCornerWhenEnd)
                    {
                        cornerIndex_L.Add(g_NumImageSide - 1 - (i - par.IntNumDefCornerWhenStart));
                    }
                }

                List<ResultInspection> removeList = new List<ResultInspection>();
                for (int i = 0; i < ResultInspSingeCell_L.Count; i++)
                {
                    //要嘛破角 要嘛Remove
                    bool blNeedRemove = true;

                    var result = ResultInspSingeCell_L[i];
                    //当前边 包含序号并且被判定为边而不是角
                    if (result.SideInfo == SideIndex && cornerIndex_L.Contains(result.IndexImage) &&
                        !result.IsCorner && result.NoCamera == g_NoCamera)
                    {
                        //一张图片里的缺陷
                        foreach (var fault in result.SingleFalutInfo_L)
                        {
                            //xc-0115，只有角位置的贝壳会更正到破角，其余的不做修正
                            if (fault.FaultType_E == FaultType_Enum.贝壳)
                            {
                                if ((fault.DepthFault > par.ThCornerY && fault.WidthFault > par.ThCornerX && !par.UsingORToCorner)
                                                                || ((fault.DepthFault > par.ThCornerY || fault.WidthFault > par.ThCornerX) && par.UsingORToCorner))
                                {
                                    //贝壳复判为破角
                                    fault.FaultType_E = FaultType_Enum.破角;
                                    blNeedRemove = false;
                                    new Computer().FileSystem.RenameFile(result.ImagePath, "已更正破角" + result.ImageSafeName);
                                    result.ImagePath = BasePathNGImageSave + "已更正破角" + result.ImageSafeName;
                                    break;
                                }

                                if (blNeedRemove)
                                {
                                    //不再通过自加过滤的图片来计数
                                    //Interlocked.Increment(ref NumRemoveWhenReCheck);

                                    //i--;
                                    ShowState("产生复判过滤结果");
                                    new Computer().FileSystem.RenameFile(result.ImagePath, "已过滤" + result.ImageSafeName);
                                    result.ImagePath = BasePathNGImageSave + "已过滤" + result.ImageSafeName;
                                    //

                                    //xc-1219,for循环不能在内部改变list的数量，这样会导致计数错误，漏处理,改为计数，最后统一remove
                                    //ResultInspSingeCell_L.Remove(result);
                                    removeList.Add(result);

                                    //  ResultInspSingeCell_L.Remove(result);
                                }
                            }
                        }
                    }
                }

                ShowState("相机" + g_NoCamera + "复判过滤数：" + removeList.Count);
                foreach (var item in removeList)
                {
                    ResultInspSingeCell_L.Remove(item);
                    NumNG--;
                }
                //xc-1229
                //DealComprehensiveResultTemp.D_I.g_UCSingleRecord.blRefresh = true; ;
                UCRecordTemp.blRefresh = true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                mtxEditSingleResult.ReleaseMutex();
            }
        }

        ResultScaledShape DealScaledShape(HObject ho_Image, ParScaledShapeT par)
        {
            #region 定义
            ResultScaledShape result = new ResultScaledShape();

            List<double> Gray_L = new List<double>();//中心点灰度

            HTuple width = 0;
            HTuple height = 0;
            HTuple row1 = 0;
            HTuple col1 = 0;
            HTuple length1 = 0;
            HTuple length2 = 0;
            HTuple phi = 0;

            HTuple hv_ModelRow = null;
            HTuple hv_ModelColumn = null;
            HTuple hv_ModelAngle = null;
            HTuple hv_ModelScale = null;
            HTuple hv_ModelScore = null;

            HObject ho_ReduceImage = null;
            HObject ho_ResultPreprocess = null;

            HOperatorSet.GenEmptyObj(out ho_ReduceImage);
            HOperatorSet.GenEmptyObj(out ho_ResultPreprocess);

            List<HObject> xld_L = new List<HObject>();
            //List<HObject> regionRect2_L = new List<HObject>();
            List<HObject> regionFill_L = new List<HObject>();
            #endregion 定义
            try
            {
                HOperatorSet.SetSystem("border_shape_models", "true");

                #region 读取模板ID
                ResultTemplate resultTemplate = g_DealComprehensiveBase.g_HtResult[par.NameCell + "T"] as ResultTemplate;
                if (resultTemplate == null)
                {
                    result.LevelError_e = LevelError_enum.Error;
                    result.TypeErrorProcess_e = result.g_ResultPreProcess.TypeErrorProcess_e;
                    result.Annotation = par.NameCell.ToString() + "形状匹配模板类为Null";

                    result.SetDefault();//赋值默认值
                    return result;
                }

                if (resultTemplate.ModelID == null)
                {
                    result.LevelError_e = LevelError_enum.Error;
                    result.TypeErrorProcess_e = result.g_ResultPreProcess.TypeErrorProcess_e;
                    result.Annotation = par.NameCell.ToString() + "形状匹配模板ID为Null";

                    result.SetDefault();//赋值默认值
                    return result;
                }
                #endregion 读取模板ID

                #region 搜索目标
                if (par.Deformation == "none") //是否使用最大可变形
                {
                    HOperatorSet.FindScaledShapeModel(ho_Image, resultTemplate.ModelID,
                    ((HTuple)par.g_ParTempScaledShape.AngleStart).TupleRad(), ((HTuple)par.g_ParTempScaledShape.AngleExtent).TupleRad(),
                    (par.g_ParTempScaledShape.ScaleMin), (par.g_ParTempScaledShape.ScaleMax),
                     par.MinScore, par.NumMatches, par.MaxOverlap,
                    par.SubPixel, par.g_ParTempScaledShape.NumLevels, par.Greediness,
                    out hv_ModelRow, out hv_ModelColumn, out hv_ModelAngle, out hv_ModelScale, out hv_ModelScore);
                }
                else
                {

                    HOperatorSet.FindScaledShapeModel(ho_Image, resultTemplate.ModelID,
                    ((HTuple)par.g_ParTempScaledShape.AngleStart).TupleRad(), ((HTuple)par.g_ParTempScaledShape.AngleExtent).TupleRad(),
                    (par.g_ParTempScaledShape.ScaleMin), (par.g_ParTempScaledShape.ScaleMax),
                    par.MinScore, par.NumMatches, par.MaxOverlap,
                    (new HTuple(par.SubPixel)).TupleConcat(par.Deformation), par.NumLevels, par.Greediness,
                    out hv_ModelRow, out hv_ModelColumn, out hv_ModelAngle, out hv_ModelScale, out hv_ModelScore);

                }

                result.Num = hv_ModelColumn.Length;//目标个数           

                return result;
                #endregion 搜索目标


            }
            catch (Exception ex)
            {
                result.LevelError_e = LevelError_enum.Error;
                result.TypeErrorProcess_e = TypeErrorProcess_enum.Catch;
                result.Annotation = par.NameCell.ToString() + "形状匹配异常:" + ex.Message;

                if (ex.Message.Contains("memory"))
                {
                    result.TypeErrorProcess_e = TypeErrorProcess_enum.OutMemory;
                    result.Annotation = par.NameCell.ToString() + "形状匹配内存异常";
                }

                result.SetDefault();//设置结果的默认值
                return result;
            }
            finally
            {

            }
        }

        protected void FinishAllPhotoPLC()
        {
            string reg = DealComprehensiveResult1.D_I.g_regClearCamera;
            if (ParCameraWork.NumCamera > 1)
            {
                reg += DealComprehensiveResult2.D_I.g_regClearCamera;
            }
            if (ParCameraWork.NumCamera > 2)
            {
                reg += DealComprehensiveResult3.D_I.g_regClearCamera;
            }
            if (ParCameraWork.NumCamera > 3)
            {
                reg += DealComprehensiveResult4.D_I.g_regClearCamera;
            }
            int[] value = new int[reg.Split('\n').Length - 1];

            LogicPLC.L_I.WriteBlockReg(reg, value.Length, value, "");
        }
        #endregion

        public static void Init()
        {
            ShowState("新产品，数据初始化");

            NoPicture_Cam1Side1 = 0;
            NoPicture_Cam1Side2 = 0;
            NoPicture_Cam2Side1 = 0;
            NoPicture_Cam2Side2 = 0;
            NoPicture_Cam3Side1 = 0;
            NoPicture_Cam3Side2 = 0;
            NoPicture_Cam4Side1 = 0;
            NoPicture_Cam4Side2 = 0;

            IsAllCompleted = false;
            //初始化二维码
            if (!ModelParams.IfGetCodeFromPLC)
            {
                ShowState("开启读码，按读码结果存图");
                CodeNow = RegeditMain.R_I.CodePlat;     
                ShowState("读到的二维码为：" + CodeNow);
                if (CodeNow.Replace("0", "") == "" ||
                    ModelParams.DefaultQrCodeOK ||
                    (ModelParams.IfPassCodeNG && CodeNow == "FAILED"))
                {
                    CodeNow = "Default" + new Random().Next(100000, 999999).ToString();
                }
            }
            else
            {
                ShowState("未开启读码，从PLC获取二维码");
                CodeNow = ReadCodeFromPLC();
                ShowState("读到的二维码为：" + CodeNow);
                if (CodeNow.Replace("0", "") == "")
                {
                    CodeNow = "Default" + new Random().Next(100000, 999999).ToString();
                }
            }

            ShowState("确认后的二维码为：" + CodeNow);
            RecordInspChipID(CodeNow);
           // BlCodeReady = true;
            g_UCResultInsp.ShowCode(CodeNow);

            //初始化集合
            ClearSingleDG();

            DealComprehensiveResult1.D_I.g_NumInvalid = 0;
            DealComprehensiveResult1.D_I.g_NumAllImage = 0;
            DealComprehensiveResult2.D_I.g_NumInvalid = 0;
            DealComprehensiveResult2.D_I.g_NumAllImage = 0;
            DealComprehensiveResult3.D_I.g_NumInvalid = 0;
            DealComprehensiveResult3.D_I.g_NumAllImage = 0;
            DealComprehensiveResult4.D_I.g_NumInvalid = 0;
            DealComprehensiveResult4.D_I.g_NumAllImage = 0;
        }
        #region 显示
        /// <summary>
        /// 显示结果
        /// </summary>
        /// <param name="image"></param>
        /// <param name="result"></param>
        protected void DispImageResult(ImageAll image, ResultInspection result)
        {
            try
            {
                //Stopwatch sw = new Stopwatch();
                //sw.Start();

                g_UCDisplayCamera.ClearShapeHalWin();
                //显示图像
                g_UCDisplayCamera.DispImage(image);
                List<double[]> list = CreatRect1(result);
                foreach (double[] value in list)
                {
                    g_UCDisplayCamera.DispRectangle1(value, 1, "red", "margin");
                }
                //sw.Stop();
                //string time = sw.ElapsedMilliseconds.ToString();
                //绘制结果用时
                //g_UCDisplayCamera.ShowInfoInvoke(time + "ms", "blue");
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseDealComprehensiveResult", ex);
            }
        }
        /// <summary>
        /// 生产rect1
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public List<double[]> CreatRect1(ResultInspection result)
        {
            List<double[]> list = new List<double[]>();
            try
            {
                for (int i = 0; i < result.SingleFalutInfo_L.Count; i++)
                {
                    Point2D pos = result.SingleFalutInfo_L[i].PosFalut;
                    double width = result.SingleFalutInfo_L[i].WidthFault / g_ParIns.GetParBySideIndex(SideIndex).Amp;
                    double height = result.SingleFalutInfo_L[i].DepthFault / g_ParIns.GetParBySideIndex(SideIndex).Amp;
                    //row col x y
                    double[] value = new double[] { pos.DblValue1 - width / 2, pos.DblValue2 - height / 2, pos.DblValue1 + width / 2, pos.DblValue2 + height / 2 };
                    list.Add(value);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseDealComprehensiveResult", ex);
            }
            return list;
        }
        #endregion

        #region 处理界面 针对多线程 加保护
        protected static void ClearSingleDG()
        {
            try
            {
                mtxEditSingleResult.WaitOne();
                ResultInspSingeCell_L.Clear();
                NumNG = 0;
                //xc-1229
                //DealComprehensiveResultTemp.D_I.g_UCSingleRecord.blRefresh = true;
                UCRecordTemp.blRefresh = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                mtxEditSingleResult.ReleaseMutex();
            }
        }

        void RefreshSingleDG(ResultInspection result)
        {
            try
            {
                mtxEditSingleResult.WaitOne();
                ResultInspSingeCell_L.Insert(0, result);

                //DealComprehensiveResultTemp.D_I.g_UCSingleRecord.blRefresh = true;
                UCRecordTemp.blRefresh = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                mtxEditSingleResult.ReleaseMutex();
            }
        }

        void RefreshDG()
        {
            try
            {
                mtxEditResult.WaitOne();
                if (ResultInspCell_L.Count > 50)
                {
                    ResultInspCell_L.RemoveRange(0, ResultInspSingeCell_L.Count);
                }

                ResultInspCell_L.InsertRange(0, ResultInspSingeCell_L);

                DealComprehensiveResultTemp.D_I.g_UCRecord.DGRefresh_Invoke();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                mtxEditResult.ReleaseMutex();
            }
        }

        #endregion

        #region 处理结果
        void CheckFinish()
        {
            if (this == DealComprehensiveResult1.D_I)
            {
                BlFinishPos_Cam1 = true;
            }
            if (this == DealComprehensiveResult2.D_I)
            {
                BlFinishPos_Cam2 = true;
            }
            if (this == DealComprehensiveResult3.D_I)
            {
                BlFinishPos_Cam3 = true;
            }
            if (this == DealComprehensiveResult4.D_I)
            {
                BlFinishPos_Cam4 = true;
            }
            if (BlFinishPos_Cam1 && BlFinishPos_Cam2 && BlFinishPos_Cam3 && BlFinishPos_Cam4)//全部处理结束，则加入显示
            {
                //显示结果X
                //由于不确定是否全部完成，暂时不在这里显示结果
                g_UCStateWork.AddInfo("相机" + g_NoCamera + "边" + SideIndex + "检测全部完成！");
                //InspectionCompleted.Set();
                IsAllCompleted = true;
            }
        }


        public static void CommunicateInspResult()
        {
            BlCodeReady = false;
            ShowState("PLC请求发送检测结果");

            //while (!(BlFinishPos_Cam1 && BlFinishPos_Cam2 && BlFinishPos_Cam3 && BlFinishPos_Cam4))
            //{
            //    Thread.Sleep(10);
            //}
            int i = 0;
            while (i++ < 10 && !IsAllCompleted)
                Thread.Sleep(100);

            IsAllCompleted = false;
            //InspectionCompleted.WaitOne();

            if (BlFinishPos_Cam1 == false)
            {
                ShowState(string.Format("--------相机1未完成[{0}/{1}]----------", DealComprehensiveResult1.D_I.g_NumInvalid + DealComprehensiveResult1.D_I.g_NumImageSide, DealComprehensiveResult1.D_I.g_NumAllImage));
                //ShowAlarm(string.Format("--------相机1未完成[{0}/{1}]----------", DealComprehensiveResult1.D_I.g_NumInvalid + DealComprehensiveResult1.D_I.g_NumImageSide, DealComprehensiveResult1.D_I.g_NumAllImage));
            }
            if (BlFinishPos_Cam2 == false)
            {
                ShowState(string.Format("--------相机2未完成[{0}/{1}]----------", DealComprehensiveResult2.D_I.g_NumInvalid + DealComprehensiveResult2.D_I.g_NumImageSide, DealComprehensiveResult2.D_I.g_NumAllImage));
                //ShowAlarm(string.Format("--------相机2未完成[{0}/{1}]----------", DealComprehensiveResult2.D_I.g_NumInvalid + DealComprehensiveResult2.D_I.g_NumImageSide, DealComprehensiveResult2.D_I.g_NumAllImage));
            }
            if (BlFinishPos_Cam3 == false)
            {
                ShowState(string.Format("--------相机3未完成[{0}/{1}]----------", DealComprehensiveResult3.D_I.g_NumInvalid + DealComprehensiveResult3.D_I.g_NumImageSide, DealComprehensiveResult3.D_I.g_NumAllImage));
                //ShowAlarm(string.Format("--------相机3未完成[{0}/{1}]----------", DealComprehensiveResult3.D_I.g_NumInvalid + DealComprehensiveResult3.D_I.g_NumImageSide, DealComprehensiveResult3.D_I.g_NumAllImage));
            }
            if (BlFinishPos_Cam4 == false)
            {
                ShowState(string.Format("--------相机4未完成[{0}/{1}]----------", DealComprehensiveResult4.D_I.g_NumInvalid + DealComprehensiveResult4.D_I.g_NumImageSide, DealComprehensiveResult4.D_I.g_NumAllImage));
                //ShowAlarm(string.Format("--------相机4未完成[{0}/{1}]----------", DealComprehensiveResult4.D_I.g_NumInvalid + DealComprehensiveResult4.D_I.g_NumImageSide, DealComprehensiveResult4.D_I.g_NumAllImage));
            }

            try
            {
                double ratio = 0.9;
                bool blInvalidInsp = false;
                int numCamera = 0;
                if (DealComprehensiveResult1.D_I.RatioInvalid > ratio)
                {
                    numCamera = numCamera * 10 + 1;
                    ShowAlarm("相机1无效图片阈值大于设定值，当前比例" + DealComprehensiveResult1.D_I.RatioInvalid);

                    ShowState("相机1无效图：" + DealComprehensiveResult1.D_I.g_NumInvalid + ",总数：" + DealComprehensiveResult1.D_I.g_NumAllImage);
                    blInvalidInsp = true;
                }
                DealComprehensiveResult1.D_I.g_NumInvalid = 0;
                DealComprehensiveResult1.D_I.g_NumAllImage = 0;
                if (ParCameraWork.NumCamera > 1 && DealComprehensiveResult2.D_I.RatioInvalid > ratio)
                {
                    numCamera = numCamera * 10 + 2;
                    ShowAlarm("相机2无效图片阈值大于设定值，当前比例" + DealComprehensiveResult2.D_I.RatioInvalid);

                    ShowState("相机2无效图：" + DealComprehensiveResult2.D_I.g_NumInvalid + ",总数：" + DealComprehensiveResult2.D_I.g_NumAllImage);

                    blInvalidInsp = true;
                }
                DealComprehensiveResult2.D_I.g_NumInvalid = 0;
                DealComprehensiveResult2.D_I.g_NumAllImage = 0;
                if (ParCameraWork.NumCamera > 2 && DealComprehensiveResult3.D_I.RatioInvalid > ratio)
                {
                    numCamera = numCamera * 10 + 3;
                    ShowAlarm("相机3无效图片阈值大于设定值，当前比例" + DealComprehensiveResult3.D_I.RatioInvalid);

                    ShowState("相机3无效图：" + DealComprehensiveResult3.D_I.g_NumInvalid + ",总数：" + DealComprehensiveResult3.D_I.g_NumAllImage);

                    blInvalidInsp = true;
                }
                DealComprehensiveResult3.D_I.g_NumInvalid = 0;
                DealComprehensiveResult3.D_I.g_NumAllImage = 0;
                if (ParCameraWork.NumCamera > 3 && DealComprehensiveResult4.D_I.RatioInvalid > ratio)
                {
                    numCamera = numCamera * 10 + 4;
                    ShowAlarm("相机4无效图片阈值大于设定值，当前比例" + DealComprehensiveResult4.D_I.RatioInvalid);

                    ShowState("相机4无效图：" + DealComprehensiveResult4.D_I.g_NumInvalid + ",总数：" + DealComprehensiveResult4.D_I.g_NumAllImage);

                    blInvalidInsp = true;
                }
                DealComprehensiveResult4.D_I.g_NumInvalid = 0;
                DealComprehensiveResult4.D_I.g_NumAllImage = 0;
                if (blInvalidInsp)
                {
                    List<FaultInfo> list = new List<FaultInfo>
                    {
                        new FaultInfo()
                        {
                            DepthFault = 99,
                            WidthFault = 99,
                            FaultType_E = FaultType_Enum.位置异常,
                        }
                    };
                    ResultInspSingeCell_L.Add(new ResultInspection()
                    {
                        Code = "黑图",
                        SingleFalutInfo_L = list,
                        NoCamera = numCamera,
                        Time = DateTime.Now.ToShortTimeString(),
                    });
                }

                if (ResultInspSingeCell_L.Count - NumRemoveWhenReCheck > 0)
                {
                    if (ResultInspSingeCell_L[0].FaultTypeStr.Contains("贝壳"))
                    {
                        ParAnalysis.P_I.g_ProductNumInfoNow.NumNGShell++;
                    }
                    else if (ResultInspSingeCell_L[0].FaultTypeStr.Contains("破角"))
                    {
                        ParAnalysis.P_I.g_ProductNumInfoNow.NumNGCorner++;
                    }
                    else if (ResultInspSingeCell_L[0].FaultTypeStr.Contains("凹边"))
                    {
                        ParAnalysis.P_I.g_ProductNumInfoNow.NumNGOther++;

                    }
                }


                //显示结果
                // g_UCResultInsp.ShowResult(ResultInspSingeCell_L.Count - NumRemoveWhenReCheck);

                //g_UCResultInsp.ShowResult(ResultInspSingeCell_L.Count);
                ShowState("当前总产能：" + ParAnalysis.P_I.g_ProductNumInfoNow.NumAll);

                ShowState("NG列表：" + ResultInspSingeCell_L.Count + ",NG数：" + NumNG);
                // int result = ResultInspSingeCell_L.Count - NumRemoveWhenReCheck > 0 ? 2 : 1;

                int result = ResultInspSingeCell_L.Count == 0 ? 1 : 2;


                ShowState("发送检测结果：" + result);

                DealComprehensiveResult1.D_I.RefreshDG();

                LogicPLC.L_I.WriteRegData1(5, result);
                //捡片机，巡边结果要发给机器人，33
                if (ModelParams.IfSendResultToRobot)
                    LogicRobotCam3.L_I.WriteData3(new Point4D(result, 0, 0, 0));

                if (blInvalidInsp)
                    LogicPLC.L_I.WriteRegData1((int)DataRegister1.PCAlarm, (int)PCArarm_Enum.CameraDown);

                //显示产能
                g_UCResultInsp.ShowResult(result);
                //   IsNew = true;
                //NoPicture_Cam1Side1 = 0;
                //NoPicture_Cam1Side2 = 0;
                //NoPicture_Cam2Side1 = 0;
                //NoPicture_Cam2Side2 = 0;

                // NumRemoveWhenReCheck = 0;
                
                DealComprehensiveResult1.D_I.imageProcessCnt = 0;
                DealComprehensiveResult1.D_I.imageGrabCnt = 0;
                DealComprehensiveResult1.D_I.ContinueCnt = 0;
                DealComprehensiveResult2.D_I.imageProcessCnt = 0;
                DealComprehensiveResult2.D_I.imageGrabCnt = 0;
                DealComprehensiveResult2.D_I.ContinueCnt = 0;
                DealComprehensiveResult3.D_I.imageProcessCnt = 0;
                DealComprehensiveResult3.D_I.imageGrabCnt = 0;
                DealComprehensiveResult3.D_I.ContinueCnt = 0;
                DealComprehensiveResult4.D_I.imageProcessCnt = 0;
                DealComprehensiveResult4.D_I.imageGrabCnt = 0;
                DealComprehensiveResult4.D_I.ContinueCnt = 0;
            }
            catch (Exception ex)
            {
                ShowAlarm("检测结果发送异常");
                LogicPLC.L_I.WriteRegData1(5, 2);
                Log.L_I.WriteError("BaseComprehensiveResult.CommunicateInspResult", ex);
            }

            InspectionCompleted.Reset();
        }

        #endregion

        /// <summary>
        /// 创建图片保存文件夹
        /// 格式为 日期 + 二维码 + 相机n
        /// NG的格式为日期 + NG + 二维码
        /// </summary>
        /// <returns></returns>
        string CreateImageDir()
        {
            try
            {
                string path = Log.CreateAllTimeFile(BaseDir);
                string pathOK = path + string.Format("{0}\\{1}\\", CodeNow, ParSetDisplay.P_I.g_BaseParSetDisplay_L[g_NoCamera - 1].Title);
                if (!Directory.Exists(pathOK))
                {
                    Directory.CreateDirectory(pathOK);
                }
                return pathOK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("文件夹路径" + BaseDir + "初始化失败！", "错误");
                throw;
            }
        }

        public void ManualTrigger(int SideIndex)
        {
            //调用算法处理
            ResultInspection result = new ResultInspection();
            try
            {
                //算法
                using (ImageAll im = g_UCDisplayCamera.GrabImageAll())
                {
                    Stopwatch sw = new Stopwatch();
                    DealComprehensiveResult1.D_I.RefreshDG();

                    sw.Restart();
                    int i = 0;
                    new FunInsp().DealImage_Manual(im, g_ParIns.GetParBySideIndex(SideIndex), ref result, ref i);
                    sw.Stop();
                    ShowState("TimeDeal:" + sw.ElapsedMilliseconds);
                    if (result.SingleFalutInfo_L.Count > 0)
                    {
                        ShowState("手动触发检测，结果NG:" + result.FaultTypeStr + result.FaultInfoStr);
                        sw.Restart();
                        DispImageResult(im, result);
                        sw.Stop();
                        ShowState("TimeShow:" + sw.ElapsedMilliseconds);
                    }
                    else
                    {
                        ShowState("手动触发检测，结果OK");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseComprehensiveResult.CommunicateInspResult", ex);
            }
        }


        public void WriteCodeToPLC(string code)
        {
            try
            {
                byte[] byte_Arr = Encoding.ASCII.GetBytes(code.PadRight(40, ' '));

                double[] data = new double[10];

                for (int i = 0; i < 10; i++)
                {
                    data[i] = (byte_Arr[i * 4] + byte_Arr[i * 4 + 1] * 256) + (byte_Arr[i * 4 + 2] + byte_Arr[i * 4 + 3] * 256) * 65536;
                }
                Stopwatch sw = new Stopwatch();
                sw.Start();
                LogicPLC.L_I.WriteRegData1(5, 10, data);
                sw.Stop();
                ShowState("写入40个寄存器耗时：" + sw.ElapsedMilliseconds);

                ShowState("二维码写入成功：" + code);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow.WriteCodeToPLC", ex);
            }
        }


        public static string ReadCodeFromPLC()
        {
            try
            {
                //double[] value = LogicPLC.L_I.ReadRegData1(ParWorkInsp.P_I.CodeStartIndex, ParWorkInsp.P_I.NumCodeReg);
                double[] value = LogicPLC.L_I.ReadRegData1(6, 10);
                string code = "";
                for (int i = 0; i < value.Length; i++)
                {
                    int data1 = ((int)value[i]) % 65536;
                    int data2 = ((int)value[i]) / 65536;
                    char c1 = (char)(data1 % 256);
                    char c2 = (char)(data1 / 256);
                    char c3 = (char)(data2 % 256);
                    char c4 = (char)(data2 / 256);
                    code += c1.ToString() + c2.ToString() + c3.ToString() + c4.ToString();
                }
                return Regex.Replace(code.Trim(), "[^a-z0-9]", "", RegexOptions.IgnoreCase);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow.WriteCodeToPLC", ex);
                return "";
            }
        }

        public void DeleteFilesOneByOnce(string rootPath, int offsetDays)
        {
            //get today
            DateTime datethreshold = DateTime.Today;
            //calculate threshold
            datethreshold.AddDays(-offsetDays);
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
                                Log.L_I.WriteError("DELIMAGE", fcode[0]);
                                return;
                            }
                            else
                                Directory.Delete(dirHour);
                        }
                    }
                    else
                        Directory.Delete(fdate[0]);
                }
            }
        }

    }

    public class DataImage
    {
        public ImageAll Image { get; set; }
        public DateTime DtNow { get; set; }


        public DataImage(ImageAll im)
        {
            Image = im;
            DtNow = DateTime.Now;
        }


    }
}



