using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM
{
    public enum DbType
    {
        None=0,
        SqlServer,
        MySQL,
        Oracle
    }

    public abstract class Context
    {
        public Context(DbType dbType)
        {
            this.mDbType = dbType;
        }

        DbType mDbType = DbType.None;
        public List<TModel> GetModel<TModel>(TModel model)
        {
            Pkg pkg = PkgFactory.GetSelectPkg<TModel>(model);
            
            List<TModel> list = QueryManager.DriverWhere<TModel>(pkg,mDbType);
            return list;
        }

        public int Insert<TModel>(TModel model)
        {
            Pkg pkg = PkgFactory.GetInsertPkg<TModel>(model);
            
            return QueryManager.DriverInsert(pkg,mDbType);
        }

        public int ModifyByConditionColumns<TModel>(TModel model, TModel condition)
        {
            Pkg pkg = PkgFactory.GetModifyPkg<TModel>(model, condition);
            return QueryManager.DriverUpdate(pkg,mDbType);
        }
    }
}
