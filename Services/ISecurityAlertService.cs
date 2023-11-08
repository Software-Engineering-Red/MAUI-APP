using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UndacApp.Models;

namespace UndacApp.Services
{
    public interface ISecurityAlertService
    {
        Task<List<SecurityAlert>> GetSecurityAlerts();
        Task<int> CreateSecurityAlert(SecurityAlert alert);
        Task<int> DeleteSecurityAlert(SecurityAlert alert);
        Task<int> UpdateSecurityAlert(SecurityAlert alert);
    }
}
