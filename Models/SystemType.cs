using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
    /*! <summary>
     * Determine a class named SystemType which implements the INotifyPropertyChanged interface.
     * </summary>
     * <details>Data is stored in SQLite database.</details> 
     */
    public class SystemType : INotifyPropertyChanged
    {
        /*! <summary>
         * Determine a primary key and auto-increment property named 'type'.
         * </summary> 
         */
        [PrimaryKey, AutoIncrement]
        public int type { get; set; }
        /*! <summary>
         * Determine a private field 'name' to store the value of the 'Name' property.
         * </summary> 
         */
        private string _name;
        public string Name
        {
            get => _name;
            set => Utils.SetProperty(ref _name, value, this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
