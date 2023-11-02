using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class PrivledgeRequestsPage : ContentPage
{
    private PrivledgeRequest selectedRequest = null;

    IPrivledgeRequestService requestService;

    ObservableCollection<PrivledgeRequest> requests = new ObservableCollection<PrivledgeRequest>();

    public PrivledgeRequestsPage()
    {
        InitializeComponent();
        this.BindingContext = new PrivledgeRequest();
        this.requestService = new PrivledgeRequestService();

        Task.Run(async () => await LoadRequests());
    }

    private async Task LoadRequests()
    {
        requests = new ObservableCollection<PrivledgeRequest>(await requestService.GetPrivledgeRequestList());
        ltv_privledgeRequests.ItemsSource = requests;
    }

    private void ApprovedButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_privledgeRequests.SelectedItem == null)
        {
            Shell.Current.DisplayAlert("No request selected", "Select the request you wish to approve from the list", "OK");
            return;
        }
    }

    private async void DeniedButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_privledgeRequests.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No request selected", "Select the request you want to deny from the list", "OK");
            return;
        }
    }
}