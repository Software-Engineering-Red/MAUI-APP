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

        private bool _employed = true;
        public bool Employed
        {
            get => _employed;
            set => SetField(ref _employed, value);
        }

        private string _role;
        public string Role
        {
            get => _role;
            set => SetField(ref _role, value);
        }

        private string _team;
        public string Team
        {
            get => _team;
            set => SetField(ref _team, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetField(ref _password, value);
        }
    }
}
 
