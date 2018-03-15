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
            KnifeConfig.SQLServerConnectionString = "Data Source =localhost;Initial Catalog = DTStation;Integrated Security = SSPI";
            q = new KQueryMSSQL();
        }
        Query q;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Context sqlserver = new SqlServerContext();

            WaitingRoom wr = new WaitingRoom() { Name = "query8" };
            //int c = sqlserver.ModifyByConditionColumns<WaitingRoom>(wr, new WaitingRoom() { ID = 9 });
            //int c1 = sqlserver.Insert<WaitingRoom>(wr);
            //List<WaitingRoom> list = sqlserver.GetModel<WaitingRoom>(wr);
            KQueryMSSQL q1 = new KQueryMSSQL();
            
            // int i = q1.Greater("ID", 9).Less("ID", 12).Columns("ID","Name").Select<WaitingRoom>().Count;
            List<WaitingRoom> wrs = new List<WaitingRoom>();
            wrs.Add(wr);
            wrs.Add(wr);
            q1.Insert<WaitingRoom>(wrs);
            //MessageBox.Show(i.ToString());
        }
    }
}
