/***************************************************************************
 * 文件名:             ClassCreator.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              该类用于生成C#类文件
***************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Knife.ORM.Common
{
    internal class ClassCreator
    {
        
        const string UsingString = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;";
        
        static StreamWriter gSWriter;

        static string CreateNamespace(string name)
        {
            return string.Format(@"namespace {0}", name);
        }
        static string CreateClass(string className)
        {
            return string.Format("public class {0}", className);
        }
        
        /// <summary>
        /// 创建一个类文件
        /// </summary>
        /// <param name="nameSpace">类命名空间</param>
        /// <param name="className">类名</param>
        /// <param name="file">生成的文件</param>
        /// <param name="contentsByLine">按行分隔的类内容</param>
        internal static void CreateClassFile(string nameSpace,string className,string file,List<string> contentsByLine)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            using(FileStream fs =new FileStream(file,FileMode.CreateNew,FileAccess.Write))
            {
                gSWriter = new StreamWriter(fs);
                // 写入using
                gSWriter.Write(UsingString);
                gSWriter.Write("\r\n\r\n");
                // 写入namespace
                gSWriter.Write(CreateNamespace(nameSpace));
                
                // namespace 的开始
                gSWriter.Write("\r\n{");
                // 写入class定义
                gSWriter.Write("\r\n    " + CreateClass(className));
                // class 的开始
                gSWriter.Write("\r\n    {\r\n");
                foreach (string val in contentsByLine)
                {
                    gSWriter.Write(val);
                }
                
                gSWriter.Write("    }");
                // class的结束
                gSWriter.Write("\r\n}");
                // namespace 的结束
                gSWriter.Close();
            }
            
        }

        /// <summary>
        /// 根据名称和数据类型生成属性
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="dataType">数据类型</param>
        /// <returns>属性定义语句</returns>
        internal static string CreateProfile(string name, string dataType)
        {
            return "        public " + dataType+" " + name + " { get; set; }\r\n\r\n";
        }

    }
}
