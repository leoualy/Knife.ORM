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
        /// <summary>
        /// 处理查询语句获取DataTable对象
        /// </summary>
        /// <param name="sql">select 语句</param>
        /// <param name="sqlparms">参数</param>
        /// <returns>DataTable 对象</returns>
        DataTable ExecDQLForDataTable(string sql, List<IDataParameter> sqlparms = null);
        /// <summary>
        /// 处理数据控制语言
        /// </summary>
        /// <param name="sql">update,insert,delete 语句</param>
        /// <param name="sqlparms">参数列表</param>
        /// <returns>影响的列数</returns>
        int ExecDML(string sql, List<IDataParameter> sqlparms = null);
        /// <summary>
        /// 处理数据查询语言
        /// </summary>
        /// <param name="sql">select 语句</param>
        /// <param name="sqlparms">参数列表</param>
        /// <returns>查询的结果</returns>
        object ExecDQL(string sql, List<IDataParameter> sqlparms = null);
        /// <summary>
        /// 创建参数对象
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="val">参数值</param>
        /// <returns></returns>
        IDataParameter CreateDataParameter(string name, object val);
    }
}
