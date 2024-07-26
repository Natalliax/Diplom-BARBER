using Proekt_BarBer.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
using System.Windows.Shapes;

namespace Proekt_BarBer
{
    /// <summary>
    /// Логика взаимодействия для Wage.xaml
    /// </summary>
    public partial class Wage : Window
    {
        public Wage()
        {
            InitializeComponent();
            Cal cal = new Cal();
            dGrid.ItemsSource = App.Db.Cals.ToList();
            dGrid.DataContext = cal;
        }

        private void dGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (e.RemovedItems.Count > 0) return;
            selectedCl = (Cal)dGrid.SelectedItem;
            if (selectedCl == null) return;
            App.Db.Cals.AddOrUpdate(selectedCl);
            App.Db.SaveChanges();
         
            dGrid.ItemsSource = App.Db.Cals.ToList();

        }

        Cal selectedCl = null;

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Zarplata zarplata = new Zarplata();
            zarplata.Show();

            App.Db.SaveChanges();
            dGrid.SelectedItem = selectedCl;
            dGrid.ItemsSource = null;
            dGrid.ItemsSource = App.Db.Cals.Local.ToBindingList();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCl == null) return;
            if (MessageBox.Show("Вы уверены?", "Удалить запись?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;
            App.Db.Cals.Remove(selectedCl);
            App.Db.SaveChanges();
            dGrid.ItemsSource = null;
            dGrid.ItemsSource = App.Db.Cals.Local.ToBindingList();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClos_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
