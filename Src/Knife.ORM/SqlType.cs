using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM
{
    internal class SqlType
    {
        static string[] boolArray = { "bool", "bit" };
        static string[] stringArray = {"string", "char", "varchar", "text", "nchar", "nvarchar" };
        static string[] integerArray = { "int", "tinyint", "smallint" };
        static string[] decimalArray = { "decimal", "numeric", "smallmoney", "money" };
        static string[] floatArray = { "float", "real" };
        static string[] dateTimeArray = { "DateTime","date", "datetime", "smalldatetime" };

        internal static List<string[]> TypeSet = new List<string[]>();
        internal static string[] DonetType = { "int","float","decimal"};
        static SqlType()
        {
            TypeSet.Add(boolArray);
            TypeSet.Add(stringArray);
            TypeSet.Add(integerArray);
            TypeSet.Add(decimalArray);
            TypeSet.Add(floatArray);
            TypeSet.Add(dateTimeArray);
        }


    }
}
