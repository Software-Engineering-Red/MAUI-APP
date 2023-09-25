using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;


public partial class SystemPage : ContentPage
{
    SystemType selectedSystem = null;
    ISystemTypeService systemTypeService;
    ObservableCollection<SystemType> systemTypes = new ObservableCollection<SystemType>();

    public SystemPage()
    {
        InitializeComponent();
        this.BindingContext = this;
        this.systemTypeService = new SystemTypeService();

        Task.Run(async () => await LoadSystemType());
        txe_systemType.Text = "";
    }

    private async Task LoadSystemType()
    {
        systemTypes = new ObservableCollection<SystemType>(await systemTypeService.GetSystemTypeList());
        ltv_systemType.ItemsSource = systemTypes;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_systemType.Text)) return;

        if (selectedSystem == null)
        {
            var systemType = new SystemType() { Name = txe_systemType.Text };
            systemTypeService.AddSystemType(systemType);
            systemTypes.Add(systemType);
        }
        else
        {
            selectedSystem.Name = txe_systemType.Text;
            systemTypeService.UpdateSystemType(selectedSystem);
            var systemType = systemTypes.FirstOrDefault(x => x.type == selectedSystem.type);
            systemType.Name = txe_systemType.Text;
        }


        selectedSystem = null;
        ltv_systemType.SelectedItem = null;
        txe_systemType.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_systemType.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No System Type Selected", "Select the system type you want to delete from the list", "Ok");
            return;
        }

        await systemTypeService.DeleteSystemType(selectedSystem);
        systemTypes.Remove(selectedSystem);

        ltv_systemType.SelectedItem = null;
        txe_systemType.Text = "";
    }

    private void ltv_systemType_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedSystem = e.SelectedItem as SystemType;
        if (selectedSystem == null) return;

        txe_systemType.Text = selectedSystem.Name;
    }
}