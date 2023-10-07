using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

/*! The Role class stores data regarding a single role   
 *  and allows for the basic values to be updated
 */

namespace MauiApp1.Models
{
    public class Role : INotifyPropertyChanged
    {
        //! Auto incrementing unique ID for the database values
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string name;

        //! public definition for Name with getters and setters
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        //! public event handler
        public event PropertyChangedEventHandler? PropertyChanged;

        /*!
         * Detect a property update
         * @param propertyName (string) a properties name
         */
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /*!
         * Sets new property value based off field name
         * @param field (String) the field name
         * @param value (Stirng) the value stored in the field
         * @param CallerMemberName (String) the name of the caller to the method
         * @param propertyName (String) name of the property. This is always empty as it is defaulted in call
         */
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
    {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}

