using UndacApp.Models;
using UndacApp.Services;
using static Microsoft.Maui.Controls.Device;
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
    public OperationalTeamPage()
	{
		InitializeComponent();
        Task.Run(async () => await LoadOperationalTeamStatuses());
        Task.Run(async () => await LoadTeams());
    }

    private async Task LoadOperationalTeamStatuses()
    {
        operationalTeamStatuses = new ObservableCollection<OperationalTeamStatus>(await operationalTeamStatusService.GetStatesListAsync());
        picker_team_statuses.ItemsSource = operationalTeamStatuses;
    }

    private async Task LoadTeams()
    {
        teams = new ObservableCollection<Team>(await teamService.GetAll());
        picker_teams.ItemsSource = teams;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_operational_team_authorised.Text)) return;

        if (ltv_operationalTeam.SelectedItem == null)
        {
            OperationalTeamStatus pickerSelected = picker_team_statuses.SelectedItem as OperationalTeamStatus;
            string teamName = picker_teams.SelectedItem as string;
            OperationalTeam operationalTeam = new OperationalTeam() { Name = teamName, CreatedBy = txe_operational_team_authorised.Text, TeamStatus = pickerSelected };
            operationalTeamService.Add(operationalTeam);
            operationalTeams.Add(operationalTeam);
        }

        ltv_operationalTeam.SelectedItem = null;
        txe_operational_team_authorised.Text = null;
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