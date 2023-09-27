using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    internal interface IPositionStatusesServices
    {
        Task<List<position_statuses>> GetPosition_StatusesList();
        Task<int> AddStatus(position_statuses status);
        Task<int> DeleteStatus(position_statuses status);
        Task<int> UpdateStatus(position_statuses status);
    }
}
