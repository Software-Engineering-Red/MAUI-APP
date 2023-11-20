using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.ViewModels
{
	public class OperationalTeamViewModel : AStandardViewModel
	{
		private readonly IOperationalTeamService operationalTeamService;
		private readonly IOperationService operationService;
		private readonly IOperationalTeamStatusService operationalTeamStatusService;
		private OperationalTeam selectedOperationalTeam;

		private string name;
		private string createdBy;
		private Operation selectedOperation;
		private OperationalTeamStatus selectedStatus;


		public OperationalTeamViewModel()
		{
			operationalTeamService = new OperationalTeamService();
			operationService = new OperationService();
			operationalTeamStatusService = new OperationalTeamStatusService();
			Task.Run(async () => await LoadData());
		}

		public ObservableCollection<OperationalTeam> OperationalTeamList { get; set; }
		private ObservableCollection<Operation> operations;
		private ObservableCollection<OperationalTeamStatus> states;


		public ObservableCollection<Operation> Operations
        {
            get => operations;
            set
            {
                if (operations != value)
                {
                    operations = value;
                    OnPropertyChanged(nameof(Operations));
                }
            }
        }

        public ObservableCollection<OperationalTeamStatus> States
        {
            get => states;
            set
            {
                if (states != value)
                {
                    states = value;
                    OnPropertyChanged(nameof(States));
                }
            }
        }


		public Operation SelectedOperation
		{
			get => selectedOperation;
			set
			{
				if (selectedOperation != value)
				{
					selectedOperation = value;
					OnPropertyChanged(nameof(selectedOperation));
				}
			}
		}


		// Just for Testpurposes Please Introduce OperationalTeamStatusHistory if needed
		public OperationalTeamStatus SelectedStatus
		{
			get => selectedStatus;
			set
			{
				if (selectedStatus != value)
				{
					selectedStatus = value;
					OnPropertyChanged(nameof(selectedStatus));
				}
			}
		}

		public string Name
		{
			get => name;
			set
			{
				if (name != value)
				{
					name = value;
					OnPropertyChanged(nameof(Name));
				}
			}
		}

		public string CreatedBy
		{
			get => createdBy;
			set
			{
				if (createdBy != value)
				{
					createdBy = value;
					OnPropertyChanged(nameof(CreatedBy));
				}
			}
		}

		public OperationalTeam SelectedOperationalTeam
		{
			get => selectedOperationalTeam;
			set
			{
				if (selectedOperationalTeam != value)
				{
					selectedOperationalTeam = value;
					OnPropertyChanged(nameof(SelectedOperationalTeam));

					if (selectedOperationalTeam != null)
					{
						Name = selectedOperationalTeam.Name;
						CreatedBy = selectedOperationalTeam.CreatedBy;
					}
				}
			}
		}



		

		protected override async void Save()
		{
			if (string.IsNullOrWhiteSpace(Name) || string.IsNullOrWhiteSpace(CreatedBy) || SelectedStatus == null || selectedOperation == null )
			{
				return;
			}

			if (SelectedOperationalTeam == null)
			{
				add();
			}
			else
			{
				update();
			}
			await LoadData();
			resetValues();
		}

		protected override async void Delete()
		{
			if (SelectedOperationalTeam != null)
			{
				remove();
				resetValues();
			}
			await LoadData();
		}

		protected override async Task LoadData()
		{
			await LoadOperationalTeam();
			await LoadOperations();
			await LoadStates();
		}

		private async Task LoadOperationalTeam()
		{
			ObservableCollection<OperationalTeam> newData = new ObservableCollection<OperationalTeam>(await operationalTeamService.GetAll());
			OperationalTeamList = newData;
			OnPropertyChanged(nameof(OperationalTeamList));
		}

		private async Task LoadOperations()
		{
			ObservableCollection<Operation> operationData = new ObservableCollection<Operation>(await operationService.GetAll());
			Operations = operationData;
			OnPropertyChanged(nameof(Operations));
		}

		private async Task LoadStates()
		{
			ObservableCollection<OperationalTeamStatus> stateData = new ObservableCollection<OperationalTeamStatus>(await operationalTeamStatusService.GetStatesListAsync());
			States = stateData;
			OnPropertyChanged(nameof(States));
		}

		protected override async void add()
		{
			OperationalTeam operationalTeam = new OperationalTeam
			{
				Name = Name,
				CreatedBy = CreatedBy,
				OperationId = SelectedOperation.ID,
				TeamStatus = SelectedStatus.Name
			};

			await operationalTeamService.Add(operationalTeam);
		}

		protected override async void update()
		{
			SelectedOperationalTeam.Name = Name;
			SelectedOperationalTeam.CreatedBy = CreatedBy;
			SelectedOperationalTeam.OperationId = SelectedOperation.ID;
			SelectedOperationalTeam.TeamStatus = SelectedStatus.Name;

			await operationalTeamService.Update(SelectedOperationalTeam);
		}


		protected override void resetValues()
		{
			SelectedOperationalTeam = null;

			Name = string.Empty;
			CreatedBy = string.Empty;
			SelectedOperation = null;
			SelectedStatus = null;
		}

		protected override async void remove()
		{
			await operationalTeamService.Remove(SelectedOperationalTeam);
		}
	}
}
