using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface IRoleService
    {
        Task<List<Role>> GetRoleList();
        Task<int> AddRole(Role role);
        Task<int> DeleteRole(Role role);
        Task<int> UpdateRole(Role role);
    }
}