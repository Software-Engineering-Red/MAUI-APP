// INeedService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using UndacApp.Models;

namespace UndacApp.Services
{
    internal interface INeedService
    {
        Task<List<Need>> GetAllNeeds();
        Task<int> AddNeed(Need need);
        Task<int> DeleteNeed(Need need);
        Task<int> UpdateNeed(Need need);
    }
}