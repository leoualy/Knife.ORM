using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM.KQuery
{
    public class KQueryMySql:Query
    {
        public KQueryMySql() : base(new KSql.MySQLSql()) { }
    }
}
