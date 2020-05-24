using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DealPLC;

namespace DealPLC
{
    public partial class ParLogicPLC
    {
        #region 静态类实例
        public static ParLogicPLC P_I = new ParLogicPLC();
        #endregion 静态类实例

        #region 定义
        //bool 
        public bool BlPLCOpen = false;//PLC是否连接
        public bool BlRead = false;//是否循环读取寄存器
       
        //int
        public int intNo = 0;//配置参数序号

        public int[] intTrrigerValue = null;//模拟触发的数组

        //string 
        public string strOldCycRead = "";
        public string NameModel = "";//配置参数型号名

        //List

        #region List
        public List<ParProduct> ParProduct_L = new List<ParProduct>();//配置参数公共类
        #endregion List
        #endregion 定义

        #region 初始化
        public ParLogicPLC()
        {
            intTrrigerValue = new int[ParSetPLC.P_I.IntNumCycReg];            
        }
        #endregion 初始化
    }
    public class TrrigerPLC : RegPLC
    {
        public double DblValue { set; get; }
    }
    //PC报警错误
    public enum ErrorPC_enum
    {
        ErrorCamera1 = 10,
        ErrorCamera2 = 20,
        ErrorCamera3 = 30,
        ErrorCamera4 = 40,
        ErrorCamera41 = 41,
        ErrorCamera5 = 50,
        ErrorCamera6 = 60,

        Error0 = 1000,
        Error1 = 1001,
        Error2 = 1002,
        Error3 = 1003,
        Error4 = 1004,
        Error5 = 1005,
        Error6 = 1006,

        CIM = 100,
        RobotDataError = 200,//机器人数据发送错误
        Buzzer = 999,//通知PLC蜂鸣
    }

}
