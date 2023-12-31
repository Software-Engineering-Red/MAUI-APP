using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views;

public partial class TeamMemberPage : ContentPage
{
    /*! <summary>
        A reference pointer for storing currently selected TeamMember.
     </summary> */
    private TeamMember selectedTeamMember = null;
    private int privilegeLevel;

    /*! <summary>
        An instance of ITeamMemberService
     </summary> */
    ITeamMemberService teamMemberService = new TeamMemberService();
    IPrivilegeRequestService privilegeRequestService = new PrivilegeRequestService();

    /*! <summary>
        Collection of current TeamMembers.
    </summary> */
    ObservableCollection<TeamMember> teamMembers = new ObservableCollection<TeamMember>();

    /*! <summary>
        Constructor class, setting the binding context and initiating the TeamMember serrvice, as well as loading the TeamMember list.
    </summary> */
    public TeamMemberPage()
	{
        InitializeComponent();
        BindingContext = new TeamMember();
        Task.Run(LoadTeamMembers);
    }

    /*! <summary>
            Private method loading the TeamMember list using TeamMemberService getter.
        </summary> 
        <returns>Task promise, informing about the status of its' completion.</returns> */
    private async Task LoadTeamMembers()
    {
        teamMembers = new ObservableCollection<TeamMember>(await teamMemberService.GetAll());
        ltv_teamMembers.ItemsSource = teamMembers;
    }

    /*! <summary>
            Method responsible for saving TeamMember and System Type into SQLite database, triggered by selection of save button.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_teamMember.Text) || String.IsNullOrEmpty(txe_privilegeLevel.Text)) return;

        if (selectedTeamMember == null && int.TryParse(txe_privilegeLevel.Text, out privilegeLevel) == true)
        {
            
            var teamMember = new TeamMember()
            {
                Name = txe_teamMember.Text,
                AccessPrivilegeLevel = txe_privilegeLevel.Text,
                SystemType = (string)pickerSystemType.SelectedItem,
                IsSystemTypeUpdatePending = true
            };
            
            teamMemberService.Add(teamMember);
            teamMembers.Add(teamMember);
            if (pickerSystemType.SelectedItem == null)
            {
                DisplayAlert("System Type Error", "Please select a system type", "OK");
                return;
            }
            else if (teamMember.IsSystemTypeUpdatePending)
            {
                
                PrivilegeRequest request = new PrivilegeRequest()
                {
                    RequestType = "System Type Update",
                    MemberID = teamMember.ID,
                    SystemType = teamMember.SystemType, 
                    Approved = false 
                };

                privilegeRequestService.AddRequest(request);
                DisplayAlert("System Type Update", "System type update requested - Pending approval", "OK");
            }
        }

        else
        {
            if (int.TryParse(txe_privilegeLevel.Text, out privilegeLevel) == true)
            {
                if (selectedTeamMember.IsSystemTypeUpdatePending)
                {
                   
                    PrivilegeRequest request = new PrivilegeRequest()
                    {
                        RequestType = "System Type Update",
                        MemberID = selectedTeamMember.ID,
                        SystemType = selectedTeamMember.SystemType, 
                        Approved = false 
                    };
                

                privilegeRequestService.AddRequest(request);
                DisplayAlert("System Type Update", "System type update requested - Pending approval", "OK");
                }
                    selectedTeamMember.AccessPrivilegeLevel =
                    selectedTeamMember.AccessPrivilegeLevel == null
                    ? selectedTeamMember.AccessPrivilegeLevel = "0"
                    : selectedTeamMember.AccessPrivilegeLevel = selectedTeamMember.AccessPrivilegeLevel;
                    selectedTeamMember.SystemType = (string)pickerSystemType.SelectedItem;

                if (selectedTeamMember.AccessPrivilegeLevel.Equals("disabled") || privilegeLevel <= int.Parse(selectedTeamMember.AccessPrivilegeLevel))
                {
                    selectedTeamMember.Name = txe_teamMember.Text;
                    selectedTeamMember.AccessPrivilegeLevel = txe_privilegeLevel.Text;
                    teamMemberService.Update(selectedTeamMember);
                    var teamMember = teamMembers.FirstOrDefault(x => x.ID == selectedTeamMember.ID);
                    teamMember.Name = txe_teamMember.Text;
                    teamMember.AccessPrivilegeLevel = txe_privilegeLevel.Text;

                }
               
                
                else
                {
                   
                    PrivilegeRequest request = new PrivilegeRequest() { RequestType = "Privilege Escalation", MemberID = selectedTeamMember.ID, PrivilegeLevel = txe_privilegeLevel.Text, Approved = false };
                    privilegeRequestService.AddRequest(request);
                    DisplayAlert("Failure", "Unable to increase privileges without Deputy Team Leader approval - Sending for approval", "OK");
                }
            }
            else
            {
                DisplayAlert("Failure", "Unable to use privilege level that is not an integer", "OK");
            }
        }


        selectedTeamMember = null;
        ltv_teamMembers.SelectedItem = null;
        txe_teamMember.Text = null;
        txe_privilegeLevel.Text = null;
    }

    /*! <summary>
             Method responsible for removing TeamMember from SQLite database, triggered by selection of delete button.
             Note: If no TeamMember is selected, no TeamMember will be removed.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_teamMembers.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No TeamMember Selected", "Select the teamMember you want to delete from the list", "OK");
            return;
        }

        await teamMemberService.Remove(selectedTeamMember);
        teamMembers.Remove(selectedTeamMember);

        ltv_teamMembers.SelectedItem = null;
        txe_teamMember.Text = null;
        txe_privilegeLevel.Text = null;
    }

    private async void RemoveAccessButton_Clicked(object sender, EventArgs e)
    {
        selectedTeamMember.AccessPrivilegeLevel = "disabled";
        selectedTeamMember.SystemType = null;
        selectedTeamMember.IsSystemTypeUpdatePending = false;
        await teamMemberService.Update(selectedTeamMember);
        await Shell.Current.DisplayAlert("Team member access removed", "Team member access has been removed", "OK");
        return;
    }

    /*! <summary>
            Method responsible for updating currently selected item, integrating UI and Backend functionality.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void ltv_teamMembers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedTeamMember = e.SelectedItem as TeamMember;
        if (selectedTeamMember == null) return;

        txe_teamMember.Text = selectedTeamMember.Name;
        txe_privilegeLevel.Text = selectedTeamMember.AccessPrivilegeLevel;
    }
}