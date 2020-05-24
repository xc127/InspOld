using BasicClass;
using DealFile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml;

namespace Main
{
    public partial class SampleManager
    {
        #region define
        //静态实例
        public static SampleManager instance = new SampleManager();
        const string ClassName = "SampleManager";
        //私有构造函数，防止被创建
        private SampleManager() { }
        //缺陷列表
        public List<ResultInspection> SampleResult_list = new List<ResultInspection>();
        public List<ResultInspection> LocalResult_list = new List<ResultInspection>();
        //界面显示列表
        internal ObservableCollection<VModel> SampleInfo_list { get; set; } = new ObservableCollection<VModel>();
        internal ObservableCollection<VModel> LocalInfo_list { get; set; } = new ObservableCollection<VModel>();
        //路径常量
        public static readonly string SampleRootPath = ParPathRoot.PathRoot + @"Store\Sample\";
        public static readonly string SampleImagePath = SampleRootPath + @"Image\";
        public static readonly string SampleParPath = SampleRootPath + "par.ini";
        public static readonly string SampleDefectPath = SampleRootPath + "defect.ini";
        public static readonly string SampleDefectPath_XMLModule = SampleRootPath + "defect.xml";
        public static readonly string SampleDefectPath_XML = SampleRootPath + "sample_defect.xml";
        const string Section = "defect";
        
        public delegate void ShowResult_event(ResultInspection result, Camera.BaseUCDisplayCamera baseUCDisplayCamera);
        public static ShowResult_event ShowResult;
        public static int FileIndex = 0;
        XmlDocument xmlDoc = new XmlDocument();

        public static double MeanGray = 0;
        public static double Sharpness = 0;
        #endregion

        #region setSample
        /// <summary>
        /// 创建sample时，将图片路径保存下来
        /// </summary>
        /// <param name="fileNames"></param>
        public void SetFileName(string[] fileNames)
        {
            SampleInfo_list.Clear();
            SampleResult_list.Clear();
            int i = 0;
            foreach (string item in fileNames)
            {
                SampleInfo_list.Add(new VModel() { Index = i, FileName = item });
                SampleResult_list.Add(new ResultInspection { ImagePath = item });
                ++i;
            }
        }
        /// <summary>
        /// 创建子节点，以img为单位，同时所有img依附于参数side
        /// </summary>
        /// <param name="faultInfos"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private XmlElement CreateDefectNode(List<FaultInfo> faultInfos, int index)
        {            
            XmlElement xe = xmlDoc.CreateElement("img" + index);
            foreach (FaultInfo fault in faultInfos)
            {
                XmlElement xeDefect = xmlDoc.CreateElement("Type");
                xeDefect.SetAttribute("kind", fault.FaultType_E.ToString());                
                XmlElement xeTemp = xmlDoc.CreateElement("Width");
                xeTemp.InnerText = fault.WidthFault.ToString();
                xeDefect.AppendChild(xeTemp);
                xeTemp = xmlDoc.CreateElement("Depth");
                xeTemp.InnerText = fault.DepthFault.ToString();
                xeDefect.AppendChild(xeTemp);
                xeTemp = xmlDoc.CreateElement("X");
                xeTemp.InnerText = fault.PosFalut.DblValue1.ToString();
                xeDefect.AppendChild(xeTemp);
                xeTemp = xmlDoc.CreateElement("Y");
                xeTemp.InnerText = fault.PosFalut.DblValue2.ToString();
                xeDefect.AppendChild(xeTemp);
                xe.AppendChild(xeDefect);
            }           
            return xe;
        }
        /// <summary>
        /// 保存defect到本地的外部接口
        /// </summary>
        /// <returns></returns>
        public bool SaveSampleDefect(int sideIndex)
        {
            int i = 0;
            try
            {
                if(!File.Exists(SampleDefectPath_XMLModule))
                {
                    MessageBox.Show("Store中缺少模板文件！");
                    return false;
                }
                xmlDoc.Load(SampleDefectPath_XMLModule);
                XmlNode root = xmlDoc.SelectSingleNode("defect_info");
                XmlElement xe = xmlDoc.CreateElement("Side" + sideIndex);
                foreach (ResultInspection item in SampleResult_list)
                {                    
                    xe.AppendChild(CreateDefectNode(item.SingleFalutInfo_L, i));
                    i++;
                }
                root.AppendChild(xe);
                xmlDoc.Save(SampleDefectPath_XML);
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(ClassName, ex);
            }
            return true;
        }
        #endregion

        #region 加载defect
        /// <summary>
        /// 加载本地记录的缺陷，同时初始化界面列表和缺陷列表，包括sample和local的
        /// </summary>
        public void LoadSampleDefect_XML(int sideIndex)
        {
            SampleResult_list.Clear();
            SampleInfo_list.Clear();
            LocalInfo_list.Clear();
            LocalResult_list.Clear();

            xmlDoc.Load(SampleDefectPath_XML);
            XmlNode root = xmlDoc.SelectSingleNode("defect_info");
            XmlNodeList xnl = root.SelectSingleNode("Side" + sideIndex).ChildNodes;
            int i = 0;
            //获得img
            foreach(XmlNode xn in xnl)
            {
                ResultInspection resultInspection = new ResultInspection();
                VModel vModel = new VModel();

                string imagePath = SampleImagePath + sideIndex + @"\" + i + @".jpg";
                resultInspection.ImagePath = vModel.FileName = imagePath;

                LocalInfo_list.Add(new VModel() { Index = i, FileName = imagePath });
                LocalResult_list.Add(new ResultInspection());
                SampleInfo_list.Add(vModel);
                SampleResult_list.Add(resultInspection);
                //获得defect
                foreach (XmlNode xn_defect in xn.ChildNodes)
                {
                    //获取缺陷种类
                    string kind = ((XmlElement)xn_defect).GetAttribute("kind");
                    if (kind == "贝壳")
                        vModel.Shell = true;
                    else if (kind == "破角")
                        vModel.Cornor = true;
                    else if (kind == "凸边")
                        vModel.Convex = true;
                    
                    FaultInfo faultInfo = new FaultInfo()
                    {
                        FaultType_E = (FaultType_Enum)Enum.Parse(typeof(FaultType_Enum), kind),
                        PosFalut = new Point2D()
                    };
                    //获取具体的缺陷参数
                    foreach(XmlNode xn_data in xn_defect.ChildNodes)
                    {
                        if (xn_data.Name == "Width")
                            faultInfo.WidthFault = double.Parse(xn_data.InnerText);
                        if (xn_data.Name == "Depth")
                            faultInfo.DepthFault = double.Parse(xn_data.InnerText);
                        if (xn_data.Name == "X")
                            faultInfo.PosFalut.DblValue1 = double.Parse(xn_data.InnerText);
                        if (xn_data.Name == "Y")
                            faultInfo.PosFalut.DblValue2 = double.Parse(xn_data.InnerText);
                    }
                    resultInspection.SingleFalutInfo_L.Add(faultInfo);
                }
                ++i;
            }
        }
        /// <summary>
        /// 读取单个section中的缺陷数据
        /// </summary>
        /// <param name="faultType_Enum"></param>
        /// <param name="index"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private FaultInfo LoadSection(FaultType_Enum faultType_Enum, int index, string path)
        {
            return new FaultInfo()
            {
                WidthFault = IniFile.I_I.ReadIniDbl(Section + FaultType_Enum.贝壳 + index, "Width", path),
                DepthFault = IniFile.I_I.ReadIniDbl(Section + FaultType_Enum.贝壳 + index, "Depth", path),
                PosFalut = new Point2D(IniFile.I_I.ReadIniDbl(Section + FaultType_Enum.贝壳 + index, "X", path),
                IniFile.I_I.ReadIniDbl(Section + FaultType_Enum.贝壳 + index, "Y", path)),
                FaultType_E = FaultType_Enum.贝壳
            };
        }
        #endregion
    }
}
