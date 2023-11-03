using UndacApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public interface ISubTeamStatus
    {
        Task<List<SubTeamStatus>> GetSubTeamStatuses();
        Task<int> AddSubTeamStatus(SubTeamStatus subTeam);
        Task<int> DeleteSubTeamStatus(SubTeamStatus subTeam);
        Task<int> UpdateSubTeamStatus(SubTeamStatus subTeam);
    }
}
