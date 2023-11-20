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

        public async void LoadNotifications()
        {
            var notifications = await _weatherAnomalyNotificationService.GetAll();
            CategorizeNotifications(notifications);
            cv_currentNotifications.ItemsSource = _currentNotifications;
            ltv_clearedNotifications.ItemsSource = _clearedNotifications;
        }

        public void CategorizeNotifications(IEnumerable<WeatherAnomalyNotification> notifications)
        {
            foreach (var notification in notifications)
            {
                if (notification.Cleared)
                    _clearedNotifications.Add(notification);
                else
                    _currentNotifications.Add(notification);
            }
        }

        public async void ReportNotificationButton_Clicked(object sender, EventArgs e)
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

        public void cv_currentNotifications_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUIForSelectedNotification(e.CurrentSelection.FirstOrDefault() as WeatherAnomalyNotification);
        }

        public void UpdateUIForSelectedNotification(WeatherAnomalyNotification selectedNotification)
        {
            _selectedNotification = selectedNotification;
            btnClear.IsVisible = _selectedNotification != null;
        }

        public async void ClearButton_Clicked(object sender, EventArgs e)
        {
            if (_selectedNotification == null)
                return;

            await UpdateNotificationStatus(_selectedNotification);

            UpdateUIAfterClearingNotification();
        }

        public async Task UpdateNotificationStatus(WeatherAnomalyNotification notification)
        {
            notification.Cleared = true;
            await _weatherAnomalyNotificationService.Update(notification);
        }

        public void UpdateUIAfterClearingNotification()
        {
            _currentNotifications.Remove(_selectedNotification);
            _clearedNotifications.Add(_selectedNotification);
            cv_currentNotifications.SelectedItem = null;
            btnClear.IsVisible = false;
        }
    }
}