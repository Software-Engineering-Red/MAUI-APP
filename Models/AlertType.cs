namespace UndacApp.Models
{

    public class AlertType : AModel
    {
        private string _status;
        public string Status
        {
            get => _status;
            set => SetField(ref _status, value);
        }

        private DateTime _dateCreated;
        public DateTime DateCreated
        {
            get => _dateCreated;
            set => SetField(ref _dateCreated, value);
        }

        private string _detail;
        public string Detail
        {
            get => _detail;
            set => SetField(ref _detail, value);
        }

        private int _type;
        public int Type
        {
            get => _type;
            set => SetField(ref _type, value);
        }

        private string _raisedBy;
        public string RaisedBy
        {
            get => _raisedBy;
            set => SetField(ref _raisedBy, value);
        }

    }
}
