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

        public DataTable ExecDQLForDataTable(string sql, List<IDataParameter> sqlparms = null)
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
                try
                {
                    da.Fill(ds);
                }
                catch (Exception e)
                {
                    throw new Exception("执行Driver中的函数ExceDQLForDataTable时出错,异常信息:" + e.Message);
                }
                
                return ds.Tables[0];
            }
        }

        public int ExecDML(string sql, List<IDataParameter> sqlparms = null)
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
                try
                {
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception("执行Driver中的函数ExceDML时出错,异常信息:" + e.Message);
                }
            }

        }

        public object ExecDQL(string sql, List<IDataParameter> sqlparms = null)
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
                try
                {
                    return cmd.ExecuteScalar();
                }
                catch(Exception e)
                {
                    throw new Exception("执行Driver中的函数ExceDQL时出错,异常信息:" + e.Message);
                }
            }

        }

        public IDataParameter CreateDataParameter(string name, object val)
        {
            IDataParameter p = mProviderFactory.CreateParameter();
            p.ParameterName = name;
            p.Value = val;
            return p;
        }


        #region 派生类私有成员
        DbProviderFactory mProviderFactory;
        string mConnectionString;
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
                throw new Exception(string.Format("打开数据库连接异常,异常消息：{0}", e.Message));
            }
        }

        /// <summary>
        /// 生成一个命令对象
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
        #endregion 派生类私有成员


    }
}
