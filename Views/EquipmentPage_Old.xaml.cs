
using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views;
/*! <summary>
        EquipmentPage class extending ContentPage, responsible for functionality on EquipmentPage view.
    </summary> */
public partial class EquipmentPage_Old : ContentPage
{
    /*! <summary>
        A reference pointer for storing currently selected Equipment.
     </summary> */
    AnEquipment selectedEquipment = null;
    /*! <summary>
        An instance of IEquipmentService
     </summary> */
    IEquipmentService equipmentService;
    /*! <summary>
        Collection of current Equipments.
    </summary> */
    ObservableCollection<AnEquipment> equipments = new ObservableCollection<AnEquipment>();
    /*! <summary>
        Constructor class, setting the binding context and initiating the equipment service, as well as loading the equipment list.
    </summary> */
    public EquipmentPage_Old()
    {
        InitializeComponent();
        BindingContext =new AnEquipment();
        this.equipmentService = new EquipmentService();

        Task.Run(async () => await LoadEquipment());
        txe_equipment.Text = "";
    }
    /*! <summary>
        Private method loading the Equipment list using equipmentService getter.
    </summary> 
    <returns>Task promise, informing about the status of its' completion.</returns> */
    private async Task LoadEquipment()
    {
        equipments = new ObservableCollection<AnEquipment>(await equipmentService.GetAll());
        ltv_equipment.ItemsSource = equipments;
    }
    /*! <summary>
         Method responsible for saving equipment into SQLite database, triggered by selection of save button.
     </summary> 
     <param name="sender">Details about the element that triggered the event.</param>
     <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_equipment.Text)) return;

        if (selectedEquipment == null)
        {
            var role = new AnEquipment() { Name = txe_equipment.Text };
            equipmentService.Add(role);
            equipments.Add(role);
        }
        else
        {
            selectedEquipment.Name = txe_equipment.Text;
            equipmentService.Update(selectedEquipment);
            var equipment = equipments.FirstOrDefault(x => x.ID == selectedEquipment.ID);
            equipment.Name = txe_equipment.Text;
        }


        selectedEquipment = null;
        ltv_equipment.SelectedItem = null;
        txe_equipment.Text = "";
    }
    /*! <summary>
         Method responsible for removing organisation from SQLite database, triggered by selection of delete button.
         Note: If no Organisation is selected, no organisation will be removed.
    </summary> 
    <param name="sender">Details about the element that triggered the event.</param>
    <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_equipment.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Equipment Selected", "Select the role you want to delete from the list", "OK");
            return;
        }

        await equipmentService.Remove(selectedEquipment);
        equipments.Remove(selectedEquipment);

        ltv_equipment.SelectedItem = null;
        txe_equipment.Text = "";
    }

    /*! <summary>
            Method responsible for updating currently selected item, integrating UI and Backend functionality.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void ltv_equipment_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedEquipment = e.SelectedItem as AnEquipment;
        if (selectedEquipment == null) return;

        txe_equipment.Text = selectedEquipment.Name;
    }
}