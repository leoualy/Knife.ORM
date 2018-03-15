/***************************************************************************
 * 文件名:             IDriver.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              数据库访问通用接口
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Knife.ORM
{
    public interface IDriver
    {
        void TestConnection();
        DataTable GetDataTable(string sql, List<IDataParameter> sqlparms = null);
        int ExecNoQuery(string sql, List<IDataParameter> sqlparms = null);
        object ExecQuery(string sql, List<IDataParameter> sqlparms = null);
        // 2018-03-12添加的新接口函数，用于创建通用参数
        IDataParameter CreateDataParameter(string name, object val);
    }
}
