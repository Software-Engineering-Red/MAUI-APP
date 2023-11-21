using UndacApp.Models;

/*! The IRoleService class allows for a service interface   
 *  to be used to run basic CRUD commands on a role from the RoleService class
 */

namespace UndacApp.Services
{
    public interface IRoleService : IService<Role>
    {

    }
}
