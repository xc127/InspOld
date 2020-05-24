using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCIM
{
    /// <summary>
    /// CIM相关的参数
    /// </summary>
    public enum CIM_PARAMS
    {
        SendQueue,
        ReadQueue,
        IP,
        Port,
        CycTimes,
        UserID,
        Fab,
        Area,
        Line,
        Operation,
        ModelNo,
        RunCard,
        COM,
        Baudrate,
        CodeDelay,
        CodeType,
        ModeType,
        BlLog
    }

    public enum CIMMODE_ENUM
    {
        CodeOn,
        CimOn,
        VerifyChipid,
        PassVerifyCode
    }

    /// <summary>
    /// 客户要求保存两套CIM连接参数，分别为BEOL和MOD
    /// </summary>
    public enum TypeMode 
    {
        BEOL,
        MOD
    }

    /// <summary>
    /// 过账类型，分chipid/lot/trackout
    /// </summary>
    public enum PostType
    {
        ChipID,
        Lot,
        TrackOut,
        Null
    }
}
