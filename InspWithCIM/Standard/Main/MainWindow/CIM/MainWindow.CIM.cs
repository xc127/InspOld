using BasicClass;
using DealPLC;
using DealRobot;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DealCIM;
using System.Configuration;

namespace Main
{
    partial class MainWindow
    {
        #region 配置
        //二维码
        public QRCodeBase Code = null;

        static int[] plc_addr = {(int)DataRegister1.ChipIDResult,
                                (int)DataRegister1.RunCardResult,
                                (int)DataRegister1.TrackOutResult};

        const bool isPLC = true;
        const int OK = 1;
        const int NG = 2;
        const int ON = 1;
        const int OFF = 2;

        static XmlHelplerFactory factory = new XmlHelper_XM();
        XmlCreaterBase xmlCreater = factory.GetXmlCreater();
        //XmlCreaterBase xmlCreater = Activator.CreateInstance(
        //    Type.GetType(ConfigurationManager.AppSettings["cimcreater"].ToString())) as XmlCreaterBase;
        XmlParserBase xmlParser = factory.GetXmlParser();
        //XmlParserBase xmlParser = Activator.CreateInstance(
        //        Type.GetType(ConfigurationManager.AppSettings["cimparser"].ToString())
        //        ) as XmlParserBase;

        public Task MonitorTask = null;

        #endregion

        /// <summary>
        /// 新建一个Task来初始化CIM端口
        /// </summary>
        public void Init_CIM()
        {
            PostParams.P_I.InitParams();
            new Task(new Action(() =>
            {
                try
                {
                    ConnectCode();
                    ConnectCIM();                    
                }
                catch (Exception ex)
                {
                    Log.L_I.WriteError(NameClass, ex);
                }
            })).Start();
            //二维码初始化

            FTPSDK.FtpConnectionModel data = new FTPSDK.FtpConnectionModel
            {
                Host = @"ftp://192.168.0.106/",
                Username = "xc",
                Password = "910127",
                Port = 21
            };
            ftp = new FTPSDK.RequesterFtp(data);
        }

        void ConnectCode()
        {
            try
            {
                if (!ModelParams.DefaultQrCodeOK)
                {
                    Code = CodeFactory.Instance.GetCodeType(PostParams.P_I.ETypeCode);
                    if (Code.Init(PostParams.P_I.StrCom, PostParams.P_I.iBaudrate))
                        SetCodeStatus("二维码初始化成功", false);
                    else
                        SetCodeStatus("二维码初始化失败", true);
                }
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }

        void ConnectCIM()
        {
            try
            {
                if (ModelParams.IfCimOn)
                {
                    ShowState(string.Format("导入ChipID数量：{0}", CIM.ChipIDCount));
                    CIM.LoadList(CIM.ChipIDCount);
                    if (CIM.C_I.Init())
                    {
                        // Task.Factory.StartNew(ReceiveHelper.Monitor);
                        MonitorTask = Task.Factory.StartNew(ReceiveHelper.Monitor, xmlParser);
                        SetCimStatus("CIM连接成功", false);
                    }
                    else
                        SetCimStatus("CIM连接失败", true);
                }
                else
                    SetCimStatus("CIM屏蔽", true);
            }
            catch(Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
    }
}
