using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Models
{
    public class Team : AModel
    {
        public Team()
        {
            TeamMembers = new();
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetField(ref _name, value);
        }

        private List<TeamMember> _teamMembers;
        public List<TeamMember> TeamMembers
        {
            get => _teamMembers;
            set => SetField(ref _teamMembers, value);
        }
    }
}
