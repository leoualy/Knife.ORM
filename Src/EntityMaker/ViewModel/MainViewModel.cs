using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Knife.ORM;
using Knife.ORM.KMeta;


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
            mHostName = "localhost";
            mUserName = "sa";
            mPasswd = "000000";
            mDBType = 0;
        }

        #region 绑定的属性
        private int mDBType;
        public int DBType
        {
            get { return mDBType; }
            set
            {
                mDBType = value;
                OnPropertyChanged("DBType");
            }
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
        #endregion 绑定的属性

        IDatabase db;
        string mDbSelected;
        public bool TryLoginServer()
        {
            try
            {

                switch (DBType)
                {
                    case 0: db = new SQLServerMeta(); break;
                    case 1: db = new MySqlMeta(); break;
                    default: break;
                }

                if (db.Connect(mHostName, mUserName, mPasswd) < 0)
                {
                    return false;
                }
                if (mDatabases == null)
                {
                    mDatabases = new List<BindingDatabase>();
                }
                mDatabases.Clear();
                foreach (string name in db.GetDatabases())
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
        public void SelectDB(object dbName)
        {
            list = db.GetTablesOfDB(dbName.ToString());
            mTables = new List<BindingTable>();
            mDbSelected = dbName.ToString();
            if (list == null)
            {
                return;
            }
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

        public void CreateEntities()
        {
            foreach (string val in mSelectedTables)
            {
                db.CreateEntity(db.GetTable(this.mDbSelected, val), this.Namespace, this.Dir);
            }
        }

    }
}
