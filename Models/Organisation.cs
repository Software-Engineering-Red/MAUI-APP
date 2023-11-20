using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
    /*! <summary>
        A model structure for Organisation data
    </summary> 
    <details>Data is stored in SQLite database.</details> */
    public class Organisation : AModel
    {

        /*! <summary>
            A private variable, storing organisaton name
        </summary> */
        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
    }
}
