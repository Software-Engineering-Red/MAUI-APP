using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.ViewModels
{
	public class OperationViewModel : INotifyPropertyChanged
	{
		private readonly IOperationService operationService;

		public ObservableCollection<Operation> OperationList { get; set; }




		private async Task LoadData()
		{
			await LoadOperation();
		}

		private async Task LoadOperation()
		{
			ObservableCollection<Operation> operationData = new ObservableCollection<Operation>(await operationService.GetAll());
			OperationList = operationData;
			OnPropertyChanged(nameof(OperationList));
		}


		public event PropertyChangedEventHandler? PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
