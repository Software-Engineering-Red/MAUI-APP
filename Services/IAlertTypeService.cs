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
        Task<int> AddAlertType(AlertType continent);
        Task<int> DeleteAlertType(AlertType continent);
        Task<int> UpdateAlertType(AlertType continent);

        Task<List<AlertType>> GetAlertTypesByStatus(string status);



    }
}
