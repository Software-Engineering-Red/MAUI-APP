using UndacApp.Models;
using UndacApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace UndacApp.Views;

/// <summary>
/// this page displays, saves and deletes the data from the database via user interaction and inherits ContentPage
/// </summary>
 partial class position_status : ContentPage
{

	position_statuses selectedPostionStatus = null;
	IPositionStatusesServices statusesServices;
	ObservableCollection<position_statuses> PStatus = new ObservableCollection<position_statuses>();

    /*this is the contructor initialize the code on the page*/
    public position_status()
	{
		InitializeComponent();
		BindingContext = new position_statuses();
		this.statusesServices = new PositionStatusesServices();

		Task.Run(async () => await LoadpostionSatus());
		TBX_PositionStatus.Text = "";
	}

	/*this task function get the list of positions statuses in the model Position Statuses.cs
	 * it will wait for the task to be done before moveing on with the next instruction*/
    private async Task LoadpostionSatus()
	{
		PStatus = new ObservableCollection<position_statuses>(await statusesServices.GetPosition_StatusesList());
		LS_PositionStatuses.ItemsSource = PStatus;
	}
	

	/*this event function it will start the functions code if the user clicks on the xaml UI button save
	 *  and will check if the text box has data if true stops the function*/

	 /*this function also check if database index is null and if true will create a new object called the data inside of the text box 
	and add it to the collection*/

	/*other wise it will change the selected item on the list and update the the item in the collection at the index
	then resets the list box and selectedPositionStatus to null and text box to blank*/
	private void B_Save_Clicked(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(TBX_PositionStatus.Text)) return;

		if (selectedPostionStatus == null)
		{
			var status = new position_statuses() { Name = TBX_PositionStatus.Text };
			statusesServices.AddStatus(status);
            PStatus.Add(status);
        }
		else
		{
			selectedPostionStatus.Name = TBX_PositionStatus.Text;
			statusesServices.UpdateStatus(selectedPostionStatus);
			var status = PStatus.FirstOrDefault(x => x.Id == selectedPostionStatus.Id);
			status.Name = TBX_PositionStatus.Text;

			selectedPostionStatus = null;
			LS_PositionStatuses.SelectedItem = null;
			TBX_PositionStatus.Text = "";
        }
	}

    /*this event function it will start the functions code if the user clicks on the xaml UI button Delete
     and will check if the selected item in the list is null and if true will display a message and stop the function*/

    /*otherwise await the function to call DeleteStatus from interface IPositionStatusesServices and then call the function from PositionStatusesServices
     which will delete the item from  the database as well as the collections PStatus*/

	/*it will then set the list box selected item to null and the text box to blank*/
    private async void B_Delete_Clicked(object sender, EventArgs e)
    {
		if (LS_PositionStatuses.SelectedItem == null)
		{
			await DisplayAlert("No position satus selected", "select the position status you wish to delete from the list", "ok");
			return;
		}

		await statusesServices.DeleteStatus(selectedPostionStatus);
		PStatus.Remove(selectedPostionStatus);

		LS_PositionStatuses.SelectedItem = null;
		TBX_PositionStatus.Text = "";
	}

	/*event functiuon sets the list box with data from the database also set the text box with data.*/
	/* if the the database is null then stop the function*/
	/*otherwise set the text box to the selected item clicked on*/
	private void LS_PositionStatuses_ItemSelected(object sender, SelectedItemChangedEventArgs e)
	{
		selectedPostionStatus = e.SelectedItem as position_statuses;
		if (selectedPostionStatus == null) return;

		TBX_PositionStatus.Text = selectedPostionStatus.Name;
	}
}