using System.Collections.ObjectModel;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.ViewModels
{
	public class OperationViewModel : AStandardViewModel
	{
		private readonly IOperationService operationService;

		public ObservableCollection<Operation> OperationList { get; set; }

		private Operation selectedOperation;

		private string type;
		private string purpose;
		private string location;
		private string createdBy;
		private string name;



		public Operation SelectedOperation
		{
			get => selectedOperation;
			set
			{
				if (selectedOperation != value)
				{
					selectedOperation = value;
					OnPropertyChanged(nameof(SelectedOperation));

					if (selectedOperation != null)
					{
						Name = selectedOperation.Name;
						Type = selectedOperation.Type;
						Purpose = selectedOperation.Purpose;
						Location = selectedOperation.Location;
						CreatedBy = selectedOperation.CreatedBy;
					}
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
					OnPropertyChanged(nameof(name));
				}
			}
		}

		public string Type
		{
			get => type;
			set
			{
				if (type != value)
				{
					type = value;
					OnPropertyChanged(nameof(Type));
				}
			}
		}

		public string Purpose
		{
			get => purpose;
			set
			{
				if (purpose != value)
				{
					purpose = value;
					OnPropertyChanged(nameof(Purpose));
				}
			}
		}

		public string Location
		{
			get => location;
			set
			{
				if (location != value)
				{
					location = value;
					OnPropertyChanged(nameof(Location));
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


		public OperationViewModel()
		{
			operationService = new OperationService();

			Task.Run(async () => await LoadData());
		}



		protected override async void add()
		{
			Operation newOperation = new Operation
			{
				Name = Name,
				Type = Type,
				Purpose = Purpose,
				Location = Location,
				CreatedBy = CreatedBy,
				CreatedDate = DateTime.Today,
			};

			await operationService.Add(newOperation);
		}

		protected override async void Delete()
		{
			remove();
			resetValues();
			await LoadData();
		}

		protected override async Task LoadData()
		{
			await LoadOperations();
		}

		private async Task LoadOperations()
		{
			ObservableCollection<Operation> operationData = new ObservableCollection<Operation>(await operationService.GetAll());
			OperationList = operationData;
			OnPropertyChanged(nameof(OperationList));
		}

		protected override async void remove()
		{
			if (SelectedOperation != null)
			{
				await operationService.Remove(SelectedOperation);
			}
		}

		protected override async void resetValues()
		{
			Name = string.Empty;
			Type = string.Empty;
			Purpose = string.Empty;
			Location = string.Empty;
			CreatedBy = string.Empty;
		}

		protected override async void Save()
		{
			if (string.IsNullOrWhiteSpace(Type) || string.IsNullOrWhiteSpace(Purpose) || string.IsNullOrWhiteSpace(Location) || string.IsNullOrWhiteSpace(CreatedBy))
			{
				return;
			}

			if (SelectedOperation == null)
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

		protected override async void update()
		{
			SelectedOperation.Name = Name;
			SelectedOperation.Type = Type;
			SelectedOperation.Purpose = Purpose;
			SelectedOperation.Location = Location;

			await operationService.Update(SelectedOperation);
		}
	}
}
