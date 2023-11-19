using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
    public class User : AModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetField(ref _email, value);
        }

        private string _accessLevel;
        public string AccessLevel
        {
            get => _accessLevel;
            set => SetField(ref _accessLevel, value);
        }
    }
}
