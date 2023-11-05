using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public bool Approved
        {
            get => approved;
            set => SetProperty(ref approved, value);
        }

        public string RequestType
        {
            get => requestType;
            set => SetProperty(ref requestType, value);
        }

        public string PrivilegeLevel
        {
            get => privilegeLevel;
            set => SetProperty(ref privilegeLevel, value);
        }

        public int MemberID
        {
            get => memberID;
            set => SetProperty(ref  memberID, value);
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
