using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.Models
{
    /// <summary>
    /// Represents a Rota object.
    /// </summary>
    public class Rota : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string name;

        /// <summary>
        /// Gets or sets the name of the Rota.
        /// </summary>
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Notifies clients that a property value has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Sets the field to a new value and notifies clients if the value has changed.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="field">A reference to the field to be set.</param>
        /// <param name="value">The new value to assign to the field.</param>
        /// <param name="propertyName">The name of the property (automatically set).</param>
        /// <returns>True if the value has changed; otherwise, false.</returns>
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
