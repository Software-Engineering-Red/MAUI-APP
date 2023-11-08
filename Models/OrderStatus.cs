using SQLite;
using System.ComponentModel;

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
        private string _name;
        public string Name
        {
            get => _name;
            set => Utils.SetProperty(ref _name, value, this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
