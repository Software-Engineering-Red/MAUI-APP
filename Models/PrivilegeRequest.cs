using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
    public class PrivilegeRequest : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        private string requestType;
        private int memberID;
        private string privilegeLevel;
        private bool approved;
        private string systemType;

        public bool Approved
        {
            get => approved;
            set => Utils.SetProperty(ref approved, value, this);
        }

        public string RequestType
        {
            get => requestType;
            set => Utils.SetProperty(ref requestType, value, this);
        }

        public string PrivilegeLevel
        {
            get => privilegeLevel;
            set => Utils.SetProperty(ref privilegeLevel, value, this);
        }

        public int MemberID
        {
            get => memberID;
            set => Utils.SetProperty(ref  memberID, value, this);
        }
        public string SystemType
        {        get => systemType;
                 set => Utils.SetProperty(ref systemType, value, this);
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
