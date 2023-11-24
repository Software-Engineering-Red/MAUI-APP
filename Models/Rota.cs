using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
    /// <summary>
    /// Represents a Rota object.
    /// </summary>
    public class Rota : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => Utils.SetProperty(ref _name, value, this);
        }

        private string _location;
        public string Location {
            get => _location;
            set => SetProperty(ref _location, value);
        }

        private bool _valid;
        public bool Valid {
            get => _valid;
            set => SetProperty(ref _valid, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
