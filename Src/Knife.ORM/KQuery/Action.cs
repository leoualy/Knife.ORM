/***************************************************************************
 * 文件名:             Action.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              提供给外部查询调用的通用接口实现
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knife.ORM.KSql;
using System.Data;

namespace Knife.ORM
{
    public abstract class Action
    {
        public Action(Sql sql)
        {
            this.pmSql = sql;
        }

        protected Sql pmSql;

        public virtual List<TEntity> Select<TEntity>()
        {
            List<TEntity> list = pmSql.SelectCompleted<TEntity>();
            // 查询完成后清空参数和重置sql语句，防止重用查询实例时出错
            pmSql.ClearParams();
            pmSql.ResetSelectText();
            return list;
        }

        public int Update<TEntity>(TEntity entity)
        {
            return pmSql.UpdateCompleted<TEntity>(entity);
        }

        public int Insert<TEntity>(List<TEntity> ts)
        {
            return pmSql.InsertCompleted<TEntity>(ts);
        }

    }
}
