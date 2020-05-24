using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace Main
{
    public class AccessTable
    {
        public virtual string TableName => "AccessTable";

        public virtual List<string> ColNames => new List<string>();

        public virtual bool CreateColumns(ADOX.Catalog cat, string tableName)
        {
            return true;
        }

        public virtual string GenerateInsertSqlString(List<string> datas, string tableName)
        {
            return string.Empty;
        }
    }

    public enum TableEnum
    {
        InspTable,
    }
}
