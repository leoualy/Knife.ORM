using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM
{
    public class Query:Where
    {
        public Query(KSql.Sql sql) : base(sql) {
        }
    }
}
