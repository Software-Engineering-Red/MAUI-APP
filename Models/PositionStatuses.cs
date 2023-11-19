using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
    /// <summary>
    /// this class is for setting up the table for the database and implemetns INotifyPropertyChanged functions
    /// by retrieving the name of the table and the index for the database
    /// </summary>
    [Table("position_statuses")]
    internal class PositionStatuses : AModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
    }
}
