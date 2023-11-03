using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UndacApp.Models
{
    [Table("sub_team_status")]
    public class SubTeamStatus : INotifyPropertyChanged
    {
        /// <summary>
        /// Unique primary key ID for the sub-team.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Name of the operational sub-team.
        /// </summary>
        private string name;

        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        /// <summary>
        /// Location of the operational sub-team on a map.
        /// </summary>
        private string location;

        public string Location
        {
            get => location;
            set => SetField(ref location, value);
        }

        /// <summary>
        /// Personnel assigned to the operational sub-team.
        /// </summary>
        private string personnel;

   
        public string Personnel
        {
            get => personnel;
            set => SetField(ref personnel, value);
        }

        /// <summary>
        /// Resources assigned to the operational sub-team.
        /// </summary>
        private string resources;

     
        public string Resources
        {
            get => resources;
            set => SetField(ref resources, value);
        }

        /// <summary>
        /// Communications from the operational sub-team leader.
        /// </summary>
        private string leaderCommunication;

        public string LeaderCommunication
        {
            get => leaderCommunication;
            set => SetField(ref leaderCommunication, value);
        }

        /// <summary>
        /// Event handling PropertyChange.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
