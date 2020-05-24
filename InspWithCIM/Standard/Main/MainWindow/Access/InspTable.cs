using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Main
{
    public class InspTable : AccessTable
    {
        public override string TableName => "InspTable";

        public override List<string> ColNames => new List<string>
        {
            @"索引",
            @"日期",
            @"产能总量",
            @"贝壳",
            @"破角",
            @"凸边"
        };

        public override bool CreateColumns(ADOX.Catalog cat, string tableName)
        {
            try
            {
                ADOX.Table tbl = new ADOX.Table
                {
                    ParentCatalog = cat,
                    Name = tableName
                };

                ADOX.Column col0 = new ADOX.Column
                {
                    ParentCatalog = cat,
                    Type = ADOX.DataTypeEnum.adInteger,
                    Name = "索引"
                };
                col0.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tbl.Columns.Append(col0, ADOX.DataTypeEnum.adInteger, 1);

                ADOX.Column col1 = new ADOX.Column
                {
                    ParentCatalog = cat,
                    Type = ADOX.DataTypeEnum.adDate,
                    Name = "日期"
                };
                col1.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tbl.Columns.Append(col1, ADOX.DataTypeEnum.adDate, 1);

                ADOX.Column col2 = new ADOX.Column
                {
                    ParentCatalog = cat,
                    Name = "产能总量",
                    Type = ADOX.DataTypeEnum.adInteger
                };
                col2.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tbl.Columns.Append(col2, ADOX.DataTypeEnum.adInteger, 1);

                ADOX.Column col3 = new ADOX.Column
                {
                    ParentCatalog = cat,
                    Name = "贝壳",
                    Type = ADOX.DataTypeEnum.adInteger
                };
                col3.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tbl.Columns.Append(col3, ADOX.DataTypeEnum.adInteger, 1);

                ADOX.Column col4 = new ADOX.Column
                {
                    ParentCatalog = cat,
                    Name = "破角",
                    Type = ADOX.DataTypeEnum.adInteger
                };
                col4.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tbl.Columns.Append(col4, ADOX.DataTypeEnum.adInteger, 1);

                ADOX.Column col5 = new ADOX.Column
                {
                    ParentCatalog = cat,
                    Name = "凸边",
                    Type = ADOX.DataTypeEnum.adInteger
                };
                col5.Properties["Jet OLEDB:Allow Zero Length"].Value = false;
                tbl.Columns.Append(col5, ADOX.DataTypeEnum.adInteger, 1);

                tbl.Keys.Append("PrimaryKey", ADOX.KeyTypeEnum.adKeyPrimary, "索引", "", "");
                cat.Tables.Append(tbl); ;

                tbl = null;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GenerateInsertSqlString(string index, string date, string sum, string shell, string corner,
            string convex, string tableName, List<string> colNames)
        {
            List<string> datas = new List<string>
            {
                index,
                date,
                sum,
                shell,
                corner,
                convex
            };
            return GenerateInsertSqlString(datas, tableName);
        }

        public override string GenerateInsertSqlString(List<string> datas, string tableName)
        {
            string sql = string.Empty;
            if (datas.Count != ColNames.Count)
                return "-1000";
            try
            {
                string strCols = "(" + string.Join(",", ColNames.ToArray()) + ")";
                string strDatas = "(" + string.Join(",", datas.ToArray()) + ")";
                sql = "insert into " + tableName + ' ' + strCols + " values " + strDatas;
            }
            catch (Exception ex)
            {

            }
            return sql;
        }
    }
}
