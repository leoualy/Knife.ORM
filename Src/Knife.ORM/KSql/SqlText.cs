/***************************************************************************
 * 文件名:             SqlText.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              基本的sql语句自动生成逻辑实现
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knife.ORM.KSql
{
    public abstract class SqlText:SqlDriver
    {
        public SqlText(IDriver driver) : base(driver) { }

        StringBuilder 
            mSelectText, mUpdateText, 
            mWhereText, mColumnsText,
            mInsertText;

        internal List<TEntity> SelectCompleted<TEntity>()
        {
            if (mSelectText == null)
            {
                mSelectText = new StringBuilder("SELECT");
            }
            // 如果列语句不为空，则追加到语句中
            if (mColumnsText != null)
            {
                mSelectText.Append(" "+mColumnsText+" ");
            }
            else
            {
                List<string> columns = Reflector.GetColumns<TEntity>();
                for (int i = 0; i < columns.Count; i++)
                {
                    mSelectText.Append(" " + columns[i]);
                    if (i != columns.Count-1)
                    {
                        mSelectText.Append(",");
                    }
                }
                mSelectText.Append(" ");
            }


            // 在sql 中添加表名
            mSelectText.Append("FROM "+getTableName<TEntity>() + " ");
            // 在sql中添加where语句
            string sql = null;
            if (mWhereText != null)
            {
                mSelectText.Append(mWhereText.ToString()).ToString();
            }
            Console.WriteLine(mSelectText.ToString());

            return Query<TEntity>(mSelectText.ToString());
        }

        internal int UpdateCompleted<TEntity>(TEntity entity)
        {
            if (mUpdateText == null)
            {
                mUpdateText = new StringBuilder("UPDATE ");
            }
            mUpdateText.Append(getTableName<TEntity>()+" SET ");

            List<CV> list = Reflector.GetCVs<TEntity>(entity);

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Name == "ID")
                {
                    continue;
                }
                mUpdateText.Append(string.Format("{0}=@{0}", list[i].Name));
                AddParams(list[i].Name, list[i].Value);

                if (i != list.Count - 1)
                {
                    mUpdateText.Append(",");
                }
                else
                {
                    mUpdateText.Append(" ");
                }
            }

            if (mWhereText != null)
            {
                mUpdateText.Append(mWhereText);
            }
            Console.WriteLine(mUpdateText.ToString());
            return UnQuery(mUpdateText.ToString());
        }

        internal int InsertCompleted<TEntity>(List<TEntity> ts)
        {
            if (mInsertText == null)
            {
                mInsertText = new StringBuilder();
                mInsertText.Append("INSERT INTO");
            }
            mInsertText.Append(" " + getTableName<TEntity>() + " ");
            // 获取列名集合追加到sql中
            List<string> columns = Reflector.GetColumns<TEntity>();
            mInsertText.Append("(");

            for (int i = 0; i < columns.Count; i++)
            {
                if (columns[i] == "ID")
                {
                    continue;
                }
                mInsertText.Append(columns[i]);
                if (i != columns.Count-1)
                {
                    mInsertText.Append(",");
                }
            }

            mInsertText.Append(") VALUES ");

            List<CV> cvs = null;
            // 生成值列表
            for (int j = 0; j < ts.Count; j++)
            {
                cvs = Reflector.GetCVs<TEntity>(ts[0]);
                mInsertText.Append(generateValueSql(cvs,j));
                cvs.Clear();
                if (j != ts.Count-1)
                {
                    mInsertText.Append(",");
                }
            }
            Console.WriteLine(mInsertText.ToString());
            return UnQuery(mInsertText.ToString());
        }


        StringBuilder generateValueSql(List<CV> cvs,int index)
        {
            StringBuilder sbldr = new StringBuilder("(");
            
            for (int i = 0; i < cvs.Count; i++)
            {
                if (cvs[i].Name == "ID")
                {
                    continue;
                }
                string name=string.Format("@{0}{1}", cvs[i].Name,index);
                sbldr.Append(name);
                if (i != cvs.Count-1)
                {
                    sbldr.Append(",");
                }
                AddParams(name, cvs[i].Value);
            }
            sbldr.Append(")");
            return sbldr;
        }

        /// <summary>
        /// 追加where条件
        /// </summary>
        /// <param name="sql"></param>
        internal void WhereTextAppend(string sql)
        {
            if (mWhereText == null)
            {
                mWhereText = new StringBuilder("WHERE " + sql);
            }
            else
            {
                mWhereText.Append(" AND " + sql);
            }
        }

        internal void ColumnsTextAppend(string sql)
        {
            if (mColumnsText == null)
            {
                mColumnsText = new StringBuilder();
            }
            mColumnsText.Append(sql);
        }

        internal void ResetSelectText()
        {
            mSelectText = null;
            mWhereText = null;
        }

        string getTableName<TEntity>()
        {
            return typeof(TEntity).Name;
        }
    }
}
