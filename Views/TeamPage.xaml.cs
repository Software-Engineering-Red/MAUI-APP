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
        txe_alert.Text = "";
    }

    private async Task LoadTeams()
    {
        _Teams = new ObservableCollection<Team>(await _TeamService.GetTeams());
        ltv_Teams.ItemsSource = _Teams;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_alert.Text)) return;

        if (_selectedTeam == null)
        {
            var alert = new Team() { Name = txe_alert.Text };
            _TeamService.AddTeam(alert);
            _Teams.Add(alert);
        }
        else
        {
            _selectedTeam.Name = txe_alert.Text;
            _TeamService.UpdateTeam(_selectedTeam);
            var alert = _Teams.FirstOrDefault(x => x.ID == _selectedTeam.ID);
            alert.Name = txe_alert.Text;
        }


        _selectedTeam = null;
        ltv_Teams.SelectedItem = null;
        txe_alert.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_Teams.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No alert types Selected", "Select the alert type you want to delete from the list", "OK");
            return;
        }

        await _TeamService.DeleteTeam(_selectedTeam);
        _Teams.Remove(_selectedTeam);

        ltv_Teams.SelectedItem = null;
        txe_alert.Text = "";
    }

    private void ltv_Teams_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _selectedTeam = e.SelectedItem as Team;
        if (_selectedTeam == null) return;

        txe_alert.Text = _selectedTeam.Name;
    }
}