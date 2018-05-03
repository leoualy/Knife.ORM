/***************************************************************************
 * 文件名:             SqlDriver.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              IDirver 接口的进一步封装，以适应不同的数据库类型
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Knife.ORM.KSql
{
    public abstract class SqlDriver
    {
        List<IDataParameter> pmParams;

        public SqlDriver(IDriver driver)
        {
            this.mDriver = driver;
        }
        private IDriver mDriver;

        protected List<TEntity> Query<TEntity>(string sql)
        {
            DataTable dt = mDriver.ExecDQLForDataTable(sql, pmParams);
            if (dt == null || dt.Rows.Count <= 0)
            {
                return null;
            }

            List<TEntity> list = new List<TEntity>();
            foreach (DataRow row in dt.Rows)
            {
                list.Add(Reflector.Row2Model<TEntity>(row));
            }

            return list;
        }

        protected int UnQuery(string sql)
        {
            return mDriver.ExecDML(sql, pmParams);
        }
        internal void AddParams(string name, object val)
        {
            if (pmParams == null)
            {
                pmParams = new List<IDataParameter>();
            }
            pmParams.Add(mDriver.CreateDataParameter(name, val));
        }

        internal void ClearParams()
        {
            // 2018-03-22 改：防止清空NULL对象
            if (pmParams != null)
            {
                pmParams.Clear();
            }
        }
    }
}
