using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views;


/*! <summary>
 * SystemPage class extending ContentPage, responsible for functionality on SystemPage view.
 * </summary> */
public partial class SystemPage : ContentPage
{
    /*! <summary>
     * Determine a private field to store the selected system.  
     * </summary> */
    SystemType selectedSystem = null;

    /*! <summary>
     * Declare a service for managing SystemType objects.
     * </summary> */
    ISystemTypeService systemTypeService;

    /*! <summary>
     * Create an ObservableCollection to hold a list of SystemType objects.
     * </summary> */
    ObservableCollection<SystemType> systemTypes = new ObservableCollection<SystemType>();

    /*! <summary>
     * Constructor for the SystemPage class. 
     * </summary>*/
    public SystemPage()
    {
        InitializeComponent();
        BindingContext = new SystemType();       
        this.systemTypeService = new SystemTypeService();

        /*! <summary>
         * Asynchronously load the list of SystemTypes.
         * </summary>
         */
        Task.Run(async () => await LoadSystemType());

        /*! <summary>
         * Initialize the txe_systemType Text property.
         * </summary>
         */
        txe_systemType.Text = "";
    }
      /*! <summary>
       * Asynchronously load SystemType objects from the service and populate the ObservableCollection.
       * </summary>
       */
    private async Task LoadSystemType()
    {
        systemTypes = new ObservableCollection<SystemType>(await systemTypeService.GetSystemTypeList());
        ltv_systemType.ItemsSource = systemTypes;
    }

    /*! <summary>
     * Event handler for the Save button click event.
     * </summary>
     */
    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        /*! <summary>
         * Check if the txe_systemType Text property is empty.
         * </summary>
         */
        if (String.IsNullOrEmpty(txe_systemType.Text)) return;

        /*! <summary>
         * Create a new SystemType with the entered name and add it to the service and ObservableCollection.
         * </summary>
         */
        if (selectedSystem == null)
        {            
            var systemType = new SystemType() { Name = txe_systemType.Text };
            systemTypeService.AddSystemType(systemType);
            systemTypes.Add(systemType);
        }
        /*! <summary>
         * Update the name of the selected SystemType and update it in the service and ObservableCollection.
         * </summary>
         */
        else
        {
            selectedSystem.Name = txe_systemType.Text;
            systemTypeService.UpdateSystemType(selectedSystem);
            var systemType = systemTypes.FirstOrDefault(x => x.type == selectedSystem.type);
            systemType.Name = txe_systemType.Text;
        }
        /*! <summary>
         * Reset the selectedSystem, selected item, and clear the txe_systemType Text property.
         * </summary>
         */
        selectedSystem = null;
        ltv_systemType.SelectedItem = null;
        txe_systemType.Text = "";
    }
    /*! <summary>
    * Event handler for the Delete button click event
    * </summary>
    */
    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_systemType.SelectedItem == null)
        {
            /*! <summary>
             * Display an alert if no SystemType is selected for deletion.
             * </summary>
             */
            await Shell.Current.DisplayAlert("No System Type Selected", "Select the system type you want to delete from the list", "Ok");
            return;
        }

            /*! <summary>
             * Delete the selected SystemType from the service and ObservableCollection.
             * </summary>
             */
        await systemTypeService.DeleteSystemType(selectedSystem);
        systemTypes.Remove(selectedSystem);


        /*! <summary>
         * Reset the selected item and clear the txe_systemType Text property.
         * </summary>
         */
        ltv_systemType.SelectedItem = null;
        txe_systemType.Text = "";
    }


    /*! <summary>
     * Event handler for the selection of an item in the ListView.
     * </summary>
     */
    private void ltv_systemType_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedSystem = e.SelectedItem as SystemType;
        if (selectedSystem == null) return;        
        txe_systemType.Text = selectedSystem.Name;
    }
}