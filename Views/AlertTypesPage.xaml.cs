using UndacApp.Models;
using UndacApp.Services;
using System.Collections.ObjectModel;

namespace UndacApp.Views;

public partial class AlertTypesPage : ContentPage
{
    AlertType _selectedAlertType = null;
    IAlertTypeService _alertTypeService;
    ObservableCollection<AlertType> _alertTypes = new ObservableCollection<AlertType>();

    public AlertTypesPage()
    {
        InitializeComponent();
        this.BindingContext = this;
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

        if (_selectedAlertType == null)
        {
            var alert = new AlertType() { Name = txe_alert.Text };
            _alertTypeService.AddAlertType(alert);
            _alertTypes.Add(alert);
        }
        else
        {
            _selectedAlertType.Name = txe_alert.Text;
            _alertTypeService.UpdateAlertType(_selectedAlertType);
            var alert = _alertTypes.FirstOrDefault(x => x.ID == _selectedAlertType.ID);
            alert.Name = txe_alert.Text;
        }


        _selectedAlertType = null;
        ltv_alerttypes.SelectedItem = null;
        txe_alert.Text = "";
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

    private void ltv_alerttypes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _selectedAlertType = e.SelectedItem as AlertType;
        if (_selectedAlertType == null) return;

        txe_alert.Text = _selectedAlertType.Name;
    }
}