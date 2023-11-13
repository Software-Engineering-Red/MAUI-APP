using SQLite;
using System;
using System.ComponentModel;

namespace UndacApp.Models
{
    public class Volunteer : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string name;
        private string email;
        private string skill;
        private string geographicalLocation;
        private string status = "Neutral";
        private DateTime? dateOfArrival = null;
        private DateTime? dateOfDeparture = null;

        public string Name
        {
            get => name;
            set => Utils.SetProperty(ref name, value, this);
        }
        public string Email
        {
            get => email;
            set => Utils.SetProperty(ref email, value, this);
        }

        public string Skill
        {
            get => skill;
            set => Utils.SetProperty(ref skill, value, this);
        }

        public string GeographicalLocation
        {
            get => geographicalLocation;
            set => Utils.SetProperty(ref geographicalLocation, value, this);
        }

        public string Status
        {
            get => status;
            set => Utils.SetProperty(ref status, value, this);
        }

        public DateTime? DateOfArrival
        {
            get => dateOfArrival;
            set => Utils.SetProperty(ref dateOfArrival, value, this);
        }

        public DateTime? DateOfDeparture
        {
            get => dateOfDeparture;
            set => Utils.SetProperty(ref dateOfDeparture, value, this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
