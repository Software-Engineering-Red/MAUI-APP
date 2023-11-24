using UndacApp.Models;

namespace UndacApp.Services {
    public interface IExpertService {
        Task<List<Expert>> GetExpertsList();
        Task<int> AddExpert(Expert expert);
        Task<int> DeleteExpert(Expert expert);
        Task<int> UpdateExpert(Expert expert);
    }
}
