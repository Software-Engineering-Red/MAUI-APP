using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface ITeamMemberService
    {
        Task<List<Equipment>> GetTeamMemberList();
        Task<int> AddTeamMember(TeamMember teamMember);
        Task<int> DeleteTeamMember(TeamMember teamMember);
        Task<int> UpdateTeamMember(TeamMember teamMember);
    }
}