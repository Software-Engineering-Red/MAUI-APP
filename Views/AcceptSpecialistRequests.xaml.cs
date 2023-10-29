using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.Views;

public partial class AcceptSpecialistRequests : ContentPage
{
	private readonly DatabaseOperations _dbOps;
	private ISpecialistRequestService specialistRequestService;
	private List<SkillRequest> _currentRecords;

	public AcceptSpecialistRequests()
	{
		InitializeComponent();

		_dbOps = new DatabaseOperations($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reference_values.sqlite")}");
		specialistRequestService = new SpecialistRequestService($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reference_values.sqlite")}");
		populateRequestList();
	}

	private void populateRequestList()
	{
		_currentRecords = specialistRequestService.GetAllSkillRequests();
		SkillsRequestsListView.ItemsSource = _currentRecords;
	}
}