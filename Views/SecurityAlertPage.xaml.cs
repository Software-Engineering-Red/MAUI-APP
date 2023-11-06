using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using UndacApp.Models;

namespace UndacApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecurityAlertsPage : ContentPage
    {
        ObservableCollection<SecurityAlert> _currentAlerts = new ObservableCollection<SecurityAlert>();
        ObservableCollection<SecurityAlert> _resolvedAlerts = new ObservableCollection<SecurityAlert>();
        SecurityAlert _selectedAlert;
        public SecurityAlertsPage()
        {
            InitializeComponent();

            _currentAlerts = new ObservableCollection<SecurityAlert>();
            ltv_currentAlerts.ItemsSource = _currentAlerts;

            _resolvedAlerts = new ObservableCollection<SecurityAlert>();
            ltv_resolvedAlerts.ItemsSource = _resolvedAlerts;
            btnSnooze.IsVisible = false;
            btnResolve.IsVisible = false;
        }


        private async void CreateAlertButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txe_securityAlert.Text))
                return;

            var securityAlert = CreateSecurityAlert(txe_securityAlert.Text);

            _currentAlerts.Insert(0, securityAlert);
            txe_securityAlert.Text = "";

            await DisplaySecurityAlerts(securityAlert);
        }

        private SecurityAlert CreateSecurityAlert(string message)
        {
            return new SecurityAlert
            {
                Message = message,
                CreatedTime = DateTime.Now,
                Resolved = false
            };
        }

        private async Task DisplaySecurityAlerts(SecurityAlert securityAlert)
        {
            await DisplayAlert("Alert", securityAlert.Message, "OK");

            PlayAlertSound();
        }

        private void PlayAlertSound()
        {
            using (var soundPlayer = new SoundPlayer("Resources/Sounds/alert.mp3"))
            {
                soundPlayer.Play();
            }
        }

        private async void NotifySeniorManagement(SecurityAlert securityAlert)
        {
            string emailRecipient = "crisismanager@UNSAC.com";
            string emailSubject = "Security Alert";
            string emailBody = securityAlert.Message;

            try
            {
                var message = new EmailMessage
                {
                    Subject = emailSubject,
                    Body = emailBody,
                    To = { emailRecipient }
                };

                await Email.ComposeAsync(message);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Email Error", $"Error sending email: {ex.Message}", "OK");
            }
        }

        private void ltv_currentAlerts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is SecurityAlert selectedAlert)
            {
                _selectedAlert = selectedAlert;
                btnSnooze.IsVisible = true;
                btnResolve.IsVisible = true;
            }
        }

        private CancellationTokenSource _snoozeTokenSource;

        private async void SnoozeButton_Clicked(object sender, EventArgs e)
        {
            if (_selectedAlert != null)
            {
                btnSnooze.IsVisible = false;
                btnResolve.IsVisible = false;
                _snoozeTokenSource = new CancellationTokenSource();
                bool timerElapsed = await StartSnoozeTimer(TimeSpan.FromMinutes(5), _snoozeTokenSource.Token);
                btnSnooze.IsVisible = true;
                btnResolve.IsVisible = true;

                if (timerElapsed)
                {
                    await DisplayAlert("Alert", _selectedAlert.Message, "OK");
                }
            }
        }

        private async Task<bool> StartSnoozeTimer(TimeSpan duration, CancellationToken cancellationToken)
        {
            try
            {
                await Task.Delay(duration, cancellationToken);
                return true;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
        }

        private void CancelSnoozeTimer()
        {
            if (_snoozeTokenSource != null && !_snoozeTokenSource.IsCancellationRequested)
            {
                _snoozeTokenSource.Cancel();
            }
        }

        private void ResolveButton_Clicked(object sender, EventArgs e)
        {
            if (_selectedAlert != null)
            {
                _selectedAlert.Resolved = true;
                _resolvedAlerts.Insert(0, _selectedAlert);
                _currentAlerts.Remove(_selectedAlert);
                btnSnooze.IsVisible = false;
                btnResolve.IsVisible = false;
            }
        }

        private void ClearAlertsButton_Clicked(object sender, EventArgs e)
        {
            _currentAlerts.Clear();
            _resolvedAlerts.Clear();
        }

        private void FilterAlerts_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = e.NewTextValue;
            var filteredAlerts = _currentAlerts.Where(alert => alert.Message.Contains(searchText)).ToList();
            ltv_currentAlerts.ItemsSource = new ObservableCollection<SecurityAlert>(filteredAlerts);
        }

        private void ClearFilterButton_Clicked(object sender, EventArgs e)
        {
            ltv_currentAlerts.ItemsSource = _currentAlerts;
        }

       
       
    }
}
