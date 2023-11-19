using System.Collections.ObjectModel;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.Views;
public partial class AddResourcePage : ContentPage
{
	private IResourceTypeService resourceTypeService;
    private IResourceService resourceService;
	private IBuildingTypeService buildingTypeService;

    private ObservableCollection<ResourceType> ResourceTypes { get; set; } = new ObservableCollection<ResourceType>();
    private ObservableCollection<AResource> ResourceList { get; set; } = new ObservableCollection<AResource>();
    private ObservableCollection<BuildingType> BuildingList { get; set; } = new ObservableCollection<BuildingType>();

	private AResource? selectedResource = null;

    public AddResourcePage()
	{
		InitializeComponent();
		BindingContext = this;

		this.resourceTypeService = new ResourceTypeService();
        this.resourceService = new ResourceService();
        this.buildingTypeService = new BuildingTypeService();
    }

	protected async override void OnAppearing()
	{
		ClearInput();

        base.OnAppearing();
		await PopulateResourceTypes();
	}

	private void ClearInput()
	{
        ResourceName.Text = String.Empty;
        NumberRequiredEntry.Text = String.Empty;
        ResourceTypePicker.SelectedIndex = 0;
    }

	private void OnAddRecordClicked(object sender, EventArgs e)
	{
		AddNewResourceTypeRequest();
	}

	private void OnUpdateClicked(object sender, EventArgs e)
	{
		try
		{
            if (selectedResource is null) return;

            ResourceType? type = ResourceTypePicker.SelectedItem as ResourceType;
            BuildingType? loc = BuildingTypePicker.SelectedItem as BuildingType;
            if (String.IsNullOrEmpty(type.Name)
                || String.IsNullOrEmpty(loc.Name)
                || String.IsNullOrEmpty(ResourceName.Text)
                || String.IsNullOrEmpty(NumberRequiredEntry.Text)) return;

            selectedResource.Name = ResourceName.Text;
			selectedResource.Quantity = Int32.Parse(NumberRequiredEntry.Text);
			selectedResource.Type = type.Name;
			selectedResource.Location = loc.Name;

            resourceService.Update(selectedResource);
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to update record. Error: {ex.Message}", "OK");
            return;
        }
    }

    private void OnRemoveClicked(object sender, EventArgs e)
    {
        try
        {
            if (selectedResource is null) return;
            resourceService.Update(selectedResource);
			ResourceList.Remove(selectedResource);
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to remove record. Error: {ex.Message}", "OK");
            return;
        }

    }

    private void AddNewResourceTypeRequest()
	{
		try
		{
            ResourceType? type = ResourceTypePicker.SelectedItem as ResourceType;
            BuildingType? loc = BuildingTypePicker.SelectedItem as BuildingType;
            if (String.IsNullOrEmpty(type.Name)
				|| String.IsNullOrEmpty(loc.Name)
                || String.IsNullOrEmpty(ResourceName.Text)
                || String.IsNullOrEmpty(NumberRequiredEntry.Text)) return;

            AResource resource = new AResource();
				
            resource.Name = ResourceName.Text;
			resource.Type = type.Name;
			resource.Location = loc.Name;
			resource.Quantity = Int32.Parse(NumberRequiredEntry.Text);

			resourceService.Add(resource);
			ResourceList.Add(resource);
            DisplayAlert("Success", $"Successfully inserted record.", "OK");
			
		}
		catch (Exception ex)
		{
			DisplayAlert("Error", $"Failed to insert record. Error: {ex.Message}", "OK");
			return;
		}
	}

	private int GetNumberRequired()
	{
		try
		{
			return Convert.ToInt32(NumberRequiredEntry.Text);
		}
		catch 
		{
			DisplayAlert("Error", $"Failed to insert record into skills. Error: Number of Helpers required is not a number", "OK");
			NumberRequiredEntry.Text = "";
			return 0;
		}
	}

	private async Task PopulateResourceTypes()
	{
		try
		{
			ResourceTypes = new ObservableCollection<ResourceType>(await resourceTypeService.GetAll());
			ResourceTypePicker.ItemsSource = ResourceTypes;
			if (ResourceTypes.Count > 0) ResourceTypePicker.SelectedIndex = 0;

            ResourceList = new ObservableCollection<AResource>(await resourceService.GetAll());
			ltv_resources.ItemsSource = ResourceList;

			BuildingList = new ObservableCollection<BuildingType>(await buildingTypeService.GetAll());
			BuildingTypePicker.ItemsSource = BuildingList;
            if (BuildingList.Count > 0) BuildingTypePicker.SelectedIndex = 0;
        }
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Failed to load ResourceTypeRequests. Error: {ex.Message}", "OK");
			return;
		}
	}

	private void ltv_resources_ItemSelected(object sender, EventArgs e)
	{
		selectedResource = ltv_resources.SelectedItem as AResource;
		if (selectedResource is null) return;

		ResourceName.Text = selectedResource.Name;
		NumberRequiredEntry.Text = selectedResource.Quantity.ToString();

		ResourceTypePicker.SelectedIndex = ResourceTypePicker.Items.IndexOf(selectedResource.Type);
		BuildingTypePicker.SelectedIndex = BuildingTypePicker.Items.IndexOf(selectedResource.Location);
	}

    private void UpdateFilter()
    {
		string? filterType = filterPicker.SelectedItem.ToString();
        string? selectedFilter = searchEntry.Text;
        if (ResourceTypes.Count < 1 || ResourceList.Count < 1 || String.IsNullOrEmpty(searchEntry.Text) 
			|| String.IsNullOrEmpty(filterType))
        {
			ltv_resources.ItemsSource = ResourceList;
            return;
        }

		if (filterType.Equals("Type"))
        {
            ltv_resources.ItemsSource = ResourceList.Where(r => r.Type.ToLower().Contains(selectedFilter.ToLower()));
        }
		else if (filterType.Equals("Location"))
		{
            ltv_resources.ItemsSource = ResourceList.Where(r => r.Location.ToLower().Contains(selectedFilter.ToLower()));
        }
		else
		{
            ltv_resources.ItemsSource = ResourceList.Where(r => r.Name.ToLower().Contains(selectedFilter.ToLower()));
        }
    }

	void OnEntryTextChanged(object sender, TextChangedEventArgs e)
	{
		UpdateFilter();
	}

   private void Button_Clicked(object sender, EventArgs e)
    {
        UpdateFilter();
    }
}
