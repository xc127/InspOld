using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Common;
using DealFile;
using System.Threading;
using System.Threading.Tasks;
using BasicClass;
using System.Diagnostics;

namespace DealConfigFile
{
    partial class WinConfigPar
    {       
        #region 保存
        /// <summary>
        /// 保存参数
        /// </summary>
        bool SavePar()
        {
            try
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                for (int i = 0; i < ParConfigPar.P_I.ParProduct_L.Count; i++)
                {
                    ParProduct inst = dgConfigPar.Items[i] as ParProduct;
                    ParConfigPar.P_I.ParProduct_L[i].DblValue = inst.DblValue;
                    ParConfigPar.P_I.ParProduct_L[i].DblMin = inst.DblMin;
                    ParConfigPar.P_I.ParProduct_L[i].DblMax = inst.DblMax;
                    ParConfigPar.P_I.ParProduct_L[i].Annotation = inst.Annotation;
                }     
                sw.Stop();
                return true;
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("WinConfigPar", ex);
                return false;
            }
        }
        #endregion 保存
    }
}
