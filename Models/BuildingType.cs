using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MauiApp1.Models
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
            set => SetProperty(ref _name, value);
        }
        // Event to notify subscribers when a property changes.
        public event PropertyChangedEventHandler? PropertyChanged;
        // Method to raise the PropertyChanged event with the property name.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            // If the new value is the same as the current value, no change is made, and false is returned.
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value; // Update the field with the new value.
            OnPropertyChanged(propertyName);// Notify subscribers that the property has changed.
            return true; // Return true to indicate that the property was changed.
        }
    }
}
