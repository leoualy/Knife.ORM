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
using EntityMaker.ViewController;

namespace EntityMaker
{
    /// <summary>
    /// MainViewController.xaml 的交互逻辑
    /// </summary>
    public partial class MainViewController : UserControl
    {
        public MainViewController()
        {
            InitializeComponent();
            mViewModel = new MainViewModel();
            this.DataContext = mViewModel;
            this.Loaded += MainViewController_Loaded;
        }

        void MainViewController_Loaded(object sender, RoutedEventArgs e)
        {
            this.gridLogin.Visibility = Visibility.Visible;
        }
        private MainViewModel mViewModel;

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (mViewModel.TryLoginServer())
            {
                this.gridLogin.Visibility = Visibility.Collapsed;
                return;
            }
            MessageBox.Show("error");
        }

        private void ChangeServer_Click(object sender, RoutedEventArgs e)
        {
            this.gridLogin.Visibility = Visibility.Visible;
        }

        private void SelectDB_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            mViewModel.SelectDB(btn.Tag);
        }
        private void CreateEntity_Click(object sender, RoutedEventArgs e)
        {
            mViewModel.CreateEntities();
        }

        private void Folder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dia = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult ret = dia.ShowDialog();
            mViewModel.Dir = dia.SelectedPath.Trim();
        }

        private void SelectTB_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (ControlAttachManager.GetIsTableSelected(btn))
            {
                mViewModel.UnSelectTable(btn.Tag.ToString());
                ControlAttachManager.SetIsTableSelected(btn, false);
            }
            else
            {
                mViewModel.SelectTable(btn.Tag.ToString());
                ControlAttachManager.SetIsTableSelected(btn, true);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (this.mViewModel == null)
            {
                return;
            }
            this.mViewModel.DBType = cb.SelectedIndex;
        }

    }
}
