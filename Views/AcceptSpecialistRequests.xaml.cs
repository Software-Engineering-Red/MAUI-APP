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
	/// Database Service for Specialist Requests 
	/// </summary>
	private ISpecialistRequestService specialistRequestService;

	/// <summary>
	/// Database Service for Organisation Requests 
	/// </summary>
	private IOrganisationService organisationService;

	/// <summary>
	/// Observable Collection of SkillRequests
	/// </summary>
	public ObservableCollection<SkillRequest> SkillRequests { get; set; } = new ObservableCollection<SkillRequest>();

	/// <summary>
	/// Observable Collection of Organisations
	/// </summary>
	public ObservableCollection<Organisation> Organisations { get; set; } = new ObservableCollection<Organisation>();
	/// <summary>
	/// Constructor initialising Database Services and filling UI with values.
	/// </summary>
	public AcceptSpecialistRequests()
	{
		InitializeComponent();
		this.BindingContext = new SkillRequest();
		this.specialistRequestService = new SpecialistRequestService();
		this.organisationService = new OrganisationService();
		PopulateOrganisationPicker();
		Task.Run(async () => await LoadRequests());
	}

	/// <summary>
	/// Overides On Appearing Method to Update Bindings
	/// </summary>
	protected override void OnAppearing()
	{
		base.OnAppearing();
		PopulateOrganisationPicker();
		LoadRequests();
	}

	/// <summary>
	/// Adds All Requests to list and to ListView.
	/// </summary>
	private async Task LoadRequests()
	{
		try
		{
			SkillRequests = new ObservableCollection<SkillRequest>(await specialistRequestService.GetAll());
			SkillsRequestsListView.ItemsSource = SkillRequests;
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
				if (organisation == null)
				{
					await DisplayAlert("Error", $"Organisation not selected.", "OK");
					return;
				}
				if (CheckUpdateable(item))
				{
					await specialistRequestService.approveSkillRequest(item.ID, organisation);
					LoadRequests();

					await DisplayAlert("Success", $"Successfully inserted record into skills.", "OK");
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", $"Failed to approve Request. Error: {ex.Message}", "OK");
				return;
			}
		}
	}



	/// <summary>
	/// Deletes Skill Request when button is pushed.
	/// </summary>
	/// <param name="sender">Sender</param>
	/// <param name="e">Event</param>
	private async void OnButtonClickDelete(object sender, EventArgs e)
	{
		if (sender is Button button && button.BindingContext is SkillRequest item)
		{
			try
			{
				DeleteRecord(item.ID);
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", $"Failed to delete Request. Error: {ex.Message}", "OK");
				return;
			}
		}
	}

	/// <summary>
	/// Deletes Skill Request.
	/// </summary>
	/// <param name="item">Skill Request to be deleted</param>
	private async void DeleteRecord(int id)
	{
		var deleteBool = true;
		deleteBool = await DisplayAlert("Confirmation", "Are you sure you want to delete this request?", "Yes", "No");
		if (deleteBool)
		{
			await specialistRequestService.RemoveByID(id);
		}
		PopulateOrganisationPicker();
		LoadRequests();
	}


	/// <summary>
	/// Checks whether it is possible to update value.
	/// </summary>
	/// <param name="skillRequest">Skill Request</param>
	/// <returns></returns>
	private bool CheckUpdateable(SkillRequest skillRequest)
	{
		if (skillRequest == null)
			return false;
		if (skillRequest.Status == "Approved")
		{
			DisplayAlert("Not Possible", "The Request already has been approved.", "OK");
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
			Organisations = new ObservableCollection<Organisation>(await organisationService.GetOrganisationList());
			OrganisationPicker.ItemsSource = Organisations;
		}
		catch 
		{
			await DisplayAlert("Error", "Failed to load Organisations.", "OK");
		}
	}
}