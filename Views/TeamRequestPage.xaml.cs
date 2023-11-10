using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.Views{ 
public partial class TeamRequestPage : ContentPage
{
    private BasicExpertService _expertService;
    public ObservableCollection<Expert> AvailableExperts { get; set; }

    public TeamRequestPage()
    {
        InitializeComponent();
        _expertService = new BasicExpertService();
        AvailableExperts = new ObservableCollection<Expert>();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var experts = await _expertService.GetAvailableExpertsAsync();
        AvailableExperts.Clear();
        foreach (var expert in experts)
        {
            AvailableExperts.Add(expert);
        }
    }
}
}