using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.Models
{
    /*! <summary>
        A model structure for Equipment data
    </summary> 
    <details>Data is stored in SQLite database.</details> */
    public class Equipment : INotifyPropertyChanged
    {
        /*! <summary>
          An unique primary key used to manage database entries
       </summary> */
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        /*! <summary>
       A private variable, storing Equipment name
          </summary> */
        private string name;
        /*! <summary>
          A public variable, responsible for getting and setting equipment name.
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
        /*! <summary>
           Method determining if property was changed, and if so triggers PropertyChangeEvent
       </summary>
       <param name="propertyName">String value of the property Name</param> */
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /*! <summary>
            Field method setter, triggers PropertyChangeEventHandler, when OnPropertyChanged is true
        </summary>
        <param name="field">String value of the field</param>
        <param name="value">String value of the coparator</param>
        <param name="propertyName">String value of the property changed</param>
        <returns>Boolean if property was successfuly changed.</returns> */
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}