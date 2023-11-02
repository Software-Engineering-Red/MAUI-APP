using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class TeamMemberPage : ContentPage
{
    private TeamMember selectedTeamMember = null;

    ITeamMemberService teammemberService;

    ObservableCollection<TeamMember> teammembers = new ObservableCollection<TeamMember>();

    public TeamMemberPage()
	{
		InitializeComponent();
        this.BindingContext = this;
        this.teammemberService = new TeamMemberService();

        Task.Run(async () => await LoadContinents());
        txe_teammember.Text = "";
    }

    /*! <summary>
            Private method loading the Continent list using ContinentService getter.
        </summary> 
        <returns>Task promise, informing about the status of its' completion.</returns> */
    private async Task LoadContinents()
    {
        teammembers = new ObservableCollection<TeamMember>(await teammemberService.GetAll());
        ltv_teammember.ItemsSource = teammembers;
    }

    /*! <summary>
            Method responsible for saving Continent into SQLite database, triggered by selection of save button.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_teammember.Text)) return;

        if (selectedTeamMember == null)
        {
            var teammember = new TeamMember() { Name = txe_teammember.Text };
            teammemberService.Add(teammember);
            teammembers.Add(teammember);
        } else
        {
            selectedTeamMember.Name = txe_teammember.Text;
            teammemberService.Update(selectedTeamMember);
            var teammember = teammembers.FirstOrDefault(x => x.ID == selectedTeamMember.ID);
            teammember.Name = txe_teammember.Text;
        }


        selectedTeamMember = null;
        ltv_teammember.SelectedItem = null;
        txe_teammember.Text = "";
    }

    /*! <summary>
             Method responsible for removing Continent from SQLite database, triggered by selection of delete button.
             Note: If no Continent is selected, no Continent will be removed.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_teammember.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Team Member Selected", "Select the Team Member you want to delete from the list", "OK");
            return;
        }

        await teammemberService.Remove(selectedTeamMember);
        teammembers.Remove(selectedTeamMember);

        ltv_teammember.SelectedItem = null;
        txe_teammember.Text = "";
    }

    /*! <summary>
            Method responsible for updating currently selected item, integrating UI and Backend functionality.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void ltv_teammember_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedTeamMember = e.SelectedItem as TeamMember;
        if (selectedTeamMember == null) return;

        txe_teammember.Text = selectedTeamMember.Name;
    }
}