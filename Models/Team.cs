namespace UndacApp.Models
{
    public class Team : AModel
    {
        public Team()
        {
            TeamMembers = new();
        }

        private List<TeamMember> _teamMembers;
        public List<TeamMember> TeamMembers
        {
            get => _teamMembers;
            set => SetField(ref _teamMembers, value);
        }
    }
}
