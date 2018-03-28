using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM
{
    public class MySQLContext : Context
    {
        public MySQLContext() : base(DbType.MySQL) {
            IDriver driver = new MySQLDriver();
        }
    }
}
