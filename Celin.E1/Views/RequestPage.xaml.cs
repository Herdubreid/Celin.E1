using System.ComponentModel.DataAnnotations;

namespace Celin.Views;

public partial class RequestPage : ContentPage
{
    public async Task<bool> ExecuteAsync(Func<CancellationToken, Task> unitOfWork)
    {
        try
        {
            await unitOfWork(Cancellation.Token);
            return true;
        }
        catch (OperationCanceledException) { }

        return false;
    }
    private void cancel_Clicked(object sender, EventArgs e)
    {
        Cancellation.Cancel();
    }
    [Required]
    public string Header { get; }
    public CancellationTokenSource Cancellation { get; }
        = new CancellationTokenSource();
    public RequestPage(string header)
    {
        Header = header;
        BindingContext = this;

        InitializeComponent();
    }
}