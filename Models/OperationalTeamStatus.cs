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
        private string name;

        /// <summary>
        /// The Public getter and setter for the variable name.
        /// </summary>
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        /// <summary>
        /// Event handling PropertyChange.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Handles Property Changes if they occur.
        /// </summary>
        /// <param name="propertyName">String value of propertyName</param>
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Set field value based on name.
        /// </summary>
        /// <typeparam name="T">string value name of the field</typeparam>
        /// <param name="field">Reference to the field</param>
        /// <param name="value">Value of the field</param>
        /// <param name="propertyName">String value of the property name</param>
        /// <returns>Boolean whether property has been successfully changed</returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
