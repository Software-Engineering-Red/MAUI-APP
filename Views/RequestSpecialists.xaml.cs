using MauiApp1.Models;

namespace MauiApp1.Views;

public partial class RequestSpecialists : ContentPage
{
	private readonly DatabaseOperations _dbOps;


	public RequestSpecialists()
	{
		InitializeComponent();

		// Initialize database operations
		_dbOps = new DatabaseOperations($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reference_values.sqlite")}");
	}

	private void OnAddRecordClicked(object sender, EventArgs e)
	{
		if (_dbOps != null)
		{

		}
		// Retrieve values from the Entry and DatePicker controls
		var skillName = SkillNamePicker.Title;
		var organisationId = Convert.ToInt32(OrganisationIdEntry.Text);
		var requestDate = DateTime.Today;
		var requestedBy = 0;
		var numberRequired = Convert.ToInt32(NumberRequiredEntry.Text);
		var startDate = StartDatePicker.Date;
		var endDate = EndDatePicker.Date;
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
			var skills = _dbOps.GetAllRowNames("skill");
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
}