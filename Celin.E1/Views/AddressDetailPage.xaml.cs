using System.Windows.Input;

namespace Celin.Views;

public partial class AddressDetailPage : ContentPage
{
	public ICommand EditCommand { get; }
	bool _editMode;
	public bool EditMode
	{
		get => _editMode;
		set
		{
			_editMode = value;
			OnPropertyChanged();
		}
	}
	public bool IsReadOnly { get => !_editMode; }
    public Models.W01012A.Data Data { get; }
    public AddressDetailPage(Models.W01012A.Data data)
	{
        Data = data;

		EditCommand = new Command(
			() => EditMode = true,
			() => !EditMode);

		BindingContext = this;

		InitializeComponent();
	}
}