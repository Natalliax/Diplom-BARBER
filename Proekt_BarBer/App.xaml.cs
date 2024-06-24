using Proekt_BarBer.Core;
using System.Windows;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proekt_BarBer
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			
			
		}

		static internal MyContext Db = null;
		


		public App()
        {
            Db = new MyContext();
        }
    }
}
