using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Diagnostics;
using DealLog;
using System.Windows;

namespace DealPLC
{
    public class ReadExcelReg
    {
        #region 静态类实例
        public static ReadExcelReg R_I = new ReadExcelReg();
        #endregion 静态类实例

        public BaseRegPLC ReadExcel(string path)
        {
            return null;
            BaseRegPLC baseRegPLC = new BaseRegPLC();
            //try
            //{
            //    CloseProcess("EXCEL");
            //    Excel.Application xlsApp = new Excel.Application();   //创建Application对象

            //    Excel.Workbook xlsBook;
            //    object missing = Missing.Value;
            //    xlsBook = xlsApp.Workbooks.Open(path,
            //        missing, missing, missing, missing, missing,
            //        missing, missing, missing, missing, missing,
            //        missing, missing, missing, missing);
            //    Excel.Worksheet xlsSheet = (Excel.Worksheet)xlsBook.ActiveSheet;

            //    //起始寄存器
            //    Excel.Range range = (Excel.Range)xlsSheet.Cells[1, 2];
            //    baseRegPLC.regStart = range.Value2.ToString();

            //    range = (Excel.Range)xlsSheet.Cells[2, 2];
            //    baseRegPLC.NumRegSet = int.Parse(range.Value2.ToString());

            //    for (int i = 0; i < baseRegPLC.NumRegSet; i++)
            //    {
            //        RegPLC regPLC = new RegPLC();
            //        try
            //        {
            //            range = (Excel.Range)xlsSheet.Cells[i + 4, 1];
            //            string low = range.Value2.ToString();

            //            range = (Excel.Range)xlsSheet.Cells[i + 4, 2];
            //            string high = range.Value2.ToString();
            //            regPLC.NameReg = low + high;

            //            try
            //            {
            //                range = (Excel.Range)xlsSheet.Cells[i + 4, 3];
            //                regPLC.PLCSend = range.Value2.ToString();
            //            }
            //            catch (Exception ex)
            //            {

            //            }

            //            try
            //            {
            //                range = (Excel.Range)xlsSheet.Cells[i + 4, 4];
            //                regPLC.PCSend = range.Value2.ToString();
            //            }
            //            catch (Exception ex)
            //            {

            //            }
                        
            //            range = (Excel.Range)xlsSheet.Cells[i + 4, 5];
            //            regPLC.DblMin = double.Parse(range.Value2.ToString());

            //            range = (Excel.Range)xlsSheet.Cells[i + 4, 6];
            //            regPLC.DblMax = double.Parse(range.Value2.ToString());

            //            range = (Excel.Range)xlsSheet.Cells[i + 4, 7];
            //            regPLC.Co = double.Parse(range.Value2.ToString());

            //            try
            //            {
            //                range = (Excel.Range)xlsSheet.Cells[i + 4, 8];
            //                regPLC.Annotation = range.Value2.ToString();
            //            }
            //            catch (Exception ex)
            //            {

            //            }

            //            regPLC.NameCustomReg = "";
            //            regPLC.No = i;

            //        }
            //        catch (Exception)
            //        {
            //            MessageBox.Show("Excel寄存器配置有误");
            //            return baseRegPLC;
            //        }
            //        baseRegPLC.Reg_L.Add(regPLC);
            //    }
            //    try
            //    {
            //        xlsSheet = null;
            //        xlsBook.Close(true, Type.Missing, Type.Missing);
            //        xlsBook = null;
            //        xlsApp.Quit();
            //        xlsApp = null;
            //    }
            //    catch (Exception ex)
            //    {

            //    }

            //    return baseRegPLC;
            //}
            //catch (Exception ex)
            //{
            //    return null;
            //}
            //finally
            //{
            //    CloseProcess("EXCEL");
            //}
        }

        /// <summary>
        /// 关闭所有Excel进程
        /// </summary>
        /// <param name="P_str_Process">"EXCEL"</param>
        public void CloseProcess(string process)
        {
            try
            {
                Process[] excelProcess = Process.GetProcessesByName(process);//实例化进程对象
                foreach (Process p in excelProcess)
                {
                    p.Kill();//关闭进程
                }
            }
            catch (Exception ex)
            {
                //Log.L_I.WriteError("XmlFile", ex);
            }           
        }
    }
}
