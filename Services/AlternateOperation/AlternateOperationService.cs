using UndacApp.Data;
using UndacApp.Models;
using SQLite;

namespace UndacApp.Services
{
    public class AlternateOperationService : AService<AlternateOperation>, IAlternateOperationService
    {

        public async Task<List<AlternateOperation>> GetOperationsByStatus(OperationStatus status)
        {
            return await _database.Table<AlternateOperation>().Where(a => a.Status == status).ToListAsync();
        }
        
    }
}
