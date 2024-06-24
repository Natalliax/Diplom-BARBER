using Proekt_BarBer.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proekt_BarBer
{
	/// <summary>
	/// Логика взаимодействия для Dis.xaml
	/// </summary>
	public partial class Dis : Window
	{
		public Dis()
		{
			InitializeComponent();
			
			DiscountInfo discountInfo = new DiscountInfo();
			disGrid.ItemsSource = App.Db.DisInfo.ToList();

			disGrid.DataContext = discountInfo;
			stackpanelDis = new StackPanel();
		}


		private void pGrid_Loaded(object sender, RoutedEventArgs e)
		{
			SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
			sqlConnection.Open();

		}
		DiscountInfo selectedDs = null;
		private void disGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.RemovedItems.Count > 0) return;

			selectedDs = (DiscountInfo)disGrid.SelectedItem;
			if (selectedDs == null) return;
			textBox1.Text = selectedDs.NameDis;
			textBox2.Text = selectedDs.Persent.ToString();
			



			App.Db.DisInfo.AddOrUpdate(selectedDs);


			App.Db.SaveChanges();
			disGrid.ItemsSource = null;
			disGrid.ItemsSource = App.Db.DisInfo.ToList();
		}


		private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
		{

        }

		private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
		{


			
		}

		

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			disGrid.ItemsSource = null;
			disGrid.ItemsSource = App.Db.DisInfo.Local.ToBindingList();
			App.Db.DisInfo.Add(new DiscountInfo
			{
				NameDis = textBox1.Text,
				Persent = decimal.Parse(textBox2.Text),
				

			});
			App.Db.SaveChanges();
		}

		private void EditButton_Click_1(object sender, RoutedEventArgs e)
		{
			if (selectedDs != null)
			{
				selectedDs.NameDis= textBox1.Text;
				selectedDs.Persent = decimal.Parse(textBox2.Text);
				

				App.Db.DisInfo.AddOrUpdate(selectedDs);
				App.Db.SaveChanges();
				disGrid.SelectedItem = selectedDs;

				disGrid.ItemsSource = null;
				disGrid.ItemsSource = App.Db.DisInfo.Local.ToBindingList();
			}
		}

		private void DelButton_Click_2(object sender, RoutedEventArgs e)
		{
			if (selectedDs == null) return;
			if (System.Windows.MessageBox.Show("Вы уверены?", "Удалить запись?", MessageBoxButton.YesNo) == MessageBoxResult.No)
				return;
			App.Db.DisInfo.Remove(selectedDs);
			App.Db.SaveChanges();
			disGrid.ItemsSource = null;
			disGrid.ItemsSource = App.Db.DisInfo.Local.ToBindingList();
		}

		private void ClosButton_Click_3(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
