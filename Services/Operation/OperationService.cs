using UndacApp.Data;
using UndacApp.Models;
using SQLite;

namespace UndacApp.Services
{
    public class OperationService : AService<Operation>, IOperationService
    {

        public async Task<List<Operation>> GetOperationsByStatus(OperationStatus status)
        {
            return await _database.Table<Operation>().Where(a => a.Status == status).ToListAsync();
        }
        
    }
}
