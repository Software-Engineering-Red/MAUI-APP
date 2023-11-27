using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.ViewModels
{
	public class RequestListViewModel : INotifyPropertyChanged
	{
		private readonly IFindOperationForRequestService findOperationForRequestService;
		private Operation selectedOperation;

		public Operation SelectedOperation
		{
			get => selectedOperation;
			set
			{
				if (selectedOperation != value)
				{
					selectedOperation = value;
					OnPropertyChanged(nameof(SelectedOperation));
					Task.Run(async () => await LoadRequests());
				}
			}
		}

		public ObservableCollection<OperationResourceRequest> RequestList { get; set; }
		public ICommand ApproveRequestCommand { get; }

		public RequestListViewModel(Operation selectedOperation, IFindOperationForRequestService service)
		{
			this.selectedOperation = selectedOperation;
			findOperationForRequestService = service;
			ApproveRequestCommand = new Command<OperationResourceRequest>(async (request) => await ApproveRequestAsync(request));
			Task.Run(async () => await LoadRequests());
		}

		private async Task LoadRequests()
		{
			if (SelectedOperation != null)
			{
				var requests = await findOperationForRequestService.GetRequestsByOperation(SelectedOperation);
				RequestList = new ObservableCollection<OperationResourceRequest>(requests);
				OnPropertyChanged(nameof(RequestList));
			}
		}

		private async Task ApproveRequestAsync(OperationResourceRequest request)
		{
			try
			{
				int result = await findOperationForRequestService.ApproveRequest(request);

				await LoadRequests();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in ApproveRequestAsync: {ex.Message}");
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
