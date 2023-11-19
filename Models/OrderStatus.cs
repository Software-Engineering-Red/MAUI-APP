using SQLite;

namespace UndacApp.Models
/*! <summary>
A model structure for Order Status data
</summary> 
<details>Data is stored in database.</details> */
{
    [Table("order_status")]
    public class OrderStatus : AModel
    {

        /*! <summary>
            A private variable, storing order status name
        </summary> */
        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
    }
}
