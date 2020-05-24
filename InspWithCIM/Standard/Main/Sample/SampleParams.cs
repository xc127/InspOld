using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Main
{
    public partial class SampleParams
    {


    }

    class VModel
    {
        public int Index { get; set; }
        public string FileName { get; set; }
        public bool Shell { get; set; }
        public bool Convex { get; set; }
        public bool Cornor { get; set; }
    }

    public enum ParamsType
    {
        电极边 = 1,
        非电极边
    }
}
