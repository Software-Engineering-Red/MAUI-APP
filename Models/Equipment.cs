namespace UndacApp.Models
{
    /*! <summary>
        A model structure for Equipment data
    </summary> 
    <details>Data is stored in SQLite database.</details> */
    public class Equipment : AModel
    {

        /*! <summary>
       A private variable, storing Equipment name
          </summary> */
        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
    }
}