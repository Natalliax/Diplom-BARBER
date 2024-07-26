using Microsoft.Office.Interop.Excel;
using Proekt_BarBer.Core;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using Window = System.Windows.Window;


namespace Proekt_BarBer
{
    /// <summary>
    /// Логика взаимодействия для Report.xaml
    /// </summary>
    public partial class Report : Window
    {
        
        public Report()
        {
            InitializeComponent();
            Loaded+= OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            ns.Items.Add(new NameService());
            foreach (NameService nameService in App.Db.NameServices) ns.Items.Add(nameService);

            mat.Items.Add(new Material());
            foreach (Material material in App.Db.Materials) mat.Items.Add(material);

            ms.Items.Add(new MastSchedule());
            foreach (MastSchedule mastSchedule in App.Db.MastSchedules) ms.Items.Add(mastSchedule);


            FiltrationContext();
        }
        private void DateFilterChanged(object sender, SelectionChangedEventArgs e) => FiltrationContext();
        private void textFilterChanged(object sender, TextChangedEventArgs e)=>FiltrationContext();
        private void comboFilterChanges(object sender, SelectionChangedEventArgs e)=>FiltrationContext();
        private void Close_Click(object sender, RoutedEventArgs e) => Close();
        private void FiltrationContext()
        {
            dGrid.ItemsSource = null;
            dGrid.ItemsSource = App.Db.Persons.Where(Filter).ToList();
        }
        private NameService _ns => (NameService)ns.SelectedItem;
        
        private Material _mat => (Material)this.mat.SelectedItem;
        private MastSchedule _ms => (MastSchedule)ms.SelectedItem;
        private bool Filter(Person p)
        {
            if (p == null) return false;

            DateTime? startDate = startDatePicker.SelectedDate;
            DateTime? endDate = endDatePicker.SelectedDate;

            if (startDate.HasValue && endDate.HasValue)
            {
                DateTime personDate;
                if (!DateTime.TryParse(p.Date, out personDate) || personDate < startDate.Value || personDate > endDate.Value)
                {
                    return false;
                }
            }

            return !((!string.IsNullOrWhiteSpace(dt.Text) && p.Date != null && !p.Date.Trim().Contains(dt.Text.Trim())) ||
                     (!string.IsNullOrWhiteSpace(tm.Text) && p.Time != null && !p.Time.Trim().Contains(tm.Text.Trim())) ||
                     (!string.IsNullOrWhiteSpace(person.Text) && p.Name != null && !p.Name.Trim().Contains(person.Text.Trim())) ||
                     (_ns != null && ns.SelectedIndex > 0 && p.NameService?.Id != _ns.Id) ||
                     (_mat != null && mat.SelectedIndex > 0 && p.Material?.Id != _mat.Id) ||
                     (_ms != null && ms.SelectedIndex > 0 && p.MastSchedule?.Id != _ms.Id));
        }

            private void Button_Click(object sender, RoutedEventArgs e)
		{
            Close();
		}

        private void ButtonExp_Click(object sender, RoutedEventArgs e)
        {
            dynamic excelApp = Activator.CreateInstance(Type.GetTypeFromProgID("Excel.Application"));
            var workbook = excelApp.Workbooks.Add();
            var sheet = workbook.Sheets[1];



            // Добавляем заголовок с диапазоном дат
            string startDate = startDatePicker.SelectedDate.HasValue ? startDatePicker.SelectedDate.Value.ToString("dd.MM.yyyy") : "не указана";
            string endDate = endDatePicker.SelectedDate.HasValue ? endDatePicker.SelectedDate.Value.ToString("dd.MM.yyyy") : "не указана";
            sheet.Cells[1, 1].Value2 = $"Отчет за период: {startDate} - {endDate}";
            sheet.Cells[1, 1].Font.Bold = true;
            sheet.Cells[1, 1].Interior.Color = System.Drawing.Color.Yellow;

            // Заполняем данными из DataGrid
            for (int i = 0; i < dGrid.Columns.Count; i++)
            {
                sheet.Cells[3, i + 1].Value2 = dGrid.Columns[i].Header;
                for (int j = 0; j < dGrid.Items.Count; j++)
                {
                    TextBlock cellContent = dGrid.Columns[i].GetCellContent(dGrid.Items[j]) as TextBlock;
                    sheet.Cells[j + 4, i + 1].Value2 = cellContent?.Text ?? "-";
                }
            }

            // Настройка ширины столбцов
            for (int i = 1; i <= 6; i++)
            {
                sheet.Columns[i].ColumnWidth = 15;
            }

            // Центрирование текста в первой строке
            sheet.Cells[3, 1].HorizontalAlignment = XlHAlign.xlHAlignCenter;
            sheet.Cells[3, 1].Font.Size = 12;

            excelApp.Visible = true;

        }
        }
}



