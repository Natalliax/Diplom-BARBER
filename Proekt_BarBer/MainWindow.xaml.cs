﻿using Microsoft.Office.Interop.Excel;
using Mysqlx.Crud;
using Mysqlx.Resultset;
using Org.BouncyCastle.Asn1.Crmf;
using Org.BouncyCastle.Asn1.Nist;
using Org.BouncyCastle.Asn1.X509;
using Proekt_BarBer.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Window = System.Windows.Window;

namespace Proekt_BarBer
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow: Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Person person = new Person();
			dGrid.ItemsSource = App.Db.Persons.ToList();
			dGrid.DataContext = person;
			stackpanelReg = new StackPanel();

			textBoxUsl.ItemsSource = App.Db.NameServices.ToList();
			Material.ItemsSource = App.Db.Materials.ToList();
			textBoxMaster.ItemsSource = App.Db.MastSchedules.ToList();
			textBoxTime.ItemsSource = App.Db.MastSchedules.ToList();
			textBoxDis.ItemsSource = App.Db.DisInfo.ToList();

		}

		private void dGrid_Loaded(object sender, RoutedEventArgs e)
		{
			SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
			sqlConnection.Open();

		}
		Person selectedPr = null;

		private void dGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			if (e.RemovedItems.Count > 0) return;

			selectedPr = (Person)dGrid.SelectedItem;
			if (selectedPr == null) return;
			textBoxName.Text = selectedPr.Name;
			textBoxDate.Text = selectedPr.Date;
			textBoxTime.Text = selectedPr.Time;
			textBoxUsl.Text = selectedPr.NameService.Usl;

			

           


            Price.Text = Price.ToString();


			App.Db.Persons.AddOrUpdate(selectedPr);


			App.Db.SaveChanges();
			dGrid.ItemsSource = null;
			dGrid.ItemsSource = App.Db.Persons.ToList();
		}

		private void CalculateTotalPrice()
		{
			if (decimal.TryParse(Price1.Text, out decimal price1) && decimal.TryParse(Price2.Text, out decimal price2))
			{
				Price.Text = (price1 + price2).ToString();
			}
		}
		private void Discount()
		{
			DateTime? birthday = textBoxAge.SelectedDate;
			DateTime? visitDate = textBoxDate.SelectedDate;

			if (birthday.HasValue && visitDate.HasValue)
			{
				if (birthday.Value.Month == visitDate.Value.Month && birthday.Value.Day == visitDate.Value.Day)
				{
					MessageBox.Show("День рождение!");
				}
			}
		}


		private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void textBoxDate_TextChanged(object sender, SelectionChangedEventArgs e)
		{
			Discount();
		}

		private void textBoxAge_TextChanged(object sender, SelectionChangedEventArgs e)
		{
			Discount();
		}

		private void Usl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{


			if (textBoxUsl.SelectedItem is NameService ns)
			{
				Price1.Text = ns.Price.ToString("#0.00");

				if (textBoxDis.SelectedItem is DiscountInfo dis)
				{
					Price1.Text = (ns.Price - (ns.Price * dis.Persent)).ToString("#0.00");

				}

			}
		}

		private void Material_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (Material.SelectedItem is Material m)
			{
				Price2.Text = m?.Price.ToString();

			}
		}

        private HashSet<string> uniqueMasters = new HashSet<string>();

        private void textBoxMaster_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedMaster = textBoxMaster.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedMaster))
            {
                // Проверьте, что выбранный мастер не повторяется
                if (!uniqueMasters.Contains(selectedMaster))
                {
                    // Добавьте мастера в ComboBox
                    textBoxMaster.Items.Add(selectedMaster);

                    // Добавьте мастера в коллекцию уникальных мастеров
                    uniqueMasters.Add(selectedMaster);
                }
            }
        }

        private void checkBoxViz_Checked(object sender, RoutedEventArgs e)
		{

		}

		private void Price1_TextChanged(object sender, TextChangedEventArgs e)
		{

		}


		private void Price2_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void textBoxTotal_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void ButtonPrice_Click(object sender, RoutedEventArgs e)
		{
			if (checkBoxViz.IsChecked != false)
			{
				try
				{
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

		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
			dGrid.ItemsSource = null;
			dGrid.ItemsSource = App.Db.Persons.Local.ToBindingList();
			App.Db.Persons.Add(new Person
			{
				Name = textBoxName.Text,
				Date = textBoxDate.Text,
				Time = textBoxTime.Text,
				NameService = (NameService)textBoxUsl.SelectedItem,
				MastSchedule = (MastSchedule)textBoxMaster.SelectedItem,
				Price = decimal.Parse(Price1.Text) + decimal.Parse(Price2.Text),



			});
			MessageBox.Show("Пациент успешно зарегистрирован!");

			App.Db.SaveChanges();
		}

		private void btnSchedule_Click_1(object sender, RoutedEventArgs e)
		{
			Schedule schedule = new Schedule();
			schedule.Show();
		}

		private void btnReport_Click(object sender, RoutedEventArgs e)
		{
			Report report = new Report();
			report.Show();
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			if (dGrid.SelectedItems.Count == 0)
				return;
			if (MessageBox.Show("Вы уверены?", "Удалить запись?", MessageBoxButton.YesNo) == MessageBoxResult.No)
				return;
			for (int i = dGrid.SelectedItems.Count - 1; i >= 0; i--)
			{
				App.Db.Persons.Remove(dGrid.SelectedItems[i] as Person);
			}
			App.Db.SaveChanges();

			dGrid.ItemsSource = null;
			dGrid.ItemsSource = App.Db.Persons.ToList();
		}



		private void btnService_Click(object sender, RoutedEventArgs e)
		{
			Service service = new Service();
			service.Show();
		}

		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			if (selectedPr != null)
			{
				selectedPr.Name = textBoxName.Text;
				selectedPr.Date = textBoxDate.Text;
				selectedPr.Time = textBoxTime.Text;
				selectedPr.NameService = (NameService)textBoxUsl.SelectedItem;
				selectedPr.MastSchedule = (MastSchedule)textBoxMaster.SelectedItem;
				Price.Text = Price.ToString();

				App.Db.Persons.AddOrUpdate(selectedPr);
				App.Db.SaveChanges();
				dGrid.SelectedItem = selectedPr;

				dGrid.ItemsSource = null;
				dGrid.ItemsSource = App.Db.Persons.Local.ToBindingList();
			}
		}



		private void Persent_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void NameDis_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (textBoxDis.SelectedItem is DiscountInfo dis)
			{
				Persent.Text = dis.Persent.ToString("#0%");
			}
		}

		public void ButtonClear_Click(object sender, RoutedEventArgs e)
		{
			textBoxName.Text = "";
			textBoxAge.Text = "";
			textBoxDate.Text = "";
			textBoxTime.Text = "";
			textBoxUsl.Text = " ";
			textBoxDis.Text = "";
			Price1.Text = " ";
			Price2.Text = " ";
			textBoxMaster.Text = " ";
			Price.Text = "";
			Material.Text = "";
			Persent.Text = "";

		}

		private void textBoxTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
           
        }

		private void btnExp_Click_1(object sender, RoutedEventArgs e)
		{

			dynamic excelApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
			var workbook = excelApp.Workbooks.Add();
			var sheet = workbook.Sheets[1];

			// Заполните данными из DataGrid 
			for (int i = 0; i < dGrid.Columns.Count; i++)
			{
				sheet.Cells[2, i + 1].Value2 = dGrid.Columns[i].Header;
				for (int j = 0; j < dGrid.Items.Count; j++)
				{
					TextBlock cellContent = dGrid.Columns[i].GetCellContent(dGrid.Items[j]) as TextBlock;
					sheet.Cells[j + 3, i + 1].Value2 = cellContent?.Text ?? "-";
				}
			}
            sheet.Cells[1, 3].Font.Bold = true; // Жирный шрифт для заголовка
           /* sheet.Cells[1, 3].Interior.Color = System.Drawing.Color.Yellow;*/ // Желтый фон для заголовка
            sheet.Cells[1, 3].Value2 = "Ведомость записи клиентов"; // Заголовок таблицы

            sheet.Cells[2, 1].Interior.Color = System.Drawing.Color.Gray;
            sheet.Cells[2, 2].Interior.Color = System.Drawing.Color.Gray;
            sheet.Cells[2, 3].Interior.Color = System.Drawing.Color.Gray;
            sheet.Cells[2, 4].Interior.Color = System.Drawing.Color.Gray;
            sheet.Cells[2, 5].Interior.Color = System.Drawing.Color.Gray;
            sheet.Cells[2, 6].Interior.Color = System.Drawing.Color.Gray;
            

            sheet.Columns[1].ColumnWidth = 15; // Установите ширину  столбца (в символах)
            sheet.Columns[2].ColumnWidth = 15; // Установите ширину  столбца (в символах)
            sheet.Columns[3].ColumnWidth = 15; // Установите ширину столбца (в символах)
            sheet.Columns[4].ColumnWidth = 15; // Установите ширину  столбца (в символах)
            sheet.Columns[5].ColumnWidth = 15; // Установите ширину  столбца (в символах)
            sheet.Columns[6].ColumnWidth = 15; // Установите ширину  столбца (в символах)

            sheet.Cells[2, 1].HorizontalAlignment = XlHAlign.xlHAlignCenter; // Центрирование текста в первой 
            sheet.Cells[2, 2].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            sheet.Cells[2, 3].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            sheet.Cells[2, 4].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            sheet.Cells[2, 5].HorizontalAlignment = XlHAlign.xlHAlignCenter;
			sheet.Cells[2, 6].HorizontalAlignment = XlHAlign.xlHAlignCenter;
           

            sheet.Cells[2, 1].Font.Size = 12; // Установите размер шрифта в 12 пунктов
            sheet.Cells[2, 2].Font.Size = 12;
            sheet.Cells[2, 3].Font.Size = 12;
            sheet.Cells[2, 4].Font.Size = 12;
            sheet.Cells[2, 5].Font.Size = 12;
            sheet.Cells[2, 6].Font.Size = 12;

            excelApp.Visible = true;
        }
	}
}