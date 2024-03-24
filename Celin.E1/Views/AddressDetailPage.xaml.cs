namespace Celin.Views;

public partial class AddressDetailPage : ContentPage
{
    public Models.W01012A.Data Data { get; }
    public AddressDetailPage(Models.W01012A.Data data)
	{
        Data = data;
		BindingContext = this;

		InitializeComponent();
	}
}