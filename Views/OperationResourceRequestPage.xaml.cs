namespace UndacApp.Views;
using UndacApp.ViewModels;

public partial class OperationRessourceRequestPage : ContentPage
{
	public OperationRessourceRequestPage()
	{
		InitializeComponent();

		BindingContext = new OperationResourceRequestViewModel();
	}
}