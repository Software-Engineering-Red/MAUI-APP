using SQLite;
using System.ComponentModel;

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
            set => Utils.SetProperty(ref _name, value, this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
