using System.Configuration;
using System.Data;
using System.Windows;

using System.Windows;
using ReservasApp.Views;

namespace ReservasApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            LoginWindow login = new LoginWindow();
            login.Show();
        }
    }
}
