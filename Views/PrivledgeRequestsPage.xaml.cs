using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class PrivledgeRequestsPage : ContentPage
{
    private PrivledgeRequest selectedRequest = null;

    IPrivledgeRequestService requestService;
    ITeamMemberService memberService;

    ObservableCollection<PrivledgeRequest> requests = new ObservableCollection<PrivledgeRequest>();

    public PrivledgeRequestsPage()
    {
        InitializeComponent();
        this.BindingContext = new PrivledgeRequest();
        this.requestService = new PrivledgeRequestService();
        this.memberService = new TeamMemberService();

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
        else
        {
            var selectedRequest = ltv_privledgeRequests.SelectedItem as PrivledgeRequest;
            int updatedID = selectedRequest.ID;
            TeamMember updatedMember = new TeamMember() { ID = updatedID, AccessPrivledgeLevel = selectedRequest.PrivledgeLevel};
            memberService.UpdateTeamMember(updatedMember);
            selectedRequest.Approved = true;
        }
    }

    private async void DeniedButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_privledgeRequests.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No request selected", "Select the request you want to deny from the list", "OK");
            return;
        }
        else
        {
            await Shell.Current.DisplayAlert("Request denied", "Denied request", "OK");
            return;
        }
    }
}