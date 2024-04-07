using System.Diagnostics;

namespace Celin.Views;

public partial class AddressBookPage : ContentPage
{
    public AddressBookPage(ViewModels.AddressBookViewModel vm)
    {
        BindingContext = vm;

        InitializeComponent();

        searchHandler.PropertyChanged += (object? sender, System.ComponentModel.PropertyChangedEventArgs e)
            =>
        {
            Debug.WriteLine($"{e.PropertyName} = {vm.Query}");
        };
    }

}