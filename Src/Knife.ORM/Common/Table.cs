/***************************************************************************
 * 文件名:             Table.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              数据库表结构基础定义
***************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM
{
    internal class Field
    {
        internal string Name { get; set; }
        internal string DataType { get; set; }
        internal object Value { get; set; }
    }

    internal class Fields : List<Field>
    {

    }

    internal class Table
    {
        internal string Name { get; set; }
        internal List<Field> Fields { get; set; }
    }
}
