using UndacApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*! The IRoleService class allows for a service interface   
 *  to be used to run basic CRUD commands on a role from the RoleService class
 */

namespace UndacApp.Services
{
    public interface IRoleService
    {
        Task<List<Role>> GetRoleList();
        Task<int> AddRole(Role role);
        Task<int> DeleteRole(Role role);
        Task<int> UpdateRole(Role role);
    }
}
