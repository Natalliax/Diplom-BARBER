using Proekt_BarBer.Core;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;
using Window = System.Windows.Window;


namespace Proekt_BarBer
{
    /// <summary>
    /// Логика взаимодействия для Service.xaml
    /// </summary>
    public partial class Service : Window
    {
        public Service()
        {
            InitializeComponent();
            NameService nameService = new NameService();
            serviceGrid.ItemsSource = App.Db.NameServices.ToList();
            stackpanelService.DataContext = nameService;
            serviceGrid.DataContext = nameService;
            stackpanelService = new StackPanel();

        }


        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            sqlConnection.Open();

        }


        public void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            serviceGrid.ItemsSource = null;
            serviceGrid.ItemsSource = App.Db.NameServices.Local.ToBindingList();
            App.Db.NameServices.Add(new NameService { Usl = textBox1.Text, 
                Price = decimal.Parse(textBox2.Text), 
                Description = textBox3.Text, 
                Timecomletion = textBox4.Text });
            App.Db.SaveChanges();

        }


        public void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedNs == null) return;
            if (MessageBox.Show("Вы уверены?", "Удалить запись?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            App.Db.NameServices.Remove(selectedNs);
            App.Db.SaveChanges();
            serviceGrid.ItemsSource = null;
            serviceGrid.ItemsSource = App.Db.NameServices.Local.ToBindingList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            //SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            //sqlConnection.Open();



            if (selectedNs != null)
            {
                selectedNs.Usl = textBox1.Text;
                selectedNs.Price = decimal.Parse(textBox2.Text);
                selectedNs.Description = textBox3.Text;
                selectedNs.Timecomletion = textBox4.Text;


                App.Db.NameServices.AddOrUpdate(selectedNs);
                App.Db.SaveChanges();
                serviceGrid.SelectedItem = selectedNs;

                serviceGrid.ItemsSource = null;
                serviceGrid.ItemsSource = App.Db.NameServices.Local.ToBindingList();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        NameService selectedNs = null;


        private void serviceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0) return;

            selectedNs = (NameService)serviceGrid.SelectedItem;
            if (selectedNs == null) return;
            textBox1.Text = selectedNs.Usl;
            textBox2.Text = selectedNs.Price.ToString();
            textBox3.Text = selectedNs.Description;
            textBox4.Text = selectedNs.Timecomletion;
            App.Db.NameServices.AddOrUpdate(selectedNs);


            App.Db.SaveChanges();
            serviceGrid.ItemsSource = null;
            serviceGrid.ItemsSource = App.Db.NameServices.ToList();


        }
    }
}

