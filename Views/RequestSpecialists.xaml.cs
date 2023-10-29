using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.Views;

public partial class RequestSpecialists : ContentPage
{
	private readonly DatabaseOperations _dbOps;
	private ISpecialistRequestService specialistRequestService;

	public RequestSpecialists()
	{
		InitializeComponent();

		_dbOps = new DatabaseOperations($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reference_values.sqlite")}");
		specialistRequestService = new SpecialistRequestService($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reference_values.sqlite")}");

		PopulateSkillPicker();
	}

	private async void OnAddRecordClicked(object sender, EventArgs e)
	{
		var skillName = SkillNamePicker.Title;
		var numberRequired = Convert.ToInt32(NumberRequiredEntry.Text);
		var startDate = StartDatePicker.Date;
		var endDate = EndDatePicker.Date;

		if (_dbOps != null && skillName != null)
		{
			try
			{
				specialistRequestService.AddSkillRequest(skillName, numberRequired,
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

	private void OnCheckBoxCheckedChanged()
	{
		// Handle checkbox checked changed event here
		// You can access the checked state using e.Value
	}

	private string GetSelectedSkill() => SkillNamePicker.SelectedItem?.ToString();


	private void PopulateSkillPicker()
	{
		try
		{
			var skills = _dbOps.GetAllRecords("skill");
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

	private void StartDatePicker_DateSelected(object sender, DateChangedEventArgs e)
	{
		EndDatePicker.MinimumDate = e.NewDate;
	}
}