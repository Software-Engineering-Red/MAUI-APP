using System.Collections.ObjectModel;
using System.Windows.Input;
using UndacApp.Models;
using Microsoft.Maui.Controls;

namespace UndacApp.Views
{
    public partial class TeamRequestPage : ContentPage
    {
        private BasicExpertService _expertService;
        public ObservableCollection<BasicExpert> AvailableExperts { get; set; }
        public ICommand ReserveCommand { get; private set; }

        public TeamRequestPage()
        {
            InitializeComponent();
            _expertService = new BasicExpertService();
            AvailableExperts = new ObservableCollection<BasicExpert>();
            ReserveCommand = new Command<BasicExpert>(ReserveExpert);
            BindingContext = this;
        }

        private object expertService()
        {
            throw new NotImplementedException();
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

        private void ReserveExpert(BasicExpert expert)
        {
            if (expert.IsAvailable)
            {
                expert.IsAvailable = false; // Mark the expert as reserved
                ReservationStatus.Text = $"Expert {expert.Name} reserved!";
                RefreshExpertsList(); // Refresh the list to reflect the changes
            }
            else
            {
                ReservationStatus.Text = $"Expert {expert.Name} is already reserved.";
            }
        }

        private void RefreshExpertsList()
        {
            AvailableExperts.Clear();
            foreach (var expert in _expertService.GetExperts()) // GetExperts() returns all experts
            {
                if (expert.IsAvailable)
                {
                    AvailableExperts.Add(expert);
                }
            }
        }
    }
}
