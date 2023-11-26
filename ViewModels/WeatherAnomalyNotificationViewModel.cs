using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UndacApp.Commands;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.ViewModels
{
    public class WeatherAnomalyNotificationsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<WeatherAnomalyNotification> _currentNotifications = new ObservableCollection<WeatherAnomalyNotification>();
        private ObservableCollection<WeatherAnomalyNotification> _clearedNotifications = new ObservableCollection<WeatherAnomalyNotification>();
        private WeatherAnomalyNotification _selectedNotification;
        private readonly IWeatherAnomalyNotificationService _service = new WeatherAnomalyNotificationService();
        private string _notificationMessage;
        public ReportNotificationCommand ReportNotificationCommand { get; private set; }
        public ClearNotificationCommand ClearNotificationCommand { get; private set; }

        public WeatherAnomalyNotificationsViewModel()
        {
            LoadNotifications();
            ReportNotificationCommand = new ReportNotificationCommand(ReportNotification);
            ClearNotificationCommand = new ClearNotificationCommand(ClearNotification);
        }

        public ObservableCollection<WeatherAnomalyNotification> CurrentNotifications
        {
            get => _currentNotifications;
            set => SetField(ref _currentNotifications, value);
        }

        public ObservableCollection<WeatherAnomalyNotification> ClearedNotifications
        {
            get => _clearedNotifications;
            set => SetField(ref _clearedNotifications, value);
        }

        public WeatherAnomalyNotification SelectedNotification
        {
            get => _selectedNotification;
            set
            {
                SetField(ref _selectedNotification, value);
                OnPropertyChanged(nameof(IsClearButtonVisible));
            }
        }

        public string NotificationMessage
        {
            get => _notificationMessage;
            set => SetField(ref _notificationMessage, value);
        }

        public bool IsClearButtonVisible => SelectedNotification != null;

        public event PropertyChangedEventHandler PropertyChanged;

        private async void LoadNotifications()
        {
            var notifications = await _service.GetAll();
            foreach (var notification in notifications)
            {
                if (notification.Cleared)
                    _clearedNotifications.Add(notification);
                else
                    _currentNotifications.Add(notification);
            }
        }

        public async void ReportNotification()
        {
            if (string.IsNullOrEmpty(NotificationMessage))
                return;

            var notification = new WeatherAnomalyNotification
            {
                Message = NotificationMessage,
                CreatedAt = DateTime.Now,
                Cleared = false
            };

            await _service.Add(notification);
            CurrentNotifications.Add(notification);
            NotificationMessage = string.Empty;
        }

        public async void ClearNotification()
        {
            if (SelectedNotification == null)
                return;

            SelectedNotification.Cleared = true;
            await _service.Update(SelectedNotification);

            CurrentNotifications.Remove(SelectedNotification);
            ClearedNotifications.Add(SelectedNotification);
            SelectedNotification = null;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
