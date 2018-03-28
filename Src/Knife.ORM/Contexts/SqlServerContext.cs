using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM
{
    public class SqlServerContext : Context
    {
        public SqlServerContext()
            : base(DbType.SqlServer)
        {

        }
    }
}
