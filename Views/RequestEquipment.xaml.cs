using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views
{ 
public partial class RequestEquipment : ContentPage
{
    // Private fields for service instances and observable collections
    private EquipmentService Equipmentservice = new EquipmentService();
    private LogisticsService operationService = new LogisticsService();
    public AModel SelectedEquipment { get; set; }
    public ObservableCollection<AModel> Equipments { get; set; } = new ObservableCollection<AModel>();
    public ObservableCollection<AModel> Operations { get; set; } = new ObservableCollection<AModel>();
    public ObservableCollection<AModel> RequestedEquipment { get; set; } = new ObservableCollection<AModel>();

        /* Constructor for the RequestEquipment class*/
        public RequestEquipment()
    {
        /* Initialize the component, start the asynchronous initialization, and set the BindingContext*/

        InitializeComponent();
        _ = InitializeAsync();
        BindingContext = this;
        equipmentPicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        operationPicker.SelectedIndexChanged += OnOperationPickerSelectedIndexChanged;
    }

    private async Task InitializeAsync()
    {
        // Retrieve all team members and operations asynchronously
        var equipment = await Equipmentservice.GetAll();
        var operations = await operationService.GetAll();
        // Populate the Equipments and Operations collections
        foreach (var equipments in equipment)
        {
            Equipments.Add(equipments);
        }
        foreach (var operation in operations)
        {
            Operations.Add(operation);

        }
    }
    // Event handler for the "Add" button click
    private void OnAddClicked(object sender, EventArgs e)
    {
        try
        {
            // Check if a team member and operation are selected
            if (SelectedEquipment != null && operationPicker.SelectedIndex >= 0)
            {
                // Check the type of the selected operation
                if (Operations[operationPicker.SelectedIndex] is LogisticsOperation selectedOperation)
                {
                    // Create a new object to represent the request
                    AModel request = null;


                    var requestName = $"{SelectedEquipment.Name} - {selectedOperation.Name}";

                    // Instantiate the appropriate concrete class based on the type of the selected operation
                    if (selectedOperation is Equipment)
                    {
                        request = new Equipment { Name = requestName };
                    }
                    else if (selectedOperation is LogisticsOperation)
                    {
                        request = new LogisticsOperation { Name = requestName };
                    }



                    // Add the request to the RequestedEquipments list
                    RequestedEquipment.Add(request);

                    // Clear the selected items in the pickers
                    equipmentPicker.SelectedIndex = -1;
                    operationPicker.SelectedIndex = -1;
                }
            }
            else
            {

                throw new InvalidOperationException("Please select both a team member and an operation before adding.");
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine($"An error occurred: {ex.Message}");

        }
    }




    // Event handler for item selection in the RequestedEquipments list

    private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            var SelectedEquipment = e.SelectedItem as AModel;

        }
    }
    // Event handler for the picker selection change
    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (equipmentPicker.SelectedIndex >= 0)
        {
            SelectedEquipment = Equipments[equipmentPicker.SelectedIndex];

            // Assuming you have a label named 'teamMemberDetailsLabel' in your XAML
        }
    }

    // Event handler for the operation picker selection change
    private void OnOperationPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        if (operationPicker.SelectedIndex >= 0)
        {
            var selectedOperation = Operations[operationPicker.SelectedIndex];
            
        }
    }
}
}