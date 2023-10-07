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


        private string name;

        /// <summary>
        /// The name of the building type.
        /// </summary>
        public string Name {
            get => name;
            set => SetField(ref name, value);
        }

        /// <summary>
        /// Event for the building type properties changing.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Reflection helper for implementing <see cref="INotifyPropertyChanged"/>.
        /// </summary>
        /// <typeparam name="T">The type of the field.</typeparam>
        /// <param name="field">A reference to the field.</param>
        /// <param name="value">The value of the field.</param>
        /// <param name="propertyName">The property name changing. This is automatically assigned by <see cref="CallerMemberNameAttribute"/>.</param>
        /// <returns></returns>

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "") {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
