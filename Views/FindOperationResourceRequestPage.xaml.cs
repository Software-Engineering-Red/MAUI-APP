namespace UndacApp.Views;
using UndacApp.ViewModels;

public partial class FindOperationResourceRequestPage : ContentPage
{
	private readonly FindOperationResourceRequestViewModel viewModel;
	public FindOperationResourceRequestPage()
	{
		this.viewModel = new FindOperationResourceRequestViewModel();
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		if (e.SelectedItem is HighlightedOperation highlightedOperation)
		{
			if (BindingContext is FindOperationResourceRequestViewModel viewModel)
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