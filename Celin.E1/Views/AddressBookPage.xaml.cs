namespace Celin.Views;

public partial class AddressBookPage : ContentPage
{
    public AddressBookPage(ViewModels.AddressBookViewModel search)
    {
        BindingContext = search;

        InitializeComponent();
    }
}