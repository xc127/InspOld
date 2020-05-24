using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DealPLC;
using DealConfigFile;
using BasicClass;

namespace Main
{
    partial class MainWindow
    {
        /// <summary>
        /// 换型的时候写入PLC的值
        /// </summary>
        void WritePLCModelPar()
        {
            try
            {
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.Plat1ComX, ModelParams.Plat1ComX);
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.Plat1ComY, ModelParams.Plat1ComY);
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.Plat2ComX, ModelParams.Plat2ComX);
                LogicPLC.L_I.WriteRegData1((int)DataRegister1.Plat2ComY, ModelParams.Plat2ComY);
            }
            catch (Exception ex)
            {
                Log.L_I.WriteError("MainWindow", ex);
            }
        }
    }
}
