using System.ComponentModel.DataAnnotations;

namespace Celin.Views;

public partial class RequestPage : ContentPage
{
    public async Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> unitOfWork)
    {
        T? result = default;
        try
        {
             result = await unitOfWork(Cancellation.Token);
        }
        finally
        {
#if WINDOWS
	// Code smell dev.0041a1: Dirty hack for Windows to return focus to the MAUI main window by "Restoring" the window if it was minimised
	// and adding "Focus" if the window is not minimised but also out of focus.
	// TODO: Wait for MAUI bug fix then remove.
	var window = GetParentWindow().Handler.PlatformView as MauiWinUIWindow;
	var appWindow = GetAppWindow(window);
	switch (appWindow.Presenter)
        {
            case Microsoft.UI.Windowing.OverlappedPresenter overlappedPresenter:
                //overlappedPresenter.SetBorderAndTitleBar(false, false);
                overlappedPresenter.Restore();
                break;
        }

	Focus();
#endif
            await Navigation.PopModalAsync(animated: true); await Navigation.PopAsync();
        }
        return result;
    }
    private void cancel_Clicked(object sender, EventArgs e)
        => Cancellation.Cancel();
    [Required]
    public string Header { get; }
    public CancellationTokenSource Cancellation { get; }
        = new CancellationTokenSource();
#if WINDOWS
    // Code smell dev.0041a2: Dirty hack for Windows to return focus to the MAUI main window by "Restoring" the window if it was minimised
    // and adding "Focus" if the window is not minimised but also out of focus.
    // TODO: Wait for MAUI bug fix then remove.
    private Microsoft.UI.Windowing.AppWindow GetAppWindow(MauiWinUIWindow window)
    {
        var handle = WinRT.Interop.WindowNative.GetWindowHandle(window);
        var id = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(handle);
        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(id);
        return appWindow;
    }
#endif
    public RequestPage(string header)
    {
        Header = header;
        BindingContext = this;

        InitializeComponent();
    }
}