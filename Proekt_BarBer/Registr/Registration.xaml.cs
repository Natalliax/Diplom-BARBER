using Proekt_BarBer.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proekt_BarBer.Registr
{
	/// <summary>
	/// Логика взаимодействия для Registration.xaml
	/// </summary>
	public partial class Registration : Window
	{
		MyContext db;

		public Registration()
		{
			InitializeComponent();
			db = new MyContext();
		}

		//private void rGrid_Loaded(object sender, RoutedEventArgs e)
		//{
		//	SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
		//	sqlConnection.Open();

		//}

		public void RegButton_Click(object sender, RoutedEventArgs e)
		{
			string login = textBoxLogin.Text.Trim();
			string pass = textBoxPass.Password.Trim();
			string pass2 = textBoxPass2.Password.Trim();
			string email = textBoxEmail.Text.ToLower();

			if (login.Length < 5)
			{
				textBoxLogin.ToolTip = "Это поле введено не корректно";
				textBoxLogin.Background = Brushes.Beige;
			}
			else if (pass.Length < 5)
			{
				textBoxPass.ToolTip = "Это поле введено не корректно";
				textBoxPass.Background = Brushes.Beige;
			}
			else if (pass != pass2)
			{
				textBoxPass2.ToolTip = "Это поле введено не корректно";
				textBoxPass2.Background = Brushes.Beige;
			}
			else if (email.Length < 5 || !email.Contains("@") || !email.Contains("."))
			{
				textBoxEmail.ToolTip = "Это поле введено не корректно";
				textBoxEmail.Background = Brushes.Beige;
			}
			else
			{
				textBoxLogin.ToolTip = "";
				textBoxLogin.Background = Brushes.Beige;
				textBoxPass.ToolTip = "";
				textBoxPass.Background = Brushes.Beige;
				textBoxPass2.ToolTip = "";
				textBoxPass2.Background = Brushes.Beige;
				textBoxEmail.ToolTip = "";
				textBoxEmail.Background = Brushes.Beige;

				MessageBox.Show("Регистрация прошла успешно");
				Close();
				
				Users users = new Users(login, pass, email);
				db.Users.Add(users);
				db.SaveChanges();
			}

		}
	}
}
