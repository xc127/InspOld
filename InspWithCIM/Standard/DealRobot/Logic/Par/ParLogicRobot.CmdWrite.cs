using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace DealRobot
{
    partial class ParLogicRobot
    {      
        //机器人握手
        public const string c_CmdShake = "10000";//机器人握手

        #region 配置参数
        public const string c_CmdParCom = "50";//公共参数
        public const string c_CmdPar1 = "55";//工位2参数  
        public const string c_CmdPar2 = "60";//工位3参数  
        public const string c_CmdPar3 = "65";//工位4参数  
        public const string c_CmdPar4 = "70";//工位4参数  
        #endregion 配置参数        

        //Home
        public const string c_CmdHome = "1";       

        //NG
        public const string c_CmdNGHome = "99";

        #region 相机
        //相机1
        public const string c_CmdSend1Camera1_S = "11";
        public const string c_CmdSend2Camera1_S = "12";
        public const string c_CmdSend3Camera1_S = "13";
        public const string c_CmdSend4Camera1_S = "14";

        public const string c_CmdSend6Camera1_S = "16";
        public const string c_CmdSend9Camera1_S = "19";//NG

        //相机2
        public const string c_CmdSend1Camera2_S = "21";
        public const string c_CmdSend2Camera2_S = "22";
        public const string c_CmdSend3Camera2_S = "23";
        public const string c_CmdSend4Camera2_S = "24";

        public const string c_CmdSend6Camera2_S = "26";
        public const string c_CmdSend9Camera2_S = "29";//NG

        //相机3
        public const string c_CmdSend1Camera3_S = "31";
        public const string c_CmdSend2Camera3_S = "32";
        public const string c_CmdSend3Camera3_S = "33";
        public const string c_CmdSend4Camera3_S = "34";

        public const string c_CmdSend6Camera3_S = "36";
        public const string c_CmdSend9Camera3_S = "39";//NG

        //相机4
        public const string c_CmdSend1Camera4_S = "41";
        public const string c_CmdSend2Camera4_S = "42";
        public const string c_CmdSend3Camera4_S = "43";
        public const string c_CmdSend4Camera4_S = "44";

        public const string c_CmdSend6Camera4_S = "46";
        public const string c_CmdSend9Camera4_S = "49";//NG

        //相机5
        public const string c_CmdSend1Camera5_S = "51";
        public const string c_CmdSend2Camera5_S = "52";
        public const string c_CmdSend3Camera5_S = "53";
        public const string c_CmdSend4Camera5_S = "54";

        public const string c_CmdSend6Camera5_S = "56";
        public const string c_CmdSend9Camera5_S = "59";//NG

        //相机6
        public const string c_CmdSend1Camera6_S = "61";
        public const string c_CmdSend2Camera6_S = "62";
        public const string c_CmdSend3Camera6_S = "63";
        public const string c_CmdSend4Camera6_S = "64";

        public const string c_CmdSend6Camera6_S = "66";
        public const string c_CmdSend9Camera6_S = "69";//NG        
        #endregion 相机

    }
}
