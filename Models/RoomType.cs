using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace MauiApp1.Models
{
    /// <summary>
    /// Represents a type of room in the application
    /// </summary>
     
    public class RoomType : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the unique identifier of the room type
        /// </summary>
      
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }


        private string name;
        /// <summary>
        /// Gets or sets the name of the room type
        /// </summary>
        
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        /// <summary>
        /// Event that is raised when a property of the room type is changed
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        
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
