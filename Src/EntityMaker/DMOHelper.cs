using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLDMO;

namespace EntityMaker
{
    internal class DMOHelper
    {
        internal static List<string> GetSQLServers()
        {
            SQLDMO.Application app = new SQLDMO.Application();
            NameList nameList = app.ListAvailableSQLServers();
            
            
            if (nameList.Count <= 0)
            {
                return null;
            }
            List<string> list = new List<string>();
            for (int i = 0; i < nameList.Count; i++)
            {
                list.Add(nameList.Item(i+1));
            }
           



            return list;
        }
        static SQLServer mSQLServer;
        internal static void LoginServer(string srvName, string user, string pwd)
        {
             mSQLServer = new SQLServer();
            
            try
            {
                mSQLServer.Connect(srvName, user, pwd);
            }
            catch(Exception e)
            {
                throw new Exception(string.Format("登陆数据库服务器失败,异常消息:{0}", e.Message));
            }
        }

        internal static List<string> GetDatabases()
        {
            if (mSQLServer == null)
            {
                return null;
            }
            List<string> list = new List<string>();
            foreach (SQLDMO.Database val in mSQLServer.Databases)
            {
                
                list.Add(val.Name);
            }
            return list;
        }


    }
}
