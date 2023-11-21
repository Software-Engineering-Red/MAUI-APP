namespace UndacApp.Models
{
    public class Volunteer : AModel
    {

        private string email;
        private string skill;
        private string geographicalLocation;
        private string status = "Neutral";
        private DateTime? dateOfArrival = null;
        private DateTime? dateOfDeparture = null;

        public string Email
        {
            get => email;
            set => SetField(ref email, value);
        }

        public string Skill
        {
            get => skill;
            set => SetField(ref skill, value);
        }

        public string GeographicalLocation
        {
            get => geographicalLocation;
            set => SetField(ref geographicalLocation, value);
        }

        public string Status
        {
            get => status;
            set => SetField(ref status, value);
        }

        public DateTime? DateOfArrival
        {
            get => dateOfArrival;
            set => SetField(ref dateOfArrival, value);
        }

        public DateTime? DateOfDeparture
        {
            get => dateOfDeparture;
            set => SetField(ref dateOfDeparture, value);
        }
    }
}
