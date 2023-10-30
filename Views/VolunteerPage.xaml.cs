using MauiApp1.Models;
using MauiApp1.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace MauiApp1.Views
{
    public partial class VolunteerPage : ContentPage
    {
        Volunteer selectedVolunteer = null;
        IVolunteerService volunteerService;
        ObservableCollection<Volunteer> volunteers = new ObservableCollection<Volunteer>();

        public VolunteerPage()
        {
            InitializeComponent();
            this.BindingContext = this;
            this.volunteerService = new VolunteerService();

            Task.Run(async () => await LoadVolunteers());
            txe_volunteer.Text = "";
        }

        private async Task LoadVolunteers()
        {
            volunteers = new ObservableCollection<Volunteer>(await volunteerService.GetVolunteerList());
            ltv_volunteers.ItemsSource = volunteers;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txe_volunteer.Text)) return;

            if (selectedVolunteer == null)
            {
                var volunteer = new Volunteer() { Name = txe_volunteer.Text };
                volunteerService.AddVolunteer(volunteer);
                volunteers.Add(volunteer);
            }
            else
            {
                selectedVolunteer.Name = txe_volunteer.Text;
                volunteerService.UpdateVolunteer(selectedVolunteer);
                var volunteer = volunteers.FirstOrDefault(x => x.ID == selectedVolunteer.ID);
                volunteer.Name = txe_volunteer.Text;
            }

            selectedVolunteer = null;
            ltv_volunteers.SelectedItem = null;
            txe_volunteer.Text = "";

        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (ltv_volunteers.SelectedItem == null)
            {
                await DisplayAlert("No Volunteer Selected", "Select the volunteer you want to delete from the list", "OK");
                return;
            }

            await volunteerService.DeleteVolunteer(selectedVolunteer);
            volunteers.Remove(selectedVolunteer);

            ltv_volunteers.SelectedItem = null;
            txe_volunteer.Text = "";
        }

        private void ltv_volunteers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedVolunteer = e.SelectedItem as Volunteer;
            if (selectedVolunteer == null) return;

            txe_volunteer.Text = selectedVolunteer.Name;
        }
    }
}
