// NeedService.cs
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using UndacApp.Data;
using UndacApp.Models;

namespace UndacApp.Services
{
    internal class NeedService : AService<Need>, INeedService
    {
        // Get the count of each priority
        public async Task<Dictionary<string, int>> GetPriorityCounts()
        {
            var needs = await _database.Table<Need>().ToListAsync();
            var priorityCounts = needs.GroupBy(n => n.Priority)
                                      .ToDictionary(group => group.Key, group => group.Count());
            return priorityCounts;
        }
    }
}
