using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knife.ORM;


namespace EntityMaker
{
    public class BindingTable : ViewModel
    {
        private string mName;
        public string Name
        {
            get { return mName; }
            set
            {
                mName = value;
                OnPropertyChanged("Name");
            }
        }

        private string mIsSelected;
        public string IsSelected
        {
            get { return mIsSelected; }
            set
            {
                mIsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }
    }

    public class BindingDatabase : ViewModel
    {
        private string mName;
        public string Name
        {
            get { return mName; }
            set
            {
                mName = value;
                OnPropertyChanged("Name");
            }
        }

    }

    public class MainViewModel:ViewModel
    {
        public MainViewModel()
        {
            mHostName = ".";
            mUserName = "sa";
            mPasswd = "000000";
        }

        private string mHostName;
        public string HostName
        {
            get { return mHostName; }
            set
            {
                mHostName = value;
                OnPropertyChanged("HostName");
            }
        }

        private string mUserName;
        public string UserName
        {
            get { return mUserName; }
            set
            {
                mUserName = value;
                OnPropertyChanged("UserName");
            }
        }

        private string mPasswd;
        public string Password
        {
            get { return mPasswd; }
            set
            {
                mPasswd = value;
                OnPropertyChanged("Password");
            }
        }

        private string mNamespace;

        public string Namespace
        {
            get { return mNamespace; }
            set
            {
                mNamespace = value;
                OnPropertyChanged("Namespace");
            }
        }

        private string mDir;
        public string Dir
        {
            get { return mDir; }
            set
            {
                mDir = value;
                OnPropertyChanged("Dir");
            }
        }

        private List<BindingDatabase> mDatabases;
        public List<BindingDatabase> Databases
        {
            get { return mDatabases; }
            set
            {
                mDatabases = value;
                OnPropertyChanged("Databases");
            }
        }


        private List<BindingTable> mTables;
        public List<BindingTable> Tables
        {
            get { return mTables; }
            set
            {
                mTables = value;
                OnPropertyChanged("Tables");
            }
        }

        public bool TryLoginServer()
        {
            try
            {
                DMOHelper.LoginServer(mHostName,mUserName,mPasswd);
                if (mDatabases == null)
                {
                    mDatabases = new List<BindingDatabase>();
                }
                mDatabases.Clear();
                foreach (string name in DMOHelper.GetDatabases())
                {
                    mDatabases.Add(new BindingDatabase() { Name = name });
                }

                Databases = mDatabases;
                return true;
            }
            catch
            {
                return false;
            }
        }

        List<string> list;
        public void SelectDB(object db)
        {
            list = GetTables(db.ToString());
            mTables = new List<BindingTable>();
            foreach (string val in list)
            {
                mTables.Add(new BindingTable() { Name = val });
            }
           Tables = mTables;
           if (mSelectedTables == null)
           {
               mSelectedTables = new List<string>();
           }
           mSelectedTables.Clear();
        }

        List<string> mSelectedTables;
        public void SelectTable(object obj)
        {
            mSelectedTables.Add(obj.ToString());
        }

        public void UnSelectTable(object obj)
        {
            mSelectedTables.Remove(obj.ToString());
        }

        private List<string> GetTables(string dbName)
        {
            List<string> list = new List<string>();
            KnifeConfig.SQLServerConnectionString = string.Format("Data Source ={0};Initial Catalog ={1};User ID={2};Password={3}",
                this.mHostName, dbName, this.mUserName, this.mPasswd);
            foreach (var val in (EntityContext.GetTablesOfDatabase()))
            {
                list.Add(val);
            }
            return list;
        }
        
        public void CreateEntities()
        {
            foreach (string val in mSelectedTables)
            {
                EntityContext.CreateEntity(val, this.Namespace, this.Dir);
            }
        }

    }
}
