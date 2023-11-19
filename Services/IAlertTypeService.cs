using UndacApp.Models;

namespace UndacApp.Services
{
    public interface IAlertTypeService : IService<AlertType>
    {
        Task<List<AlertType>> GetAlertTypesByStatus(string status);
    }
}
