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
		var skillName = SkillNameEntry.Text;
		var organisationId = Convert.ToInt32(OrganisationIdEntry.Text);
		var requestDate = DateTime.Today;
		var requestedBy = Convert.ToInt32(RequestedByEntry.Text);
		var numberRequired = Convert.ToInt32(NumberRequiredEntry.Text);
		var startDate = StartDatePicker.Date;
		var endDate = EndDatePicker.Date;

		try
		{
			
		}
	}

}