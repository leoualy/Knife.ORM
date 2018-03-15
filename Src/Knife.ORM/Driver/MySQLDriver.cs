using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
//using MySql.Data;

namespace Knife.ORM
{
    internal class MySQLDriver : Driver
    {
        public MySQLDriver() : base(ProviderFactorys.MySQL,KnifeConfig.MySqlConnectionString) { }
    }
}
