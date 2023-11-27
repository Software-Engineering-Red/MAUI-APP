
using UndacApp.ViewModels;

namespace UndacApp.Views;

public partial class ResourceRequestListPage : ContentPage
{
	public ResourceRequestListPage(PendingResourceRequestListViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}