using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.ViewModels
{
	public class RequestListViewModel : INotifyPropertyChanged
	{
		private readonly IFindOperationForRequestService findOperationForRequestService;
		private Operation selectedOperation;

		public ObservableCollection<OperationResourceRequest> RequestList { get; set; }

		public RequestListViewModel(Operation selectedOperation, IFindOperationForRequestService service)
		{
			this.selectedOperation = selectedOperation;
			findOperationForRequestService = service;
			Task.Run(async () => await LoadRequests());
		}

		private async Task LoadRequests()
		{
			var requests = await findOperationForRequestService.GetRequestsByOperation(selectedOperation);
			RequestList = new ObservableCollection<OperationResourceRequest>(requests);
			OnPropertyChanged(nameof(RequestList));
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
