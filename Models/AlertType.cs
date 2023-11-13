using SQLite;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Windows.System;

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

        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get => _dateCreated;
            set => Utils.SetProperty(ref _dateCreated, value, this);    
        }

        private string _detail;
        public string Detail
        {
            get => _detail;
            set => Utils.SetProperty(ref _detail, value, this);
        }

        private int _type;
        public int Type
        {
            get => _type;
            set => Utils.SetProperty(ref _type, value, this);
        }

        private string _raisedBy;
        public string RaisedBy
        {
            get => _raisedBy;
            set => Utils.SetProperty(ref _raisedBy, value, this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
