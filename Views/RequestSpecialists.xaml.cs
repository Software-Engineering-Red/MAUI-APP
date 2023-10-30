using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.Views;

public partial class RequestSpecialists : ContentPage
{
	private readonly DatabaseOperations _dbOps;
	private ISpecialistRequestService specialistRequestService;
	private DateTime selectedDate;

	public RequestSpecialists()
	{
		InitializeComponent();

		_dbOps = new DatabaseOperations($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reference_values.sqlite")}");
		specialistRequestService = new SpecialistRequestService($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reference_values.sqlite")}");

		PopulateSkillPicker();
	}

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

	private  bool CheckAddable(string skillName, int numberRequired)
	{
		if (_dbOps == null)
		{
			DisplayAlert("Error", $"No Database-Connection", "OK");
			return false;
		}
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