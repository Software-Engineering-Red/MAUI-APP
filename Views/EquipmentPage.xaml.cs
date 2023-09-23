
using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;

public partial class EquipmentPage : ContentPage
{
    Equipment selectedEquipment = null;
    IEquipmentService equipmentService;
    ObservableCollection<Equipment> equipments = new ObservableCollection<Equipment>();

    public EquipmentPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        this.equipmentService = new EquipmentService();

        Task.Run(async () => await LoadRoles());
        txe_equipment.Text = "";
    }

    private async Task LoadRoles()
    {
        equipments = new ObservableCollection<Equipment>(await equipmentService.GetEquipmentList());
        ltv_equipment.ItemsSource = equipments;
    }

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

    private void ltv_equipment_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedEquipment = e.SelectedItem as Equipment;
        if (selectedEquipment == null) return;

        txe_equipment.Text = selectedEquipment.Name;
    }
}