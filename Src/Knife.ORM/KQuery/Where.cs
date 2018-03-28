/***************************************************************************
 * 文件名:             Where.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              条件查询的接口实现
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM
{
    public abstract  class Where:Action
    {
        public Where(KSql.Sql sql)
            : base(sql) { }

        public virtual Where Columns(params string[] keys)
        {
            if (keys == null)
            {
                throw new Exception("Columns 参数为空");
            }
            StringBuilder columns = new StringBuilder();
            
            for (int i = 0; i < keys.Length; i++)
            {
                columns.Append(keys[i]);
                if (i != keys.Length - 1)
                {
                    columns.Append(",");
                }
            }
            pmSql.ColumnsTextAppend(columns.ToString());
            return this;
        }

        public virtual Where Equal(string key, object val)
        {
            string name = GenerateParameterName("Equal",key);
            pmSql.WhereTextAppend(string.Format("{0}={1}",key,name));
            pmSql.AddParams(name, val);
            return this;
        }
        public virtual Where NoEqual(string key, object val)
        {
            string name = GenerateParameterName("NoEqual",key);
            pmSql.WhereTextAppend(string.Format("{0}<>{1}",key,name));
            pmSql.AddParams(name, val);
            return this;
        }
        public virtual Where Greater(string key, object val)
        {
            string name = GenerateParameterName("Greater",key);
            pmSql.WhereTextAppend(string.Format("{0}>{1}", key,name));
            pmSql.AddParams(name, val);
            return this;
        }
        public virtual Where Less(string key, object val)
        {
            string name = GenerateParameterName("Less",key);
            pmSql.WhereTextAppend(string.Format("{0}<{1}", key, name));
            pmSql.AddParams(name, val);
            return this;
        }

        protected string GenerateParameterName(string key,string d)
        {
            return string.Format("@{0}{1}",d,key);
        }

    }
}
