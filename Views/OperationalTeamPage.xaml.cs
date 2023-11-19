using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views;

public partial class OperationalTeamPage : ContentPage
{
    private IOperationalTeamService operationalTeamService = new OperationalTeamService();
    private IOperationalTeamStatusService operationalTeamStatusService = new OperationalTeamStatusService();
    private ITeamService teamService = new TeamService();
    ObservableCollection<OperationalTeam> operationalTeams = new ObservableCollection<OperationalTeam>();
    ObservableCollection<OperationalTeamStatus> operationalTeamStatuses = new ObservableCollection<OperationalTeamStatus>();
    ObservableCollection<Team> teams = new ObservableCollection<Team>();
    private OperationalTeam selectedOperation = null;
	public OperationalTeamPage()
	{
		InitializeComponent();
        Task.Run(async () => await LoadOperationalTeamStatuses());
        Task.Run(async () => await LoadTeams());
        Task.Run(async () => await LoadTeamStatuses());
    }

    private async Task LoadTeamStatuses()
    {
        operationalTeams = new ObservableCollection<OperationalTeam>(await operationalTeamService.GetAll());
        ltv_operationalTeam.ItemsSource = operationalTeams;
    }

    private async Task LoadOperationalTeamStatuses()
    {
        operationalTeamStatuses = new ObservableCollection<OperationalTeamStatus>(await operationalTeamStatusService.GetStatesListAsync());
        picker_team_statuses.ItemsSource = operationalTeamStatuses;
    }

    private async Task LoadTeams()
    {
        //teams = new ObservableCollection<Team>(await teamService.GetAll());
        TeamMember member1 = new TeamMember { Name = "Bob", AccessPrivilegeLevel = "2", Available = true };
        TeamMember member2 = new TeamMember { Name = "Craig", AccessPrivilegeLevel = "1", Available = true };
        TeamMember member3 = new TeamMember { Name = "Steve", AccessPrivilegeLevel = "5", Available = false };
        List<TeamMember> teamMemberList = new List<TeamMember>() { member1, member2, member3 };
        Team team1 = new Team { Name = "Test1", TeamMembers=teamMemberList};
        Team team2 = new Team { Name = "Test2", TeamMembers = teamMemberList };
        Team team3 = new Team { Name = "Test3", TeamMembers = teamMemberList };
        teams = new ObservableCollection<Team>() { team1, team2, team3 };
        picker_teams.ItemsSource = teams;
    }

    private void teamSelected(object sender, EventArgs e)
    {
        if (picker_teams.SelectedItem != null)
        {
            Team selectedTeam = picker_teams.SelectedItem as Team;
            ltv_teamMembers.ItemsSource = selectedTeam.TeamMembers;
        }
    }

    private void ltv_operationalTeam_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedOperation = e.SelectedItem as OperationalTeam;
        if (selectedOperation == null) return;

        txe_operational_team_authorised.Text = selectedOperation.CreatedBy;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_operational_team_authorised.Text)) return;

        if (ltv_operationalTeam.SelectedItem == null)
        {
            OperationalTeamStatus teamStatus = picker_team_statuses.SelectedItem as OperationalTeamStatus;
            Team teamName = picker_teams.SelectedItem as Team;
            OperationalTeam operationalTeam = new OperationalTeam() { Name = teamName.Name, CreatedBy = txe_operational_team_authorised.Text, TeamStatus = teamStatus.Name };
            operationalTeamService.Add(operationalTeam);
            operationalTeams.Add(operationalTeam);
        }
        else
        {
            OperationalTeam operationalTeam = ltv_operationalTeam.SelectedItem as OperationalTeam;
            operationalTeam.CreatedBy = txe_operational_team_authorised.Text;
            operationalTeamService.Update(operationalTeam);
        }

        ltv_operationalTeam.SelectedItem = null;
        txe_operational_team_authorised.Text = null;
        picker_team_statuses.SelectedItem = null;
        picker_teams.SelectedItem = null;
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_operationalTeam.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Status Selected", "Select the status you want to delete from the list", "OK");
            return;
        }

        await operationalTeamService.Remove(ltv_operationalTeam.SelectedItem as OperationalTeam);
        operationalTeams.Remove(ltv_operationalTeam.SelectedItem as OperationalTeam);

        ltv_operationalTeam.SelectedItem = null;
        txe_operational_team_authorised.Text = null;
	}
}