using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
    /// <summary>
    /// A class representing a building type.
    /// </summary>
    public class BuildingType : INotifyPropertyChanged
    {
        /// <summary>
        /// The database ID of the building type.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // A public property for an integer ID.
        private string _name;
        // A private field to store the value of the Name property.

        public string Name
        {
            // Getter returns the value of the private _name field.
            get => _name;
            // Setter uses SetProperty to update the value and raise PropertyChanged event.
            set => Utils.SetProperty(ref _name, value, this);
        }
        // Event to notify subscribers when a property changes.
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
