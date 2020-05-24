using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    public class TableFactory
    {
        public static AccessTable GetTableCreater(TableEnum tableEnum)
        {
            switch (tableEnum)
            {
                case TableEnum.InspTable:
                    return new InspTable();
                default:
                    return new InspTable();
            }
        }
    }
}
