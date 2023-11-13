using UndacApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public interface ITeamMemberService : IService<TeamMember> {
        public Task<List<TeamMember>> GetAvailable();
    }
}
