namespace UndacApp.Views;
using UndacApp.ViewModels;

public partial class FindOperationResourceRequestPage : ContentPage
{
	public FindOperationResourceRequestPage()
	{
		InitializeComponent();
		BindingContext = new FindOperationResourceRequestViewModel();
	}
}