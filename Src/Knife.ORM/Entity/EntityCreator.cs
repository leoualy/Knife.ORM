/***************************************************************************
 * 文件名:             EntityCreator.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              生成实体的实现
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knife.ORM.Common;
using System.Data;


namespace Knife.ORM.Entity
{
    internal class EntityCreator
    {
        internal static int CreateEntitys(string nameSpace, string dir)
        {
            List<string> tNames = GetTableNames();
            foreach (var s in tNames)
            {
                Table t = GetTable(s);
                EntityCreator.CreateEntity(t, nameSpace, dir);
            }
            return tNames.Count;
        }

        internal static void CreateEntity(Table table,string nameSpace, string dir)
        {
            List<string> profiles = new List<string>();
            string dataType=string.Empty;
            foreach (Field val in table.Fields)
            {
                if (!SqlType.DonetType.Contains(val.DataType))
                {
                    foreach (string[] vals in SqlType.TypeSet)
                    {
                        if (vals.Contains(val.DataType))
                        {
                            dataType = vals[0];
                            break;
                        }
                    }
                }
                else
                {
                    dataType = val.DataType;
                }
                
                profiles.Add(ClassCreator.CreateProfile(val.Name,dataType));
            }


            ClassCreator.CreateClassFile(nameSpace, table.Name, string.Format(@"{0}\{1}.cs",dir, table.Name), profiles);
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        internal static List<string> GetTableNames()
        {
            DataTable dt = (new SqlServerDriver()).GetDataTable("select Name from SysObjects where XType='U' ");
            if (dt == null || dt.Rows.Count < 0)
            {
                return null;
            }
            List<string> listName = new List<string>();
            foreach (DataRow row in dt.Rows)
            {
                listName.Add(row["Name"].ToString());
            }
            return listName;
        }

        internal static Table GetTable(string tableName)
        {
            string sql = string.Format(
@"select syscolumns.name as name,systypes.name as dataType 
from syscolumns,systypes 
where syscolumns.id=OBJECT_ID('{0}') and syscolumns.xusertype=systypes.xusertype", tableName);
            DataTable dt = (new SqlServerDriver()).GetDataTable(sql);
            Table t = new Table();
            t.Name = tableName;
            t.Fields = new List<Field>();
            foreach (DataRow dr in dt.Rows)
            {
                t.Fields.Add(new Field()
                {
                    Name = dr["name"].ToString(),
                    DataType = dr["dataType"].ToString()
                });
            }
            return t;
        }

    }
}
