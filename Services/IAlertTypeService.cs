using MauiApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface IAlertTypeService
    {
        Task<List<AlertType>> GetAlertTypes();
        Task<int> AddAlertType(AlertType continent);
        Task<int> DeleteAlertType(AlertType continent);
        Task<int> UpdateAlertType(AlertType continent);
    }
}
