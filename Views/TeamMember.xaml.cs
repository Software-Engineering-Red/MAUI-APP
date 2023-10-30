using MauiApp1.Models;
using MauiApp1.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

/*! The TeamMembersPage class allows for the basic functions 
 *  implemented in the TeamMemberService class to be used within
 *  the UI
 */

namespace MauiApp1.Views;

public partial class TeamMembersPage : ContentPage
{
    TeamMember selectedTeamMember = null;
    ITeamMemberService teamMemberService;
    ObservableCollection<TeamMember> teamMembers = new ObservableCollection<TeamMember>();

    //! public initialisation of components
    public TeamMembersPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        this.teamMemberService = new TeamMemberService();

        Task.Run(async () => await LoadTeamMembers());
    }


    //! Task to load teamMembers into a variable
    private async Task LoadTeamMembers()
    {
        teamMembers = new ObservableCollection<TeamMember>(await teamMemberService.GetTeamMemberList());
        ltv_teamMembers.ItemsSource = teamMembers;
    }

    /*!
    * Detect save button being clicked
    * @param sender (Object) the sender object created by the event
    * @param e (EventArgs) the arguments passed into the event
    */
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        string firstName = txe_teamMemberFirstName.Text;
        string lastName = txe_teamMemberLastName.Text;
        string email = txe_teamMemberEmail.Text;

        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email)) return;

        var teamMember = new TeamMember()
        {
            Firstname = firstName,
            Lastname = lastName,
            Email = email
        };

        teamMemberService.AddTeamMember(teamMember);
        teamMembers.Add(teamMember);

        // Clear input fields
        txe_teamMemberFirstName.Text = "";
        txe_teamMemberLastName.Text = "";
        txe_teamMemberEmail.Text = "";
    }


    /*!
    * Detect delete button being clicked
    * @param sender (Object) the sender object created by the event
    * @param e (EventArgs) the arguments passed into the event
    */
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_teamMembers.SelectedItem == null)
        {
            await DisplayAlert("No TeamMember Selected", "Select the teamMember you want to delete from the list", "OK");
            return;
        }

        TeamMember selectedTeamMember = (TeamMember)ltv_teamMembers.SelectedItem;

        await teamMemberService.DeleteTeamMember(selectedTeamMember);
        teamMembers.Remove(selectedTeamMember);

        // Clear input fields
        txe_teamMemberFirstName.Text = "";
        txe_teamMemberLastName.Text = "";
        txe_teamMemberEmail.Text = "";

        ltv_teamMembers.SelectedItem = null;
    }


    /*!
    * Detect all items selected in list view
    * @param sender (Object) the sender object created by the event
    * @param e (SelectedItemChangedEventArgs) the arguments passed into the event
    */
    private void ltv_teamMembers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        TeamMember selectedTeamMember = e.SelectedItem as TeamMember;
        if (selectedTeamMember == null) return;

        txe_teamMemberFirstName.Text = selectedTeamMember.Firstname;
        txe_teamMemberLastName.Text = selectedTeamMember.Lastname;
        txe_teamMemberEmail.Text = selectedTeamMember.Email;
    }

}