using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views;

public partial class AlertTypesPage : ContentPage
{
    AlertType _selectedAlertType = null; 
    IAlertTypeService _alertTypeService;
    ObservableCollection<AlertType> _alertTypes = new ObservableCollection<AlertType>();
    private string selectedStatus = "";



    public AlertTypesPage()
    {
        InitializeComponent();
        BindingContext = new AlertType();
        _alertTypeService = new AlertTypeService();

        Task.Run(async () => await LoadAlertTypes());
        txe_alert.Text = "";
        
       

    }

  

    private async Task LoadAlertTypes()
    {
        _alertTypes = new ObservableCollection<AlertType>(await _alertTypeService.GetAlertTypes());
        ltv_alerttypes.ItemsSource = _alertTypes;



    }

    private void SaveButton_Clicked(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(txe_alert.Text)) return;

        string selectedStatus = alertStatusPicker.SelectedItem.ToString();

        if (_selectedAlertType == null)
        {

            var alert = new AlertType() { Name = txe_alert.Text, Status = selectedStatus, DateCreated = DateTime.Now, RaisedBy = txe_RequestedBy.Text };
            _alertTypeService.AddAlertType(alert);
            _alertTypes.Add(alert);
        }
        else
        {
            _selectedAlertType.Name = txe_alert.Text;
            _selectedAlertType.Status = selectedStatus;
            _alertTypeService.UpdateAlertType(_selectedAlertType);
            
            var alert = _alertTypes.FirstOrDefault(x => x.ID == _selectedAlertType.ID);
            alert.Name = txe_alert.Text;
            alert.RaisedBy = txe_RequestedBy.Text;
            alert.Status = selectedStatus; 
        }


        _selectedAlertType = null;
        ltv_alerttypes.SelectedItem = null;
        txe_alert.Text = "";
        txe_RequestedBy.Text = "";
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_alerttypes.SelectedItem == null)
        {
            await Shell.Current.DisplayAlert("No alert types Selected", "Select the alert type you want to delete from the list", "OK");
            return;
        }

        await _alertTypeService.DeleteAlertType(_selectedAlertType);
        _alertTypes.Remove(_selectedAlertType);

        ltv_alerttypes.SelectedItem = null;
        txe_alert.Text = "";
    }


    private async void OnClearAllClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Confirmation", "Are you sure you want to delete all items?", "Yes", "No");

        if (answer)
        {
            await _alertTypeService.DeleteAllAlertType();
            _alertTypes.Clear();
        }
    }

    private void ltv_alerttypes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _selectedAlertType = e.SelectedItem as AlertType;
        if (_selectedAlertType == null) return;
        _selectedAlertType.Status = alertStatusPicker.SelectedItem.ToString();
        txe_alert.Text = _selectedAlertType.Name;


    }

    private void UpdateAlertTypes()
    {
        string selectedFilter = filterPicker.SelectedItem.ToString();
        string searchText = searchEntry.Text;
        if (_alertTypes == null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(searchText))
        {
            if (selectedFilter == "All")
            {
                ltv_alerttypes.ItemsSource = _alertTypes;
            }
            else
            {
                ltv_alerttypes.ItemsSource = _alertTypes.Where(item => item.Status == selectedFilter);
            }
        }
        else
        {
            ltv_alerttypes.ItemsSource = _alertTypes
                .Where(item => (selectedFilter == "All" || item.Status == selectedFilter) && item.Name.ToLower().Contains(searchText));
        }
    }


    private void FilterPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateAlertTypes();
    }
}