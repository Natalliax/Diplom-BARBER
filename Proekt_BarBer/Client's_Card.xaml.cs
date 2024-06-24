using Org.BouncyCastle.Asn1.Cms;
using Proekt_BarBer.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Web.UI.DataVisualization.Charting;
using System.Windows;
using System.Windows.Controls;

namespace Proekt_BarBer
{
	/// <summary>
	/// Логика взаимодействия для Client_s_Card.xaml
	/// </summary>
	public partial class Client_s_Card : Window
	{
		internal Driver driver = new Driver();
		
		public Client_s_Card(Person person)
		{
			InitializeComponent();
			textBoxUsl.ItemsSource = App.Db.NameServices.ToList();
			Material.ItemsSource = App.Db.Materials.ToList();
			textBoxMaster.ItemsSource = App.Db.MastSchedules.ToList();


			grid.DataContext = person;
			Price1.Text = (person != null ? person?.Price ?? 0 : 0).ToString("00.00");
			Price2.Text = "0";
			Price.Text = "##.##";

			textBoxUsl.SelectedItem = person.NameService;
			Material.SelectedItem = person.Material;
			textBoxMaster.SelectedItem = person.MastSchedule;
			
			



			Loaded += Client_s_Card_Loaded;
		}

		

		private void Client_s_Card_Loaded(object sender, RoutedEventArgs e)
		{

		}



		private void Usl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (textBoxUsl.SelectedItem is NameService ns)
			{
				Price1.Text = ns.Price.ToString();

			}
		}


		private void Material_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (Material.SelectedItem is Material m)
			{
				Price2.Text = m?.Price.ToString();

			}
		}

		private void CalculateTotalPrice()
		{
			if (decimal.TryParse(Price1.Text, out decimal price1) && decimal.TryParse(Price2.Text, out decimal price2))
			{
				Price.Text = (price1 + price2).ToString();
			}
		}

		private void ButtonOff_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void ButtonPrice_Click(object sender, RoutedEventArgs e)
		{
			if (checkBoxViz.IsChecked != false)
			{
				try
				{
					//Price.Text = (double.Parse(Price1.Text) + double.Parse(Price2.Text)).ToString();
					CalculateTotalPrice();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
			else
			{
				MessageBox.Show("Вы не посетили нас (((");
			}

		}


	
		private void ButtonZap_Click(object sender, RoutedEventArgs e)
		{
			driver.TextBoxName = textBoxName.Text;
			driver.TextBoxDate = textBoxDate.Text.ToString();
			driver.TextBoxUsl = textBoxUsl.Text;
			driver.TextBoxMaster = textBoxMaster.ToString();
			driver.TextBoxPrice1 = Price1.Text;
			driver.TextBoxPrice2 = Price2.Text;
			driver.Price = decimal.Parse(Price1.Text) + decimal.Parse(Price2.Text);




			MessageBox.Show(driver.ToString());

			App.Db.Persons.Add(new Person());

			this.DialogResult = true;





		}

		private void checkBoxViz_Checked(object sender, RoutedEventArgs e)
		{

		}

		private void textBoxTotal_TextChanged(object sender, TextChangedEventArgs e)
		{
			CalculateTotalPrice();
		}

		private void Master_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			
		}

		private void textBoxTime_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void Price_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void Price2_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void Price1_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
		{

		}
		private void textBoxDate_TextChanged(object sender, TextChangedEventArgs e)
		{

		}
	}
}