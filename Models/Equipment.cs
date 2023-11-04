using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UndacApp.Models
{
    /*! <summary>
        A model structure for Equipment data
    </summary> 
    <details>Data is stored in SQLite database.</details> */
    public class Equipment : INotifyPropertyChanged
    {
        /*! <summary>
          An unique primary key used to manage database entries
       </summary> */
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /*! <summary>
       A private variable, storing Equipment name
          </summary> */
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