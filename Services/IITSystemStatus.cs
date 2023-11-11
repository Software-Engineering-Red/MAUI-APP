using UndacApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public interface IITSystemStatus
    {
        Task<List<ITSystemStatus>> GetITSystemStatuses();
        Task<int> AddITSystemStatus(ITSystemStatus item);
        Task<int> DeleteITSystemStatus(ITSystemStatus item);
        Task<int> UpdateITSystemStatus(ITSystemStatus item);
    }
}
