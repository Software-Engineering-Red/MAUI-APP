// INeedService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using UndacApp.Models;

namespace UndacApp.Services
{
    internal interface INeedService: IService<Need>
    {
        Task<Dictionary<string, int>> GetPriorityCounts();
    }
}