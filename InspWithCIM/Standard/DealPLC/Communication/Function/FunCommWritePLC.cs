using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicClass;
using System.Collections;
using BasicComprehensive;
using DealResult;

namespace DealPLC
{
    public class FunCommWritePLC : BaseComprehensive
    {
        #region 初始化
        public FunCommWritePLC()
        {
            NameClass = "FunCommWritePLC";
        }
        #endregion 初始化


        public BaseResultCommunication DealCommWritePLC(ParCommWritePLC par, Hashtable htResult)
        {
            BaseResultCommunication result = new BaseResultCommunication();
            try
            {
                for (int i = 0; i < par.ResultforWritePLC_L.Count; i++)
                {
                    string nameCell = par.ResultforWritePLC_L[i].NameCell;
                    string nameResult = par.ResultforWritePLC_L[i].NameResult;

                    GetReusltValueFromResultcs fun = new GetReusltValueFromResultcs();
                    double value = fun.GetResultValue(nameCell, nameResult, htResult);
                    par.ResultforWritePLC_L[i].Result = value;
                }
                
                //写入PLC
                bool blResult = LogicPLC.L_I.WriteData(par.ResultforWritePLC_L);
                if (!blResult)
                {
                    result.LevelError_e = LevelError_enum.Error;
                    result.Annotation = "";
                }
                return result;
            }
            catch (Exception ex)
            {
                result.LevelError_e = LevelError_enum.Error;
                result.Annotation = par.NameCell.ToString() + "写入PLC:" + ex.Message;
                LogError(NameClass, par, ex);
                return result;
            }
        }
    }
}
