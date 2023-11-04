using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views;

public partial class PrivilegeRequestsPage : ContentPage
{
    IPrivilegeRequestService requestService;
    ITeamMemberService memberService;

    ObservableCollection<PrivilegeRequest> requests = new ObservableCollection<PrivilegeRequest>();

    public PrivilegeRequestsPage()
    {
        Task.Run(async () => await LoadRequests());
        
        this.BindingContext = new PrivilegeRequest();
        this.requestService = new PrivilegeRequestService();
        this.memberService = new TeamMemberService();

        InitializeComponent();
    }

    private async Task LoadRequests()
    {
        requests = new ObservableCollection<PrivilegeRequest>(await requestService.GetPrivilegeRequestList());
        ltv_privilegeRequests.ItemsSource = requests;
    }

    private void ApprovedButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_privilegeRequests.SelectedItem == null)
        {
            Shell.Current.DisplayAlert("No request selected", "Select the request you wish to approve from the list", "OK");
            return;
        }
        else
        {
            var selectedRequest = ltv_privilegeRequests.SelectedItem as PrivilegeRequest;
            int updatedID = selectedRequest.ID;
            TeamMember updatedMember = memberService.GetTeamMemberById(updatedID).Result;
            updatedMember.AccessPrivilegeLevel = selectedRequest.PrivilegeLevel;
            memberService.UpdateTeamMember(updatedMember);
            selectedRequest.Approved = true;
        }
    }

    private async void DeniedButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_privilegeRequests.SelectedItem == null)
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