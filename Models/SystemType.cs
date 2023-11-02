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
    /*! <summary>
     * Determine a class named SystemType which implements the INotifyPropertyChanged interface.
     * </summary>
     * <details>Data is stored in SQLite database.</details> 
     */
    public class SystemType : INotifyPropertyChanged
    {
        /*! <summary>
         * Determine a primary key and auto-increment property named 'type'.
         * </summary> 
         */
        [PrimaryKey, AutoIncrement]
        public int type { get; set; }
        /*! <summary>
         * Determine a private field 'name' to store the value of the 'Name' property.
         * </summary> 
         */
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
