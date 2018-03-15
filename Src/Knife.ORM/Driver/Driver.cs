/***************************************************************************
 * 文件名:             Driver.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              通用数据库访问接口实现和添加带参数的构造函数
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Knife.ORM
{
    public abstract class Driver : IDriver
    {
        internal Driver(DbProviderFactory providerFactory,string connectionString)
        {
            this.mProviderFactory = providerFactory;
            this.mConnectionString = connectionString;
        }

        /// <summary>
        /// 测试连接数据库
        /// </summary>
        public void TestConnection()
        {
            OpenConn();
        }

        /// <summary>
        /// 打开一个数据库连接
        /// </summary>
        /// <returns></returns>
        IDbConnection OpenConn()
        {
            IDbConnection conn = mProviderFactory.CreateConnection();
            conn.ConnectionString = this.mConnectionString;
            
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                return conn;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("打开数据库连接异常，异常消息：{0}", e.Message));
            }

        }

        /// <summary>
        /// 准备SqlCommand对象
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <returns></returns>

        IDbCommand PrepareCmd(IDbConnection conn, CommandType cmdType, string cmdText)
        {
            IDbCommand cmd = mProviderFactory.CreateCommand();
            cmd.CommandType = cmdType;
            cmd.CommandText = cmdText;
            cmd.Connection = conn;
            return cmd;
        }

        /// <summary>
        /// 执行sql 获取DataTable 对象
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, List<IDataParameter> sqlparms = null)
        {
            using (IDbConnection conn = OpenConn())
            {
                IDbCommand cmd = PrepareCmd(conn, CommandType.Text, sql);
                if (sqlparms != null)
                {
                    foreach (IDbDataParameter p in sqlparms)
                    {
                        try
                        {
                            cmd.Parameters.Add(p);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                }

                IDbDataAdapter da = mProviderFactory.CreateDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                int count = da.Fill(ds);
                return ds.Tables[0];
            }
        }

        /// <summary>
        /// 执行非查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlparms"></param>
        /// <returns></returns>
        public int ExecNoQuery(string sql, List<IDataParameter> sqlparms = null)
        {
            using (IDbConnection conn = OpenConn())
            {
                IDbCommand cmd = PrepareCmd(conn, CommandType.Text, sql);
                if (sqlparms != null)
                {
                    foreach (IDataParameter val in sqlparms)
                    {
                        cmd.Parameters.Add(val);
                    }
                }
                return cmd.ExecuteNonQuery();
            }

        }

        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlparms"></param>
        /// <returns></returns>
        public object ExecQuery(string sql, List<IDataParameter> sqlparms = null)
        {
            using (IDbConnection conn = OpenConn())
            {
                IDbCommand cmd = PrepareCmd(conn, CommandType.Text, sql);
                if (sqlparms != null)
                {
                    foreach (IDataParameter val in sqlparms)
                    {
                        cmd.Parameters.Add(val);
                    }
                }

                return cmd.ExecuteScalar();
            }

        }

        public IDataParameter CreateDataParameter(string name, object val)
        {
            IDataParameter p = mProviderFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = val;
            return p;
        }


        DbProviderFactory mProviderFactory;
        string mConnectionString;
    }
}
