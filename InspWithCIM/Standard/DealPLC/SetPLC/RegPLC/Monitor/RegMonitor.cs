using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;
using DealConfigFile;

namespace DealPLC
{
    //触发寄存器
    public class RegMonitor : BaseRegPLC
    {
        #region 静态类实例
        public static RegMonitor R_I = new RegMonitor();
        #endregion 静态类实例

        #region 定义
        //固定的触发寄存器
        public int NumFixTrigger
        {
            get
            {
                return 14;
            }
        }

        //触发寄存器
        public int NumTrigger
        {
            get
            {
                if (NumRegSet > 20)//最多20个触发保留寄存器
                {
                    return 14 + 20;
                }
                return 14 + NumRegSet;
            }
        }

        /// <summary>
        /// 寄存器总个数
        /// </summary>
        public override int NumReg
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)
                {
                    return NumFixTrigger + NumRegSet;
                }
                else
                {
                    return 34 + NumRegSet;
                }
            }
            set
            {

            }
        }

        public int NumRegSingle//单个寄存器个数
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)
                {
                    if (NumRegSet > 20)
                    {
                        return NumTrigger + (NumRegSet - 20) * 2;
                    }
                    return NumTrigger;
                }
                else
                {
                    return 34 + NumRegSet * 2;//已定义功能的14个寄存器和保留寄存器
                }
            }
            set
            {

            }
        }
        //设置触发保留+读值保留寄存器
        int numRegSet = 0;
        public override int NumRegSet
        {
            get
            {
                return numRegSet;
            }
            set
            {
                numRegSet = value;
            }
        }

        /// <summary>
        /// PLCPC传输方向
        /// </summary>
        public override string[] PLCSendPC
        {
            get
            {
                string[] str = new string[NumReg];
                for (int i = 0; i < NumReg; i++)
                {
                    str[i] = "Y";
                }
                return str;
            }
        }
        /// <summary>
        /// PCPLC传输方向
        /// </summary>
        public override string[] PCSendPLC
        {
            get
            {
                string[] str = new string[NumReg];

                for (int i = 0; i < (NumReg); i++)
                {
                    str[i] = "Y";
                }
                return str;
            }
        }

        /// <summary>
        /// 注释
        /// </summary>
        public override string[] Annotation
        {
            get
            {
                string[] str = new string[NumReg];
                for (int i = 0; i < Annotation1.Length; i++)
                {
                    str[i] = Annotation1[i];
                }

                for (int i = Annotation1.Length; i < NumReg; i++)
                {
                    str[i] = Annotation2[i - Annotation1.Length];
                }
                return str;
            }
        }
        public string[] Annotation1
        {
            get
            {
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//精简版本协议
                {
                    return new string[]{ 
                    "连接心跳","设备状态","设备报警信息","配置文件设置",
                    "重启机器人或通用接口通信","设备其他信息 ",
                    "相机1拍照","相机2拍照","相机3拍照","相机4拍照","相机5拍照","相机6拍照","相机7拍照","相机8拍照",};
                }
                else
                {
                    return new string[]{ "软件重启" ,"电脑关机","电脑重启","连接心跳","设备运行状态",
                    "设备报警信息","设备物料信息","机器人状态","新建配置文件","切换配置文件",
                    "删除配置文件","重启机器人或通用接口通信 ","语音信息","",};
                }
            }
        }

        public string[] Annotation2
        {
            get
            {
                int num = NumRegSet;
                string[] str = null;

                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)//精简版本协议
                {
                    str = new string[NumRegSet];

                    if (NumRegSet > 20)
                    {
                        num = 20;
                    }
                    for (int i = 0; i < num; i++)
                    {
                        str[i] = "触发保留" + (i + 1);
                    }
                    if (NumRegSet > 20)
                    {
                        str[20] = "当前轴坐标1";
                    }
                    if (NumRegSet > 21)
                    {
                        str[21] = "当前轴坐标2";
                    }
                    if (NumRegSet > 22)
                    {
                        str[22] = "当前轴坐标3";
                    }
                    if (NumRegSet > 23)
                    {
                        str[23] = "当前轴坐标4";
                    }

                    for (int i = 24; i < NumRegSet; i++)
                    {
                        str[i] = "读值保留" + (i - 24 + 1).ToString();
                    }
                }
                else
                {
                    str = new string[NumRegSet + 20];
                    for (int i = 0; i < 20; i++)
                    {
                        str[i] = "触发保留" + (i + 1);
                    }
                    if (NumRegSet > 0)
                    {
                        str[20] = "当前轴坐标1";
                    }
                    if (NumRegSet > 1)
                    {
                        str[21] = "当前轴坐标2";
                    }
                    if (NumRegSet > 2)
                    {
                        str[22] = "当前轴坐标3";
                    }
                    if (NumRegSet > 3)
                    {
                        str[23] = "当前轴坐标4";
                    }

                    for (int i = 24; i < NumRegSet + 20; i++)
                    {
                        str[i] = "读值保留" + (i - 24 + 1).ToString();
                    }
                }
                return str;
            }
        }
        /// <summary>
        /// 数据寄存器的系数
        /// </summary>
        public override double[] Co
        {
            get
            {
                double[] co = null;
                if (ParSetPLC.P_I.TypePLCProtocol_e == TypePLCProtocol_enum.FullNew)
                {
                    co = new double[NumReg];
                    for (int i = 0; i < NumTrigger; i++)
                    {
                        co[i] = 1;
                    }

                    if (NumRegSet > 20)
                    {
                        co[34] = 0.0001;
                    }
                    if (NumRegSet > 21)
                    {
                        co[35] = 0.0001;
                    }
                    if (NumRegSet > 22)
                    {
                        co[36] = 0.0001;
                    }
                    if (NumRegSet > 23)
                    {
                        co[37] = 0.0001;
                    }
                    for (int i = 38; i < NumReg; i++)
                    {
                        co[i] = 0.0001;
                    }
                }
                else
                {
                    co = new double[NumReg];
                    for (int i = 0; i < NumReg; i++)
                    {
                        co[i] = 1;
                    }

                    if (NumRegSet > 0)
                    {
                        co[34] = 0.0001;
                    }
                    if (NumRegSet > 1)
                    {
                        co[35] = 0.0001;
                    }
                    if (NumRegSet > 2)
                    {
                        co[36] = 0.0001;
                    }
                    if (NumRegSet > 3)
                    {
                        co[37] = 0.0001;
                    }

                    for (int i = 38; i < NumReg; i++)
                    {
                        co[i] = 0.0001;
                    }
                }

                return co;
            }
        }

        /// <summary>
        /// 索引器,索引保留值寄存器
        /// </summary>
        public override RegPLC this[int index]
        {
            get
            {
                try
                {
                    return Reg_L[index];
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        #endregion 定义

        #region 初始化
        public RegMonitor()
        {            
            base.NameClass = "RegMonitor";

            //ini存储地址
            base.PathRegIni = ParPathRoot.PathRoot + "Store\\PLC\\RegMonitor.ini";
        }
        #endregion 初始化
    }

}
