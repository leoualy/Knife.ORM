using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Knife.ORM
{
    public sealed class KQueryMSSQL:Query
    {
        public KQueryMSSQL():base(new KSql.MSSQLSql()){}
        
    }
}
