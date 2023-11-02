using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp1.Models
{
    /// <summary>
    /// this class is for setting up the table for the database and implemetns INotifyPropertyChanged functions
    /// by retreiving the name of the table and the index for the database
    /// </summary>
    internal class position_statuses : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get => name; 
            
            set => SetField(ref name,value);
        }
        
        
        public event PropertyChangedEventHandler? PropertyChanged;

        /*this protected function searches for the correct parent class for use in this case an event handler for proerty changed*/
        /*and creates a new instance of the class to handle the parameter named propertyname*/
        protected void OnPropertyChanged(string proertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proertyName));

        /*this protected function sets a letter as a reference for both value and field and does a check for change in the feild name*/
        /*if true then it returns a false otherwise it set the field to the value */
        /*then call OnPropertyChanged(prpertyName) with the parameters proertyName*/
        protected bool SetField<T>(ref T feild, T value, [CallerMemberName] string propetyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(feild, value)) return false;
            feild = value;
            OnPropertyChanged(propetyName);
            return true;
        }
    }
}
