using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views
{
	public partial class OperationRecord : ContentPage
	{
        OperationRecords operationRecords = null;

        IOperation_Records operationRecords_Service;

		ObservableCollection<OperationRecords> OperationRequests = new ObservableCollection<OperationRecords>();
		

		public OperationRecord()
		{
			InitializeComponent();
			BindingContext = new OperationRecords();
			operationRecords_Service = new OperationRecords_Service();
            Task.Run(async () => await LoadOperationRecords());
        }

        private async Task LoadOperationRecords()
        {
			OperationRequests = new ObservableCollection<OperationRecords>(await operationRecords_Service.GetOperationRecordsTable());
			LW_OperationsRequests.ItemsSource = OperationRequests;
		}


    }
}