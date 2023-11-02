using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.Models
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
        private string name;
        /*! <summary>
         * Determine a public property 'Name' that gets and sets the 'name' field.
         * SetField method is a helper method to set the field and invoke PropertyChanged event.                                              
         * </summary> 
         */
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }
        /*! <summary>
         * Determine an event that notifies when a property has changed.
         * </summary>
         */
        public event PropertyChangedEventHandler? PropertyChanged;
        /*! <summary>
         * Protected method to raise the PropertyChanged event when a property changes.
         * </summary>
         * <param name="propertyName">String value of the property Name</param> 
         */

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        /*! <summary>
         * Helper method to set a field and raise PropertyChanged event if the value changes.
         * </summary>
         * <param name="field">(string) the name of field</param>
         * <param name="value">(string) the value stored in the field </param>
         * <param name="CallerMemberName"> name of the caller method
         * <param name="propertyName">(string) value of the property changed</param>
         * <returns>Boolean if property was successfuly changed.</returns> 
         */
        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
