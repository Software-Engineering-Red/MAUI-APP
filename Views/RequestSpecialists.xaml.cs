using System.Collections.ObjectModel;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.Views;

/// <summary>
/// Logic to creating new Specialist Requests.
/// </summary>
public partial class RequestSpecialists : ContentPage
{
	private ISpecialistRequestService specialistRequestService;
	private ISkillService skillService;
	private ObservableCollection<Skill> Skills { get; set; } = new ObservableCollection<Skill>();

	/// <summary>
	/// Constructor initialising Database Services and filling UI with values.
	/// </summary>
	public RequestSpecialists()
	{
		InitializeComponent();
		BindingContext = this;

		specialistRequestService = new SpecialistRequestService();
		this.skillService = new SkillService();
		PopulateSkillPicker();
	}

	/// <summary>
	/// Overides On Appearing Method to Update Bindings
	/// </summary>
	protected override void OnAppearing()
	{
		base.OnAppearing();
		Task.Run(async () => await PopulateSkillPicker());
	}


	/// <summary>
	/// Reacts to a button push calls methods to initialize steps
	/// to Add a new Record, if that is possible.
	/// </summary>
	/// <param name="sender">Button</param>
	/// <param name="e">Event</param>
	private void OnAddRecordClicked(object sender, EventArgs e)
	{
		var numberRequired = GetNumberRequired();
		var skillId = GetSkillId();
		var startDate = StartDatePicker.Date;
		var endDate = EndDatePicker.Date;
		AddNewSkillRequest(skillId, numberRequired, startDate, endDate);
	}

	/// <summary>
	/// Adds a new Skill Request Record if that is possible.
	/// </summary>
	/// <param name="skillId"></param>
	/// <param name="numberRequired"></param>
	/// <param name="startDate"></param>
	/// <param name="endDate"></param>
	private void AddNewSkillRequest(int skillId, int numberRequired, DateTime startDate, DateTime endDate)
	{
		if (CheckAddable(skillId, numberRequired))
		{
			try
			{
				specialistRequestService.AddUnapprovedSkillRequest(skillId, numberRequired,
					startDate, endDate);
				DisplayAlert("Success", $"Successfully inserted record into skills.", "OK");
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", $"Failed to insert record into skills. Error: {ex.Message}", "OK");
				return;
			}
		}
	}

	/// <summary>
	/// Returns the Number (of Helpers) Required, if that is possible.
	/// </summary>
	/// <returns>Number (of Helpers) Required</returns>
	private int GetNumberRequired()
	{
		try
		{
			return Convert.ToInt32(NumberRequiredEntry.Text);
		}
		catch 
		{
			DisplayAlert("Error", $"Failed to insert record into skills. Error: Number of Helpers required is not a number", "OK");
			NumberRequiredEntry.Text = "";
			return 0;
		}
	}

	/// <summary>
	/// Returns the Id of a certain Skill, if that is possible.
	/// </summary>
	/// <returns>Skill ID</returns>
	private int GetSkillId()
	{
		try
		{
			var skill = (Skill)SkillNamePicker.SelectedItem;
			if (skill == null)
			{
				DisplayAlert("Error", $"Failed to insert record into skills. Error: Skill not found", "OK");
				return 0;
			}
			return skill.ID;
		}
		catch
		{
			DisplayAlert("Error", $"Failed to insert record into skills. Error: Skill not found", "OK");
			NumberRequiredEntry.Text = "";
			return 0;
		}
	}

	/// <summary>
	/// Checks whether a new Skill Request, can be added or whether there are missing vaues.
	/// </summary>
	/// <param name="skillId">Skill Id</param>
	/// <param name="numberRequired">Number of requird Persons</param>
	/// <returns></returns>
	private  bool CheckAddable(int skillId, int numberRequired)
	{
		if (skillId == 0)
		{
			DisplayAlert("Error", $"No Skill entered", "OK");
			return false;
		}
		if (numberRequired <= 0)
		{
			DisplayAlert("Error", $"Number of requested Helpers requires to be bigger than Zero", "OK");
			return false;
		}
		return true;
	}

	/// <summary>
	/// Adds the skills to chose from to SkillPicker.
	/// </summary>
	private async Task PopulateSkillPicker()
	{
		try
		{
			Skills = new ObservableCollection<Skill>(await skillService.GetAll());
			SkillNamePicker.ItemsSource = Skills;
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Failed to load SkillRequests. Error: {ex.Message}", "OK");
			return;
		}
	}

	/// <summary>
	/// Always sets the Minimum of endDate to the current startDate.
	/// </summary>
	/// <param name="sender">Datepicker</param>
	/// <param name="e">Event</param>
	private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
	{
		EndDatePicker.MinimumDate = e.NewDate;
	}

}