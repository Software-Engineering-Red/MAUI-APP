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
    /*! <summary>
        A model structure for Organisation data
    </summary> 
    <details>Data is stored in SQLite database.</details> */
    public class Organisation : INotifyPropertyChanged
    {
        /*! <summary>
            An unique primary key used to manage database entries
        </summary> */
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        /*! <summary>
            A private variable, storing organisaton name
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
