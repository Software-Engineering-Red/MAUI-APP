using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
    /*! <summary>
        A model structure for Equipment data.
    </summary> 
    <details>Data is stored in SQLite database.</details> */
    public class Equipment : INotifyPropertyChanged
    {
        /*! <summary>
          An unique primary key used to manage database entries.
       </summary> */
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /*! <summary>
       A private variable, storing Equipment name.
          </summary> */
        private string _name;
        public string Name
        {
            get => _name;
            set => Utils.SetProperty(ref _name, value, this);
        }

        /*! <summary>
       A private variable, indicating whether the equipment is reserved.
          </summary> */
        private bool _reserved;
        public bool Reserved
        {
            get => _reserved;
            set => SetProperty(ref _reserved, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}