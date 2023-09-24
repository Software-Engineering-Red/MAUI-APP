
using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class ContinentPage : ContentPage {
    BuildingType selectedContinent = null;
    IContinentService continentService;
    ObservableCollection<BuildingType> continents = new ObservableCollection<BuildingType>();

    public ContinentPage() {
        InitializeComponent();
        this.BindingContext = this;
        this.continentService = new ContinentService();

        Task.Run(async () => await LoadContinents());
        txe_continent.Text = "";
    }

    private async Task LoadContinents() {
        continents = new ObservableCollection<BuildingType>(await continentService.GetContinentList());
        ltv_continents.ItemsSource = continents;
    }

    private void SaveButton_Clicked(object sender, EventArgs e) {
        if (String.IsNullOrEmpty(txe_continent.Text)) return;

        if(selectedContinent == null) {
            var continent = new BuildingType() { Name=txe_continent.Text};
            continentService.AddContinent(continent);
            continents.Add(continent);
        } else {
            selectedContinent.Name = txe_continent.Text;
            continentService.UpdateContinent(selectedContinent);
            var continent = continents.FirstOrDefault(x => x.ID == selectedContinent.ID);
            continent.Name = txe_continent.Text;
        }

        
        selectedContinent = null;
        ltv_continents.SelectedItem = null;
        txe_continent.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e) {
        if(ltv_continents.SelectedItem == null) {
            await Shell.Current.DisplayAlert("No Continent Selected", "Select the continent you want to delete from the list", "OK");
            return;
        }

        await continentService.DeleteContinent(selectedContinent);
        continents.Remove(selectedContinent);

        ltv_continents.SelectedItem = null;
        txe_continent.Text = "";
    }

    private void ltv_continents_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
        selectedContinent = e.SelectedItem as BuildingType;
        if (selectedContinent == null) return;

        txe_continent.Text = selectedContinent.Name;
    }
}