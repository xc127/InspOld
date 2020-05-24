using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealPLC;
using System.Threading;
using System.Threading.Tasks;
using DealComprehensive;
using Common;
using BasicClass;
using System.Collections;
using DealDisplay;
using BasicDisplay;
using Camera;
using DealConfigFile;

namespace Main
{
    //处理外部触发拍照的命令,并调用DealComprehensiveResult类中的处理方法，进行处理并输出结果
    public partial class MainWindow
    {
        #region 定义
        Hashtable g_HtUCDisplayImage = new Hashtable();

        BaseUCDisplayCamera g_UCDisplayCamera1 = null;
        BaseUCDisplayCamera g_UCDisplayCamera2 = null;
        BaseUCDisplayCamera g_UCDisplayCamera3 = null;
        BaseUCDisplayCamera g_UCDisplayCamera4 = null;
        BaseUCDisplayCamera g_UCDisplayCamera5 = null;
        BaseUCDisplayCamera g_UCDisplayCamera6 = null;
        BaseUCDisplayCamera g_UCDisplayCamera7 = null;
        BaseUCDisplayCamera g_UCDisplayCamera8 = null;

        //线程互斥
        Mutex g_MtCamera1 = new Mutex();
        Mutex g_MtCamera2 = new Mutex();
        Mutex g_MtCamera3 = new Mutex();
        Mutex g_MtCamera4 = new Mutex();
        Mutex g_MtCamera5 = new Mutex();
        Mutex g_MtCamera6 = new Mutex();
        Mutex g_MtCamera7 = new Mutex();
        Mutex g_MtCamera8 = new Mutex();

        bool BlExCameraTrigger1 = false;
        bool BlExCameraTrigger2 = false;
        bool BlExCameraTrigger3 = false;
        bool BlExCameraTrigger4 = false;
        #endregion 定义

        #region 初始化
        /// <summary>
        /// 初始化控件和参数
        /// </summary>
        void Init_TrrigerDealResult()
        {
            try
            {
                //按照窗体顺序
                for (int i = 0; i < ParSetDisplay.P_I.NumWinDisplayImage; i++)
                {
                    BaseParSetDisplay baseParSetDisplay = ParSetDisplay.P_I[i];

                    if (baseParSetDisplay.TypeDisplayImage.Contains("Image")
                        && !g_HtUCDisplayImage.Contains(baseParSetDisplay.TypeDisplayImage))
                    {
                        g_HtUCDisplayImage.Add(baseParSetDisplay.TypeDisplayImage, g_BaseUCDisplaySum.g_BaseUCDisplayCameras[i]);
                    }
                    else//相机显示窗体只包含一个
                    {
                        switch (baseParSetDisplay.TypeDisplayImage_e)
                        {
                            case TypeDisplayImage_enum.Camera1:
                                g_UCDisplayCamera1 = (BaseUCDisplayCamera)g_BaseUCDisplaySum.g_BaseUCDisplayCameras[i];
                                break;

                            case TypeDisplayImage_enum.Camera2:
                                g_UCDisplayCamera2 = (BaseUCDisplayCamera)g_BaseUCDisplaySum.g_BaseUCDisplayCameras[i];
                                break;

                            case TypeDisplayImage_enum.Camera3:
                                g_UCDisplayCamera3 = (BaseUCDisplayCamera)g_BaseUCDisplaySum.g_BaseUCDisplayCameras[i];
                                break;

                            case TypeDisplayImage_enum.Camera4:
                                g_UCDisplayCamera4 = (BaseUCDisplayCamera)g_BaseUCDisplaySum.g_BaseUCDisplayCameras[i];
                                break;

                            case TypeDisplayImage_enum.Camera5:
                                g_UCDisplayCamera5 = (BaseUCDisplayCamera)g_BaseUCDisplaySum.g_BaseUCDisplayCameras[i];
                                break;

                            case TypeDisplayImage_enum.Camera6:
                                g_UCDisplayCamera6 = (BaseUCDisplayCamera)g_BaseUCDisplaySum.g_BaseUCDisplayCameras[i];
                                break;

                            case TypeDisplayImage_enum.Camera7:
                                g_UCDisplayCamera7 = (BaseUCDisplayCamera)g_BaseUCDisplaySum.g_BaseUCDisplayCameras[i];
                                break;

                            case TypeDisplayImage_enum.Camera8:
                                g_UCDisplayCamera8 = (BaseUCDisplayCamera)g_BaseUCDisplaySum.g_BaseUCDisplayCameras[i];
                                break;
                        }
                    }
                }

                DealComprehensiveResult1.D_I.Init(g_UCDisplayCamera1, g_HtUCDisplayImage, uCResult, uCAlarm, uCStateWork);
                DealComprehensiveResult2.D_I.Init(g_UCDisplayCamera2, g_HtUCDisplayImage, uCResult, uCAlarm, uCStateWork);
                DealComprehensiveResult3.D_I.Init(g_UCDisplayCamera3, g_HtUCDisplayImage, uCResult, uCAlarm, uCStateWork);
                DealComprehensiveResult4.D_I.Init(g_UCDisplayCamera4, g_HtUCDisplayImage, uCResult, uCAlarm, uCStateWork);
                DealComprehensiveResult5.D_I.Init(g_UCDisplayCamera5, g_HtUCDisplayImage, uCResult, uCAlarm, uCStateWork);
                DealComprehensiveResult6.D_I.Init(g_UCDisplayCamera6, g_HtUCDisplayImage, uCResult, uCAlarm, uCStateWork);
                DealComprehensiveResult7.D_I.Init(g_UCDisplayCamera7, g_HtUCDisplayImage, uCResult, uCAlarm, uCStateWork);
                DealComprehensiveResult8.D_I.Init(g_UCDisplayCamera8, g_HtUCDisplayImage, uCResult, uCAlarm, uCStateWork);
                this.Dispatcher.Invoke(new Action(delegate()
                {
                    DealComprehensiveResultTemp.D_I.InitInspControl(ucRecord, ucSingleRecord, ucResultInsp, ucSingleResult);
                }));
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 初始化

        #region 相机外触发线程
        void Init_ExCameraTrigger()
        {
            try
            {
                if (ParCamera1.P_I.BlUsingTrigger)
                {
                    BlExCameraTrigger1 = true;
                    new Task(new Action(ExCameraTrigger1)).Start();
                }

                if (ParCamera2.P_I.BlUsingTrigger)
                {
                    BlExCameraTrigger2 = true;
                    new Task(new Action(ExCameraTrigger2)).Start();
                }
                if (ParCamera3.P_I.BlUsingTrigger)
                {
                    BlExCameraTrigger3 = true;
                    new Task(new Action(ExCameraTrigger3)).Start();
                }
                if (ParCamera4.P_I.BlUsingTrigger)
                {
                    BlExCameraTrigger4 = true;
                    new Task(new Action(ExCameraTrigger4)).Start();
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        /// <summary>
        /// 相机1外触发
        /// </summary>
        void ExCameraTrigger1()
        {
            try
            {
                while (BlExCameraTrigger1)
                {
                    if (ParCamera1.P_I.BlUsingTrigger)//如果使用外触发
                    {
                        Thread.Sleep(20);
                        Camera_BSLSDK cb = Camera1.C_I.g_CameraAbstract as Camera_BSLSDK;
                        if (cb.CountQ > 0)//外触发队列里面有图像
                        {
                            DealComprehensive_Camera1_event(TriggerSource_enum.Camera, 1, 0);
                        }
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机1外触发
        /// </summary>
        void ExCameraTrigger2()
        {
            try
            {
                while (BlExCameraTrigger2)
                {
                    if (ParCamera2.P_I.BlUsingTrigger)//如果使用外触发
                    {
                        Thread.Sleep(20);
                        Camera_BSLSDK cb = Camera2.C_I.g_CameraAbstract as Camera_BSLSDK;
                        if (cb.CountQ > 0)//外触发队列里面有图像
                        {
                            DealComprehensive_Camera2_event(TriggerSource_enum.Camera, 1, 0);
                        }
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机1外触发
        /// </summary>
        void ExCameraTrigger3()
        {
            try
            {
                while (BlExCameraTrigger3)
                {
                    if (ParCamera3.P_I.BlUsingTrigger)//如果使用外触发
                    {
                        Thread.Sleep(20);
                        Camera_BSLSDK cb = Camera3.C_I.g_CameraAbstract as Camera_BSLSDK;
                        if (cb.CountQ > 0)//外触发队列里面有图像
                        {
                            DealComprehensive_Camera3_event(TriggerSource_enum.Camera, 1, 0);
                        }
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        /// <summary>
        /// 相机1外触发
        /// </summary>
        void ExCameraTrigger4()
        {
            try
            {
                while (BlExCameraTrigger4)
                {
                    if (ParCamera4.P_I.BlUsingTrigger)//如果使用外触发
                    {
                        Thread.Sleep(20);
                        Camera_BSLSDK cb = Camera4.C_I.g_CameraAbstract as Camera_BSLSDK;
                        if (cb.CountQ > 0)//外触发队列里面有图像
                        {
                            DealComprehensive_Camera4_event(TriggerSource_enum.Camera, 1, 0);
                        }
                    }
                    else
                    {
                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 相机外触发线程

        #region 相机1
        //传递算法，以及处理之前的拍照，和处理之后的数据处理
        void DealComprehensive_Camera1_event(TriggerSource_enum trigerSource_e, int i)
        {
            g_MtCamera1.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult1.D_I.DealComprehensiveResultFun(trigerSource_e, i);

                ShowState("相机1拍照调用结束");
                if (ParCameraWork.NumCamera > 1)
                {
                    DealComprehensiveResult2.D_I.DealComprehensiveResultFun(trigerSource_e, i);
                }

                if (ParCameraWork.NumCamera > 2)
                {
                    DealComprehensiveResult3.D_I.DealComprehensiveResultFun(trigerSource_e, i);
                }

                if (ParCameraWork.NumCamera > 3)
                {
                    DealComprehensiveResult4.D_I.DealComprehensiveResultFun(trigerSource_e, i);
                }

                //ShowInfo(trigerSource_e, stateComprehensive_e, 1, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera1.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考值index
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera1_event(TriggerSource_enum trigerSource_e, int i, int index)
        {
            g_MtCamera1.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult1.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 1, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera1.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考数组
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera1_event(TriggerSource_enum trigerSource_e, int i, int[] index)
        {
            g_MtCamera1.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult1.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 1, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera1.ReleaseMutex();
            }
        }
        #endregion 相机1

        #region 相机2
        void DealComprehensive_Camera2_event(TriggerSource_enum trigerSource_e, int i)
        {
            g_MtCamera2.WaitOne();
            try
            {
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult2.D_I.DealComprehensiveResultFun(trigerSource_e, i);
                ShowInfo(trigerSource_e, stateComprehensive_e, 2, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera2.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考值index
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera2_event(TriggerSource_enum trigerSource_e, int i, int index)
        {
            g_MtCamera2.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult2.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 2, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera2.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考数组
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera2_event(TriggerSource_enum trigerSource_e, int i, int[] index)
        {
            g_MtCamera2.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult2.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 2, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera2.ReleaseMutex();
            }
        }
        #endregion 相机2

        #region 相机3
        void DealComprehensive_Camera3_event(TriggerSource_enum trigerSource_e, int i)
        {
            g_MtCamera3.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult3.D_I.DealComprehensiveResultFun(trigerSource_e, i);
                ShowInfo(trigerSource_e, stateComprehensive_e, 3, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera3.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考值index
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera3_event(TriggerSource_enum trigerSource_e, int i, int index)
        {
            g_MtCamera3.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult3.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 3, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera3.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考数组
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera3_event(TriggerSource_enum trigerSource_e, int i, int[] index)
        {
            g_MtCamera3.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult3.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 3, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera3.ReleaseMutex();
            }
        }
        #endregion 相机3

        #region 相机4
        void DealComprehensive_Camera4_event(TriggerSource_enum trigerSource_e, int i)
        {
            g_MtCamera4.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult4.D_I.DealComprehensiveResultFun(trigerSource_e, i);
                ShowInfo(trigerSource_e, stateComprehensive_e, 4, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera4.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考值index
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera4_event(TriggerSource_enum trigerSource_e, int i, int index)
        {
            g_MtCamera4.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult4.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 4, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera4.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考数组
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera4_event(TriggerSource_enum trigerSource_e, int i, int[] index)
        {
            g_MtCamera4.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult4.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 4, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera4.ReleaseMutex();
            }
        }
        #endregion 相机4

        #region 相机5
        void DealComprehensive_Camera5_event(TriggerSource_enum trigerSource_e, int i)
        {
            g_MtCamera5.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult5.D_I.DealComprehensiveResultFun(trigerSource_e, i);
                ShowInfo(trigerSource_e, stateComprehensive_e, 5, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera5.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考值index
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera5_event(TriggerSource_enum trigerSource_e, int i, int index)
        {
            g_MtCamera5.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult5.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 5, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera5.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考数组
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera5_event(TriggerSource_enum trigerSource_e, int i, int[] index)
        {
            g_MtCamera5.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult5.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 5, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera5.ReleaseMutex();
            }
        }
        #endregion 相机5

        #region 相机6
        void DealComprehensive_Camera6_event(TriggerSource_enum trigerSource_e, int i)
        {
            g_MtCamera6.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult6.D_I.DealComprehensiveResultFun(trigerSource_e, i);
                ShowInfo(trigerSource_e, stateComprehensive_e, 6, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera6.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考值index
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera6_event(TriggerSource_enum trigerSource_e, int i, int index)
        {
            g_MtCamera6.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult6.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 6, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera6.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考数组
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera6_event(TriggerSource_enum trigerSource_e, int i, int[] index)
        {
            g_MtCamera6.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult6.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 6, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera6.ReleaseMutex();
            }
        }
        #endregion 相机6

        #region 相机7
        void DealComprehensive_Camera7_event(TriggerSource_enum trigerSource_e, int i)
        {
            g_MtCamera7.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult7.D_I.DealComprehensiveResultFun(trigerSource_e, i);
                ShowInfo(trigerSource_e, stateComprehensive_e, 7, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera7.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考值index
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera7_event(TriggerSource_enum trigerSource_e, int i, int index)
        {
            g_MtCamera7.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult7.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 7, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera7.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考数组
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera7_event(TriggerSource_enum trigerSource_e, int i, int[] index)
        {
            g_MtCamera7.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult7.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 7, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera7.ReleaseMutex();
            }
        }
        #endregion 相机7

        #region 相机8
        void DealComprehensive_Camera8_event(TriggerSource_enum trigerSource_e, int i)
        {
            g_MtCamera8.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult8.D_I.DealComprehensiveResultFun(trigerSource_e, i);
                ShowInfo(trigerSource_e, stateComprehensive_e, 8, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera8.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考值index
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera8_event(TriggerSource_enum trigerSource_e, int i, int index)
        {
            g_MtCamera8.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult8.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 8, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera8.ReleaseMutex();
            }
        }

        /// <summary>
        /// 触发拍照并传入区分参考数组
        /// </summary>
        /// <param name="trigerSource_e"></param>
        /// <param name="i"></param>
        /// <param name="index"></param>
        void DealComprehensive_Camera8_event(TriggerSource_enum trigerSource_e, int i, int[] index)
        {
            g_MtCamera8.WaitOne();
            try
            {
                //同一流程内，第几次拍照
                StateComprehensive_enum stateComprehensive_e = DealComprehensiveResult8.D_I.DealComprehensiveResultFun(trigerSource_e, i, index);

                ShowInfo(trigerSource_e, stateComprehensive_e, 8, i);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
            finally
            {
                g_MtCamera8.ReleaseMutex();
            }
        }
        #endregion 相机8

        #region 显示
        void ShowInfo(TriggerSource_enum trigerSource_e, StateComprehensive_enum stateComprehensive_e, int i, int j)
        {
            try
            {
                switch (stateComprehensive_e)
                {
                    case StateComprehensive_enum.False:
                        ShowAlarm(trigerSource_e.ToString() + "触发相机" + i.ToString() + "第" + j.ToString() + "次图像处理异常！");
                        break;

                    case StateComprehensive_enum.NGTrue:
                        ShowAlarm(trigerSource_e.ToString() + "触发相机" + i.ToString() + "第" + j.ToString() + "次图像处理异常！但不产生警报！");
                        break;

                    case StateComprehensive_enum.True:
                        ShowState(trigerSource_e.ToString() + "触发相机" + i.ToString() + "第" + j.ToString() + "次图像处理成功！");
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
        #endregion 显示

    }
}
