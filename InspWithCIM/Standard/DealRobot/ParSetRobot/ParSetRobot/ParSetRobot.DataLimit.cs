using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DealRobot
{   
    //机器人数据设定
    public class DataLimitRobot
    {
        public int No { get; set; }
        public int IntCameraNo { get; set; }//相机序号
        public int IntPosNo { get; set; } //拍照位置
        public double DblXMin { get; set; } //Min
        public double DblXMax { get; set; } //Max
        public double DblYMin { get; set; } //Min
        public double DblYMax { get; set; } //Max
        public double DblRMin { get; set; } //Min
        public double DblRMax { get; set; } //Max
        public double DblTMin { get; set; } //Min
        public double DblTMax { get; set; } //Max
        public string Annotation { get; set; } //注释
    }
}
