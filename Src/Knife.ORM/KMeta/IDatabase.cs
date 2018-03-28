using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM.KMeta
{
    public interface IDatabase
    {
        int Connect(string host,string user, string pwd);
        List<string> GetDatabases();
        List<string> GetTablesOfDB(string dbName);

        Table GetTable(string dbName, string tableName);

        void CreateEntity(Table t, string nameSpace, string dir);
    }
}
