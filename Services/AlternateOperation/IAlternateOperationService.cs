using UndacApp.Models;

namespace UndacApp.Services
{
    public interface IAlternateOperationService : IService<AlternateOperation>
    {
        Task<List<AlternateOperation>> GetOperationsByStatus(OperationStatus status);
    }
}
