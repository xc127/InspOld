using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealCIM
{
    public class CodeFactory
    {
        private static CodeFactory _instance = null;
        public static CodeFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CodeFactory();
                return _instance;
            }
        }

        public QRCodeBase GetCodeType(TypeCode_enum typeBot)
        {
            //if (!PostParams.P_I.BlCimOn)
            //    return null;
            //TODO
            switch (typeBot)
            {
                case TypeCode_enum.SVH:
                    return SVHCode.GetInstance();
                case TypeCode_enum.VY:
                    return VYCode.GetInstance();
                case TypeCode_enum.Mars:
                    return MarsCode.GetInstance();
                case TypeCode_enum.TW_SVH:
                    return TW_SVH.GetInstance();
                default:
                    return SVHCode.GetInstance();
            }
        }
    }
}
