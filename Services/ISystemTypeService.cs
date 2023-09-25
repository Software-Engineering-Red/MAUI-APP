using MauiApp1.Data;
using MauiApp1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Services
{
    public interface ISystemTypeService
    {
        Task<List<SystemType>> GetSystemTypeList();
        Task<int> AddSystemType(SystemType systemType);
        Task<int> DeleteSystemType(SystemType systemType);
        Task<int> UpdateSystemType(SystemType systemType);
    }
}
