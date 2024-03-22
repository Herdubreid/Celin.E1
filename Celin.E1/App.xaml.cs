using Celin.Services;

namespace Celin;

public partial class App : Application
{
    public App(Host host)
    {
        InitializeComponent();

        MainPage = new AppShell(host);
    }
}
