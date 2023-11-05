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
	/// Creates new Request when button is pushed.
	/// </summary>
	/// <param name="sender">Button</param>
	/// <param name="e">Event</param>
	private async void OnAddRecordClicked(object sender, EventArgs e)
	{
		Skill skill;
		var numberRequired = 0;
		try
		{
			numberRequired = Convert.ToInt32(NumberRequiredEntry.Text);
		}
		catch 
		{
			await DisplayAlert("Error", $"Failed to insert record into skills. Error: NumberRequired not a number", "OK");
			NumberRequiredEntry.Text = "";
			return;
		}
		try
		{
			skill = (Skill)SkillNamePicker.SelectedItem;
		}
		catch
		{
			await DisplayAlert("Error", $"Failed to insert record into skills. Error: Skill not found", "OK");
			NumberRequiredEntry.Text = "";
			return;
		}
		var startDate = StartDatePicker.Date;
		var endDate = EndDatePicker.Date;
		this.AddSkillRequest(skill.ID, numberRequired, startDate, endDate);
	}

	private async void AddSkillRequest(int skillId, int numberRequired, DateTime startDate, DateTime endDate)
	{
		if (CheckAddable(skillId, numberRequired))
		{
			try
			{
				specialistRequestService.AddUnapprovedSkillRequest(skillId, numberRequired,
					startDate, endDate);
				await DisplayAlert("Success", $"Successfully inserted record into skills.", "OK");
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", $"Failed to insert record into skills. Error: {ex.Message}", "OK");
				return;
			}
		}
	}

	/// <summary>
	/// Checks whether something can be added or if there are missing vaues.
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
		Skills = new ObservableCollection<Skill>(await skillService.GetSkillList());
		SkillNamePicker.ItemsSource = Skills;
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