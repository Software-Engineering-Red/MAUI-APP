using System.Collections.ObjectModel;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.Views;

public partial class TeamPage : ContentPage
{
    private Team selectedTeam = null;
    ITeamService teamService = new TeamService();
    ITeamMemberService teamMemberService = new TeamMemberService();
    ObservableCollection<Team> teams = new();
    ObservableCollection<TeamMember> availableTeamMembers = new();

    public TeamPage()
    {
        InitializeComponent();
        BindingContext = new Team();

        Task.Run(LoadTeams);
        txe_team.Text = "";
    }

    private async Task LoadTeams()
    {
        teams = new ObservableCollection<Team>(await teamService.GetAll());
        ltv_teams.ItemsSource = teams;
    }

    private async Task LoadAvailableTeamMembers()
    {
        availableTeamMembers = new ObservableCollection<TeamMember>((await teamMemberService.GetAll()).Where(t => t.Available).ToList());
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txe_team.Text)) return;

        if (selectedTeam == null)
        {
            var team = new Team() { Name = txe_team.Text };
            teamService.Add(team);
            teams.Add(team);
        } else
        {
            selectedTeam.Name = txe_team.Text;
            teamService.Update(selectedTeam);
            var team = teams.FirstOrDefault(x => x.ID == selectedTeam.ID);
            team.Name = txe_team.Text;
        }


        selectedTeam = null;
        ltv_teams.SelectedItem = null;
        txe_team.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_teams.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Team Selected", "Select the team you want to delete from the list", "OK");
            return;
        }

        await teamService.Remove(selectedTeam);
        teams.Remove(selectedTeam);

        ltv_teams.SelectedItem = null;
        txe_team.Text = "";
    }


    private void ltv_teams_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedTeam = e.SelectedItem as Team;
        if (selectedTeam == null) return;

        txe_team.Text = selectedTeam.Name;
    }
}