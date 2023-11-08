using UndacApp.Models;

namespace UndacApp.Services
{
    internal interface ILogisticsService
    {
        Task<List<LogisticsOperation>> GetAllOperations();
        Task<List<LogisticsOperation>> GetAllOperationsByVehicle(string value);
        Task<List<LogisticsOperation>> GetAllOperationsByEquip(string value);
        Task<List<LogisticsOperation>> GetDateOperations(DateTime start, DateTime end);
    }
}
