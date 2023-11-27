namespace UndacApp.Views;
using UndacApp.ViewModels;

public partial class OperationPage : ContentPage
{
	public OperationPage()
	{
		InitializeComponent();
		BindingContext = new OperationViewModel();
	}
}