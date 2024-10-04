using UserManagar.Presentation;

namespace UserManagar;

public partial class App : System.Windows.Application
{
    private void Application_Startup(object sender, System.Windows.StartupEventArgs e)
    {
        new MainWindow().Show();
    }
}
