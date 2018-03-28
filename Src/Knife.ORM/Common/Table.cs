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
    public class Field
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public object Value { get; set; }
    }

    public class Table
    {
        public string Name { get; set; }
        public List<Field> Fields { get; set; }
    }
}
