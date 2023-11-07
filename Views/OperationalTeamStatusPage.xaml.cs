using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;
using UndacApp.Services;

namespace UndacApp.Views;

/// <summary>
/// This class extending ContentPage provides the functional logic to
/// the User interface to interact with the Model.
/// </summary>
public partial class OperationalTeamStatusPage : ContentPage
{
    /// <summary>
    /// currently selected instance of OperationalTeamStatus.
    /// </summary>
    OperationalTeamStatus selectedStatus = null;

    /// <summary>
    /// Instance of IOperationalTeamStatusService Interface.
    /// </summary>
    IOperationalTeamStatusService statusService;

    /// <summary>
    /// Collection of of all current instances of OperationalTeamStatus.
    /// </summary>
    ObservableCollection<OperationalTeamStatus> states = new ObservableCollection<OperationalTeamStatus>();

    /// <summary>
    /// The Constructor, initializes the Component, sets instance variables
    /// and loads instances of OperationalTeamStatus from the database.
    /// </summary>
    public OperationalTeamStatusPage()
    {
        InitializeComponent();
        BindingContext = new OperationalTeamStatus();
        this.statusService = new OperationalTeamStatusService();

        Task.Run(async () => await LoadStates());
        text_editor_status.Text = "";
    }

    /// <summary>
    /// Loads a OperationalTeamStatus Collection from the database
    /// and reference the Collection to the in UI.
    /// </summary>
    /// <returns>Task promise</returns>
    private async Task LoadStates()
    {
        states = new ObservableCollection<OperationalTeamStatus>(await statusService.GetStatesListAsync());
        list_view_states.ItemsSource = states;
    }

    /// <summary>
    /// Saves current Element of OperationalTeamStatus to Database
    /// unless string is empty. Creates new object if selectedStatus
    /// is currently null and adds it to the database
    /// through a clicking save button in the UI.
    /// </summary>
    /// <param name="sender">UI Element that send Event.</param>
    /// <param name="e">UI Event triggering Method call </param>
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(text_editor_status.Text)) return;

        if (selectedStatus == null) 
        {
            createNewStatus();
        }
        else
        {
            updateStatus();
        }

        reset_SelectedStatus_and_Text();
    }

    /// <summary>
    /// Deletes current Element from the Database
    /// </summary>
    /// <param name="sender">UI Element that send Event.</param>
    /// <param name="e">UI Event triggering Method call</param>
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (list_view_states.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Status Selected", "Select the status you want to delete from the list", "OK");
            return;
        }

        await statusService.DeleteStatusAsync(selectedStatus);
        states.Remove(selectedStatus);

        reset_SelectedStatus_and_Text();
    }

    /// <summary>
    /// Method to update currently selected OperationalTeamStatus
    /// </summary>
    /// <param name="sender">UI Element that send Event.</param>
    /// <param name="e">UI Event triggering Method call</param>
    private void select_Item_from_ListView(object sender, SelectedItemChangedEventArgs e)
    {
        selectedStatus = e.SelectedItem as OperationalTeamStatus;
        if (selectedStatus == null) return;

        text_editor_status.Text = selectedStatus.Name;
    }

    /// <summary>
    /// Updates the currently saved OperationalTeamStatus
    /// to its new value
    /// </summary>
    private void updateStatus()
    {
        selectedStatus.Name = text_editor_status.Text;
        statusService.UpdateStatusAsync(selectedStatus);
        var status = states.FirstOrDefault(x => x.ID == selectedStatus.ID);
        status.Name = text_editor_status.Text;
    }

    /// <summary>
    /// Creates new OperationalTeamStatus and adds it to teh database
    /// </summary>
    private void createNewStatus()
    {
        var status = new OperationalTeamStatus() { Name = text_editor_status.Text };
        statusService.AddStatus(status);
        states.Add(status);
    }

    /// <summary>
    /// Resets selectedStatus to null, clears the Text editor Field
    /// and sets list_view_states.SelectedItem to null
    /// </summary>
    private void reset_SelectedStatus_and_Text()
    {
        selectedStatus = null;
        list_view_states.SelectedItem = null;
        text_editor_status.Text = "";
    }
}