using UndacApp.Data;
using UndacApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public class TeamMemberService : AService<TeamMember>, ITeamMemberService
    {
        public async Task<List<TeamMember>> GetAvailable()
        {
            return await _database.Table<TeamMember>().Where(t => t.Available).ToListAsync();
        }
    }
}
