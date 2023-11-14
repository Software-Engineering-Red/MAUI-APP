using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.ViewModels
{
	public class OperationResourceRequestStatusViewModel : INotifyPropertyChanged
	{
		private IOperationResourceRequestStatusService operationResourceRequestStatusService;
		private OperationResourceRequestStatus selectedOperationResourceRequestStatus;

		private string name;


		public OperationResourceRequestStatusViewModel()
		{
			operationResourceRequestStatusService = new OperationResourceRequestStatusService();
			Task.Run(async () => await LoadData());
		}

		public ObservableCollection<OperationResourceRequestStatus> OperationResourceRequestStatusList { get; set; }

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


		public OperationResourceRequestStatus SelectedOperationResourceRequestStatus
		{
			get => selectedOperationResourceRequestStatus;
			set
			{
				if (selectedOperationResourceRequestStatus != value)
				{
					selectedOperationResourceRequestStatus = value;
					OnPropertyChanged(nameof(SelectedOperationResourceRequestStatus));

					if (selectedOperationResourceRequestStatus != null)
					{
						Name = selectedOperationResourceRequestStatus.Name;
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

			if (SelectedOperationResourceRequestStatus == null)
			{
				add();
				resetValues();
			}
			else
			{
				update();
			}
			await LoadData();
		}

		private async void add()
		{
			OperationResourceRequestStatus operationalTeamStatusService = new OperationResourceRequestStatus
			{
				Name = Name,
			};

			await operationResourceRequestStatusService.Add(operationalTeamStatusService);
		}

		private async void update()
		{
			SelectedOperationResourceRequestStatus.Name = Name;

			await operationResourceRequestStatusService.Update(SelectedOperationResourceRequestStatus);
		}
		private void resetValues()
		{
			SelectedOperationResourceRequestStatus = null;

			Name = string.Empty;

		}

		private async void remove()
		{
			var result = await operationResourceRequestStatusService.Remove(SelectedOperationResourceRequestStatus);
		}

		private async void Delete()
		{
			if (SelectedOperationResourceRequestStatus != null)
			{
				remove();
				resetValues();
			}
			await LoadData();
		}

		private async Task LoadData()
		{
			ObservableCollection<OperationResourceRequestStatus> newData = new ObservableCollection<OperationResourceRequestStatus>(await operationResourceRequestStatusService.GetAll());

			OperationResourceRequestStatusList = newData;
			OnPropertyChanged(nameof(OperationResourceRequestStatusList));
		}


		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
