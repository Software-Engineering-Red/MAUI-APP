using SQLite;
using System.ComponentModel;

namespace UndacApp.Models
{
    public class TeamMember : AModel {

        public TeamMember()
        {
            Available = true;
        }

        private string _name;
        public string Name
    {
            get => _name;
            set => SetField(ref _name, value);
        }
        private string _accessPrivilegeLevel;
        public string AccessPrivilegeLevel
        {
            get => _accessPrivilegeLevel;
            set => SetField(ref _accessPrivilegeLevel, value);
        }

        private bool _available;
        public bool Available
        {
            get => _available;
            set => SetField(ref _available, value);
        }
    }
}
