/***************************************************************************
 * 文件名:             PathUtil.cs
 * 作者：              Aaron
 * QQ   :                 991258519
 * 日期：              2018-03-15
 * 描述：              路径处理类，目前只能处理桌面应用的路径问题，
 * 后续添加web路径解决
***************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Knife.ORM.Common
{
    internal class PathUtil
    {
        internal static string GetProjDir(string projFile)
        {
            return Path.GetDirectoryName(projFile);
        }

        internal static string GetFullFileName(string name)
        {
            return Path.Combine(Application.StartupPath, name);
        }

        /// <summary>
        /// 程序启动目录
        /// </summary>
        internal static string AppStartupPath = Application.StartupPath;

    }
}
