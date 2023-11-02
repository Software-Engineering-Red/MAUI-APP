using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.Views;

/// <summary>
/// Logic to Approving Specialist Requests.
/// </summary>
public partial class AcceptSpecialistRequests : ContentPage
{

	/// <summary>
	/// database Service for Specialist Requests 
	/// </summary>
	private ISpecialistRequestService specialistRequestService;

	private IOrganisationService organisationService;

	/// <summary>
	/// List of SkillRequests
	/// </summary>
	private List<SkillRequest> _currentRecords;
	/// <summary>
	/// Constructor initialising Database Services and filling UI with values.
	/// </summary>
	public AcceptSpecialistRequests()
	{
		InitializeComponent();
		this.BindingContext = this;
		this.specialistRequestService = new SpecialistRequestService();
		this.organisationService = new OrganisationService();

		populateRequestList();
		PopulateOrganisationPicker();
	}

	/// <summary>
	/// Adds All Requests to list and to ListView.
	/// </summary>
	private async void populateRequestList()
	{
		try
		{
			_currentRecords = await this.specialistRequestService.GetSkillRequestListAsync();
			SkillsRequestsListView.ItemsSource = _currentRecords;
		}
		catch (Exception ex) 
		{
			await DisplayAlert("Error", $"Failed to load Requests. Error: {ex.Message}", "OK");
			return;
		}
	}

	/// <summary>
	/// Approves Request when button is pushed.
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Event</param>
	private async void OnButtonClickApprove(object sender, EventArgs e)
	{
		if (sender is Button button && button.BindingContext is SkillRequest item)
		{
			try
			{
				var organisationName = (string)OrganisationPicker.SelectedItem;
				var organisationId = GetOrganisationId(organisationName);
				if (CheckUpdateable(item.Status, item.OrganisationId, organisationId))
				{
					specialistRequestService.approveSkillRequest(item.Id, organisationId);
					await DisplayAlert("Success", $"Successfully inserted record into skills.", "OK");
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", $"Failed to approve Request. Error: {ex.Message}", "OK");
				return;
			}
		}
		RefreshRequests();
	}

	/// <summary>
	/// Checks whether it is possible to update value.
	/// </summary>
	/// <param name="status">Current Status</param>
	/// <param name="currentOrganisationId">Current OrganisationID</param>
	/// <param name="newOrganisationId">OrganisationID that should be entered</param>
	/// <returns>bool whether Updateable</returns>
	private bool CheckUpdateable(string status, int currentOrganisationId, int newOrganisationId)
	{
		if (status == "Approved")
		{
			DisplayAlert("Not Possible", "The Request already has been approved.", "OK");
			return false;
		}
		if (newOrganisationId == currentOrganisationId)
		{
			DisplayAlert("Not Possible", "The Request already was approved by this Organisation.", "OK");
			return false;
		}
		return true;
	}

	/// <summary>
	/// Gets (first) Organisation ID by its name.
	/// </summary>
	/// <param name="name">Organisation Name</param>
	/// <returns></returns>
	private int GetOrganisationId(string name)
	{
		var result = 1; //HAs to be fixed later 
		return result;
	}

	/// <summary>
	/// Adds Values to OrganisationPicker.
	/// </summary>
	private async void PopulateOrganisationPicker()
	{
		try
		{
			var organisations = await organisationService.GetOrganisationList();
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


	/// <summary>
	/// Refreshes the ListView of all Requests.
	/// </summary>
	private async void RefreshRequests()
	{
		try
		{
			_currentRecords = await specialistRequestService.GetSkillRequestListAsync();
			SkillsRequestsListView.ItemsSource = _currentRecords;
			populateRequestList();
		}
		catch (Exception ex)
		{

			DisplayAlert("Error", $"Failed to load records for Requests.", "OK");
		}
	}
}