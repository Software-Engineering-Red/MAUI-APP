
using UndacApp.ViewModels;

namespace UndacApp.Views;

public partial class ResourceRequestListPage : ContentPage
{
	public ResourceRequestListPage(RequestListViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}