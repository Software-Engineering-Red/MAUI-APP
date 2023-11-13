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
        InitializeComponent();
        this.requestService = new PrivilegeRequestService();              
        this.memberService = new TeamMemberService();
        Task.Run(async () => await LoadRequests());
        this.BindingContext = new PrivilegeRequest();

        
    }

    private async Task LoadRequests()
    {
        requests = new ObservableCollection<PrivilegeRequest>(await requestService.GetPrivilegeRequestList());
        ltv_privilegeRequests.ItemsSource = requests;
    }

    private async void ApprovedButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_privilegeRequests.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No request selected", "Select the request you wish to approve from the list", "OK");
            return;
        }
        else
        {
            var selectedRequest = ltv_privilegeRequests.SelectedItem as PrivilegeRequest;
            int updatedID = selectedRequest.MemberID;
            var teamMembers = new ObservableCollection<TeamMember>(await memberService.GetAll());
            var teamMember = teamMembers.FirstOrDefault(x => x.ID == updatedID);

            if (teamMember != null)
            {
                teamMember.AccessPrivilegeLevel = selectedRequest.PrivilegeLevel;
                await memberService.Update(teamMember);
                selectedRequest.Approved = true;
                await requestService.UpdatePrivilegeRequest(selectedRequest);

                requests.Remove(selectedRequest);
                await requestService.DeleteRequest(selectedRequest);
                
                ltv_privilegeRequests.ItemsSource = requests;
            }
            else
        {
            
            await Shell.Current.DisplayAlert("Member not found", "The selected member was not found in the team members collection", "OK");
              
            }
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
            var selectedRequest = ltv_privilegeRequests.SelectedItem as PrivilegeRequest;
            await requestService.DeleteRequest(selectedRequest);
            await Shell.Current.DisplayAlert("Request denied", "Denied request", "OK");
            return;
        }
    }
}