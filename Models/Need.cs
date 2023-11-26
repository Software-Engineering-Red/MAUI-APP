namespace UndacApp.Models
{
    public class Need : AModel
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        private NeedStatus _status;
        public NeedStatus Status
        {
            get => _status;
            set => SetField(ref _status, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetField(ref _description, value);
        }

        private int _priority;
        public int Priority
        {
            get => _priority;
            set => SetField(ref _priority, value);
        }
    }

    public enum NeedStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Aborted
    }
}
 