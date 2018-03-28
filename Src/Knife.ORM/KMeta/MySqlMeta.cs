using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Knife.ORM.KMeta
{
    public class MySqlMeta:DatabaseImpl
    {
        public MySqlMeta() : base(
            "select SCHEMA_NAME as DBName from SCHEMATA") {
                pSchema = "information_schema";
        }


        public override int Connect(string host,string user, string pwd)
        {
            KnifeConfig.MySqlConnectionString = pGenerateConnectionString(host,this.pSchema, user, pwd);
            mDriver = new MySQLDriver();
            return TestConnect();
        }

        public override List<string> GetTablesOfDB(string dbName)
        {
            KnifeConfig.MySqlConnectionString = pGenerateConnectionString(this.pHost,this.pSchema,this.pUser,this.pPwd);
            mDriver = new MySQLDriver();
            DataTable dt = mDriver.ExecDQLForDataTable(
                string.Format("select TABLE_NAME as TableName from `TABLES` where TABLE_SCHEMA='{0}'", dbName));
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            this.pDbName = dbName;
            
            return pGetTableNames(dt);
        }

        public override Table GetTable(string dbName, string tableName)
        {
            KnifeConfig.MySqlConnectionString = pGenerateConnectionString(this.pHost,dbName, this.pUser, this.pPwd);
            string sql = string.Format("select column_name,data_type from information_schema.COLUMNS where table_name='{0}'", tableName);
            DataTable dt = (new MySQLDriver()).ExecDQLForDataTable(sql);
            Table t = new Table();
            t.Name = tableName;
            t.Fields = new List<Field>();
            foreach (DataRow dr in dt.Rows)
            {
                t.Fields.Add(new Field()
                {
                    Name = dr["column_name"].ToString(),
                    DataType = dr["data_type"].ToString()
                });
            }
            return t;
        }
    }
}
