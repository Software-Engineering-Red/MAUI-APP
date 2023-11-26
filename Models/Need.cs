namespace UndacApp.Models
{
    public class Need : AModel
    {
        private string _priority;
        public string Priority
        {
            get => _priority;
            set => SetField(ref _priority, value);
        }
        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }
    }
}
 