using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using Microsoft.Maui.Controls; 

namespace UndacApp.Views
{
    public partial class DisasterManagementCoordination : ContentPage
    {
        // Private fields for service instances and observable collections
        private TeamMemberService teamMemberService = new TeamMemberService();
        private LogisticsService operationService = new LogisticsService(); 
        public AModel SelectedTeamMember { get; set; }
        public ObservableCollection<AModel> TeamMembers { get; set; } = new ObservableCollection<AModel>();
        public ObservableCollection<AModel> Operations { get; set; } = new ObservableCollection<AModel>(); 
        public ObservableCollection<AModel> RequestedTeamMembers { get; set; } = new ObservableCollection<AModel>();

        // Constructor for the DisasterManagementCoordination class
        public DisasterManagementCoordination()
    {
            // Initialize the component, start the asynchronous initialization, and set the BindingContext

            InitializeComponent();
            _ = InitializeAsync();            
            BindingContext = this;
            // Event handlers for picker selection changes
            teamMemberPicker.SelectedIndexChanged += OnPickerSelectedIndexChanged;
            operationPicker.SelectedIndexChanged += OnOperationPickerSelectedIndexChanged;
    }

        private async Task InitializeAsync()
        {
            // Retrieve all team members and operations asynchronously
            var members = await teamMemberService.GetAll();
            var operations = await operationService.GetAll(); 
            // Populate the TeamMembers and Operations collections
            foreach (var teamMember in members)
            {
                TeamMembers.Add(teamMember);
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
                if (SelectedTeamMember != null && operationPicker.SelectedIndex >= 0)
                {
                    // Check the type of the selected operation
                    if (Operations[operationPicker.SelectedIndex] is LogisticsOperation selectedOperation)
                    {
                        // Create a new object to represent the request
                        AModel request = null;

                        
                        var requestName = $"{SelectedTeamMember.Name} - {selectedOperation.Name}";

                        // Instantiate the appropriate concrete class based on the type of the selected operation
                        if (selectedOperation is TeamMember)
                        {
                            request = new TeamMember { Name = requestName };
                        }
                        else if (selectedOperation is LogisticsOperation)
                        {
                            request = new LogisticsOperation { Name = requestName };
                        }

                        

                        // Add the request to the RequestedTeamMembers list
                        RequestedTeamMembers.Add(request);

                        // Clear the selected items in the pickers
                        teamMemberPicker.SelectedIndex = -1;
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




        // Event handler for item selection in the RequestedTeamMembers list

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedTeamMember = e.SelectedItem as AModel;
                
            }
        }
        // Event handler for the team member picker selection change
        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (teamMemberPicker.SelectedIndex >= 0)
            {
                SelectedTeamMember = TeamMembers[teamMemberPicker.SelectedIndex];

                // Assuming you have a label named 'teamMemberDetailsLabel' in your XAML
            }
        }

        // Event handler for the operation picker selection change
        private void OnOperationPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            if (operationPicker.SelectedIndex >= 0)
            {
                var selectedOperation = Operations[operationPicker.SelectedIndex];
                // Handle the selected operation as needed
            }
        }
    }
}
