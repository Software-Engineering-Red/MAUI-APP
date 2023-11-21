using UndacApp.Data;
using UndacApp.Models;
using SQLite;

namespace UndacApp.Services
{
    public class AlertTypeService : AService<AlertType>, IAlertTypeService
    {

        public async Task<List<AlertType>> GetAlertTypesByStatus(string status)
        {
            return await _database.Table<AlertType>().Where(a => a.Status == status).ToListAsync();
        }
        
    }
}
