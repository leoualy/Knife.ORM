using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Knife.ORM;
using Knife.ORM.KMeta;

namespace SampleWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //KnifeConfig.SQLServerConnectionString = "Data Source =localhost;Initial Catalog = DTStation;Integrated Security = SSPI";
            //KnifeConfig.SQLServerConnectionString = "server =localhost;database = master;uid=sa;pwd=000000";
            KnifeConfig.MySqlConnectionString = "server=localhost;database=information_schema;uid=root;pwd=root";
            q = new KQueryMSSQL();
        }
        Query q;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            //WaitingRoom wr = new WaitingRoom() { Name = "query8" };
           
            //KQueryMSSQL q1 = new KQueryMSSQL();
            //List<WaitingRoom> wrs = new List<WaitingRoom>();
            //wrs.Add(wr);
            //wrs.Add(wr);
            //q1.Insert<WaitingRoom>(wrs);


            // KQueryMySql mySql = new KQueryMySql();

            //mySql.Insert<user>(new List<user>() { new user() { Name = "zz" } });
            // List<user> us = mySql.Less("ID", 2).Select<user>();
            IDatabase mysql = new SQLServerMeta();
            mysql.Connect("localhost", "sa", "000000");
            MessageBox.Show(mysql.GetTable("DBTrain","Led").Name);
            
        }
    }
}
