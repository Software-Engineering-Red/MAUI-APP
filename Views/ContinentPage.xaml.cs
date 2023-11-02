
using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

/*! <summary>
        ContinentPage class extending ContentPage, responsible for functionality on ContinentPage view.
    </summary> */
public partial class ContinentPage : ContentPage {
    /*! <summary>
        A reference pointer for storing currently selected Continent.
     </summary> */
    private Continent selectedContinent = null;

    /*! <summary>
        An instance of IContinentService
     </summary> */
    IContinentService continentService;

    /*! <summary>
        Collection of current Continents.
    </summary> */
    ObservableCollection<Continent> continents = new ObservableCollection<Continent>();

    /*! <summary>
        Constructor class, setting the binding context and initiating the Continent serrvice, as well as loading the Continent list.
    </summary> */
    public ContinentPage() {
        InitializeComponent();
        this.BindingContext = this;
        this.continentService = new ContinentService();

        Task.Run(async () => await LoadContinents());
        txe_continent.Text = "";
    }

    /*! <summary>
            Private method loading the Continent list using ContinentService getter.
        </summary> 
        <returns>Task promise, informing about the status of its' completion.</returns> */
    private async Task LoadContinents() {
        continents = new ObservableCollection<Continent>(await continentService.GetAll());
        ltv_continents.ItemsSource = continents;
    }

    /*! <summary>
            Method responsible for saving Continent into SQLite database, triggered by selection of save button.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void SaveButton_Clicked(object sender, EventArgs e) {
        if (String.IsNullOrEmpty(txe_continent.Text)) return;

        if(selectedContinent == null) {
            var continent = new Continent() { Name=txe_continent.Text};
            continentService.Add(continent);
            continents.Add(continent);
        } else {
            selectedContinent.Name = txe_continent.Text;
            continentService.Update(selectedContinent);
            var continent = continents.FirstOrDefault(x => x.ID == selectedContinent.ID);
            continent.Name = txe_continent.Text;
        }

        
        selectedContinent = null;
        ltv_continents.SelectedItem = null;
        txe_continent.Text = "";
    }

    /*! <summary>
             Method responsible for removing Continent from SQLite database, triggered by selection of delete button.
             Note: If no Continent is selected, no Continent will be removed.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private async void DeleteButton_Clicked(object sender, EventArgs e) {
        if(ltv_continents.SelectedItem == null) {
            await Shell.Current.DisplayAlert("No Continent Selected", "Select the continent you want to delete from the list", "OK");
            return;
        }

        await continentService.Remove(selectedContinent);
        continents.Remove(selectedContinent);

        ltv_continents.SelectedItem = null;
        txe_continent.Text = "";
    }

    /*! <summary>
            Method responsible for updating currently selected item, integrating UI and Backend functionality.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void ltv_continents_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
        selectedContinent = e.SelectedItem as Continent;
        if (selectedContinent == null) return;

        txe_continent.Text = selectedContinent.Name;
    }
}