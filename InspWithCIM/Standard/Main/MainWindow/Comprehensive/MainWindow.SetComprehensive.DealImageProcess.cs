
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealComprehensive;
using System.Threading;
using System.Threading.Tasks;
using BasicClass;
using DealConfigFile;
using SetPar;
using HalconDotNet;
using Common;
using DealImageProcess;

namespace Main
{
    partial class MainWindow
    {
        /// <summary>
        /// 图像处理初始化
        /// </summary>
        public void Init_ImageProcess()
        {
            CreateTemp(true);//生产模板
            InitCalib();//初始化校准
        }


        #region 模板
        /// <summary>
        /// 加载对应的模板
        /// </summary>       
        public void CreateTemp(bool blInitWin)
        {
            string cellError = "";
            int numScale = 0;
            int numNcc = 0;
            int numScaleT = 0;
            int numNccT = 0;

            bool blScale = false;
            bool blNcc = false;
            bool blScaleT = false;
            bool blNccT = false;
            try
            {
                #region 相机1
                switch (DealComprehensive1.D_I.CreateTemplate(out cellError, out numScale, out numNcc, out numScaleT, out numNccT))
                {
                    case StateTemplate_enum.Null:
                        break;

                    case StateTemplate_enum.False:
                        ShowWinError_Invoke("相机1模板加载失败,模板单元:" + cellError + "可重启软件！");
                        break;

                    case StateTemplate_enum.True:
                        ShowState("相机1模板加载成功");
                        break;
                }
                if (numScale > 0)
                {
                    blScale = true;
                }
                if (numNcc > 0)
                {
                    blNcc = true;
                }
                if (numScaleT > 0)
                {
                    blScaleT = true;
                }
                if (numNccT > 0)
                {
                    blNccT = true;
                }
                #endregion 相机1

                #region 相机2
                if (ParCameraWork.NumCamera > 1)
                {
                    switch (DealComprehensive2.D_I.CreateTemplate(out cellError, out numScale, out numNcc, out numScaleT, out numNccT))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机2模板加载失败,模板单元:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机2模板加载成功");
                            break;
                    }
                }
                if (numScale > 0)
                {
                    blScale = true;
                }
                if (numNcc > 0)
                {
                    blNcc = true;
                }
                if (numScaleT > 0)
                {
                    blScaleT = true;
                }
                if (numNccT > 0)
                {
                    blNccT = true;
                }
                #endregion 相机2

                #region 相机3
                if (ParCameraWork.NumCamera > 2)
                {
                    switch (DealComprehensive3.D_I.CreateTemplate(out cellError, out numScale, out numNcc, out numScaleT, out numNccT))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机3模板加载失败,模板单元:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机3模板加载成功");
                            break;
                    }
                }
                if (numScale > 0)
                {
                    blScale = true;
                }
                if (numNcc > 0)
                {
                    blNcc = true;
                }
                if (numScaleT > 0)
                {
                    blScaleT = true;
                }
                if (numNccT > 0)
                {
                    blNccT = true;
                }
                #endregion 相机3

                #region 相机4
                if (ParCameraWork.NumCamera > 3)
                {
                    switch (DealComprehensive4.D_I.CreateTemplate(out cellError, out numScale, out numNcc, out numScaleT, out numNccT))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机4模板加载失败,模板单元:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机4模板加载成功");
                            break;
                    }
                }
                if (numScale > 0)
                {
                    blScale = true;
                }
                if (numNcc > 0)
                {
                    blNcc = true;
                }
                if (numScaleT > 0)
                {
                    blScaleT = true;
                }
                if (numNccT > 0)
                {
                    blNccT = true;
                }
                #endregion 相机4

                #region 相机5
                if (ParCameraWork.NumCamera > 4)
                {
                    switch (DealComprehensive5.D_I.CreateTemplate(out cellError, out numScale, out numNcc, out numScaleT, out numNccT))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机5模板加载失败,模板单元:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机5模板加载成功");
                            break;
                    }
                }
                if (numScale > 0)
                {
                    blScale = true;
                }
                if (numNcc > 0)
                {
                    blNcc = true;
                }
                if (numScaleT > 0)
                {
                    blScaleT = true;
                }
                if (numNccT > 0)
                {
                    blNccT = true;
                }
                #endregion 相机5

                #region 相机6
                if (ParCameraWork.NumCamera > 5)
                {
                    switch (DealComprehensive6.D_I.CreateTemplate(out cellError, out numScale, out numNcc, out numScaleT, out numNccT))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机6模板加载失败,模板单元:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机6模板加载成功");
                            break;
                    }
                }
                if (numScale > 0)
                {
                    blScale = true;
                }
                if (numNcc > 0)
                {
                    blNcc = true;
                }
                if (numScaleT > 0)
                {
                    blScaleT = true;
                }
                if (numNccT > 0)
                {
                    blNccT = true;
                }
                #endregion 相机6

                #region 相机7
                if (ParCameraWork.NumCamera > 6)
                {
                    switch (DealComprehensive7.D_I.CreateTemplate(out cellError, out numScale, out numNcc, out numScaleT, out numNccT))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机7模板加载失败,模板单元:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机7模板加载成功");
                            break;
                    }
                }
                if (numScale > 0)
                {
                    blScale = true;
                }
                if (numNcc > 0)
                {
                    blNcc = true;
                }
                if (numScaleT > 0)
                {
                    blScaleT = true;
                }
                if (numNccT > 0)
                {
                    blNccT = true;
                }
                #endregion 相机7

                #region 相机8
                if (ParCameraWork.NumCamera > 7)
                {
                    switch (DealComprehensive8.D_I.CreateTemplate(out cellError, out numScale, out numNcc, out numScaleT, out numNccT))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机8模板加载失败,模板单元:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机8模板加载成功");
                            break;
                    }
                }
                if (numScale > 0)
                {
                    blScale = true;
                }
                if (numNcc > 0)
                {
                    blNcc = true;
                }
                if (numScaleT > 0)
                {
                    blScaleT = true;
                }
                if (numNccT > 0)
                {
                    blNccT = true;
                }
                #endregion 相机8

                if (blInitWin)
                {
                    //初始化模板窗体
                    new Task(new Action(() =>
                    {
                        InitTempWin(blScale, blNcc, blScaleT, blNccT);

                    })).Start();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 初始化模板窗体
        /// </summary>
        void InitTempWin(bool blScale, bool blNcc, bool blScaleT, bool blNccT)
        {
            try
            {
                int i = 0;
                while (i < 20
                    && !BlInitComprehensiveWin)
                {
                    i++;
                    Thread.Sleep(500);
                }

                this.Dispatcher.Invoke(new Action(() =>
                {
                    bool blNew = false;
                    if (blScale)
                    {
                        WinTempScaledShape.GetWinInst(out blNew).Show();
                        WinTempScaledShape.GetWinInst().Hide();
                        //WinTempScaledShape.GetWinInst().Topmost = true;
                    }
                    if (blNcc)
                    {
                        WinTempNcc.GetWinInst(out blNew).Show();
                        WinTempNcc.GetWinInst().Hide();
                        //WinTempNcc.GetWinInst().Topmost = true;
                    }
                    if (blScaleT)
                    {
                        WinScaledShapeT.GetWinInst(out blNew).Show();
                        WinScaledShapeT.GetWinInst().Hide();
                        //WinTempScaledShape.GetWinInst().Topmost = true;
                    }
                    if (blNccT)
                    {
                        WinNccT.GetWinInst(out blNew).Show();
                        WinNccT.GetWinInst().Hide();
                        //WinTempNcc.GetWinInst().Topmost = true;
                    }
                }));

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
            finally
            {
                BlInitComprehensiveTempWin = true;
                FinishInitWin();//结束初始化
            }
        }
        #endregion 模板

        #region 校准
        /// <summary>
        /// 加载校准
        /// </summary>
        void InitCalib()
        {
            try
            {
                string cellError = "";
                #region 相机1
                switch (DealComprehensive1.D_I.InitCalib(out cellError))
                {
                    case StateTemplate_enum.Null:
                        break;

                    case StateTemplate_enum.False:
                        ShowWinError_Invoke("相机1校准加载失败:" + cellError + "可重启软件！");
                        break;

                    case StateTemplate_enum.True:
                        ShowState("相机1校准加载成功");
                        break;
                }
                #endregion 相机1

                #region 相机2
                if (ParCameraWork.NumCamera > 1)
                {
                    switch (DealComprehensive2.D_I.InitCalib(out cellError))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机2校准加载失败:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机2校准加载成功");
                            break;
                    }
                }
                #endregion 相机2

                #region 相机3
                if (ParCameraWork.NumCamera > 2)
                {
                    switch (DealComprehensive3.D_I.InitCalib(out cellError))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机3模板加载失败:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机3校准加载成功");
                            break;
                    }
                }
                #endregion 相机3

                #region 相机4
                if (ParCameraWork.NumCamera > 3)
                {
                    switch (DealComprehensive4.D_I.InitCalib(out cellError))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机4模板加载失败:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机4校准加载成功");
                            break;
                    }
                }
                #endregion 相机4

                #region 相机5
                if (ParCameraWork.NumCamera > 4)
                {
                    switch (DealComprehensive5.D_I.InitCalib(out cellError))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机5校准加载失败:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机5校准加载成功");
                            break;
                    }
                }
                #endregion 相机5

                #region 相机6
                if (ParCameraWork.NumCamera > 5)
                {
                    switch (DealComprehensive6.D_I.InitCalib(out cellError))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机6校准加载失败:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机6校准加载成功");
                            break;
                    }
                }
                #endregion 相机6

                #region 相机7
                if (ParCameraWork.NumCamera > 6)
                {
                    switch (DealComprehensive7.D_I.InitCalib(out cellError))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机7校准加载失败:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机7校准加载成功");
                            break;
                    }
                }
                #endregion 相机7

                #region 相机8
                if (ParCameraWork.NumCamera > 7)
                {
                    switch (DealComprehensive8.D_I.InitCalib(out cellError))
                    {
                        case StateTemplate_enum.Null:
                            break;

                        case StateTemplate_enum.False:
                            ShowWinError_Invoke("相机8校准加载失败:" + cellError + "可重启软件！");
                            break;

                        case StateTemplate_enum.True:
                            ShowState("相机8校准加载成功");
                            break;
                    }
                }
                #endregion 相机8
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 校准

        #region 事件注销
        public void EventLogout_ImageProcess()
        {

        }
        #endregion 事件注销
    }
}
