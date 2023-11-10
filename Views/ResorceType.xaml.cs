using UndacApp.Models;
using System.Collections.ObjectModel;
using UndacApp.Services;

namespace UndacApp.Views;

/*! <summary>
        ResourceTypePage class extending ContentPage, responsible for functionality on ResourceTypePage view.
    </summary> */
public partial class ResourceTypePage : ContentPage
{
/*! <summary>
        A reference pointer for storing currently selected ResourceType.
     </summary> */
    ResourceType? selectedOrg = null;

/*! <summary>
        An instance of IResourceTypeService
     </summary> */
    readonly IResourceTypeService resource_typeService;

/*! <summary>
        Collection of current ResourceTypes.
    </summary> */
    ObservableCollection<ResourceType> orgs = new();


/*! <summary>
        Constructor class, setting the binding context and initiating the resource_type serrvice, as well as loading the resource_type list.
    </summary> */
    public ResourceTypePage()
    {
        InitializeComponent();
        BindingContext = new ResourceType();
        this.resource_typeService = new ResourceTypeService();

        Task.Run(async () => await LoadResourceTypes());
        txe_resource_type.Text = "";
    }

    /*! <summary>
            Private method loading the ResourceType list using resource_typeService getter.
        </summary> 
        <returns>Task promise, informing about the status of its' completion.</returns> */
    private async Task LoadResourceTypes()
    {
        orgs = new ObservableCollection<ResourceType>(await resource_typeService.GetAll());
        ltv_resource_types.ItemsSource = orgs;
    }

    /*! <summary>
            Method responsible for saving resource_type into SQLite database, triggered by selection of save button.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_resource_type.Text)) return;

        if (selectedOrg == null)
        {
            var org = new ResourceType() { Name = txe_resource_type.Text };
            resource_typeService.Add(org);
            orgs.Add(org);
        }
        else
        {
            selectedOrg.Name = txe_resource_type.Text;
            resource_typeService.Update(selectedOrg);

            var org = orgs.FirstOrDefault(x => x.ID == selectedOrg.ID);
            org.Name = txe_resource_type.Text;

        }
        selectedOrg = null;
        ltv_resource_types.SelectedItem = null;
        txe_resource_type.Text = "";
        txe_resource_type.Focus();
    }

    /*! <summary>
             Method responsible for removing resource_type from SQLite database, triggered by selection of delete button.
             Note: If no ResourceType is selected, no resource_type will be removed.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_resource_types.SelectedItem == null)
        {
            await DisplayAlert("No Org Selected", "Select resource_type you want to delete from the list", "OK");
            return;
        }

        await resource_typeService.Remove(selectedOrg);
        orgs.Remove(selectedOrg);

        ltv_resource_types.SelectedItem = null;
        txe_resource_type.Text = "";
        txe_resource_type.Focus();
    }

    /*! <summary>
            Method responsible for updating currently selected item, integrating UI and Backend functionality.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private void Ltv_resource_types_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedOrg = e.SelectedItem as ResourceType;
        if (selectedOrg == null) return;
        txe_resource_type.Text = selectedOrg.Name;
        txe_resource_type.Focus();
    }
}