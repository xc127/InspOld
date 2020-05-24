using System;
using System.Collections.Generic;
using System.Text;
using DealPLC;
using System.Threading;
using Common;
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

namespace Main
{
    public partial class BaseDealComprehensiveResult
    {
        #region 定义
        bool g_BlManualCycleTest = false;
        #endregion

        public void LocalTest()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "图片|*.jpg;*.bmp;*.jpeg;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            try
            {
                var arr = openFileDialog.FileNames;
                if (arr.Length > 0)
                {
                    g_UCDisplayCamera.BlLocalImage = true;
                    g_UCDisplayCamera.g_PathLocalImage_L = new List<string>(arr);
                    g_UCDisplayCamera.BlAutoNextLocal = true;
                    g_UCDisplayCamera.NoLocalImage = -1;
                    ImageNeedDeal_Q.Clear();
                    for (int i = 0; i < arr.Length; i++)
                    {
                        ImageNeedDeal_Q.Enqueue(new DataImage(g_UCDisplayCamera.GrabImageHal()));
                        Thread.Sleep(10);
                        //listImage.Add(g_UCDisplayCamera.GrabImageHal());SSSS
                    }

                    g_BlManualCycleTest = true;
                    SideIndex = 1;
                    g_NumImageSide = 0;
                    g_NumAllImage = ImageNeedDeal_Q.Count;
                    g_NumInvalid = 0;
                    BasePathImageSave = CreateImageDir();
                    ResultInspSingeCell_L.Clear();
                    //xc-1229
                    //DealComprehensiveResultTemp.D_I.g_UCSingleRecord.blRefresh = true;
                    UCRecordTemp.blRefresh = true;

                    CodeNow = "LocalTest";
                    //new Thread(DealQueue).Start();
                    StartProcess = true;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseDealComprehensiveResult.ManualTest", ex);
            }
        }

        public void GetDefectData(string[] fileNames, int sideIndex, Camera.BaseUCDisplayCamera baseUCDisplayCamera,
            ref List<ResultInspection> result_list, bool ifSave = true)
        {
            try
            {
                if (fileNames.Length > 0)
                {
                    baseUCDisplayCamera.BlLocalImage = true;
                    baseUCDisplayCamera.g_PathLocalImage_L = new List<string>(fileNames);
                    baseUCDisplayCamera.BlAutoNextLocal = true;
                    baseUCDisplayCamera.NoLocalImage = -1;
                    ImageNeedDeal_Q.Clear();
                    for (int i = 0; i < fileNames.Length; i++)
                    {
                        ImageNeedDeal_Q.Enqueue(new DataImage(baseUCDisplayCamera.GrabImageAndShow()));
                        Thread.Sleep(10);
                    }

                    g_BlManualCycleTest = true;
                    SideIndex = 1;
                    g_NumImageSide = 0;
                    g_NumAllImage = ImageNeedDeal_Q.Count;
                    g_NumInvalid = 0;
                    BasePathImageSave = CreateImageDir();
                    ResultInspSingeCell_L.Clear();
                    //xc-1229
                    //DealComprehensiveResultTemp.D_I.g_UCSingleRecord.blRefresh = true;
                    UCRecordTemp.blRefresh = true;

                    DealQueue_Sample(sideIndex, ref result_list, ifSave);
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("BaseDealComprehensiveResult.ManualTest", ex);
            }
        }

        public bool SaveSampleParams(string path, int index)
        {
            try
            {
                ParInspection parInspection = g_ParIns.GetParBySideIndex(index);
                parInspection.WriteIni_Sample(index, path);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
                return false;
            }
            return true;
        }
    }
}



