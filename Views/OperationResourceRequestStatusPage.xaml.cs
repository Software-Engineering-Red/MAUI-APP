namespace UndacApp.Views;

using UndacApp.ViewModels;

public partial class OperationResourceRequestStatusPage : ContentPage
{
	public OperationResourceRequestStatusPage()
	{
		InitializeComponent();

		BindingContext = new OperationResourceRequestStatusViewModel();
	}
}