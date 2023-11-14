using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.ViewModels
{
	public class OperationalTeamViewModel : INotifyPropertyChanged
	{
		private IOperationalTeamService operationalTeamService;
		private OperationalTeam selectedOperationalTeam;

		private string name;
		private int createdBy;
		private int teamId;
		private int status;


		public OperationalTeamViewModel()
		{
			operationalTeamService = new OperationalTeamService();
			Task.Run(async () => await LoadData());
		}

		public ObservableCollection<OperationalTeam> OperationalTeamList { get; set; }

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

		public int CreatedBy
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

		public int TeamId
		{
			get => teamId;
			set
			{
				if (teamId != value)
				{
					teamId = value;
					OnPropertyChanged(nameof(TeamId));
				}
			}
		}

		public int Status
		{
			get => status;
			set
			{
				if (status != value)
				{
					status = value;
					OnPropertyChanged(nameof(Status));
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
						TeamId = selectedOperationalTeam.TeamId;
						Status = selectedOperationalTeam.Status;
					}
				}
			}
		}

		public ICommand SaveCommand => new Command(Save);
		public ICommand DeleteCommand => new Command(Delete);

		private async void Save()
		{
			if (string.IsNullOrWhiteSpace(Name))
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

		private async void add()
		{
			OperationalTeam operationalTeam = new OperationalTeam
			{
				Name = Name,
				CreatedBy = CreatedBy,
				TeamId = TeamId,
				Status = Status
			};

			await operationalTeamService.Add(operationalTeam);
		}

		private async void update()
		{
			SelectedOperationalTeam.Name = Name;
			SelectedOperationalTeam.CreatedBy = CreatedBy;
			SelectedOperationalTeam.TeamId = TeamId;
			SelectedOperationalTeam.Status = Status;

			await operationalTeamService.Update(SelectedOperationalTeam);
		}
		private void resetValues()
		{
			SelectedOperationalTeam = null;

			Name = string.Empty;
			CreatedBy = 0;
			TeamId = 0;
			Status = 0;

		}

		private async void remove()
		{
			var result = await operationalTeamService.Remove(SelectedOperationalTeam);
		}

		private async void Delete()
		{
			if (SelectedOperationalTeam != null)
			{
				remove();
				resetValues();
			}
			await LoadData();
		}

	


		private async Task LoadData()
		{
			ObservableCollection<OperationalTeam> newData = new ObservableCollection<OperationalTeam>(await operationalTeamService.GetAll());

			OperationalTeamList = newData;
			OnPropertyChanged(nameof(OperationalTeamList));
		}


		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
