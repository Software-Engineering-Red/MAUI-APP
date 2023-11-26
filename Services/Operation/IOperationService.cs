using UndacApp.Models;

namespace UndacApp.Services
{
    public interface IOperationService : IService<Operation>
    {
        Task<List<Operation>> GetOperationsByStatus(OperationStatus status);
    }
}
