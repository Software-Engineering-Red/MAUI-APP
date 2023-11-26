namespace UndacApp.Views;
using UndacApp.ViewModels;

public partial class FindOperationResourceRequestPage : ContentPage
{
	public FindOperationResourceRequestPage()
	{
		InitializeComponent();
		BindingContext = new FindOperationResourceRequestViewModel();
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
}