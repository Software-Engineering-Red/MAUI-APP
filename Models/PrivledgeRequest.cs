using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MauiApp1.Models
{
    public class PrivledgeRequest : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        private string requestType;
        private int memberID;
        private string privledgeLevel;
        private bool approved;

        public bool Approved
        {
            get => approved;
            set => SetField(ref approved, value);
        }

        public string RequestType
        {
            get => requestType;
            set => SetField(ref requestType, value);
        }

        public string PrivledgeLevel
        {
            get => privledgeLevel;
            set => SetField(ref privledgeLevel, value);
        }

        public int MemberID
        {
            get => memberID;
            set => SetField(ref  memberID, value);
        }

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
