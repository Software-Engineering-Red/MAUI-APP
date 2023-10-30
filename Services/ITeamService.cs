using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamList();
        Task<int> AddTeam(Team team);
        Task<int> DeleteTeam(Team team);
        Task<int> UpdateTeam(Team team);
    }
}