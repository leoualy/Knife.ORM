/***************************************************************************
 * 文件名:             Reflector.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              反射封装
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace Knife.ORM
{

    internal class CV
    {
        internal string Name { get; set; }
        internal object Value { get; set; }
    }
    internal class Reflector
    {
        internal static TModel Row2Model<TModel>(DataRow dr)
        {
            Type t = typeof(TModel);

            object instance = Activator.CreateInstance(t, true);
           
            PropertyInfo[] ps = t.GetProperties();

            for (int i = 0; i < ps.Count(); i++)
            {
                ps[i].SetValue(instance,dr[ps[i].Name]);
            }
            return (TModel)instance;
        }

        internal static List<CV> GetCVs<TModel>(TModel obj)
        {
            Type t = typeof(TModel);
            PropertyInfo[] infos = t.GetProperties();
            List<CV> list = new List<CV>();
            foreach (PropertyInfo info in infos)
            {
                if (info.GetValue(obj) == null)
                {
                    continue;
                }
                list.Add(new CV() { Name = info.Name, Value = info.GetValue(obj) });
            }
            return list;
        }

        internal static List<string> GetColumns<TModel>()
        {
            Type t = typeof(TModel);
            PropertyInfo[] infos = t.GetProperties();
            List<string> list = new List<string>();
            foreach (PropertyInfo info in infos)
            {
                if (info.Name == "ID")
                {
                    continue;
                }
                list.Add(info.Name);
            }
            return list;
        }

        /// <summary>
        /// 从给定的程序集和类型获取类型实例
        /// </summary>
        /// <param name="path">程序集路径</param>
        /// <param name="nameOfType">要提取的类型名</param>
        /// <returns></returns>
        internal static object GetInstanceFromDLL(string path,string nameOfType)
        {
            Assembly asm = Assembly.LoadFrom(path);
            object obj = asm.CreateInstance(nameOfType);
            return obj;
        }

    }
}
