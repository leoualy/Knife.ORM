using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Knife.ORM.Common;

namespace Knife.ORM.KMeta
{
    public abstract class DatabaseImpl:IDatabase
    {
        public DatabaseImpl(string sqlForDatabases)
        {
            this.mSqlForDatabases = sqlForDatabases;
        }



        public abstract int Connect(string host,string user, string pwd);
        public List<string> GetDatabases()
        {
            DataTable dt = mDriver.ExecDQLForDataTable(mSqlForDatabases);
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["DBName"].ToString());
            }
            return list;
        }
        public abstract List<string> GetTablesOfDB(string dbName);

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <returns></returns>
        protected int TestConnect()
        {
            try
            {
                mDriver.TestConnection();
                return 1;
            }
            catch
            {
                return -1;
            }
        }
        /// <summary>
        /// 从DataTable 中获取表名集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        protected List<string> pGetTableNames(DataTable dt)
        {
            List<string> list = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(row["TableName"].ToString());
            }
            return list;
        }
        /// <summary>
        /// 生成数据库连接字符串
        /// </summary>
        /// <param name="host">主机名</param>
        /// <param name="dbName">数据库名</param>
        /// <param name="user">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>数据库连接字符串</returns>
        protected string pGenerateConnectionString(string host, string dbName, string user, string pwd)
        {
            this.pHost = host;
            this.pDbName = dbName;
            this.pUser = user;
            this.pPwd = pwd;
            return string.Format("server ={0};database ={1};uid={2};pwd={3}",
                pHost, pDbName, pUser, pPwd);
        }

        protected IDriver mDriver;
        protected string pHost, pDbName, pUser, pPwd;
        string mSqlForDatabases, mSqlForTable;
        protected string pSchema;

        public abstract Table GetTable(string dbName, string tableName);

        public void CreateEntity(Table table, string nameSpace, string dir)
        {
            List<string> profiles = new List<string>();
            string dataType = string.Empty;
            foreach (Field val in table.Fields)
            {
                if (!SqlType.DonetType.Contains(val.DataType))
                {
                    foreach (string[] vals in SqlType.TypeSet)
                    {
                        if (vals.Contains(val.DataType))
                        {
                            dataType = vals[0];
                            break;
                        }
                    }
                }
                else
                {
                    dataType = val.DataType;
                }

                profiles.Add(ClassCreator.CreateProfile(val.Name, dataType));
            }


            ClassCreator.CreateClassFile(nameSpace, table.Name, string.Format(@"{0}\{1}.cs", dir, table.Name), profiles);
        }

    }
}
