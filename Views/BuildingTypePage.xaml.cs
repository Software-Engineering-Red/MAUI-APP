
using UndacApp.Models;
using System.Collections.ObjectModel;
using UndacApp.Services;

namespace UndacApp.Views;


/// <summary>
/// A page containing logic for handling building types.
/// </summary>
public partial class BuildingTypePage : ContentPage {

    /// <summary>
    /// The currently selected building type.
    /// </summary>
    BuildingType selectedBuildingType = null;

    /// <summary>
    /// An instance of the <see cref="IBuildingTypeService"/>.
    /// </summary>
    IBuildingTypeService buildingTypeService;

    /// <summary>
    /// A list of available building types.
    /// </summary>
    ObservableCollection<BuildingType> buildingTypes = new ObservableCollection<BuildingType>();

    public BuildingTypePage() {
        InitializeComponent();
        BindingContext = new BuildingType();
        this.buildingTypeService = new BuildingTypeService();

        Task.Run(async () => await LoadBuildingTypes());
        txe_buildingtype.Text = "";
    }

    /// <summary>
    /// Load building types from the database.
    /// </summary>
    private async Task LoadBuildingTypes() {
        buildingTypes = new ObservableCollection<BuildingType>(await buildingTypeService.GetAll());
        ltv_buildingtype.ItemsSource = buildingTypes;
    }


    /// <summary>
    /// Event handler for the save button.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void SaveButton_Clicked(object sender, EventArgs e) {
        if (String.IsNullOrEmpty(txe_buildingtype.Text)) return;

        if(selectedBuildingType == null) {
            var continent = new BuildingType() { Name = txe_buildingtype.Text};
            buildingTypeService.Add(continent);
            buildingTypes.Add(continent);
        } else {
            selectedBuildingType.Name = txe_buildingtype.Text;
            buildingTypeService.Update(selectedBuildingType);
            var continent = buildingTypes.FirstOrDefault(x => x.ID == selectedBuildingType.ID);
            continent.Name = txe_buildingtype.Text;
        }

        
        selectedBuildingType = null;
        ltv_buildingtype.SelectedItem = null;
        txe_buildingtype.Text = "";
    }

    /// <summary>
    /// Event handler for the delete button.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private async void DeleteButton_Clicked(object sender, EventArgs e) {
        if(ltv_buildingtype.SelectedItem == null) {
            await Shell.Current.DisplayAlert("No Building Type Selected", "Select the building type you want to delete from the list", "OK");
            return;
        }

        await buildingTypeService.Remove(selectedBuildingType);
        buildingTypes.Remove(selectedBuildingType);

        ltv_buildingtype.SelectedItem = null;
        txe_buildingtype.Text = "";
    }

    /// <summary>
    /// Even handler for the building type list.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The arguments of the event.</param>
    private void ltv_buildingtypes_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
        selectedBuildingType = e.SelectedItem as BuildingType;
        if (selectedBuildingType == null) return;

        txe_buildingtype.Text = selectedBuildingType.Name;
    }
}