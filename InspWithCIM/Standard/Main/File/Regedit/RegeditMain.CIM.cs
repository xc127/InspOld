using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealFile;

namespace Main
{
    partial class RegeditMain
    {

        public int LotNum
        {
            get
            {
                try
                {
                    return Convert.ToInt32(ReadRegedit("LotNum"));
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                WriteRegedit("LotNum", value.ToString());
            }
        }

        public string CodeArm
        {
            get
            {
                try
                {
                    return ReadRegedit("CodeArm");
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                WriteRegedit("CodeArm", value);
            }
        }

        public string CodePlat
        {
            get
            {
                try
                {
                    return ReadRegedit("CodePlat");
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                WriteRegedit("CodePlat", value);
            }
        }

        public string CodeFork
        {
            get
            {
                try
                {
                    return ReadRegedit("CodeFork");
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                WriteRegedit("CodeFork", value);
            }
        }

        public string CodeArm2
        {
            get
            {
                try
                {
                    return ReadRegedit("CodeArm2");
                }
                catch
                {
                    return string.Empty;
                }
            }
            set
            {
                WriteRegedit("CodeArm2", value);
            }
        }

        public int CodeNGCnt
        {
            get
            {
                try
                {
                    return int.Parse(ReadRegedit("CodeNGCnt"));
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                WriteRegedit("CodeNGCnt", value.ToString());
            }
        }

        public int ChipIDNGCnt
        {
            get
            {
                try
                {
                    return int.Parse(ReadRegedit("ChipIDNGCnt"));
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                WriteRegedit("ChipIDNGCnt", value.ToString());
            }
        }
    }
}
