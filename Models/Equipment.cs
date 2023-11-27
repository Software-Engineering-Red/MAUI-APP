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
        private string equip;
        public string Equip
        {
            get => equip;
            set => SetField(ref equip, value);
        }
    }
}