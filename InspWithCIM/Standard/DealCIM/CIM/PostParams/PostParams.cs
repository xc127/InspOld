using BasicClass;
using System.ComponentModel;

namespace DealCIM
{
    public partial class PostParams : INotifyPropertyChanged
    {
        string ClassName = "PostParams";
        //配置文件section
        public const string commonSection = "CONFIG";
        //配置文件路径
        public static string Path_Config = ParPathRoot.PathRoot + "Store\\Custom\\" + "Cim.ini";

        public static PostParams P_I = new PostParams();
        #region params
        /// <summary>
        /// 根节点内容
        /// </summary>
        public const string strRoot = @"si300_interface";
        /// <summary>
        /// 用户id
        /// </summary>
        string userid;
        public string StrUserID { get { return userid; } set { userid = value; NotifyPropertyChanged("StrUserID"); } }
        /// <summary>
        /// fab号
        /// </summary>
        string fab;
        public string StrFab { get { return fab; } set { fab = value; NotifyPropertyChanged("StrFab"); } }
        /// <summary>
        /// 工作区域
        /// </summary>
        string area;
        public string StrArea { get { return area; } set { area = value; NotifyPropertyChanged("StrArea"); } }
        /// <summary>
        /// 线体
        /// </summary>
        string line;
        public string StrLine { get { return line; } set { line = value; NotifyPropertyChanged("StrLine"); } }
        /// <summary>
        /// 工作站
        /// </summary>
        string operation;
        public string StrOperation { get { return operation; } set { operation = value; NotifyPropertyChanged("StrOperation"); } }
        /// <summary>
        /// 有效的lot号
        /// </summary>
        string lot;
        public string StrLot { get { return lot; } set { lot = value; NotifyPropertyChanged("StrLot"); } }

        string templot;
        public string StrTempLot { get { return templot; } set { templot = value; } }
        /// <summary>
        /// runcard型号
        /// </summary>
        string modelno;
        public string StrModelNo { get { return modelno; } set { modelno = value; NotifyPropertyChanged("StrModelNo"); } }
        /// <summary>
        /// 发送数据队列号
        /// </summary>
        string sendqueue;
        public string StrSendQueue { get { return sendqueue; } set { sendqueue = value; NotifyPropertyChanged("StrSendQueue"); } }
        /// <summary>
        /// 接收数据队列号
        /// </summary>
        string readqueue;
        public string StrReadQueue { get { return readqueue; } set { readqueue = value; NotifyPropertyChanged("StrReadQueue"); } }
        /// <summary>
        /// cim连接的ip地址
        /// </summary>
        string ip;
        public string StrIP { get { return ip; } set { ip = value; NotifyPropertyChanged("StrIP"); } }
        /// <summary>
        /// cim连接的端口号
        /// </summary>
        string port;
        public string StrPort { get { return port; } set { port = value; NotifyPropertyChanged("StrPort"); } }
        /// <summary>
        /// 循环读取次数
        /// </summary>
        int cyctimes;
        public int iCycTimes { get { return cyctimes; } set { cyctimes = value; NotifyPropertyChanged("iCycTimes"); } }
        /// <summary>
        /// 二维码串口号
        /// </summary>
        string com;
        public string StrCom { get { return com; } set { com = value; NotifyPropertyChanged("StrCom"); } }
        /// <summary>
        /// 二维码波特率
        /// </summary>
        int baudrate;
        public int iBaudrate { get { return baudrate; } set { baudrate = value; NotifyPropertyChanged("iBaudrate"); } }
        /// <summary>
        /// 二维码读取延迟
        /// </summary>
        int codedelay;
        public int iCodeDelay { get { return codedelay; } set { codedelay = value; NotifyPropertyChanged("iCodeDelay"); } }
        /// <summary>
        /// 二维码类型
        /// </summary>
        TypeCode_enum typecode;
        public TypeCode_enum ETypeCode { get { return typecode; } set { typecode = value; NotifyPropertyChanged("ETypeCode"); } }
        /// <summary>
        /// CIM连接模式
        /// </summary>
        TypeMode typemode;
        public TypeMode ETypeMode { get { return typemode; } set { typemode = value; NotifyPropertyChanged("ETypeMode"); } }
        /// <summary>
        /// 是否记录日志
        /// </summary>
        bool blLog = true;
        public bool BlLog { get { return blLog; } set { blLog = value; NotifyPropertyChanged("BlLog"); } }
        #endregion

        #region mode
        /// <summary>
        /// cim是否打开
        /// </summary>
        bool blCimOn;
        public bool BlCimOn { get { return blCimOn; } set { blCimOn = value; NotifyPropertyChanged("BlCimOn"); NotifyPropertyChanged("BlVerifyChipIDEnabled"); NotifyPropertyChanged("BlPassVerifyCodeEnabled"); } }        
        /// <summary>
        /// 是否读码
        /// </summary>
        bool blCodeOn;
        public bool BlCodeOn { get { return blCodeOn; } set { blCodeOn = value; NotifyPropertyChanged("BlCodeOn"); NotifyPropertyChanged("BlVerifyChipIDEnabled"); NotifyPropertyChanged("BlPassVerifyCodeEnabled"); } }
        /// <summary>
        /// 是否chipid认证
        /// </summary>
        bool blVerifyChipID;
        public bool BlVerifyChipID { get { return blVerifyChipID; } set { blVerifyChipID = value; NotifyPropertyChanged("BlVerifyChipID"); } }
        /// <summary>
        /// 是否chipid认证控件使能标志位
        /// </summary>
        bool blVerifyChipIDEnabled;
        public bool BlVerifyChipIDEnabled { get { return BlCimOn & BlCodeOn; } set { blVerifyChipIDEnabled = value; NotifyPropertyChanged("BlVerifyChipIDEnabled"); } }
        /// <summary>
        /// 是否读码ng不抛料
        /// </summary>
        bool blPassVerifyCode;
        public bool BlPassVerifyCode { get { return blPassVerifyCode; } set { blPassVerifyCode = value; NotifyPropertyChanged("BlPassVerifyCode"); } }
        /// <summary>
        /// 是否Pass读码失败认证控件使能标志位
        /// </summary>
        bool blPassVerifyCodeEnabled;
        public bool BlPassVerifyCodeEnabled { get { return !BlCimOn & BlCodeOn; } set { blPassVerifyCodeEnabled = value; NotifyPropertyChanged("BlPassVerifyCodeEnabled"); } }
        #endregion


    }
}
