using UndacApp.Models;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public class UsersService : AService<User>, IUsersService
    {
        public UsersService() : base()
        {
            // Additional initialization if required
        }

        // Implement any additional methods specific to the User entity
        // For now, all basic CRUD operations are inherited from AService<User>
    }
}
