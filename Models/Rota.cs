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

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
