
using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class BuildingTypePage : ContentPage {
    BuildingType selectedBuildingType = null;
    IBuildingTypeService buildingTypeService;
    ObservableCollection<BuildingType> buildingTypes = new ObservableCollection<BuildingType>();

    public BuildingTypePage() {
        InitializeComponent();
        this.BindingContext = this;
        this.buildingTypeService = new BuildingTypeService();

        Task.Run(async () => await LoadContinents());
        txe_buildingtype.Text = "";
    }

    private async Task LoadContinents() {
        buildingTypes = new ObservableCollection<BuildingType>(await buildingTypeService.GetBuildingTypeList());
        ltv_buildingtype.ItemsSource = buildingTypes;
    }

    private void SaveButton_Clicked(object sender, EventArgs e) {
        if (String.IsNullOrEmpty(txe_buildingtype.Text)) return;

        if(selectedBuildingType == null) {
            var continent = new BuildingType() { Name = txe_buildingtype.Text};
            buildingTypeService.AddBuildingType(continent);
            buildingTypes.Add(continent);
        } else {
            selectedBuildingType.Name = txe_buildingtype.Text;
            buildingTypeService.UpdateBuildingType(selectedBuildingType);
            var continent = buildingTypes.FirstOrDefault(x => x.ID == selectedBuildingType.ID);
            continent.Name = txe_buildingtype.Text;
        }

        
        selectedBuildingType = null;
        ltv_buildingtype.SelectedItem = null;
        txe_buildingtype.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e) {
        if(ltv_buildingtype.SelectedItem == null) {
            await Shell.Current.DisplayAlert("No Building Type Selected", "Select the building type you want to delete from the list", "OK");
            return;
        }

        await buildingTypeService.DeleteBuildingType(selectedBuildingType);
        buildingTypes.Remove(selectedBuildingType);

        ltv_buildingtype.SelectedItem = null;
        txe_buildingtype.Text = "";
    }

    private void ltv_buildingtypes_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
        selectedBuildingType = e.SelectedItem as BuildingType;
        if (selectedBuildingType == null) return;

        txe_buildingtype.Text = selectedBuildingType.Name;
    }
}