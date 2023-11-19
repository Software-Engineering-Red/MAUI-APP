namespace UndacApp.Models
{
    public class SecurityAlert : AModel
    {

        private string _message;
        public string Message
        {
            get => _message;
            set => SetField(ref _message, value);
        }

        private DateTime _createdTime;
        public DateTime CreatedTime
        {
            get => _createdTime;
            set => SetField(ref _createdTime, value);
        }

        private bool _resolved;
        public bool Resolved
        {
            get => _resolved;
            set => SetField(ref _resolved, value);
        }

    }
}
