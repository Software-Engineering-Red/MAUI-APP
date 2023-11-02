using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.Views;

/// <summary>
/// Logic to creating new Specialist Requests.
/// </summary>
public partial class RequestSpecialists : ContentPage
{

	/// <summary>
	/// database Service for Specialist Requests .
	/// </summary>
	private ISpecialistRequestService specialistRequestService;
	private ISkillService skillService;

	/// <summary>
	/// Constructor initialising Database Services and filling UI with values.
	/// </summary>
	public RequestSpecialists()
	{
		InitializeComponent();

		specialistRequestService = new SpecialistRequestService();
		skillService = new SkillService();	
		PopulateSkillPicker();
	}


	/// <summary>
	/// Creates new Request when button is pushed.
	/// </summary>
	/// <param name="sender">Button</param>
	/// <param name="e">Event</param>
	private async void OnAddRecordClicked(object sender, EventArgs e)
	{
		var skillName = (string)SkillNamePicker.SelectedItem;
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
		var startDate = StartDatePicker.Date;
		var endDate = EndDatePicker.Date;
		if (CheckAddable(skillName, numberRequired))
		{
			try
			{
				specialistRequestService.AddUnapprovedSkillRequest(skillName, numberRequired,
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
	/// <param name="skillName">SkillName</param>
	/// <param name="numberRequired">Number of requird Persons</param>
	/// <returns></returns>
	private  bool CheckAddable(string skillName, int numberRequired)
	{
		if (skillName == null || skillName == "")
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
	private async void PopulateSkillPicker()
	{
		try
		{
			var skills = (await skillService.GetSkillList()).Select(skill => skill.Name).ToList();
			foreach (var skill in skills)
			{
				Console.WriteLine(skill);
			}
			SkillNamePicker.ItemsSource = skills;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			DisplayAlert("Error", "Failed to load skills.", "OK");
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