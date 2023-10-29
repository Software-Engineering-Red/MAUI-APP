namespace MauiApp1.Views;

public partial class RequestSpecialists : ContentPage
{
	public RequestSpecialists()
	{
		InitializeComponent();
	}

	private void OnAddRecordClicked(object sender, EventArgs e)
	{
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

}