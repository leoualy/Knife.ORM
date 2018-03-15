/***************************************************************************
 * 文件名:             ProviderFactory.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              各个数据库引擎提供的访问接口工厂集合
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;


namespace Knife.ORM
{
    public class ProviderFactorys
    {
        internal static DbProviderFactory SQLServer = System.Data.SqlClient.SqlClientFactory.Instance;

        static DbProviderFactory mySql;
        /// <summary>
        /// MySql的驱动工厂
        /// </summary>
        internal static DbProviderFactory MySQL
        {
            get
            {
                if (mySql == null)
                {
                    string path = Common.PathUtil.GetFullFileName("Dlls\\MySql.Data.dll");
                    mySql =
                        (DbProviderFactory)Reflector.
                        GetInstanceFromDLL(path, "MySql.Data.MySqlClient.MySqlClientFactory");
                }
                return mySql;
            }
        }

    }
}
