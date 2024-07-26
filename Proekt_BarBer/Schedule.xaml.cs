using Proekt_BarBer.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.DataVisualization.Charting;
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
	/// Логика взаимодействия для Schedule.xaml
	/// </summary>
	public partial class Schedule : Window
	{

      

        public Schedule()
		{

			InitializeComponent();
			MastSchedule mastschedule = new MastSchedule();
			scheduleGrid.ItemsSource = App.Db.MastSchedules.ToList();
            //stackpanelSchedule.DataContext = mastschedule;
            scheduleGrid.DataContext = mastschedule;
			stackpanelSchedule = new StackPanel();
        }


        private void mGrid_Loaded(object sender, RoutedEventArgs e)
		{
			SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
			sqlConnection.Open();

		}

		MastSchedule selectedSc = null;

		private void scheduleGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.RemovedItems.Count > 0) return;

			selectedSc = (MastSchedule)scheduleGrid.SelectedItem;
			if (selectedSc == null) return;
			textBox1.Text = selectedSc.Master;
			textBox2.Text = selectedSc.Date;
			textBox3.Text = selectedSc.Time;
			
            App.Db.MastSchedules.AddOrUpdate(selectedSc);
			App.Db.SaveChanges();
			scheduleGrid.ItemsSource = App.Db.MastSchedules.ToList();
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{

            var newSchedule = new MastSchedule
            {
                Master = textBox1.Text,
                Date = textBox2.Text,
                Time = textBox3.Text,
				IsRegistered = true,
				
            };

            App.Db.MastSchedules.AddOrUpdate(newSchedule);
            App.Db.SaveChanges();

          
            // Обновляем DataGrid
            scheduleGrid.ItemsSource = null;
            scheduleGrid.ItemsSource = App.Db.MastSchedules.Local.ToBindingList();
        }

		private void EditButton_Click_1(object sender, RoutedEventArgs e)
		{
			if (selectedSc != null)
			{
				selectedSc.Master = textBox1.Text;
				selectedSc.Date = textBox2.Text;
				selectedSc.Time = textBox3.Text;
				

				App.Db.MastSchedules.AddOrUpdate(selectedSc);
				App.Db.SaveChanges();
				scheduleGrid.SelectedItem = selectedSc;

				scheduleGrid.ItemsSource = null;
				scheduleGrid.ItemsSource = App.Db.MastSchedules.Local.ToBindingList();
			}
		}

	

		private void DelButton_Click_2(object sender, RoutedEventArgs e)
		{
			if (selectedSc == null) return;
			if (MessageBox.Show("Вы уверены?", "Удалить запись?", MessageBoxButton.YesNo) == MessageBoxResult.No)
				return;
			App.Db.MastSchedules.Remove(selectedSc);
			
			App.Db.SaveChanges();
			scheduleGrid.ItemsSource = null;
			scheduleGrid.ItemsSource = App.Db.MastSchedules.Local.ToBindingList();
		}

		private void ClosButton_Click_3(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void DisButton_Click(object sender, RoutedEventArgs e)
		{
			Dis dis = new Dis();
			dis.ShowDialog();

		}
	}
}
