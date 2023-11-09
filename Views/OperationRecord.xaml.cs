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

			//Task.Run(async () => await LoadState());
		}

        //private async Task LoadState()
		/*{
			OperationRequests = new ObservableCollection<List<string>> (await operationRecords_Service.OperationRecordsRquest());
			
			LW_OperationsRequests.ItemsSource = OperationRequests; 
		}*/


	}
}