using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace Knife.ORM
{
    internal class SqlServerDriver : Driver
    {
        internal SqlServerDriver() : base(
            ProviderFactorys.SQLServer,
            KnifeConfig.SQLServerConnectionString) { }
    }
}
