using UndacApp.Models;
using System.Collections.ObjectModel;
using UndacApp.Services;

namespace UndacApp.Views;

public partial class ResourceTypePage : ContentPage
{

    ResourceType? selectedType = null;
    readonly IResourceTypeService resource_typeService;
    ObservableCollection<ResourceType> resourceTypes = new();

    public ResourceTypePage()
    {
        InitializeComponent();
        BindingContext = new ResourceType();
        this.resource_typeService = new ResourceTypeService();

        Task.Run(async () => await LoadResourceTypes());
        txe_resource_type.Text = "";
    }

    private async Task LoadResourceTypes()
    {
        resourceTypes = new ObservableCollection<ResourceType>(await resource_typeService.GetAll());
        ltv_resource_types.ItemsSource = resourceTypes;
    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_resource_type.Text)) return;

        if (selectedType == null)
        {
            var resourceType = new ResourceType() { Name = txe_resource_type.Text };
            resource_typeService.Add(resourceType);
            resourceTypes.Add(resourceType);
        }
        else
        {
            selectedType.Name = txe_resource_type.Text;
            resource_typeService.Update(selectedType);

            var resourceType = resourceTypes.FirstOrDefault(x => x.ID == selectedType.ID);
            resourceType.Name = txe_resource_type.Text;

        }
        selectedType = null;
        ltv_resource_types.SelectedItem = null;
        txe_resource_type.Text = "";
        txe_resource_type.Focus();
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_resource_types.SelectedItem == null)
        {
            await DisplayAlert("No Type Selected", "Select resource_type you want to delete from the list", "OK");
            return;
        }

        await resource_typeService.Remove(selectedType);
        resourceTypes.Remove(selectedType);

        ltv_resource_types.SelectedItem = null;
        txe_resource_type.Text = "";
        txe_resource_type.Focus();
    }

    private void Ltv_resource_types_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedType = e.SelectedItem as ResourceType;
        if (selectedType == null) return;
        txe_resource_type.Text = selectedType.Name;
        txe_resource_type.Focus();
    }
}