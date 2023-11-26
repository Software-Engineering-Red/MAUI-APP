
using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views;

public partial class OperationPage : ContentPage
{

    private Operation? selectedOperation = null;

    OperationService operationService;

    ObservableCollection<Operation> operations = new ObservableCollection<Operation>();

    public OperationPage()
    {
        InitializeComponent();
        this.BindingContext = new Operation();
        this.operationService = new OperationService();

        Task.Run(async () => await LoadOperations());
        NameEntry.Text = "";
    }

    private async Task LoadOperations()
    {
        operations = new ObservableCollection<Operation>(await operationService.GetAll());
        ltv_operations.ItemsSource = operations;
    }

    /*! <summary>
            Method responsible for saving Continent into SQLite database, triggered by selection of save button.
        </summary> 
        <param name="sender">Details about the element that triggered the event.</param>
        <param name="e">Event details, passed by eventHandler due to clicking event button.</param> */
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(NameEntry.Text)) return;
        if (String.IsNullOrEmpty(StatusEntry.SelectedItem as string)) return;
        if (DateStartedEntry.Date == default) return;
        if (String.IsNullOrEmpty(LocationEntry.Text)) return;
        if (String.IsNullOrEmpty(NumberOfPersonnelEntry.Text)) return;
        if (String.IsNullOrEmpty(FinalReportEntry.Text)) return;

        if (selectedOperation == null)
        {
            var operation = new Operation()
            {
                Name = NameEntry.Text,
                Status = Enum.Parse<OperationStatus>(StatusEntry.SelectedItem as string),
                DateStarted = DateStartedEntry.Date,
                Location = LocationEntry.Text,
                NumberOfPersonnel = int.Parse(NumberOfPersonnelEntry.Text),
                FinalReport = FinalReportEntry.Text,
                
            };
            await operationService.Add(operation);
            operations.Add(operation);
        }
        else
        {
            selectedOperation.Name = NameEntry.Text;
            selectedOperation.Status = Enum.Parse<OperationStatus>(StatusEntry.SelectedItem as string);
            selectedOperation.DateStarted = DateStartedEntry.Date;
            selectedOperation.Location = LocationEntry.Text;
            selectedOperation.NumberOfPersonnel = int.Parse(NumberOfPersonnelEntry.Text);
            selectedOperation.FinalReport = FinalReportEntry.Text;
            await operationService.Update(selectedOperation);
            var operation = this.operations.FirstOrDefault(x => x.ID == selectedOperation.ID);
            operation.Name = NameEntry.Text;
            operation.Status = Enum.Parse<OperationStatus>(StatusEntry.SelectedItem as string);
            operation.DateStarted = DateStartedEntry.Date;
            operation.Location = LocationEntry.Text;
            operation.NumberOfPersonnel = int.Parse(NumberOfPersonnelEntry.Text);
            operation.FinalReport = FinalReportEntry.Text;
        }


        selectedOperation = null;
        ltv_operations.SelectedItem = null;
        NameEntry.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_operations.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No Operation Selected", "Select the operation you want to delete from the list", "OK");
            return;
        }

        await operationService.Remove(selectedOperation);
        operations.Remove(selectedOperation);

        ltv_operations.SelectedItem = null;
        NameEntry.Text = "";
    }

    private void ltv_operations_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedOperation = e.SelectedItem as Operation;
        if (selectedOperation == null) return;

        NameEntry.Text = selectedOperation.Name;
        StatusEntry.SelectedItem = selectedOperation.Status.ToString();
        DateStartedEntry.Date = selectedOperation.DateStarted;
        LocationEntry.Text = selectedOperation.Location;
        NumberOfPersonnelEntry.Text = selectedOperation.NumberOfPersonnel.ToString();
        FinalReportEntry.Text = selectedOperation.FinalReport;

    }
}