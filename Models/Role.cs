using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

/*! The Role class stores data regarding a single role   
 *  and allows for the basic values to be updated
 */

namespace UndacApp.Models
{
    public class Role : INotifyPropertyChanged
    {
        //! Auto incrementing unique ID for the database values
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

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

