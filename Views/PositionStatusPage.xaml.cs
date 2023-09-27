using MauiApp1.Models;
using MauiApp1.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MauiApp1.Views;

public partial class position_status : ContentPage
{

	position_statuses selectedPostionStatus = null;
	IPositionStatusesServices statusesServices;
	ObservableCollection<position_statuses> PStatus = new ObservableCollection<position_statuses>();


	public position_status()
	{
		InitializeComponent();
		this.BindingContext = this;
		this.statusesServices = new PositionStatusesServices();

		Task.Run(async () => await LoadpostionSatus());
		TBX_PositionStatus.Text = "";
	}

    private async Task LoadpostionSatus()
	{
		PStatus = new ObservableCollection<position_statuses>(await statusesServices.GetPosition_StatusesList());
		LS_PositionStatuses.ItemsSource = PStatus;
	}
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

    private void LS_PositionStatuses_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		selectedPostionStatus = e.SelectedItem as position_statuses;
		if (selectedPostionStatus == null) return;

		TBX_PositionStatus.Text = selectedPostionStatus.Name;//issue here System.NullReferenceException: 'Object reference not set to an instance of an object.'
    }
}