using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
    /// <summary>
    /// this class is for setting up the table for the database and implemetns INotifyPropertyChanged functions
    /// by retrieving the name of the table and the index for the database
    /// </summary>
    internal class position_statuses : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => Utils.SetProperty(ref _name, value, this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
