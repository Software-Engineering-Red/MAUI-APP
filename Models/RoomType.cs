using SQLite;
using System.ComponentModel;


namespace UndacApp.Models
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
            set => Utils.SetProperty(ref _name, value, this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

   

}
