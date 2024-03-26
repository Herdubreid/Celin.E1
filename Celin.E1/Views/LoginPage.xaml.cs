using Celin.AIS;
using Celin.Services;
using CommunityToolkit.Maui.Behaviors;

namespace Celin.Views;

public partial class LoginPage : ContentPage
{
    public string Username { get; set; }
    public string? Password { get; set; }
    string? _message;
    public string Message
    {
        get => _message ?? string.Empty;
        set
        {
            _message = value;
            OnPropertyChanged(nameof(Message));
        }
    }
    private async void login_Clicked(object sender, EventArgs e)
    {
        bool success = true;
        foreach (Entry ctrl in form.Children.Where(x => x is Entry))
        {
            var bhvr = ctrl.Behaviors
                .OfType<TextValidationBehavior>()
                .FirstOrDefault();

            if (bhvr != null)
            {
                await bhvr.ForceValidate();
                success = success && bhvr.IsValid;
            }
        }
        if (success)
        {
            Message = string.Empty;
            _host.AuthRequest.username = Username;
            _host.AuthRequest.password = Password;

            var dlg = new RequestPage("Authenticating...");
            await Navigation.PushModalAsync(dlg);
            try
            {
                await dlg.ExecuteAsync(_host.AuthenticateAsync);

                await Navigation.PopModalAsync();
            }
            catch (AisException ex)
            {
                Message = ex?.ErrorResponse?.message ?? ex?.Message ?? "Unknown Error";
            }
            finally
            {
                await Navigation.PopModalAsync();
            }
        }
        else
        {
            Message = "Form is invalid!";
        }
    }
    readonly Host _host;
    public LoginPage(Host host)
    {
        _host = host;
        Username = host.AuthRequest.username;
        BindingContext = this;

        InitializeComponent();
    }
}