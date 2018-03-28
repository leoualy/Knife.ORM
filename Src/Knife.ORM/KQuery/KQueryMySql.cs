using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM
{
    public sealed class KQueryMySql:Query
    {
        public KQueryMySql() : base(new KSql.MySQLSql()) { }
    }
}
