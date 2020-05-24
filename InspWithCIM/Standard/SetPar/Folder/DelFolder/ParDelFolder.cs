using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using BasicClass;

namespace SetPar
{
    public partial class ParDelFolder
    {
        #region 静态类实例
        public static ParDelFolder P_I = new ParDelFolder();
        #endregion 静态类实例

        #region 定义
        //Path
        static string c_PathSetDelete = ComValue.c_PathSetPar + "SetDelFolder.ini";

        public bool BlDel1 = false;//是否启用删除时间1
        public bool BlDel2 = false;//是否启用删除时间2

        public string Time1 = "";//删除时间1
        public string Time2 = "";//删除时间2

        public int Hour1
        {
            get
            {
                return int.Parse(Time1.Split(':')[0]);
            }
        }
        public int Minute1
        {
            get
            {
                return int.Parse(Time1.Split(':')[1]);
            }
        }

        public int Hour2
        {
            get
            {
                return int.Parse(Time2.Split(':')[0]);
            }
        }
        public int Minute2
        {
            get
            {
                return int.Parse(Time2.Split(':')[1]);
            }
        }

        //List
        public List<BaseParDelFolder> BaseParDelFolder_L = new List<BaseParDelFolder>();

        //Class
        /// <summary>
        /// 异常信息日志
        /// </summary>
        BaseParDelFolder ErrorLog = new BaseParDelFolder();

        /// <summary>
        /// 机器人日志
        /// </summary>
        BaseParDelFolder RobotLog = new BaseParDelFolder();

        /// <summary>
        /// PLC日志
        /// </summary>
        BaseParDelFolder PLCLog = new BaseParDelFolder();

        /// <summary>
        ///Custom日志
        /// </summary>
        BaseParDelFolder CustomLog = new BaseParDelFolder();

        /// <summary>
        ///Result通信日志
        /// </summary>
        BaseParDelFolder ResultLog = new BaseParDelFolder();

        /// <summary>
        ///运行及报警日志
        /// </summary>
        BaseParDelFolder AlarmWorkLog = new BaseParDelFolder();

        /// <summary>
        //数据备份
        /// </summary>
        BaseParDelFolder BackUp = new BaseParDelFolder();

        /// <summary>
        //CIM
        /// </summary>
        BaseParDelFolder CIM = new BaseParDelFolder();

        /// <summary>
        //Tact
        /// </summary>
        BaseParDelFolder Tact_DealCell = new BaseParDelFolder();
        BaseParDelFolder Tact_Camera = new BaseParDelFolder();

        public BaseParDelFolder ImageInsp = new BaseParDelFolder();
        public BaseParDelFolder ImageInspNG = new BaseParDelFolder();

        #region 图片文件夹删除处理
        /// <summary>
        ///当前型号记录图片,按照天进行文件夹存储
        /// </summary>
        BaseParDelFolder imageRecord = new BaseParDelFolder();
        BaseParDelFolder ImageRecord
        {
            get
            {
                imageRecord.PathFolder = ParPathRoot.PathRootRecord + "软件运行记录\\图片记录\\" + ComConfigPar.C_I.NameModel + "\\";
                imageRecord.Annotation = "当前型号图片记录";
                return imageRecord;
            }
            set
            {
                imageRecord = value;
            }
        }

        /// <summary>
        ///当前不使用型号图片删除
        /// </summary>
        BaseParDelFolder oldImageRecord = new BaseParDelFolder();
        public BaseParDelFolder OldImageRecord
        {
            get
            {
                oldImageRecord.PathFolder = ParPathRoot.PathRootRecord + "软件运行记录\\图片记录\\";
                oldImageRecord.Annotation = "未使用型号图片记录(删除当前日期指定天数之前的图片)";
                return oldImageRecord;
            }
            set
            {
                oldImageRecord = value;
            }
        }
        #endregion 图片文件夹删除处理
        #endregion 定义

        #region 初始化
        ParDelFolder()
        {
            NameClass = "ParDelFolder";

            ErrorLog.PathFolder = ParPathRoot.PathRoot + "软件运行记录\\异常信息日志\\";
            ErrorLog.Annotation = "异常信息日志";

            RobotLog.PathFolder = ParPathRoot.PathRoot + "软件运行记录\\通用接口通信日志\\";
            RobotLog.Annotation = "通用接口通信日志";

            PLCLog.PathFolder = ParPathRoot.PathRoot + "软件运行记录\\PLC通信日志\\";
            PLCLog.Annotation = "PLC通信日志";

            CustomLog.PathFolder = ParPathRoot.PathRoot + "软件运行记录\\Custom\\";
            CustomLog.Annotation = "Custom日志";

            ResultLog.PathFolder = ParPathRoot.PathRoot + "软件运行记录\\Result通信日志\\";
            ResultLog.Annotation = "Result通信日志";

            AlarmWorkLog.PathFolder = ParPathRoot.PathRoot + "软件运行记录\\运行及报警日志\\";
            AlarmWorkLog.Annotation = "运行及报警日志";

            BackUp.PathFolder = ParPathRoot.PathRoot + "软件运行记录\\数据备份\\";
            BackUp.Annotation = "数据备份";

            CIM.PathFolder = ParPathRoot.PathRoot + "软件运行记录\\CIM\\";
            CIM.Annotation = "CIM";

            Tact_DealCell.PathFolder = ParPathRoot.PathRoot + "软件运行记录\\RecordCell\\";
            Tact_DealCell.Annotation = "算法单元节拍";

            Tact_Camera.PathFolder = ParPathRoot.PathRoot + "软件运行记录\\RecordCamera\\";
            Tact_Camera.Annotation = "整体节拍";

            ImageInsp.PathFolder = "E:\\Image\\AllImage\\";
            ImageInsp.Annotation = "巡边正常图片";

            ImageInspNG.PathFolder = "E:\\Image\\NGImage\\";
            ImageInspNG.Annotation = "巡边检NG图片";

        }
        /// <summary>
        /// 向集合中添加值
        /// </summary>
        public void AddParDelFolder()
        {
            try
            {
                //首先清空
                BaseParDelFolder_L.Clear();

                BaseParDelFolder_L.Add(ErrorLog);//异常信息日志
                BaseParDelFolder_L.Add(RobotLog);//机器人日志
                BaseParDelFolder_L.Add(PLCLog);//PLC日志
                BaseParDelFolder_L.Add(CustomLog);//CustomLog
                BaseParDelFolder_L.Add(ResultLog);//ResultLog
                BaseParDelFolder_L.Add(AlarmWorkLog);//AlarmWorkLog
                BaseParDelFolder_L.Add(BackUp);//数据备份
                BaseParDelFolder_L.Add(CIM);//CIM

                BaseParDelFolder_L.Add(Tact_DealCell);//Tact_DealCell
                BaseParDelFolder_L.Add(Tact_Camera);//Tact_Camera
                //图片删除
                BaseParDelFolder_L.Add(ImageRecord);
                BaseParDelFolder_L.Add(OldImageRecord);
                BaseParDelFolder_L.Add(ImageInsp);
                BaseParDelFolder_L.Add(ImageInspNG);



                for (int i = 0; i < BaseParDelFolder_L.Count; i++)
                {
                    BaseParDelFolder_L[i].No = i;
                }
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError(NameClass, ex);
            }
        }
        #endregion 初始化

    }
    public class BaseParDelFolder : BaseClass
    {
        public string PathFolder { set; get; }//文件路径

        public bool BlFolder { set; get; }//删除文件夹
        public bool BlFile { set; get; }//删除文件
        public bool BlExcute { set; get; }//是否执行

        public bool ConnetModel { set; get; }//是否关联型号

        int num = 0;//最小删除天数为1
        public virtual int Num
        {
            get
            {
                //if (num < 1)
                //{
                //    num = 1;
                //}
                return num;
            }
            set
            {
                num = value;
            }
        }
    }
}
