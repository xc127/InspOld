using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using DealFile;
using BasicClass;
using DealConfigFile;

namespace DealPLC
{
    public class UCSetRegConfigPar : BaseUCReg
    {
        #region 初始化
        public override void Init()
        {
            try
            {
                base.dudNumReg.ToolTip = "Min:20;Max:100";
                base.dudNumReg.Maximum = 100;
                base.Init(RegConfigPar.R_I, "配置参数寄存器"); 
            }
            catch (Exception ex)
            {
                
            }                     
        }
        #endregion 初始化

        #region 导入Excel
        /// <summary>
        /// 导入excel
        /// </summary>
        /// <param name="path"></param>
        public override void SetRegFromExcel(object path)
        {
            try
            {
                BaseRegPLC baseRegPLC = ReadExcelReg.R_I.ReadExcel(path.ToString());
                g_RegPLCBase.regStart = baseRegPLC.regStart;
                g_RegPLCBase.NumRegSet = baseRegPLC.NumRegSet - 10 - ParCameraWork.P_I[ParCameraWork.NumCamera] * 4;

                g_RegPLCBase.Reg_L.Clear();
                g_RegPLCBase.Reg_L = baseRegPLC.Reg_L;

                ShowPar_Invoke();
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion 导入Excel

        #region 显示      
      
        #endregion 显示
    }
}
