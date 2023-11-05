using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
            set => SetProperty(ref _name, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

    }
}
