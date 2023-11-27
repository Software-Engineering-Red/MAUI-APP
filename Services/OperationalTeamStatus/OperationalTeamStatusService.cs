using UndacApp.Data;
using UndacApp.Models;
using SQLite;

namespace UndacApp.Services
{
    /// <summary>
    /// This class extending IOperationalTeamStatusService 
    /// </summary>
    public class OperationalTeamStatusService : AService<OperationalTeamStatus>, IOperationalTeamStatusService
    {
    }
}
