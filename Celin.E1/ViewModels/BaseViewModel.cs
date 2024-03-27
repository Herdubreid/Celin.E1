using Celin.AIS;
using Celin.Services;
using Celin.Views;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Celin.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    protected bool HandleException(AisException exception)
    {
        if (exception.HttpStatusCode == System.Net.HttpStatusCode.Forbidden ||
            exception.HttpStatusCode == (System.Net.HttpStatusCode)444)
        {
            Shell.Current.Navigation.PushModalAsync(new LoginPage(_host));
            return true;
        }
        return false;
    }
    protected readonly Host _host;
    public BaseViewModel(Host host)
    {
        _host = host;
    }
}
