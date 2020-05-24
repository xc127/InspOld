using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealPLC;
using System.Threading;
using System.Threading.Tasks;
using DealFile;
using DealComprehensive;
using Common;
using SetPar;
using ParComprehensive;
using BasicClass;
using Camera;
using System.Collections;
using DealResult;
using DealConfigFile;
using DealCalibrate;
using DealRobot;

namespace Main
{
    public class DealComprehensiveResultTemp : BaseDealComprehensiveResult
    {
        #region 静态类实例
        public static DealComprehensiveResultTemp D_I = new DealComprehensiveResultTemp();

        readonly string g_NameClass = "MyResultControls";
        #endregion 静态类实例

        public BaseUCDisplayCamera g_UCSingleResult = null;

        public void InitInspControl(UCRecord ucRecord, UCRecordTemp ucRecordTemp, UCResultIns ucResultInsp, BaseUCDisplayCamera ucDisp)
        {
            try
            {
                g_UCRecord = ucRecord;
                g_UCSingleRecord = ucRecordTemp;
                g_UCSingleResult = ucDisp;
                g_UCResultInsp = ucResultInsp;

                if (!ParSetWork.P_I[8].BlSelect)
                {
                    g_UCRecord.dgRecord.ItemsSource = ResultInspCell_L;
                    g_UCRecord.DGRecordSelChanged_event += new IntAction(g_UCRecord_DGRecordSelChanged_event);
                    SampleManager.ShowResult += new SampleManager.ShowResult_event(ShowResult_Sample);
                }

                if (!ParSetWork.P_I[9].BlSelect)
                {
                    g_UCSingleRecord.dgRecordSingle.ItemsSource = ResultInspSingeCell_L;
                    g_UCSingleRecord.DGSelChanged_event += new IntAction(g_UCSingleRecord_DGSelChanged_event);
                }

                g_ParIns = ParCam1.P_I;

            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(g_NameClass, ex);
            }
        }

        #region 单个显示

        void g_UCSingleRecord_DGSelChanged_event(int i)
        {
            try
            {
                ResultInspection result = ResultInspSingeCell_L[i];
                ShowResult(result);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(g_NameClass, ex);
            }
        }

        void g_UCRecord_DGRecordSelChanged_event(int i)
        {
            try
            {
                ResultInspection result = ResultInspCell_L[i];
                ShowResult(result);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(g_NameClass, ex);
            }
        }

        void ShowResult(ResultInspection result)
        {
            try
            {
                bool existed = System.IO.File.Exists(result.ImagePath);
                if (result.ImagePath != null && System.IO.File.Exists(result.ImagePath))
                {
                    g_UCSingleResult.ClearShapeHalWin();
                    g_UCSingleResult.LoadLocalImage(result.ImagePath);
                    List<double[]> list = CreatRect1(result);
                    foreach (double[] value in list)
                    {
                        g_UCSingleResult.DispRectangle1(value, 1, "red", "margin");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void ShowResult_Sample(ResultInspection result, BaseUCDisplayCamera baseUCDisplayCamera)
        {
            try
            {
                baseUCDisplayCamera.ClearShapeHalWin();
                baseUCDisplayCamera.LoadLocalImage(result.ImagePath);
                if (result.ImagePath != null && System.IO.File.Exists(result.ImagePath))
                {
                    List<double[]> list = CreatRect1(result);
                    foreach (double[] value in list)
                    {
                        baseUCDisplayCamera.DispRectangle1(value, 1, "red", "margin");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion
    }
}
