/***************************************************************************
 * 文件名:             EntityContext.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              实体上下文
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Knife.ORM.Entity;

namespace Knife.ORM
{
    public class EntityContext
    {
        public static List<string> GetTablesOfDatabase()
        {
            return EntityCreator.GetTableNames();
        }
        public static void CreateEntities(List<string> tableNames,string nameSpace,string dir)
        {
            foreach (var val in tableNames)
            {
                Table table = EntityCreator.GetTable(val);
                EntityCreator.CreateEntity(table, nameSpace, dir);
            }
        }
        public static void CreateEntity(string tableName, string nameSpace, string dir)
        {
            EntityCreator.CreateEntity(EntityCreator.GetTable(tableName),nameSpace,dir);
        }
    }
}
