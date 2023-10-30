using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.Views;

public partial class AcceptSpecialistRequests : ContentPage
{
	private readonly DatabaseOperations _dbOps;
	private ISpecialistRequestService specialistRequestService;
	private List<SkillRequest> _currentRecords;

	public AcceptSpecialistRequests()
	{
		InitializeComponent();

		_dbOps = new DatabaseOperations($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reference_values.sqlite")}");
		specialistRequestService = new SpecialistRequestService($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reference_values.sqlite")}");
		populateRequestList();
		PopulateOrganisationPicker();
	}

	private async void populateRequestList()
	{
		try
		{
			_currentRecords = specialistRequestService.GetAllSkillRequests();
			SkillsRequestsListView.ItemsSource = _currentRecords;
		}
		catch (Exception ex) 
		{
			await DisplayAlert("Error", $"Failed to load Requests. Error: {ex.Message}", "OK");
			return;
		}
	}


	private async void OnButtonClick(object sender, EventArgs e)
	{
		if (sender is Button button && button.BindingContext is SkillRequest item)
		{
			try
			{
				var OrganisationName = (string)OrganisationPicker.SelectedItem;
				specialistRequestService.approveSkillRequest(item.Id, GetOrganisationId(OrganisationName));
				await DisplayAlert("Success", $"Successfully inserted record into skills.", "OK");
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", $"Failed to approve Request. Error: {ex.Message}", "OK");
				return;
			}
		}
		RefreshRecordsList();
	}


	private int GetOrganisationId(string name)
	{
		var result = _dbOps.GetRecordIdByName("organisation", name);
		return result;
	}

	private void PopulateOrganisationPicker()
	{
		try
		{
			var organisations = _dbOps.GetAllRecords("organisation");
			foreach (var organisation in organisations)
			{
				Console.WriteLine(organisation);
			}
			OrganisationPicker.ItemsSource = organisations;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			DisplayAlert("Error", "Failed to load Organisations.", "OK");
		}
	}



	private void OnRequestSelected(object sender, EventArgs e)
	{
		try
		{
			_currentRecords = specialistRequestService.GetAllSkillRequests();
			SkillsRequestsListView.ItemsSource = _currentRecords;
			populateRequestList();
		}
		catch (Exception ex)
		{

			DisplayAlert("Error", $"Failed to load records for Requests.", "OK");
		}
	}
	private void RefreshRecordsList() => OnRequestSelected(null, EventArgs.Empty);
}