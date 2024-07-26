using Proekt_BarBer.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using MessageBox = System.Windows.MessageBox;
using Window = System.Windows.Window;


namespace Proekt_BarBer
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
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
			textBoxMaster.ItemsSource = App.Db.MastSchedules.Select(m => m.Master).Distinct().ToList();  //comboBox - заполнение данными (список мастеров без повторений)
			textBoxTime.ItemsSource = App.Db.MastSchedules.ToList();
			textBoxDis.ItemsSource = App.Db.DisInfo.ToList();
			textBoxDate.ItemsSource = App.Db.MastSchedules.Select(m => m.Date).Distinct().ToList();
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
			textBoxTime.Text = selectedPr?.MastSchedule?.Time ?? "";
			textBoxUsl.Text = selectedPr.NameService.Usl;
			textBoxMaster.Text = selectedPr?.MastSchedule?.Master ?? "";
			Material.Text = selectedPr.Material.Name;
			Price1.Text = Price1.Text.ToString();
			Price2.Text = Price2.Text.ToString();
			Price.Text = Price.Text.ToString();
		
			App.Db.Persons.AddOrUpdate(selectedPr);
			App.Db.SaveChanges();
            dGrid.ItemsSource = null;
            dGrid.ItemsSource = App.Db.Persons.ToList();
		}

		private void CalculateTotalPrice()
		{

			if (decimal.TryParse(Price1.Text, out decimal pr1) && !string.IsNullOrEmpty(Price2.Text))
			{
				if (decimal.TryParse(Price2.Text, out decimal pr2))
				{
					Price.Text = (pr1 + pr2).ToString();
				}
				else if (decimal.TryParse(Price1.Text, out decimal price1))
				{
					Price.Text = price1.ToString();
				}
			}
		}


		//Вызов метода при расчете скидки при вводе даты записи
		private void Discount()
		{
            DateTime? birthday = textBoxAge.SelectedDate;
            string visitDateStr = textBoxDate.Text; // Получаем строку даты

            if (DateTime.TryParse(visitDateStr, out DateTime visitDate)) // Преобразуем строку в DateTime
            {
                if (birthday.HasValue)
                {
                    DateTime birthdayThisYear = new DateTime(visitDate.Year, birthday.Value.Month, birthday.Value.Day);
                    DateTime twoDaysBefore = birthdayThisYear.AddDays(-2);
                    DateTime twoDaysAfter = birthdayThisYear.AddDays(2);

                    if (visitDate >= twoDaysBefore && visitDate <= twoDaysAfter)
                    {
                        MessageBox.Show("День рождения!");
                    }
                }
            }
        }

		//Метод расчета скидки
		private void textBoxDis_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		
			if (textBoxDis.SelectedItem is DiscountInfo dis)
			{
				Persent.Text = dis.Persent.ToString("#0%");
			}

		}

       

        //Выбор услуги и применении скидки в случае день рождения
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

		//Вызов метода при расчете скидки при вводе даты рождения
		private void textBoxAge_TextChanged(object sender, SelectionChangedEventArgs e)
		{
			Discount();
		}

		

		private void Price_TextChanged(object sender, TextChangedEventArgs e)
		{
			CalculateTotalPrice();
		}

		private void btnWag_Click_1(object sender, RoutedEventArgs e)
		{
			Wage wage = new Wage();
			wage.Show();
		}

		//Выбор материал из таблицы Material
		private void Material_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (Material.SelectedItem is Material m)
			{
				Price2.Text = m?.Price.ToString();

			}
		}
		public async Task<List<MastSchedule>> LoadRecordsFromDatabaseAsync(MyContext context)
		{
			var records = await context.MastSchedules.ToListAsync();
			return records;
		}

		private async void UpdateFilteredRecords()
		{
			if (textBoxDate.SelectedItem != null && textBoxMaster.SelectedItem != null)
			{
				using (var context = new MyContext())
				{
					var personRecords = await LoadRecordsFromDatabaseAsync(context);
					string dateString = textBoxDate.Text;

					string dateFormat = "dd.MM.yyyy";

					if (DateTime.TryParseExact(dateString, dateFormat, CultureInfo.InvariantCulture,
						DateTimeStyles.None, out DateTime selectedDate))
					{
                        
                            var filteredRecords = personRecords
				  .Where(p => DateTime.Parse(p.Date) >= selectedDate &&
							  p.Master.Equals(textBoxMaster.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase))
									  .ToList();

						var allMasterTimes = filteredRecords.Select(s => s.Time).ToList();

						textBoxTime.ItemsSource = allMasterTimes;
						textBoxTime.IsEnabled = true;
					}
				}
			}
			else
			{
				textBoxTime.IsEnabled = false;
			}
		}

		private async void textBoxMaster_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateFilteredRecords();

			if (textBoxMaster.SelectedItem != null)
			{
				using (var context = new MyContext())
				{
					var personRecords = await LoadRecordsFromDatabaseAsync(context);

					var selectedMaster = textBoxMaster.SelectedItem.ToString();

					// Фильтруем записи для выбранного мастера
					var recordsForSelectedMaster = personRecords
						.Where(p => p.Master.Equals(selectedMaster, StringComparison.OrdinalIgnoreCase) && p.IsRegistered == true)
						.ToList();

					// Получаем уникальные даты работы для выбранного мастера
					var uniqueDates = recordsForSelectedMaster

						.Select(p => p.Date)
						.Distinct()
						.ToList();

					// Устанавливаем источник данных для textBoxDate
					textBoxDate.ItemsSource = uniqueDates;
				}
			}
		}


		//Расчет стоимости при посещении
		private void ButtonPrice_Click(object sender, RoutedEventArgs e)
		{
			CalculateTotalPrice();
		}


		//Вызов справочника расписания и скидки
		private void btnSchedule_Click_1(object sender, RoutedEventArgs e)
		{
			Schedule schedule = new Schedule();

			schedule.ShowDialog();
		}


		//Вызов отчета
		private void btnReport_Click(object sender, RoutedEventArgs e)
		{
			Report report = new Report();
			report.Show();
		}
		//Удаление записи 
		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
            if (selectedPr == null) return;
            if (System.Windows.MessageBox.Show("Вы уверены?", "Удалить запись?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            // Сохраняем ссылку на MastSchedule перед удалением записи
            var mastSchedule = selectedPr.MastSchedule;

            // Удаляем запись из таблицы Person
            App.Db.Persons.Remove(selectedPr);
            App.Db.SaveChanges();

            // Обновляем источник данных для DataGrid
            dGrid.ItemsSource = null;
            dGrid.ItemsSource = App.Db.Persons.Local.ToBindingList();

            // Обновляем таблицу MastSchedule
            if (mastSchedule != null)
            {
                // Проверяем, актуальна ли дата
                DateTime scheduleDate;
                if (DateTime.TryParse(mastSchedule.Date, out scheduleDate) && scheduleDate > DateTime.Now)
                {
                    mastSchedule.IsRegistered = true; // Устанавливаем IsRegistered в false
                }
                else
                {
                    App.Db.MastSchedules.Remove(mastSchedule); // Удаляем запись, если дата не актуальна
                }

                App.Db.SaveChanges();
            }

            MessageBox.Show("Запись успешно удалена!");
        }
    
		//Вызов справочника услуг
		private void btnService_Click(object sender, RoutedEventArgs e)
		{
			Service service = new Service();
			service.ShowDialog();

           
        }

		//Изменение записи
		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			if (selectedPr != null)
			{
				selectedPr.Name = textBoxName.Text;
				selectedPr.Date = textBoxDate.Text;
				selectedPr.Time = textBoxTime.Text;
				selectedPr.NameService = (NameService)textBoxUsl.SelectedItem;
				selectedPr.MastSchedule = App.Db.MastSchedules.FirstOrDefault(m => m.Master == textBoxMaster.Text);
				selectedPr.Material = (Material)Material.SelectedItem;
				selectedPr.Price1 = Price1.Text;
				selectedPr.Price2 = Price2.Text;
				selectedPr.Price = Price.Text;


				App.Db.Persons.AddOrUpdate(selectedPr);
				App.Db.SaveChanges();
				dGrid.SelectedItem = selectedPr;

				dGrid.ItemsSource = null;
				dGrid.ItemsSource = App.Db.Persons.Local.ToBindingList();
			}
		}
		//Кнопка очистить
		public void ButtonClear_Click(object sender, RoutedEventArgs e)
		{
			if (textBoxMaster.SelectedIndex >= -1)

				textBoxMaster.ItemsSource = App.Db.MastSchedules.Select(m => m.Master).Distinct().ToList();

			if (textBoxDate.SelectedIndex == -1)

				textBoxDate.ItemsSource = App.Db.MastSchedules.Select(m => m.Date).Distinct().ToList();

            //if (textBoxDis.SelectedIndex == -1)

            //    textBoxDis.ItemsSource = App.Db.DisInfo.Select(m => m.NameDis).ToList();

            //if (textBoxUsl.SelectedIndex == -1)

            //    textBoxUsl.ItemsSource = App.Db.NameServices.Select(m => m.Usl).ToList();

            textBoxUsl.ItemsSource = null;

            textBoxUsl.ItemsSource = App.Db.NameServices.ToList();

			textBoxDis.ItemsSource = null;
			textBoxDis.ItemsSource = App.Db.DisInfo.ToList();	

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

		private async void textBoxDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UpdateFilteredRecords();

			if (textBoxDate.SelectedItem != null)
			{
				using (var context = new MyContext())
				{
					var personRecords = await LoadRecordsFromDatabaseAsync(context);

					var selectedDate = textBoxDate.SelectedItem.ToString();

					// Фильтруем записи для выбранной даты
					var recordsForSelectedDate = personRecords
						.Where(p => p.Date.Equals(selectedDate, StringComparison.OrdinalIgnoreCase))
						.ToList();

					// Получаем уникальных мастеров для выбранной даты
					var uniqueMasters = recordsForSelectedDate
						.Select(p => p.Master)
						.Distinct()
						.ToList();

					// Устанавливаем источник данных для textBoxMaster
					textBoxMaster.ItemsSource = uniqueMasters;
				}
			
			}
			
		}


		//запись клиента
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{

            dGrid.ItemsSource = null;
            dGrid.ItemsSource = App.Db.Persons.Local.ToBindingList();

            var mastSchedule = App.Db.MastSchedules.FirstOrDefault(m => m.Master == textBoxMaster.Text);

            if (mastSchedule != null)
            {
                mastSchedule.IsRegistered = false;             
			}


            dGrid.ItemsSource = null;
			dGrid.ItemsSource = App.Db.Persons.Local.ToBindingList();
			App.Db.Persons.Add(new Person
			{
				Name = textBoxName.Text,
				Date = textBoxDate.Text,
				Time = textBoxTime.Text,
				NameService = (NameService)textBoxUsl.SelectedItem,
				MastSchedule = App.Db.MastSchedules.FirstOrDefault(m => m.Master == textBoxMaster.Text),
				Material = (Material)Material.SelectedItem,
				Price1 = Price1.Text,
				Price2 = Price2.Text,
				Price = Price.Text,
				
			});
			MessageBox.Show("Пациент успешно зарегистрирован!");
			App.Db.SaveChanges();
		}	
	}
}
	



      

                
















