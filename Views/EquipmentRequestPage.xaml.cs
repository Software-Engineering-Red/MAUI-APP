using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiApp1.Views;

public partial class EquipmentRequestPage : ContentPage
{
    private IEquipmentRequestService _requestService;

    public EquipmentRequestPage(IEquipmentRequestService requestService)
    {
        InitializeComponent();
        _requestService = requestService;
        LoadRequests();
    }

    private async void LoadRequests()
    {
        var requests = await _requestService.GetAllRequestsAsync();
        requestsListView.ItemsSource = requests;
    }

    private async void OnRequestEquipmentClicked(object sender, EventArgs e)
    {
        var request = new EquipmentRequest
        {
            EquipmentType = equipmentTypeEntry.Text,
            Quantity = int.Parse(quantityEntry.Text),
            Status = "Requested"
        };

        await _requestService.CreateRequestAsync(request);
        LoadRequests();
    }
}