using Proekt_BarBer.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Proekt_BarBer
{
   

    /// <summary>
    /// Логика взаимодействия для Zarplata.xaml
    /// </summary>
    public partial class Zarplata : Window
    {

       

        public async Task<List<Person>> LoadPersonRecordsFromDatabaseAsync(MyContext context)
        {
            var records = await context.Persons.ToListAsync();
            return records;
        }

        public Zarplata()
        {
            InitializeComponent();
            Master.ItemsSource = App.Db.Persons.Select(p => p.MastSchedule.Master).Distinct().ToList(); //comboBox - заполнение данными (список мастеров без повторений)
            Procent1.ItemsSource = App.Db.DisInfo.ToList();
            Procent2.ItemsSource = App.Db.DisInfo.ToList();

        }
        private void Date1_TextChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void Date2_TextChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonCl_Click(object sender, RoutedEventArgs e)
        {
            if (Master.SelectedIndex >= -1)

                Master.ItemsSource = App.Db.Persons.Select(m => m.MastSchedule.Master).Distinct().ToList();

            Date1.Text = "";
            Date2.Text = "";
            Master.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            Procent1.Text = "";
            Procent2.Text = "";
            Proc1.Text = "";
            Proc2.Text = "";
        }
        private void ButtonRas_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(textBox2.Text, out decimal pr1) && !string.IsNullOrEmpty(textBox4.Text))
            {
                if (decimal.TryParse(textBox4.Text, out decimal pr2))
                {
                    textBox5.Text = (pr1 + pr2).ToString();
                }
                else if (decimal.TryParse(textBox2.Text, out decimal price1))
                {
                    textBox5.Text = price1.ToString();
                }
            }
        }

        private void ButtonOt_Click(object sender, RoutedEventArgs e)
        {
                App.Db.Cals.AddOrUpdate(new Cal
                {
                    Date1 = Date1.Text,
                    Date2 = Date2.Text,
                    Master = Master.Text,
                    Procent1 = Proc1.Text,
                    Summa1 = textBox1.Text,
                    Procent2 = Proc2.Text,
                    Summa2 = textBox3.Text,
                    Total = textBox5.Text
                });

                MessageBox.Show("Документ записан");
                App.Db.SaveChanges();
                Close();
        }
        private void Procent1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Procent1.SelectedItem is DiscountInfo dis)
            {
                Proc1.Text = dis.Persent.ToString("#0%");

                if (textBox1.Text != null)
                {
                    textBox2.Text = (decimal.Parse(textBox1.Text) * dis.Persent).ToString("#0.00");
                }
            }
        }
        private void Procent2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Procent2.SelectedItem is DiscountInfo dis)
            {
                Proc2.Text = dis.Persent.ToString("#0%");

                if (textBox3.Text != null)
                {
                    textBox4.Text = (decimal.Parse(textBox3.Text) * dis.Persent).ToString("#0.00");
                }
            }
        }


        private async void Master_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Date1.Text) || string.IsNullOrEmpty(Date2.Text))
            {
                // Даты не введены
                MessageBox.Show("Введите нужный период.");
                return;
            }

            if (Master.SelectedItem != null)
            {
                using (var context = new MyContext())
                {
                    var personRecords = await LoadPersonRecordsFromDatabaseAsync(context);

                    // Преобразуем строки в объекты DateTime с проверкой на успешное преобразование
                    if (DateTime.TryParseExact(Date1.Text, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date1) &&
                        DateTime.TryParseExact(Date2.Text, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date2))
                    {
                        // Получение диапазонов дат из таблицы Cal
                        var calRecords = context.Cals.ToList();

                        foreach (var calRecord in calRecords)
                        {
                            DateTime rangeStart = DateTime.Parse(calRecord.Date1.ToString());
                            DateTime rangeEnd = DateTime.Parse(calRecord.Date2.ToString());

                            // Проверка, пересекаются ли диапазоны дат
                            if (date1 <= rangeEnd && date2 >= rangeStart && calRecord.Master == Master.Text.ToString())
                            {
                                MessageBox.Show("Целевая дата находится в указанном диапазоне");
                                return;
                            }
                        }

                        // Фильтруем записи по мастеру и датам
                        var filteredRecords = personRecords
                            .Where(p => DateTime.Parse(p.Date) >= date1 && DateTime.Parse(p.Date) <= date2 &&
                                        p.MastSchedule.Master.Equals(Master.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();

                        if (filteredRecords.Any())
                        {
                            decimal totalCost = 0;
                            foreach (var record in filteredRecords)
                            {
                                if (decimal.TryParse(record.Price1, out decimal price))
                                {
                                    totalCost += price;
                                }
                            }
                            textBox1.Text = totalCost.ToString();
                        }
                        else
                        {
                            textBox1.Text = "0";
                            MessageBox.Show("За указанный период у мастера нет данных.");
                        }
                    }
                
            
                        }
                    }
                            if (Master.SelectedItem != null)
                {
                    using (var context = new MyContext())
                    {
                        var personRecords = await LoadPersonRecordsFromDatabaseAsync(context);

                        string dateString1 = Date1.Text;
                        string dateString2 = Date2.Text;
                        string dateFormat = "dd.MM.yyyy";

                        if (DateTime.TryParseExact(dateString1, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date1) &&
                            DateTime.TryParseExact(dateString2, dateFormat, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime date2))
                        {


                        // Фильтруем записи по мастеру и датам
                        var filteredRecords = personRecords
                            .Where(p => DateTime.Parse(p.Date) >= date1 && DateTime.Parse(p.Date) <= date2 &&
                           p.MastSchedule.Master.Equals(Master.SelectedItem.ToString(), StringComparison.OrdinalIgnoreCase)).ToList();

                        if (filteredRecords.Any())
                            {
                                decimal totalCost = 0;
                                foreach (var record in filteredRecords)
                                {
                                    if (decimal.TryParse(record.Price2, out decimal price))
                                    {
                                        totalCost += price;
                                    }
                                }
                                textBox3.Text = totalCost.ToString();
                            }
                        }
                    }
                }
            }
        

        private void Proc1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    private void Proc2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }  
    }
}
    


