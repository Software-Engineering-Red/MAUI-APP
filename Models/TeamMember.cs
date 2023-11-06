using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
    public class TeamMember : INotifyPropertyChanged {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        private string name;
        private string accessPrivilegeLevel;
        private bool available = true;

        public string Name
    {
            get => name;
            set => Utils.SetProperty(ref name, value, this);
        }

        public string AccessPrivilegeLevel
        {
            get => accessPrivilegeLevel;
            set => Utils.SetProperty(ref accessPrivilegeLevel, value, this);
        }

        public bool Available
        {
            get => available;
            set => Utils.SetProperty(ref available, value, this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
