using CommunityToolkit.Mvvm.DependencyInjection;
using System.Collections.ObjectModel;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.Views;
public partial class EquipmentPage : ContentPage
{
	private ILogisticsService logisticsService;
    private IBuildingTypeService buildingTypeService;
	private IEquipmentService equipmentService;


    private ObservableCollection<AnEquipment> EquipmentList { get; set; } = new ObservableCollection<AnEquipment>();
    private ObservableCollection<BuildingType> BuildingList { get; set; } = new ObservableCollection<BuildingType>();
    private ObservableCollection<LogisticsOperation> OperationList { get; set; } = new ObservableCollection<LogisticsOperation>();

    private AnEquipment? selectedEquipment = null;

    public EquipmentPage()
	{
		InitializeComponent();
		BindingContext = this;

        this.equipmentService = new EquipmentService();
        this.buildingTypeService = new BuildingTypeService();
		this.logisticsService = new LogisticsService();
    }

	protected async override void OnAppearing()
	{
		ClearInput();

        base.OnAppearing();
		await PopulateEquipmentTypes();
	}

	private void ClearInput()
	{
        EquipmentName.Text = String.Empty;
        NumberRequiredEntry.Text = String.Empty;
        EquipmentTypePicker.SelectedIndex = 0;
    }

	private void OnAddRecordClicked(object sender, EventArgs e)
	{
		AddNewEquipmentTypeRequest();
	}

	private void OnUpdateClicked(object sender, EventArgs e)
	{
		try
		{
            AnEquipment eq = GetCurrentEquipment();
            if (selectedEquipment is null || eq is null) return;

            selectedEquipment.Name = eq.Name;
            selectedEquipment.Type = eq.Type; ;
            selectedEquipment.Location = eq.Location;
            selectedEquipment.CurrentOperation = eq.CurrentOperation;
            selectedEquipment.Quantity = eq.Quantity;
            equipmentService.Update(selectedEquipment);
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
            if (selectedEquipment is null) return;
            equipmentService.Remove(selectedEquipment);
			EquipmentList.Remove(selectedEquipment);
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to remove record. Error: {ex.Message}", "OK");
            return;
        }

    }

    private AnEquipment GetCurrentEquipment()
    {
        String? type = EquipmentTypePicker.SelectedItem as String;
        BuildingType? loc = BuildingTypePicker.SelectedItem as BuildingType;
        LogisticsOperation? op = OperationListPicker.SelectedItem as LogisticsOperation;

        if (String.IsNullOrEmpty(type)
            || String.IsNullOrEmpty(op.Name)
            || String.IsNullOrEmpty(loc.Name)
            || String.IsNullOrEmpty(EquipmentName.Text)
            || String.IsNullOrEmpty(NumberRequiredEntry.Text)) return null;

        AnEquipment eq = new AnEquipment();

        eq.Name = EquipmentName.Text;
        eq.Type = type;
        eq.Location = loc.Name;
        eq.CurrentOperation = op.Name;
        eq.Quantity = Int32.Parse(NumberRequiredEntry.Text);

        return eq;
    }

    private void AddNewEquipmentTypeRequest()
	{
		try
		{
            AnEquipment eq = GetCurrentEquipment();
            if (eq == null) return;

            equipmentService.Add(eq);
			EquipmentList.Add(eq);
            DisplayAlert("Success", $"Successfully inserted record.", "OK");
		}
		catch (Exception ex)
		{
			DisplayAlert("Error", $"Failed to insert record. Error: {ex.Message}", "OK");
			return;
		}
	}

	private async Task PopulateEquipmentTypes()
	{
		try
		{
			OperationList = new ObservableCollection<LogisticsOperation>(await logisticsService.GetAll());

			LogisticsOperation defaultOperation = new LogisticsOperation();
			defaultOperation.Name = "No Logistic Mission Assigned";

			OperationList.Insert(0, defaultOperation);
            OperationListPicker.ItemsSource = OperationList;
            OperationListPicker.SelectedIndex = 0;


			BuildingList = new ObservableCollection<BuildingType>(await buildingTypeService.GetAll());
			BuildingTypePicker.ItemsSource = BuildingList;
            if (BuildingList.Count > 0) BuildingTypePicker.SelectedIndex = 0;

            EquipmentList = new ObservableCollection<AnEquipment>(await equipmentService.GetAll());
            ltv_equipments.ItemsSource = EquipmentList;
        }
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Failed to load EquipmentTypeRequests. Error: {ex.Message}", "OK");
			return;
		}
	}

	private void ltv_equipments_ItemSelected(object sender, EventArgs e)
	{
        selectedEquipment = ltv_equipments.SelectedItem as AnEquipment;
		if (selectedEquipment is null) return;

		EquipmentName.Text = selectedEquipment.Name;
		NumberRequiredEntry.Text = selectedEquipment.Quantity.ToString();

        EquipmentTypePicker.SelectedIndex = EquipmentTypePicker.Items.IndexOf(selectedEquipment.Type);
		BuildingTypePicker.SelectedIndex = BuildingTypePicker.Items.IndexOf(selectedEquipment.Location);
        OperationListPicker.SelectedIndex = OperationListPicker.Items.IndexOf(selectedEquipment.CurrentOperation);
    }

    private void UpdateFilter()
    {
		string? filterType = filterPicker.SelectedItem.ToString();
        string? selectedFilter = searchEntry.Text;
        if (EquipmentList.Count < 1 || String.IsNullOrEmpty(searchEntry.Text) 
			|| String.IsNullOrEmpty(filterType))
        {
            ltv_equipments.ItemsSource = EquipmentList;
            return;
        }

		if (filterType.Equals("Type"))
        {
            ltv_equipments.ItemsSource = EquipmentList.Where(r => r.Type.ToLower().Contains(selectedFilter.ToLower()));
        }
		else if (filterType.Equals("Location"))
		{
            ltv_equipments.ItemsSource = EquipmentList.Where(r => r.Location.ToLower().Contains(selectedFilter.ToLower()));
        }
		else
		{
            ltv_equipments.ItemsSource = EquipmentList.Where(r => r.Name.ToLower().Contains(selectedFilter.ToLower()));
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
