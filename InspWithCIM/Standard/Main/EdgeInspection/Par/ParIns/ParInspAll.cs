using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using BasicClass;
using System.IO;

namespace Main
{
    public class ParInspAll
    {
        /// <summary>
        /// 每个相机检测的边数
        /// </summary>
        public int NumInsp
        {
            get
            {
                return 2;
            }
        }

        public string NameCam { get; set; }
        public ParInspection this[int i]
        {
            get
            {
                if (i < BaseParInspection_L.Count)
                {
                    return BaseParInspection_L[i];

                }
                return BaseParInspection_L[0];
            }
        }

        public List<ParInspection> BaseParInspection_L = new List<ParInspection>();

        public ParInspection GetParBySideIndex(int i)
        {
            if (BaseParInspection_L[0].IntArrSidesMatch.Contains(i))
            {
                return BaseParInspection_L[0];
            }
            else
            {
                return BaseParInspection_L[1];
            }
        }

        public void ReadIni()
        {
            BaseParInspection_L.Clear();
            for (int i = 0; i < NumInsp; i++)
            {
                ParInspection par = new ParInspection(NameCam);
                par.ReadIni(i);
                BaseParInspection_L.Add(par);
            }
        }

        public bool WriteIni()
        {
            try
            {
                for (int i = 0; i < BaseParInspection_L.Count; i++)
                {
                    BaseParInspection_L[i].WriteIni(i);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("ParInspAll", ex);
                return false;
            }
        }
    }
}
