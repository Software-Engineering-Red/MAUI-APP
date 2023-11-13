using UndacApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndacApp.Services
{
    public interface IAlertTypeService
    {
        Task<List<AlertType>> GetAlertTypes();
        Task<int> AddAlertType(AlertType alertType);
        Task<int> DeleteAlertType(AlertType alertType);
        Task<int> DeleteAllAlertType();
        Task<int> UpdateAlertType(AlertType alertType);
        Task<List<AlertType>> GetAlertTypesByStatus(string status);
    }
}
