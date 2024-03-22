using Celin.Services;
using Celin.Views;
using System.Windows.Input;

namespace Celin;

public partial class AppShell : Shell
{
    public ICommand LogoutCommand { get; }
    protected override async void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is not null)
        {
            await Navigation.PushModalAsync(new LoginPage(_host));
        }
    }
    readonly Host _host;
    public AppShell(Host host)
    {
        _host = host;
        BindingContext = this;

        LogoutCommand = new Command(() =>
        {
            _ = host.LogoutAsync();
            Navigation.PushModalAsync(new LoginPage(host));
        });

        InitializeComponent();
    }
}
