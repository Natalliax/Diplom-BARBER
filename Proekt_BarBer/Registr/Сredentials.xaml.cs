using Proekt_BarBer.Core;
using Proekt_BarBer.Registr;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Proekt_BarBer
{
	/// <summary>
	/// Логика взаимодействия для credentials.xaml
	/// </summary>
	public partial class Сredentials : Window
	{
		public Сredentials()
		{
			InitializeComponent();
		}

		private void CredButton_Click(object sender, RoutedEventArgs e)
		{
			Registration registration = new Registration();
			registration.Show();
		}

        private void AutButton_Click(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string pass = textBoxPass.Password.Trim();




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
            else
            {
                textBoxLogin.ToolTip = "";
                textBoxLogin.Background = Brushes.Beige;
                textBoxPass.ToolTip = "";
                textBoxPass.Background = Brushes.Beige;

                Users AutUser = null;
                using (MyContext db = new MyContext())
                {
                    AutUser = db.Users.Where(b => b.login == login && b.pass == pass).FirstOrDefault();
                }

                if (AutUser != null)
                {
                    MessageBox.Show("Пользователь найден");
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Пользователь не найден");
                }
            }
            //MainWindow mainWindow = new MainWindow();
            //	this.Close();
            ////mainWindow.ShowDialog();

        }

        

        private void textBoxPass_PasswordChanged(object sender, TextChangedEventArgs e)
        {


            Button2.IsEnabled = !string.IsNullOrEmpty(textBoxPass.Password);


        }

        private void textBoxLogin_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            Button2.IsEnabled = !string.IsNullOrEmpty(textBoxLogin.Text) || !string.IsNullOrEmpty(textBoxPass.Password);
        }
    }
}

