using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class TeamMemberPage : ContentPage
{
    /*! <summary>
        A reference pointer for storing currently selected TeamMember.
     </summary> */
    private TeamMember selectedTeamMember = null;

    /*! <summary>
        An instance of ITeamMemberService
     </summary> */
    ITeamMemberService teamMemberService;
    IPrivledgeRequestService privledgeRequestService;

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
        this.BindingContext = new TeamMember();
        this.teamMemberService = new TeamMemberService();
        this.privledgeRequestService = new PrivledgeRequestService();

        Task.Run(async () => await LoadTeamMembers());
        txe_teamMember.Text = "";
        txe_privledgeLevel.Text = "";
    }

    /*! <summary>
            Private method loading the TeamMember list using TeamMemberService getter.
        </summary> 
        <returns>Task promise, informing about the status of its' completion.</returns> */
    private async Task LoadTeamMembers()
    {
        teamMembers = new ObservableCollection<TeamMember>(await teamMemberService.GetTeamMemberList());
        ltv_teamMembers.ItemsSource = teamMembers;
    }

    /*! <summary>
            Method responsible for saving TeamMember into SQLite database, triggered by selection of save button.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_teamMember.Text)) return;

        if (selectedTeamMember == null)
        {
            var teamMember = new TeamMember() { Name = txe_teamMember.Text, AccessPrivledgeLevel = txe_privledgeLevel.Text };
            teamMemberService.AddTeamMember(teamMember);
            teamMembers.Add(teamMember);
        }
        else
        {
            if (int.Parse(txe_privledgeLevel.Text) <= int.Parse(selectedTeamMember.AccessPrivledgeLevel))
            {
                selectedTeamMember.Name = txe_teamMember.Text;
                selectedTeamMember.AccessPrivledgeLevel = txe_privledgeLevel.Text;
                teamMemberService.UpdateTeamMember(selectedTeamMember);
                var teamMember = teamMembers.FirstOrDefault(x => x.ID == selectedTeamMember.ID);
                teamMember.Name = txe_teamMember.Text;
                teamMember.AccessPrivledgeLevel = txe_privledgeLevel.Text;
            } 
            else
            {
                
                PrivledgeRequest request = new PrivledgeRequest() { RequestType = "Privledge Escalation", MemberID = selectedTeamMember.ID, PrivledgeLevel = txe_privledgeLevel.Text, Approved = false };
                privledgeRequestService.AddRequest(request);
                DisplayAlert("Failure", "Unable to increase privledges without Deputy Team Leader approval - Sending for approval", "OK");
            }
        }


        selectedTeamMember = null;
        ltv_teamMembers.SelectedItem = null;
        txe_teamMember.Text = "";
        txe_privledgeLevel.Text = "";
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

        await teamMemberService.DeleteTeamMember(selectedTeamMember);
        teamMembers.Remove(selectedTeamMember);

        ltv_teamMembers.SelectedItem = null;
        txe_teamMember.Text = "";
        txe_privledgeLevel.Text = "";
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
        txe_privledgeLevel.Text = selectedTeamMember.AccessPrivledgeLevel;
    }
}