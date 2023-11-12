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
		private string name;
		private int createdBy;
		private int teamId;
		private int status;

		public OperationalTeamViewModel()
		{
			operationalTeamService = new OperationalTeamService();
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

		public ICommand SaveCommand => new Command(Save);
		public ICommand DeleteCommand => new Command(Delete);

		// Add other necessary properties, commands, and methods here

		private void Save()
		{
			// Implement save logic here
		}

		private void Delete()
		{
			// Implement delete logic here
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
