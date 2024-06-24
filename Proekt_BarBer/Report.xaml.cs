using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Proekt_BarBer.Core;
using System;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Policy;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using ZstdSharp.Unsafe;
using MessageBox = System.Windows.MessageBox;
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

        private void textFilterChanged(object sender, TextChangedEventArgs e)=>FiltrationContext();
        private void comboFilterChanges(object sender, SelectionChangedEventArgs e)=>FiltrationContext();
        private void Close_Click(object sender, RoutedEventArgs e) => Close();
        private void FiltrationContext()
        {
            dGrid.ItemsSource= null;
            dGrid.ItemsSource= App.Db.Persons.Where(Filter).ToList();
        }
        private NameService _ns => (NameService)ns.SelectedItem;
        
        private Material _mat => (Material)this.mat.SelectedItem;
        private MastSchedule _ms => (MastSchedule)ms.SelectedItem;
        private bool Filter(Person p)
        {
            if (p == null) return false;
            return !((!string.IsNullOrWhiteSpace(dt.Text) && p.Date != null && !p.Date.Trim().Contains(dt.Text.Trim())) ||
                   (!string.IsNullOrWhiteSpace(tm.Text) && p.Time != null && !p.Time.Trim().Contains(tm.Text.Trim())) ||
                   (!string.IsNullOrWhiteSpace(person.Text) && p.Name != null && !p.Name.Trim().Contains(person.Text.Trim())) ||
                   //(!string.IsNullOrWhiteSpace(ms.Text) && p.MastSchedule != null && !p.MastSchedule.Trim().Contains(ms.Text.Trim())) ||
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
            sheet.Cells[1, 3].Interior.Color = System.Drawing.Color.Yellow; // Желтый фон для заголовка
            sheet.Cells[1, 3].Value2 = "Выборочная ведомость"; // Заголовок таблицы
            sheet.Columns[1].ColumnWidth = 15; // Установите ширину  столбца (в символах)
            sheet.Columns[2].ColumnWidth = 15; // Установите ширину  столбца (в символах)
            sheet.Columns[3].ColumnWidth = 15; // Установите ширину столбца (в символах)
            sheet.Columns[4].ColumnWidth = 15; // Установите ширину  столбца (в символах)
            sheet.Columns[5].ColumnWidth = 15; // Установите ширину  столбца (в символах)
            sheet.Columns[6].ColumnWidth = 15; // Установите ширину  столбца (в символах)

            sheet.Cells[2, 1].HorizontalAlignment = XlHAlign.xlHAlignCenter; // Центрирование текста в первой 
            sheet.Cells[2, 1].Font.Size = 12; // Установите размер шрифта в 12 пунктов

            excelApp.Visible = true;
        }
    }
}



