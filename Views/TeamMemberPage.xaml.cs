using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class TeamMemberPage : ContentPage
{
    private TeamMember selectedTeamMember = null;

    ITeamMemberService teammemberService;

    public ObservableCollection<TeamMember> Teammembers { get; set; }

    public TeamMemberPage()
	{
        InitializeComponent();
        Teammembers = new ObservableCollection<TeamMember>();
        this.BindingContext = this;
        this.teammemberService = new TeamMemberService();

        Task.Run(async () => await LoadData());
        txe_teammember.Text = "";
    }

    private async Task LoadData()
    {
        var newTeamMembers = await teammemberService.GetAll();
        foreach (var tm in newTeamMembers)
        {
            Teammembers.Add(tm);
        }
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_teammember.Text)) return;

        if (selectedTeamMember == null)
        {
            var teammember = new TeamMember() { Name = txe_teammember.Text };
            teammemberService.Add(teammember);
            Teammembers.Add(teammember);
        } else
        {
            selectedTeamMember.Name = txe_teammember.Text;
            teammemberService.Update(selectedTeamMember);
            var teammember = Teammembers.FirstOrDefault(x => x.ID == selectedTeamMember.ID);
            teammember.Name = txe_teammember.Text;
        }


        selectedTeamMember = null;
        ltv_teammember.SelectedItem = null;
        txe_teammember.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_teammember.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Team Member Selected", "Select the Team Member you want to delete from the list", "OK");
            return;
        }

        await teammemberService.Remove(selectedTeamMember);
        Teammembers.Remove(selectedTeamMember);

        ltv_teammember.SelectedItem = null;
        txe_teammember.Text = "";
    }

    private void ltv_teammember_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedTeamMember = e.SelectedItem as TeamMember;
        if (selectedTeamMember == null) return;

        txe_teammember.Text = selectedTeamMember.Name;
    }
}