using SQLite;
using System.ComponentModel;
namespace UndacApp.Models
{

    public class AlertType : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        private string _name;
        public string Name
        {
            get => _name;
            set => Utils.SetProperty(ref _name, value, this);
        }
        private string _status;
        public string Status
        {
            get => _status;
            set => Utils.SetProperty(ref _status, value, this);
        }

   


        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
