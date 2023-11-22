using System.Collections.ObjectModel;
using UndacApp.Models;
using UndacApp.Services;
using UndacApp.ViewModels;


namespace UndacApp.Views
{
    public partial class WeatherAnomalyNotificationsPage : ContentPage
    {
        public WeatherAnomalyNotificationsPage()
        {
            InitializeComponent();
            BindingContext = new WeatherAnomalyNotificationsViewModel();
        }
    }
}