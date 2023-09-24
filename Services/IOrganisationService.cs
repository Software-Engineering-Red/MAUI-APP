using MauiApp1.Models;

namespace MauiApp1.Services
{
    public interface IOrganisationService
    {
        Task<List<Organisation>> GetOrganisationList();
        Task<int> AddOrganisation(Organisation org);
        Task<int> DeleteOrganisation(Organisation org);
        Task<int> UpdateOrganisation(Organisation org);
    }
}
