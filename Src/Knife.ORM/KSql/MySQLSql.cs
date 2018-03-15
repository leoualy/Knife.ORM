using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM.KSql
{
    public class MySQLSql:Sql
    {
        public MySQLSql() : base(new MySQLDriver()) { }
    }
}
