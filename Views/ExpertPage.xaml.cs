using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace UndacApp.Views;



public partial class ExpertPage : ContentPage
{
    Expert selectedExpert = null;
    IExpertService expertService;
    ObservableCollection<Expert> experts = new ObservableCollection<Expert>();

    public ExpertPage()
    {
        InitializeComponent();
        BindingContext = new SystemType();
        this.expertService = new ExpertService();


        Task.Run(async () => await LoadExpert());

        txe_expert.Text = "";
    }

    private async Task LoadExpert()
    {
        try {
            experts = new ObservableCollection<Expert>(await expertService.GetExpertsList());
            ltv_expert.ItemsSource = experts;
        }
        catch (Exception e) {
            Debug.WriteLine(e.Message);
        }
    }

    /*! <summary>
     * Event handler for the Save button click event.
     * </summary>
     */
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_expert.Text)) return;

        if (selectedExpert == null)
        {            
            var systemType = new Expert() { Skill = txe_expert.Text , Location = "", Status = ""};
            expertService.AddExpert(systemType);
            experts.Add(systemType);
        }

        else
        {
            selectedExpert.Skill = txe_expert.Text;
            expertService.UpdateExpert(selectedExpert);
            var systemType = experts.FirstOrDefault(x => x.Skill == selectedExpert.Skill);
            systemType.Skill = txe_expert.Text;
        }

        selectedExpert = null;
        ltv_expert.SelectedItem = null;
        txe_expert.Text = "";
    }
    /*! <summary>
    * Event handler for the Delete button click event
    * </summary>
    */
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_expert.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Expert Selected", "Select the Expert you want to delete from the list", "Ok");
            return;
        }

        await expertService.DeleteExpert(selectedExpert);
        experts.Remove(selectedExpert);


        ltv_expert.SelectedItem = null;
        txe_expert.Text = "";
    }


    /*! <summary>
     * Event handler for the selection of an item in the ListView.
     * </summary>
     */
    private void ltv_expert_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedExpert = e.SelectedItem as Expert;
        if (selectedExpert == null) return;
        txe_expert.Text = selectedExpert.Skill;
    }
}