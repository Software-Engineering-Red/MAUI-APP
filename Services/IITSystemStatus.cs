using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface IITSystemStatus
    {
        Task<List<ITSystemStatus>> GetITSystemStatuses();
        Task<int> AddITSystemStatus(ITSystemStatus item);
        Task<int> DeleteITSystemStatus(ITSystemStatus item);
        Task<int> UpdateITSystemStatus(ITSystemStatus item);
    }
}
