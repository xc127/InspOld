using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Main
{
    public class AccessHelper
    {
        #region define
        string strCon = null;
        OleDbConnection oleConnection = null;
        OleDbCommand oleCommand = null;
        OleDbDataReader oleReader = null;
        DataTable dt = null;
        AccessTable acTable = null;

        #endregion

        #region 初始化
        /// <summary>
        /// 构造，注入accdb文件名
        /// </summary>
        /// <param name="dbPath"></param>
        public AccessHelper(string dbPath, TableEnum tableEnum)
        {
            strCon = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + dbPath + @"'";
            InitDB();
            acTable = TableFactory.GetTableCreater(tableEnum);
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        void InitDB()
        {
            oleConnection = new OleDbConnection(strCon);
            oleCommand = new OleDbCommand();
        }

        /// <summary>
        /// 创建数据库文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="tableName"></param>
        public void CreateAccDB(string fileName, string tableName)
        {
            try
            {
                if (System.IO.File.Exists(fileName))
                    return;
                ADOX.Catalog cat = new ADOX.Catalog();
                cat.Create("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName);

                acTable.CreateColumns(cat, tableName);
                cat = null;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region 读取
        DataTable ConvertOleDBReaderToDataTable(ref OleDbDataReader reader)
        {
            int column = reader.FieldCount;
            DataTable dt_tmp = InitDataTable(column);

            if (dt_tmp == null)
                return null;
            while (reader.Read())
            {
                DataRow dr = dt_tmp.NewRow();
                for (int i = 0; i < column; ++i)
                {
                    dr[i] = reader[i];
                }
                dt_tmp.Rows.Add(dr);
            }

            return dt_tmp;
        }

        DataTable InitDataTable(int filed_cnt)
        {
            if (filed_cnt <= 0)
                return null;
            DataTable dt_tmp = new DataTable();
            for (int i = 0; i < filed_cnt; ++i)
            {
                DataColumn dc = new DataColumn(i.ToString());
                dt_tmp.Columns.Add(dc);
            }

            return dt_tmp;
        }

        public DataTable GetDataTableFromDB(string strSql)
        {
            if (strCon == null)
                return null;

            oleConnection.Open();
            if (oleConnection.State == ConnectionState.Closed)
                return null;

            oleCommand.CommandText = strSql;
            oleCommand.Connection = oleConnection;

            oleReader = oleCommand.ExecuteReader(CommandBehavior.Default);
            dt = ConvertOleDBReaderToDataTable(ref oleReader);
            oleReader.Close();

            if (oleConnection.State != ConnectionState.Closed)
                oleConnection.Close();

            return dt;
        }
        #endregion

        #region 增删查改
        //插入数据
        public int Insert(List<string> datas, string tableName)
        {
            return ExcuteSql(acTable.GenerateInsertSqlString(datas, tableName));
        }

        public int Delete(string index, string tableName)
        {
            string sql = "delete from " + tableName + @" where 索引=" + index;
            return ExcuteSql(sql);
        }
        #endregion

        /// <summary>
        /// sql执行
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        int ExcuteSql(string strSql)
        {
            int nResult = 0;

            try
            {
                oleConnection.Open();
                if (oleConnection.State == ConnectionState.Closed)
                    return nResult;

                oleCommand.Connection = oleConnection;
                oleCommand.CommandText = strSql;

                nResult = oleCommand.ExecuteNonQuery();

                if (oleConnection.State != ConnectionState.Closed)
                    oleConnection.Close();

                return nResult;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }

}
