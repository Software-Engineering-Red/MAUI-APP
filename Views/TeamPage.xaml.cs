using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class TeamPage : ContentPage
{
    Team _selectedTeam = null;
    ITeamService _TeamService;
    ObservableCollection<Team> _Teams = new ObservableCollection<Team>();

    public TeamPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        _TeamService = new TeamService();

        Task.Run(async () => await LoadTeams());
        txe_team.Text = "";
    }

    private async Task LoadTeams()
    {
        _Teams = new ObservableCollection<Team>(await _TeamService.GetTeamList());
        ltv_teams.ItemsSource = _Teams;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_team.Text)) return;

        if (_selectedTeam == null)
        {
            var Team = new Team() { Name = txe_team.Text };
            _TeamService.AddTeam(Team);
            _Teams.Add(Team);
        }
        else
        {
            _selectedTeam.Name = txe_team.Text;
            _TeamService.UpdateTeam(_selectedTeam);
            var Team = _Teams.FirstOrDefault(x => x.ID == _selectedTeam.ID);
            Team.Name = txe_team.Text;
        }


        _selectedTeam = null;
        txe_team.SelectedItem = null;
        txe_team.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (txe_team.SelectedItem == null)
        {
            await Shell.Current.DisplayTeam("No Team types Selected", "Select the Team type you want to delete from the list", "OK");
            return;
        }

        await _TeamService.DeleteTeam(_selectedTeam);
        _Teams.Remove(_selectedTeam);

        txe_team.SelectedItem = null;
        txe_team.Text = "";
    }

    private void ltv_Teams_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _selectedTeam = e.SelectedItem as Team;
        if (_selectedTeam == null) return;

        txe_team.Text = _selectedTeam.Name;
    }
}