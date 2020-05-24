using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;

namespace Main
{
    public partial class MainWindow
    {
        void TrrigerPC()
        {
            try
            {
                bool blNew = false;
                WinTrrigerComprehensive win = WinTrrigerComprehensive.GetWinInst(out blNew);
                win.Init(uCStateWork);
                win.Show();
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }    
    }
}
