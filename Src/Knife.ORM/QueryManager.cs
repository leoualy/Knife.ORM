using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knife.ORM.Pkgs;
using System.Data;
using System.Data.SqlClient;

namespace Knife.ORM
{
    internal class QueryManager
    {
        static IDriver getDriver(DbType dbType)
        {
            IDriver driver = null;
            switch (dbType)
            {
                case DbType.SqlServer: driver = SqlServerDriver.Instance; break;
                case DbType.MySQL: break;
                case DbType.Oracle: break;
                case DbType.None: break;
            }
            return driver;
        }
        internal static  int DriverInsert(Pkg pkg,DbType dbType)
        {
            IDriver driver = getDriver(dbType);
            //driver.GenerateInsertString(ref pkg);
            return driver.ExecNoQuery(pkg.SqlString,pkg.Parameters);
        }

        internal static List<TEntity> DriverWhere<TEntity>(Pkg pkg, DbType dbType)
        {
            IDriver driver = getDriver(dbType);
            // driver.GenerateSelectString(ref pkg);
            List<TEntity> list = new List<TEntity>();

            DataTable dt = SqlServerDriver.Instance.GetDataTable(pkg.SqlString,pkg.Parameters);
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(Reflector.Row2Model<TEntity>(dr));
            }
            return list;
        }

        internal static int DriverUpdate(Pkg pkg, DbType dbType)
        {
            IDriver driver = getDriver(dbType);
            //driver.GenerateUpdateString(ref pkg);
            return driver.ExecNoQuery(pkg.SqlString,pkg.Parameters);
        }

        internal static int DriverDelete(Pkg pkg, DbType dbType)
        {
            return 0;
        }
    }
}
