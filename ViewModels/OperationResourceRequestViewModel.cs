using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.ViewModels
{
	public class OperationResourceRequestViewModel : INotifyPropertyChanged
	{
		private readonly IOperationResourceRequestService operationResourceRequestService;
		private readonly IOperationalTeamService operationalteamService;
		private readonly IResourceService resourceService;
		private readonly IOperationResourceRequestStatusService operationResourceRequestStatusService;
		private OperationResourceRequest selectedOperationResourceRequest;


		private string requestedDetail;
		private ObservableCollection<OperationalTeam> operationalTeams;
		private ObservableCollection<AResource> resources;
		private ObservableCollection<OperationResourceRequestStatus> states;

		public OperationResourceRequestViewModel()
		{
			operationResourceRequestService = new OperationResourceRequestService();
			operationalteamService	= new OperationalTeamService();
			resourceService = new ResourceService();

			Task.Run(async () => await LoadData());
		}

		public OperationResourceRequestViewModel(OperationResourceRequestService operationResourceRequestService,
			OperationalTeamService operationalTeamService, ResourceService resourceService, 
			OperationResourceRequestStatusService operationResourceRequestStatusService)
		{
			this.operationResourceRequestService = operationResourceRequestService;
			this.operationalteamService = operationalTeamService;
			this.resourceService = resourceService;
			this.operationResourceRequestStatusService = operationResourceRequestStatusService;

			Task.Run(async () => await LoadData());
		}

		public ObservableCollection<OperationResourceRequest> OperationResourceRequestList { get; set; }

		public OperationResourceRequest SelectedOperationResourceRequest
		{
			get => selectedOperationResourceRequest;
			set
			{
				if (selectedOperationResourceRequest != value)
				{
					selectedOperationResourceRequest = value;
					OnPropertyChanged(nameof(SelectedOperationResourceRequest));

					if (selectedOperationResourceRequest != null)
					{
						RequestedDetail = selectedOperationResourceRequest.RequestedDetail;
					}
				}
			}
		}

		public string RequestedDetail
		{
			get => requestedDetail;
			set
			{
				if (requestedDetail != value)
				{
					requestedDetail = value;
					OnPropertyChanged(nameof(RequestedDetail));
				}
			}
		}

		public ObservableCollection<OperationalTeam> OperationalTeams
		{
			get => operationalTeams;
			set
			{
				if (operationalTeams != value)
				{
					operationalTeams = value;
					OnPropertyChanged(nameof(OperationalTeams));
				}
			}
		}

		public ObservableCollection<AResource> Resources
		{
			get => resources;
			set
			{
				if (resources != value)
				{
					resources = value;
					OnPropertyChanged(nameof(Resources));
				}
			}
		}
		public OperationResourceRequestStatus SelectedStatus
		{
			get => selectedStatus;
			set
			{
				if (selectedStatus != value)
				{
					selectedStatus = value;
					OnPropertyChanged(nameof(SelectedStatus));
				}
			}
		}


		private OperationalTeam selectedOperationalTeam;
		public OperationalTeam SelectedOperationalTeam
		{
			get => selectedOperationalTeam;
			set
			{
				if (selectedOperationalTeam != value)
				{
					selectedOperationalTeam = value;
					OnPropertyChanged(nameof(SelectedOperationalTeam));
				}
			}
		}

		private AResource selectedResource;
		public AResource SelectedResource
		{
			get => selectedResource;
			set
			{
				if (selectedResource != value)
				{
					selectedResource = value;
					OnPropertyChanged(nameof(SelectedResource));
				}
			}
		}


		public ICommand SaveCommand => new Command(Save);
		public ICommand DeleteCommand => new Command(Delete);

		private async void Save()
		{
			if (string.IsNullOrWhiteSpace(RequestedDetail) || SelectedOperationalTeam == null || SelectedResource == null)
			{
				return;
			}

			if (SelectedOperationResourceRequest == null)
			{
				Add();
				ResetValues();
			}
			else
			{
				Update();
			}
			await LoadData();
		}

		private async void Add()
		{

			OperationResourceRequest newOperationResourceRequest = new OperationResourceRequest
			{
				RequestedDetail = RequestedDetail,
				RequestDate = DateTime.Today,
				OperationalTeamId = SelectedOperationalTeam.ID,
				ResourceId = SelectedResource.ID, 
				Status = "Pending",						
			};

			await operationResourceRequestService.Add(newOperationResourceRequest);
		}

		private async void Update()
		{
			SelectedOperationResourceRequest.RequestedDetail = RequestedDetail;
			SelectedOperationResourceRequest.OperationalTeamId = SelectedOperationalTeam.ID;
			SelectedOperationResourceRequest.ResourceId = SelectedResource.ID;
			await operationResourceRequestService.Update(SelectedOperationResourceRequest);
		}

		private void ResetValues()
		{
			RequestedDetail = string.Empty;
		}

		private async void Remove()
		{
			if (SelectedOperationResourceRequest != null)
			{
				await operationResourceRequestService.Remove(SelectedOperationResourceRequest);
			}
		}

		private async void Delete()
		{
			Remove();
			ResetValues();
			await LoadData();
		}



		private async Task LoadData()
		{
			await LoadOperationResourceRequests();
			await LoadOperationalTeams();
			await LoadResources();
		}

		private async Task LoadOperationResourceRequests()
		{
			ObservableCollection<OperationResourceRequest> operationResourceRequestData = new ObservableCollection<OperationResourceRequest>(await operationResourceRequestService.GetAll());
			OperationResourceRequestList = operationResourceRequestData;
			OnPropertyChanged(nameof(OperationResourceRequestList));
		}

		private async Task LoadOperationalTeams()
		{
			ObservableCollection<OperationalTeam> operationalTeamsData = new ObservableCollection<OperationalTeam>(await operationalteamService.GetAll());
			OperationalTeams = operationalTeamsData;
			OnPropertyChanged(nameof(OperationalTeams));
		}

		private async Task LoadResources()
		{
			ObservableCollection<AResource> resourcesData = new ObservableCollection<AResource>(await resourceService.GetAll());
			Resources = resourcesData;
			OnPropertyChanged(nameof(Resources));
		}


		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
