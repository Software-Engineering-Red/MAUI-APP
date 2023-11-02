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


        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }

   

}
