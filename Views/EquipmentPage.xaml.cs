
using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views;
/*! <summary>
        EquipmentPage class extending ContentPage, responsible for functionality on EquipmentPage view.
    </summary> */
public partial class EquipmentPage : ContentPage
{
    /*! <summary>
        A reference pointer for storing currently selected Equipment.
     </summary> */
    Equipment selectedEquipment = null;
    /*! <summary>
        An instance of IEquipmentService
     </summary> */
    IEquipmentService equipmentService;
    /*! <summary>
        Collection of current Equipments.
    </summary> */
    ObservableCollection<Equipment> equipments = new ObservableCollection<Equipment>();
    /*! <summary>
        Constructor class, setting the binding context and initiating the equipment service, as well as loading the equipment list.
    </summary> */
    public EquipmentPage()
    {
        InitializeComponent();
        BindingContext = new Equipment();
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
        equipments = new ObservableCollection<Equipment>(await equipmentService.GetEquipmentList());
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
            var role = new Equipment() { Name = txe_equipment.Text };
            equipmentService.AddEquipment(role);
            equipments.Add(role);
        }
        else
        {
            selectedEquipment.Name = txe_equipment.Text;
            equipmentService.UpdateEquipment(selectedEquipment);
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

        await equipmentService.DeleteEquipment(selectedEquipment);
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
        selectedEquipment = e.SelectedItem as Equipment;
        if (selectedEquipment == null) return;

        txe_equipment.Text = selectedEquipment.Name;
    }

    /// <summary>
    /// Handles the click event of the Reserve button for each equipment item.
    /// </summary>
    /// <remarks>
    /// When the Reserve button is clicked, this method retrieves the equipment ID from the button's
    /// CommandParameter. It then calls the ReserveEquipment method to attempt to reserve the equipment.
    /// An alert is displayed to the user indicating whether the reservation was successful.
    /// </remarks>
    /// <param name="sender">The source of the event, typically the Reserve button.</param>
    /// <param name="e">Event data that provides information and event data associated with routed events.</param>
    private async void ReserveButton_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            var equipmentId = (int)button.CommandParameter;

            // Simulate reservation logic
            var isReserved = ReserveEquipment(equipmentId);

            var message = isReserved ? "Equipment reserved for you" : "Reservation failed";
            await DisplayAlert("Reservation", message, "OK");
        }
    }

    /// <summary>
    /// Simulates the reservation of an equipment item.
    /// </summary>
    /// <remarks>
    /// This method is a placeholder for actual reservation logic. It should be replaced with
    /// a real implementation, such as updating the reservation status in a database.
    /// </remarks>
    /// <param name="equipmentId">The ID of the equipment to be reserved.</param>
    /// <returns>Returns true if the reservation is successful, false otherwise.</returns>
    private bool ReserveEquipment(int equipmentId)
    {
        var equipment = equipments.FirstOrDefault(e => e.ID == equipmentId);
        if (equipment != null && !equipment.Reserved)
        {
            equipment.Reserved = true;
            equipmentService.UpdateEquipment(equipment); // Update the equipment in your service or database
            return true; // Reservation successful
        }
        return false; // Reservation failed
    }
}