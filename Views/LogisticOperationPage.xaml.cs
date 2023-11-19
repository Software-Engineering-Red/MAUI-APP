using System.Collections.ObjectModel;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.Views;
public partial class LogisticOperationPage : ContentPage
{

	private ILogisticsService logisticService;
    private ObservableCollection<LogisticsOperation> OperationList { get; set; } = new ObservableCollection<LogisticsOperation>();

    private LogisticsOperation? selectedOperation = null;

    public LogisticOperationPage()
	{
		InitializeComponent();
		BindingContext = this;

		this.logisticService = new LogisticsService();
    }

	protected async override void OnAppearing()
	{
		ClearInput();

        base.OnAppearing();
		await PopulateOperationTypes();
	}

	private void ClearInput()
	{
        OperationName.Text = String.Empty;
    }

	private void OnAddRecordClicked(object sender, EventArgs e)
	{
		AddNewOperationTypeRequest();
	}

	private void OnUpdateClicked(object sender, EventArgs e)
	{
		try
		{
            if (selectedOperation is null) return;

            if (String.IsNullOrEmpty(OperationName.Text)) return;

            selectedOperation.Name = OperationName.Text;
            logisticService.Update(selectedOperation);
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to update record. Error: {ex.Message}", "OK");
            return;
        }
    }

    private void OnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (selectedOperation is null) return;
            logisticService.Remove(selectedOperation);
			OperationList.Remove(selectedOperation);
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to remove record. Error: {ex.Message}", "OK");
            return;
        }

    }

    private void AddNewOperationTypeRequest()
	{
		try
		{
            if (String.IsNullOrEmpty(OperationName.Text)) return;

            LogisticsOperation op = new LogisticsOperation();
				
            op.Name = OperationName.Text;
		
			logisticService.Add(op);
			OperationList.Add(op);
            DisplayAlert("Success", $"Successfully inserted record.", "OK");
			
		}
		catch (Exception ex)
		{
			DisplayAlert("Error", $"Failed to insert record. Error: {ex.Message}", "OK");
			return;
		}
	}


	private async Task PopulateOperationTypes()
	{
		try
		{
            OperationList = new ObservableCollection<LogisticsOperation>(await logisticService.GetAll());
			ltv_operations.ItemsSource = OperationList;

        }
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Failed to load OperationTypeRequests. Error: {ex.Message}", "OK");
			return;
		}
	}

	private void ltv_operations_ItemSelected(object sender, EventArgs e)
	{
		selectedOperation = ltv_operations.SelectedItem as LogisticsOperation;
		if (selectedOperation is null) return;

		OperationName.Text = selectedOperation.Name;
	}

    private void UpdateFilter()
    {
		string? filterType = filterPicker.SelectedItem.ToString();
        string? selectedFilter = searchEntry.Text;
        if (OperationList.Count < 1 || String.IsNullOrEmpty(searchEntry.Text) 
			|| String.IsNullOrEmpty(filterType))
        {
			ltv_operations.ItemsSource = OperationList;
            return;
        }

        ltv_operations.ItemsSource = OperationList.Where(r => r.Name.ToLower().Contains(selectedFilter.ToLower()));
    }

	void OnEntryTextChanged(object sender, TextChangedEventArgs e)
	{
		UpdateFilter();
	}

   private void Button_Clicked(object sender, EventArgs e)
    {
        UpdateFilter();
    }
}
