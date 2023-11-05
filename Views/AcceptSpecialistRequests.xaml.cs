using System.Collections.ObjectModel;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.Views;

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
	public ObservableCollection<SkillRequest> Skills { get; set; } = new ObservableCollection<SkillRequest>();

	public ObservableCollection<Organisation> Organisations { get; set; } = new ObservableCollection<Organisation>();
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
	private async Task populateRequestList()
	{
		try
		{
			Skills = new ObservableCollection<SkillRequest>(await specialistRequestService.GetSkillRequestListAsync());
			SkillsRequestsListView.ItemsSource = Skills;
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
				var organisation = OrganisationPicker.SelectedItem as Organisation;
				if (CheckUpdateable(item.Status, item.OrganisationId, organisation.id))
				{
					specialistRequestService.approveSkillRequest(item.Id, organisation);
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
			populateRequestList();
		}
		catch (Exception ex)
		{

			DisplayAlert("Error", $"Failed to load records for Requests.", "OK");
		}
	}
}