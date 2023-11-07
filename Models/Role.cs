using SQLite;
using System.ComponentModel;

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
            set => Utils.SetProperty(ref _name, value, this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}

