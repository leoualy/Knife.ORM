using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Knife.ORM.KMeta
{
    public class SQLServerMeta : DatabaseImpl
    {
        public SQLServerMeta():base(
            @"SELECT name AS DBName FROM master.sys.databases"){
                pSchema = "master";
        }

        public override int Connect(string host,string user, string pwd)
        {
            KnifeConfig.SQLServerConnectionString = pGenerateConnectionString(host,this.pSchema, user, pwd);
            mDriver = new SqlServerDriver();
            return TestConnect();
        }

        public override List<string> GetTablesOfDB(string dbName)
        {
            KnifeConfig.SQLServerConnectionString = pGenerateConnectionString(this.pHost, dbName,this.pUser,this.pPwd);
            mDriver = new SqlServerDriver();
            DataTable dt = mDriver.ExecDQLForDataTable("select Name as TableName from SysObjects where XType='U' ");
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            return pGetTableNames(dt);
        }

        public override Table GetTable(string dbName, string tableName)
        {
            string sql = string.Format(
 @"select syscolumns.name as name,systypes.name as dataType 
from syscolumns,systypes 
where syscolumns.id=OBJECT_ID('{0}') and syscolumns.xusertype=systypes.xusertype", tableName);
            KnifeConfig.SQLServerConnectionString = pGenerateConnectionString(this.pHost, dbName, this.pUser, this.pPwd);

            DataTable dt = (new SqlServerDriver()).ExecDQLForDataTable(sql);
            Table t = new Table();
            t.Name = tableName;
            t.Fields = new List<Field>();
            foreach (DataRow dr in dt.Rows)
            {
                t.Fields.Add(new Field()
                {
                    Name = dr["name"].ToString(),
                    DataType = dr["dataType"].ToString()
                });
            }
            return t;
        }
    }
}
