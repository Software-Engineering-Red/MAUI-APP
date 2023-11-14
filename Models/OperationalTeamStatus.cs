using SQLite;
using SQLiteNetExtensions.Attributes;
using System.ComponentModel;

namespace UndacApp.Models
{
    /// <summary>
    /// Class representing the Status of OperationalTeam extending INotifyPropertyChanged.
    /// Data will be stored in SQLite Database "operational_team_status".
    /// </summary>
    [Table("operational_team_status")]
    public class OperationalTeamStatus : INotifyPropertyChanged
    {
        /// <summary>
        /// Unique primary key ID to distinguish elements.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Name Stored private for OperationalTeamStatus. 
        /// </summary>
        private string _name;
        public string Name
        {
            get => _name;
            set => Utils.SetProperty(ref _name, value, this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
