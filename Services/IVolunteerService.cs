using UndacApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public interface IVolunteerService
    {
        Task<List<Volunteer>> GetVolunteerList();
        Task<int> AddVolunteer(Volunteer volunteer);
        Task<int> DeleteVolunteer(Volunteer volunteer);
        Task<int> UpdateVolunteer(Volunteer volunteer);
        Task<int> FlagVolunteer(Volunteer volunteer);
        Task<int> ClearFlagVolunteer(Volunteer volunteer);
        Task<int> SendInvitationEmail(Volunteer volunteer, DateTime arrivalDate, DateTime departureDate);
    }
}
