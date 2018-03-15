using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM.KSql
{
    public class MSSQLSql:Sql
    {
        public MSSQLSql() : base(new SqlServerDriver()) { }
    }
}
