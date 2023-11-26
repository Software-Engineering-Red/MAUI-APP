namespace UndacApp.Models
{
    public class Operation : AModel
    {
        private OperationStatus _status;
        public OperationStatus Status
        {
            get => _status;
            set => SetField(ref _status, value);
        }

        private DateTime _dateStarted;
        public DateTime DateStarted
        {
            get => _dateStarted;
            set => SetField(ref _dateStarted, value);
        }

        private string _location;
        public string Location
        {
            get => _location;
            set => SetField(ref _location, value);
        }

        private int _numberOfPersonnel;
        public int NumberOfPersonnel
        {
            get => _numberOfPersonnel;
            set => SetField(ref _numberOfPersonnel, value);
        }

        private string _finalReport;
        public string FinalReport
        {
            get => _finalReport;
            set => SetField(ref _finalReport, value);
        }

    }

    public enum OperationStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Aborted
    }
}
