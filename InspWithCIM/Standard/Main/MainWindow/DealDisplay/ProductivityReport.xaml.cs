using System;
using System.Collections.Generic;
using System.Windows;
using BasicClass;
using DealFile;

namespace Main
{
    /// <summary>
    /// ProductivityReport.xaml 的交互逻辑
    /// </summary>
    public partial class ProductivityReport : Window
    {
        public static string ReportIniPath
        {
            get
            {
                return ParPathRoot.PathRoot + "软件运行记录\\Custom\\" + "Report.ini";
            }
        }

        static Queue<ReportData> Data_Q = new Queue<ReportData>();

        public ProductivityReport()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Data_Q.Clear();
            ReadReportIni();
            ProReportDG.ItemsSource = Data_Q;
        }

        private void ReadReportIni()
        {
            try
            {
                for (int i = 0; i < 30; ++i)
                {
                    string section = "Report" + i.ToString();
                    ReportData temp = new ReportData();
                    temp.Date = ReadIniStr(section, "Date");
                    temp.NumAll = ReadIniStr(section, "NumAll");
                    temp.NumNG = ReadIniStr(section, "NumNG");
                    temp.NumNGCorner = ReadIniStr(section, "NumNGCorner");
                    temp.NumNGShell = ReadIniStr(section, "NumNGShell");
                    temp.NumNGOther = ReadIniStr(section, "NumNGOther");
                    temp.CodeNG = ReadIniStr(section, "CodeNG");
                    temp.ChipIDNG = ReadIniStr(section, "ChipIDNG");

                    if (temp.Date != string.Empty)
                    {
                        Data_Q.Enqueue(temp);
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch
            {

            }
        }

        public string ReadIniStr(string section, string key)
        {
            try
            {
                return IniFile.I_I.ReadIniStr(section, key, ReportIniPath);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static void WriteReportIni(int i)
        {
            try
            {
                AddNew(i);
                int j = 0;
                foreach (object obj in Data_Q)
                {
                    string section = "Report" + j.ToString();
                    ReportData temp = (ReportData)obj;

                    WriteIniStr(section, "Date", temp.Date);
                    WriteIniStr(section, "NumAll", temp.NumAll);
                    WriteIniStr(section, "NumNG", temp.NumNG);
                    WriteIniStr(section, "NumNGCorner", temp.NumNGCorner);
                    WriteIniStr(section, "NumNGShell", temp.NumNGShell);
                    WriteIniStr(section, "NumNGOther", temp.NumNGOther);
                    WriteIniStr(section, "CodeNG", temp.CodeNG);
                    WriteIniStr(section, "ChipIDNG", temp.ChipIDNG);
                    j++;
                }
            }
            catch
            {
            }
        }

        public static void WriteIniStr(string section, string key, string value)
        {
            try
            {
                IniFile.I_I.WriteIni(section, key, value, ReportIniPath);
                return;
            }
            catch
            {
            }
        }

        static void AddNew(int i)
        {
            if (Data_Q.Count == 30)
            {
                Data_Q.Dequeue();
            }

            ReportData temp = new ReportData();
            if (i == 1)
            {
                temp.Date = DateTime.Today.ToShortDateString() + "Day";
            }
            else
            {
                temp.Date = DateTime.Today.AddDays(-1).ToShortDateString() + "Night";
            }
            temp.NumAll = ParAnalysis.P_I.g_ProductNumInfoNow.NumAll.ToString();
            temp.NumNG = ParAnalysis.P_I.g_ProductNumInfoNow.NumNG.ToString();
            temp.NumNGCorner = ParAnalysis.P_I.g_ProductNumInfoNow.NumNGCorner.ToString();
            temp.NumNGShell = ParAnalysis.P_I.g_ProductNumInfoNow.NumNGShell.ToString();
            temp.NumNGOther = ParAnalysis.P_I.g_ProductNumInfoNow.NumNGOther.ToString();
            temp.CodeNG = DealCIM.CIM.CodeNGCount.ToString();
            temp.ChipIDNG = DealCIM.CIM.ChipIDNGCount.ToString();

            Data_Q.Enqueue(temp);
        }

        public static void ClearReportData()
        {
            try
            {
                ParAnalysis.P_I.g_ProductNumInfoNow.NumAll = 0;
                ParAnalysis.P_I.g_ProductNumInfoNow.NumOK = 0;
                ParAnalysis.P_I.g_ProductNumInfoNow.NumNG = 0;
                ParAnalysis.P_I.g_ProductNumInfoNow.NumNGShell = 0;
                ParAnalysis.P_I.g_ProductNumInfoNow.NumNGCorner = 0;
                ParAnalysis.P_I.g_ProductNumInfoNow.NumNGOther = 0;
                DealCIM.CIM.CodeNGCount = 0;
                DealCIM.CIM.ChipIDNGCount = 0;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
    }

    public class ReportData
    {
        public string Date { get; set; }
        public string NumAll { get; set; }
        public string NumNG { get; set; }
        public string NumNGCorner { get; set; }
        public string NumNGShell { get; set; }
        public string NumNGOther { get; set; }
        public string CodeNG { get; set; }
        public string ChipIDNG { get; set; }
    }
}
