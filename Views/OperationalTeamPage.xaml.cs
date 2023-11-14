using UndacApp.Models;
using UndacApp.Services;
using static Microsoft.Maui.Controls.Device;
using System.Collections.ObjectModel;

namespace UndacApp.Views;

public partial class OperationalTeamPage : ContentPage
{
    private IOperationalTeamService operationalTeamService = new OperationalTeamService();
    private IOperationalTeamStatusService operationalTeamStatusService = new OperationalTeamStatusService();
    ObservableCollection<OperationalTeam> operationalTeams = new ObservableCollection<OperationalTeam>();
    ObservableCollection<OperationalTeamStatus> operationalTeamStatuses = new ObservableCollection<OperationalTeamStatus>();
    public OperationalTeamPage()
	{
		InitializeComponent();
        Task.Run(async () => await LoadOperationalTeams());
        Task.Run(async () => await LoadOperationalTeamStatuses());
    }

    private async Task LoadOperationalTeams()
    {
        operationalTeams = new ObservableCollection<OperationalTeam>(await operationalTeamService.GetAll());
        ltv_operationalTeam.ItemsSource = operationalTeams;
    }

    private async Task LoadOperationalTeamStatuses()
    {
        operationalTeamStatuses = new ObservableCollection<OperationalTeamStatus>(await operationalTeamStatusService.GetStatesListAsync());
        picker_team_statuses.ItemsSource = operationalTeamStatuses;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_operational_team.Text) || String.IsNullOrEmpty(txe_operational_team_authorised.Text)) return;

        if (ltv_operationalTeam.SelectedItem == null)
        {
            OperationalTeamStatus pickerSelected = picker_team_statuses.SelectedItem as OperationalTeamStatus;
            OperationalTeam operationalTeam = new OperationalTeam() { Name = txe_operational_team.Text, CreatedBy = txe_operational_team_authorised.Text, TeamStatus = pickerSelected };
            operationalTeamService.Add(operationalTeam);
            operationalTeams.Add(operationalTeam);
        }

        ltv_operationalTeam.SelectedItem = null;
        txe_operational_team.Text = null;
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
        txe_operational_team.Text = null;
        txe_operational_team_authorised.Text = null;
    }
}