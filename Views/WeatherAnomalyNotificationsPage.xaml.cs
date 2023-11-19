using System.Collections.ObjectModel;
using UndacApp.Models;
using UndacApp.Services;


namespace UndacApp.Views
{
    public partial class WeatherAnomalyNotificationsPage : ContentPage
    {
        ObservableCollection<WeatherAnomalyNotification> _currentNotifications = new ObservableCollection<WeatherAnomalyNotification>();
        ObservableCollection<WeatherAnomalyNotification> _clearedNotifications = new ObservableCollection<WeatherAnomalyNotification>();
        WeatherAnomalyNotification _selectedNotification;
        IWeatherAnomalyNotificationService _weatherAnomalyNotificationService = new WeatherAnomalyNotificationService();

        public WeatherAnomalyNotificationsPage()
        {
            InitializeComponent();
            LoadNotifications();
        }

        private async void LoadNotifications()
        {
            var notifications = await _weatherAnomalyNotificationService.GetAll();
            foreach (var notification in notifications)
            {
                if (notification.Cleared)
                    _clearedNotifications.Add(notification);
                else
                    _currentNotifications.Add(notification);
            }

            cv_currentNotifications.ItemsSource = _currentNotifications;
            ltv_clearedNotifications.ItemsSource = _clearedNotifications;
        }

        private async void ReportNotificationButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txe_weatherAnomalyNotification.Text))
                return;

            var notification = new WeatherAnomalyNotification
            {
                Message = txe_weatherAnomalyNotification.Text,
                CreatedAt = DateTime.Now,
                Cleared = false
            };

            await _weatherAnomalyNotificationService.Add(notification);
            _currentNotifications.Add(notification);
            txe_weatherAnomalyNotification.Text = "";
        }

        private void cv_currentNotifications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedNotification = e.CurrentSelection.FirstOrDefault() as WeatherAnomalyNotification;
            btnClear.IsVisible = _selectedNotification != null;
        }

        private async void ClearButton_Clicked(object sender, EventArgs e)
        {
            if (_selectedNotification == null)
                return;

            _selectedNotification.Cleared = true;
            await _weatherAnomalyNotificationService.Update(_selectedNotification);

            _currentNotifications.Remove(_selectedNotification);
            _clearedNotifications.Add(_selectedNotification);
            cv_currentNotifications.SelectedItem = null;
            btnClear.IsVisible = false;
        }
    }
}