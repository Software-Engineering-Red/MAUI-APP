using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UndacApp.Models
/*! <summary>
A model structure for Order Status data
</summary> 
<details>Data is stored in database.</details> */
{
    [Table("order_status")]
    public class OrderStatus : INotifyPropertyChanged
    {
        /*! <summary>
         Unique primary key. Manages relationships in DB
        </summary> */
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /*! <summary>
            A private variable, storing order status name
        </summary> */
        private string name;

        /*! <summary>
        A public variable, responsible for getting and setting order status.
        </summary> */
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        /*! <summary>
            Event responsible for handling propertyChange.
        </summary> */
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

    }
}
