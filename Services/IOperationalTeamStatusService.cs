using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    internal interface IOperationalTeamStatusService
    {
        Task<int> AddStatus(OperationalTeamStatus status);
        Task<int> DeleteStatusAsync(OperationalTeamStatus status);
        Task<List<OperationalTeamStatus>> GetStatusesListAsync();
        Task<OperationalTeamStatus> GetStatusAsync(string name);
        Task<int> SaveStatusAsync(OperationalTeamStatus status);
        Task<int> UpdateStatusAsync(OperationalTeamStatus status);

    }
}
