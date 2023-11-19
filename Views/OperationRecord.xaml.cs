using UndacApp.Models;
using UndacApp.Models.Temp_Models;
using UndacApp.Services;
using UndacApp.Services.Temp_Services;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;


namespace UndacApp.Views
{
	public partial class OperationRecord : ContentPage
	{
        OperationRecords operationRecords = null;

        IOperation_Records operationRecords_Service;

		OperationRecords_Service operationRecords_Service_Service = new OperationRecords_Service();

		List<string> OperationRequests = new List<string>();

        Temp_Resource_Operation_Team_Operation_Resource_Request service = new Temp_Resource_Operation_Team_Operation_Resource_Request();


        public OperationRecord()
		{
			InitializeComponent();
			//LW_OperationsRequests.ItemsSource = operationRecords_Service.GetOperationalTeamStatusesID()
			
			DisplayAlert("test", service.GetResourceName().Result.ToString(), "ok");
			/*private void LoadOperationRecords()
			{
				OperationRequests = new List<string>(operationRecords_Service.GetOperationalTeamStatusesIDL().Result.ToString().ToList());
				LW_OperationsRequests.ItemsSource = OperationRequests;
			}*/
		}

    }
}