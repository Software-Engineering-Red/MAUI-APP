namespace UndacApp.Views;
using UndacApp.ViewModels;

public partial class FindOperationResourceRequestPage : ContentPage
{
	private readonly FindPendingOperationResourceRequestViewModel viewModel;
	public FindOperationResourceRequestPage()
	{
		this.viewModel = new FindPendingOperationResourceRequestViewModel();
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem is HighlightedOperation highlightedOperation)
		{
			if (BindingContext is FindPendingOperationResourceRequestViewModel viewModel)
			{
				viewModel.ViewRequestsCommand.Execute(null);
			}
		}
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Task.Run(() => viewModel.ApplyFilter());
	}
}