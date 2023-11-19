using System.Collections.ObjectModel;
using System.Linq;
using UndacApp.Models;
using UndacApp.Services;

namespace UndacApp.Views;

public partial class VolunteerRequestPage : ContentPage
{
    Volunteer selectedVolunteer = null;
    IVolunteerService volunteerService;
    ObservableCollection<Volunteer> volunteers = new ObservableCollection<Volunteer>();
    private ObservableCollection<Volunteer> _volunteers = new ObservableCollection<Volunteer>();
    private Volunteer _selectedVolunteer = null;

    //! public initialisation of components
    public VolunteerRequestPage()
    {
        InitializeComponent();
        BindingContext = new Volunteer();
        this.volunteerService = new VolunteerService();
        FlagButton.IsVisible = false;
        Task.Run(async () => await LoadVolunteers());
       
    }

    private void DeleteButton_ClickedWrapper(object sender, EventArgs e)
    {
        _ = DeleteButton_Clicked();
    }

    private async Task DeleteButton_Clicked()
    {
        try
        {
            if (ltv_volunteers.SelectedItem == null)
            {
                await DisplayAlert("No Volunteer Selected", "Select the volunteer you want to delete from the list", "OK");
                return;
            }

            await volunteerService.DeleteVolunteer(selectedVolunteer);
            volunteers.Remove(selectedVolunteer);

            ltv_volunteers.SelectedItem = null;
            NameEntry.Text = null;
            EmailEntry.Text = null;

            SkillEntry.Text = null;
            LocationEntry.Text = null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error deleting volunteer: {ex.Message}");
        }
    }

    private async Task LoadVolunteers()
    {
        volunteers = new ObservableCollection<Volunteer>(await volunteerService.GetVolunteerList());
        ltv_volunteers.ItemsSource = volunteers;
    }
    private void ltv_volunteers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        selectedVolunteer = e.SelectedItem as Volunteer;
        if (selectedVolunteer == null) return;

        NameEntry.Text = selectedVolunteer.Name;
        EmailEntry.Text = selectedVolunteer.Email;
        SkillEntry.Text = selectedVolunteer.Skill;
        LocationEntry.Text = selectedVolunteer.GeographicalLocation;
        FlagButton.IsVisible = true;
    }

    /*!
    * Detect save button being clicked
    * @param sender (Object) the sender object created by the event
    * @param e (EventArgs) the arguments passed into the event
    */
    private void SaveButton_Clicked(object sender, EventArgs e)

    {
        if (selectedVolunteer == null)
        {
            var newVolunteer = new Volunteer
            {
                Name = NameEntry.Text,
                Email = EmailEntry.Text,
                Skill = SkillEntry.Text,
                GeographicalLocation = LocationEntry.Text,
                Status = "Neutral"
            };
            volunteerService.AddVolunteer(newVolunteer);
            volunteers.Add(newVolunteer);
        }
        

        selectedVolunteer = null;
        ltv_volunteers.SelectedItem = null;
        NameEntry.Text = null;
        EmailEntry.Text = null; 
        
        SkillEntry.Text = null;
        LocationEntry.Text = null;

    }

    private void FilterVolunteers_TextChanged(object sender, TextChangedEventArgs e)
    {
        string nameFilter = NameFilter.Text;
        string emailFilter = EmailFilter.Text;
        string skillFilter = SkillFilter.Text;
        string locationFilter = LocationFilter.Text;

        var filteredVolunteers = volunteers
            .Where(volunteer => PassesNameFilter(volunteer, nameFilter))
            .Where(volunteer => PassesEmailFilter(volunteer, emailFilter))
            .Where(volunteer => PassesSkillFilter(volunteer, skillFilter))
            .Where(volunteer => PassesLocationFilter(volunteer, locationFilter))
            .ToList();

        ltv_volunteers.ItemsSource = new ObservableCollection<Volunteer>(filteredVolunteers);
    }


    private bool PassesNameFilter(Volunteer volunteer, string nameFilter)
    {
        return string.IsNullOrEmpty(nameFilter) || volunteer.Name.Contains(nameFilter);
    }

    private bool PassesEmailFilter(Volunteer volunteer, string emailFilter)
    {
        return string.IsNullOrEmpty(emailFilter) || volunteer.Email.Contains(emailFilter);
    }

    private bool PassesSkillFilter(Volunteer volunteer, string skillFilter)
    {
        return string.IsNullOrEmpty(skillFilter) || volunteer.Skill.Contains(skillFilter);
    }

    private bool PassesLocationFilter(Volunteer volunteer, string locationFilter)
    {
        return string.IsNullOrEmpty(locationFilter) || volunteer.GeographicalLocation.Contains(locationFilter);
    }



    private void ClearFilterButton_Clicked(object sender, EventArgs e)
    {
        // Clear the text in all filter input fields
        NameFilter.Text = string.Empty;
        EmailFilter.Text = string.Empty;
        SkillFilter.Text = string.Empty;
        LocationFilter.Text = string.Empty;

        // Reset the list to show all volunteers
        ltv_volunteers.ItemsSource = _volunteers;
    }

    private const string NeutralStatus = "Neutral";
    private const string ConfirmedStatus = "Confirmed";

   

    private void FlagButton_Clicked(object sender, EventArgs e)
    {
        if (ltv_volunteers.SelectedItem is Volunteer selectedVolunteer)
        {
            _selectedVolunteer = selectedVolunteer;
            ArrivalDatePicker.IsVisible = true;
            DepartureDatePicker.IsVisible = true;
            RequestDatesButton.IsVisible = true;
            FlagButton.IsVisible = false;
        }
    }

        

    private void RequestDatesButton_Clicked(object sender, EventArgs e)
        {
            if (_selectedVolunteer != null)
            {
                // Set requested arrival and departure dates for the selected volunteer
                _selectedVolunteer.DateOfArrival = ArrivalDatePicker.Date;
                _selectedVolunteer.DateOfDeparture = DepartureDatePicker.Date;
                _selectedVolunteer.Status = "Invited";

            RequestDatesButton.IsVisible = false;
            SendConfirmationEmail(_selectedVolunteer);
            
        }
        }



    private void SetNeutralButton_Clicked(object sender, EventArgs e)
    {
        if (_selectedVolunteer != null)
        {
            // Reset the status and clear the dates in the database
            _selectedVolunteer.Status = "Neutral";
            _selectedVolunteer.DateOfArrival = null;
            _selectedVolunteer.DateOfDeparture = null;
            // Clear the dates in the database

            SetNeutralButton.IsVisible = false;
            RequestDatesButton.IsVisible = false;
            SetConfirmedButton.IsVisible = false;

        }
    }
    private void SetConfirmButton_Clicked(object sender, EventArgs e)
    {
        if (_selectedVolunteer != null)
        {
            // Reset the status and clear the dates in the database
            _selectedVolunteer.Status = "Confirmed";

                // Clear the dates in the database
                SetNeutralButton.IsVisible = false;
                RequestDatesButton.IsVisible = false;
                SetConfirmedButton.IsVisible = false;
            }
    }
    private void SendConfirmationEmail(Volunteer volunteer)
        {
            if (volunteer != null)
            {
                string emailRecipient = volunteer.Email; // Get the volunteer's email
                string emailSubject = "Volunteer Confirmation";
                string emailBody = $"Hi there {volunteer.Name}, we request your assistance from {volunteer.DateOfArrival} until {volunteer.DateOfDeparture}.";

                try
                {
                    var message = new EmailMessage
                    {
                        Subject = emailSubject,
                        Body = emailBody,
                        To = { emailRecipient }
                    };

                    // Send the email
                   // Email.ComposeAsync(message);
                }
                catch (Exception ex)
                {
                    // Handle email sending error
                    DisplayAlert("Email Error", $"Error sending email: {ex.Message}", "OK");
                }
            SetNeutralButton.IsVisible = true;
            SetConfirmedButton.IsVisible = true;

        }
        }

     
        
    }

