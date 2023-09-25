using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class OperationalTeamStatusPage : ContentPage
{

    OperationalTeamStatus selectedStatus = null;
    IOperationalTeamStatusService statusService;
    ObservableCollection<OperationalTeamStatus> statuses = new ObservableCollection<OperationalTeamStatus>();

    public OperationalTeamStatusPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        this.statusService = new OperationalTeamStatusService();

        Task.Run(async () => await LoadStatuses());
        txe_status.Text = "";
    }

    private async Task LoadStatuses()
    {
        statuses = new ObservableCollection<OperationalTeamStatus>(await statusService.GetStatusesListAsync());
        ltv_statuses.ItemsSource = statuses;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_status.Text)) return;

        if (selectedStatus == null)
        {
            var status = new OperationalTeamStatus() { Name = txe_status.Text };
            statusService.SaveStatusAsync(status);
            statuses.Add(status);
        }
        else
        {
            selectedStatus.Name = txe_status.Text;
            statusService.UpdateStatusAsync(selectedStatus);
            var status = statuses.FirstOrDefault(x => x.ID == selectedStatus.ID);
            status.Name = txe_status.Text;
        }


        selectedStatus = null;
        ltv_statuses.SelectedItem = null;
        txe_status.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_statuses.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Status Selected", "Select the status you want to delete from the list", "OK");
            return;
        }

        await statusService.DeleteStatusAsync(selectedStatus);
        statuses.Remove(selectedStatus);

        ltv_statuses.SelectedItem = null;
        txe_status.Text = "";
    }

    private void ltv_status_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedStatus = e.SelectedItem as OperationalTeamStatus;
        if (selectedStatus == null) return;

        txe_status.Text = selectedStatus.Name;
    }
    
}